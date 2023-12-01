namespace NguyenTienDat_10122119
{
    partial class frmViewScreen
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
            this.pbViewScreen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbViewScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // pbViewScreen
            // 
            this.pbViewScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbViewScreen.Location = new System.Drawing.Point(0, 0);
            this.pbViewScreen.Name = "pbViewScreen";
            this.pbViewScreen.Size = new System.Drawing.Size(800, 450);
            this.pbViewScreen.TabIndex = 0;
            this.pbViewScreen.TabStop = false;
            // 
            // frmViewScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbViewScreen);
            this.Name = "frmViewScreen";
            this.Text = "frmViewScreen";
            this.Load += new System.EventHandler(this.frmViewScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbViewScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbViewScreen;
    }
}