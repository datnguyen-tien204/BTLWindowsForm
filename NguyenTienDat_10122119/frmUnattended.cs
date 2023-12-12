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
    public partial class frmUnattended : Form
    {
        public frmUnattended()
        {
            InitializeComponent();

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
            }
        }
    }
}
