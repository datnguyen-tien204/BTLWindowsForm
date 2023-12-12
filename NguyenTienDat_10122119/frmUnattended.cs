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
    public partial class frmUnattended : Form
    {
        public frmUnattended()
        {
            InitializeComponent();

        }
        private const string filePath = "UnattenedSettings.txt";
        private void SaveFormState()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(tglLogged_in.Checked);
                writer.WriteLine(tglSecurityCode.Checked);
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

                    if (!string.IsNullOrEmpty(logged_in) && !string.IsNullOrEmpty(security_code))
                    {
                        tglLogged_in.Checked = bool.Parse(logged_in);
                        tglSecurityCode.Checked = bool.Parse(security_code);
                    }
                }
            }
        }

        private void tglLogged_in_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            
            frmSettings mainForm = this.ParentForm as frmSettings;
            if (mainForm != null)
            {
                mainForm.EnableBtnSetting(true, "btnUntended");
            }
            if (tglSecurityCode.Checked)
            {
                txtSecurityCode.Show();
                btnSave.Show();
                lblSecuriteCode.Show();
                
            }
            else
            {
                txtSecurityCode.Hide();
                btnSave.Hide();
                lblSecuriteCode.Hide();
                string filePath = "code.txt";
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write("");
                }
            }
        }


        private void tglSecurityCode_Load(object sender, EventArgs e)
        {
            txtSecurityCode.Hide();
            btnSave.Hide();
            lblSecuriteCode.Hide();

            frmSettings mainForm = this.ParentForm as frmSettings;
            if (mainForm != null)
            {
                mainForm.EnableBtnSetting(true, "btnUntended");
            }
        }

        private void frmUnattended_Load(object sender, EventArgs e)
        {
            LoadFormState();
            frmSettings mainForm = this.ParentForm as frmSettings;
            if (mainForm != null)
            {
                mainForm.EnableBtnSetting(true, "btnUntended");
            }
            if (tglSecurityCode.Checked)
            {
                txtSecurityCode.Show();
                btnSave.Show();
                lblSecuriteCode.Show();
                string filePath = "code.txt";   
                string fileContent = File.ReadAllText(filePath);
                txtSecurityCode.Text = fileContent; 
            }
            else
            {
                txtSecurityCode.Hide();
                btnSave.Hide();
                lblSecuriteCode.Hide();
                string filePath = "code.txt";
                File.WriteAllText(filePath, string.Empty);

            }
        }

        private void frmUnattended_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFormState();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSecurityCode.Text.Length <= 3)
            {
                MessageBox.Show("Security code must be at least 4 characters long", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string filePath = "code.txt";

                if (File.Exists(filePath) && new FileInfo(filePath).Length == 0)
                {
                    File.WriteAllText(filePath, txtSecurityCode.Text);
                }
                MessageBox.Show("Security code saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tglLogged_in_CheckedChanged_1(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            //SaveFormState();
        }

        private void tglLogged_in_Click(object sender, EventArgs e)
        {
            SaveFormState();
        }

        private void tglSecurityCode_Click(object sender, EventArgs e)
        {
            SaveFormState();
        }
    }
}
