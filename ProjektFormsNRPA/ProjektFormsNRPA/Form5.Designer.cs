namespace ProjektFormsNRPA
{
    partial class Form5
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
            label1 = new Label();
            dvig = new TextBox();
            dvigP = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 23);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 0;
            label1.Text = "Vpišite zenesek";
            // 
            // dvig
            // 
            dvig.Location = new Point(30, 41);
            dvig.Name = "dvig";
            dvig.Size = new Size(100, 23);
            dvig.TabIndex = 1;
            // 
            // dvigP
            // 
            dvigP.Location = new Point(30, 79);
            dvigP.Name = "dvigP";
            dvigP.Size = new Size(100, 23);
            dvigP.TabIndex = 2;
            dvigP.Text = "Potrdi";
            dvigP.UseVisualStyleBackColor = true;
            dvigP.Click += dvigP_Click;
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(152, 122);
            ControlBox = false;
            Controls.Add(dvigP);
            Controls.Add(dvig);
            Controls.Add(label1);
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "Form5";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form5";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox dvig;
        private Button dvigP;
    }
}