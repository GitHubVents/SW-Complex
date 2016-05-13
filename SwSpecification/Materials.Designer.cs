namespace SwSpecification
{
    partial class Materials
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle47 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle48 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CloseButton = new System.Windows.Forms.Button();
            this.MatTree = new System.Windows.Forms.TreeView();
            this.ButtonOK = new System.Windows.Forms.Button();
            this.BtnOkAndClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.MaterialName = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Plotnost = new System.Windows.Forms.TextBox();
            this.SwProp = new System.Windows.Forms.TextBox();
            this.KodERP = new System.Windows.Forms.TextBox();
            this.KodMateriala = new System.Windows.Forms.TextBox();
            this.Description = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.DataGridConfig = new System.Windows.Forms.DataGridView();
            this.ColChB = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Col2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CboClass = new System.Windows.Forms.ComboBox();
            this.CboType = new System.Windows.Forms.ComboBox();
            this.CboColor = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PropertyBox = new System.Windows.Forms.GroupBox();
            this.txtBoxSearch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridConfig)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.PropertyBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.Linen;
            this.CloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CloseButton.Location = new System.Drawing.Point(708, 551);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(97, 27);
            this.CloseButton.TabIndex = 28;
            this.CloseButton.Text = "Закрыть";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // MatTree
            // 
            this.MatTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(224)))));
            this.MatTree.Location = new System.Drawing.Point(12, 12);
            this.MatTree.Name = "MatTree";
            this.MatTree.Size = new System.Drawing.Size(328, 530);
            this.MatTree.TabIndex = 27;
            this.MatTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.MatTree_AfterSelect);
            // 
            // ButtonOK
            // 
            this.ButtonOK.BackColor = System.Drawing.Color.Linen;
            this.ButtonOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ButtonOK.Location = new System.Drawing.Point(455, 551);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.Size = new System.Drawing.Size(96, 27);
            this.ButtonOK.TabIndex = 29;
            this.ButtonOK.Text = "Применить";
            this.ButtonOK.UseVisualStyleBackColor = false;
            this.ButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // BtnOkAndClose
            // 
            this.BtnOkAndClose.BackColor = System.Drawing.Color.Linen;
            this.BtnOkAndClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnOkAndClose.Location = new System.Drawing.Point(557, 551);
            this.BtnOkAndClose.Name = "BtnOkAndClose";
            this.BtnOkAndClose.Size = new System.Drawing.Size(145, 27);
            this.BtnOkAndClose.TabIndex = 33;
            this.BtnOkAndClose.Text = "Применить и закрыть";
            this.BtnOkAndClose.UseVisualStyleBackColor = false;
            this.BtnOkAndClose.Click += new System.EventHandler(this.BtnOkAndClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(509, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 20);
            this.label2.TabIndex = 35;
            this.label2.Text = "Выберите материал";
            // 
            // MaterialName
            // 
            this.MaterialName.Location = new System.Drawing.Point(129, 19);
            this.MaterialName.Name = "MaterialName";
            this.MaterialName.ReadOnly = true;
            this.MaterialName.Size = new System.Drawing.Size(324, 20);
            this.MaterialName.TabIndex = 0;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label4.Location = new System.Drawing.Point(6, 22);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(60, 13);
            this.Label4.TabIndex = 1;
            this.Label4.Text = "Материал:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label5.Location = new System.Drawing.Point(6, 48);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(64, 13);
            this.Label5.TabIndex = 2;
            this.Label5.Text = "Плотность:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(6, 152);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(60, 13);
            this.Label6.TabIndex = 3;
            this.Label6.Text = "Описание:";
            // 
            // Plotnost
            // 
            this.Plotnost.Location = new System.Drawing.Point(129, 45);
            this.Plotnost.Name = "Plotnost";
            this.Plotnost.ReadOnly = true;
            this.Plotnost.Size = new System.Drawing.Size(324, 20);
            this.Plotnost.TabIndex = 6;
            // 
            // SwProp
            // 
            this.SwProp.Location = new System.Drawing.Point(129, 71);
            this.SwProp.Name = "SwProp";
            this.SwProp.ReadOnly = true;
            this.SwProp.Size = new System.Drawing.Size(324, 20);
            this.SwProp.TabIndex = 7;
            // 
            // KodERP
            // 
            this.KodERP.Location = new System.Drawing.Point(129, 97);
            this.KodERP.Name = "KodERP";
            this.KodERP.ReadOnly = true;
            this.KodERP.Size = new System.Drawing.Size(324, 20);
            this.KodERP.TabIndex = 8;
            // 
            // KodMateriala
            // 
            this.KodMateriala.Location = new System.Drawing.Point(129, 123);
            this.KodMateriala.Name = "KodMateriala";
            this.KodMateriala.ReadOnly = true;
            this.KodMateriala.Size = new System.Drawing.Size(324, 20);
            this.KodMateriala.TabIndex = 8;
            // 
            // Description
            // 
            this.Description.Location = new System.Drawing.Point(129, 149);
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Size = new System.Drawing.Size(324, 20);
            this.Description.TabIndex = 9;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label9.Location = new System.Drawing.Point(6, 74);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(115, 13);
            this.Label9.TabIndex = 12;
            this.Label9.Text = "Свойство SolidWorks:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label10.Location = new System.Drawing.Point(6, 100);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(54, 13);
            this.Label10.TabIndex = 13;
            this.Label10.Text = "Код ERP:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label11.Location = new System.Drawing.Point(6, 126);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(87, 13);
            this.Label11.TabIndex = 13;
            this.Label11.Text = "Код материала:";
            // 
            // DataGridConfig
            // 
            this.DataGridConfig.AllowUserToAddRows = false;
            this.DataGridConfig.AllowUserToDeleteRows = false;
            this.DataGridConfig.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DataGridConfig.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(224)))));
            this.DataGridConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColChB,
            this.Col2,
            this.ColMatName,
            this.ColumnColor,
            this.ColType,
            this.ColClass});
            this.DataGridConfig.Location = new System.Drawing.Point(9, 349);
            this.DataGridConfig.Name = "DataGridConfig";
            this.DataGridConfig.RowHeadersVisible = false;
            this.DataGridConfig.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridConfig.Size = new System.Drawing.Size(444, 175);
            this.DataGridConfig.TabIndex = 10;
            // 
            // ColChB
            // 
            this.ColChB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle43.NullValue = false;
            dataGridViewCellStyle43.SelectionBackColor = System.Drawing.Color.PaleTurquoise;
            this.ColChB.DefaultCellStyle = dataGridViewCellStyle43;
            this.ColChB.FillWeight = 58.76139F;
            this.ColChB.HeaderText = "";
            this.ColChB.Name = "ColChB";
            this.ColChB.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColChB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColChB.Width = 19;
            // 
            // Col2
            // 
            this.Col2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle44.SelectionBackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle44.SelectionForeColor = System.Drawing.Color.Black;
            this.Col2.DefaultCellStyle = dataGridViewCellStyle44;
            this.Col2.FillWeight = 69.2755F;
            this.Col2.HeaderText = "Испол.";
            this.Col2.Name = "Col2";
            this.Col2.ReadOnly = true;
            this.Col2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ColMatName
            // 
            this.ColMatName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle45.SelectionBackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle45.SelectionForeColor = System.Drawing.Color.Black;
            this.ColMatName.DefaultCellStyle = dataGridViewCellStyle45;
            this.ColMatName.FillWeight = 138.551F;
            this.ColMatName.HeaderText = "Имя материала";
            this.ColMatName.Name = "ColMatName";
            this.ColMatName.ReadOnly = true;
            // 
            // ColumnColor
            // 
            this.ColumnColor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle46.SelectionBackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle46.SelectionForeColor = System.Drawing.Color.Black;
            this.ColumnColor.DefaultCellStyle = dataGridViewCellStyle46;
            this.ColumnColor.FillWeight = 138.551F;
            this.ColumnColor.HeaderText = "Цвет";
            this.ColumnColor.Name = "ColumnColor";
            this.ColumnColor.ReadOnly = true;
            // 
            // ColType
            // 
            this.ColType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle47.SelectionBackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle47.SelectionForeColor = System.Drawing.Color.Black;
            this.ColType.DefaultCellStyle = dataGridViewCellStyle47;
            this.ColType.FillWeight = 35.7868F;
            this.ColType.HeaderText = "Тип";
            this.ColType.Name = "ColType";
            this.ColType.ReadOnly = true;
            this.ColType.Width = 51;
            // 
            // ColClass
            // 
            this.ColClass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle48.SelectionBackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle48.SelectionForeColor = System.Drawing.Color.Black;
            this.ColClass.DefaultCellStyle = dataGridViewCellStyle48;
            this.ColClass.FillWeight = 29.07432F;
            this.ColClass.HeaderText = "Класс";
            this.ColClass.Name = "ColClass";
            this.ColClass.ReadOnly = true;
            this.ColClass.Width = 63;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CboClass);
            this.groupBox2.Controls.Add(this.CboType);
            this.groupBox2.Controls.Add(this.CboColor);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(9, 225);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(444, 118);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Покрытие";
            // 
            // CboClass
            // 
            this.CboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboClass.FormattingEnabled = true;
            this.CboClass.Location = new System.Drawing.Point(120, 85);
            this.CboClass.Name = "CboClass";
            this.CboClass.Size = new System.Drawing.Size(139, 21);
            this.CboClass.TabIndex = 31;
            // 
            // CboType
            // 
            this.CboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboType.FormattingEnabled = true;
            this.CboType.Location = new System.Drawing.Point(120, 52);
            this.CboType.Name = "CboType";
            this.CboType.Size = new System.Drawing.Size(139, 21);
            this.CboType.TabIndex = 30;
            // 
            // CboColor
            // 
            this.CboColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboColor.FormattingEnabled = true;
            this.CboColor.Location = new System.Drawing.Point(120, 20);
            this.CboColor.Name = "CboColor";
            this.CboColor.Size = new System.Drawing.Size(139, 21);
            this.CboColor.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Класс:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Тип:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Цвет:";
            // 
            // PropertyBox
            // 
            this.PropertyBox.BackColor = System.Drawing.Color.Linen;
            this.PropertyBox.Controls.Add(this.groupBox2);
            this.PropertyBox.Controls.Add(this.DataGridConfig);
            this.PropertyBox.Controls.Add(this.Label11);
            this.PropertyBox.Controls.Add(this.Label10);
            this.PropertyBox.Controls.Add(this.Label9);
            this.PropertyBox.Controls.Add(this.Description);
            this.PropertyBox.Controls.Add(this.KodMateriala);
            this.PropertyBox.Controls.Add(this.KodERP);
            this.PropertyBox.Controls.Add(this.SwProp);
            this.PropertyBox.Controls.Add(this.Plotnost);
            this.PropertyBox.Controls.Add(this.Label6);
            this.PropertyBox.Controls.Add(this.Label5);
            this.PropertyBox.Controls.Add(this.Label4);
            this.PropertyBox.Controls.Add(this.MaterialName);
            this.PropertyBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PropertyBox.Location = new System.Drawing.Point(346, 12);
            this.PropertyBox.Name = "PropertyBox";
            this.PropertyBox.Size = new System.Drawing.Size(459, 530);
            this.PropertyBox.TabIndex = 34;
            this.PropertyBox.TabStop = false;
            this.PropertyBox.Text = "Свойство материала";
            // 
            // txtBoxSearch
            // 
            this.txtBoxSearch.Location = new System.Drawing.Point(12, 551);
            this.txtBoxSearch.Name = "txtBoxSearch";
            this.txtBoxSearch.Size = new System.Drawing.Size(328, 20);
            this.txtBoxSearch.TabIndex = 36;
            this.txtBoxSearch.TextChanged += new System.EventHandler(this.txtBoxSearch_TextChanged);
            this.txtBoxSearch.Enter += new System.EventHandler(this.txtBoxSearch_Enter);
            this.txtBoxSearch.Leave += new System.EventHandler(this.txtBoxSearch_Leave);
            // 
            // Materials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(817, 587);
            this.Controls.Add(this.txtBoxSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PropertyBox);
            this.Controls.Add(this.BtnOkAndClose);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.MatTree);
            this.Controls.Add(this.ButtonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Materials";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор материалов";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Materials_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridConfig)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.PropertyBox.ResumeLayout(false);
            this.PropertyBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button CloseButton;
        internal System.Windows.Forms.TreeView MatTree;
        internal System.Windows.Forms.Button ButtonOK;
        internal System.Windows.Forms.Button BtnOkAndClose;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox MaterialName;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox Plotnost;
        internal System.Windows.Forms.TextBox SwProp;
        internal System.Windows.Forms.TextBox KodERP;
        internal System.Windows.Forms.TextBox KodMateriala;
        internal System.Windows.Forms.TextBox Description;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.DataGridView DataGridConfig;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColChB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColClass;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox CboClass;
        private System.Windows.Forms.ComboBox CboType;
        private System.Windows.Forms.ComboBox CboColor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.GroupBox PropertyBox;
        private System.Windows.Forms.TextBox txtBoxSearch;
    }
}