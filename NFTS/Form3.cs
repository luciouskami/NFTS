using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NFTS
{
    public class Form3 : Form
    {
        private static Form form;
        private static RichTextBox Description;
        private static RichTextBox Comment;
        private static PictureBox ImgBox;
        private static List<string> list;
        private static int index;
        private static Label label;
        private static Button bt1;
        private static Button bt2;
        private Button button1;
        private Button button2;
        private readonly IContainer components = null;
        private Label label1;
        private PictureBox pictureBox1;
        private RichTextBox richTextBox1;
        private RichTextBox richTextBox2;

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            index--;
            if (index < 0) index = list.Count - 1;
            ImgBox.ImageLocation = list[index];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            index++;
            if (index == list.Count) index = 0;
            ImgBox.ImageLocation = list[index];
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            form = this;
            Description = richTextBox1;
            Comment = richTextBox2;
            ImgBox = pictureBox1;
            label = label1;
            bt1 = button1;
            bt2 = button2;
        }

        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            richTextBox1 = new RichTextBox();
            richTextBox2 = new RichTextBox();
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            ((ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            pictureBox1.Location = new Point(0x2b, 0xe0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(0xc5, 100);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            richTextBox1.Location = new Point(12, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(260, 100);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            richTextBox2.Location = new Point(11, 0x76);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.ReadOnly = true;
            richTextBox2.Size = new Size(260, 100);
            richTextBox2.TabIndex = 4;
            richTextBox2.Text = "";
            button1.Location = new Point(12, 0xe0);
            button1.Name = "button1";
            button1.Size = new Size(0x19, 100);
            button1.TabIndex = 5;
            button1.Text = "上一张";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            button2.Location = new Point(0xf6, 0xe0);
            button2.Name = "button2";
            button2.Size = new Size(0x19, 100);
            button2.TabIndex = 6;
            button2.Text = "下一张";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            label1.AutoSize = true;
            label1.Font = new Font("宋体", 30f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            label1.Location = new Point(0x35, 0xfc);
            label1.Name = "label1";
            label1.Size = new Size(0xb1, 40);
            label1.TabIndex = 7;
            label1.Text = "无预览图";
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(0x11c, 0x14e);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(richTextBox2);
            Controls.Add(richTextBox1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form3";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form3";
            Load += Form3_Load;
            ((ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var form = new Form4
            {
                Visible = true
            };
            Form4.Show(list[index]);
        }

        public static void ShowBox(string name, string str1, string str2, List<string> l)
        {
            form.Text = name;
            Description.Text = str1;
            Comment.Text = str2;
            list = l;
            if (list.Count > 0)
            {
                ImgBox.ImageLocation = list[index];
                label.Visible = false;
            }
            else
            {
                ImgBox.Enabled = false;
                bt1.Enabled = false;
                bt2.Enabled = false;
            }
        }
    }
}