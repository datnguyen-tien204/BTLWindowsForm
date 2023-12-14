﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NguyenTienDat_10122119
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
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

                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập không thành công. Vui lòng kiểm tra lại Email và Password!");
                    }
                }
            }
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
            this.Close();
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

        private void frmLogin_Load(object sender, EventArgs e)
        {
            LoadFormState();
        }
    }
}