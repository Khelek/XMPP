namespace serializ2
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
            this.SuspendLayout();
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(12, 318);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(136, 35);
            this.buttonCreate.TabIndex = 0;
            this.buttonCreate.Text = "Создать конференцию";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // listViewUsers
            // 
            this.listViewUsers.CheckBoxes = true;
            this.listViewUsers.Location = new System.Drawing.Point(12, 10);
            this.listViewUsers.Name = "listViewUsers";
            this.listViewUsers.Size = new System.Drawing.Size(236, 302);
            this.listViewUsers.TabIndex = 1;
            this.listViewUsers.UseCompatibleStateImageBehavior = false;
            this.listViewUsers.View = System.Windows.Forms.View.List;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(154, 318);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(93, 34);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormCreateConferention
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 365);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.listViewUsers);
            this.Controls.Add(this.buttonCreate);
            this.Name = "FormCreateConferention";
            this.Text = "FormCreateConferention";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.ListView listViewUsers;
        private System.Windows.Forms.Button buttonCancel;
    }
}