namespace Magna_TestApplication
{
    partial class Magna
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Magna));
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            panel3 = new Panel();
            PrintButton = new Button();
            LabelText_TB = new TextBox();
            label1 = new Label();
            PrinterStatus_LBL = new Label();
            label2 = new Label();
            ConnectionStatus_LBL = new Label();
            label3 = new Label();
            PrinterName_CB = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1555, 98);
            panel1.TabIndex = 0;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(1439, 22);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(106, 55);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(168, 55);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.Controls.Add(panel3);
            panel2.Location = new Point(0, 104);
            panel2.Name = "panel2";
            panel2.Size = new Size(1555, 676);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Gainsboro;
            panel3.Controls.Add(PrintButton);
            panel3.Controls.Add(LabelText_TB);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(PrinterStatus_LBL);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(ConnectionStatus_LBL);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(PrinterName_CB);
            panel3.Controls.Add(label4);
            panel3.Controls.Add(label5);
            panel3.Location = new Point(449, 284);
            panel3.Name = "panel3";
            panel3.Size = new Size(270, 275);
            panel3.TabIndex = 12;
            // 
            // PrintButton
            // 
            PrintButton.Location = new Point(182, 225);
            PrintButton.Name = "PrintButton";
            PrintButton.Size = new Size(69, 32);
            PrintButton.TabIndex = 2;
            PrintButton.Text = "Print";
            PrintButton.UseVisualStyleBackColor = true;
            PrintButton.Click += PrintButton_Click;
            // 
            // LabelText_TB
            // 
            LabelText_TB.BorderStyle = BorderStyle.FixedSingle;
            LabelText_TB.Location = new Point(130, 180);
            LabelText_TB.Name = "LabelText_TB";
            LabelText_TB.Size = new Size(121, 23);
            LabelText_TB.TabIndex = 11;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(72, 23);
            label1.Name = "label1";
            label1.Size = new Size(105, 17);
            label1.TabIndex = 3;
            label1.Text = "TSC TE210 TEST ";
            // 
            // PrinterStatus_LBL
            // 
            PrinterStatus_LBL.AutoSize = true;
            PrinterStatus_LBL.Location = new Point(101, 143);
            PrinterStatus_LBL.Name = "PrinterStatus_LBL";
            PrinterStatus_LBL.Size = new Size(98, 15);
            PrinterStatus_LBL.TabIndex = 10;
            PrinterStatus_LBL.Text = "PrinterStatus_LBL";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 75);
            label2.Name = "label2";
            label2.Size = new Size(48, 15);
            label2.TabIndex = 4;
            label2.Text = "Printer :";
            // 
            // ConnectionStatus_LBL
            // 
            ConnectionStatus_LBL.AutoSize = true;
            ConnectionStatus_LBL.Location = new Point(101, 111);
            ConnectionStatus_LBL.Name = "ConnectionStatus_LBL";
            ConnectionStatus_LBL.Size = new Size(125, 15);
            ConnectionStatus_LBL.TabIndex = 9;
            ConnectionStatus_LBL.Text = "ConnectionStatus_LBL";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 111);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 5;
            label3.Text = "Connection :";
            // 
            // PrinterName_CB
            // 
            PrinterName_CB.FormattingEnabled = true;
            PrinterName_CB.Location = new Point(130, 67);
            PrinterName_CB.Name = "PrinterName_CB";
            PrinterName_CB.Size = new Size(121, 23);
            PrinterName_CB.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 183);
            label4.Name = "label4";
            label4.Size = new Size(65, 15);
            label4.TabIndex = 6;
            label4.Text = "Label Text :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(20, 143);
            label5.Name = "label5";
            label5.Size = new Size(48, 15);
            label5.TabIndex = 7;
            label5.Text = "Printer :";
            // 
            // Magna
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1557, 782);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Magna";
            Text = "Magna";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Panel panel2;
        private PictureBox pictureBox2;
        private Button PrintButton;
        private Label label1;
        private Label label2;
        private TextBox LabelText_TB;
        private Label PrinterStatus_LBL;
        private Label ConnectionStatus_LBL;
        private ComboBox PrinterName_CB;
        private Label label5;
        private Label label4;
        private Label label3;
        private Panel panel3;
    }
}
