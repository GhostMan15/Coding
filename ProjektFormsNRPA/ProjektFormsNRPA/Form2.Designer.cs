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
            id.Text = "Id:";
            // 
            // transakcije
            // 
            transakcije.FormattingEnabled = true;
            transakcije.ItemHeight = 15;
            transakcije.Location = new Point(23, 71);
            transakcije.Name = "transakcije";
            transakcije.Size = new Size(120, 94);
            transakcije.TabIndex = 2;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(transakcije);
            Controls.Add(id);
            Controls.Add(ime);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ime;
        private Label id;
        private ListBox transakcije;
    }
}