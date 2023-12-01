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
    public partial class frmRemote : Form
    {
        public frmRemote()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            axMsRdpClient8NotSafeForScripting1.Server = "192.168.80.129";
            axMsRdpClient8NotSafeForScripting1.UserName = "nguye";
            axMsRdpClient8NotSafeForScripting1.AdvancedSettings2.ClearTextPassword = "invalid_password";
            axMsRdpClient8NotSafeForScripting1.AdvancedSettings7.EnableCredSspSupport = true;

            axMsRdpClient8NotSafeForScripting1.ColorDepth = 16;
            axMsRdpClient8NotSafeForScripting1.DesktopWidth = 1920;
            axMsRdpClient8NotSafeForScripting1.DesktopHeight = 1080;
            axMsRdpClient8NotSafeForScripting1.AdvancedSettings7.SmartSizing = true;

            axMsRdpClient8NotSafeForScripting1.Connect();

        }
    }
}
