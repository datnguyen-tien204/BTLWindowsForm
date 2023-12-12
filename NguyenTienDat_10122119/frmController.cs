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
    public partial class frmController : Form
    {
        public frmController()
        {
            InitializeComponent();
        }

        private void frmController_Load(object sender, EventArgs e)
        {
            Active_Menu(true);

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
        }

        private void cboImageQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            Active_Menu(true);
        }

        private void frmController_MouseClick(object sender, MouseEventArgs e)
        {
            Active_Menu(true);
        }

        private void tglHideWallpaper_Click(object sender, EventArgs e)
        {
            Active_Menu(true);
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
        }
    }
}
