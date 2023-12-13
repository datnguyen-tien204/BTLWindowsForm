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
    public partial class frmProfile : Form
    {
        public frmProfile()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmSignUp"] != null)
            {
                Form frmLogIn = Application.OpenForms["frmSignUp"];
                frmLogIn.Close();
            }

            frmLogin frm = new frmLogin();
            frm.ShowDialog();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmLogIn"] != null)
            {
                Form frmLogIn = Application.OpenForms["frmLogIn"];
                frmLogIn.Close();
            }

            frmSignUp frm = new frmSignUp();
            frm.ShowDialog();
        }
    }
}
