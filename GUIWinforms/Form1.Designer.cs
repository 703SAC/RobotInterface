namespace GUIWinforms
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_PickCount1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_PickCount2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.openBinId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_OpenBin = new System.Windows.Forms.Button();
            this.btn_CloseBin = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_URRun = new System.Windows.Forms.Button();
            this.cmb_Product1 = new System.Windows.Forms.ComboBox();
            this.cmb_Product2 = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_PickCount1
            // 
            this.txt_PickCount1.Location = new System.Drawing.Point(20, 92);
            this.txt_PickCount1.Name = "txt_PickCount1";
            this.txt_PickCount1.Size = new System.Drawing.Size(102, 21);
            this.txt_PickCount1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Product 종류";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Pick 개수";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmb_Product1);
            this.groupBox1.Controls.Add(this.txt_PickCount1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(19, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 189);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Target 1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmb_Product2);
            this.groupBox2.Controls.Add(this.txt_PickCount2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(264, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(195, 189);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Target 2";
            // 
            // txt_PickCount2
            // 
            this.txt_PickCount2.Location = new System.Drawing.Point(21, 92);
            this.txt_PickCount2.Name = "txt_PickCount2";
            this.txt_PickCount2.Size = new System.Drawing.Size(102, 21);
            this.txt_PickCount2.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "Product 종류";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "Pick 개수";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_CloseBin);
            this.groupBox3.Controls.Add(this.btn_OpenBin);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.openBinId);
            this.groupBox3.Location = new System.Drawing.Point(49, 22);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(525, 187);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "AutoStore";
            // 
            // openBinId
            // 
            this.openBinId.Location = new System.Drawing.Point(19, 43);
            this.openBinId.Name = "openBinId";
            this.openBinId.Size = new System.Drawing.Size(118, 21);
            this.openBinId.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Open Bin";
            // 
            // btn_OpenBin
            // 
            this.btn_OpenBin.Location = new System.Drawing.Point(155, 28);
            this.btn_OpenBin.Name = "btn_OpenBin";
            this.btn_OpenBin.Size = new System.Drawing.Size(90, 53);
            this.btn_OpenBin.TabIndex = 2;
            this.btn_OpenBin.Text = "Open Bin";
            this.btn_OpenBin.UseVisualStyleBackColor = true;
            // 
            // btn_CloseBin
            // 
            this.btn_CloseBin.Location = new System.Drawing.Point(155, 98);
            this.btn_CloseBin.Name = "btn_CloseBin";
            this.btn_CloseBin.Size = new System.Drawing.Size(90, 53);
            this.btn_CloseBin.TabIndex = 3;
            this.btn_CloseBin.Text = "Close Bin";
            this.btn_CloseBin.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_URRun);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Location = new System.Drawing.Point(49, 216);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(590, 222);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "UR-Pickit";
            // 
            // btn_URRun
            // 
            this.btn_URRun.Location = new System.Drawing.Point(490, 40);
            this.btn_URRun.Name = "btn_URRun";
            this.btn_URRun.Size = new System.Drawing.Size(85, 57);
            this.btn_URRun.TabIndex = 14;
            this.btn_URRun.Text = "UR Run";
            this.btn_URRun.UseVisualStyleBackColor = true;
            this.btn_URRun.Click += new System.EventHandler(this.btn_URRun_Click);
            // 
            // cmb_Product1
            // 
            this.cmb_Product1.FormattingEnabled = true;
            this.cmb_Product1.Items.AddRange(new object[] {
            "0. 없음",
            "1. 직사각형 (Box)",
            "2. 원통 (Can)",
            "3. Mixed 1",
            "4. Mixed 2"});
            this.cmb_Product1.Location = new System.Drawing.Point(20, 46);
            this.cmb_Product1.Name = "cmb_Product1";
            this.cmb_Product1.Size = new System.Drawing.Size(102, 20);
            this.cmb_Product1.TabIndex = 6;
            // 
            // cmb_Product2
            // 
            this.cmb_Product2.FormattingEnabled = true;
            this.cmb_Product2.Items.AddRange(new object[] {
            "0. 없음",
            "1. 직사각형 (Box)",
            "2. 원통 (Can)",
            "3. Mixed 1",
            "4. Mixed 2"});
            this.cmb_Product2.Location = new System.Drawing.Point(21, 46);
            this.cmb_Product2.Name = "cmb_Product2";
            this.cmb_Product2.Size = new System.Drawing.Size(102, 20);
            this.cmb_Product2.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txt_PickCount1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_PickCount2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_CloseBin;
        private System.Windows.Forms.Button btn_OpenBin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox openBinId;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_URRun;
        private System.Windows.Forms.ComboBox cmb_Product1;
        private System.Windows.Forms.ComboBox cmb_Product2;
    }
}

