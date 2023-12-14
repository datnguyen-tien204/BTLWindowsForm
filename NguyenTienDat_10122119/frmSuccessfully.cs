using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace NguyenTienDat_10122119
{
    public partial class frmSuccessfully : Form
    {
        public frmSuccessfully()
        {
            InitializeComponent();
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            if(pnlMore.Visible == false)
            {
                pnlMore.Visible = true;
                pnlMore.BringToFront();
            }
            else
            {
                pnlMore.Visible = false;
            }
        }

        private void frmSuccessfully_Load(object sender, EventArgs e)
        {
            pnlMore.Visible = false;
        }
        private const string filePath = "AutoLogin.txt";

        private void btnLogOut_Click(object sender, EventArgs e)
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
    }
}
