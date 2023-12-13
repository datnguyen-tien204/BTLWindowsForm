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
    public partial class frmController : Form
    {
        public frmController()
        {
            InitializeComponent();
        }
        private const string filePath = "Controller.txt";
        private void SaveFormState()
        {
            int selectedIndex = cboImageQuality.SelectedIndex;
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(tglHideWallpaper.Checked);
                writer.WriteLine(tglHideWallpaper.Checked);
                writer.Write(selectedIndex);
            }
        }

        private void LoadFormState()
        {
            if (File.Exists("Controller_cbo.txt"))
            {
                string[] recipientLines = File.ReadAllLines("Controller_cbo.txt");
                if (recipientLines.Length >= 1)
                {
                    int selectedIndex;
                    if (int.TryParse(recipientLines[0], out selectedIndex))
                    {
                        string[] linesToUpdate = File.ReadAllLines(filePath);
                        linesToUpdate[2] = selectedIndex.ToString();

                        File.WriteAllLines(filePath, linesToUpdate);
                    }
                }
            }

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
                        tglHideWallpaper.Checked = tglAllowAcceptValue;
                        tglSaveSecurityCode.Checked = tglAllowTemporaryValue;
                        cboImageQuality.SelectedIndex = selectedIndexValue;
                    }
                }
            }
        }

        private void frmController_Load(object sender, EventArgs e)
        {
            Active_Menu(true);
            LoadFormState();

        }
        private void Active_Menu(bool isActive)
        {
            if (isActive)
            {
                frmSettings mainForm = this.ParentForm as frmSettings;
                if (mainForm != null)
                {
                    mainForm.EnableBtnSetting(true, "btnController");
                }
            }
            else
            {
                frmSettings mainForm = this.ParentForm as frmSettings;
                if (mainForm != null)
                {
                    mainForm.EnableBtnSetting(false, "btnController");
                }
            }
        }

        private void cboImageQuality_Click(object sender, EventArgs e)
        {
            Active_Menu(true);
            SaveFormState();
        }

        private void cboImageQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            Active_Menu(true);
            int selectedIndex = cboImageQuality.SelectedIndex;
            using (StreamWriter writer = new StreamWriter("Controller_cbo.txt"))
            {
                writer.WriteLine(selectedIndex);
            }

        }

        private void frmController_MouseClick(object sender, MouseEventArgs e)
        {
            Active_Menu(true);
        }

        private void tglHideWallpaper_Click(object sender, EventArgs e)
        {
            Active_Menu(true);
            SaveFormState();
        }

        private void tglHideWallpaper_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            Active_Menu(true);
        }

        private void bunifuToggleSwitch1_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            Active_Menu(true);
        }

        private void bunifuToggleSwitch1_Click(object sender, EventArgs e)
        {
            Active_Menu(true);
            SaveFormState();
        }

        private void cboImageQuality_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cboImageQuality_MouseClick(object sender, MouseEventArgs e)
        {
            SaveFormState();
        }
    }
}
