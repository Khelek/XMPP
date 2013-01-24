namespace EnterpriseMICApplicationDemo
{
    partial class FormCreateConferention
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
            this.buttonCreate = new System.Windows.Forms.Button();
            this.listViewUsers = new System.Windows.Forms.ListView();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxConfName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxConfIdent = new System.Windows.Forms.TextBox();
            this.checkBoxHistory = new System.Windows.Forms.CheckBox();
            this.checkBoxPersistentRoom = new System.Windows.Forms.CheckBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCreate
            // 
            this.buttonCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreate.Location = new System.Drawing.Point(245, 222);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(136, 35);
            this.buttonCreate.TabIndex = 0;
            this.buttonCreate.Text = "Создать конференцию";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // listViewUsers
            // 
            this.listViewUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewUsers.CheckBoxes = true;
            this.listViewUsers.Location = new System.Drawing.Point(261, 25);
            this.listViewUsers.Name = "listViewUsers";
            this.listViewUsers.Size = new System.Drawing.Size(228, 191);
            this.listViewUsers.TabIndex = 1;
            this.listViewUsers.UseCompatibleStateImageBehavior = false;
            this.listViewUsers.View = System.Windows.Forms.View.List;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(387, 223);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(104, 34);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(261, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Участники";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Название конференции";
            // 
            // textBoxConfName
            // 
            this.textBoxConfName.Location = new System.Drawing.Point(12, 25);
            this.textBoxConfName.Name = "textBoxConfName";
            this.textBoxConfName.Size = new System.Drawing.Size(243, 20);
            this.textBoxConfName.TabIndex = 4;
            this.textBoxConfName.Text = "Конференция IT-разработчиков";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Идентификатор конференции";
            // 
            // textBoxConfIdent
            // 
            this.textBoxConfIdent.Location = new System.Drawing.Point(12, 64);
            this.textBoxConfIdent.Name = "textBoxConfIdent";
            this.textBoxConfIdent.Size = new System.Drawing.Size(243, 20);
            this.textBoxConfIdent.TabIndex = 6;
            this.textBoxConfIdent.Text = "grrconf";
            // 
            // checkBoxHistory
            // 
            this.checkBoxHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxHistory.AutoSize = true;
            this.checkBoxHistory.Location = new System.Drawing.Point(28, 176);
            this.checkBoxHistory.Name = "checkBoxHistory";
            this.checkBoxHistory.Size = new System.Drawing.Size(196, 17);
            this.checkBoxHistory.TabIndex = 8;
            this.checkBoxHistory.Text = "Сохранять историю конференции";
            this.checkBoxHistory.UseVisualStyleBackColor = true;
            // 
            // checkBoxPersistentRoom
            // 
            this.checkBoxPersistentRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxPersistentRoom.AutoSize = true;
            this.checkBoxPersistentRoom.Location = new System.Drawing.Point(28, 199);
            this.checkBoxPersistentRoom.Name = "checkBoxPersistentRoom";
            this.checkBoxPersistentRoom.Size = new System.Drawing.Size(203, 17);
            this.checkBoxPersistentRoom.TabIndex = 9;
            this.checkBoxPersistentRoom.Text = "Сделать конференцию постоянной";
            this.checkBoxPersistentRoom.UseVisualStyleBackColor = true;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxDescription.Location = new System.Drawing.Point(12, 107);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(243, 63);
            this.textBoxDescription.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Описание комнаты";
            // 
            // FormCreateConferention
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 266);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.checkBoxPersistentRoom);
            this.Controls.Add(this.checkBoxHistory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxConfIdent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxConfName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.listViewUsers);
            this.Controls.Add(this.buttonCreate);
            this.Name = "FormCreateConferention";
            this.Text = "FormCreateConferention";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.ListView listViewUsers;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxConfName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxConfIdent;
        private System.Windows.Forms.CheckBox checkBoxHistory;
        private System.Windows.Forms.CheckBox checkBoxPersistentRoom;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label4;
    }
}