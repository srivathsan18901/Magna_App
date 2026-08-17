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
            button1 = new Button();
            PRINT_STS = new Label();
            PLC_LBL = new Label();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            FT_DGV = new DataGridView();
            label2 = new Label();
            label1 = new Label();
            TET_DGV = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)FT_DGV).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TET_DGV).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(PRINT_STS);
            panel1.Controls.Add(PLC_LBL);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1612, 98);
            panel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(1368, 31);
            button1.Name = "button1";
            button1.Size = new Size(122, 31);
            button1.TabIndex = 4;
            button1.Text = "Export as Excel";
            button1.UseVisualStyleBackColor = true;
            // 
            // PRINT_STS
            // 
            PRINT_STS.AutoSize = true;
            PRINT_STS.Location = new Point(212, 62);
            PRINT_STS.Name = "PRINT_STS";
            PRINT_STS.Size = new Size(77, 15);
            PRINT_STS.TabIndex = 3;
            PRINT_STS.Text = "Printer Status";
            // 
            // PLC_LBL
            // 
            PLC_LBL.AutoSize = true;
            PLC_LBL.Location = new Point(212, 31);
            PLC_LBL.Name = "PLC_LBL";
            PLC_LBL.Size = new Size(63, 15);
            PLC_LBL.TabIndex = 2;
            PLC_LBL.Text = "PLC Status";
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(1496, 22);
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
            panel2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.Transparent;
            panel2.Controls.Add(FT_DGV);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(TET_DGV);
            panel2.Location = new Point(0, 108);
            panel2.Name = "panel2";
            panel2.Size = new Size(1612, 676);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // FT_DGV
            // 
            FT_DGV.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            FT_DGV.BackgroundColor = SystemColors.Control;
            FT_DGV.BorderStyle = BorderStyle.Fixed3D;
            FT_DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            FT_DGV.Location = new Point(12, 29);
            FT_DGV.Name = "FT_DGV";
            FT_DGV.Size = new Size(1590, 303);
            FT_DGV.TabIndex = 4;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(12, 335);
            label2.Name = "label2";
            label2.Size = new Size(167, 19);
            label2.TabIndex = 3;
            label2.Text = "Travel and Endurance Test";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(12, 7);
            label1.Name = "label1";
            label1.Size = new Size(100, 19);
            label1.TabIndex = 2;
            label1.Text = "Functional Test";
            // 
            // TET_DGV
            // 
            TET_DGV.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TET_DGV.BackgroundColor = SystemColors.Control;
            TET_DGV.BorderStyle = BorderStyle.Fixed3D;
            TET_DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TET_DGV.Location = new Point(12, 357);
            TET_DGV.Name = "TET_DGV";
            TET_DGV.Size = new Size(1590, 313);
            TET_DGV.TabIndex = 1;
            // 
            // Magna
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1614, 790);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Magna";
            Text = "Magna";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)FT_DGV).EndInit();
            ((System.ComponentModel.ISupportInitialize)TET_DGV).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Panel panel2;
        private PictureBox pictureBox2;
        private Label label2;
        private Label label1;
        private DataGridView TET_DGV;
        private DataGridView FT_DGV;
        private Label PLC_LBL;
        private Label PRINT_STS;
        private Button button1;
    }
}
