using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EasyPhoto
{

    [Serializable]
    public class SerialClass
    {
        public int defaultlyaernamenum;
        public EasyPhoto.StageCase SerialStage;
        public EasyPhoto.LayerCase[] SerialLayers;

        public SerialClass(EasyPhoto.EPControl.Stage stage, System.Collections.ArrayList layerlist)
        {
            this.SerialStage = new StageCase(stage);
            this.defaultlyaernamenum = stage.SuperParent.defaultTabPageNum;

            int count = layerlist.Count;
            if (count > 0)
            {
                this.SerialLayers = new LayerCase[count];
                for (int i = 0; i < count; i++)
                {
                    this.SerialLayers[i] = new LayerCase((EasyPhoto.EPControl.Layer)layerlist[i]);
                }
            }
        }

        //序列化前的初始化设置
        public void SerialBegin()
        {
            if (this.SerialStage.arrayList != null)
            {
                for (int i = 0; i < this.SerialStage.arrayList.Count; i++)
                {
                    ((EasyPhoto.CustomClass.Cell)this.SerialStage.arrayList[i]).Serial();
                }
            }

            if (this.SerialLayers != null)
            {
                for (int i = 0; i < this.SerialLayers.Length; i++)
                {
                    for (int j = 0; j < (this.SerialLayers[i].arrayList).Count; j++)
                    {
                        ((EasyPhoto.CustomClass.Cell)this.SerialLayers[i].arrayList[j]).Serial();
                    }
                }
            }

        }

        //反序列化后的初始设置
        public void DeSerial()
        {
            if (this.SerialStage.arrayList != null)
            {
                for (int i = 0; i < this.SerialStage.arrayList.Count; i++)
                {
                    ((EasyPhoto.CustomClass.Cell)this.SerialStage.arrayList[i]).Deserial();
                }
            }
            if (this.SerialLayers != null)
            {
                for (int i = 0; i < this.SerialLayers.Length; i++)
                {
                    for (int j = 0; j < (this.SerialLayers[i].arrayList).Count; j++)
                    {
                        ((EasyPhoto.CustomClass.Cell)this.SerialLayers[i].arrayList[j]).Deserial();
                    }
                }
            }
        }
    }
 
    [Serializable]
    public class LayerCase
    {
        public Color backgroundColor;
        public int backgroundWidth;
        public int backgroundHeight;
        public System.Drawing.Size size;
        public string paperName;
        public System.Drawing.Point startPoint;
        public int activeNum;
        public Color backPaperColor;
        public System.Drawing.Rectangle showRectangle;
        public System.Drawing.Bitmap baseImage;
        public System.Drawing.Bitmap backImage;
        public System.Drawing.Bitmap finalImage;
        public CustomClass.Cell tempCell;
        public int zoom;
        public System.Drawing.Bitmap BackgroundImage;
        public System.Collections.ArrayList arrayList;
        public bool Cansee;


        
        public LayerCase(EasyPhoto.EPControl.Layer temp)
        {
            this.backgroundColor = temp.BackgroundColor;
            this.backgroundHeight = temp.BackgroundHeight;
            this.backgroundWidth = temp.BackgroundWidth;
            this.size = temp.Size;
            this.paperName = temp.PaperName;
            this.startPoint = temp.StartPosition;
            this.activeNum = temp.ActiveNum;
            this.backPaperColor = ((SolidBrush)temp.BackPaperColor).Color ;
            this.showRectangle = temp.showRectangle;
            this.baseImage = (System.Drawing.Bitmap)temp.baseImage.Clone();
            this.backImage = temp.BackImage;
            this.finalImage = temp.FinalImage;
            this.tempCell = temp.GetTempCell;
            this.zoom = temp.Zoom;
            this.arrayList = temp.GetArrayList();
            this.Cansee = temp.CanSee;
        }
    }

    [Serializable]
    public class StageCase
    {
        public Color backgroundColor;
        public int backgroundWidth;
        public int backgroundHeight;
        public System.Drawing.Size size;
        public string paperName;
        public System.Drawing.Point startPoint;
        public int activeNum;
        public Color backPaperColor;
        public System.Drawing.Rectangle showRectangle;
        public System.Drawing.Bitmap baseImage;
        public System.Drawing.Bitmap backImage;
        public System.Drawing.Bitmap finalImage;
        public CustomClass.Cell tempCell;
        public int zoom;
        public System.Drawing.Bitmap BackgroundImage;
        public System.Collections.ArrayList arrayList;



        public StageCase(EasyPhoto.EPControl.Stage temp)
        {
            this.backgroundColor = temp.BackgroundColor;
            this.backgroundHeight = temp.BackgroundHeight;
            this.backgroundWidth = temp.BackgroundWidth;
            this.size = temp.Size;
            this.paperName = temp.PaperName;
            this.startPoint = temp.StartPosition;
            this.activeNum = temp.ActiveNum;
            this.backPaperColor = ((SolidBrush)temp.BackPaperColor).Color;
            this.showRectangle = temp.showRectangle;
            this.baseImage = (System.Drawing.Bitmap)temp.baseImage.Clone();
            this.backImage = temp.BackImage;
            this.finalImage = temp.FinalImage;
            this.tempCell = temp.GetTempCell;
            this.zoom = temp.Zoom;
            this.arrayList = temp.GetArrayList();
        }
    }
}
