using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace NguyenTienDat_10122119
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();

            
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
        private const string filePath = "AutoLogin.txt";
        protected string connStr = @"Data Source=NGUYENTIENDAT;Initial Catalog=RemoteDesktop;Integrated Security=True";
        public string getTime()
        {
            string r;
            DateTime currentTime = DateTime.Now;
            if (currentTime.Hour >= 5 && currentTime.Hour < 12)
            {
                r= "Good Morning";
            }
            else if(currentTime.Hour>=12 && currentTime.Hour<18)
            {
                r="Good Afternoon";
            }
            else 
            {
                r="Good Evening"; 
            }
            return r;
           
        }
        public void getName()
        {
            string EmailGetfromSettings=Properties.Settings.Default.ChaoMung;
            string dau=getTime();
            using (SqlConnection sqlCon = new SqlConnection(connStr))
            {
                sqlCon.Open();
                using (SqlCommand sqlCom = new SqlCommand($"select * from Account where Email='{EmailGetfromSettings}'", sqlCon))
                {
                    using (SqlDataReader sqlRe = sqlCom.ExecuteReader())
                    {
                        while (sqlRe.Read())
                        {
                            string Email = sqlRe[0].ToString();
                            string Password = sqlRe[1].ToString();
                            string Name = sqlRe[2].ToString();
                            lblChaoMung.Text= dau+ ", "+ Name;
                        }
                    }
                }
                sqlCon.Close();
            }
           
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            Init_CustomLabel_Font();
            frmMain mainForm = this.ParentForm as frmMain;
            if (mainForm != null)
            {
                mainForm.EnableBtnSetting(false, "btnConnect");
                mainForm.EnableBtnSetting(false, "btnSetting");
                mainForm.EnableBtnSetting(false, "btnDevice");
                mainForm.EnableBtnSetting(false, "btnProfile");
            }

            lblChaoMung.Font = new Font(pfc.Families[0], lblChaoMung.Font.Size,FontStyle.Bold);
            
            getName();
            //lblChaoMung.Text = "Welcome Đạt Nguyễn,";
        }
    }
}
