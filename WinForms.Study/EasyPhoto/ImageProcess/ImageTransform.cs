using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace EasyPhoto.ImageProcess
{
  /// <summary>
  /// ͼ��任��
  /// </summary>
  public class ImageTransform : ImageInfo
  {
    /// <summary>
    /// δ֪��������ģʽ
    /// </summary>
    public enum AreasMode
    {
      /// <summary>
      /// ͸��
      /// </summary>
      Transparent,

      /// <summary>
      /// �ظ���Ե����
      /// </summary>
      RepeatEdgePixels,

      /// <summary>
      /// ���ܻ���
      /// </summary>
      WrapAround
    }

    /// <summary>
    /// ����ģʽ
    /// </summary>
    public enum TrimMode : int
    { 
      /// <summary>
      /// �������κα�
      /// </summary>
      None = 0,

      /// <summary>
      /// �����ϱ�
      /// </summary>
      Top = 1,

      /// <summary>
      /// �����±�
      /// </summary>
      Bottom = 2,

      /// <summary>
      /// �������
      /// </summary>
      Left = 4,

      /// <summary>
      /// �����ұ�
      /// </summary>
      Right = 8
    }


    /************************************************************
     * 
     * ƽ�ơ��ߴ硢�ü�
     * ��ת����ת��ת�á���б������
     * 
     ************************************************************/

    /// <summary>
    /// ͼ��ƽ��
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="horizontal">ˮƽƫ����</param>
    /// <param name="vertical">��ֱƫ����</param>
    /// <param name="areaMode">ƽ�ƺ�ͼ�����µ�δ֪�������÷�ʽ</param>
    /// <returns></returns>
    public Bitmap Translate(Bitmap b, int horizontal, int vertical, AreasMode areaMode)
    {
      int width = b.Width;
      int height = b.Height;

      horizontal %= width;
      vertical %= height;

      Bitmap dstImage = new Bitmap(width, height);
      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dstImage);

      // ͼ��ƫ�Ʋ���
      Point diagonal = new Point(horizontal, vertical);
      g.DrawImage(b, diagonal);

      switch (areaMode)
      {
        case AreasMode.WrapAround:
          if (horizontal < 0)
            diagonal.X += width;
          else
            diagonal.X -= width;
          // ˮƽ�Ƴ�����
          g.DrawImage(b, diagonal);

          if (vertical < 0)
            diagonal.Y += height;
          else
            diagonal.Y -= height;
          // �Խ��Ƴ�����
          g.DrawImage(b, diagonal);

          // ��ֱ�Ƴ�����
          diagonal.X = horizontal;
          g.DrawImage(b, diagonal);
          break;

        case AreasMode.RepeatEdgePixels:
          // ˮƽ�Ƴ�����
          diagonal.X += (horizontal < 0 ? width : -width);
          g.DrawImage(b,
            new Rectangle(diagonal.X, diagonal.Y, width, height),
            new Rectangle(horizontal < 0 ? width - 1 : 0, 0, 1, height),
            GraphicsUnit.Pixel
            );

          // �Խ��Ƴ�����
          diagonal.Y += (vertical < 0 ? height : -height);
          g.DrawImage(b,
            new Rectangle(diagonal.X, diagonal.Y, width, height),
            new Rectangle(horizontal < 0 ? width - 1 : 0, vertical < 0 ? height - 1 : 0, 1, 1),
            GraphicsUnit.Pixel
            );

          // ��ֱ�Ƴ�����
          diagonal.X = horizontal;
          g.DrawImage(b,
           new Rectangle(diagonal.X, diagonal.Y, width, height),
           new Rectangle(0, vertical < 0 ? height - 1 : 0, width, 1),
           GraphicsUnit.Pixel
           );
          break;

        case AreasMode.Transparent:
          break;
      } // switch


      g.Save();
      g.Dispose();

      b.Dispose();

      return dstImage;
    } // end of Translate


    /// <summary>
    /// ͼ��ߴ����
    /// </summary>
    /// <param name="b">ԭʼͼ��</param>
    /// <param name="dstWidth">Ŀ����</param>
    /// <param name="dstHeight">Ŀ��߶�</param>
    /// <returns></returns>
    public Bitmap Resize(Bitmap b, int dstWidth, int dstHeight)
    {
      Bitmap dstImage = new Bitmap(dstWidth, dstHeight);
      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dstImage);

      // ���ò�ֵģʽ
      g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;

      // ����ƽ��ģʽ
      g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

      g.DrawImage(b, 
        new System.Drawing.Rectangle(0, 0, dstImage.Width, dstImage.Height), 
        new System.Drawing.Rectangle(0, 0, b.Width, b.Height), 
        System.Drawing.GraphicsUnit.Pixel);
      g.Save();
      g.Dispose();

      return dstImage;
    } // end of Resize


    /// <summary>
    /// ��ָ���Ĳü�·����ͼ����вü�
    /// </summary>
    /// <param name="b">ԭʼͼ��</param>
    /// <param name="region">�ü�����</param>
    /// <returns></returns>
    public Bitmap Crop(Bitmap b, Region region)
    {
      Graphics g = System.Drawing.Graphics.FromImage(b);

      // ��ȡ����߽�
      RectangleF validRect = region.GetBounds(g);

      int x = (int)validRect.X;
      int y = (int)validRect.Y;
      int width = (int)validRect.Width;
      int height = (int)validRect.Height;

      // ���������ƽ��
      Region validRegion = region;
      validRegion.Translate(-x, -y);

      Bitmap dstImage = new Bitmap(width, height);
      Graphics dstGraphics = System.Drawing.Graphics.FromImage(dstImage);

      // ���ü�������
      dstGraphics.SetClip(validRegion, CombineMode.Replace);

      // ��ͼ
      dstGraphics.DrawImage(b, new Rectangle(0, 0, width, height), 
        validRect, GraphicsUnit.Pixel);

      g.Dispose();
      dstGraphics.Dispose();
      b.Dispose();

      return dstImage;
    } // end of Crop


    /// <summary>
    /// ����ʱ�뷽��Ϊ�������ͼ�������ת
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="angle">��ת�Ƕ�[0, 360]</param>
    /// <returns></returns>
    public Bitmap Rotate(Bitmap b, int angle)
    {
      angle = angle % 360;

      // ����ת��
      double radian = angle * Math.PI / 180.0;
      double cos = Math.Cos(radian);
      double sin = Math.Sin(radian);

      // ԭͼ���
      int w = b.Width;
      int h = b.Height;

      // ��ͼ�Ŀ��
      int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
      int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));

      // Ŀ��λͼ������ת���ͼ
      Bitmap dstImage = new Bitmap(W, H);
      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dstImage);
      g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
      g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

      // ƫ����
      Point offset = new Point((W - w) / 2, (H - h) / 2);

      // ����ͼ����ʾ������ͼ������ĵ��봰�ڵ����ĵ�һ��
      Rectangle rect = new Rectangle(offset.X, offset.Y, w, h);
      Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

      // ��ͼ������ĵ���ת
      g.TranslateTransform(center.X, center.Y);
      g.RotateTransform(360 - angle);

      // �ָ�ͼ����ˮƽ�ʹ�ֱ�����ƽ��
      g.TranslateTransform(-center.X, -center.Y);

      // ������ת��Ľ��ͼ
      g.DrawImage(b, rect);

      // ���û�ͼ�����б任
      g.ResetTransform();

      g.Save();

      return dstImage;
    } // end of Rotate


    /// <summary>
    /// ��ͼ����з�ת�任��������
    /// </summary>
    /// <param name="b">ԭʼͼ��</param>
    /// <param name="isHorz">�Ƿ�ˮƽ������з�ת</param>
    /// <returns></returns>
    public Bitmap Flip(Bitmap b, bool isHorz)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = new Bitmap(width, height);

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      int stride = srcData.Stride;
      System.IntPtr srcScan0 = srcData.Scan0;
      System.IntPtr dstScan0 = dstData.Scan0;
      int offset = stride - width * BPP;

      unsafe
      {
        byte* src = (byte*)srcScan0;
        byte* dst = (byte*)dstScan0;

        if (isHorz)
        {
          // ˮƽ��ת
          for (int y = 0; y < height; y++)
          {
            for (int x = 0; x < width; x++)
            {
              dst = (byte*)dstScan0 + (y * stride) + ((width - x - 1) * BPP);

              dst[0] = src[0]; // B
              dst[1] = src[1]; // G
              dst[2] = src[2]; // R
              dst[3] = src[3]; // A

              src += BPP;
            } // x

            src += offset;
          } // y
        }
        else
        {
          // ��ֱ��ת
          for (int y = 0; y < height; y++)
          {
            for (int x = 0; x < width; x++)
            {
              dst = (byte*)dstScan0 + ((height - y - 1) * stride) + (x * BPP);

              dst[0] = src[0]; // B
              dst[1] = src[1]; // G
              dst[2] = src[2]; // R
              dst[3] = src[3]; // A

              src += BPP;
            } // x

            src += offset;
          } // y
        } // isHorz
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      return dstImage;
    } // end of Flip


    /// <summary>
    /// ��ͼ�����ת�ñ任
    /// </summary>
    /// <param name="b">ԭʼͼ��</param>
    /// <returns></returns>
    public Bitmap Transpose(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = new Bitmap(height, width);

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, height, width),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      int srcStride = srcData.Stride;
      int dstStride = dstData.Stride;
      System.IntPtr srcScan0 = srcData.Scan0;
      System.IntPtr dstScan0 = dstData.Scan0;
      int offset = srcData.Stride - width * BPP;

      unsafe
      {
        byte* src = (byte*)srcScan0;
        byte* dst = (byte*)dstScan0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            dst = (byte*)dstScan0 + (x * dstStride) + (y * BPP);

            dst[0] = src[0];
            dst[1] = src[1];
            dst[2] = src[2];
            dst[3] = src[3];

            src += BPP;
          } // x

          src += offset;
        } // y
      }

      dstImage.UnlockBits(dstData);
      b.UnlockBits(srcData);

      b.Dispose();

      return dstImage;
    } // end of Transpose


    /// <summary>
    /// ��ͼ�����ˮƽ������б�任����׼��Ϊƽ���ı������϶���
    /// </summary>
    /// <param name="b">ԭʼͼ��</param>
    /// <param name="horz">ˮƽ������б��</param>
    /// <returns></returns>
    public Bitmap SlantHorz(Bitmap b, int horz)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = new Bitmap(Math.Abs(horz) + width, height);

      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dstImage);

      g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
      g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

      Point[] dstPoints = new Point[3];

      // �ֱ����ƽ���ı��ε����ϡ����Ϻ�������������
      if (horz != 0)
      {
        if (horz > 0)
        {
          dstPoints[0] = new Point(        horz,      0);
          dstPoints[1] = new Point(width + horz,      0);
          dstPoints[2] = new Point(           0, height);
        }
        else
        {
          dstPoints[0] = new Point(    0,      0);
          dstPoints[1] = new Point(width,      0);
          dstPoints[2] = new Point(-horz, height);
        }
      }

      // ��ˮƽ��бͼ
      g.DrawImage(b, dstPoints);

      g.Save();
      g.Dispose();

      b.Dispose();

      return dstImage;
    } // end of SlantHorz


    /// <summary>
    /// ��ͼ����д�ֱ������б�任����׼��Ϊƽ���ı������϶���
    /// </summary>
    /// <param name="b">ԭʼͼ��</param>
    /// <param name="vert">��ֱ������б��</param>
    /// <returns></returns>
    public Bitmap SlantVert(Bitmap b, int vert)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = new Bitmap(width, Math.Abs(vert) + height);

      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dstImage);

      g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
      g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

      Point[] dstPoints = new Point[3];

      // �ֱ����ƽ���ı��ε����ϡ����Ϻ�������������
      if (vert != 0)
      {
        if (vert > 0)
        {
          dstPoints[0] = new Point(    0,          vert);
          dstPoints[1] = new Point(width,             0);
          dstPoints[2] = new Point(    0, height + vert);
        }
        else
        {
          dstPoints[0] = new Point(    0,      0);
          dstPoints[1] = new Point(width,  -vert);
          dstPoints[2] = new Point(    0, height);
        }

      }

      // ��ˮƽ��бͼ
      g.DrawImage(b, dstPoints);

      g.Save();
      g.Dispose();

      b.Dispose();

      return dstImage;
    } // end of SlantVert


    /// <summary>
    /// ����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="trimAway">������Χ</param>
    /// <returns></returns>
    public Bitmap Trim(Bitmap b, TrimMode trimAway)
    {
      int width = b.Width;
      int height = b.Height;

      // ԭʼͼ���С
      int area = width * height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      int stride = data.Stride;
      System.IntPtr scan0 = data.Scan0;
      int offset = stride - width * BPP;

      // ���������Ч����
      int rectTop = 0;
      int rectBottom = height;
      int rectLeft = 0;
      int rectRight = width;

      unsafe
      {
        byte* p = (byte*)scan0;
        int i = 1;

        // ��
        if ((trimAway & TrimMode.Top) == TrimMode.Top)
        {
          p = (byte*)scan0;
          i = 1;
          while (i < area)
          {
            if (p[3] != 0)
            {
              // ��ȡ�ϱ߽�
              rectTop = i / width;
              break;
            }

            p += BPP;
            if (i % width == 0)
              p += offset;
            i++;
          } // while
        }

        // ��
        if ((trimAway & TrimMode.Bottom) == TrimMode.Bottom)
        {
          p = (byte*)scan0 + (height - 1) * stride + (width - 1) * BPP;
          i = 1;
          while (i < area)
          {
            if (p[3] != 0)
            {
              // ��ȡ�±߽�
              rectBottom = height - i / width;
              break;
            }

            p -= BPP;
            if (i % width == 0)
              p -= offset;
            i++;
          } // while
        }

        // ��
        if ((trimAway & TrimMode.Left) == TrimMode.Left)
        {
          p = (byte*)scan0;
          i = 1;
          while (i < area)
          {
            if (p[3] != 0)
            {
              // ��ȡ��߽�
              rectLeft = i / height;
              break;
            }

            p += stride;
            if (i % height == 0)
            {
              p = (byte*)scan0;
              p += (i / height) * BPP;
            }
            i++;
          } // while
        }

        // ��
        if ((trimAway & TrimMode.Right) == TrimMode.Right)
        {
          p = (byte*)scan0 + (width - 1) * BPP;
          i = 1;
          while (i < area)
          {
            if (p[3] != 0)
            {
              // ��ȡ�ұ߽�
              rectRight = width - i / height;
              break;
            }

            p += stride;
            if (i % height == 0)
            {
              p = (byte*)scan0 + (width - 1) * BPP;
              p -= (i / height) * BPP;
            }
            i++;
          } // while
        }
      }

      b.UnlockBits(data);


      // ������Ч������ͼ��
      Bitmap dst = new Bitmap(rectRight - rectLeft, rectBottom - rectTop);
      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dst);
      g.DrawImage(b,
        new Rectangle(0, 0, dst.Width, dst.Height),
        new Rectangle(rectLeft, rectTop, dst.Width, dst.Height),
        GraphicsUnit.Pixel
        );
      g.Save();

      b.Dispose();

      return dst;
    } // end of Trim


  }
}
