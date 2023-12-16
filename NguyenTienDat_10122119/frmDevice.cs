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
    public partial class frmDevice : Form
    {
        private bool moreButtonClicked = false;
        public frmDevice()
        {
            InitializeComponent();
            AddButtonsToPanel();
        }

        private void frmDevice_Load(object sender, EventArgs e)
        {
            frmMain mainForm = this.ParentForm as frmMain;
            if (mainForm != null)
            {
                mainForm.EnableBtnSetting(false, "btnConnect");
                mainForm.EnableBtnSetting(false, "btnDashboard");
                mainForm.EnableBtnSetting(false, "btnSetting");
                mainForm.EnableBtnSetting(false, "btnProfile");
            }
        }
        private void AddButtonsToPanel()
        {
            // Tạo ra các button mới
            for (int i = 0; i < 5; i++)
            {
                Button newButton = new Button();
                newButton.Text = $"Button {i + 1}";
                newButton.Size = new Size(100, 30);
                newButton.Location = new Point(10, 40 * i); // Vị trí của mỗi button trong panel (cách nhau 40 pixels)
                //newButton.Click += NewButton_Click; // Gán sự kiện Click cho từng button nếu cần thiết

                // Thêm button vào panel
                bunifuPanel3.Controls.Add(newButton);
            }
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            if (!moreButtonClicked)
            {
                btnMore.Image = Properties.Resources.icons8_forward_32__1_;
                moreButtonClicked = true;
            }
            else
            {
                btnMore.Image = Properties.Resources.icons8_forward_32; 
                moreButtonClicked = false;
            }
        }

        private void bunifuPanel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPanel2_Click(object sender, EventArgs e)
        {

        }
    }
}
