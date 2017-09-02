using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyPhoto.AttributeForm
{
    public partial class LayerMoveAttribute : UserControl
    {
        public MainForm SubParent = null;
        private int zoom;
        private string layerName;
        private float diaph;
        private bool cansee=true;

        public bool Cansee
        {
            set { this.cansee = value; }
        }

        public float Diaph
        {
            set { this.diaph = value; }
            get { return this.diaph; }
        }


        public int Zoom
        {
            set { this.zoom = value; }
            get { return zoom; }
        }

        public string LayerName
        {
            set { this.layerName = value; }
            get { return this.layerName; }
        }

        public LayerMoveAttribute()
        {
            InitializeComponent();
        }

        private void LayerMoveAttribute_Load(object sender, EventArgs e)
        {
            this.numericUpDown3.Value = (int)(this.diaph*100);
            this.textBox1.Text = this.layerName;
            if ((this.zoom == 1) || (this.zoom == 2) || (this.zoom == 4) || (this.zoom == 8))
            {
                this.comboBox1.Text = this.zoom.ToString();
            }
            else
            {
                this.comboBox1.Items.Add(this.zoom.ToString());
                this.comboBox1.Text = this.zoom.ToString();
            }
            if (this.cansee)
                this.comboBox2.Text = "是";
            else
                this.comboBox2.Text = "否";
            this.textBox1.LostFocus += new EventHandler(textBox1_LostFocus);
            this.comboBox1.TextChanged += new EventHandler(comboBox1_TextChanged);
            this.numericUpDown3.LostFocus += new EventHandler(numericUpDown3_LostFocus);
        }

        void numericUpDown3_LostFocus(object sender, EventArgs e)
        {
            float temp =(float) this.numericUpDown3.Value / 100;
            ((EasyPhoto.EPControl.Layer)this.SubParent.currentPaper).SetDiaph(temp);
        }


        void comboBox1_TextChanged(object sender, EventArgs e)
        {
            float temp = int.Parse(this.comboBox1.Text.Trim());
            float x = this.SubParent.Bounds.Width * (temp - 1) / (2 * temp);
            float y = this.SubParent.Bounds.Height * (temp - 1) / (2 * temp);
            this.SubParent.currentPaper.SetZoom((int)temp, new Point((int)x,(int)y));
        }

        void textBox1_LostFocus(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("画纸名称不能为空！", "错误");
                textBox1.Focus();
                return;
            }
            if (this.textBox1.Text.Trim() == this.SubParent.CurrentStage.PaperName)
            {
                MessageBox.Show("画纸中已存在该名称", "错误");
                textBox1.Text = this.SubParent.currentPaper.PaperName;
                textBox1.Focus();
                return;
            }
            for (int i = 0; i < this.SubParent.LayerArrayList.Count; i++)
            {
                if ((((EasyPhoto.EPControl.Layer)this.SubParent.LayerArrayList[i]).PaperName == textBox1.Text.Trim())&&(((EasyPhoto.EPControl.Layer)this.SubParent.LayerArrayList[i]).PaperName!=this.SubParent.currentPaper.PaperName))
                {
                    MessageBox.Show("图层中已存在该名称", "错误");
                    textBox1.Text = this.SubParent.currentPaper.PaperName;
                    textBox1.Focus();
                    return;
                }
            }

            this.SubParent.currentPaper.PaperName = this.textBox1.Text.Trim();
            this.SubParent.currentPaper.Parent.Text = this.textBox1.Text.Trim();
            this.SubParent.CheckLayerInformaiton();
        }

        void comboBox2_TextChanged(object sender, System.EventArgs e)
        {
            if (this.comboBox2.Text == "是")
                ((EasyPhoto.EPControl.Layer)this.SubParent.currentPaper).CanSee = true;
            else
                ((EasyPhoto.EPControl.Layer)this.SubParent.currentPaper).CanSee = false;
        }

        

        
    }
}
