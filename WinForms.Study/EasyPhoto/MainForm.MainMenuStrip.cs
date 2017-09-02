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
        #region 文件菜单


        private void 文件ToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                this.保存画纸ToolStripMenuItem.Enabled = false;
                this.重置ToolStripMenuItem.Enabled = false;
                this.导出ToolStripMenuItem.Enabled = false;
                this.预览ToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.保存画纸ToolStripMenuItem.Enabled = true;
                this.重置ToolStripMenuItem.Enabled = true;
                this.导出ToolStripMenuItem.Enabled = true;
                this.预览ToolStripMenuItem.Enabled = true;
            }
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage != null)
            {
                MessageBox.Show("当前已存在画纸", "提示");
                return;
            }
            Dialog.BuildStageDlg buildstagedlg = new EasyPhoto.Dialog.BuildStageDlg();
            buildstagedlg.ShowDialog();
            if (buildstagedlg.BuildFlag)
            {
                this.mainTabControl.Visible = true;
                System.Drawing.Size tempsize = this.baseTabPage.ClientSize;
                if (buildstagedlg.StageWidth + 50 > this.baseTabPage.ClientSize.Width)
                {
                    tempsize.Width = buildstagedlg.StageWidth + 50;
                }
                if (buildstagedlg.StageHeight + 50 > this.baseTabPage.ClientSize.Width)
                {
                    tempsize.Height = buildstagedlg.StageHeight + 50;
                }
                EPControl.Stage newstage = new EasyPhoto.EPControl.Stage(this,buildstagedlg.StageName, tempsize, buildstagedlg.StageWidth, buildstagedlg.StageHeight, buildstagedlg.StageColor);
                this.baseTabPage.Text = newstage.PaperName;
                this.baseTabPage.Controls.Add(newstage);
                this.currentPaper = newstage;
                this.CurrentStage = newstage;
                this.mainTabControl.Visible = true;
                this.treeView1.Visible = true;
                this.CheckLayerInformaiton();
            }
            else
            {
                buildstagedlg.Dispose();
                return;
            }
        }

        private void 导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog.ImportImageDlg importDlg = new EasyPhoto.Dialog.ImportImageDlg();
            importDlg.ShowDialog();
            if (!importDlg.IsImport)
                return;
            else
            {
                if (this.CurrentStage == null)
                {
                    Image tempImage = importDlg.ImportImage;
                    if (this.currentPaper == null)
                    {
                        this.mainTabControl.Visible = true;
                        System.Drawing.Size tempsize = this.baseTabPage.ClientSize;
                        if (importDlg.ImportSize.Width + 50 > this.baseTabPage.ClientSize.Width)
                        {
                            tempsize.Width = importDlg.ImportSize.Width + 50;
                        }
                        if (importDlg.ImportSize.Height + 50 > this.baseTabPage.ClientSize.Width)
                        {
                            tempsize.Height = importDlg.ImportSize.Height + 50;
                        }
                        EPControl.Stage newstage = new EasyPhoto.EPControl.Stage(this, "未命名", tempsize, importDlg.ImportSize.Width, importDlg.ImportSize.Height, Color.White);
                        this.baseTabPage.Text = newstage.PaperName;
                        newstage.AddGraphics(GraphicsType.Image, tempImage, newstage.StartPosition, importDlg.ImportSize.Width, importDlg.ImportSize.Height);
                        this.baseTabPage.Controls.Add(newstage);
                        this.currentPaper = newstage;
                        this.CurrentStage = newstage;
                        this.mainTabControl.Visible = true;
                        this.treeView1.TopNode.Text = this.currentPaper.PaperName;
                        this.treeView1.Visible = true;
                        this.CheckLayerInformaiton();
                    }
                }
                else
                {
                    System.Drawing.Size tempsize = new Size();
                    if ((importDlg.ImportSize.Width > this.currentPaper.Width) || (importDlg.ImportSize.Height > this.currentPaper.Height))
                    {
                        float fw = (float)importDlg.ImportSize.Width / (float)this.currentPaper.Width;
                        float fh = (float)importDlg.ImportSize.Height / (float)this.currentPaper.Height;
                        if (fw > fh)
                        {

                            tempsize.Width = this.currentPaper.Width;
                            tempsize.Height = (int)((float)importDlg.ImportSize.Height / fw);
                        }
                        else
                        {
                            tempsize.Height = this.currentPaper.Height;
                            tempsize.Width = (int)((float)importDlg.ImportSize.Width / fh);
                        }
                    }
                    else
                    {
                        tempsize.Width = importDlg.ImportSize.Width;
                        tempsize.Height = importDlg.ImportSize.Height;
                    }
                    Point temppoint = new Point();
                    temppoint.X = (this.currentPaper.Width - tempsize.Width) / 2;
                    temppoint.Y = (this.currentPaper.Height - tempsize.Height) / 2;
                    this.currentPaper.AddGraphics(EasyPhoto.GraphicsType.Image, importDlg.ImportImage, temppoint, tempsize.Width, tempsize.Height);
                }
            }
        }

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("未建立任何图层, 无法导出", "错误");
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JPG图像文件|*.jpg|位图文件|*.bmp";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.CurrentStage.ExportCustomImage().Save(sfd.FileName);
                sfd.Dispose();
            }
        }  

        

        


        private void 背景区域截图ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            Dialog.ScreenForm screenform = new EasyPhoto.Dialog.ScreenForm();
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            screenform.ShowDialog();
            if (screenform.IsSelect)
            {
                Image tempImage;
                tempImage = screenform.FinalImage;
                if (this.currentPaper == null)
                {
                    
                    System.Drawing.Size tempsize = this.baseTabPage.ClientSize;
                    if (tempImage.Width + 50 > this.baseTabPage.ClientSize.Width)
                    {
                        tempsize.Width = tempImage.Width + 50;
                    }
                    if (tempImage.Height + 50 > this.baseTabPage.ClientSize.Width)
                    {
                        tempsize.Height = tempImage.Height + 50;
                    }
                    EPControl.Stage newstage = new EasyPhoto.EPControl.Stage(this, "未命名", tempsize, tempImage.Width, tempImage.Height, Color.White);
                    this.baseTabPage.Text = newstage.PaperName;
                    newstage.AddGraphics(GraphicsType.Image, tempImage, newstage.StartPosition, tempImage.Width, tempImage.Height);
                    this.baseTabPage.Controls.Add(newstage);
                    this.currentPaper = newstage;
                    this.CurrentStage = newstage;
                    this.mainTabControl.Visible = true;
                    this.treeView1.TopNode.Text = this.currentPaper.PaperName;
                    this.treeView1.Visible = true;
                    this.CheckLayerInformaiton();
                }
                else
                {
                    this.currentPaper.AddGraphics(GraphicsType.Image, tempImage, this.currentPaper.StartPosition, tempImage.Width, tempImage.Height);
                }
            }
            else
                return;
        }

        private void 当前区域截图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog.ScreenForm screenform = new EasyPhoto.Dialog.ScreenForm();
            screenform.ShowDialog();
            if (screenform.IsSelect)
            {
                Image tempImage;
                tempImage = screenform.FinalImage;
                if (this.currentPaper == null)
                {
                    this.mainTabControl.Visible = true;
                    System.Drawing.Size tempsize = this.baseTabPage.ClientSize;
                    if (tempImage.Width + 50 > this.baseTabPage.ClientSize.Width)
                    {
                        tempsize.Width = tempImage.Width + 50;
                    }
                    if (tempImage.Height + 50 > this.baseTabPage.ClientSize.Width)
                    {
                        tempsize.Height = tempImage.Height + 50;
                    }
                    EPControl.Stage newstage = new EasyPhoto.EPControl.Stage(this, "未命名", tempsize, tempImage.Width, tempImage.Height, Color.White);
                    this.baseTabPage.Text = newstage.PaperName;
                    newstage.AddGraphics(GraphicsType.Image, tempImage, newstage.StartPosition, tempImage.Width, tempImage.Height);
                    this.baseTabPage.Controls.Add(newstage);
                    this.currentPaper = newstage;
                    this.CurrentStage = newstage;
                    this.mainTabControl.Visible = true;
                    this.treeView1.TopNode.Text = this.currentPaper.PaperName;
                    this.treeView1.Visible = true;
                    this.CheckLayerInformaiton();
                }
                else
                {
                    this.currentPaper.AddGraphics(GraphicsType.Image, tempImage, this.currentPaper.StartPosition, tempImage.Width, tempImage.Height);
                }
            }
            else
                return;

        }

        private void 背景区域截图ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            Dialog.ScreenForm screenform = new EasyPhoto.Dialog.ScreenForm();
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            screenform.ShowDialog();
            if (screenform.IsSelect)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "BMP文件|*.bmp";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    screenform.FinalImage.Save(sfd.FileName);
                }
                sfd.Dispose();
            }

        }

        private void 当前区域截图ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Dialog.ScreenForm screenform = new EasyPhoto.Dialog.ScreenForm();
            screenform.ShowDialog();
            if (screenform.IsSelect)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "BMP文件|*.bmp";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    screenform.FinalImage.Save(sfd.FileName);
                    sfd.Dispose();
                }
            }
        }

        private void 重置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while (this.mainTabControl.Controls.Count > 1)
                this.mainTabControl.Controls.RemoveAt(1);
            this.baseTabPage.Controls.Clear();
            this.treeView1.Visible = false;
            this.CurrentStage.Dispose();
            this.MainForm_Load(this, null);
            foreach (ToolStripButton tsbutton in this.mainToolStrip.Items)
            {
                tsbutton.CheckState = CheckState.Unchecked;
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 预览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog.Preview preview = new EasyPhoto.Dialog.Preview();
            preview.LoadImage(this.CurrentStage.ExportCustomImage());
            preview.ShowDialog();
        }

        private void 保存画纸ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("当前不存在任何画纸", "提示");
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "保存为";
            sfd.Filter = "EasyPhoto文件|*.hdk";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream fs = new System.IO.FileStream(sfd.FileName, System.IO.FileMode.Create);
                EasyPhoto.SerialClass serialclass = new SerialClass(this.CurrentStage, this.LayerArrayList);
                serialclass.SerialBegin();
                try
                {
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    formatter.Serialize(fs, serialclass);
                }
                catch (Exception f)
                {
                    MessageBox.Show(f.Message, "");
                }
                finally
                {
                    fs.Close();

                }
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "EasyPhoto文件|*.hdk";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                EasyPhoto.SerialClass serialclass = null;
                System.IO.FileStream fs = new System.IO.FileStream(ofd.FileName, System.IO.FileMode.Open);
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

        #endregion 

        #region 图层操作

        private void 图层操作ToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (this.currentPaper == null)
            {
                this.新建图层ToolStripMenuItem.Enabled = false;
                this.删除图层ToolStripMenuItem.Enabled = false;
                this.复制图层ToolStripMenuItem.Enabled = false;
                this.粘贴图层ToolStripMenuItem.Enabled = false;
                this.打开图层ToolStripMenuItem.Enabled = false;
                this.关闭本图层ToolStripMenuItem.Enabled = false;
            }
            else
            {
                if (this.currentPaper.GetType().ToString() == "EasyPhoto.EPControl.Stage")
                {
                    this.新建图层ToolStripMenuItem.Enabled = true;
                    this.删除图层ToolStripMenuItem.Enabled = false;
                    this.复制图层ToolStripMenuItem.Enabled = false;
                    this.粘贴图层ToolStripMenuItem.Enabled = false;
                    this.打开图层ToolStripMenuItem.Enabled = true;
                    this.关闭本图层ToolStripMenuItem.Enabled = false;
                }
                else
                {
                    this.新建图层ToolStripMenuItem.Enabled = true;
                    this.删除图层ToolStripMenuItem.Enabled = true;
                    this.复制图层ToolStripMenuItem.Enabled = true;
                    this.打开图层ToolStripMenuItem.Enabled = true;
                    this.关闭本图层ToolStripMenuItem.Enabled = true;
                    if ((this.TempLayer == null) || (!this.IsCopyLayer))
                        this.粘贴图层ToolStripMenuItem.Enabled = false;
                    else
                        this.粘贴图层ToolStripMenuItem.Enabled = true;
                }
                this.打开图层ToolStripMenuItem.DropDownItems.Clear();
                if (this.LayerArrayList.Count == 0)
                {
                    this.打开图层ToolStripMenuItem.Enabled = false;
                    return;
                }
                else
                {
                    this.打开图层ToolStripMenuItem.Enabled = true;
                    System.Windows.Forms.ToolStripMenuItem tsmi;
                    for (int i = 0; i < this.LayerArrayList.Count; i++)
                    {
                        tsmi = new ToolStripMenuItem();
                        tsmi.Name = ((EasyPhoto.EPControl.Layer)this.LayerArrayList[i]).PaperName;
                        tsmi.Text = tsmi.Name;
                        foreach (TabPage tp in this.mainTabControl.Controls)
                        {
                            if (tp.Text == tsmi.Name)
                                tsmi.Image = global::EasyPhoto.Properties.Resources.gou;
                        }
                        this.打开图层ToolStripMenuItem.DropDownItems.Add(tsmi);
                    }
                }
                    
            }
        }

        void 打开图层ToolStripMenuItem_DropDownItemClicked(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Image == null)
            {
                for (int i = 0; i < this.LayerArrayList.Count; i++)
                {
                    if (e.ClickedItem.Text == ((EPControl.Layer)this.LayerArrayList[i]).PaperName)
                    {
                        EPControl.Layer temp;
                        temp = (EPControl.Layer)this.LayerArrayList[i];
                        System.Windows.Forms.TabPage newtabpage = new System.Windows.Forms.TabPage();
                        newtabpage.Text = temp.PaperName;
                        newtabpage.Name = temp.PaperName;
                        this.mainTabControl.Controls.Add(newtabpage);
                        newtabpage.Controls.Add(temp);
                        this.currentPaper = temp;
                        this.mainTabControl.SelectedTab = newtabpage;
                        break;
                    }
                }
            }
            else
            {
                foreach (TabPage tb in this.mainTabControl.Controls)
                {
                    if (tb.Text == e.ClickedItem.Text)
                    {
                        this.mainTabControl.SelectedTab = tb;
                        break;
                    }
                }
            }
        }

        private void 新建图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未新建画纸！", "错误");
                return;
            }
            this.defaultTabPageNum++;
            EPControl.Layer newlayer = new EasyPhoto.EPControl.Layer(this, this.defaultTabPageName + this.defaultTabPageNum.ToString(), this.CurrentStage.Size, this.CurrentStage.BackgroundWidth, this.CurrentStage.BackgroundHeight, this.CurrentStage.BackgroundColor);
            this.LayerArrayList.Add(newlayer);
            System.Windows.Forms.TabPage newtabpage = new System.Windows.Forms.TabPage();
            newtabpage.Name = newlayer.PaperName;
            newtabpage.Text = newtabpage.Name;
            this.mainTabControl.Controls.Add(newtabpage);
            newtabpage.Controls.Add(newlayer);
            this.currentPaper = newlayer;
            this.mainTabControl.SelectedTab = newtabpage;
        }

        private void 删除图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentPaper.GetType().ToString() == "EasyPhoto.EPControl.Stage")
            {
                MessageBox.Show("无法删除基层", "错误！");
                return;
            }
            this.LayerArrayList.Remove(this.currentPaper);
            this.mainTabControl.Controls.Remove(this.mainTabControl.SelectedTab);
            
        }

        private void 复制图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentPaper == null)
            {
                MessageBox.Show("未建立任何图层", " 错误");
                return;
            }
            if (this.currentPaper.GetType().ToString() == "EasyPhoto.EPControl.Stage")
            {
                MessageBox.Show("无法复制基层", "错误！");
                return;
            }
            else
            {
                this.TempLayer = (EPControl.Layer)this.currentPaper;
                this.IsCopyLayer = true;
            }
            
            
        }

        private void 粘贴图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentPaper == null)
            {
                MessageBox.Show("未建立任何图层", " 错误");
                return;
            }
            if ((this.TempLayer != null) && (this.IsCopyLayer))
            {
                this.defaultTabPageNum++;
                EPControl.Layer newlayer = TempLayer.Copylayer();
                newlayer.PaperName = this.defaultTabPageName + this.defaultTabPageNum.ToString();
                this.LayerArrayList.Add(newlayer);
                System.Windows.Forms.TabPage newtabpage = new System.Windows.Forms.TabPage();
                newtabpage.Name = newlayer.PaperName;
                newtabpage.Text = newtabpage.Name;
                this.mainTabControl.Controls.Add(newtabpage);
                newtabpage.Controls.Add(newlayer);
                this.currentPaper = newlayer;
                this.mainTabControl.SelectedTab = newtabpage;
                
            }
            else
            {
                MessageBox.Show("未复制任何图层", " 错误");
                return;
            }
        }

        private void 关闭本图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentPaper.GetType().ToString() == "EasyPhoto.EPControl.Stage")
            {
                MessageBox.Show("不能关闭基层", "错误");
                return;
            }
            else
            {
                this.mainTabControl.Controls.Remove(this.currentPaper.Parent);
            }
        }
        #endregion

        #region 元件操作

        private void 元件操作ToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (this.currentPaper == null)
            {
                this.元件复制ToolStripMenuItem.Enabled = false;
                this.元件剪切ToolStripMenuItem.Enabled = false;
                this.元件删除ToolStripMenuItem.Enabled = false;
                this.元件粘贴ToolStripMenuItem.Enabled = false;
            }
            else
            {
                if (this.currentPaper.ActiveNum == -1)
                {
                    this.元件复制ToolStripMenuItem.Enabled = false;
                    this.元件剪切ToolStripMenuItem.Enabled = false;
                    this.元件删除ToolStripMenuItem.Enabled = false;
                }
                else
                {
                    this.元件复制ToolStripMenuItem.Enabled = true;
                    this.元件剪切ToolStripMenuItem.Enabled = true;
                    this.元件删除ToolStripMenuItem.Enabled = true;
                }
                if ((this.TempCell == null) || (!this.IsCopyCell))
                    this.元件粘贴ToolStripMenuItem.Enabled = false;
                else
                    this.元件粘贴ToolStripMenuItem.Enabled = true;
            }
        } 

        private void 元件复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentPaper == null)
            {
                MessageBox.Show("未建立任何图层", " 错误");
                return;
            }
            if (this.currentPaper.ActiveNum == -1)
            {
                MessageBox.Show("未选定元件", "提示");
                return;
            }
            else
            {
                this.TempCell = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CopyCell();
                this.IsCopyCell = true;
            }
        }

        private void 元件剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentPaper == null)
            {
                MessageBox.Show("未建立任何图层", " 错误");
                return;
            }
            if (this.currentPaper.ActiveNum == -1)
            {
                MessageBox.Show("未选定元件", "提示");
                return;
            }
            else
            {
                this.TempCell = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CopyCell();
                this.currentPaper.RemoveCell();
                this.IsCopyCell = true;
            }
        }

        private void 元件删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentPaper == null)
            {
                MessageBox.Show("未建立任何图层", " 错误");
                return;
            }
            if (this.currentPaper.ActiveNum == -1)
            {
                MessageBox.Show("未选定元件", "提示");
                return;
            }
            else
            {
                this.currentPaper.RemoveCell();
                this.IsMouseClick = false;
                this.IsMouseDown = false;
                this.attributeInfoTab.Controls.Clear();
            }
        }

        private void 元件粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentPaper == null)
            {
                MessageBox.Show("未建立任何图层", " 错误");
                return;
            }
            if ((this.IsCopyCell)&&(this.TempCell!=null))
            {
                CustomClass.Cell temp = new EasyPhoto.CustomClass.Cell(this.TempCell);
                this.currentPaper.arrayList.Add(temp);
                this.currentPaper.RePaint();
                this.IsMouseClick = false;
                this.IsMouseDown = false;
            }
            else
            {
                MessageBox.Show("未复制任何元件", " 错误");
                return;
            }
        }
        #endregion

        #region 色彩处理

        private void 色彩处理ToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if ((this.currentPaper == null) || (this.currentPaper.GetType().ToString() != "EasyPhoto.EPControl.Stage"))
            {
                this.色彩平衡ToolStripMenuItem.Enabled = false;
                this.亮度ToolStripMenuItem.Enabled = false;
                this.对比度ToolStripMenuItem.Enabled = false;
                this.饱和度ToolStripMenuItem.Enabled = false;
                this.toolStripMenuItem1.Enabled = false;
                this.色彩平衡ToolStripMenuItem.Enabled = false;
                this.gamma矫正ToolStripMenuItem.Enabled = false;
                this.灰度ToolStripMenuItem.Enabled = false;
                this.伪彩色ToolStripMenuItem.Enabled = false;
                this.轮换通道ToolStripMenuItem.Enabled = false;
                this.负色ToolStripMenuItem.Enabled = false;
                this.提取通道ToolStripMenuItem.Enabled = false;
                this.过滤通道ToolStripMenuItem.Enabled = false;
                this.亮度映射ToolStripMenuItem.Enabled = false;
                this.均衡化ToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.色彩平衡ToolStripMenuItem.Enabled = true;
                this.亮度ToolStripMenuItem.Enabled = true;
                this.对比度ToolStripMenuItem.Enabled = true;
                this.饱和度ToolStripMenuItem.Enabled = true;
                this.toolStripMenuItem1.Enabled = true;
                this.色彩平衡ToolStripMenuItem.Enabled = true;
                this.gamma矫正ToolStripMenuItem.Enabled = true;
                this.灰度ToolStripMenuItem.Enabled = true;
                this.伪彩色ToolStripMenuItem.Enabled = true;
                this.轮换通道ToolStripMenuItem.Enabled = true;
                this.负色ToolStripMenuItem.Enabled = true;
                this.提取通道ToolStripMenuItem.Enabled = true;
                this.过滤通道ToolStripMenuItem.Enabled = true;
                this.亮度映射ToolStripMenuItem.Enabled = true;
                this.均衡化ToolStripMenuItem.Enabled = true;
            }
        }

        private void 色彩平衡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            ColorProcess.ColorBalanceDialog colorbalancedialog = new EasyPhoto.ColorProcess.ColorBalanceDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            colorbalancedialog.ShowDialog();
            if (colorbalancedialog.IsFinish)
            {
                this.ClearAllLayer(colorbalancedialog.finalImage);
            }
        }



        private void 亮度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }

            ColorProcess.DegreeDialog dlg = new EasyPhoto.ColorProcess.DegreeDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            dlg.Text = "亮度";
            dlg.Description = "亮度调节";
            dlg.Minimum = -255;
            dlg.Maximum = 255;
            dlg.Degree = 0;
            dlg.Support = ColorProcess.DegreeDialog.SupportMethod.Brightness;
            dlg.ShowDialog();
            if (dlg.IsFinish)
            {
                this.ClearAllLayer(dlg.FinalImage);
            }
        }

        private void 对比度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }

            ColorProcess.DegreeDialog dlg = new EasyPhoto.ColorProcess.DegreeDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            dlg.Text = "对比度";
            dlg.Description = "对比度调节";
            dlg.Minimum = -255;
            dlg.Maximum = 255;
            dlg.Degree = 0;
            dlg.Support = ColorProcess.DegreeDialog.SupportMethod.Contrast;
            dlg.ShowDialog();
            if (dlg.IsFinish)
            {
                this.ClearAllLayer(dlg.FinalImage);
            }
        }

        private void 饱和度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            ColorProcess.HslDialog hd = new EasyPhoto.ColorProcess.HslDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            hd.ShowDialog();
            if (hd.IsFinish)
            {
                this.ClearAllLayer(hd.FinalImage);
            }
        }

        private void gamma矫正ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            ColorProcess.GammaCorrectDialog hd = new EasyPhoto.ColorProcess.GammaCorrectDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            hd.ShowDialog();
            if (hd.IsFinish)
            {
                this.ClearAllLayer(hd.FinalImage);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }

            ColorProcess.DegreeDialog dlg = new EasyPhoto.ColorProcess.DegreeDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            dlg.Text = "阀值";
            dlg.Description = "阀值调节";
            dlg.Support = ColorProcess.DegreeDialog.SupportMethod.Thresholding;
            dlg.ShowDialog();
            if (dlg.IsFinish)
            {
                this.ClearAllLayer(dlg.FinalImage);
            }
        }

        private void 灰度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            ColorProcess.GrayDialog hd = new EasyPhoto.ColorProcess.GrayDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            hd.ShowDialog();
            if (hd.IsFinish)
            {
                this.ClearAllLayer(hd.FinalImage);
            }
        }

        private void 负色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 0);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 伪彩色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            ColorProcess.PseudoColorDialog hd = new EasyPhoto.ColorProcess.PseudoColorDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            hd.ShowDialog();
            if (hd.IsFinish)
            {
                this.ClearAllLayer(hd.FinalImage);
            }
        }

        private void 轮换通道ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 1);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }



        private void 红色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 2);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 绿色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 3);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 蓝色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 4);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 红色ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 5);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 绿色ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 6);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 蓝色ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 7);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 青色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 8);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 品红ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 9);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 黄色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 10);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 亮度映射ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            ColorProcess.MappingDialog hd = new EasyPhoto.ColorProcess.MappingDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            hd.ShowDialog();
            if (hd.IsFinish)
            {
                this.ClearAllLayer(hd.FinalImage);
            }
        }

        private void 均衡化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 11);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }
        #endregion

        #region 特效处理

        void 特效处理ToolStripMenuItem_DropDownOpening(object sender, System.EventArgs e)
        {
            if ((this.currentPaper == null) || (this.currentPaper.GetType().ToString() != "EasyPhoto.EPControl.Stage"))
            {
                this.模糊ToolStripMenuItem.Enabled = false;
                this.锐化ToolStripMenuItem.Enabled = false;
                this.杂点ToolStripMenuItem.Enabled = false;
                this.浮雕ToolStripMenuItem.Enabled = false;
                this.艺术ToolStripMenuItem.Enabled = false;
                this.扭曲ToolStripMenuItem.Enabled = false;
                this.风格化ToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.模糊ToolStripMenuItem.Enabled = true;
                this.锐化ToolStripMenuItem.Enabled = true;
                this.杂点ToolStripMenuItem.Enabled = true;
                this.浮雕ToolStripMenuItem.Enabled = true;
                this.艺术ToolStripMenuItem.Enabled = true;
                this.扭曲ToolStripMenuItem.Enabled = true;
                this.风格化ToolStripMenuItem.Enabled = true;
            }
        }

        private void 平滑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 12);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 高斯模糊ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 13);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 运动模糊ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            ColorProcess.MotionBlurDialog colorbalancedialog = new EasyPhoto.ColorProcess.MotionBlurDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            colorbalancedialog.ShowDialog();
            if (colorbalancedialog.IsFinish)
            {
                this.ClearAllLayer(colorbalancedialog.FinalImage);
            }
        }

        private void 径向模糊ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            ColorProcess.AngleDialog colorbalancedialog = new EasyPhoto.ColorProcess.AngleDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            colorbalancedialog.Text = "径向模糊";
            colorbalancedialog.Support = ColorProcess.AngleDialog.SupportMethod.RadialBlur;
            colorbalancedialog.ShowDialog();
            if (colorbalancedialog.IsFinish)
            {
                this.ClearAllLayer(colorbalancedialog.FinalImage);
            }
        }

        private void 锐化ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 14);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 加强锐化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 15);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 自由锐化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }

            ColorProcess.DegreeDialog dlg = new EasyPhoto.ColorProcess.DegreeDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            dlg.Text = "锐化";
            dlg.Description = "锐化度调节";
            dlg.Minimum = 1;
            dlg.Maximum = 100;
            dlg.Degree = 10;
            dlg.Support = ColorProcess.DegreeDialog.SupportMethod.Sharpen;
            dlg.ShowDialog();
            if (dlg.IsFinish)
            {
                this.ClearAllLayer(dlg.FinalImage);
            }
        }

        private void 锐化模版ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }

            ColorProcess.DegreeDialog dlg = new EasyPhoto.ColorProcess.DegreeDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            dlg.Text = "锐化模版";
            dlg.Description = "锐化度调节";
            dlg.Minimum = 1;
            dlg.Maximum = 100;
            dlg.Degree = 10;
            dlg.Support = ColorProcess.DegreeDialog.SupportMethod.UnsharpMask;
            dlg.ShowDialog();
            if (dlg.IsFinish)
            {
                this.ClearAllLayer(dlg.FinalImage);
            }
        }

        private void 调和浮雕ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 16);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 八方向浮雕ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            ColorProcess.DirectionDialog colorbalancedialog = new EasyPhoto.ColorProcess.DirectionDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            colorbalancedialog.ShowDialog();
            if (colorbalancedialog.IsFinish)
            {
                this.ClearAllLayer(colorbalancedialog.FinalImage);
            }
        }

        private void 灰度浮雕ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            ColorProcess.AngleDialog colorbalancedialog = new EasyPhoto.ColorProcess.AngleDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            colorbalancedialog.Text = "灰度浮雕";
            colorbalancedialog.Support = ColorProcess.AngleDialog.SupportMethod.Emboss;
            colorbalancedialog.ShowDialog();
            if (colorbalancedialog.IsFinish)
            {
                this.ClearAllLayer(colorbalancedialog.FinalImage);
            }
        }

        private void 彩色浮雕ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            ColorProcess.AngleDialog colorbalancedialog = new EasyPhoto.ColorProcess.AngleDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            colorbalancedialog.Text = "彩色浮雕";
            colorbalancedialog.Support = ColorProcess.AngleDialog.SupportMethod.Relief;
            colorbalancedialog.ShowDialog();
            if (colorbalancedialog.IsFinish)
            {
                this.ClearAllLayer(colorbalancedialog.FinalImage);
            }
        }

        private void 新增杂点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }

            ColorProcess.DegreeDialog dlg = new EasyPhoto.ColorProcess.DegreeDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            dlg.Text = "新增杂点";
            dlg.Maximum = 255;
            dlg.Degree = 20;
            dlg.Support = ColorProcess.DegreeDialog.SupportMethod.AddNoise;
            dlg.ShowDialog();
            if (dlg.IsFinish)
            {
                this.ClearAllLayer(dlg.FinalImage);
            }
        }

        private void 雪花ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }

            ColorProcess.DegreeDialog dlg = new EasyPhoto.ColorProcess.DegreeDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            dlg.Text = "雪花杂点";
            dlg.Description = "雪花抛撒概率";
            dlg.Maximum = 100;
            dlg.Degree = 5;
            dlg.Support = ColorProcess.DegreeDialog.SupportMethod.Sprinkle;
            dlg.ShowDialog();
            if (dlg.IsFinish)
            {
                this.ClearAllLayer(dlg.FinalImage);
            }
        }

        private void 剪纸ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            ColorProcess.PaperCutDialog colorbalancedialog = new EasyPhoto.ColorProcess.PaperCutDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            colorbalancedialog.ShowDialog();
            if (colorbalancedialog.IsFinish)
            {
                this.ClearAllLayer(colorbalancedialog.FinalImage);
            }
        }

        private void 素描ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            ColorProcess.SketchDialog colorbalancedialog = new EasyPhoto.ColorProcess.SketchDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            colorbalancedialog.ShowDialog();
            if (colorbalancedialog.IsFinish)
            {
                this.ClearAllLayer(colorbalancedialog.FinalImage);
            }
        }

        private void 连环画ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 17);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 碧绿ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 18);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 棕褐ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 19);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 冰冻ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 20);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 熔铸ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 21);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 暗调ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 22);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 对调ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 23);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 怪调ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 24);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 挤压ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }

            ColorProcess.DegreeDialog dlg = new EasyPhoto.ColorProcess.DegreeDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            dlg.Text = "挤压";
            dlg.Maximum = 32;
            dlg.Minimum = 1;
            dlg.Degree = 15;
            dlg.Support = ColorProcess.DegreeDialog.SupportMethod.Pinch;
            dlg.ShowDialog();
            if (dlg.IsFinish)
            {
                this.ClearAllLayer(dlg.FinalImage);
            }
        }

        private void 球面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 25);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 漩涡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }

            ColorProcess.DegreeDialog dlg = new EasyPhoto.ColorProcess.DegreeDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            dlg.Text = "漩涡";
            dlg.Maximum = 100;
            dlg.Minimum = 1;
            dlg.Degree = 50;
            dlg.Support = ColorProcess.DegreeDialog.SupportMethod.Swirl;
            dlg.ShowDialog();
            if (dlg.IsFinish)
            {
                this.ClearAllLayer(dlg.FinalImage);
            }
        }

        private void 波浪ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }

            ColorProcess.DegreeDialog dlg = new EasyPhoto.ColorProcess.DegreeDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            dlg.Text = "波浪";
            dlg.Maximum = 32;
            dlg.Minimum = 1;
            dlg.Degree = 15;
            dlg.Support = ColorProcess.DegreeDialog.SupportMethod.Wave;
            dlg.ShowDialog();
            if (dlg.IsFinish)
            {
                this.ClearAllLayer(dlg.FinalImage);
            }
        }

        private void 摩尔纹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }

            ColorProcess.DegreeDialog dlg = new EasyPhoto.ColorProcess.DegreeDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            dlg.Text = "摩尔纹";
            dlg.Maximum = 16;
            dlg.Minimum = 1;
            dlg.Degree = 3;
            dlg.Support = ColorProcess.DegreeDialog.SupportMethod.MoireFringe;
            dlg.ShowDialog();
            if (dlg.IsFinish)
            {
                this.ClearAllLayer(dlg.FinalImage);
            }
        }

        private void 扩散ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }

            ColorProcess.DegreeDialog dlg = new EasyPhoto.ColorProcess.DegreeDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            dlg.Text = "扩散";
            dlg.Maximum = 32;
            dlg.Minimum = 1;
            dlg.Degree = 3;
            dlg.Support = ColorProcess.DegreeDialog.SupportMethod.Diffuse;
            dlg.ShowDialog();
            if (dlg.IsFinish)
            {
                this.ClearAllLayer(dlg.FinalImage);
            }
        }

        private void 查找边缘ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            ColorProcess.AngleDialog colorbalancedialog = new EasyPhoto.ColorProcess.AngleDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            colorbalancedialog.Text = "查找边缘";
            colorbalancedialog.Support = ColorProcess.AngleDialog.SupportMethod.FindEdge;
            colorbalancedialog.ShowDialog();
            if (colorbalancedialog.IsFinish)
            {
                this.ClearAllLayer(colorbalancedialog.FinalImage);
            }
        }

        private void 照亮边缘ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 26);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }

        private void 灯光ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }

            ColorProcess.DegreeDialog dlg = new EasyPhoto.ColorProcess.DegreeDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            dlg.Text = "灯光";
            dlg.Description = "光照强度";
            dlg.Degree = 220;
            dlg.Support = ColorProcess.DegreeDialog.SupportMethod.Lighting;
            dlg.ShowDialog();
            if (dlg.IsFinish)
            {
                this.ClearAllLayer(dlg.FinalImage);
            }
        }

        private void 马赛克ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }

            ColorProcess.DegreeDialog dlg = new EasyPhoto.ColorProcess.DegreeDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            dlg.Text = "马赛克";
            dlg.Maximum = 32;
            dlg.Minimum = 1;
            dlg.Degree = 5;
            dlg.Support = ColorProcess.DegreeDialog.SupportMethod.Mosaic;
            dlg.ShowDialog();
            if (dlg.IsFinish)
            {
                this.ClearAllLayer(dlg.FinalImage);
            }
        }

        private void 油画ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            ColorProcess.OilPaintingDialog colorbalancedialog = new EasyPhoto.ColorProcess.OilPaintingDialog((Bitmap)this.CurrentStage.ExportCustomImage());
            colorbalancedialog.ShowDialog();
            if (colorbalancedialog.IsFinish)
            {
                this.ClearAllLayer(colorbalancedialog.FinalImage);
            }
        }

        private void 曝光ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentStage == null)
            {
                MessageBox.Show("还未建立任何图层", "错误");
                return;
            }
            Dialog.PreviewEffect pe = new EasyPhoto.Dialog.PreviewEffect((Bitmap)this.CurrentStage.ExportCustomImage(), 27);
            pe.ShowDialog();
            if (pe.IsFinish)
            {
                this.ClearAllLayer(pe.FinalImage);
            }
        }
        #endregion
    }
}