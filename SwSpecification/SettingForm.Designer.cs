namespace SwSpecification
{
    partial class SettingForm
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
            this.Panel1 = new System.Windows.Forms.Panel();
            this.BtnObzor = new System.Windows.Forms.Button();
            this.Otmena = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Path1c = new System.Windows.Forms.TextBox();
            this.PathKey = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.pass = new System.Windows.Forms.TextBox();
            this.Login = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Linen;
            this.Panel1.Controls.Add(this.BtnObzor);
            this.Panel1.Controls.Add(this.Otmena);
            this.Panel1.Controls.Add(this.Save);
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.Path1c);
            this.Panel1.Controls.Add(this.PathKey);
            this.Panel1.Controls.Add(this.Label4);
            this.Panel1.Controls.Add(this.Label5);
            this.Panel1.Controls.Add(this.pass);
            this.Panel1.Controls.Add(this.Login);
            this.Panel1.Location = new System.Drawing.Point(8, 35);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(335, 231);
            this.Panel1.TabIndex = 51;
            // 
            // BtnObzor
            // 
            this.BtnObzor.BackColor = System.Drawing.Color.Linen;
            this.BtnObzor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnObzor.Location = new System.Drawing.Point(295, 115);
            this.BtnObzor.Name = "BtnObzor";
            this.BtnObzor.Size = new System.Drawing.Size(23, 23);
            this.BtnObzor.TabIndex = 67;
            this.BtnObzor.Text = ">";
            this.BtnObzor.UseVisualStyleBackColor = false;
            this.BtnObzor.Click += new System.EventHandler(this.BtnObzor_Click);
            // 
            // Otmena
            // 
            this.Otmena.BackColor = System.Drawing.Color.Linen;
            this.Otmena.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Otmena.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Otmena.Location = new System.Drawing.Point(199, 195);
            this.Otmena.Name = "Otmena";
            this.Otmena.Size = new System.Drawing.Size(119, 23);
            this.Otmena.TabIndex = 66;
            this.Otmena.Text = "Отмена";
            this.Otmena.UseVisualStyleBackColor = false;
            this.Otmena.Click += new System.EventHandler(this.Otmena_Click);
            // 
            // Save
            // 
            this.Save.BackColor = System.Drawing.Color.Linen;
            this.Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Save.Location = new System.Drawing.Point(16, 195);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(119, 23);
            this.Save.TabIndex = 65;
            this.Save.Text = "Сохранить";
            this.Save.UseVisualStyleBackColor = false;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label2.Location = new System.Drawing.Point(14, 100);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(149, 16);
            this.Label2.TabIndex = 64;
            this.Label2.Text = "Путь к 1С Предприятие:";
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label1.Location = new System.Drawing.Point(15, 144);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(135, 13);
            this.Label1.TabIndex = 63;
            this.Label1.Text = "Ключ запуска:";
            // 
            // Path1c
            // 
            this.Path1c.Location = new System.Drawing.Point(16, 117);
            this.Path1c.Name = "Path1c";
            this.Path1c.Size = new System.Drawing.Size(273, 20);
            this.Path1c.TabIndex = 62;
            // 
            // PathKey
            // 
            this.PathKey.Location = new System.Drawing.Point(16, 163);
            this.PathKey.Name = "PathKey";
            this.PathKey.Size = new System.Drawing.Size(302, 20);
            this.PathKey.TabIndex = 61;
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label4.Location = new System.Drawing.Point(14, 57);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(57, 14);
            this.Label4.TabIndex = 60;
            this.Label4.Text = "Пароль:";
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label5.Location = new System.Drawing.Point(15, 13);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(106, 12);
            this.Label5.TabIndex = 59;
            this.Label5.Text = "Имя:";
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(16, 75);
            this.pass.Name = "pass";
            this.pass.PasswordChar = '*';
            this.pass.Size = new System.Drawing.Size(302, 20);
            this.pass.TabIndex = 58;
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(16, 31);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(302, 20);
            this.Login.TabIndex = 57;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label3.Location = new System.Drawing.Point(12, 9);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(297, 15);
            this.Label3.TabIndex = 50;
            this.Label3.Text = "Настройки подключения к 1С Предприятие";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(352, 275);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingForm";
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button BtnObzor;
        internal System.Windows.Forms.Button Otmena;
        internal System.Windows.Forms.Button Save;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox Path1c;
        internal System.Windows.Forms.TextBox PathKey;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox pass;
        internal System.Windows.Forms.TextBox Login;
        internal System.Windows.Forms.Label Label3;

    }
}