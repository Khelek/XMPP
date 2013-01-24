using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using agsXMPP;
using agsXMPP.protocol.client;

namespace EnterpriseMICApplicationDemo
{
    public partial class FormDialog : Form
    {
        public TabControl conversation;
        public FormDialog()
        {
            InitializeComponent();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public void createTabPage(object sender, string tabName, string key, string insideText) {
            
            // 
            // checkSendIfEnter
            // 
            CheckBox checkSendIfEnter = new CheckBox();
            checkSendIfEnter.AutoSize = true;
            checkSendIfEnter.Checked = true;
            checkSendIfEnter.CheckState = System.Windows.Forms.CheckState.Checked;
            checkSendIfEnter.Location = new System.Drawing.Point(4, 288);
            checkSendIfEnter.Name = "checkSendIfEnter";
            checkSendIfEnter.Size = new System.Drawing.Size(288, 17);
            checkSendIfEnter.TabIndex = 4;
            checkSendIfEnter.Text = "Отправлять сообщение при нажатии клавиши Enter";
            checkSendIfEnter.UseVisualStyleBackColor = true;
            // 
            // textBoxSend
            // 
            TextBox textBoxSend = new TextBox();
            textBoxSend.Location = new System.Drawing.Point(0, 306);
            textBoxSend.Multiline = true;
            textBoxSend.Name = "textBoxSend";
            textBoxSend.Size = new System.Drawing.Size(387, 49);
            textBoxSend.TabIndex = 2;
            // 
            // rtfDialog
            // 
            RichTextBox rtfDialog = new RichTextBox();
            rtfDialog.Location = new System.Drawing.Point(0, 0);
            rtfDialog.Name = "rtfDialog";
            rtfDialog.Size = new System.Drawing.Size(482, 288);
            rtfDialog.TabIndex = 1;
            rtfDialog.Text = "";
            // 
            // buttonSend
            // 
            Button buttonSend = new Button();
            buttonSend.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            buttonSend.Location = new System.Drawing.Point(393, 306);
            buttonSend.Name = "buttonSend";
            buttonSend.Size = new System.Drawing.Size(89, 49);
            buttonSend.TabIndex = 3;
            buttonSend.Text = "Отправить";
            buttonSend.UseVisualStyleBackColor = true;
            buttonSend.Click += delegate(object s, EventArgs a) {
                ((FormJabberStart)sender).SendTextMessage(new Jid(key), rtfDialog, textBoxSend.Text);
                textBoxSend.Clear();
            };
            textBoxSend.KeyDown += delegate(object o, KeyEventArgs k) {
                if ( k.KeyCode == Keys.Enter && checkSendIfEnter.Checked ) {
                    ((FormJabberStart)sender).SendTextMessage(new Jid(key), rtfDialog, textBoxSend.Text);
                    textBoxSend.Clear();
                }
            };

            TabPage tabPage = new TabPage();
            tabPage.Controls.Add(buttonSend);
            tabPage.Controls.Add(checkSendIfEnter);
            tabPage.Controls.Add(textBoxSend);
            tabPage.Controls.Add(rtfDialog);
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = key;
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(482, 355);
            tabPage.Text = tabName;
            tabPage.UseVisualStyleBackColor = true;

            tabControlDialogs.TabPages.Add(tabPage);
        }

    }
}
