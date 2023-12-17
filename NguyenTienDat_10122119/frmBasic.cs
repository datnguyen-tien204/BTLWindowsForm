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
        private const string filePath = "AllFormsState.json";
        private FormState currentState = new FormState();
        /// <Save data moi khi thay doi>
        private void SaveFormState()
        {
            FormState currentState = new FormState
            {
                LockInterface = tglStartWithWindows.Checked,
                LockedComputer = tglPreventDevice.Checked,
                MinutesText = txtDeviceName.Text.ToString()
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

                tglStartWithWindows.Checked = currentState.LockInterface;
                tglPreventDevice.Checked = currentState.LockedComputer;
                txtDeviceName.Text = currentState.MinutesText;
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
