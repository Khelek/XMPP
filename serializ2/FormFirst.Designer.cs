namespace serializ2
{
    partial class FormFirst
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
            this.createConference = new System.Windows.Forms.Button();
            this.checkBoxConnected = new System.Windows.Forms.CheckBox();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.loginBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.Login_Button = new System.Windows.Forms.Button();
            this.Add_Users = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Location = new System.Drawing.Point(59, 12);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.Size = new System.Drawing.Size(114, 20);
            this.textBoxStatus.TabIndex = 3;
            // 
            // createConference
            // 
            this.createConference.Location = new System.Drawing.Point(188, 66);
            this.createConference.Name = "createConference";
            this.createConference.Size = new System.Drawing.Size(101, 24);
            this.createConference.TabIndex = 8;
            this.createConference.Text = "Конференция";
            this.createConference.UseVisualStyleBackColor = true;
            this.createConference.Visible = false;
            this.createConference.Click += new System.EventHandler(this.createConference_Click);
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
            // listBoxUsers
            // 
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.Location = new System.Drawing.Point(12, 66);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new System.Drawing.Size(161, 264);
            this.listBoxUsers.TabIndex = 10;
            this.listBoxUsers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox2_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Статус";
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
            this.loginBox.Location = new System.Drawing.Point(189, 245);
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(101, 20);
            this.loginBox.TabIndex = 14;
            this.loginBox.Text = "Логин";
            this.loginBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.login_MouseClick);
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(189, 271);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.Size = new System.Drawing.Size(101, 20);
            this.passwordBox.TabIndex = 15;
            this.passwordBox.Text = "Пароль";
            this.passwordBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.password_MouseClick);
            // 
            // Login_Button
            // 
            this.Login_Button.Location = new System.Drawing.Point(189, 297);
            this.Login_Button.Name = "Login_Button";
            this.Login_Button.Size = new System.Drawing.Size(100, 34);
            this.Login_Button.TabIndex = 16;
            this.Login_Button.Text = "Войти";
            this.Login_Button.UseVisualStyleBackColor = true;
            this.Login_Button.Click += new System.EventHandler(this.Login_Button_Click);
            // 
            // Add_Users
            // 
            this.Add_Users.Location = new System.Drawing.Point(189, 12);
            this.Add_Users.Name = "Add_Users";
            this.Add_Users.Size = new System.Drawing.Size(100, 36);
            this.Add_Users.TabIndex = 17;
            this.Add_Users.Text = "Добавить контакт";
            this.Add_Users.UseVisualStyleBackColor = true;
            this.Add_Users.Click += new System.EventHandler(this.Add_Users_Click);
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(189, 297);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(101, 33);
            this.Exit.TabIndex = 18;
            this.Exit.Text = "Выйти";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Visible = false;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // FormFirst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 348);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Add_Users);
            this.Controls.Add(this.Login_Button);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.loginBox);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxUsers);
            this.Controls.Add(this.checkBoxConnected);
            this.Controls.Add(this.createConference);
            this.Controls.Add(this.textBoxStatus);
            this.Name = "FormFirst";
            this.Text = "FormFirst";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.Button createConference;
        private System.Windows.Forms.CheckBox checkBoxConnected;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox loginBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Button Login_Button;
        private System.Windows.Forms.Button Add_Users;
        private System.Windows.Forms.Button Exit;
    }
}

