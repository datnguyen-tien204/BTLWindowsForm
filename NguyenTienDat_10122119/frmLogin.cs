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

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string enteredEmail = txtEmail.Text;
            string enteredPassword = txtPassword.Text;

            string connStr = @"Data Source=NGUYENTIENDAT;Initial Catalog=RemoteDesktop;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM Account WHERE Email = @Email AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Email", enteredEmail);
                    command.Parameters.AddWithValue("@Password", enteredPassword);

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        this.Hide();
                        string Email= enteredEmail;
                        Properties.Settings.Default.ChaoMung = Email;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        string Email = "";
                        Properties.Settings.Default.ChaoMung = Email;
                        Properties.Settings.Default.Save();
                        MessageBox.Show("Đăng nhập không thành công. Vui lòng kiểm tra lại Email và Password!");
                    }
                }
            }
            if(chkAutoLogin.Checked)
            {
                SaveFormState();
            }
            this.Close();
        }
        public void Checked_AutoLogin()
        {
            if(chkAutoLogin.Checked)
            {
                SaveFormState();
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("");
                }
            }
        }
        private const string filePath = "AutoLogin.txt";
        private void SaveFormState()
        {
            string Email = txtEmail.Text;
            string Password = txtPassword.Text;
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(chkAutoLogin.Checked);
                writer.WriteLine(Email);
                writer.WriteLine(Password);
            }
        }
        private void LoadFormState()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length >= 3) 
                {
                    bool autoLogin = Convert.ToBoolean(lines[0]);
                    string savedEmail = lines[1];
                    string savedPassword = lines[2];

                    txtEmail.Text = savedEmail;
                    txtPassword.Text = savedPassword;
                    chkAutoLogin.Checked = autoLogin;

                    btnLogIn.PerformClick();
                }
                else
                {
                    
                }
            }
            else
            {
            }
        }

        private void chkAutoLogin_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (chkAutoLogin.Checked)
            {
                string Email = txtEmail.Text;
                string Password = txtPassword.Text;
                Properties.Settings.Default.Email = Email;
                Properties.Settings.Default.Password = Password;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Email = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
            }    
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            LoadFormState();
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
    }
}
