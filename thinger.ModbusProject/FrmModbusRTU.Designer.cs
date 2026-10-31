namespace thinger.ModbusProject
{
    partial class FrmModbusRTU
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
            groupBox1 = new GroupBox();
            btn_Disconnect = new Button();
            btn_Connect = new Button();
            label6 = new Label();
            cmb_DataFormat = new ComboBox();
            label5 = new Label();
            cmb_StopBits = new ComboBox();
            label4 = new Label();
            cmb_DataBits = new ComboBox();
            label3 = new Label();
            cmb_Parity = new ComboBox();
            label2 = new Label();
            cmb_BaudRate = new ComboBox();
            label1 = new Label();
            cmb_Port = new ComboBox();
            groupBox2 = new GroupBox();
            label13 = new Label();
            listView1 = new ListView();
            textBox4 = new TextBox();
            txt_ReadLength = new TextBox();
            txt_StartAddress = new TextBox();
            txt_SlaveId = new TextBox();
            label12 = new Label();
            btn_Write = new Button();
            btn_Read = new Button();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            cmb_DataType = new ComboBox();
            label8 = new Label();
            cmb_StoreArea = new ComboBox();
            label7 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btn_Disconnect);
            groupBox1.Controls.Add(btn_Connect);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(cmb_DataFormat);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(cmb_StopBits);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(cmb_DataBits);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(cmb_Parity);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(cmb_BaudRate);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cmb_Port);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(604, 114);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "通信参数";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // btn_Disconnect
            // 
            btn_Disconnect.Location = new Point(508, 67);
            btn_Disconnect.Name = "btn_Disconnect";
            btn_Disconnect.Size = new Size(75, 29);
            btn_Disconnect.TabIndex = 13;
            btn_Disconnect.Text = "断开连接";
            btn_Disconnect.UseVisualStyleBackColor = true;
            // 
            // btn_Connect
            // 
            btn_Connect.Location = new Point(362, 67);
            btn_Connect.Name = "btn_Connect";
            btn_Connect.Size = new Size(75, 29);
            btn_Connect.TabIndex = 12;
            btn_Connect.Text = "建立连接";
            btn_Connect.UseVisualStyleBackColor = true;
            btn_Connect.Click += button1_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(166, 70);
            label6.Name = "label6";
            label6.Size = new Size(44, 17);
            label6.TabIndex = 11;
            label6.Text = "大小端";
            // 
            // cmb_DataFormat
            // 
            cmb_DataFormat.FormattingEnabled = true;
            cmb_DataFormat.Location = new Point(215, 67);
            cmb_DataFormat.Name = "cmb_DataFormat";
            cmb_DataFormat.Size = new Size(82, 25);
            cmb_DataFormat.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(13, 70);
            label5.Name = "label5";
            label5.Size = new Size(44, 17);
            label5.TabIndex = 9;
            label5.Text = "停止位";
            // 
            // cmb_StopBits
            // 
            cmb_StopBits.FormattingEnabled = true;
            cmb_StopBits.Location = new Point(62, 67);
            cmb_StopBits.Name = "cmb_StopBits";
            cmb_StopBits.Size = new Size(82, 25);
            cmb_StopBits.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(458, 25);
            label4.Name = "label4";
            label4.Size = new Size(44, 17);
            label4.TabIndex = 7;
            label4.Text = "数据位";
            // 
            // cmb_DataBits
            // 
            cmb_DataBits.FormattingEnabled = true;
            cmb_DataBits.Location = new Point(508, 22);
            cmb_DataBits.Name = "cmb_DataBits";
            cmb_DataBits.Size = new Size(82, 25);
            cmb_DataBits.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(312, 25);
            label3.Name = "label3";
            label3.Size = new Size(44, 17);
            label3.TabIndex = 5;
            label3.Text = "校验位";
            // 
            // cmb_Parity
            // 
            cmb_Parity.FormattingEnabled = true;
            cmb_Parity.Location = new Point(362, 22);
            cmb_Parity.Name = "cmb_Parity";
            cmb_Parity.Size = new Size(82, 25);
            cmb_Parity.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(165, 25);
            label2.Name = "label2";
            label2.Size = new Size(44, 17);
            label2.TabIndex = 3;
            label2.Text = "波特率";
            label2.Click += label2_Click;
            // 
            // cmb_BaudRate
            // 
            cmb_BaudRate.FormattingEnabled = true;
            cmb_BaudRate.Location = new Point(215, 22);
            cmb_BaudRate.Name = "cmb_BaudRate";
            cmb_BaudRate.Size = new Size(82, 25);
            cmb_BaudRate.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 25);
            label1.Name = "label1";
            label1.Size = new Size(44, 17);
            label1.TabIndex = 1;
            label1.Text = "端口号";
            // 
            // cmb_Port
            // 
            cmb_Port.FormattingEnabled = true;
            cmb_Port.Location = new Point(62, 22);
            cmb_Port.Name = "cmb_Port";
            cmb_Port.Size = new Size(82, 25);
            cmb_Port.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(listView1);
            groupBox2.Controls.Add(textBox4);
            groupBox2.Controls.Add(txt_ReadLength);
            groupBox2.Controls.Add(txt_StartAddress);
            groupBox2.Controls.Add(txt_SlaveId);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(btn_Write);
            groupBox2.Controls.Add(btn_Read);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(cmb_DataType);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(cmb_StoreArea);
            groupBox2.Controls.Add(label7);
            groupBox2.Location = new Point(12, 152);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(604, 359);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "读写测试";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(13, 166);
            label13.Name = "label13";
            label13.Size = new Size(56, 17);
            label13.TabIndex = 28;
            label13.Text = "起始地址";
            label13.Click += label13_Click;
            // 
            // listView1
            // 
            listView1.Location = new Point(13, 196);
            listView1.Name = "listView1";
            listView1.Size = new Size(577, 148);
            listView1.TabIndex = 27;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // textBox4
            // 
            textBox4.Font = new Font("宋体", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            textBox4.Location = new Point(159, 132);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(197, 23);
            textBox4.TabIndex = 26;
            // 
            // txt_ReadLength
            // 
            txt_ReadLength.Font = new Font("宋体", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txt_ReadLength.Location = new Point(274, 84);
            txt_ReadLength.Name = "txt_ReadLength";
            txt_ReadLength.Size = new Size(82, 23);
            txt_ReadLength.TabIndex = 25;
            // 
            // txt_StartAddress
            // 
            txt_StartAddress.Font = new Font("宋体", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txt_StartAddress.Location = new Point(85, 80);
            txt_StartAddress.Name = "txt_StartAddress";
            txt_StartAddress.Size = new Size(82, 23);
            txt_StartAddress.TabIndex = 24;
            // 
            // txt_SlaveId
            // 
            txt_SlaveId.Font = new Font("宋体", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txt_SlaveId.Location = new Point(85, 36);
            txt_SlaveId.Name = "txt_SlaveId";
            txt_SlaveId.Size = new Size(82, 23);
            txt_SlaveId.TabIndex = 23;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(13, 134);
            label12.Name = "label12";
            label12.Size = new Size(140, 17);
            label12.TabIndex = 22;
            label12.Text = "写入数据（空格分隔）：";
            // 
            // btn_Write
            // 
            btn_Write.Location = new Point(423, 134);
            btn_Write.Name = "btn_Write";
            btn_Write.Size = new Size(75, 29);
            btn_Write.TabIndex = 21;
            btn_Write.Text = "写入";
            btn_Write.UseVisualStyleBackColor = true;
            // 
            // btn_Read
            // 
            btn_Read.Location = new Point(423, 86);
            btn_Read.Name = "btn_Read";
            btn_Read.Size = new Size(75, 29);
            btn_Read.TabIndex = 20;
            btn_Read.Text = "读取";
            btn_Read.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(208, 86);
            label11.Name = "label11";
            label11.Size = new Size(56, 17);
            label11.TabIndex = 19;
            label11.Text = "读取长度";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(13, 86);
            label10.Name = "label10";
            label10.Size = new Size(56, 17);
            label10.TabIndex = 17;
            label10.Text = "起始地址";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(423, 41);
            label9.Name = "label9";
            label9.Size = new Size(56, 17);
            label9.TabIndex = 15;
            label9.Text = "数据类型";
            // 
            // cmb_DataType
            // 
            cmb_DataType.FormattingEnabled = true;
            cmb_DataType.Location = new Point(485, 36);
            cmb_DataType.Name = "cmb_DataType";
            cmb_DataType.Size = new Size(82, 25);
            cmb_DataType.TabIndex = 14;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(208, 41);
            label8.Name = "label8";
            label8.Size = new Size(44, 17);
            label8.TabIndex = 13;
            label8.Text = "存储区";
            // 
            // cmb_StoreArea
            // 
            cmb_StoreArea.FormattingEnabled = true;
            cmb_StoreArea.Location = new Point(274, 35);
            cmb_StoreArea.Name = "cmb_StoreArea";
            cmb_StoreArea.Size = new Size(82, 25);
            cmb_StoreArea.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(13, 38);
            label7.Name = "label7";
            label7.Size = new Size(56, 17);
            label7.TabIndex = 11;
            label7.Text = "从站地址";
            // 
            // FrmModbusRTU
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(628, 523);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            MaximizeBox = false;
            Name = "FrmModbusRTU";
            Text = "基于ModbusRTU的通信测试平台";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btn_Connect;
        private Label label6;
        private ComboBox cmb_DataFormat;
        private Label label5;
        private ComboBox cmb_StopBits;
        private Label label4;
        private ComboBox cmb_DataBits;
        private Label label3;
        private ComboBox cmb_Parity;
        private Label label2;
        private ComboBox cmb_BaudRate;
        private Label label1;
        private ComboBox cmb_Port;
        private Button btn_Disconnect;
        private GroupBox groupBox2;
        private Label label12;
        private Button btn_Write;
        private Button btn_Read;
        private Label label11;
        private Label label10;
        private Label label9;
        private ComboBox cmb_DataType;
        private Label label8;
        private ComboBox cmb_StoreArea;
        private Label label7;
        private Label label13;
        private ListView listView1;
        private TextBox textBox4;
        private TextBox txt_ReadLength;
        private TextBox txt_StartAddress;
        private TextBox txt_SlaveId;
    }
}
