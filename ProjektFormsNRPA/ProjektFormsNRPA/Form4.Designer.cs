namespace ProjektFormsNRPA
{
    partial class Form4
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
            nakazi = new TextBox();
            label1 = new Label();
            nakaziP = new Button();
            SuspendLayout();
            // 
            // nakazi
            // 
            nakazi.Location = new Point(45, 38);
            nakazi.Name = "nakazi";
            nakazi.Size = new Size(100, 23);
            nakazi.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 20);
            label1.Name = "label1";
            label1.Size = new Size(80, 15);
            label1.TabIndex = 1;
            label1.Text = "Vpišite zensek";
            // 
            // nakaziP
            // 
            nakaziP.Location = new Point(45, 79);
            nakaziP.Name = "nakaziP";
            nakaziP.Size = new Size(100, 23);
            nakaziP.TabIndex = 2;
            nakaziP.Text = "Potrdi";
            nakaziP.UseVisualStyleBackColor = true;
            nakaziP.Click += nakaziP_Click;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(181, 114);
            ControlBox = false;
            Controls.Add(nakaziP);
            Controls.Add(label1);
            Controls.Add(nakazi);
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "Form4";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form4";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox nakazi;
        private Label label1;
        private Button nakaziP;
    }
}