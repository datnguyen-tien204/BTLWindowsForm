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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
            string jsonFilePath = "AllFormsState.json";
            string jsonString = File.ReadAllText(jsonFilePath);
            dynamic jsonData = JsonConvert.DeserializeObject(jsonString);

            bool accepted = jsonData.frmLogin.LockInterface;
            string email = jsonData.frmLogin.MinutesText;
            if(accepted==true&&email!="")
            {
                using (SqlConnection sqlCon = new SqlConnection(connStr))
                {
                    sqlCon.Open();
                    using (SqlCommand sqlCom = new SqlCommand($"select * from Account where Email='{email}'", sqlCon))
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

            
        }
        private const string filePath = "AutoLogin.txt";


        private void btnLogOut_Click(object sender, EventArgs e)
        {
            ModifyLoginFormState();
        }
        private const string filePath3 = "AllFormsState.json";

        private void ModifyLoginFormState()
        {
            string json = File.ReadAllText(filePath3);
            JObject data = JObject.Parse(json);
            if (data.ContainsKey("frmLogin"))
            {
                JObject frmLoginData = (JObject)data["frmLogin"];
                frmLoginData["LockInterface"] = false; 
                frmLoginData["MinutesText"] = ""; 
            }

            File.WriteAllText(filePath3, data.ToString());
        }

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {

        }
    }
}
