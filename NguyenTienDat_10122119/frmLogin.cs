using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NguyenTienDat_10122119
{
    public partial class frmLogin : Form
    {
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
        public frmLogin()
        {
            InitializeComponent();
            this.bunifuPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bunifuPanel1_MouseDown);
            this.bunifuPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bunifuPanel1_MouseMove);
            this.bunifuPanel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bunifuPanel1_MouseUp);

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            frmSignUp frm = new frmSignUp();
            frm.ShowDialog();
            this.Close();
        }
        private const string filePath2 = "AllFormsState.json";
        private FormState currentState = new FormState();
        private void SaveFormState(bool accepted, string email)
        {

            // Tạo một đối tượng FormState để lưu trạng thái của form Recipient
            FormState currentState = new FormState
            {
                LockInterface = accepted,
                LockedComputer = false,
                MinutesText = email
            };

            Dictionary<string, FormState> allFormsState = LoadAllFormsState();
            allFormsState[GetType().Name] = currentState;

            string json = JsonConvert.SerializeObject(allFormsState, Formatting.Indented);
            File.WriteAllText(filePath2, json);
        }

        private Dictionary<string, FormState> LoadAllFormsState()
        {
            if (File.Exists(filePath2))
            {
                string json = File.ReadAllText(filePath2);
                return JsonConvert.DeserializeObject<Dictionary<string, FormState>>(json);
            }
            return new Dictionary<string, FormState>();
        }

        clsDatabase cls = new clsDatabase();
        private void btnLogIn2_Click(object sender, EventArgs e)
        {
            string enteredEmail = txtEmail.Text;
            string enteredPassword = txtPassword.Text;

            bool accepted= cls.getPassword(enteredEmail, enteredPassword);

            if (accepted)
            {
                SaveFormState(true, enteredEmail);
                this.Hide();
                
            }
            else
            {
                SaveFormState(false, "");
                MessageBox.Show("Đăng nhập không thành công. Vui lòng kiểm tra lại Email và Password!");
                        
            }

            if (chkAutoLogin.Checked)
            {
                SaveFormStateAutoLogin();
            }
            this.Close();
        }
        public void Checked_AutoLogin()
        {
            if(chkAutoLogin.Checked)
            {
                SaveFormStateAutoLogin();
            }
            else
            {
                File.WriteAllText(filePath3, string.Empty);
            }
        }
        

        private void chkAutoLogin_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (chkAutoLogin.Checked)
            {
                SaveFormStateAutoLogin();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            LoadFormStateAutoLogin();
        }

        private void btnForgetPass_Click(object sender, EventArgs e)
        {
            frmForgetPassword frm = new frmForgetPassword();
            frm.ShowDialog();
            this.Close();
        }
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;
        private void bunifuPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastCursor = Cursor.Position;
            lastForm = this.Location;
        }

        private void bunifuPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = Cursor.Position.X - lastCursor.X;
                int deltaY = Cursor.Position.Y - lastCursor.Y;
                this.Location = new Point(lastForm.X + deltaX, lastForm.Y + deltaY);
            }
        }

        private void bunifuPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
        
        /// <Save data moi khi thay doi>
        string filePath3 = "AutoLogin.json";
        private void SaveFormStateAutoLogin()
        {
            AutoLogin currentState = new AutoLogin()
            {
                autologining = chkAutoLogin.Checked,
                email = txtEmail.Text.ToString(),
                password = txtPassword.Text.ToString()
            };

            Dictionary<string, AutoLogin> allFormsState = LoadAllFormsStateAutoLogin();
            allFormsState[GetType().Name] = currentState;

            string json = JsonConvert.SerializeObject(allFormsState, Formatting.Indented);
            File.WriteAllText(filePath3, json);
        }

        private void LoadFormStateAutoLogin()
        {
            Dictionary<string, AutoLogin> allFormsState = LoadAllFormsStateAutoLogin();
            string formName = GetType().Name;

            if (allFormsState != null && allFormsState.ContainsKey(formName))
            {
                AutoLogin currentState = allFormsState[formName];

                chkAutoLogin.Checked = currentState.autologining;
                txtEmail.Text = currentState.email;
                txtPassword.Text = currentState.password;
            }
            else
            {
            }
        }


        private Dictionary<string, AutoLogin> LoadAllFormsStateAutoLogin()
        {
            if (File.Exists(filePath3))
            {
                string json = File.ReadAllText(filePath3);
                return JsonConvert.DeserializeObject<Dictionary<string, AutoLogin>>(json);
            }
            return new Dictionary<string, AutoLogin>();
        }
    }
}
