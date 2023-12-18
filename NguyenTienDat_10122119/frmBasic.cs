using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace NguyenTienDat_10122119
{
    public partial class frmBasic : Form
    {
        // Khai báo hằng số để ngăn chặn sleep
        const uint EXECUTION_STATE_SYSTEM_REQUIRED = 0x00000001;
        const uint EXECUTION_STATE_CONTINUOUS = 0x80000000;
        public frmBasic()
        {
            InitializeComponent();
        }
        /// <Cấu hình các chức năng cần thiết của Form>
        private void PreventSleep()
        {
            SetThreadExecutionState(EXECUTION_STATE_SYSTEM_REQUIRED | EXECUTION_STATE_CONTINUOUS);
        }

        private void RestoreSleep()
        {
            SetThreadExecutionState(EXECUTION_STATE_CONTINUOUS);
        }

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
            if(tglPreventDevice.Checked==true)
            {
                PreventSleep();
            }
            else
            {
                RestoreSleep();
            }
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
        [DllImport("kernel32.dll")]
        static extern uint SetThreadExecutionState(uint esFlags);

        
        private void frmBasic_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(tglStartWithWindows.Checked==true)
            {
                string jsonFilePath = "AllFormsState.json";
                string jsonString = File.ReadAllText(jsonFilePath);
                dynamic jsonData = JsonConvert.DeserializeObject(jsonString);

                bool lockInterfaceOfFrmBasic = jsonData.frmBasic.LockInterface;

                if (lockInterfaceOfFrmBasic == true)
                {
                    RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    reg.SetValue("Dat's Viewer", System.Windows.Forms.Application.ExecutablePath.ToString());
                }
                else
                {
                    RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                    if (lockInterfaceOfFrmBasic && reg != null)
                    {
                        if (reg.GetValue("Dat's Viewer") != null)
                        {
                            reg.DeleteValue("Dat's Viewer");
                        }
                    }
                }
            }
        }
    }
}
