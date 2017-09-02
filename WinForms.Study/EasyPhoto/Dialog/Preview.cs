using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyPhoto.Dialog
{
    public partial class Preview : Form
    {
        public Bitmap Image;
        public Preview()
        {
            InitializeComponent();
        }

        public void LoadImage(Image loadimage)
        {
            this.Image = (Bitmap)loadimage.Clone();
            this.Size = new Size(loadimage.Size.Width + 25, loadimage.Size.Height+80);
            this.pictureBox1.Size = loadimage.Size;
            this.pictureBox1.Location = new Point(10, 10);
            this.button1.Location = new Point(10, loadimage.Size.Height + 20);
            this.button2.Location = new Point(loadimage.Size.Width + 10-this.button2.Size.Width, loadimage.Size.Height + 20);
            this.pictureBox1.Image = loadimage;
            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JPG图像文件|*.jpg|位图文件|*.bmp";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.Image.Save(sfd.FileName);
                sfd.Dispose();
            }
        }
    }
}
