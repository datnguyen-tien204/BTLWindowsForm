using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace NguyenTienDat_10122119
{
    public partial class frmBasic : Form
    {
        public frmBasic()
        {
            InitializeComponent();
        }
        /// <Cấu hình các chức năng cần thiết của Form>
        
        private void Active_Menu(bool isActive)
        {
            if (isActive)
            {
                frmSettings mainForm = this.ParentForm as frmSettings;
                if (mainForm != null)
                {
                    mainForm.EnableBtnSetting(true, "btnBasic");
                }
            }
            else
            {
                frmSettings mainForm = this.ParentForm as frmSettings;
                if (mainForm != null)
                {
                    mainForm.EnableBtnSetting(false, "btnBasic");
                }
            }
        }
        private const string filePath = "basic.txt";
        private void SaveFormState()
        {
            string text = txtDeviceName.Text;
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(tglStartWithWindows.Checked);
                writer.WriteLine(tglPreventDevice.Checked);
                writer.WriteLine(text);
            }
        }

        private void LoadFormState()
        {
            if (File.Exists("basic_textbox.txt"))
            {
                string[] recipientLines = File.ReadAllLines("basic_textbox.txt");
                if (recipientLines.Length >= 1)
                {
                    string selectedIndexStr = recipientLines[0];
                    string[] linesToUpdate = File.ReadAllLines(filePath);
                    linesToUpdate[2] = selectedIndexStr;

                    File.WriteAllLines(filePath, linesToUpdate);
                }
            }

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                bool tglAllowAcceptValue;
                bool tglAllowTemporaryValue;
                string selectedIndexValue;
                if (lines.Length >= 3)
                {
                    if (bool.TryParse(lines[0], out tglAllowAcceptValue) &&
                        bool.TryParse(lines[1], out tglAllowTemporaryValue))
                    {
                        tglStartWithWindows.Checked = tglAllowAcceptValue;
                        tglPreventDevice.Checked = tglAllowTemporaryValue;

                        txtDeviceName.Text = lines[2]; 
                    }
                }
            }
        }
        /// </Kết thúc phần cấu hình>
        private void frmBasic_Load(object sender, EventArgs e)
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string result = userName.Split('\\')[0];
            txtDeviceName.Text = result;
            Active_Menu(true);
            LoadFormState();
        }

        private void btnStartWithWindows_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            Active_Menu(true);
            SaveFormState();
        }

        private void btnPreventDevice_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            Active_Menu(true);
            SaveFormState();
        }

        private void txtDeviceName_TextChanged(object sender, EventArgs e)
        {

            Active_Menu(true);
            using (StreamWriter writer = new StreamWriter("security_textbox.txt"))
            {
                writer.WriteLine(txtDeviceName.Text);
            }
        }

        private void btnSaveDeviceName_Click(object sender, EventArgs e)
        {
            Active_Menu(true);
            SaveFormState();
        }

        
    }
}
