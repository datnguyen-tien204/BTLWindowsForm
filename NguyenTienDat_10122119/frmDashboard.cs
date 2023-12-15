using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Controls;

namespace NguyenTienDat_10122119
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();

            
        }
        private PrivateFontCollection pfc;
        private void Init_CustomLabel_Font()
        {
            pfc = new PrivateFontCollection();


            int fontLength = Properties.Resources.BeautiqueDisplayCondensed_Black.Length;
            byte[] fontdata = Properties.Resources.BeautiqueDisplayCondensed_Black;
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);
        }
        private const string filePath = "AutoLogin.txt";
        protected string connStr = @"Data Source=NGUYENTIENDAT;Initial Catalog=RemoteDesktop;Integrated Security=True";
        public string getTime()
        {
            string r;
            DateTime currentTime = DateTime.Now;
            if (currentTime.Hour >= 5 && currentTime.Hour < 12)
            {
                r= "Good Morning";
            }
            else if(currentTime.Hour>=12 && currentTime.Hour<18)
            {
                r="Good Afternoon";
            }
            else 
            {
                r="Good Evening"; 
            }
            return r;
           
        }
        public void getName()
        {
            string EmailGetfromSettings=Properties.Settings.Default.ChaoMung;
            string dau=getTime();
            using (SqlConnection sqlCon = new SqlConnection(connStr))
            {
                sqlCon.Open();
                using (SqlCommand sqlCom = new SqlCommand($"select * from Account where Email='{EmailGetfromSettings}'", sqlCon))
                {
                    using (SqlDataReader sqlRe = sqlCom.ExecuteReader())
                    {
                        while (sqlRe.Read())
                        {
                            string Email = sqlRe[0].ToString();
                            string Password = sqlRe[1].ToString();
                            string Name = sqlRe[2].ToString();
                            lblChaoMung.Text= dau+ ", "+ Name;
                        }
                    }
                }
                sqlCon.Close();
            }
           
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            Init_CustomLabel_Font();
            frmMain mainForm = this.ParentForm as frmMain;
            if (mainForm != null)
            {
                mainForm.EnableBtnSetting(false, "btnConnect");
                mainForm.EnableBtnSetting(false, "btnSetting");
                mainForm.EnableBtnSetting(false, "btnDevice");
                mainForm.EnableBtnSetting(false, "btnProfile");
            }

            lblChaoMung.Font = new Font(pfc.Families[0], lblChaoMung.Font.Size,FontStyle.Bold);
            
            getName();
            string filePath = "connection_log.txt";
            DisplayConnectionLogs(filePath);
            //lblChaoMung.Text = "Welcome Đạt Nguyễn,";
        }
        private string FormatTime(TimeSpan timeSpan)
        {
            if (timeSpan.TotalMinutes < 60)
            {
                return $"{timeSpan.TotalMinutes}m";
            }
            else
            {
                return $"{(int)timeSpan.TotalHours}h{(int)timeSpan.Minutes}m";
            }
        }
        private void DisplayConnectionLogs(string filePath)
        {
            try
            {
                // Đọc dữ liệu từ file
                string[] lines = File.ReadAllLines(filePath);

                // Tạo dictionary để lưu trữ thời gian kết nối gần nhất của mỗi ID
                Dictionary<string, DateTime> latestConnectionTime = new Dictionary<string, DateTime>();

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 2)
                    {
                        string[] idAndTime = parts[0].Split(' ');
                        if (idAndTime.Length >= 4)
                        {
                            string id = $"{idAndTime[0]} {idAndTime[1]} {idAndTime[2]} {idAndTime[3]}";
                            string timeString = parts[1];

                            DateTime time;
                            if (DateTime.TryParseExact(timeString, "MM/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
                            {
                                if (latestConnectionTime.ContainsKey(id))
                                {
                                    // Nếu ID đã tồn tại, cập nhật thời gian kết nối mới nhất
                                    latestConnectionTime[id] = time > latestConnectionTime[id] ? time : latestConnectionTime[id];
                                }
                                else
                                {
                                    latestConnectionTime.Add(id, time);
                                }
                            }
                        }
                    }
                }

                // Hiển thị kết quả lên giao diện
                foreach (var kvp in latestConnectionTime)
                {
                    TimeSpan elapsedTime = DateTime.Now - kvp.Value;
                    string formattedTime = FormatTime(elapsedTime);
                    //listBox1.Items.Add($"ID: {kvp.Key}, Connected {formattedTime} ago.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
