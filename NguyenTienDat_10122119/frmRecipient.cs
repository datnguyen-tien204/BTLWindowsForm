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
                writer.WriteLine(selectedIndex);
            }
        }

        private void LoadFormState()
        {
            if (File.Exists("recipient_cbo.txt"))
            {
                string[] recipientLines = File.ReadAllLines("recipient_cbo.txt");
                if (recipientLines.Length >= 1)
                {
                    int selectedIndex;
                    if (int.TryParse(recipientLines[0], out selectedIndex))
                    {
                        // Update line 3 of Receipent.txt with selectedIndex from recipient_cbo.txt
                        string[] linesToUpdate = File.ReadAllLines(filePath);
                        linesToUpdate[2] = selectedIndex.ToString();

                        File.WriteAllLines(filePath, linesToUpdate);
                    }
                }
            }

            // Read data from Receipent.txt after updating its line 3
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                bool tglAllowAcceptValue;
                bool tglAllowTemporaryValue;
                int selectedIndexValue;

                // Check if the file has at least 3 lines
                if (lines.Length >= 3)
                {
                    if (bool.TryParse(lines[0], out tglAllowAcceptValue) &&
                        bool.TryParse(lines[1], out tglAllowTemporaryValue) &&
                        int.TryParse(lines[2], out selectedIndexValue))
                    {
                        tglAllowAccept.Checked = tglAllowAcceptValue;
                        tglAllowTemporary.Checked = tglAllowTemporaryValue;
                        cboChoose.SelectedIndex = selectedIndexValue;
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
            int selectedIndex = cboChoose.SelectedIndex;
            using (StreamWriter writer = new StreamWriter("recipient_cbo.txt"))
            {
                writer.WriteLine(selectedIndex);
            }
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

        private void cboChoose_Click(object sender, EventArgs e)
        {
            SaveFormState();
        }
    }
}
