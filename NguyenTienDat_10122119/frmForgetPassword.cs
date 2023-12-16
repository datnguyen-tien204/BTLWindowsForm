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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace NguyenTienDat_10122119
{
    public partial class frmForgetPassword : Form
    {
        public frmForgetPassword()
        {
            InitializeComponent();
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
        private bool getEmailAvailable(string email)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM Account WHERE Email = @Email";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@Email", email);

                    int existingEmailCount = (int)checkCommand.ExecuteScalar();

                    if (existingEmailCount > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Enter your email", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (IsEmailValid(txtEmail.Text) == false)
            {
                MessageBox.Show("Enter your email", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (getEmailAvailable(txtEmail.Text.Trim()) == false)
                {
                    MessageBox.Show("Email not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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
                        string mailBody = $"Your reset password confirmation code is below — enter it in the form window where you’ve reseted password for CharriseElaina Viewer.<br><br>{boldText}<br><br>Questions about setting up CharriseElaina Viewer? Email us at ceviewer@ce.com<br><br>If you didn’t request this email, there’s nothing to worry about — you can safely ignore it.";

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
        }
        protected string connStr = @"Data Source=NGUYENTIENDAT;Initial Catalog=RemoteDesktop;Integrated Security=True";
        private void CheckData(string email, string newPassword)
        {
            if (getEmailAvailable(email) == false)
            {
                MessageBox.Show("Email not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string updateQuery = "UPDATE Account SET Password = @NewPassword WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(updateQuery, conn))
                    {
                        command.Parameters.AddWithValue("@NewPassword", newPassword);
                        command.Parameters.AddWithValue("@Email", email);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Password changed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            Console.WriteLine(codeSent);
            if (txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (txtSecureCode.Text != codeSent)
            {
                MessageBox.Show("Wrong secure code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Console.WriteLine(txtEmail.Text);
                Console.WriteLine(txtPassword);
                CheckData(txtEmail.Text, txtPassword.Text);
                this.Close();
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            frmSignUp frmSignUp = new frmSignUp();
            frmSignUp.Show();
            this.Close();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
            this.Close();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            frmMain frmMain = new frmMain();
            frmMain.Show();
            this.Close();
        }

        private void frmForgetPassword_Load(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.Close();
        }
    }
}
