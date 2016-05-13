namespace SwSpecification
{
    partial class addincheckbox
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
            this.DGDoc = new System.Windows.Forms.DataGridView();
            this.DocChb = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Col2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.ChkElectro2 = new System.Windows.Forms.CheckBox();
            this.ChkElectro1 = new System.Windows.Forms.CheckBox();
            this.ChkElectro = new System.Windows.Forms.CheckBox();
            this.ChkComplect = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGDoc)).BeginInit();
            this.SuspendLayout();
            // 
            // DGDoc
            // 
            this.DGDoc.AllowUserToAddRows = false;
            this.DGDoc.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(224)))));
            this.DGDoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDoc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DocChb,
            this.Col2});
            this.DGDoc.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(224)))));
            this.DGDoc.Location = new System.Drawing.Point(7, 7);
            this.DGDoc.Name = "DGDoc";
            this.DGDoc.RowHeadersVisible = false;
            this.DGDoc.Size = new System.Drawing.Size(309, 274);
            this.DGDoc.TabIndex = 29;
            // 
            // DocChb
            // 
            this.DocChb.HeaderText = "";
            this.DocChb.Name = "DocChb";
            this.DocChb.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DocChb.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DocChb.Width = 25;
            // 
            // Col2
            // 
            this.Col2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Col2.HeaderText = "Документ";
            this.Col2.Name = "Col2";
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(160, 364);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(75, 23);
            this.BtnOk.TabIndex = 28;
            this.BtnOk.Text = "Ok";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(241, 363);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(75, 23);
            this.BtnClose.TabIndex = 27;
            this.BtnClose.Text = "Отмена";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // ChkElectro2
            // 
            this.ChkElectro2.AutoSize = true;
            this.ChkElectro2.Location = new System.Drawing.Point(69, 340);
            this.ChkElectro2.Name = "ChkElectro2";
            this.ChkElectro2.Size = new System.Drawing.Size(40, 17);
            this.ChkElectro2.TabIndex = 26;
            this.ChkElectro2.Text = "ТБ";
            this.ChkElectro2.UseVisualStyleBackColor = true;
            this.ChkElectro2.CheckedChanged += new System.EventHandler(this.ChkElectro2_CheckedChanged_1);
            // 
            // ChkElectro1
            // 
            this.ChkElectro1.AutoSize = true;
            this.ChkElectro1.Location = new System.Drawing.Point(21, 340);
            this.ChkElectro1.Name = "ChkElectro1";
            this.ChkElectro1.Size = new System.Drawing.Size(42, 17);
            this.ChkElectro1.TabIndex = 25;
            this.ChkElectro1.Text = "МЭ";
            this.ChkElectro1.UseVisualStyleBackColor = true;
            this.ChkElectro1.CheckedChanged += new System.EventHandler(this.ChkElectro1_CheckedChanged_1);
            // 
            // ChkElectro
            // 
            this.ChkElectro.AutoSize = true;
            this.ChkElectro.Location = new System.Drawing.Point(7, 317);
            this.ChkElectro.Name = "ChkElectro";
            this.ChkElectro.Size = new System.Drawing.Size(318, 17);
            this.ChkElectro.TabIndex = 24;
            this.ChkElectro.Text = "Добавить раздел \"Устанавливают при электромонтаже\"";
            this.ChkElectro.UseVisualStyleBackColor = true;
            this.ChkElectro.CheckedChanged += new System.EventHandler(this.ChkElectro_CheckedChanged);
            // 
            // ChkComplect
            // 
            this.ChkComplect.AutoSize = true;
            this.ChkComplect.Location = new System.Drawing.Point(7, 293);
            this.ChkComplect.Name = "ChkComplect";
            this.ChkComplect.Size = new System.Drawing.Size(186, 17);
            this.ChkComplect.TabIndex = 23;
            this.ChkComplect.Text = "Добавить раздел \"Комплекты\"";
            this.ChkComplect.UseVisualStyleBackColor = true;
            // 
            // addincheckbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(325, 392);
            this.Controls.Add(this.DGDoc);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.ChkElectro2);
            this.Controls.Add(this.ChkElectro1);
            this.Controls.Add(this.ChkElectro);
            this.Controls.Add(this.ChkComplect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "addincheckbox";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавления";
            this.Load += new System.EventHandler(this.addincheckbox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGDoc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView DGDoc;
        internal System.Windows.Forms.DataGridViewCheckBoxColumn DocChb;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Col2;
        internal System.Windows.Forms.Button BtnOk;
        internal System.Windows.Forms.Button BtnClose;
        internal System.Windows.Forms.CheckBox ChkElectro2;
        internal System.Windows.Forms.CheckBox ChkElectro1;
        internal System.Windows.Forms.CheckBox ChkElectro;
        internal System.Windows.Forms.CheckBox ChkComplect;
    }
}