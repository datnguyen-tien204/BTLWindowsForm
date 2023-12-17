using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WindowsFormsControlLibrary1;

namespace NguyenTienDat_10122119
{
    public partial class frmDeviceProperties : Form
    {
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;
        public frmDeviceProperties()
        {
            InitializeComponent();
            this.bunifuPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bunifuPanel1_MouseDown);
            this.bunifuPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bunifuPanel1_MouseMove);
            this.bunifuPanel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bunifuPanel1_MouseUp);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void frmDeviceProperties_Load(object sender, EventArgs e)
        {
            string jsonFilePath = "button_info.json";

            try
            {
                string json = File.ReadAllText(jsonFilePath);
                ButtonInfo buttonInfo = JsonConvert.DeserializeObject<ButtonInfo>(json);
                lblDeviceName.Text = buttonInfo.ID;
                lblLastLoginTime.Text = buttonInfo.Time;
                lblDeviceID.Text = buttonInfo.ID;
                lblLastLoginIP.Text = buttonInfo.ID;
            }
            catch (Exception ex)
            {
                
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastCursor = Cursor.Position;
            lastForm = this.Location;
        }

        private void bunifuPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = Cursor.Position.X - lastCursor.X;
                int deltaY = Cursor.Position.Y - lastCursor.Y;
                this.Location = new Point(lastForm.X + deltaX, lastForm.Y + deltaY);
            }
        }

        private void bunifuPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
