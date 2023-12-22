using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace NguyenTienDat_10122119
{
    public partial class frmSuccessfully : Form
    {
        public frmSuccessfully()
        {
            InitializeComponent();
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            if(pnlMore.Visible == false)
            {
                pnlMore.Visible = true;
                pnlMore.BringToFront();
            }
            else
            {
                pnlMore.Visible = false;
            }
        }
        protected string connStr = @"Data Source=NGUYENTIENDAT;Initial Catalog=RemoteDesktop;Integrated Security=True";

        private void frmSuccessfully_Load(object sender, EventArgs e)
        {
           
            pnlMore.Visible = false;
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length >= 3)
                {
                    bool autoLogin = Convert.ToBoolean(lines[0]);
                    string savedEmail = lines[1];
                    string savedPassword = lines[2];
                    string result = string.Empty;
                  
                    using (SqlConnection sqlCon = new SqlConnection(connStr))
                    {
                        sqlCon.Open();

                        // Create SqlCommand with connection
                        using (SqlCommand sqlCom = new SqlCommand($"select * from Account where Email='{savedEmail}'", sqlCon))
                        {
                            using (SqlDataReader sqlRe = sqlCom.ExecuteReader())
                            {
                                while (sqlRe.Read())
                                {
                                    string Email = sqlRe[0].ToString();
                                    string Password = sqlRe[1].ToString();
                                    string Name = sqlRe[2].ToString();
                                    lblEmail.Text = Email;
                                    lblName.Text = Name;                                     
                                }
                                
                            }
                        }
                        sqlCon.Close(); 
                    }

                }
                else
                {

                }
            }
            else
            {
            }
            

        }
        private const string filePath = "AutoLogin.txt";

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            try
            {

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write("");
                }
                

            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
