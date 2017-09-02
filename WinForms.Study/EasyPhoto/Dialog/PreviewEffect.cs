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
    public partial class PreviewEffect : Form
    {
        public bool IsFinish = false;
        public Bitmap FinalImage = null;
        public Bitmap SrcImage = null;

        public PreviewEffect(Bitmap image,int i)
        {
            InitializeComponent();
            this.SrcImage = image;
            this.SwtchMethod(i);

        }

        public void SwtchMethod(int i)
        {
            switch (i)
            {
                    //负色
                case 0:                        
                    {
                        EasyPhoto.ImageProcess.Adjustment a = new EasyPhoto.ImageProcess.Adjustment();
                        Bitmap dstImage = a.Invert(SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                    }
                    break;
                    //轮换通道
                case 1:                       
                    {
                        EasyPhoto.ImageProcess.Adjustment a1 = new EasyPhoto.ImageProcess.Adjustment();
                        Bitmap dstImage1 = a1.RotateChannel(SrcImage);
                        this.panel1.BackgroundImage = dstImage1;
                        this.FinalImage = (Bitmap)dstImage1.Clone();
                        break;
                    }
                    //提取通道 红色
                case 2:                        
                    EasyPhoto.ImageProcess.Adjustment a2 = new EasyPhoto.ImageProcess.Adjustment();
                    Bitmap dstImage2 = a2.ExtractChannel(SrcImage, EasyPhoto.ImageProcess.Adjustment.ChannelMode.Red);
                    this.panel1.BackgroundImage = dstImage2;
                    this.FinalImage = (Bitmap)dstImage2.Clone();
                    break;
                    //提取通道 绿色
                case 3:                         
                    EasyPhoto.ImageProcess.Adjustment a3 = new EasyPhoto.ImageProcess.Adjustment();
                    Bitmap dstImage3 = a3.ExtractChannel(SrcImage, EasyPhoto.ImageProcess.Adjustment.ChannelMode.Green);
                    this.panel1.BackgroundImage = dstImage3;
                    this.FinalImage = (Bitmap)dstImage3.Clone();
                    break;
                    //提取通道 蓝色
                case 4:                        
                    { 
                        EasyPhoto.ImageProcess.Adjustment a4 = new EasyPhoto.ImageProcess.Adjustment();
                        Bitmap dstImage4 = a4.ExtractChannel(SrcImage, EasyPhoto.ImageProcess.Adjustment.ChannelMode.Blue);
                        this.panel1.BackgroundImage = dstImage4;
                        this.FinalImage = (Bitmap)dstImage4.Clone();
                        break;
                    }
                    //过滤通道 红色
                case 5:                        
                    {
                        EasyPhoto.ImageProcess.Adjustment a4 = new EasyPhoto.ImageProcess.Adjustment();
                        Bitmap dstImage4 = a4.FilterChannel(SrcImage, EasyPhoto.ImageProcess.Adjustment.ChannelMode.Red);
                        this.panel1.BackgroundImage = dstImage4;
                        this.FinalImage = (Bitmap)dstImage4.Clone();
                        break;
                    }
                    //过滤通道 绿色
                case 6:                          
                    {
                        EasyPhoto.ImageProcess.Adjustment a4 = new EasyPhoto.ImageProcess.Adjustment();
                        Bitmap dstImage4 = a4.FilterChannel(SrcImage, EasyPhoto.ImageProcess.Adjustment.ChannelMode.Green);
                        this.panel1.BackgroundImage = dstImage4;
                        this.FinalImage = (Bitmap)dstImage4.Clone();
                        break;
                    }
                    //过滤通道 蓝色
                case 7:                          
                    {
                        EasyPhoto.ImageProcess.Adjustment a4 = new EasyPhoto.ImageProcess.Adjustment();
                        Bitmap dstImage4 = a4.FilterChannel(SrcImage, EasyPhoto.ImageProcess.Adjustment.ChannelMode.Blue);
                        this.panel1.BackgroundImage = dstImage4;
                        this.FinalImage = (Bitmap)dstImage4.Clone();
                        break;
                    }
                case 8:                                
                    //过滤通道  青色
                    {
                        EasyPhoto.ImageProcess.Adjustment a4 = new EasyPhoto.ImageProcess.Adjustment();
                        Bitmap dstImage4 = a4.FilterChannel(SrcImage, EasyPhoto.ImageProcess.Adjustment.ChannelMode.Cyan);
                        this.panel1.BackgroundImage = dstImage4;
                        this.FinalImage = (Bitmap)dstImage4.Clone();
                        break;
                    }
                     //过滤通道 品红:
                case 9:                           
                    {
                        EasyPhoto.ImageProcess.Adjustment a4 = new EasyPhoto.ImageProcess.Adjustment();
                        Bitmap dstImage4 = a4.FilterChannel(SrcImage, EasyPhoto.ImageProcess.Adjustment.ChannelMode.Megenta);
                        this.panel1.BackgroundImage = dstImage4;
                        this.FinalImage = (Bitmap)dstImage4.Clone();
                        break;
                    }
                //过滤通道 黄色
                case 10:                                     
                    {
                        EasyPhoto.ImageProcess.Adjustment a4 = new EasyPhoto.ImageProcess.Adjustment();
                        Bitmap dstImage4 = a4.FilterChannel(SrcImage, EasyPhoto.ImageProcess.Adjustment.ChannelMode.Yellow);
                        this.panel1.BackgroundImage = dstImage4;
                        this.FinalImage = (Bitmap)dstImage4.Clone();
                        break;
                    }
                //均衡化
                case 11:                                  
                    {
                        EasyPhoto.ImageProcess.Histogram h = new EasyPhoto.ImageProcess.Histogram(this.SrcImage);
                        Bitmap dstImage = h.Equalizer();
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
                //平滑
                case 12:                                  
                    {
                        EasyPhoto.ImageProcess.Effect f = new EasyPhoto.ImageProcess.Effect();
                        Bitmap dstImage = f.Smooth(this.SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
                    //高斯模糊
                case 13:
                    {
                        EasyPhoto.ImageProcess.Effect f = new EasyPhoto.ImageProcess.Effect();
                        Bitmap dstImage = f.GaussBlur(this.SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
                  //锐化  
                case 14:
                    {
                        EasyPhoto.ImageProcess.Effect f = new EasyPhoto.ImageProcess.Effect();
                        Bitmap dstImage = f.Sharpen(this.SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
                   //加强锐化 
                case 15:
                    {
                        EasyPhoto.ImageProcess.Effect f = new EasyPhoto.ImageProcess.Effect();
                        Bitmap dstImage = f.SharpenMore(this.SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
                //调和浮雕
                case 16:
                    {
                        EasyPhoto.ImageProcess.Effect f = new EasyPhoto.ImageProcess.Effect();
                        Bitmap dstImage = f.Emboss(this.SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
                    //连环画
                case 17:
                    {
                        EasyPhoto.ImageProcess.Effect f = new EasyPhoto.ImageProcess.Effect();
                        Bitmap dstImage = f.Comic(this.SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
                //碧绿
                case 18:
                    {
                        EasyPhoto.ImageProcess.Effect f = new EasyPhoto.ImageProcess.Effect();
                        Bitmap dstImage = f.Aqua(this.SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
                //棕褐
                case 19:
                    {
                        EasyPhoto.ImageProcess.Effect f = new EasyPhoto.ImageProcess.Effect();
                        Bitmap dstImage = f.Sepia(this.SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
                //冰冻
                case 20:
                    {
                        EasyPhoto.ImageProcess.Effect f = new EasyPhoto.ImageProcess.Effect();
                        Bitmap dstImage = f.Ice(this.SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
                //熔铸
                case 21:
                    {
                        EasyPhoto.ImageProcess.Effect f = new EasyPhoto.ImageProcess.Effect();
                        Bitmap dstImage = f.Molten(this.SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
                //暗调
                case 22:
                    {
                        EasyPhoto.ImageProcess.Effect f = new EasyPhoto.ImageProcess.Effect();
                        Bitmap dstImage = f.Darkness(this.SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
                //对调
                case 23:
                    {
                        EasyPhoto.ImageProcess.Effect f = new EasyPhoto.ImageProcess.Effect();
                        Bitmap dstImage = f.Subtense(this.SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
                //怪调
                case 24:
                    {
                        EasyPhoto.ImageProcess.Effect f = new EasyPhoto.ImageProcess.Effect();
                        Bitmap dstImage = f.Whim(this.SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
                    //球面
                case 25:
                    {
                        EasyPhoto.ImageProcess.Effect f = new EasyPhoto.ImageProcess.Effect();
                        Bitmap dstImage = f.Spherize(this.SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
                //照亮边缘
                case 26:
                    {
                        EasyPhoto.ImageProcess.Effect f = new EasyPhoto.ImageProcess.Effect();
                        Bitmap dstImage = f.GlowingEdge(this.SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
                //曝光
                case 27:
                    {
                        EasyPhoto.ImageProcess.Effect f = new EasyPhoto.ImageProcess.Effect();
                        Bitmap dstImage = f.Solarize(this.SrcImage);
                        this.panel1.BackgroundImage = dstImage;
                        this.FinalImage = (Bitmap)dstImage.Clone();
                        break;
                    }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.IsFinish = true;
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.IsFinish = false;
            this.Dispose();
        }
    }
}
