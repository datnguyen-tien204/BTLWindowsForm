using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }

        private void frmRecipient_Load(object sender, EventArgs e)
        {
            Active_Menu(true);
        }
    }
}
