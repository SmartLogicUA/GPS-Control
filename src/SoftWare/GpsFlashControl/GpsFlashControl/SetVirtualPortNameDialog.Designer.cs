namespace GpsFlashControl
{
    partial class SetVirtualPortNameDialog
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
            this.virtualPortNameBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // virtualPortNameBox
            // 
            this.virtualPortNameBox.FormattingEnabled = true;
            this.virtualPortNameBox.Location = new System.Drawing.Point(23, 28);
            this.virtualPortNameBox.Name = "virtualPortNameBox";
            this.virtualPortNameBox.Size = new System.Drawing.Size(121, 21);
            this.virtualPortNameBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.virtualPortNameBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 71);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Доступные порты";
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(12, 100);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(77, 26);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(103, 100);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(77, 26);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "Отмена";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // SetVirtualPortNameDialog
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(194, 141);
            this.ControlBox = false;
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SetVirtualPortNameDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Выберите новый порт";
            this.Load += new System.EventHandler(this.SetVirtualPortNameDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox virtualPortNameBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}