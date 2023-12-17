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
using WindowsFormsControlLibrary1;

namespace NguyenTienDat_10122119
{
    public partial class frmSignUp : Form
    {
        public frmSignUp()
        {
            InitializeComponent();
            this.bunifuPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bunifuPanel1_MouseDown);
            this.bunifuPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bunifuPanel1_MouseMove);
            this.bunifuPanel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bunifuPanel1_MouseUp);
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
        protected string connStr = @"Data Source=NGUYENTIENDAT;Initial Catalog=RemoteDesktop;Integrated Security=True";

        private void InsertData(string name, string email, string password)
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
                        MessageBox.Show("Email already exists. Please add another email or select forgot password to recover your account!","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        txtEmail.Text = "";
                        txtPassword.Text = "";
                        txtName.Text = "";
                        txtSecureCode.Text = "";
                        return; 
                    }
                }

                string insertQuery = "INSERT INTO Account (Email, Password, Name) VALUES (@Email, @Password, @Name)";
                using (SqlCommand command = new SqlCommand(insertQuery, conn))
                {
                    command.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Name", name);

                    command.ExecuteNonQuery();
                }
            }
        }


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
                InsertData(txtName.Text, txtEmail.Text, txtPassword.Text);
                this.Close();
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
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
    }
}
