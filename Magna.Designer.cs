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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            panel1 = new Panel();
            QR_LBL = new Label();
            QR_PB = new PictureBox();
            pictureBox2 = new PictureBox();
            PRINT_STS = new Label();
            pictureBox1 = new PictureBox();
            PLC_LBL = new Label();
            button1 = new Button();
            panel2 = new Panel();
            QTY_LBL = new Label();
            FT_DGV = new DataGridView();
            FT_SNo = new DataGridViewTextBoxColumn();
            FT_Date = new DataGridViewTextBoxColumn();
            FT_Time = new DataGridViewTextBoxColumn();
            FT_Shift = new DataGridViewTextBoxColumn();
            FT_Variant = new DataGridViewTextBoxColumn();
            FT_Result = new DataGridViewTextBoxColumn();
            label2 = new Label();
            label1 = new Label();
            TET_DGV = new DataGridView();
            TE_SNo = new DataGridViewTextBoxColumn();
            TE_Date = new DataGridViewTextBoxColumn();
            TE_Time = new DataGridViewTextBoxColumn();
            TE_Shift = new DataGridViewTextBoxColumn();
            TE_Variant = new DataGridViewTextBoxColumn();
            TE_Result = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)QR_PB).BeginInit();
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
            panel1.Controls.Add(QR_LBL);
            panel1.Controls.Add(QR_PB);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(PRINT_STS);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(PLC_LBL);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1612, 98);
            panel1.TabIndex = 0;
            // 
            // QR_LBL
            // 
            QR_LBL.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            QR_LBL.AutoSize = true;
            QR_LBL.Location = new Point(1364, 42);
            QR_LBL.Name = "QR_LBL";
            QR_LBL.Size = new Size(0, 15);
            QR_LBL.TabIndex = 5;
            // 
            // QR_PB
            // 
            QR_PB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            QR_PB.Location = new Point(1268, 22);
            QR_PB.Name = "QR_PB";
            QR_PB.Size = new Size(90, 55);
            QR_PB.SizeMode = PictureBoxSizeMode.StretchImage;
            QR_PB.TabIndex = 4;
            QR_PB.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(1512, 22);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(90, 55);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // PRINT_STS
            // 
            PRINT_STS.AutoSize = true;
            PRINT_STS.Location = new Point(205, 62);
            PRINT_STS.Name = "PRINT_STS";
            PRINT_STS.Size = new Size(77, 15);
            PRINT_STS.TabIndex = 3;
            PRINT_STS.Text = "Printer Status";
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
            // PLC_LBL
            // 
            PLC_LBL.AutoSize = true;
            PLC_LBL.Location = new Point(205, 32);
            PLC_LBL.Name = "PLC_LBL";
            PLC_LBL.Size = new Size(63, 15);
            PLC_LBL.TabIndex = 2;
            PLC_LBL.Text = "PLC Status";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Right;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(1480, 17);
            button1.Name = "button1";
            button1.Size = new Size(122, 31);
            button1.TabIndex = 4;
            button1.Text = "Export as Excel";
            button1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.Transparent;
            panel2.Controls.Add(QTY_LBL);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(FT_DGV);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(TET_DGV);
            panel2.Location = new Point(0, 104);
            panel2.Name = "panel2";
            panel2.Size = new Size(1612, 707);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // QTY_LBL
            // 
            QTY_LBL.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            QTY_LBL.AutoSize = true;
            QTY_LBL.Location = new Point(1549, 366);
            QTY_LBL.Name = "QTY_LBL";
            QTY_LBL.Size = new Size(53, 15);
            QTY_LBL.TabIndex = 5;
            QTY_LBL.Text = "Quantity";
            // 
            // FT_DGV
            // 
            FT_DGV.AllowUserToAddRows = false;
            FT_DGV.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            FT_DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            FT_DGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            FT_DGV.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.InfoText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            FT_DGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            FT_DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            FT_DGV.Columns.AddRange(new DataGridViewColumn[] { FT_SNo, FT_Date, FT_Time, FT_Shift, FT_Variant, FT_Result });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            FT_DGV.DefaultCellStyle = dataGridViewCellStyle2;
            FT_DGV.Location = new Point(12, 59);
            FT_DGV.MultiSelect = false;
            FT_DGV.Name = "FT_DGV";
            FT_DGV.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.ButtonHighlight;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            FT_DGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            FT_DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            FT_DGV.Size = new Size(1590, 303);
            FT_DGV.TabIndex = 4;
            // 
            // FT_SNo
            // 
            FT_SNo.DataPropertyName = "SNo";
            FT_SNo.HeaderText = "S.no";
            FT_SNo.Name = "FT_SNo";
            FT_SNo.ReadOnly = true;
            // 
            // FT_Date
            // 
            FT_Date.DataPropertyName = "Date";
            FT_Date.HeaderText = "Date";
            FT_Date.Name = "FT_Date";
            FT_Date.ReadOnly = true;
            // 
            // FT_Time
            // 
            FT_Time.DataPropertyName = "Time";
            FT_Time.HeaderText = "Time";
            FT_Time.Name = "FT_Time";
            FT_Time.ReadOnly = true;
            // 
            // FT_Shift
            // 
            FT_Shift.DataPropertyName = "Shift";
            FT_Shift.HeaderText = "Shift";
            FT_Shift.Name = "FT_Shift";
            FT_Shift.ReadOnly = true;
            // 
            // FT_Variant
            // 
            FT_Variant.DataPropertyName = "Variant";
            FT_Variant.HeaderText = "Variant";
            FT_Variant.Name = "FT_Variant";
            FT_Variant.ReadOnly = true;
            // 
            // FT_Result
            // 
            FT_Result.DataPropertyName = "Result";
            FT_Result.HeaderText = "Result";
            FT_Result.Name = "FT_Result";
            FT_Result.ReadOnly = true;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label2.Location = new Point(12, 367);
            label2.Name = "label2";
            label2.Size = new Size(169, 17);
            label2.TabIndex = 3;
            label2.Text = "Travel and Endurance Test";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label1.Location = new Point(12, 22);
            label1.Name = "label1";
            label1.Size = new Size(102, 17);
            label1.TabIndex = 2;
            label1.Text = "Functional Test";
            // 
            // TET_DGV
            // 
            TET_DGV.AllowUserToAddRows = false;
            TET_DGV.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TET_DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TET_DGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            TET_DGV.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle4.BackColor = Color.LightBlue;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowFrame;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            TET_DGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            TET_DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TET_DGV.Columns.AddRange(new DataGridViewColumn[] { TE_SNo, TE_Date, TE_Time, TE_Shift, TE_Variant, TE_Result });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.Transparent;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            TET_DGV.DefaultCellStyle = dataGridViewCellStyle5;
            TET_DGV.Location = new Point(12, 392);
            TET_DGV.MultiSelect = false;
            TET_DGV.Name = "TET_DGV";
            TET_DGV.ReadOnly = true;
            TET_DGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            TET_DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TET_DGV.Size = new Size(1590, 304);
            TET_DGV.TabIndex = 1;
            // 
            // TE_SNo
            // 
            TE_SNo.HeaderText = "S.no";
            TE_SNo.Name = "TE_SNo";
            TE_SNo.ReadOnly = true;
            // 
            // TE_Date
            // 
            TE_Date.HeaderText = "Date";
            TE_Date.Name = "TE_Date";
            TE_Date.ReadOnly = true;
            // 
            // TE_Time
            // 
            TE_Time.HeaderText = "Time";
            TE_Time.Name = "TE_Time";
            TE_Time.ReadOnly = true;
            // 
            // TE_Shift
            // 
            TE_Shift.HeaderText = "Shift";
            TE_Shift.Name = "TE_Shift";
            TE_Shift.ReadOnly = true;
            // 
            // TE_Variant
            // 
            TE_Variant.HeaderText = "Variant";
            TE_Variant.Name = "TE_Variant";
            TE_Variant.ReadOnly = true;
            // 
            // TE_Result
            // 
            TE_Result.HeaderText = "Result";
            TE_Result.Name = "TE_Result";
            TE_Result.ReadOnly = true;
            // 
            // Magna
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1614, 823);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Magna";
            Text = "Magna";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)QR_PB).EndInit();
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
        private Label QTY_LBL;
        private DataGridViewTextBoxColumn TE_SNo;
        private DataGridViewTextBoxColumn TE_Date;
        private DataGridViewTextBoxColumn TE_Time;
        private DataGridViewTextBoxColumn TE_Shift;
        private DataGridViewTextBoxColumn TE_Variant;
        private DataGridViewTextBoxColumn TE_Result;
        private PictureBox QR_PB;
        private DataGridViewTextBoxColumn FT_SNo;
        private DataGridViewTextBoxColumn FT_Date;
        private DataGridViewTextBoxColumn FT_Time;
        private DataGridViewTextBoxColumn FT_Shift;
        private DataGridViewTextBoxColumn FT_Variant;
        private DataGridViewTextBoxColumn FT_Result;
        private Label QR_LBL;
    }
}
