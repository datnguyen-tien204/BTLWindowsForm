using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private void frmBasic_Load(object sender, EventArgs e)
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string result = userName.Split('\\')[0];
            txtDeviceName.Text = result;
            Active_Menu(true);
        }

        private void btnStartWithWindows_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            Active_Menu(true);
        }

        private void btnPreventDevice_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            Active_Menu(true);
        }

        private void txtDeviceName_TextChanged(object sender, EventArgs e)
        {

            Active_Menu(true);
        }

        private void btnSaveDeviceName_Click(object sender, EventArgs e)
        {
            Active_Menu(true);
        }
    }
}
