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

namespace NguyenTienDat_10122119
{
    public partial class frmMain : Form
    {
        private Timer timer;
        private UserControl currentControl;
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;
        public frmMain()
        {
            InitializeComponent();
            InitializeTimer();
            Init_CustomLabel_Font();
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            lblWelcome.Font = new Font(pfc.Families[0], lblWelcome.Font.Size);
            lblWelcome.Text = "Welcome Đạt Nguyễn,";
            btnDashboard.Select();
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
            frm.Show();
        }
        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            if((MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question))==DialogResult.Yes)
            {
                   Application.Exit();
            }    
            
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
        /*
        public void LoadForm(Object Form)
        {
            if(this.pnlMain.Controls.Count > 0)
            {
                this.pnlMain.Controls.RemoveAt(0);
            }
            Form frm = Form as Form;
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            this.pnlMain.Controls.Add(frm);
            this.pnlMain.Tag = frm;
            frm.Show();
        }
        */

       

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
            load_form(new frmDevice());
        }

        private void picInternet_Click(object sender, EventArgs e)
        {

        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }
    }
}
