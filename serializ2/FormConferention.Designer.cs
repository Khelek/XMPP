namespace serializ2 {
    partial class FormConferention {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if ( disposing && ( components != null ) ) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.checkIfEnter = new System.Windows.Forms.CheckBox();
            this.textBoxSend = new System.Windows.Forms.TextBox();
            this.buttonHistory = new System.Windows.Forms.Button();
            this.rtfChat = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(146, 6);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(203, 20);
            this.txtSubject.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Название конференции";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(411, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Участники";
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.Location = new System.Drawing.Point(414, 30);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.ScrollAlwaysVisible = true;
            this.listBoxUsers.Size = new System.Drawing.Size(148, 238);
            this.listBoxUsers.TabIndex = 5;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(321, 299);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(87, 23);
            this.buttonSend.TabIndex = 7;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // checkIfEnter
            // 
            this.checkIfEnter.AutoSize = true;
            this.checkIfEnter.Checked = true;
            this.checkIfEnter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkIfEnter.Location = new System.Drawing.Point(321, 276);
            this.checkIfEnter.Name = "checkIfEnter";
            this.checkIfEnter.Size = new System.Drawing.Size(87, 17);
            this.checkIfEnter.TabIndex = 8;
            this.checkIfEnter.Text = "Send if Enter";
            this.checkIfEnter.UseVisualStyleBackColor = true;
            // 
            // textBoxSend
            // 
            this.textBoxSend.Location = new System.Drawing.Point(12, 271);
            this.textBoxSend.Multiline = true;
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.Size = new System.Drawing.Size(296, 51);
            this.textBoxSend.TabIndex = 9;
            this.textBoxSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxMessage_KeyDown);
            // 
            // buttonHistory
            // 
            this.buttonHistory.Location = new System.Drawing.Point(498, 276);
            this.buttonHistory.Name = "buttonHistory";
            this.buttonHistory.Size = new System.Drawing.Size(64, 29);
            this.buttonHistory.TabIndex = 10;
            this.buttonHistory.Text = "История";
            this.buttonHistory.UseVisualStyleBackColor = true;
            // 
            // rtfChat
            // 
            this.rtfChat.Location = new System.Drawing.Point(12, 30);
            this.rtfChat.Name = "rtfChat";
            this.rtfChat.ReadOnly = true;
            this.rtfChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtfChat.Size = new System.Drawing.Size(396, 235);
            this.rtfChat.TabIndex = 11;
            this.rtfChat.Text = "";
            // 
            // FormConferention
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 334);
            this.Controls.Add(this.rtfChat);
            this.Controls.Add(this.buttonHistory);
            this.Controls.Add(this.textBoxSend);
            this.Controls.Add(this.checkIfEnter);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.listBoxUsers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSubject);
            this.Name = "FormConferention";
            this.Text = "FormConferention";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormConferention_FormClosed);
            this.Load += new System.EventHandler(this.FormConferention_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.CheckBox checkIfEnter;
        private System.Windows.Forms.TextBox textBoxSend;
        private System.Windows.Forms.Button buttonHistory;
        private System.Windows.Forms.RichTextBox rtfChat;
    }
}