namespace RobotInterfaceGUI
{
    partial class RobotInterfaceGUI
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
            groupBox5 = new GroupBox();
            rdo_A_Complete = new RadioButton();
            rdo_A_Busy = new RadioButton();
            rdo_A_Ready = new RadioButton();
            cmb_BinNumber = new ComboBox();
            btn_CloseBin = new Button();
            btn_OpenBin = new Button();
            label1 = new Label();
            groupBox2 = new GroupBox();
            groupBox6 = new GroupBox();
            rdo_U_Complete = new RadioButton();
            rdo_U_Busy = new RadioButton();
            rdo_U_Ready = new RadioButton();
            btn_URRun = new Button();
            groupBox4 = new GroupBox();
            label4 = new Label();
            cmb_Product2 = new ComboBox();
            tbx_PickCount2 = new TextBox();
            label5 = new Label();
            groupBox3 = new GroupBox();
            label3 = new Label();
            cmb_Product1 = new ComboBox();
            tbx_PickCount1 = new TextBox();
            label2 = new Label();
            Console = new ListBox();
            label6 = new Label();
            btn_OpenPort = new Button();
            btn_ClosePort = new Button();
            groupBox1.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btn_ClosePort);
            groupBox1.Controls.Add(btn_OpenPort);
            groupBox1.Controls.Add(groupBox5);
            groupBox1.Controls.Add(cmb_BinNumber);
            groupBox1.Controls.Add(btn_CloseBin);
            groupBox1.Controls.Add(btn_OpenBin);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(51, 29);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(675, 187);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "AutoStore";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(rdo_A_Complete);
            groupBox5.Controls.Add(rdo_A_Busy);
            groupBox5.Controls.Add(rdo_A_Ready);
            groupBox5.Location = new Point(316, 19);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(136, 128);
            groupBox5.TabIndex = 5;
            groupBox5.TabStop = false;
            groupBox5.Text = "Status";
            // 
            // rdo_A_Complete
            // 
            rdo_A_Complete.AutoSize = true;
            rdo_A_Complete.Enabled = false;
            rdo_A_Complete.Location = new Point(16, 82);
            rdo_A_Complete.Name = "rdo_A_Complete";
            rdo_A_Complete.Size = new Size(90, 19);
            rdo_A_Complete.TabIndex = 2;
            rdo_A_Complete.TabStop = true;
            rdo_A_Complete.Text = "A_Complete";
            rdo_A_Complete.UseVisualStyleBackColor = true;
            // 
            // rdo_A_Busy
            // 
            rdo_A_Busy.AutoSize = true;
            rdo_A_Busy.Enabled = false;
            rdo_A_Busy.Location = new Point(16, 57);
            rdo_A_Busy.Name = "rdo_A_Busy";
            rdo_A_Busy.Size = new Size(63, 19);
            rdo_A_Busy.TabIndex = 1;
            rdo_A_Busy.TabStop = true;
            rdo_A_Busy.Text = "A_Busy";
            rdo_A_Busy.UseVisualStyleBackColor = true;
            // 
            // rdo_A_Ready
            // 
            rdo_A_Ready.AutoSize = true;
            rdo_A_Ready.Enabled = false;
            rdo_A_Ready.Location = new Point(16, 32);
            rdo_A_Ready.Name = "rdo_A_Ready";
            rdo_A_Ready.Size = new Size(70, 19);
            rdo_A_Ready.TabIndex = 0;
            rdo_A_Ready.TabStop = true;
            rdo_A_Ready.Text = "A_Ready";
            rdo_A_Ready.UseVisualStyleBackColor = true;
            // 
            // cmb_BinNumber
            // 
            cmb_BinNumber.FormattingEnabled = true;
            cmb_BinNumber.Items.AddRange(new object[] { "100002 : 3분 카레 (직사각형)", "100003 : 진라면 컵 (원통)", "100005 : 펩시 캔 (원통)", "100009 : Mixed 1", "100010 : Mixed 2" });
            cmb_BinNumber.Location = new Point(25, 51);
            cmb_BinNumber.Name = "cmb_BinNumber";
            cmb_BinNumber.Size = new Size(172, 23);
            cmb_BinNumber.TabIndex = 4;
            // 
            // btn_CloseBin
            // 
            btn_CloseBin.Location = new Point(215, 91);
            btn_CloseBin.Name = "btn_CloseBin";
            btn_CloseBin.Size = new Size(79, 48);
            btn_CloseBin.TabIndex = 3;
            btn_CloseBin.Text = "Close Bin";
            btn_CloseBin.UseVisualStyleBackColor = true;
            // 
            // btn_OpenBin
            // 
            btn_OpenBin.Location = new Point(215, 37);
            btn_OpenBin.Name = "btn_OpenBin";
            btn_OpenBin.Size = new Size(79, 48);
            btn_OpenBin.TabIndex = 2;
            btn_OpenBin.Text = "Open Bin";
            btn_OpenBin.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 33);
            label1.Name = "label1";
            label1.Size = new Size(72, 15);
            label1.TabIndex = 0;
            label1.Text = "Bin Number";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox6);
            groupBox2.Controls.Add(btn_URRun);
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Location = new Point(51, 279);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(675, 207);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "UR-Pickit";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(rdo_U_Complete);
            groupBox6.Controls.Add(rdo_U_Busy);
            groupBox6.Controls.Add(rdo_U_Ready);
            groupBox6.Location = new Point(353, 26);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(136, 128);
            groupBox6.TabIndex = 10;
            groupBox6.TabStop = false;
            groupBox6.Text = "Status";
            // 
            // rdo_U_Complete
            // 
            rdo_U_Complete.AutoSize = true;
            rdo_U_Complete.Enabled = false;
            rdo_U_Complete.Location = new Point(16, 82);
            rdo_U_Complete.Name = "rdo_U_Complete";
            rdo_U_Complete.Size = new Size(90, 19);
            rdo_U_Complete.TabIndex = 2;
            rdo_U_Complete.TabStop = true;
            rdo_U_Complete.Text = "U_Complete";
            rdo_U_Complete.UseVisualStyleBackColor = true;
            // 
            // rdo_U_Busy
            // 
            rdo_U_Busy.AutoSize = true;
            rdo_U_Busy.Enabled = false;
            rdo_U_Busy.Location = new Point(16, 57);
            rdo_U_Busy.Name = "rdo_U_Busy";
            rdo_U_Busy.Size = new Size(63, 19);
            rdo_U_Busy.TabIndex = 1;
            rdo_U_Busy.TabStop = true;
            rdo_U_Busy.Text = "U_Busy";
            rdo_U_Busy.UseVisualStyleBackColor = true;
            // 
            // rdo_U_Ready
            // 
            rdo_U_Ready.AutoSize = true;
            rdo_U_Ready.Enabled = false;
            rdo_U_Ready.Location = new Point(16, 32);
            rdo_U_Ready.Name = "rdo_U_Ready";
            rdo_U_Ready.Size = new Size(70, 19);
            rdo_U_Ready.TabIndex = 0;
            rdo_U_Ready.TabStop = true;
            rdo_U_Ready.Text = "U_Ready";
            rdo_U_Ready.UseVisualStyleBackColor = true;
            // 
            // btn_URRun
            // 
            btn_URRun.Location = new Point(551, 26);
            btn_URRun.Name = "btn_URRun";
            btn_URRun.Size = new Size(94, 62);
            btn_URRun.TabIndex = 9;
            btn_URRun.Text = "UR Run";
            btn_URRun.UseVisualStyleBackColor = true;
            btn_URRun.Click += btn_URRun_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label4);
            groupBox4.Controls.Add(cmb_Product2);
            groupBox4.Controls.Add(tbx_PickCount2);
            groupBox4.Controls.Add(label5);
            groupBox4.Location = new Point(179, 22);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(140, 134);
            groupBox4.TabIndex = 8;
            groupBox4.TabStop = false;
            groupBox4.Text = "Target 2";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 23);
            label4.Name = "label4";
            label4.Size = new Size(77, 15);
            label4.TabIndex = 7;
            label4.Text = "Product 종류";
            // 
            // cmb_Product2
            // 
            cmb_Product2.FormattingEnabled = true;
            cmb_Product2.Items.AddRange(new object[] { "0. 없음", "1. 직사각형 (Box)", "2. 원통 (Can)", "3. Mixed 1", "4. Mixed 2" });
            cmb_Product2.Location = new Point(9, 43);
            cmb_Product2.Name = "cmb_Product2";
            cmb_Product2.Size = new Size(118, 23);
            cmb_Product2.TabIndex = 6;
            // 
            // tbx_PickCount2
            // 
            tbx_PickCount2.Location = new Point(9, 87);
            tbx_PickCount2.Name = "tbx_PickCount2";
            tbx_PickCount2.Size = new Size(118, 23);
            tbx_PickCount2.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(9, 69);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 4;
            label5.Text = "Pick 개수";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(cmb_Product1);
            groupBox3.Controls.Add(tbx_PickCount1);
            groupBox3.Controls.Add(label2);
            groupBox3.Location = new Point(16, 22);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(140, 134);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "Target 1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 23);
            label3.Name = "label3";
            label3.Size = new Size(77, 15);
            label3.TabIndex = 7;
            label3.Text = "Product 종류";
            // 
            // cmb_Product1
            // 
            cmb_Product1.FormattingEnabled = true;
            cmb_Product1.Items.AddRange(new object[] { "0. 없음", "1. 직사각형 (Box)", "2. 원통 (Can)", "3. Mixed 1", "4. Mixed 2" });
            cmb_Product1.Location = new Point(9, 43);
            cmb_Product1.Name = "cmb_Product1";
            cmb_Product1.Size = new Size(118, 23);
            cmb_Product1.TabIndex = 6;
            // 
            // tbx_PickCount1
            // 
            tbx_PickCount1.Location = new Point(9, 87);
            tbx_PickCount1.Name = "tbx_PickCount1";
            tbx_PickCount1.Size = new Size(118, 23);
            tbx_PickCount1.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 69);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 4;
            label2.Text = "Pick 개수";
            // 
            // Console
            // 
            Console.FormattingEnabled = true;
            Console.ItemHeight = 15;
            Console.Location = new Point(755, 47);
            Console.Name = "Console";
            Console.Size = new Size(211, 439);
            Console.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(755, 29);
            label6.Name = "label6";
            label6.Size = new Size(27, 15);
            label6.TabIndex = 3;
            label6.Text = "Log";
            // 
            // btn_OpenPort
            // 
            btn_OpenPort.Location = new Point(506, 37);
            btn_OpenPort.Name = "btn_OpenPort";
            btn_OpenPort.Size = new Size(79, 48);
            btn_OpenPort.TabIndex = 6;
            btn_OpenPort.Text = "Open Port";
            btn_OpenPort.UseVisualStyleBackColor = true;
            // 
            // btn_ClosePort
            // 
            btn_ClosePort.Location = new Point(506, 91);
            btn_ClosePort.Name = "btn_ClosePort";
            btn_ClosePort.Size = new Size(79, 48);
            btn_ClosePort.TabIndex = 7;
            btn_ClosePort.Text = "Close Port";
            btn_ClosePort.UseVisualStyleBackColor = true;
            // 
            // RobotInterfaceGUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(995, 543);
            Controls.Add(label6);
            Controls.Add(Console);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "RobotInterfaceGUI";
            Text = "RobotInterfaceGUI";
            Load += RobotInterfaceGUI_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private GroupBox groupBox1;
        private Button btn_CloseBin;
        private Button btn_OpenBin;
        private Label label1;
        private GroupBox groupBox2;
        private ComboBox cmb_BinNumber;
        private TextBox tbx_PickCount1;
        private Label label2;
        private GroupBox groupBox3;
        private Label label3;
        private ComboBox cmb_Product1;
        private GroupBox groupBox4;
        private Label label4;
        private ComboBox cmb_Product2;
        private TextBox tbx_PickCount2;
        private Label label5;
        private Button btn_URRun;
        private ListBox Console;
        private Label label6;
        private GroupBox groupBox5;
        private RadioButton rdo_A_Complete;
        private RadioButton rdo_A_Busy;
        private RadioButton rdo_A_Ready;
        private GroupBox groupBox6;
        private RadioButton rdo_U_Complete;
        private RadioButton rdo_U_Busy;
        private RadioButton rdo_U_Ready;
        private Button btn_ClosePort;
        private Button btn_OpenPort;
    }
}