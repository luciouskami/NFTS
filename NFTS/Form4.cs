using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace NFTS
{
    public class Form4 : Form
    {
        private static Form form;
        private static PictureBox box;
        private static int w;
        private static int h;
        private readonly IContainer components = null;
        private PictureBox pictureBox1;

        public Form4()
        {
            InitializeComponent();
        }

        private void Box_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                w = (int) (w * 1.1f);
                h = (int) (h * 1.1f);
            }
            else
            {
                w = (int) (w * 0.9f);
                h = (int) (h * 0.9f);
            }

            box.Width = w;
            box.Height = h;
            form.Width = w;
            form.Height = h;
            box.Location = new Point(0, 0);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            form = this;
            box = pictureBox1;
            box.MouseWheel += Box_MouseWheel;
        }

        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            ((ISupportInitialize) pictureBox1).BeginInit();
            SuspendLayout();
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(10, 10);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(0x156, 0x108);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form4";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form4";
            Load += Form4_Load;
            ((ISupportInitialize) pictureBox1).EndInit();
            ResumeLayout(false);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static void Show(string url)
        {
            var requestUri = new Uri(url);
            var request = (HttpWebRequest) WebRequest.Create(requestUri);
            var responseStream = ((HttpWebResponse) request.GetResponse()).GetResponseStream();
            var image = Image.FromStream(responseStream);
            responseStream.Close();
            w = image.Width;
            h = image.Height;
            box.Width = w;
            box.Height = h;
            form.Width = w;
            form.Height = h;
            box.Location = new Point(0, 0);
            box.ImageLocation = url;
        }
    }
}