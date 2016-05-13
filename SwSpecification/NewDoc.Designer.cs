namespace SwSpecification
{
    partial class NewDoc
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("Спецификация (Вариант А)", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup8 = new System.Windows.Forms.ListViewGroup("Спецификация (Вариант Б)", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("Форма 1 и 1а");
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("Форма 5");
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem("Форма 1 и 1а");
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem("Форма 1 и 1б");
            this.BtnAddin = new System.Windows.Forms.Button();
            this.BtCreate = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.ListView1 = new System.Windows.Forms.ListView();
            this.Button2 = new System.Windows.Forms.Button();
            this.DataGridConfig = new System.Windows.Forms.DataGridView();
            this.ColChB = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Col2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnAddin
            // 
            this.BtnAddin.BackColor = System.Drawing.Color.Linen;
            this.BtnAddin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnAddin.Location = new System.Drawing.Point(7, 366);
            this.BtnAddin.Name = "BtnAddin";
            this.BtnAddin.Size = new System.Drawing.Size(100, 27);
            this.BtnAddin.TabIndex = 26;
            this.BtnAddin.Text = "Добавления";
            this.BtnAddin.UseVisualStyleBackColor = false;
            this.BtnAddin.Click += new System.EventHandler(this.BtnAddin_Click);
            // 
            // BtCreate
            // 
            this.BtCreate.BackColor = System.Drawing.Color.Linen;
            this.BtCreate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtCreate.Location = new System.Drawing.Point(256, 366);
            this.BtCreate.Name = "BtCreate";
            this.BtCreate.Size = new System.Drawing.Size(100, 27);
            this.BtCreate.TabIndex = 24;
            this.BtCreate.Text = "Создать";
            this.BtCreate.UseVisualStyleBackColor = false;
            this.BtCreate.Click += new System.EventHandler(this.BtCreate_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.ListView1);
            this.GroupBox1.Location = new System.Drawing.Point(7, 7);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(243, 353);
            this.GroupBox1.TabIndex = 28;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Список шаблонов:";
            // 
            // ListView1
            // 
            this.ListView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            listViewGroup7.Header = "Спецификация (Вариант А)";
            listViewGroup7.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup7.Name = "1";
            listViewGroup8.Header = "Спецификация (Вариант Б)";
            listViewGroup8.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup8.Name = "2";
            this.ListView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup7,
            listViewGroup8});
            listViewItem13.Group = listViewGroup7;
            listViewItem14.Group = listViewGroup8;
            listViewItem15.Group = listViewGroup8;
            listViewItem16.Group = listViewGroup8;
            this.ListView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16});
            this.ListView1.Location = new System.Drawing.Point(1, 14);
            this.ListView1.Name = "ListView1";
            this.ListView1.Size = new System.Drawing.Size(230, 325);
            this.ListView1.TabIndex = 2;
            this.ListView1.UseCompatibleStateImageBehavior = false;
            this.ListView1.View = System.Windows.Forms.View.Tile;
            // 
            // Button2
            // 
            this.Button2.BackColor = System.Drawing.Color.Linen;
            this.Button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Button2.Location = new System.Drawing.Point(376, 366);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(100, 27);
            this.Button2.TabIndex = 25;
            this.Button2.Text = "Отмена";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // DataGridConfig
            // 
            this.DataGridConfig.AllowUserToAddRows = false;
            this.DataGridConfig.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridConfig.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(224)))));
            this.DataGridConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColChB,
            this.Col2});
            this.DataGridConfig.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(224)))));
            this.DataGridConfig.Location = new System.Drawing.Point(256, 7);
            this.DataGridConfig.Name = "DataGridConfig";
            this.DataGridConfig.RowHeadersVisible = false;
            this.DataGridConfig.Size = new System.Drawing.Size(220, 344);
            this.DataGridConfig.TabIndex = 27;
            // 
            // ColChB
            // 
            this.ColChB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColChB.HeaderText = "";
            this.ColChB.Name = "ColChB";
            this.ColChB.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColChB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColChB.Width = 19;
            // 
            // Col2
            // 
            this.Col2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Col2.HeaderText = "Конфигурации";
            this.Col2.Name = "Col2";
            // 
            // timer1
            // 
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // NewDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(484, 400);
            this.Controls.Add(this.BtnAddin);
            this.Controls.Add(this.BtCreate);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.DataGridConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "NewDoc";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Создать отчет";
            this.Load += new System.EventHandler(this.NewDoc_Load);
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridConfig)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button BtnAddin;
        internal System.Windows.Forms.Button BtCreate;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.ListView ListView1;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.DataGridView DataGridConfig;
        internal System.Windows.Forms.DataGridViewCheckBoxColumn ColChB;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Col2;
        public System.Windows.Forms.Timer timer1;
    }
}