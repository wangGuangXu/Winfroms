using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace EasyPhoto.ImageProcess
{
  /// <summary>
  /// N��N ת������
  /// </summary>
  public class MatrixNxN : ImageInfo
  {
    int size = 0;
    int radius = 0;
    int scale = 1;
    int kernelOffset = 0;
    int[,] kernel; // �����

    /// <summary>
    /// ��ȡ����˴�С�������ڿ��
    /// </summary>
    public int Size
    {
      get
      {
        return size;
      }
    }

    /// <summary>
    /// ��ȡ�����þ������У����а�������Ԫ�ء����ű�����ƫ����
    /// </summary>
    public int[] Sequence
    {
      get
      {
        int[] sequence = new int[kernel.Length + 2];
        for (int i = 0; i < size; i++)
        {
          for (int j = 0; j < size; j++)
          {
            sequence[i * size + j] = kernel[i, j];
          } // j
        } // i

        sequence[sequence.Length - 2] = scale;
        sequence[sequence.Length - 1] = kernelOffset;

        return sequence;
      }
      set
      {
        int [] sequence = value;

        // ���ڿ��
        size = (int)(Math.Sqrt(sequence.Length));

        // ���ڰ뾶
        radius = size / 2;

        // ���þ���
        kernel = new int[size, size];
        for (int i = 0; i < size; i++)
        {
          for (int j = 0; j < size; j++)
          {
            kernel[i, j] = sequence[i * size + j];
          } // j
        } // i

        scale = sequence[sequence.Length - 2];
        kernelOffset = sequence[sequence.Length - 1];
      }
    } // Sequence

    /// <summary>
    /// ��ȡ�����þ���ˣ������ھ���
    /// </summary>
    public int[,] Kernel
    {
      get
      {
        return kernel;
      }
      set
      {
        kernel = value;

        // ���ڿ��
        size = kernel.GetLength(0);

        // ���ڰ뾶
        radius = size / 2;
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
    /// ��ͼ�� N��N ���ڽ��о��ת��
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
      int rectTop = radius;
      int rectBottom = height - radius;
      int rectLeft = radius;
      int rectRight = width - radius;

      unsafe
      {
        byte* src = (byte*)srcData.Scan0;
        byte* dst = (byte*)dstData.Scan0;

        int stride = srcData.Stride;
        int offset = stride - width * BPP;

        int pixel = 0;

        // ������У����� radius ��
        src += stride * rectTop;
        dst += stride * rectTop;
        for (int y = rectTop; y < rectBottom; y++)
        {
          // ���������У���ÿ�е� radius ��
          src += BPP * rectLeft;
          dst += BPP * rectLeft;

          for (int x = rectLeft; x < rectRight; x++)
          {
            // �����ǰ����Ϊ͸��ɫ��������������
            if (src[3] > 0)
            {
              // ���� B, G, R ������
              for (int i = 0; i < 3; i++)
              {
                pixel = 0;

                // �������
                for (int m = -radius; m <= radius; m++)
                {
                  for (int n = -radius; n <= radius; n++)
                  {
                    pixel += src[i + m * stride + n * BPP] * kernel[m + radius, n + radius];
                  } // n
                } // m

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
          // �����ע��Ҫ���� radius �У������ұ߻��� radius �в��ش���
          src += (offset + BPP * radius);
          dst += (offset + BPP * radius);
        } // y
      }

      srcImage.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      srcImage.Dispose();

      return dstImage;
    } // end of Convolute


  }
}
