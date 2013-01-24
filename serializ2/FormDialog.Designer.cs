namespace EnterpriseMICApplicationDemo
{
    partial class FormDialog
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonSend = new System.Windows.Forms.Button();
            this.checkSendIfEnter = new System.Windows.Forms.CheckBox();
            this.textBoxSend = new System.Windows.Forms.TextBox();
            this.rtfDialog = new System.Windows.Forms.RichTextBox();
            this.tabControlDialogs = new System.Windows.Forms.TabControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.удалитьВкладкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1.SuspendLayout();
            this.tabControlDialogs.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(482, 355);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonSend);
            this.tabPage1.Controls.Add(this.checkSendIfEnter);
            this.tabPage1.Controls.Add(this.textBoxSend);
            this.tabPage1.Controls.Add(this.rtfDialog);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(482, 355);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Location = new System.Drawing.Point(393, 306);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(89, 49);
            this.buttonSend.TabIndex = 3;
            this.buttonSend.Text = "Отправить";
            this.buttonSend.UseVisualStyleBackColor = true;
            // 
            // checkSendIfEnter
            // 
            this.checkSendIfEnter.AutoSize = true;
            this.checkSendIfEnter.Checked = true;
            this.checkSendIfEnter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkSendIfEnter.Location = new System.Drawing.Point(4, 288);
            this.checkSendIfEnter.Name = "checkSendIfEnter";
            this.checkSendIfEnter.Size = new System.Drawing.Size(288, 17);
            this.checkSendIfEnter.TabIndex = 4;
            this.checkSendIfEnter.Text = "Отправлять сообщение при нажатии клавиши Enter";
            this.checkSendIfEnter.UseVisualStyleBackColor = true;
            // 
            // textBoxSend
            // 
            this.textBoxSend.Location = new System.Drawing.Point(0, 306);
            this.textBoxSend.Multiline = true;
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.Size = new System.Drawing.Size(387, 49);
            this.textBoxSend.TabIndex = 2;
            // 
            // rtfDialog
            // 
            this.rtfDialog.Location = new System.Drawing.Point(0, 0);
            this.rtfDialog.Name = "rtfDialog";
            this.rtfDialog.Size = new System.Drawing.Size(482, 288);
            this.rtfDialog.TabIndex = 1;
            this.rtfDialog.Text = "";
            // 
            // tabControlDialogs
            // 
            this.tabControlDialogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlDialogs.Controls.Add(this.tabPage1);
            this.tabControlDialogs.Controls.Add(this.tabPage2);
            this.tabControlDialogs.Location = new System.Drawing.Point(0, 0);
            this.tabControlDialogs.Name = "tabControlDialogs";
            this.tabControlDialogs.SelectedIndex = 0;
            this.tabControlDialogs.Size = new System.Drawing.Size(490, 381);
            this.tabControlDialogs.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.удалитьВкладкуToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(165, 26);
            // 
            // удалитьВкладкуToolStripMenuItem
            // 
            this.удалитьВкладкуToolStripMenuItem.Name = "удалитьВкладкуToolStripMenuItem";
            this.удалитьВкладкуToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.удалитьВкладкуToolStripMenuItem.Text = "Удалить вкладку";
            // 
            // FormDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 381);
            this.Controls.Add(this.tabControlDialogs);
            this.Name = "FormDialog";
            this.Text = "FormDialog";
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControlDialogs.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.CheckBox checkSendIfEnter;
        private System.Windows.Forms.TextBox textBoxSend;
        private System.Windows.Forms.RichTextBox rtfDialog;
        private System.Windows.Forms.TabControl tabControlDialogs;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem удалитьВкладкуToolStripMenuItem;


    }
}