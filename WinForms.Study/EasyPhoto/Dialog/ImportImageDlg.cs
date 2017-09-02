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
    public partial class ImportImageDlg : Form
    {
        private bool isImport;
        private Image importImage;
        private Size importSize;
        /// <summary>
        /// 获取导入大小
        /// </summary>
        public Size ImportSize
        {
            get { return importSize; }
        }
        /// <summary>
        /// 获取是否导入成功
        /// </summary>
        public bool IsImport
        {
            get { return isImport; }
        }
        /// <summary>
        /// 获取导入的图像
        /// </summary>
        public Image ImportImage
        {
            get { return importImage; }
        }

        public ImportImageDlg()
        {
            InitializeComponent();
            isImport = false;
            radioButton1.Checked = true;
            this.numericUpDown1.Enabled = false;
            this.numericUpDown2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog pfd = new OpenFileDialog();
            pfd.Filter = "JPG图像文件|*.jpg|BMP图像文件|*.bmp";
            if (pfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    importImage = Bitmap.FromFile(pfd.FileName);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("图像打开错误, 请确认图像是否正确!", "错误");
                    return;
                }
                this.textBox1.Text = pfd.FileName;
                this.label2.Text = "长度:" + importImage.Width.ToString();
                this.label3.Text = "高度:" + importImage.Height.ToString();
                this.numericUpDown1.Value = importImage.Width;
                this.numericUpDown2.Value = importImage.Height;
            }
        }

        void radioButton2_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.radioButton2.Checked)
            {
                this.numericUpDown1.Enabled = true;
                this.numericUpDown2.Enabled = true;
            }
            else
            {
                this.numericUpDown1.Enabled = false;
                this.numericUpDown2.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.isImport = false;
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "")
            {
                MessageBox.Show("未指定导入的图像！", "提示");
                return;
            }
            else
            {
                this.importSize = new Size((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value);
                if (this.radioButton1.Checked)
                {
                    this.importSize = new Size(this.importImage.Width, this.importImage.Height);
                }
                else
                {
                    this.importSize = new Size((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value);
                }
                this.isImport = true;
                this.Dispose();
            }
        }
    }
}
