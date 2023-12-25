using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;

namespace NguyenTienDat_10122119
{
    public enum PasswordScore
    {
        Blank = 0,
        VeryWeak = 1,
        Weak = 2,
        Medium = 3,
        Strong = 4,
        VeryStrong = 5
    }
    public partial class frmChangePassword : Form
    {
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

        public frmChangePassword()
        {
            InitializeComponent();

            this.bunifuPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bunifuPanel1_MouseDown);
            this.bunifuPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bunifuPanel1_MouseMove);
            this.bunifuPanel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bunifuPanel1_MouseUp);

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
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

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {

        }
        public static PasswordScore CheckStrength(string password)
        {
            int score = 0;

            if (password.Length < 1)
                return PasswordScore.Blank;
            if (password.Length < 4)
                return PasswordScore.VeryWeak;

            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;
            if (Regex.IsMatch(password, @"\d")) 
                score++;
            if (Regex.IsMatch(password, @"[a-z]") && Regex.IsMatch(password, @"[A-Z]"))
                score++;
            if (Regex.IsMatch(password, @"[!@#$%^&*?_~\-£()]"))
                score++;

            return (PasswordScore)score;

        }
        public string readEmail()
        {
            string jsonFilePath = "AllFormsState.json";
            string jsonString = File.ReadAllText(jsonFilePath);
            dynamic jsonData = JsonConvert.DeserializeObject(jsonString);

            bool accepted = jsonData.frmLogin.LockInterface;
            string email = jsonData.frmLogin.MinutesText;
            if (accepted == true && email != "")
            {
                return email;
            }
            else
            {
                   return "";
            }             
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            picMedium.Visible = false;
            picStrong.Visible = false;
            picVeryStrong.Visible = false;
            picVeryWeak.Visible = false;
            picWeak.Visible = false;
            lblStrength.Text = "Blank";
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            String password = txtNewPassword.Text; // Substitute with the user input string
            PasswordScore passwordStrengthScore = CheckStrength(password);

            switch (passwordStrengthScore)
            {
                case PasswordScore.Blank:
                    {
                        picMedium.Visible = false;
                        picStrong.Visible = false;
                        picVeryStrong.Visible = false;
                        picVeryWeak.Visible = false;
                        picWeak.Visible = false;
                        lblStrength.Text = "Blank";
                    }
                    break;
                case PasswordScore.VeryWeak:
                    {
                        picMedium.Visible = false;
                        picStrong.Visible = false;
                        picVeryStrong.Visible = false;
                        picVeryWeak.Visible = true;
                        picWeak.Visible = false;
                        lblStrength.Text = "Very Weak";
                    }
                    break;
                case PasswordScore.Weak:
                    {
                        picMedium.Visible = false;
                        picStrong.Visible = false;
                        picVeryStrong.Visible = false;
                        picVeryWeak.Visible = true;
                        picWeak.Visible = true;
                        lblStrength.Text = "Weak";
                    }
                    break;
                case PasswordScore.Medium:
                    {
                        picMedium.Visible = true;
                        picStrong.Visible = false;
                        picVeryStrong.Visible = false;
                        picVeryWeak.Visible = true;
                        picWeak.Visible = true;
                        lblStrength.Text = "Medium";
                    }
                    break;
                case PasswordScore.Strong:
                    {
                        picMedium.Visible = true;
                        picStrong.Visible = true;
                        picVeryStrong.Visible = false;
                        picVeryWeak.Visible = true;
                        picWeak.Visible = true;
                        lblStrength.Text = "Strong";
                    }
                    break;
                case PasswordScore.VeryStrong:
                    {
                        picMedium.Visible = true;
                        picStrong.Visible = true;
                        picVeryStrong.Visible = true;
                        picVeryWeak.Visible = true;
                        picWeak.Visible = true;
                        lblStrength.Text = "Very Strong";
                    }
                    break;
            }
        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }
        clsDatabase cls = new clsDatabase();

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if(txtNewPassword.Text==""||txtConfirm.Text=="")
            {
                MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(txtNewPassword.Text!=txtConfirm.Text)
            {
                MessageBox.Show("Confirm password is not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string email = readEmail();
                bool k = cls.getPassword(email, txtOldPassword.Text);
                if(k==false)
                {
                    MessageBox.Show("Old password is not correct!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    cls.UpdatePassword(email, txtNewPassword.Text);
                    MessageBox.Show("Change password successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }           
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
