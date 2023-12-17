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
    public partial class frmRecipient : Form
    {

        public frmRecipient()
        {
            InitializeComponent();
        }
        private const string filePath = "AllFormsState.json";
        private FormState currentState = new FormState();
        /// <Save data moi khi thay doi>
        private void SaveFormState()
        {
            // Tạo một đối tượng FormState để lưu trạng thái của form Recipient
            FormState currentState = new FormState
            {
                LockInterface = tglAllowAccept.Checked,
                LockedComputer = tglAllowTemporary.Checked,
                MinutesText = cboChoose.SelectedIndex.ToString()
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
            if(formName == "")
            {
                currentState = new FormState();
            }
            if (allFormsState.ContainsKey(formName))
            {
                FormState currentState = allFormsState[formName];

                tglAllowAccept.Checked = currentState.LockInterface;
                tglAllowTemporary.Checked = currentState.LockedComputer;
                int selectedIndex;
                if (int.TryParse(currentState.MinutesText, out selectedIndex))
                {
                    cboChoose.SelectedIndex = selectedIndex;
                }
            }
            else
            {
                // Nếu allFormsState là null, tạo mới một Dictionary để tránh NullReferenceException
                allFormsState = new Dictionary<string, FormState>();
                currentState = new FormState();
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
