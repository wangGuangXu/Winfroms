using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace EasyPhoto.ImageProcess
{
    /// <summary>
    /// ͼ��ˮӡ������
    /// </summary>
    public class Watermark : ImageInfo
    {
        /**************************************************
         * 
         * ˮӡ���ֶ�λ
         * 
         * X ���ꡢY ���ꡢ���뷽ʽ
         * 
         **************************************************/

        /// <summary>
        /// ��ȡ������ˮӡ���� X ����
        /// </summary>
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        /// <summary>
        /// X ����
        /// </summary>
        int x = 0;


        /// <summary>
        /// ��ȡ������ˮӡ���� Y ����
        /// </summary>
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
        /// <summary>
        /// Y ����
        /// </summary>
        int y = 0;


        /// <summary>
        /// ˮӡ���ֶ��뷽ʽ
        /// </summary>
        public Function.AlignMode Align
        {
            get
            {
                return align;
            }
            set
            {
                align = value;
            }
        }
        /// <summary>
        /// ���뷽ʽ
        /// </summary>
        Function.AlignMode align = Function.AlignMode.TopLeft;


        /**************************************************
         * 
         * ��������
         * 
         * ����ء���С�����Ρ�������ɫ
         * 
         **************************************************/

        /// <summary>
        /// ��ȡ�����������
        /// </summary>
        public string FontFamily
        {
            get
            {
                return fontFamily;
            }
            set
            {
                fontFamily = value;
            }
        }
        /// <summary>
        /// �����
        /// </summary>
        string fontFamily = "Arial";

        /// <summary>
        /// ��ȡ�����������С
        /// </summary>
        public int FontSize
        {
            get
            {
                return fontSize;
            }
            set
            {
                fontSize = value;
            }
        }
        /// <summary>
        /// �����С
        /// </summary>
        int fontSize = 30;

        /// <summary>
        /// ��ȡ������������
        /// </summary>
        public System.Drawing.FontStyle FontWeight
        {
            get
            {
                return fontWeight;
            }
            set
            {
                fontWeight = value;
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        System.Drawing.FontStyle fontWeight = FontStyle.Regular;

        /// <summary>
        /// ��ȡ������������ɫ
        /// </summary>
        public Color FontColor
        {
            get
            {
                return fontColor;
            }
            set
            {
                fontColor = value;
            }
        }
        /// <summary>
        /// ������ɫ
        /// </summary>
        Color fontColor = Color.Red;


        /************************************************************
         * 
         * ͳ�����ص㡢ˮӡ����
         * 
         ************************************************************/


        /// <summary>
        /// ͳ�Ƴ�һ���ַ������ٸ����ص�
        /// </summary>
        /// <param name="character">�ַ�</param>
        /// <returns></returns>
        public int CountTextDots(char character)
        {
            Bitmap b = new Bitmap(20, 20);
            Graphics g = Graphics.FromImage(b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            Font font = new Font(new FontFamily("����"), 12, FontStyle.Regular);
            SolidBrush brush = new SolidBrush(Color.White);

            g.DrawString(character.ToString(), font, brush, 0, 0);
            g.Save();

            GrayProcessing gp = new GrayProcessing();
            byte[,] Gray = gp.BinaryArray(b, 128);

            int count = 0;

            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    // ͳ�ư׵�
                    if (Gray[x, y] > 128)
                        count++;
                } // x
            } // y

            b.Dispose();

            return count;
        } // end of CountTextDots


        /// <summary>
        /// ˮӡ����
        /// </summary>
        /// <param name="b">λͼ��</param>
        /// <param name="text">ˮӡ����</param>
        /// <param name="isHorz">�Ƿ�ˮƽ��ʽ��ͼ����д����</param>
        /// <returns></returns>
        public Bitmap WaterText(Bitmap b, string text, bool isHorz)
        {
            int W = b.Width;
            int H = b.Height;

            Graphics g = Graphics.FromImage(b);

            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;  // ��������ֵ
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;   // ������ƽ��
            //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;  // �������

            // ȷ����ˮƽ���Ǵ�ֱ��ʾ����
            System.Drawing.StringFormat stringFormat = new StringFormat();
            if (!isHorz)
            {
                // ��ֱ��ʾ����
                stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            }

            // ����ˮӡ����
            Font waterFont = new Font(fontFamily, fontSize, fontWeight, GraphicsUnit.Pixel);

            // ��ɫ
            SolidBrush brushColor = new SolidBrush(fontColor);

            // ����ָ���Ķ��뷽ʽ�����ֽ��ж�λ
            Point point = new Point(x, y);

            // ����ˮӡ����
            g.DrawString(text, waterFont, brushColor, point.X, point.Y, stringFormat);

            g.Save();
            g.Dispose();

            return b;
        } // end of WaterText


    }
}
