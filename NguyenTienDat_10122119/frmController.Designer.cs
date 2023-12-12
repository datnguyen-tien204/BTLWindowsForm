namespace NguyenTienDat_10122119
{
    partial class frmController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmController));
            Bunifu.UI.WinForms.BunifuToggleSwitch.ToggleState toggleState1 = new Bunifu.UI.WinForms.BunifuToggleSwitch.ToggleState();
            Bunifu.UI.WinForms.BunifuToggleSwitch.ToggleState toggleState2 = new Bunifu.UI.WinForms.BunifuToggleSwitch.ToggleState();
            Bunifu.UI.WinForms.BunifuToggleSwitch.ToggleState toggleState3 = new Bunifu.UI.WinForms.BunifuToggleSwitch.ToggleState();
            Bunifu.UI.WinForms.BunifuToggleSwitch.ToggleState toggleState4 = new Bunifu.UI.WinForms.BunifuToggleSwitch.ToggleState();
            Bunifu.UI.WinForms.BunifuToggleSwitch.ToggleState toggleState5 = new Bunifu.UI.WinForms.BunifuToggleSwitch.ToggleState();
            Bunifu.UI.WinForms.BunifuToggleSwitch.ToggleState toggleState6 = new Bunifu.UI.WinForms.BunifuToggleSwitch.ToggleState();
            this.bunifuLabel1 = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel2 = new Bunifu.UI.WinForms.BunifuLabel();
            this.cboImageQuality = new Guna.UI2.WinForms.Guna2ComboBox();
            this.tglHideWallpaper = new Bunifu.UI.WinForms.BunifuToggleSwitch();
            this.bunifuLabel3 = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuToggleSwitch1 = new Bunifu.UI.WinForms.BunifuToggleSwitch();
            this.bunifuLabel4 = new Bunifu.UI.WinForms.BunifuLabel();
            this.SuspendLayout();
            // 
            // bunifuLabel1
            // 
            this.bunifuLabel1.AllowParentOverrides = false;
            this.bunifuLabel1.AutoEllipsis = false;
            this.bunifuLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel1.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel1.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuLabel1.Location = new System.Drawing.Point(30, 26);
            this.bunifuLabel1.Name = "bunifuLabel1";
            this.bunifuLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel1.Size = new System.Drawing.Size(156, 25);
            this.bunifuLabel1.TabIndex = 0;
            this.bunifuLabel1.Text = "Remote Control";
            this.bunifuLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel1.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuLabel2
            // 
            this.bunifuLabel2.AllowParentOverrides = false;
            this.bunifuLabel2.AutoEllipsis = false;
            this.bunifuLabel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel2.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuLabel2.Location = new System.Drawing.Point(30, 78);
            this.bunifuLabel2.Name = "bunifuLabel2";
            this.bunifuLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel2.Size = new System.Drawing.Size(92, 16);
            this.bunifuLabel2.TabIndex = 1;
            this.bunifuLabel2.Text = "Image quality";
            this.bunifuLabel2.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel2.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // cboImageQuality
            // 
            this.cboImageQuality.BackColor = System.Drawing.Color.Transparent;
            this.cboImageQuality.BorderRadius = 15;
            this.cboImageQuality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboImageQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImageQuality.FocusedColor = System.Drawing.Color.Empty;
            this.cboImageQuality.FocusedState.Parent = this.cboImageQuality;
            this.cboImageQuality.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboImageQuality.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboImageQuality.FormattingEnabled = true;
            this.cboImageQuality.HoverState.Parent = this.cboImageQuality;
            this.cboImageQuality.ItemHeight = 30;
            this.cboImageQuality.Items.AddRange(new object[] {
            "Balanced(Default)",
            "High Quality",
            "High Speed"});
            this.cboImageQuality.ItemsAppearance.Parent = this.cboImageQuality;
            this.cboImageQuality.Location = new System.Drawing.Point(30, 118);
            this.cboImageQuality.Name = "cboImageQuality";
            this.cboImageQuality.ShadowDecoration.Parent = this.cboImageQuality;
            this.cboImageQuality.Size = new System.Drawing.Size(198, 36);
            this.cboImageQuality.TabIndex = 2;
            this.cboImageQuality.SelectedIndexChanged += new System.EventHandler(this.cboImageQuality_SelectedIndexChanged);
            this.cboImageQuality.Click += new System.EventHandler(this.cboImageQuality_Click);
            // 
            // tglHideWallpaper
            // 
            this.tglHideWallpaper.Animation = 5;
            this.tglHideWallpaper.BackColor = System.Drawing.Color.Transparent;
            this.tglHideWallpaper.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tglHideWallpaper.BackgroundImage")));
            this.tglHideWallpaper.Checked = true;
            this.tglHideWallpaper.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tglHideWallpaper.InnerCirclePadding = 3;
            this.tglHideWallpaper.Location = new System.Drawing.Point(30, 177);
            this.tglHideWallpaper.Name = "tglHideWallpaper";
            this.tglHideWallpaper.Size = new System.Drawing.Size(42, 18);
            this.tglHideWallpaper.TabIndex = 3;
            this.tglHideWallpaper.ThumbMargin = 3;
            toggleState1.BackColor = System.Drawing.Color.DarkGray;
            toggleState1.BackColorInner = System.Drawing.Color.White;
            toggleState1.BorderColor = System.Drawing.Color.DarkGray;
            toggleState1.BorderColorInner = System.Drawing.Color.White;
            toggleState1.BorderRadius = 17;
            toggleState1.BorderRadiusInner = 11;
            toggleState1.BorderThickness = 1;
            toggleState1.BorderThicknessInner = 1;
            this.tglHideWallpaper.ToggleStateDisabled = toggleState1;
            toggleState2.BackColor = System.Drawing.Color.Empty;
            toggleState2.BackColorInner = System.Drawing.Color.Empty;
            toggleState2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(0)))), ((int)(((byte)(140)))));
            toggleState2.BorderColorInner = System.Drawing.Color.Empty;
            toggleState2.BorderRadius = 1;
            toggleState2.BorderRadiusInner = 1;
            toggleState2.BorderThickness = 1;
            toggleState2.BorderThicknessInner = 1;
            this.tglHideWallpaper.ToggleStateOff = toggleState2;
            toggleState3.BackColor = System.Drawing.Color.DodgerBlue;
            toggleState3.BackColorInner = System.Drawing.Color.White;
            toggleState3.BorderColor = System.Drawing.Color.DodgerBlue;
            toggleState3.BorderColorInner = System.Drawing.Color.White;
            toggleState3.BorderRadius = 17;
            toggleState3.BorderRadiusInner = 11;
            toggleState3.BorderThickness = 1;
            toggleState3.BorderThicknessInner = 1;
            this.tglHideWallpaper.ToggleStateOn = toggleState3;
            this.tglHideWallpaper.Value = true;
            this.tglHideWallpaper.CheckedChanged += new System.EventHandler<Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs>(this.tglHideWallpaper_CheckedChanged);
            this.tglHideWallpaper.Click += new System.EventHandler(this.tglHideWallpaper_Click);
            // 
            // bunifuLabel3
            // 
            this.bunifuLabel3.AllowParentOverrides = false;
            this.bunifuLabel3.AutoEllipsis = false;
            this.bunifuLabel3.CursorType = null;
            this.bunifuLabel3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuLabel3.Location = new System.Drawing.Point(94, 177);
            this.bunifuLabel3.Name = "bunifuLabel3";
            this.bunifuLabel3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel3.Size = new System.Drawing.Size(157, 16);
            this.bunifuLabel3.TabIndex = 4;
            this.bunifuLabel3.Text = "Hide desktop wallpaper";
            this.bunifuLabel3.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel3.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuToggleSwitch1
            // 
            this.bunifuToggleSwitch1.Animation = 5;
            this.bunifuToggleSwitch1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuToggleSwitch1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuToggleSwitch1.BackgroundImage")));
            this.bunifuToggleSwitch1.Checked = true;
            this.bunifuToggleSwitch1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuToggleSwitch1.InnerCirclePadding = 3;
            this.bunifuToggleSwitch1.Location = new System.Drawing.Point(30, 224);
            this.bunifuToggleSwitch1.Name = "bunifuToggleSwitch1";
            this.bunifuToggleSwitch1.Size = new System.Drawing.Size(42, 18);
            this.bunifuToggleSwitch1.TabIndex = 5;
            this.bunifuToggleSwitch1.ThumbMargin = 3;
            toggleState4.BackColor = System.Drawing.Color.DarkGray;
            toggleState4.BackColorInner = System.Drawing.Color.White;
            toggleState4.BorderColor = System.Drawing.Color.DarkGray;
            toggleState4.BorderColorInner = System.Drawing.Color.White;
            toggleState4.BorderRadius = 17;
            toggleState4.BorderRadiusInner = 11;
            toggleState4.BorderThickness = 1;
            toggleState4.BorderThicknessInner = 1;
            this.bunifuToggleSwitch1.ToggleStateDisabled = toggleState4;
            toggleState5.BackColor = System.Drawing.Color.Empty;
            toggleState5.BackColorInner = System.Drawing.Color.Empty;
            toggleState5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(0)))), ((int)(((byte)(140)))));
            toggleState5.BorderColorInner = System.Drawing.Color.Empty;
            toggleState5.BorderRadius = 1;
            toggleState5.BorderRadiusInner = 1;
            toggleState5.BorderThickness = 1;
            toggleState5.BorderThicknessInner = 1;
            this.bunifuToggleSwitch1.ToggleStateOff = toggleState5;
            toggleState6.BackColor = System.Drawing.Color.DodgerBlue;
            toggleState6.BackColorInner = System.Drawing.Color.White;
            toggleState6.BorderColor = System.Drawing.Color.DodgerBlue;
            toggleState6.BorderColorInner = System.Drawing.Color.White;
            toggleState6.BorderRadius = 17;
            toggleState6.BorderRadiusInner = 11;
            toggleState6.BorderThickness = 1;
            toggleState6.BorderThicknessInner = 1;
            this.bunifuToggleSwitch1.ToggleStateOn = toggleState6;
            this.bunifuToggleSwitch1.Value = true;
            this.bunifuToggleSwitch1.CheckedChanged += new System.EventHandler<Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs>(this.bunifuToggleSwitch1_CheckedChanged);
            this.bunifuToggleSwitch1.Click += new System.EventHandler(this.bunifuToggleSwitch1_Click);
            // 
            // bunifuLabel4
            // 
            this.bunifuLabel4.AllowParentOverrides = false;
            this.bunifuLabel4.AutoEllipsis = false;
            this.bunifuLabel4.CursorType = null;
            this.bunifuLabel4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuLabel4.Location = new System.Drawing.Point(94, 226);
            this.bunifuLabel4.Name = "bunifuLabel4";
            this.bunifuLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel4.Size = new System.Drawing.Size(123, 16);
            this.bunifuLabel4.TabIndex = 6;
            this.bunifuLabel4.Text = "Save security code";
            this.bunifuLabel4.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel4.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // frmController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(657, 458);
            this.Controls.Add(this.bunifuLabel4);
            this.Controls.Add(this.bunifuToggleSwitch1);
            this.Controls.Add(this.bunifuLabel3);
            this.Controls.Add(this.tglHideWallpaper);
            this.Controls.Add(this.cboImageQuality);
            this.Controls.Add(this.bunifuLabel2);
            this.Controls.Add(this.bunifuLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmController";
            this.Text = "frmController";
            this.Load += new System.EventHandler(this.frmController_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmController_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel1;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel2;
        private Guna.UI2.WinForms.Guna2ComboBox cboImageQuality;
        private Bunifu.UI.WinForms.BunifuToggleSwitch tglHideWallpaper;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel3;
        private Bunifu.UI.WinForms.BunifuToggleSwitch bunifuToggleSwitch1;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel4;
    }
}