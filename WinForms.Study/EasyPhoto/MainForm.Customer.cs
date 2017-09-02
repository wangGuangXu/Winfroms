using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyPhoto
{
    public partial class MainForm : Form
    {

        public void UpdateFont(FontFamily fontfamily, int fontsize, FontStyle fontstyle)
        {
            this.CurrentFont = new Font(fontfamily, fontsize, fontstyle,GraphicsUnit.Pixel);
        }

        public void ClearAllLayer(Bitmap image)
        {
            this.LayerArrayList.Clear();
            
            while (this.mainTabControl.Controls.Count > 1)
            {
                this.mainTabControl.Controls.RemoveAt(1);
            }

            this.CurrentStage.arrayList.Clear();
            this.CurrentStage.AddGraphics(GraphicsType.Image, image, this.CurrentStage.StartPosition, image.Width, image.Height);
            this.CurrentStage.RePaint();
            this.CheckLayerInformaiton();
        }

        public void CheckLayerInformaiton()
        {
            this.treeView1.TopNode.Nodes.Clear();
            if (this.CurrentStage == null)
            {
                this.treeView1.Visible = false;
                this.button1.Enabled = false;
                this.button2.Enabled = false;
                this.button3.Enabled = false;
                this.button4.Enabled = false;
            }
            else
            {
                this.treeView1.Visible = true;
                this.treeView1.TopNode.Text = this.CurrentStage.PaperName;
                this.treeView1.Name = this.currentPaper.PaperName;
                if (this.LayerArrayList.Count == 0)
                {
                    this.treeView1.TopNode.BackColor = Color.Red;
                    this.treeView1.TopNode.Checked = true;
                    this.treeView1.TopNode.Text += "......当前图层";
                    this.button1.Enabled = false;
                    this.button2.Enabled = false;
                    this.button3.Enabled = false;
                    this.button4.Enabled = false;
                }
                else
                {
                    
                    for (int i = 0; i < this.LayerArrayList.Count; i++)
                    {
                        TreeNode temp = new TreeNode(((EasyPhoto.EPControl.Layer)this.LayerArrayList[i]).PaperName);
                        if (!((EasyPhoto.EPControl.Layer)this.LayerArrayList[i]).CanSee)
                        {
                            temp.BackColor = Color.Gray;
                            temp.Text += "(隐藏)";
                        }
                        else
                            temp.BackColor = Color.White;
                        temp.Checked = false;
                        temp.Name = ((EasyPhoto.EPControl.Layer)this.LayerArrayList[i]).PaperName;
                        this.treeView1.TopNode.Nodes.Add(temp);
                    }
                    if (this.currentPaper.GetType().ToString() == "EasyPhoto.EPControl.Stage")
                    {
                        this.treeView1.TopNode.BackColor = Color.Red;
                        this.treeView1.TopNode.Checked = true;
                        this.treeView1.TopNode.Text += "......当前图层";
                        this.button1.Enabled = false;
                        this.button2.Enabled = false;
                        this.button3.Enabled = false;
                        this.button4.Enabled = false;
                    }
                    else
                    {
                        this.treeView1.TopNode.BackColor = Color.White;
                        foreach (TreeNode temp in this.treeView1.TopNode.Nodes)
                        {
                            if (this.currentPaper.PaperName == temp.Name)
                            {
                                temp.BackColor = Color.Red;
                                temp.Checked = true;
                                temp.Text += "......当前图层";
                                if (temp.Index == 0)
                                {
                                    this.button1.Enabled = false;
                                }
                                else
                                    this.button1.Enabled = true;
                                if (temp.Index == this.treeView1.TopNode.Nodes.Count - 1)
                                    this.button2.Enabled = false;
                                else
                                    this.button2.Enabled = true;
                                this.button3.Enabled = true;
                                this.button4.Enabled = true;
                                break;
                            }
                        }
                    }
                    this.treeView1.ExpandAll();
                }
            }
        }

        public Bitmap GetFinalImage()
        {
            return (Bitmap)this.CurrentStage.ExportCustomImage();
        }

        public void LoadPaper(SerialClass serialclass)
        {
            if (serialclass == null)
            {
                MessageBox.Show("加载文件失败!", "提示");
                return;
            }
            else
            {
                while (this.mainTabControl.Controls.Count > 1)
                    this.mainTabControl.Controls.RemoveAt(1);
                this.baseTabPage.Controls.Clear();
                this.MainForm_Load(this, null);
                foreach (ToolStripButton tsbutton in this.mainToolStrip.Items)
                {
                    tsbutton.CheckState = CheckState.Unchecked;
                }
                this.defaultTabPageNum = serialclass.defaultlyaernamenum;
                this.CurrentStage = new EasyPhoto.EPControl.Stage(serialclass.SerialStage, this);
                this.baseTabPage.Controls.Add(CurrentStage);
                this.baseTabPage.Text = CurrentStage.PaperName;
                this.currentPaper = CurrentStage;
                if (serialclass.SerialLayers != null)
                {
                    for (int i = 0; i < serialclass.SerialLayers.Length; i++)
                    {
                        EasyPhoto.EPControl.Layer temp = new EasyPhoto.EPControl.Layer(serialclass.SerialLayers[i], this);
                        this.LayerArrayList.Add(temp);
                    }
                }
                this.mainTabControl.Visible = true;
                this.CheckLayerInformaiton();
            }
        }

        public void LoadFile(string path)
        {
            EasyPhoto.SerialClass serialclass = null;
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
            try
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                serialclass = (EasyPhoto.SerialClass)formatter.Deserialize(fs);
                serialclass.DeSerial();
                this.LoadPaper(serialclass);
            }
            catch (Exception f)
            {
                //MessageBox.Show(f.Message, "");
            }
            finally
            {
                fs.Close();
            }
        }

    }
}