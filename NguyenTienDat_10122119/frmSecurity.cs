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
    public partial class frmSecurity : Form
    {
        public frmSecurity()
        {
            InitializeComponent();
        }
        private const string filePath = "Security.txt";
        private void SaveFormState()
        {
            string text = txtMinutes.Text;
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(tglLockInterface.Checked);
                writer.WriteLine(tglLockedComputer.Checked);
                writer.WriteLine(text);
            }
        }

        private void LoadFormState()
        {
            if (File.Exists("security_textbox.txt"))
            {
                string[] recipientLines = File.ReadAllLines("security_textbox.txt");
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

                if (lines.Length >= 3)
                {
                    if (bool.TryParse(lines[0], out tglAllowAcceptValue) &&
                        bool.TryParse(lines[1], out tglAllowTemporaryValue) &&
                        int.TryParse(lines[2], out selectedIndexValue))
                    {
                        tglLockInterface.Checked = tglAllowAcceptValue;
                        tglLockedComputer.Checked = tglAllowTemporaryValue;
                        txtMinutes.Text = selectedIndexValue.ToString();
                    }
                }
            }
        }
        private void Active_Menu(bool isActive)
        {
            if (isActive)
            {
                frmSettings mainForm = this.ParentForm as frmSettings;
                if (mainForm != null)
                {
                    mainForm.EnableBtnSetting(true, "btnSecurity");
                }
            }
            else
            {
                frmSettings mainForm = this.ParentForm as frmSettings;
                if (mainForm != null)
                {
                    mainForm.EnableBtnSetting(false, "btnSecurity");
                }
            }
        }

        private void frmSecurity_Load(object sender, EventArgs e)
        {
            Active_Menu(true);
            LoadFormState();
            if (tglLockInterface.Checked)
            {
                txtMinutes.Enabled = true;
            }
            else
            {
                txtMinutes.Enabled = false;
            }
            
        }

        private void tglLockInterface_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            Active_Menu(true);
            if (tglLockInterface.Checked)
            {
                txtMinutes.Enabled=true;
            }
            else
            {
                txtMinutes.Enabled=false;
            }
        }

        private void tglLockedComputer_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            Active_Menu(true);
        }

        private void txtMinutes_TextChanged(object sender, EventArgs e)
        {
            Active_Menu(true);
            using (StreamWriter writer = new StreamWriter("security_textbox.txt"))
            {
                writer.WriteLine(txtMinutes.Text);
            }
        }

        private void tglLockInterface_Click(object sender, EventArgs e)
        {
            SaveFormState();
        }

        private void tglLockedComputer_Click(object sender, EventArgs e)
        {
            SaveFormState();
        }
    }
}
