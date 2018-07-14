namespace Dyzen
{
    partial class Dyzen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dyzen));
            this.gfxBox1 = new controls.GFXBox();
            ((System.ComponentModel.ISupportInitialize)(this.gfxBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gfxBox1
            // 
            this.gfxBox1.BehindBitmap = ((System.Drawing.Bitmap)(resources.GetObject("gfxBox1.BehindBitmap")));
            this.gfxBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gfxBox1.Image = ((System.Drawing.Image)(resources.GetObject("gfxBox1.Image")));
            this.gfxBox1.Location = new System.Drawing.Point(114, 79);
            this.gfxBox1.Name = "gfxBox1";
            this.gfxBox1.Selection = new System.Drawing.Rectangle(0, 0, 64, 64);
            this.gfxBox1.SelectionAccuracy = 8;
            this.gfxBox1.SelectionMinSize = 16;
            this.gfxBox1.Size = new System.Drawing.Size(512, 256);
            this.gfxBox1.TabIndex = 0;
            this.gfxBox1.TabStop = false;
            this.gfxBox1.Zoom = 4;
            // 
            // Dyzen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gfxBox1);
            this.Name = "Dyzen";
            this.Text = "Dyzen";
            ((System.ComponentModel.ISupportInitialize)(this.gfxBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private controls.GFXBox gfxBox1;
    }
}