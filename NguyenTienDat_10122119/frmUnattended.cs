using Newtonsoft.Json;
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
        private const string filePath = "AllFormsState.json";
        private FormState currentState = new FormState();
        /// <Save data moi khi thay doi>
        private void SaveFormState()
        {
            FormState currentState = new FormState
            {
                LockInterface = tglLogged_in.Checked,
                LockedComputer = tglSecurityCode.Checked,
                MinutesText = txtSecurityCode.Text.ToString()
            };

            Dictionary<string, FormState> allFormsState = LoadAllFormsState();
            allFormsState[GetType().Name] = currentState;

            string json = JsonConvert.SerializeObject(allFormsState, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private void LoadFormState()
        {
            Dictionary<string, FormState> allFormsState = LoadAllFormsState();
            string formName = GetType().Name;

            if (allFormsState.ContainsKey(formName))
            {
                FormState currentState = allFormsState[formName];

                tglLogged_in.Checked = currentState.LockInterface;
                tglSecurityCode.Checked = currentState.LockedComputer;
                txtSecurityCode.Text = currentState.MinutesText;
                
            }
        }
        private Dictionary<string, FormState> LoadAllFormsState()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<Dictionary<string, FormState>>(json);
            }
            return new Dictionary<string, FormState>();
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
