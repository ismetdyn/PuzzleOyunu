
namespace Proje5._36_PuzzleOyunu
{
    partial class Form1
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
            this.pbAlan = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbAlan)).BeginInit();
            this.SuspendLayout();
            // 
            // pbAlan
            // 
            this.pbAlan.BackColor = System.Drawing.Color.White;
            this.pbAlan.Location = new System.Drawing.Point(63, 22);
            this.pbAlan.Name = "pbAlan";
            this.pbAlan.Size = new System.Drawing.Size(100, 50);
            this.pbAlan.TabIndex = 0;
            this.pbAlan.TabStop = false;
            this.pbAlan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbAlan_MouseDown);
            this.pbAlan.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbAlan_MouseMove);
            this.pbAlan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbAlan_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbAlan);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbAlan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbAlan;
    }
}

