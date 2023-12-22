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

    public partial class frmSecurity : Form
    {
        private const string filePath = "AllFormsState.json";
        private FormState currentState = new FormState();
        public frmSecurity()
        {
            InitializeComponent();
            LoadFormState();
        }
        private void SaveFormState()
        {
            currentState.LockInterface = tglLockInterface.Checked;
            currentState.LockedComputer = tglLockedComputer.Checked;
            currentState.MinutesText = txtMinutes.Text;

            SaveAllFormsState();
        }

        private void LoadFormState()
        {
            Dictionary<string, FormState> allFormsState = LoadAllFormsState();

            string formName = this.Name; 
            if (allFormsState.ContainsKey(formName))
            {
                FormState currentState = allFormsState[formName];

                tglLockInterface.Checked = currentState.LockInterface;
                tglLockedComputer.Checked = currentState.LockedComputer;
                txtMinutes.Text = currentState.MinutesText;
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

        private void SaveAllFormsState()
        {
            Dictionary<string, FormState> allFormsState = LoadAllFormsState();
            allFormsState[this.Name] = currentState;
            Console.WriteLine(allFormsState);

            string json = JsonConvert.SerializeObject(allFormsState, Formatting.Indented);
            File.WriteAllText(filePath, json);
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
                txtMinutes.Enabled = true;
            }
            else
            {
                txtMinutes.Enabled = false;
            }
            SaveFormState();
        }

        private void tglLockedComputer_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            Active_Menu(true);
            currentState.LockedComputer = tglLockedComputer.Checked;
            SaveFormState();
        }

        private void txtMinutes_TextChanged(object sender, EventArgs e)
        {
            Active_Menu(true);
            /*
            using (StreamWriter writer = new StreamWriter("security_textbox.txt"))
            {
                writer.WriteLine(txtMinutes.Text);
            }
            */
            SaveFormState();
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
    public class FormState
    {
        public bool LockInterface { get; set; }
        public bool LockedComputer { get; set; }
        public string MinutesText { get; set; }
    }
    public class AutoLogin
    {
        public bool autologining { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}