namespace ProjektFormsNRPA
{
    partial class Form3
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
            Dodaj = new Button();
            nPin = new TextBox();
            Ime = new TextBox();
            label1 = new Label();
            label2 = new Label();
            checkBox1 = new CheckBox();
            SuspendLayout();
            // 
            // Dodaj
            // 
            Dodaj.Location = new Point(324, 231);
            Dodaj.Name = "Dodaj";
            Dodaj.Size = new Size(75, 23);
            Dodaj.TabIndex = 0;
            Dodaj.Text = "Dodaj";
            Dodaj.UseVisualStyleBackColor = true;
            Dodaj.Click += Dodaj_Click;
            // 
            // nPin
            // 
            nPin.Location = new Point(324, 152);
            nPin.Name = "nPin";
            nPin.Size = new Size(100, 23);
            nPin.TabIndex = 2;
            // 
            // Ime
            // 
            Ime.Location = new Point(324, 94);
            Ime.Name = "Ime";
            Ime.Size = new Size(100, 23);
            Ime.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(324, 76);
            label1.Name = "label1";
            label1.Size = new Size(30, 15);
            label1.TabIndex = 4;
            label1.Text = "Ime:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(324, 134);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 5;
            label2.Text = "Pin:";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(324, 191);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(74, 19);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "Zaposlen";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(checkBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Ime);
            Controls.Add(nPin);
            Controls.Add(Dodaj);
            Name = "Form3";
            Text = "Form3";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Dodaj;
        private TextBox nPin;
        private TextBox Ime;
        private Label label1;
        private Label label2;
        private CheckBox checkBox1;
    }
}