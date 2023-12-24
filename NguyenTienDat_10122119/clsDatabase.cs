using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NguyenTienDat_10122119
{
    internal class clsDatabase
    {
        public string LoadConnectionString()
        {
            try
            {
                string jsonFilePath = "sqlConnection.json";
                if (File.Exists(jsonFilePath))
                {
                    string jsonText = File.ReadAllText(jsonFilePath);
                    dynamic jsonData = JsonConvert.DeserializeObject(jsonText);

                    string dataSource = jsonData["Data Source"];
                    string initialCatalog = jsonData["Initial catalog"];

                    string sqlCon = $"Data Source={dataSource};Initial Catalog={initialCatalog};Integrated Security=True";

                    return sqlCon;
                }
                else
                {
                    return null;
                    
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public int ConfirmLogin(string enteredEmail, string enteredPassword)
        {
            string sqlCon= LoadConnectionString();
            using (SqlConnection conn = new SqlConnection(sqlCon))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM Account WHERE Email = @Email AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Email", enteredEmail);
                    command.Parameters.AddWithValue("@Password", enteredPassword);

                    int count = (int)command.ExecuteScalar();
                    Console.WriteLine(count);
                    return count;

                }
            }
            
        }
        public void InsertData(string name, string email, string password)
        {
            string sqlCon = LoadConnectionString();
            using (SqlConnection conn = new SqlConnection(sqlCon))
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM Account WHERE Email = @Email";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@Email", email);

                    int existingEmailCount = (int)checkCommand.ExecuteScalar();

                    if (existingEmailCount > 0)
                    {
                        MessageBox.Show("Email already exists. Please add another email or select forgot password to recover your account!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Sign up successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void UpdatePassword(string email, string newPassword)
        {
            string sqlCon = LoadConnectionString();
            using (SqlConnection conn = new SqlConnection(sqlCon))
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
        public void UpdateUserName(string email, string newName)
        {
            string sqlCon = LoadConnectionString();
            using (SqlConnection conn = new SqlConnection(sqlCon))
            {
                conn.Open();
                string updateQuery = "UPDATE Account SET Name = @NewName WHERE Email = @Email";
                using (SqlCommand command = new SqlCommand(updateQuery, conn))
                {
                    command.Parameters.AddWithValue("@NewName", newName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.ExecuteNonQuery();
                }
            }
        }
        public bool getEmailAvailable(string email)
        {
            string sqlCon = LoadConnectionString();
            using (SqlConnection conn = new SqlConnection(sqlCon))
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
        public bool getPassword(string email, string password)
        {
            string sqlCon = LoadConnectionString();
            using (SqlConnection conn = new SqlConnection(sqlCon))
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM Account WHERE Email = @Email AND Password = @Password";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@Email", email);
                    checkCommand.Parameters.AddWithValue("@Password", password);

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
        public User[] GetUserInfoFromDatabase(string email)
        {
            List<User> users = new List<User>();

            string query = "SELECT Email, Password, Name FROM Account WHERE Email = @Email";
            string sqlCon = LoadConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(sqlCon))
            {
                using (SqlCommand sqlCom = new SqlCommand(query, sqlConnection))
                {
                    sqlCom.Parameters.AddWithValue("@Email", email);

                    sqlConnection.Open();
                    using (SqlDataReader sqlRe = sqlCom.ExecuteReader())
                    {
                        while (sqlRe.Read())
                        {
                            User user = new User
                            {
                                Email = sqlRe["Email"].ToString(),
                                Password = sqlRe["Password"].ToString(),
                                Name = sqlRe["Name"].ToString()
                            };
                            users.Add(user);
                        }
                    }
                }

                return users.ToArray();
            }
        }


    }
}
