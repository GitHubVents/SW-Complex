namespace SwSpecification
{
    partial class FrmUsersSettings
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
            this.Label1 = new System.Windows.Forms.Label();
            this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.BtnUserSetOtmena = new System.Windows.Forms.Button();
            this.BtnUserSetOK = new System.Windows.Forms.Button();
            this.ComboBoxIP = new System.Windows.Forms.ComboBox();
            this.ChkIns = new System.Windows.Forms.CheckBox();
            this.ChkStandard = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(14, 69);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(17, 13);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "IP";
            // 
            // BtnUserSetOtmena
            // 
            this.BtnUserSetOtmena.Location = new System.Drawing.Point(379, 127);
            this.BtnUserSetOtmena.Name = "BtnUserSetOtmena";
            this.BtnUserSetOtmena.Size = new System.Drawing.Size(75, 23);
            this.BtnUserSetOtmena.TabIndex = 6;
            this.BtnUserSetOtmena.Text = "Отмена";
            this.BtnUserSetOtmena.UseVisualStyleBackColor = true;
            this.BtnUserSetOtmena.Click += new System.EventHandler(this.BtnUserSetOtmena_Click);
            // 
            // BtnUserSetOK
            // 
            this.BtnUserSetOK.Location = new System.Drawing.Point(297, 127);
            this.BtnUserSetOK.Name = "BtnUserSetOK";
            this.BtnUserSetOK.Size = new System.Drawing.Size(75, 23);
            this.BtnUserSetOK.TabIndex = 7;
            this.BtnUserSetOK.Text = "OK";
            this.BtnUserSetOK.UseVisualStyleBackColor = true;
            this.BtnUserSetOK.Click += new System.EventHandler(this.BtnUserSetOK_Click);
            // 
            // ComboBoxIP
            // 
            this.ComboBoxIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxIP.FormattingEnabled = true;
            this.ComboBoxIP.Location = new System.Drawing.Point(14, 88);
            this.ComboBoxIP.Name = "ComboBoxIP";
            this.ComboBoxIP.Size = new System.Drawing.Size(440, 21);
            this.ComboBoxIP.TabIndex = 8;
            // 
            // ChkIns
            // 
            this.ChkIns.AutoSize = true;
            this.ChkIns.Location = new System.Drawing.Point(15, 35);
            this.ChkIns.Name = "ChkIns";
            this.ChkIns.Size = new System.Drawing.Size(254, 17);
            this.ChkIns.TabIndex = 10;
            this.ChkIns.Text = "Всегда заново создавать основную надпись";
            this.ChkIns.UseVisualStyleBackColor = true;
            // 
            // ChkStandard
            // 
            this.ChkStandard.AutoSize = true;
            this.ChkStandard.Location = new System.Drawing.Point(15, 12);
            this.ChkStandard.Name = "ChkStandard";
            this.ChkStandard.Size = new System.Drawing.Size(199, 17);
            this.ChkStandard.TabIndex = 11;
            this.ChkStandard.Text = "Исправлять оформление чертежа";
            this.ChkStandard.UseVisualStyleBackColor = true;
            // 
            // FrmUsersSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(469, 165);
            this.Controls.Add(this.ChkStandard);
            this.Controls.Add(this.ChkIns);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.BtnUserSetOtmena);
            this.Controls.Add(this.BtnUserSetOK);
            this.Controls.Add(this.ComboBoxIP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmUsersSettings";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Параметры";
            this.Load += new System.EventHandler(this.FrmUsersSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1;
        internal System.Windows.Forms.Button BtnUserSetOtmena;
        internal System.Windows.Forms.Button BtnUserSetOK;
        internal System.Windows.Forms.ComboBox ComboBoxIP;
        public System.Windows.Forms.CheckBox ChkIns;
        public System.Windows.Forms.CheckBox ChkStandard;
    }
}