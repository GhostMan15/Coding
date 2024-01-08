namespace ProjektFormsNRPA
{
    partial class Form2
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
            ime = new Label();
            id = new Label();
            transakcije = new ListBox();
            Zaposleni = new Button();
            stanje = new Label();
            dvig = new Button();
            nakazi = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // ime
            // 
            ime.AutoSize = true;
            ime.Location = new Point(23, 17);
            ime.Name = "ime";
            ime.Size = new Size(30, 15);
            ime.TabIndex = 0;
            ime.Text = "Ime:";
            // 
            // id
            // 
            id.AutoSize = true;
            id.Location = new Point(23, 44);
            id.Name = "id";
            id.Size = new Size(20, 15);
            id.TabIndex = 1;
            id.Text = "Id:&";
            // 
            // transakcije
            // 
            transakcije.FormattingEnabled = true;
            transakcije.ItemHeight = 15;
            transakcije.Location = new Point(271, 134);
            transakcije.Name = "transakcije";
            transakcije.Size = new Size(517, 304);
            transakcije.TabIndex = 2;
            transakcije.SelectedIndexChanged += transakcije_SelectedIndexChanged;
            // 
            // Zaposleni
            // 
            Zaposleni.Location = new Point(600, 17);
            Zaposleni.Name = "Zaposleni";
            Zaposleni.Size = new Size(172, 23);
            Zaposleni.TabIndex = 3;
            Zaposleni.Text = "DodajUporabnika";
            Zaposleni.UseVisualStyleBackColor = true;
            Zaposleni.Click += Zaposleni_Click;
            // 
            // stanje
            // 
            stanje.AutoSize = true;
            stanje.Location = new Point(23, 75);
            stanje.Name = "stanje";
            stanje.Size = new Size(42, 15);
            stanje.TabIndex = 5;
            stanje.Text = "Stanje:";
            // 
            // dvig
            // 
            dvig.Location = new Point(295, 27);
            dvig.Name = "dvig";
            dvig.Size = new Size(75, 23);
            dvig.TabIndex = 6;
            dvig.Text = "Dvig";
            dvig.UseVisualStyleBackColor = true;
            dvig.Click += dvig_Click;
            // 
            // nakazi
            // 
            nakazi.Location = new Point(295, 67);
            nakazi.Name = "nakazi";
            nakazi.Size = new Size(75, 23);
            nakazi.TabIndex = 7;
            nakazi.Text = "Nakaži";
            nakazi.UseVisualStyleBackColor = true;
            nakazi.Click += nakazi_Click;
            // 
            // button1
            // 
            button1.Location = new Point(23, 415);
            button1.Name = "button1";
            button1.Size = new Size(191, 23);
            button1.TabIndex = 8;
            button1.Text = "Odjava";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(nakazi);
            Controls.Add(dvig);
            Controls.Add(stanje);
            Controls.Add(Zaposleni);
            Controls.Add(transakcije);
            Controls.Add(id);
            Controls.Add(ime);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form2";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ime;
        private Label id;
        private ListBox transakcije;
        private Button Zaposleni;
        private Label stanje;
        private Button dvig;
        private Button nakazi;
        private Button button1;
    }
}