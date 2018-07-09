using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NFTS
{
    public class Form5 : Form
    {
        private readonly IContainer components = null;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;

        public Form5()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            ((ISupportInitialize)pictureBox1).BeginInit();
            ((ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            pictureBox1.ImageLocation = "https://wx3.sinaimg.cn/mw690/006i6J4Ygy1fmrz93kqv0j30jg0qota2.jpg";
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 300);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            pictureBox2.ImageLocation = "https://wx3.sinaimg.cn/mw690/006i6J4Ygy1fmrz93ljbxj30z61hc77b.jpg";
            pictureBox2.Location = new Point(200, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(200, 300);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 300);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form5";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form5";
            ((ISupportInitialize)pictureBox1).EndInit();
            ((ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}