using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace NFTS
{
    public class Form6 : Form
    {
        private Button button1;
        private readonly IContainer components = null;
        private RichTextBox richTextBox1;
        private TextBox textBox1;

        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("内容不能为空");
            }
            else if (textBox1.Text == "联系方式")
            {
                MessageBox.Show("联系方式不能为空");
            }
            else
            {
                var request = (HttpWebRequest) WebRequest.Create(
                    "http://foreverxip.com/PCNFTS/Bug.php?A=" + richTextBox1.Text + "&B=969352269&C=" + textBox1.Text);
                var response = (HttpWebResponse) request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                Close();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            button1 = new Button();
            richTextBox1 = new RichTextBox();
            textBox1 = new TextBox();
            SuspendLayout();
            button1.Location = new Point(0xc5, 0xe2);
            button1.Name = "button1";
            button1.Size = new Size(0x4b, 0x17);
            button1.TabIndex = 0;
            button1.Text = "提交";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            richTextBox1.Location = new Point(12, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(260, 0xd0);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            textBox1.ForeColor = Color.LightGray;
            textBox1.Location = new Point(12, 0xe2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(0xb3, 0x15);
            textBox1.TabIndex = 2;
            textBox1.Text = "联系方式";
            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(0x11c, 0x105);
            Controls.Add(textBox1);
            Controls.Add(richTextBox1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form6";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NFTS Bug/建议反馈";
            ResumeLayout(false);
            PerformLayout();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "联系方式")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "联系方式";
                textBox1.ForeColor = Color.LightGray;
            }
        }
    }
}