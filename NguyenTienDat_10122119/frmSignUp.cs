using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
//using WindowsFormsControlLibrary1;

namespace NguyenTienDat_10122119
{
    public partial class frmSignUp : Form
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
        public frmSignUp()
        {
            InitializeComponent();
            this.bunifuPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bunifuPanel1_MouseDown);
            this.bunifuPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bunifuPanel1_MouseMove);
            this.bunifuPanel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bunifuPanel1_MouseUp);

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public bool IsEmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private string GetRandomString(int length)
        {
            Random random = new Random();
            string code = "";

            for (int i2 = 0; i2 < length; i2++)
            {
                int randomNumber = random.Next(0, 10); 
                code += randomNumber.ToString();
            }

            return code;

        }
        private string codeSent;

        private void btnSend_Click(object sender, EventArgs e)
        {

            if (txtEmail.Text == "")
            {
                MessageBox.Show("Enter your email","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else if (IsEmailValid(txtEmail.Text) == false)
            {
                MessageBox.Show("Enter your email", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string s = GetRandomString(6);
                Console.WriteLine(s);
                codeSent = s;

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("nguyendat130504@gmail.com");
                    mail.To.Add(txtEmail.Text);
                    mail.Subject = "Confirm your email address";

                    string boldText = $"<strong>{s}</strong>";
                    string mailBody = $"Your confirmation code is below — enter it in the browser window where you’ve started signing up for CharriseElaina Viewer.<br><br>{boldText}<br><br>Questions about setting up CharriseElaina Viewer? Email us at ceviewer@ce.com<br><br>If you didn’t request this email, there’s nothing to worry about — you can safely ignore it.";

                    // Tạo phần nội dung HTML chứa hình ảnh
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                        "<html><body><img src=\"cid:image1\">" +
                        "<br><br>" +
                        mailBody +
                        "</body></html>",
                        null, "text/html");

                    // Thêm hình ảnh vào email
                    LinkedResource img = new LinkedResource("imageEmail.png", "image/jpeg");
                    img.ContentId = "image1";
                    htmlView.LinkedResources.Add(img);

                    // Gán phần nội dung HTML vào email
                    mail.AlternateViews.Add(htmlView);
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("nguyendat130504@gmail.com", "niir timf rxkd hxbb");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                btnSend.Text = "Resend";
            }

        }
        clsDatabase clsDatabase = new clsDatabase();

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            Console.WriteLine(codeSent);
            if (txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if(txtSecureCode.Text!=codeSent)
            {
                MessageBox.Show("Wrong secure code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
            if(txtName.Text == "")
            {
                MessageBox.Show("Enter your name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Console.WriteLine(txtEmail.Text);
                Console.WriteLine(txtName);
                Console.WriteLine(txtPassword);
                clsDatabase.InsertData(txtName.Text.Trim(), txtEmail.Text.Trim(), txtPassword.Text.Trim());
                this.Close();
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
            frmLogin.BringToFront();
            this.Close();
        }

        private void frmSignUp_Load(object sender, EventArgs e)
        {

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

        private void picVeryWeak_Click(object sender, EventArgs e)
        {

        }

        private void picWeak_Click(object sender, EventArgs e)
        {

        }

        private void picMedium_Click(object sender, EventArgs e)
        {

        }

        private void picStrong_Click(object sender, EventArgs e)
        {

        }

        private void picVeryStrong_Click(object sender, EventArgs e)
        {

        }

        private void lblStrength_Click(object sender, EventArgs e)
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


        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            String password = txtPassword.Text; // Substitute with the user input string
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

        private void txtPassword_Load(object sender, EventArgs e)
        {
            picMedium.Visible = false;
            picStrong.Visible = false;
            picVeryStrong.Visible = false;
            picVeryWeak.Visible = false;
            picWeak.Visible = false;
            lblStrength.Text="";
        }
    }
}
