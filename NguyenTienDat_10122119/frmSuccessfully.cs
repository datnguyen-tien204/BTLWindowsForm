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
using System.Diagnostics;

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
        clsDatabase clsDatabase = new clsDatabase();

        private void frmSuccessfully_Load(object sender, EventArgs e)
        {
           
            pnlMore.Visible = false;
            string jsonFilePath = "AllFormsState.json";
            string jsonString = File.ReadAllText(jsonFilePath);
            dynamic jsonData = JsonConvert.DeserializeObject(jsonString);

            bool accepted = jsonData.frmLogin.LockInterface;
            string email = jsonData.frmLogin.MinutesText;

            LoadFormState();
            if(accepted==true&&email!="")
            {
                User[] usersInfo = clsDatabase.GetUserInfoFromDatabase(email);

                foreach (User user in usersInfo)
                {
                    lblEmail.Text = user.Email;
                    lblName.Text = user.Name;
                }
            }

            frmMain mainForm = this.ParentForm as frmMain;
            if (mainForm != null)
            {
                mainForm.EnableBtnSetting(false, "btnConnect");
                mainForm.EnableBtnSetting(false, "btnSetting");
                mainForm.EnableBtnSetting(false, "btnDevice");
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

        private void btnManagementCenter_Click(object sender, EventArgs e)
        {
            string facebookLink = "https://github.com/datnguyen-tien204";

            try
            {
                // Mở đường link Facebook trong trình duyệt mặc định
                Process.Start(facebookLink);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't open link. Error: " + ex.Message);
            }
        }

        private void btnChangeUsername_Click(object sender, EventArgs e)
        {
            frmChangeUserName frmChangeUserName = new frmChangeUserName();
            frmChangeUserName.ShowDialog();
        }

        private void btnEditProfilePicture_Click(object sender, EventArgs e)
        {
            String imageLocation = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files(*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    string destinationPath = Path.Combine(Application.StartupPath, Path.GetFileName(imageLocation));
                    File.Copy(imageLocation, destinationPath, true);
                    imgProfile.ImageLocation = destinationPath;
                    SaveFormState(destinationPath);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("An error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string imageLocation = "";

        private void imgProfile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files(*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    string destinationPath = Path.Combine(Application.StartupPath, Path.GetFileName(imageLocation));
                    File.Copy(imageLocation, destinationPath, true);
                    imgProfile.ImageLocation = destinationPath;
                    SaveFormState(destinationPath);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frmChangePassword= new frmChangePassword();
            frmChangePassword.ShowDialog();
        }
        private const string filePath2 = "AllFormsState.json";
        private FormState currentState = new FormState();
        /// <Save data moi khi thay doi>
        private void SaveFormState(string path)
        {
            FormState currentState = new FormState
            {
                LockInterface = false,
                LockedComputer = false,
                MinutesText = path
            };

            Dictionary<string, FormState> allFormsState = LoadAllFormsState2();
            allFormsState[GetType().Name] = currentState;

            string json = JsonConvert.SerializeObject(allFormsState, Formatting.Indented);
            File.WriteAllText(filePath2, json);
        }

        private void LoadFormState()
        {
            Dictionary<string, FormState> allFormsState = LoadAllFormsState2();
            string formName = GetType().Name;

            if (allFormsState.ContainsKey(formName))
            {
                FormState currentState = allFormsState[formName];

                imgProfile.ImageLocation = currentState.MinutesText;
            }
        }
        private Dictionary<string, FormState> LoadAllFormsState2()
        {
            if (File.Exists(filePath2))
            {
                string json = File.ReadAllText(filePath2);
                return JsonConvert.DeserializeObject<Dictionary<string, FormState>>(json);
            }
            return new Dictionary<string, FormState>();
        }
    }
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
