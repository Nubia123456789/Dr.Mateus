namespace Cartao
{
    partial class Planilha
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnItau = new Button();
            btnBradesco = new Button();
            txtBox = new RichTextBox();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(txtBox, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.5555553F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 88.44444F));
            tableLayoutPanel1.Size = new Size(800, 397);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(btnItau, 0, 0);
            tableLayoutPanel2.Controls.Add(btnBradesco, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(794, 39);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // btnItau
            // 
            btnItau.Dock = DockStyle.Fill;
            btnItau.Location = new Point(3, 3);
            btnItau.Name = "btnItau";
            btnItau.Size = new Size(391, 33);
            btnItau.TabIndex = 0;
            btnItau.Text = "Fatura Itau";
            btnItau.UseVisualStyleBackColor = true;
            btnItau.Click += btnItau_Click;
            // 
            // btnBradesco
            // 
            btnBradesco.Dock = DockStyle.Fill;
            btnBradesco.Location = new Point(400, 3);
            btnBradesco.Name = "btnBradesco";
            btnBradesco.Size = new Size(391, 33);
            btnBradesco.TabIndex = 1;
            btnBradesco.Text = "Fatura Bradesco";
            btnBradesco.UseVisualStyleBackColor = true;
            btnBradesco.Click += btnBradesco_Click;
            // 
            // txtBox
            // 
            txtBox.Dock = DockStyle.Fill;
            txtBox.Location = new Point(3, 48);
            txtBox.Name = "txtBox";
            txtBox.ReadOnly = true;
            txtBox.Size = new Size(794, 346);
            txtBox.TabIndex = 1;
            txtBox.Text = "";
            // 
            // Planilha
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 397);
            Controls.Add(tableLayoutPanel1);
            Name = "Planilha";
            Text = "Planilha Cartão";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button btnItau;
        private Button btnBradesco;
        private RichTextBox txtBox;
    }
}
