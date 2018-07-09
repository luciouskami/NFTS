namespace WindowsFormsApp1
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Windows.Forms;

    public class Form2 : Form
    {
        private IContainer components = null;
        private Button button1;
        private Button button2;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private TextBox textBox2;

        public Form2()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //2018/7/9 14:42 删除登录请求的代码，修改调用Form1.Login方法的代码。
            Form1.Login();
            Form1.UpdataList2();
            base.Close();
                
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.button1 = new Button();
            this.button2 = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            base.SuspendLayout();
            this.button1.Location = new Point(12, 0x85);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0;
            this.button1.Text = "登录";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.Location = new Point(0xc5, 0x85);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 1;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 20f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(0x6d, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x42, 0x1b);
            this.label1.TabIndex = 2;
            this.label1.Text = "登录";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label2.Location = new Point(0x2e, 0x35);
            this.label2.Name = "label2";
            this.label2.Size = new Size(40, 0x10);
            this.label2.TabIndex = 3;
            this.label2.Text = "账号";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label3.Location = new Point(0x2e, 0x56);
            this.label3.Name = "label3";
            this.label3.Size = new Size(40, 0x10);
            this.label3.TabIndex = 4;
            this.label3.Text = "密码";
            this.textBox1.Location = new Point(0x5c, 0x30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(140, 0x15);
            this.textBox1.TabIndex = 5;
            this.textBox2.Location = new Point(0x5c, 0x51);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new Size(140, 0x15);
            this.textBox2.TabIndex = 6;
            this.textBox2.KeyDown += new KeyEventHandler(this.textBox2_KeyDown);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Silver;
            base.ClientSize = new Size(0x11c, 0xa8);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "Form2";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Form2";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }
    }
}

