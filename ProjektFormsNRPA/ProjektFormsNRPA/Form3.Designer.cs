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
            label3 = new Label();
            Id = new TextBox();
            Stanje = new TextBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // Dodaj
            // 
            Dodaj.Location = new Point(323, 341);
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
            checkBox1.Location = new Point(324, 300);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(74, 19);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "Zaposlen";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(324, 188);
            label3.Name = "label3";
            label3.Size = new Size(20, 15);
            label3.TabIndex = 8;
            label3.Text = "Id:";
            // 
            // Id
            // 
            Id.Location = new Point(324, 206);
            Id.Name = "Id";
            Id.Size = new Size(100, 23);
            Id.TabIndex = 9;
            // 
            // Stanje
            // 
            Stanje.Location = new Point(323, 254);
            Stanje.Name = "Stanje";
            Stanje.Size = new Size(101, 23);
            Stanje.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(323, 236);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 11;
            label4.Text = "Stanje:";
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(Stanje);
            Controls.Add(Id);
            Controls.Add(label3);
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
        private Label label3;
        private TextBox Id;
        private TextBox Stanje;
        private Label label4;
    }
}