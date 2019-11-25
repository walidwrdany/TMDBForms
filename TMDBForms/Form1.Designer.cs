namespace TMDBForms
{
    partial class Form1
    {

        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox tb;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tb = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.checkBox1_poster = new System.Windows.Forms.CheckBox();
            this.checkBox1_backdrop = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tb
            // 
            this.tb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb.Location = new System.Drawing.Point(12, 89);
            this.tb.Multiline = true;
            this.tb.Name = "tb";
            this.tb.ReadOnly = true;
            this.tb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb.Size = new System.Drawing.Size(733, 612);
            this.tb.TabIndex = 0;
            this.tb.WordWrap = false;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 44);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(635, 20);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(653, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Select Path";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_ClickAsync);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 707);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(733, 18);
            this.progressBar1.TabIndex = 3;
            // 
            // checkBox1_poster
            // 
            this.checkBox1_poster.AutoSize = true;
            this.checkBox1_poster.Location = new System.Drawing.Point(13, 21);
            this.checkBox1_poster.Name = "checkBox1_poster";
            this.checkBox1_poster.Size = new System.Drawing.Size(107, 17);
            this.checkBox1_poster.TabIndex = 4;
            this.checkBox1_poster.Text = "Download Poster";
            this.checkBox1_poster.UseVisualStyleBackColor = true;
            // 
            // checkBox1_backdrop
            // 
            this.checkBox1_backdrop.AutoSize = true;
            this.checkBox1_backdrop.Location = new System.Drawing.Point(126, 21);
            this.checkBox1_backdrop.Name = "checkBox1_backdrop";
            this.checkBox1_backdrop.Size = new System.Drawing.Size(120, 17);
            this.checkBox1_backdrop.TabIndex = 5;
            this.checkBox1_backdrop.Text = "Download Backdrop";
            this.checkBox1_backdrop.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 730);
            this.Controls.Add(this.checkBox1_backdrop);
            this.Controls.Add(this.checkBox1_poster);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tb);
            this.MinimumSize = new System.Drawing.Size(650, 365);
            this.Name = "Form1";
            this.Text = "TMDB";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1_poster;
        private System.Windows.Forms.CheckBox checkBox1_backdrop;
    }
}

