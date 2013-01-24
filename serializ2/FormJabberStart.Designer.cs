namespace EnterpriseMICApplicationDemo
{
    partial class FormJabberStart
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.checkBoxConnected = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.loginBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.Add_Users = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.buttonCreateConf = new System.Windows.Forms.Button();
            this.buttonJoinConf = new System.Windows.Forms.Button();
            this.listUsers = new System.Windows.Forms.ListView();
            this.buttonShowHideContacts = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.opacityLabel1 = new System.Windows.Forms.Label();//EnterpriseMICApplicationDemo.OpacityLabel();
            this.SuspendLayout();
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Location = new System.Drawing.Point(59, 12);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.Size = new System.Drawing.Size(114, 20);
            this.textBoxStatus.TabIndex = 3;
            // 
            // checkBoxConnected
            // 
            this.checkBoxConnected.AutoSize = true;
            this.checkBoxConnected.Enabled = false;
            this.checkBoxConnected.Location = new System.Drawing.Point(12, 38);
            this.checkBoxConnected.Name = "checkBoxConnected";
            this.checkBoxConnected.Size = new System.Drawing.Size(59, 17);
            this.checkBoxConnected.TabIndex = 9;
            this.checkBoxConnected.Text = "В сети";
            this.checkBoxConnected.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(77, 38);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 23);
            this.button4.TabIndex = 13;
            this.button4.Text = "Поставить";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // loginBox
            // 
            this.loginBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loginBox.Location = new System.Drawing.Point(183, 245);
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(106, 20);
            this.loginBox.TabIndex = 14;
            this.loginBox.Text = "Логин";
            this.loginBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.login_MouseClick);
            // 
            // passwordBox
            // 
            this.passwordBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordBox.Location = new System.Drawing.Point(183, 271);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.Size = new System.Drawing.Size(106, 20);
            this.passwordBox.TabIndex = 15;
            this.passwordBox.Text = "Пароль";
            this.passwordBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.password_MouseClick);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLogin.Location = new System.Drawing.Point(183, 297);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(106, 34);
            this.buttonLogin.TabIndex = 16;
            this.buttonLogin.Text = "Войти";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // Add_Users
            // 
            this.Add_Users.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Add_Users.Location = new System.Drawing.Point(183, 12);
            this.Add_Users.Name = "Add_Users";
            this.Add_Users.Size = new System.Drawing.Size(106, 49);
            this.Add_Users.TabIndex = 17;
            this.Add_Users.Text = "Добавить контакт";
            this.Add_Users.UseVisualStyleBackColor = true;
            this.Add_Users.Click += new System.EventHandler(this.Add_Users_Click);
            // 
            // Exit
            // 
            this.Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Exit.Location = new System.Drawing.Point(183, 297);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(107, 33);
            this.Exit.TabIndex = 18;
            this.Exit.Text = "Выйти";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Visible = false;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // buttonCreateConf
            // 
            this.buttonCreateConf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreateConf.Location = new System.Drawing.Point(183, 66);
            this.buttonCreateConf.Name = "buttonCreateConf";
            this.buttonCreateConf.Size = new System.Drawing.Size(106, 39);
            this.buttonCreateConf.TabIndex = 19;
            this.buttonCreateConf.Text = "Создать конференцию";
            this.buttonCreateConf.UseVisualStyleBackColor = true;
            this.buttonCreateConf.Visible = false;
            this.buttonCreateConf.Click += new System.EventHandler(this.createConference_Click);
            // 
            // buttonJoinConf
            // 
            this.buttonJoinConf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonJoinConf.Location = new System.Drawing.Point(183, 111);
            this.buttonJoinConf.Name = "buttonJoinConf";
            this.buttonJoinConf.Size = new System.Drawing.Size(106, 39);
            this.buttonJoinConf.TabIndex = 20;
            this.buttonJoinConf.Text = "Присоединиться к конференции";
            this.buttonJoinConf.UseVisualStyleBackColor = true;
            this.buttonJoinConf.Visible = false;
            this.buttonJoinConf.Click += new System.EventHandler(this.buttonJoinConf_Click);
            // 
            // listUsers
            // 
            this.listUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listUsers.FullRowSelect = true;
            this.listUsers.Location = new System.Drawing.Point(12, 66);
            this.listUsers.MultiSelect = false;
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(161, 264);
            this.listUsers.TabIndex = 21;
            this.listUsers.UseCompatibleStateImageBehavior = false;
            this.listUsers.View = System.Windows.Forms.View.Tile;
            this.listUsers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listUsers_KeyDown);
            this.listUsers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listUsers_MouseDoubleClick);
            // 
            // buttonShowHideContacts
            // 
            this.buttonShowHideContacts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowHideContacts.Location = new System.Drawing.Point(183, 156);
            this.buttonShowHideContacts.Name = "buttonShowHideContacts";
            this.buttonShowHideContacts.Size = new System.Drawing.Size(107, 36);
            this.buttonShowHideContacts.TabIndex = 22;
            this.buttonShowHideContacts.Text = "Показать/скрыть оффлайн контакты";
            this.buttonShowHideContacts.UseVisualStyleBackColor = true;
            this.buttonShowHideContacts.Visible = false;
            this.buttonShowHideContacts.Click += new System.EventHandler(this.buttonShowHideContacts_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSettings.Location = new System.Drawing.Point(183, 198);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(107, 41);
            this.buttonSettings.TabIndex = 23;
            this.buttonSettings.Text = "Настройки";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 172;
            // 
            // opacityLabel1
            // 
            this.opacityLabel1.AutoSize = true;
            this.opacityLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(15)))), ((int)(((byte)(0)))));
            this.opacityLabel1.ForeColor = System.Drawing.Color.Black;
            //this.opacityLabel1.Indent = EnterpriseMICApplicationDemo.OpacityLabel.ControlIndent.None;
            this.opacityLabel1.Location = new System.Drawing.Point(15, 15);
            this.opacityLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.opacityLabel1.Name = "opacityLabel1";
            this.opacityLabel1.Size = new System.Drawing.Size(41, 13);
            this.opacityLabel1.TabIndex = 24;
            this.opacityLabel1.Text = "Статус";
            // 
            // FormJabberStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 348);
            this.Controls.Add(this.opacityLabel1);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.buttonShowHideContacts);
            this.Controls.Add(this.listUsers);
            this.Controls.Add(this.buttonJoinConf);
            this.Controls.Add(this.buttonCreateConf);
            this.Controls.Add(this.Add_Users);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.loginBox);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.checkBoxConnected);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.Exit);
            this.Name = "FormJabberStart";
            this.Text = "FormJabberStart";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormJabberStart_FormClosing);
            this.Load += new System.EventHandler(this.FormJabberStart_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.CheckBox checkBoxConnected;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox loginBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button Add_Users;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button buttonCreateConf;
        private System.Windows.Forms.Button buttonJoinConf;
        private System.Windows.Forms.ListView listUsers;
        private System.Windows.Forms.Button buttonShowHideContacts;
        private System.Windows.Forms.Button buttonSettings;
		private System.Windows.Forms.Label opacityLabel1;
        //private OpacityLabel opacityLabel1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}

