﻿using Newtonsoft.Json;
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
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void DeletedFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write("");
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        private const string filePathJson = "AllFormsState.json";
        private void ResetAll()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string result = userName.Split('\\')[0];

            Dictionary<string, FormState> defaultFormStates = new Dictionary<string, FormState>
            {
                ["frmSecurity"] = new FormState { LockInterface = false, LockedComputer = false, MinutesText = "" },
                ["frmRecipient"] = new FormState { LockInterface = true, LockedComputer = true, MinutesText = "1" },
                ["frmController"] = new FormState { LockInterface = false, LockedComputer = true, MinutesText = "0" },
                ["frmUnattended"] = new FormState { LockInterface = true, LockedComputer = false, MinutesText = "" },
                ["frmBasic"] = new FormState { LockInterface = true, LockedComputer = true, MinutesText = result },
                ["frmLogin"]=new FormState { LockInterface = false, LockedComputer = false, MinutesText = ""},
                ["frmSuccessfully"]=new FormState { LockInterface=false, LockedComputer=false, MinutesText=""}
            };

            string json = JsonConvert.SerializeObject(defaultFormStates, Formatting.Indented);

            File.WriteAllText(filePathJson, json);
        }

        private void btnResetAll_Click(object sender, EventArgs e)
        {
           if(MessageBox.Show("Are you sure you want to reset all settings?","Warning",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                ResetAll();
                MessageBox.Show("Reset all settings successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnBasic_Click(object sender, EventArgs e)
        {
            load_form(new frmBasic());
            Active_SettingMain(true);
            EnableBtnSetting(false, "btnUntended");
            EnableBtnSetting(true, "btnBasic");
        }

        private void btnSecurity_Click(object sender, EventArgs e)
        {
            load_form(new frmSecurity());
            Active_SettingMain(true);
            EnableBtnSetting(false, "btnUntended");
            EnableBtnSetting(true, "btnSecurity");
        }

        private void btnController_Click(object sender, EventArgs e)
        {
            load_form(new frmController());
            Active_SettingMain(true);
            EnableBtnSetting(false, "btnUntended");
            EnableBtnSetting(true, "btnController");
        }

        private void btnUntended_Click(object sender, EventArgs e)
        {
            load_form(new frmUnattended());
            EnableBtnSetting(true, "btnUntended");
            EnableBtnSetting(false, "btnUntended");
            Active_SettingMain(true);
            
        }

        private void btnRecipient_Click(object sender, EventArgs e)
        {
            load_form(new frmRecipient());
            Active_SettingMain(true);
            EnableBtnSetting(false, "btnUntended");
            EnableBtnSetting(true, "btnRecipent");
        }
        public void load_form(object Form)
        {
            if (this.panel2.Controls.Count > 0)
            {
                this.panel2.Controls.RemoveAt(0);
            }
            Form frm = Form as Form;
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(frm);
            this.panel2.Tag = frm;
            //frm.Owner = this;
            frm.Show();
        }
        public void Active_SettingMain(bool aka)
        {
            frmMain mainForm = this.ParentForm as frmMain;
            if (mainForm != null)
            {
                if (aka)
                {
                    mainForm.EnableBtnSetting(true, "btnSetting");
                }
                else
                {
                    mainForm.EnableBtnSetting(false, "btnSetting");
                }
            }
        }

        public void EnableBtnSetting(bool enabled, string button)
        {
            if (button == "btnUntended")
            {
                if (enabled)
                {
                    Color customColor = Color.FromArgb(192, 213, 255);
                    btnUntended.OnIdleState.FillColor = customColor;
                    btnUntended.Refresh();
                }
                else
                {
                    Color customColor = Color.FromArgb(245, 247, 251);
                    btnUntended.OnIdleState.FillColor = customColor;
                    //btnUntended.IdleFillColor = Color.FromArgb(245, 247, 251);
                    btnUntended.Refresh();
                }
            }
            else if (button == "btnRecipent")
            {
                if (enabled)
                {
                    btnRecipent.IdleFillColor = Color.FromArgb(192, 213, 255);
                    btnRecipent.Refresh();
                }
                else
                {
                    btnRecipent.IdleFillColor = Color.FromArgb(245, 247, 251);
                    btnRecipent.Refresh();
                }
            }
            else if (button == "btnController")
            {
                if (enabled)
                {
                    btnController.IdleFillColor = Color.FromArgb(192, 213, 255);
                    btnController.Refresh();
                }
                else
                {
                    btnController.IdleFillColor = Color.FromArgb(245, 247, 251);
                    btnController.Refresh();
                }
            }
            else if (button == "btnSecurity")
            {
                if (enabled)
                {
                    btnSecurity.IdleFillColor = Color.FromArgb(192, 213, 255);
                    btnSecurity.Refresh();
                }
                else
                {
                    btnSecurity.IdleFillColor = Color.FromArgb(245, 247, 251);
                    btnSecurity.Refresh();
                }
            }
            else if (button == "btnBasic")
            {
                if (enabled)
                {
                    btnBasic.IdleFillColor = Color.FromArgb(192, 213, 255);
                    btnBasic.Refresh();
                }
                else
                {
                    btnBasic.IdleFillColor = Color.FromArgb(245, 247, 251);
                    btnBasic.Refresh();
                }
            }
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            btnUntended.PerformClick();
            frmMain mainForm = this.ParentForm as frmMain;
            if (mainForm != null)
            {
                mainForm.EnableBtnSetting(false, "btnConnect");
                mainForm.EnableBtnSetting(false, "btnProfile");
                mainForm.EnableBtnSetting(false, "btnDevice");
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmSettings_Leave(object sender, EventArgs e)
        {

        }
    }
}
