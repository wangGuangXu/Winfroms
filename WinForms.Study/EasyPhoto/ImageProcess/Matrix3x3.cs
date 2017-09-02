using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace EasyPhoto.ImageProcess
{
  /// <summary>
  /// 3��3 ת������
  /// </summary>
  public class Matrix3x3 : ImageInfo
  {
    int topLeft = 0, topMid = 0, topRight = 0;
    int midLeft = 0, center = 1, midRight = 0;
    int bottomLeft = 0, bottomMid = 0, bottomRight = 0;

    int scale = 1;
    int kernelOffset = 0;

    /// <summary>
    /// ��ȡ���������ϵ�Ȩֵ
    /// </summary>
    public int TopLeft
    {
      get
      {
        return topLeft;
      }
      set
      {
        topLeft = value;
      }
    }

    /// <summary>
    /// ��ȡ���������ϵ�Ȩֵ
    /// </summary>
    public int TopMid
    {
      get
      {
        return topMid;
      }
      set
      {
        topMid = value;
      }
    }

    /// <summary>
    /// ��ȡ���������ϵ�Ȩֵ
    /// </summary>
    public int TopRight
    {
      get
      {
        return topRight;
      }
      set
      {
        topRight = value;
      }
    }

    /// <summary>
    /// ��ȡ���������Ȩֵ
    /// </summary>
    public int MidLeft
    {
      get
      {
        return midLeft;
      }
      set
      {
        midLeft = value;
      }
    }

    /// <summary>
    /// ��ȡ���������ĵ�Ȩֵ
    /// </summary>
    public int Center
    {
      get
      {
        return center;
      }
      set
      {
        center = value;
      }
    }

    /// <summary>
    /// ��ȡ�������ҵ�Ȩֵ
    /// </summary>
    public int MidRight
    {
      get
      {
        return midRight;
      }
      set
      {
        midRight = value;
      }
    }

    /// <summary>
    /// ��ȡ���������µ�Ȩֵ
    /// </summary>
    public int BottomLeft
    {
      get
      {
        return bottomLeft;
      }
      set
      {
        bottomLeft = value;
      }
    }

    /// <summary>
    /// ��ȡ���������µ�Ȩֵ
    /// </summary>
    public int BottomMid
    {
      get
      {
        return bottomMid;
      }
      set
      {
        bottomMid = value;
      }
    }

    /// <summary>
    /// ��ȡ���������µ�Ȩֵ
    /// </summary>
    public int BottomRight
    {
      get
      {
        return bottomRight;
      }
      set
      {
        bottomRight = value;
      }
    }

    /// <summary>
    /// ��ȡ���������ű���
    /// </summary>
    public int Scale
    {
      get
      {
        return scale;
      }
      set
      {
        scale = value;
      }
    }

    /// <summary>
    /// ��ȡ������ƫ����
    /// </summary>
    public int Offset
    {
      get
      {
        return kernelOffset;
      }
      set
      {
        kernelOffset = value;
      }
    }


    /// <summary>
    /// ��ʼ���������е�ΪͬһȨֵ
    /// </summary>
    /// <param name="degree">Ȩֵ</param>
    public void Init(int degree)
    {
      topLeft = topMid = topRight = 
      midLeft = center = midRight =
      bottomLeft = bottomMid = bottomRight = degree;
    } // end of Init


    /// <summary>
    /// ��ͼ�� 3X3 ���ڽ��о��ת��
    /// </summary>
    /// <param name="srcImage">λͼ��</param>
    /// <returns></returns>
    public Bitmap Convolute(Bitmap srcImage)
    {
      // ���ⱻ���
      if (scale == 0) scale = 1;

      int width = srcImage.Width;
      int height = srcImage.Height;

      Bitmap dstImage = (Bitmap)srcImage.Clone();

      BitmapData srcData = srcImage.LockBits(new Rectangle(0, 0, width, height), 
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height), 
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      // ͼ��ʵ�ʴ�������
      // ͼ������ΧһȦ������
      int rectTop = 1;
      int rectBottom = height - 1;
      int rectLeft = 1;
      int rectRight = width - 1;

      unsafe
      {
        byte* src = (byte*)srcData.Scan0;
        byte* dst = (byte*)dstData.Scan0;

        int stride = srcData.Stride;
        int offset = stride - width * BPP;

        int pixel = 0;

        // �����һ��
        src += stride;
        dst += stride;
        for (int y = rectTop; y < rectBottom; y++)
        {
          // ����ÿ�е�һ��
          src += BPP;
          dst += BPP;

          for (int x = rectLeft; x < rectRight; x++)
          {
            // �����ǰ����Ϊ͸��ɫ��������������
            if (src[3] > 0)
            {
              // ���� B, G, R ������
              for (int i = 0; i < 3; i++)
              {
                pixel =
                  src[i - stride - BPP] * topLeft + 
                  src[i - stride] * topMid + 
                  src[i - stride + BPP] * topRight +
                  src[i - BPP] * midLeft + 
                  src[i] * center + 
                  src[i + BPP] * midRight +
                  src[i + stride - BPP] * bottomLeft + 
                  src[i + stride] * bottomMid + 
                  src[i + stride + BPP] * bottomRight;
                pixel = pixel / scale + kernelOffset;

                if (pixel < 0) pixel = 0;
                if (pixel > 255) pixel = 255;

                dst[i] = (byte)pixel;
              } // i
            }


            // �����һ����
            src += BPP;
            dst += BPP;
          } // x

          // ������һ��
          // �����ע��Ҫ����һ�У��������в�����
          src += (offset + BPP);
          dst += (offset + BPP);
        } // y
      }

      srcImage.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      srcImage.Dispose();

      return dstImage;
    } // end of Convolute


  }
}
