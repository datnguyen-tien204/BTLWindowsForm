﻿namespace NguyenTienDat_10122119
{
    partial class frmDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDashboard));
            this.lblChaoMung = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuPanel1 = new Bunifu.UI.WinForms.BunifuPanel();
            this.bunifuLabel1 = new Bunifu.UI.WinForms.BunifuLabel();
            this.SuspendLayout();
            // 
            // lblChaoMung
            // 
            this.lblChaoMung.AllowParentOverrides = false;
            this.lblChaoMung.AutoEllipsis = false;
            this.lblChaoMung.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblChaoMung.CursorType = System.Windows.Forms.Cursors.Default;
            this.lblChaoMung.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChaoMung.Location = new System.Drawing.Point(28, 12);
            this.lblChaoMung.Name = "lblChaoMung";
            this.lblChaoMung.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblChaoMung.Size = new System.Drawing.Size(256, 25);
            this.lblChaoMung.TabIndex = 0;
            this.lblChaoMung.Text = "Good Morning, Someone";
            this.lblChaoMung.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lblChaoMung.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuPanel1
            // 
            this.bunifuPanel1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bunifuPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuPanel1.BackgroundImage")));
            this.bunifuPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuPanel1.BorderColor = System.Drawing.Color.Transparent;
            this.bunifuPanel1.BorderRadius = 25;
            this.bunifuPanel1.BorderThickness = 1;
            this.bunifuPanel1.Location = new System.Drawing.Point(28, 69);
            this.bunifuPanel1.Name = "bunifuPanel1";
            this.bunifuPanel1.ShowBorders = true;
            this.bunifuPanel1.Size = new System.Drawing.Size(212, 292);
            this.bunifuPanel1.TabIndex = 1;
            // 
            // bunifuLabel1
            // 
            this.bunifuLabel1.AllowParentOverrides = false;
            this.bunifuLabel1.AutoEllipsis = false;
            this.bunifuLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel1.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuLabel1.Location = new System.Drawing.Point(28, 44);
            this.bunifuLabel1.Name = "bunifuLabel1";
            this.bunifuLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel1.Size = new System.Drawing.Size(111, 19);
            this.bunifuLabel1.TabIndex = 2;
            this.bunifuLabel1.Text = "Recent Session";
            this.bunifuLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel1.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(566, 375);
            this.Controls.Add(this.bunifuLabel1);
            this.Controls.Add(this.bunifuPanel1);
            this.Controls.Add(this.lblChaoMung);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(251)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDashboard";
            this.Text = "frmDashboard";
            this.Load += new System.EventHandler(this.frmDashboard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.UI.WinForms.BunifuLabel lblChaoMung;
        private Bunifu.UI.WinForms.BunifuPanel bunifuPanel1;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel1;
    }
}