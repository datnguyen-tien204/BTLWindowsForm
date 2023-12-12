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

namespace NguyenTienDat_10122119
{
    public partial class frmRecipient : Form
    {
        public frmRecipient()
        {
            InitializeComponent();
        }
        /// <Save data moi khi thay doi>
        private const string filePath = "Receipent.txt";
        private void SaveFormState()
        {
            int selectedIndex = cboChoose.SelectedIndex;
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(tglAllowAccept.Checked);
                writer.WriteLine(tglAllowTemporary.Checked);
                writer.Write(selectedIndex);
            }
        }

        private void LoadFormState()
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string logged_in = reader.ReadLine();
                    string security_code = reader.ReadLine();
                    string selectedIndex = reader.ReadLine();

                    if (!string.IsNullOrEmpty(logged_in) && !string.IsNullOrEmpty(security_code))
                    {
                        tglAllowAccept.Checked = bool.Parse(logged_in);
                        tglAllowTemporary.Checked = bool.Parse(security_code);
                    }
                    if (!string.IsNullOrEmpty(selectedIndex))
                    {
                        cboChoose.SelectedIndex = int.Parse(selectedIndex);
                    }
                }
            }
        }
        /// </summary>
        /// <param name="isActive"></param>
        private void Active_Menu(bool isActive)
        {
            if(isActive)
            {
                frmSettings mainForm = this.ParentForm as frmSettings;
                if (mainForm != null)
                {
                    mainForm.EnableBtnSetting(true, "btnRecipent");
                }
            }
            else
            {
                frmSettings mainForm = this.ParentForm as frmSettings;
                if (mainForm != null)
                {
                    mainForm.EnableBtnSetting(false, "btnRecipent");
                }
            }
        }

        private void tglAllowAccept_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            Active_Menu(true);
        }

        private void tglAllowTemporary_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            Active_Menu(true);
        }

        private void cboChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            Active_Menu(true);
        }

        private void frmRecipient_Load(object sender, EventArgs e)
        {
            Active_Menu(true);
            LoadFormState();
            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);

                if (!string.IsNullOrEmpty(fileContent))
                {
                    cboChoose.SelectedIndex = 0;
                }
                else
                {
                    cboChoose.SelectedIndex = 1; 
                }
            }
            else
            {
                cboChoose.SelectedIndex = 1;
            }
        }

        private void frmRecipient_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFormState();
        }

        private void tglAllowAccept_Click(object sender, EventArgs e)
        {
            SaveFormState();
        }

        private void tglAllowTemporary_Click(object sender, EventArgs e)
        {
            SaveFormState();
        }
    }
}
