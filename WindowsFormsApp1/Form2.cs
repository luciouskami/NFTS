using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Form2 : Form
    {
        private Button button1;
        private Button button2;
        private readonly IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private TextBox textBox2;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("账号不能为空!");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("密码不能为空!");
            }
            else
            {
                textBox2.Text = Form1.MD5(textBox2.Text);
                var request = (HttpWebRequest) WebRequest.Create(
                    "http://foreverxip.com/PCNFTS/Login.php?user=" + textBox1.Text + "&password=" + textBox2.Text);
                var response = (HttpWebResponse) request.GetResponse();
                var str = new StreamReader(response.GetResponseStream()).ReadToEnd();
                if (str == "")
                {
                    MessageBox.Show("账号或密码错误");
                }
                else
                {
                    char[] separator;
                    separator = new[] {'|'};
                    var strArray = str.Split(separator);
                    Form1.Login(textBox1.Text, strArray[0], strArray[1], strArray[2]);
                    Form1.userList.Clear();
                    for (var i = 2; i < strArray.Length; i++) Form1.userList.Add(strArray[i]);
                    Form1.UpdataList2();
                    Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            SuspendLayout();
            button1.Location = new Point(12, 0x85);
            button1.Name = "button1";
            button1.Size = new Size(0x4b, 0x17);
            button1.TabIndex = 0;
            button1.Text = "登录";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            button2.Location = new Point(0xc5, 0x85);
            button2.Name = "button2";
            button2.Size = new Size(0x4b, 0x17);
            button2.TabIndex = 1;
            button2.Text = "取消";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            label1.AutoSize = true;
            label1.Font = new Font("宋体", 20f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            label1.Location = new Point(0x6d, 9);
            label1.Name = "label1";
            label1.Size = new Size(0x42, 0x1b);
            label1.TabIndex = 2;
            label1.Text = "登录";
            label2.AutoSize = true;
            label2.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            label2.Location = new Point(0x2e, 0x35);
            label2.Name = "label2";
            label2.Size = new Size(40, 0x10);
            label2.TabIndex = 3;
            label2.Text = "账号";
            label3.AutoSize = true;
            label3.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            label3.Location = new Point(0x2e, 0x56);
            label3.Name = "label3";
            label3.Size = new Size(40, 0x10);
            label3.TabIndex = 4;
            label3.Text = "密码";
            textBox1.Location = new Point(0x5c, 0x30);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(140, 0x15);
            textBox1.TabIndex = 5;
            textBox2.Location = new Point(0x5c, 0x51);
            textBox2.Name = "textBox2";
            textBox2.PasswordChar = '*';
            textBox2.Size = new Size(140, 0x15);
            textBox2.TabIndex = 6;
            textBox2.KeyDown += textBox2_KeyDown;
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(0x11c, 0xa8);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form2";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1_Click(sender, e);
        }
    }
}