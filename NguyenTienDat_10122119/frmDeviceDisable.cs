﻿using System;
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
    public partial class frmDeviceDisable : Form
    {
        public frmDeviceDisable()
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

        private void frmDeviceDisable_Load(object sender, EventArgs e)
        {
            frmMain mainForm = this.ParentForm as frmMain;
            if (mainForm != null)
            {
                mainForm.EnableBtnSetting(false, "btnConnect");
                mainForm.EnableBtnSetting(false, "btnSetting");
                mainForm.EnableBtnSetting(false, "btnDevice");
            }
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
        private const string filePath = "AutoLogin.txt";
        private void frmProfile_Load(object sender, EventArgs e)
        {
            
        }
    }
}
