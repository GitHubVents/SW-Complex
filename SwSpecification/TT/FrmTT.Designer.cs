namespace SwSpecification.TT
{
    partial class FrmTT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTT));
            this.label1 = new System.Windows.Forms.Label();
            this.CmdPM = new System.Windows.Forms.Button();
            this.CmdDeg = new System.Windows.Forms.Button();
            this.CmdDiam = new System.Windows.Forms.Button();
            this.CmdUp = new System.Windows.Forms.Button();
            this.CmdDown = new System.Windows.Forms.Button();
            this.CmdDel = new System.Windows.Forms.Button();
            this.CmdAdd = new System.Windows.Forms.Button();
            this.CboTTType = new System.Windows.Forms.ComboBox();
            this.CmdOk = new System.Windows.Forms.Button();
            this.CmdCancel = new System.Windows.Forms.Button();
            this.CmdSet = new System.Windows.Forms.Button();
            this.ChkList = new System.Windows.Forms.CheckBox();
            this.ChkAutoN = new System.Windows.Forms.CheckBox();
            this.ChkAlign = new System.Windows.Forms.CheckBox();
            this.flp = new System.Windows.Forms.FlowLayoutPanel();
            this.CboChangeLang = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(9, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(419, 2);
            this.label1.TabIndex = 24;
            this.label1.Text = "label1";
            // 
            // CmdPM
            // 
            this.CmdPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmdPM.Location = new System.Drawing.Point(401, 88);
            this.CmdPM.Name = "CmdPM";
            this.CmdPM.Size = new System.Drawing.Size(30, 30);
            this.CmdPM.TabIndex = 22;
            this.CmdPM.Text = "±";
            this.CmdPM.UseVisualStyleBackColor = true;
            this.CmdPM.Click += new System.EventHandler(this.CmdPM_Click);
            // 
            // CmdDeg
            // 
            this.CmdDeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmdDeg.Location = new System.Drawing.Point(365, 88);
            this.CmdDeg.Name = "CmdDeg";
            this.CmdDeg.Size = new System.Drawing.Size(30, 30);
            this.CmdDeg.TabIndex = 21;
            this.CmdDeg.Text = "°";
            this.CmdDeg.UseVisualStyleBackColor = true;
            this.CmdDeg.Click += new System.EventHandler(this.CmdDeg_Click);
            // 
            // CmdDiam
            // 
            this.CmdDiam.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmdDiam.Location = new System.Drawing.Point(329, 88);
            this.CmdDiam.Name = "CmdDiam";
            this.CmdDiam.Size = new System.Drawing.Size(30, 30);
            this.CmdDiam.TabIndex = 20;
            this.CmdDiam.Text = "Ø";
            this.CmdDiam.UseVisualStyleBackColor = true;
            this.CmdDiam.Click += new System.EventHandler(this.CmdDiam_Click);
            // 
            // CmdUp
            // 
            this.CmdUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmdUp.Location = new System.Drawing.Point(293, 88);
            this.CmdUp.Name = "CmdUp";
            this.CmdUp.Size = new System.Drawing.Size(30, 30);
            this.CmdUp.TabIndex = 19;
            this.CmdUp.Text = "↑";
            this.CmdUp.UseVisualStyleBackColor = true;
            this.CmdUp.Click += new System.EventHandler(this.CmdUp_Click);
            // 
            // CmdDown
            // 
            this.CmdDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmdDown.Location = new System.Drawing.Point(253, 88);
            this.CmdDown.Name = "CmdDown";
            this.CmdDown.Size = new System.Drawing.Size(30, 30);
            this.CmdDown.TabIndex = 23;
            this.CmdDown.Text = "↓";
            this.CmdDown.UseVisualStyleBackColor = true;
            this.CmdDown.Click += new System.EventHandler(this.CmdDown_Click);
            // 
            // CmdDel
            // 
            this.CmdDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmdDel.Location = new System.Drawing.Point(217, 87);
            this.CmdDel.Name = "CmdDel";
            this.CmdDel.Size = new System.Drawing.Size(30, 30);
            this.CmdDel.TabIndex = 18;
            this.CmdDel.Text = "-";
            this.CmdDel.UseVisualStyleBackColor = true;
            this.CmdDel.Click += new System.EventHandler(this.CmdDel_Click);
            // 
            // CmdAdd
            // 
            this.CmdAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmdAdd.Location = new System.Drawing.Point(181, 88);
            this.CmdAdd.Name = "CmdAdd";
            this.CmdAdd.Size = new System.Drawing.Size(30, 30);
            this.CmdAdd.TabIndex = 17;
            this.CmdAdd.Text = "+";
            this.CmdAdd.UseVisualStyleBackColor = true;
            this.CmdAdd.Click += new System.EventHandler(this.CmdAdd_Click);
            // 
            // CboTTType
            // 
            this.CboTTType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTTType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CboTTType.FormattingEnabled = true;
            this.CboTTType.Location = new System.Drawing.Point(12, 89);
            this.CboTTType.Name = "CboTTType";
            this.CboTTType.Size = new System.Drawing.Size(162, 28);
            this.CboTTType.TabIndex = 16;
            this.CboTTType.DropDownClosed += new System.EventHandler(this.CboTTType_DropDownClosed);
            // 
            // CmdOk
            // 
            this.CmdOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmdOk.Location = new System.Drawing.Point(12, 40);
            this.CmdOk.Name = "CmdOk";
            this.CmdOk.Size = new System.Drawing.Size(163, 42);
            this.CmdOk.TabIndex = 15;
            this.CmdOk.Text = "Внести изменения";
            this.CmdOk.UseVisualStyleBackColor = true;
            this.CmdOk.Click += new System.EventHandler(this.CmdOk_Click);
            // 
            // CmdCancel
            // 
            this.CmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmdCancel.Location = new System.Drawing.Point(317, 40);
            this.CmdCancel.Name = "CmdCancel";
            this.CmdCancel.Size = new System.Drawing.Size(114, 42);
            this.CmdCancel.TabIndex = 14;
            this.CmdCancel.Text = "Отмена";
            this.CmdCancel.UseVisualStyleBackColor = true;
            this.CmdCancel.Click += new System.EventHandler(this.CmdCancel_Click);
            // 
            // CmdSet
            // 
            this.CmdSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmdSet.Location = new System.Drawing.Point(181, 40);
            this.CmdSet.Name = "CmdSet";
            this.CmdSet.Size = new System.Drawing.Size(130, 42);
            this.CmdSet.TabIndex = 13;
            this.CmdSet.Text = "Назначить";
            this.CmdSet.UseVisualStyleBackColor = true;
            this.CmdSet.Click += new System.EventHandler(this.CmdSet_Click);
            // 
            // ChkList
            // 
            this.ChkList.AutoSize = true;
            this.ChkList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChkList.Location = new System.Drawing.Point(122, 12);
            this.ChkList.Name = "ChkList";
            this.ChkList.Size = new System.Drawing.Size(79, 22);
            this.ChkList.TabIndex = 11;
            this.ChkList.Text = "Список";
            this.ChkList.UseVisualStyleBackColor = true;
            this.ChkList.Click += new System.EventHandler(this.ChkList_Click);
            // 
            // ChkAutoN
            // 
            this.ChkAutoN.AutoSize = true;
            this.ChkAutoN.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChkAutoN.Location = new System.Drawing.Point(207, 12);
            this.ChkAutoN.Name = "ChkAutoN";
            this.ChkAutoN.Size = new System.Drawing.Size(134, 22);
            this.ChkAutoN.TabIndex = 10;
            this.ChkAutoN.Text = "Автонумерация";
            this.ChkAutoN.UseVisualStyleBackColor = true;
            this.ChkAutoN.Click += new System.EventHandler(this.ChkAutoN_Click);
            // 
            // ChkAlign
            // 
            this.ChkAlign.AutoSize = true;
            this.ChkAlign.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChkAlign.Location = new System.Drawing.Point(12, 12);
            this.ChkAlign.Name = "ChkAlign";
            this.ChkAlign.Size = new System.Drawing.Size(104, 22);
            this.ChkAlign.TabIndex = 9;
            this.ChkAlign.Text = "Выровнять";
            this.ChkAlign.UseVisualStyleBackColor = true;
            // 
            // flp
            // 
            this.flp.Location = new System.Drawing.Point(8, 136);
            this.flp.Name = "flp";
            this.flp.Size = new System.Drawing.Size(423, 29);
            this.flp.TabIndex = 25;
            // 
            // CboChangeLang
            // 
            this.CboChangeLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboChangeLang.FormattingEnabled = true;
            this.CboChangeLang.Location = new System.Drawing.Point(348, 12);
            this.CboChangeLang.Name = "CboChangeLang";
            this.CboChangeLang.Size = new System.Drawing.Size(83, 21);
            this.CboChangeLang.TabIndex = 26;
            this.CboChangeLang.SelectedValueChanged += new System.EventHandler(this.CboChangeLang_SelectedValueChanged);
            // 
            // FrmTT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 175);
            this.Controls.Add(this.CboChangeLang);
            this.Controls.Add(this.flp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CmdPM);
            this.Controls.Add(this.CmdDeg);
            this.Controls.Add(this.CmdDiam);
            this.Controls.Add(this.CmdUp);
            this.Controls.Add(this.CmdDown);
            this.Controls.Add(this.CmdDel);
            this.Controls.Add(this.CmdAdd);
            this.Controls.Add(this.CboTTType);
            this.Controls.Add(this.CmdOk);
            this.Controls.Add(this.CmdCancel);
            this.Controls.Add(this.CmdSet);
            this.Controls.Add(this.ChkList);
            this.Controls.Add(this.ChkAutoN);
            this.Controls.Add(this.ChkAlign);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Технические требования";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmTT_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CmdPM;
        private System.Windows.Forms.Button CmdDeg;
        private System.Windows.Forms.Button CmdDiam;
        private System.Windows.Forms.Button CmdUp;
        private System.Windows.Forms.Button CmdDown;
        private System.Windows.Forms.Button CmdDel;
        private System.Windows.Forms.Button CmdAdd;
        private System.Windows.Forms.ComboBox CboTTType;
        private System.Windows.Forms.Button CmdOk;
        private System.Windows.Forms.Button CmdCancel;
        private System.Windows.Forms.Button CmdSet;
        private System.Windows.Forms.CheckBox ChkList;
        private System.Windows.Forms.CheckBox ChkAutoN;
        private System.Windows.Forms.CheckBox ChkAlign;
        private System.Windows.Forms.FlowLayoutPanel flp;
        private System.Windows.Forms.ComboBox CboChangeLang;
    }
}