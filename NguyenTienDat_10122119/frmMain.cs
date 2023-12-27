using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Win32;
using Bunifu.UI.WinForms;

namespace NguyenTienDat_10122119
{
    public partial class frmMain : Form
    {
        private Timer timer;
        private UserControl currentControl;
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

        /// <Form Radius>
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );
        /// </End>
        private bool moreButtonClicked = false;
        public frmMain()
        {
            InitializeComponent();
            InitializeTimer();
            Init_CustomLabel_Font();
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        public Bunifu.UI.WinForms.BunifuButton.BunifuButton BtnConnect_Control
        {
            get { return btnConnect; }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            load_form(new frmConnect());
        }
        
        private void btnSetting_Click(object sender, EventArgs e)
        {

            load_form(new frmSettings());
        }

       
        public void load_form(object Form)
        {
            if (this.pnlMain.Controls.Count > 0)
            {
                this.pnlMain.Controls.RemoveAt(0);
            }
            Form frm = Form as Form;
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            this.pnlMain.Controls.Add(frm);
            this.pnlMain.Tag = frm;
            //frm.Owner = this;
            frm.Show();
        }
        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            if((MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question))==DialogResult.Yes)
            {
                   RestoreSleep();
                   Application.Exit();
                    
            }
            frmLogin mainForm = this.ParentForm as frmLogin;
            if (mainForm != null)
            {
                mainForm.Checked_AutoLogin();
            }
            


        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            frmConnect settingsForm = new frmConnect();
            settingsForm.TopLevel = false;
            settingsForm.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(settingsForm);
            settingsForm.Show();
            moreButtonClicked = false;
        }

       
        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 5000; 
            timer.Tick += Timer_Tick;
            timer.Start(); 
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double networkSpeed = GetNetworkSpeed();
            SetNetworkQualityIcon(networkSpeed);
        }

        private double GetNetworkSpeed()
        {
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                DateTime dt1 = DateTime.UtcNow;
                byte[] data = wc.DownloadData("http://google.com");
                DateTime dt2 = DateTime.UtcNow;
                return Math.Round((data.Length / 1024) / (dt2 - dt1).TotalSeconds, 2);
            }
            catch 
            {
                return 0;
            }
        }

        private void SetNetworkQualityIcon(double networkSpeed)
        {
            Icon icon = null;

            if (networkSpeed > 100)
            {
                icon = Icon.FromHandle(Properties.Resources.StrongInternet.GetHicon());
            }
            else if (networkSpeed > 50)
            {
                icon = Icon.FromHandle(Properties.Resources.MediumInternet.GetHicon());
            }
            else if (networkSpeed > 0)
            {
                icon = Icon.FromHandle(Properties.Resources.LowInternet.GetHicon());
            }
            else if (networkSpeed == 0)
            {
                icon = Icon.FromHandle(Properties.Resources.InternetLoss.GetHicon());
            }

            SetPictureBoxImage(icon);
        }

        private void SetPictureBoxImage(Icon icon)
        {
            if (icon != null)
            {
                picInternet.Image = icon.ToBitmap();
            }
        }     

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastCursor = Cursor.Position;
            lastForm = this.Location;
        }
        private PrivateFontCollection pfc;
        private void Init_CustomLabel_Font()
        {
             pfc = new PrivateFontCollection();

            
            int fontLength = Properties.Resources.BeautiqueDisplayCondensed_Black.Length;
            byte[] fontdata = Properties.Resources.BeautiqueDisplayCondensed_Black;
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);
        }
        
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = Cursor.Position.X - lastCursor.X;
                int deltaY = Cursor.Position.Y - lastCursor.Y;
                this.Location = new Point(lastForm.X + deltaX, lastForm.Y + deltaY);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void btnDevice_Click(object sender, EventArgs e)
        {

            
            

            btnConnect.Enabled = true;
            btnDevice.BackColor=Color.DarkBlue;
            string jsonFilePath = "AllFormsState.json";
            string jsonString = File.ReadAllText(jsonFilePath);
            dynamic jsonData = JsonConvert.DeserializeObject(jsonString);

            bool accepted = jsonData.frmLogin.LockInterface;
            if (accepted == true)
            {
                load_form(new frmDevice());
            }
            else
            {
                load_form(new frmDeviceDisable());
            }         
        }
        private void btnProfile_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = true;
            string jsonFilePath = "AllFormsState.json";
            string jsonString = File.ReadAllText(jsonFilePath);
            dynamic jsonData = JsonConvert.DeserializeObject(jsonString);

            bool accepted = jsonData.frmLogin.LockInterface;
            if (accepted == true)
            {
                load_form(new frmSuccessfully());
            }
            else
            {
                load_form(new frmProfile());
            }
        }
        public void EnableBtnSetting(bool enabled,string button)
        {
            if (button == "btnSetting")
            {
                if (enabled)
                {
                    btnSetting.IdleIconLeftImage = Properties.Resources.setting_dam;
                    btnSetting.Refresh();
                }
                else
                {
                    btnSetting.IdleIconLeftImage = Properties.Resources.setting_nhat;
                    btnSetting.Refresh();
                }
            }
            else if (button == "btnConnect")
            {
                if (enabled)
                {
                    btnConnect.IdleIconLeftImage = Properties.Resources.remote_dam;
                    btnConnect.Refresh();
                }
                else
                {
                    btnConnect.IdleIconLeftImage = Properties.Resources.remote_nhat;
                    btnConnect.Refresh();
                }
            }
            else if (button == "btnDevice")
            {
                if (enabled)
                {
                    btnDevice.IdleIconLeftImage = Properties.Resources.device_dam;
                    btnDevice.Refresh();
                }
                else
                {
                    btnDevice.IdleIconLeftImage = Properties.Resources.device_nhat;
                    btnDevice.Refresh();
                }
            }
            else if (button == "btnProfile")
            {
                if (enabled)
                {
                    btnProfile.IdleIconLeftImage = Properties.Resources.user_dam;
                    btnProfile.Refresh();
                }
                else
                {
                    btnProfile.IdleIconLeftImage = Properties.Resources.user_nhat;
                    btnProfile.Refresh();
                }
            }   
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Sleep();
            label3.Font = new Font(pfc.Families[0], label3.Font.Size,FontStyle.Bold);
            btnConnect.PerformClick();

            string jsonFilePath = "AllFormsState.json";
            string jsonString = File.ReadAllText(jsonFilePath);
            dynamic jsonData = JsonConvert.DeserializeObject(jsonString);

            bool lockInterfaceOfFrmBasic = jsonData.frmBasic.LockInterface;

            if (lockInterfaceOfFrmBasic == true)
            {
                RegistryKey reg= Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                reg.SetValue("Dat's Viewer", Application.ExecutablePath.ToString());
            }
            else
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                if (lockInterfaceOfFrmBasic && reg != null)
                {
                    if (reg.GetValue("Dat's Viewer") != null)
                    {
                        reg.DeleteValue("Dat's Viewer");
                    }
                }
            }

        }
        /// <Custom Sleep for Forms>
        [DllImport("kernel32.dll")]
        static extern uint SetThreadExecutionState(uint esFlags);

        const uint EXECUTION_STATE_SYSTEM_REQUIRED = 0x00000001;
        const uint EXECUTION_STATE_CONTINUOUS = 0x80000000;
        private void PreventSleep()
        {
            SetThreadExecutionState(EXECUTION_STATE_SYSTEM_REQUIRED | EXECUTION_STATE_CONTINUOUS);
        }

        private void RestoreSleep()
        {
            SetThreadExecutionState(EXECUTION_STATE_CONTINUOUS);
        }

        private void Sleep()
        {
            string jsonFilePath = "AllFormsState.json";
            string jsonString = File.ReadAllText(jsonFilePath);
            dynamic jsonData = JsonConvert.DeserializeObject(jsonString);

            bool lockInterfaceOfFrmBasic = jsonData.frmBasic.LockedComputer;
            if (lockInterfaceOfFrmBasic == true)
            {
                PreventSleep();
            }
            else
            {
               RestoreSleep();
            }
        }
        /// </End>
        /// <param name="esFlags"></param>
        /// <returns></returns>

        private void picInternet_Click(object sender, EventArgs e)
        {

        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            RestoreSleep();
        }

    }
}
