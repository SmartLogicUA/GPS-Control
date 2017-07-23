namespace GpsFlashControl
{
    partial class SetVirtualPortDialog
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.selectNewPortBtn = new System.Windows.Forms.Button();
            this.offBtn = new System.Windows.Forms.Button();
            this.onBtn = new System.Windows.Forms.Button();
            this.portNameLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.selectNewPortBtn);
            this.groupBox2.Controls.Add(this.offBtn);
            this.groupBox2.Controls.Add(this.onBtn);
            this.groupBox2.Controls.Add(this.portNameLbl);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(188, 132);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Текущий порт";
            // 
            // selectNewPortBtn
            // 
            this.selectNewPortBtn.Location = new System.Drawing.Point(12, 84);
            this.selectNewPortBtn.Name = "selectNewPortBtn";
            this.selectNewPortBtn.Size = new System.Drawing.Size(159, 25);
            this.selectNewPortBtn.TabIndex = 5;
            this.selectNewPortBtn.Text = "Выбрать новый порт...";
            this.selectNewPortBtn.UseVisualStyleBackColor = true;
            this.selectNewPortBtn.Click += new System.EventHandler(this.selectNewPortBtn_Click);
            // 
            // offBtn
            // 
            this.offBtn.Enabled = false;
            this.offBtn.Location = new System.Drawing.Point(95, 54);
            this.offBtn.Name = "offBtn";
            this.offBtn.Size = new System.Drawing.Size(76, 24);
            this.offBtn.TabIndex = 3;
            this.offBtn.Text = "Отключить";
            this.offBtn.UseVisualStyleBackColor = true;
            this.offBtn.Click += new System.EventHandler(this.offBtn_Click);
            // 
            // onBtn
            // 
            this.onBtn.Location = new System.Drawing.Point(12, 54);
            this.onBtn.Name = "onBtn";
            this.onBtn.Size = new System.Drawing.Size(76, 24);
            this.onBtn.TabIndex = 2;
            this.onBtn.Text = "Включить";
            this.onBtn.UseVisualStyleBackColor = true;
            this.onBtn.Click += new System.EventHandler(this.onBtn_Click);
            // 
            // portNameLbl
            // 
            this.portNameLbl.AutoSize = true;
            this.portNameLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.portNameLbl.Location = new System.Drawing.Point(101, 26);
            this.portNameLbl.Name = "portNameLbl";
            this.portNameLbl.Size = new System.Drawing.Size(2, 15);
            this.portNameLbl.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номер порта:";
            // 
            // closeBtn
            // 
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Location = new System.Drawing.Point(114, 155);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(86, 29);
            this.closeBtn.TabIndex = 4;
            this.closeBtn.Text = "Закрыть";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // SetVirtualPortDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeBtn;
            this.ClientSize = new System.Drawing.Size(207, 190);
            this.ControlBox = false;
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SetVirtualPortDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Настройка виртуального порта";
            this.Load += new System.EventHandler(this.SetVirtualPortDialog_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetVirtualPortDialog_FormClosing);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button onBtn;
        private System.Windows.Forms.Label portNameLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button offBtn;
        private System.Windows.Forms.Button selectNewPortBtn;
    }
}