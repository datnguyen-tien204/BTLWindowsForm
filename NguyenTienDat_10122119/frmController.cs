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
    public partial class frmController : Form
    {
        public frmController()
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
                LockInterface = tglHideWallpaper.Checked,
                LockedComputer = tglSaveSecurityCode.Checked,
                MinutesText = cboImageQuality.SelectedIndex.ToString()
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

                tglHideWallpaper.Checked = currentState.LockInterface;
                tglSaveSecurityCode.Checked = currentState.LockedComputer;
                int selectedIndex;
                if (int.TryParse(currentState.MinutesText, out selectedIndex))
                {
                    cboImageQuality.SelectedIndex = selectedIndex;
                }
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
