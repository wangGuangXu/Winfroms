using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace EasyPhoto.ImageProcess
{
  /// <summary>
  /// ��Ե�����
  /// </summary>
  public class EdgeDetect : ImageInfo
  {
    /************************************************************
     * 
     * Roberts, Sobel, Prewitt, Kirsch, GaussLaplacian
     * ˮƽ��⡢��ֱ��⡢��Ե��ǿ����Ե���⻯
     * 
     ************************************************************/


    /// <summary>
    /// ������ͼ������ݶ�����
    /// </summary>
    /// <param name="b1">λͼ 1</param>
    /// <param name="b2">λͼ 2</param>
    /// <returns></returns>
    private Bitmap Gradient(Bitmap b1, Bitmap b2)
    {
      //Algebra a = new Algebra();

      //// ������ͼ������������
      //return a.AlgebraOperate(b1, b2, Algebra.AlgebraMethod.Maximize);
      int width = b1.Width;
      int height = b1.Height;

      BitmapData data1 = b1.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
      BitmapData data2 = b2.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p1 = (byte*)data1.Scan0;
        byte* p2 = (byte*)data2.Scan0;

        int offset = data1.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            for (int i = 0; i < 3; i++)
            {
              int power = (int)Math.Sqrt((p1[i] * p1[i] + p2[i] * p2[i]));
              p1[i] = (byte)(power > 255 ? 255 : power);
            } // i

            p1 += BPP;
            p2 += BPP;
          } // x

          p1 += offset;
          p2 += offset;
        } // y
      }

      b1.UnlockBits(data1);
      b2.UnlockBits(data2);

      Bitmap dstImage = (Bitmap)b1.Clone();

      b1.Dispose();
      b2.Dispose();

      return dstImage;
    } // end of Gradient


    /// <summary>
    /// �� Roberts ���ӽ��б�Ե���
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Roberts(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = new Bitmap(width, height);

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      int stride = srcData.Stride;
      int offset = stride - width * BPP;

      unsafe
      {
        byte* src = (byte*)srcData.Scan0;
        byte* dst = (byte*)dstData.Scan0;

        int A, B;   // A(x-1, y-1)    B(x, y-1)
        int C, D;   // C(x-1,   y)    D(x,   y)

        // ָ���һ��
        src += stride;
        dst += stride;

        // ���������ϱߺ������
        for (int y = 1; y < height; y++)
        {
          // ָ��ÿ�е�һ��
          src += BPP;
          dst += BPP;
          for (int x = 1; x < width; x++)
          {
            for (int i = 0; i < 3; i++)
            {
              A = src[i - stride - BPP];
              B = src[i - stride];
              C = src[i - BPP];
              D = src[i];

              dst[i] = (byte)(Math.Sqrt((A - D) * (A - D) + (B - C) * (B - C)));
            } // i

            dst[3] = src[3];

            src += BPP;
            dst += BPP;
          } // x

          src += offset;
          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Roberts


    /// <summary>
    /// �� Sobel ���ӽ��б�Ե���
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Sobel(Bitmap b)
    {
      Matrix3x3 m = new Matrix3x3();

      //   -1 -2 -1
      //    0  0  0
      //    1  2  1
      m.Init(0);
      m.TopLeft = m.TopRight = -1;
      m.BottomLeft = m.BottomRight = 1;
      m.TopMid = -2;
      m.BottomMid = 2;
      Bitmap b1 = m.Convolute((Bitmap)b.Clone());

      //   -1  0  1
      //   -2  0  2
      //   -1  0  1
      m.Init(0);
      m.TopLeft = m.BottomLeft = -1;
      m.TopRight = m.BottomRight = 1;
      m.MidLeft = -2;
      m.MidRight = 2;
      Bitmap b2 = m.Convolute((Bitmap)b.Clone());

      //    0  1  2
      //   -1  0  1
      //   -2 -1  0
      m.Init(0);
      m.TopMid = m.MidRight = 1;
      m.MidLeft = m.BottomMid = -1;
      m.TopRight = 2;
      m.BottomLeft = -2;
      Bitmap b3 = m.Convolute((Bitmap)b.Clone());

      //   -2 -1  0
      //   -1  0  1
      //    0  1  2
      m.Init(0);
      m.TopMid = m.MidLeft = -1;
      m.MidRight = m.BottomMid = 1;
      m.TopLeft = -2;
      m.BottomRight = 2;
      Bitmap b4 = m.Convolute((Bitmap)b.Clone());

      // �ݶ�����
      b = Gradient(Gradient(b1, b2), Gradient(b3, b4));

      b1.Dispose(); b2.Dispose(); b3.Dispose(); b4.Dispose();

      return b;
    } // end of Sobel


    /// <summary>
    /// �� Prewitt ���ӽ��б�Ե���
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Prewitt(Bitmap b)
    {
      Matrix3x3 m = new Matrix3x3();

      //   -1 -1 -1
      //    0  0  0
      //    1  1  1
      m.Init(0);
      m.TopLeft = m.TopMid = m.TopRight = -1;
      m.BottomLeft = m.BottomMid = m.BottomRight = 1;
      Bitmap b1 = m.Convolute((Bitmap)b.Clone());

      //   -1  0  1
      //   -1  0  1
      //   -1  0  1
      m.Init(0);
      m.TopLeft = m.MidLeft = m.BottomLeft = -1;
      m.TopRight = m.MidRight = m.BottomRight = 1;
      Bitmap b2 = m.Convolute((Bitmap)b.Clone());

      //   -1 -1  0
      //   -1  0  1
      //    0  1  1
      m.Init(0);
      m.TopLeft = m.MidLeft = m.TopMid = -1;
      m.BottomMid = m.BottomRight = m.MidRight = 1;
      Bitmap b3 = m.Convolute((Bitmap)b.Clone());

      //    0  1  1
      //   -1  0  1
      //   -1 -1  0
      m.Init(0);
      m.TopMid = m.TopRight = m.MidRight = 1;
      m.MidLeft = m.BottomLeft = m.BottomMid = -1;
      Bitmap b4 = m.Convolute((Bitmap)b.Clone());

      // �ݶ�����
      b = Gradient(Gradient(b1, b2), Gradient(b3, b4));

      b1.Dispose(); b2.Dispose(); b3.Dispose(); b4.Dispose();

      return b;
    } // end of Prewitt


    /// <summary>
    /// �� Kirsch ���ӽ��б�Ե���
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Kirsch(Bitmap b)
    {
      Matrix3x3 m = new Matrix3x3();

      //    5  5  5
      //   -3  0 -3
      //   -3 -3 -3
      m.Init(-3);
      m.Center = 0;
      m.TopLeft = m.TopMid = m.TopRight = 5;
      Bitmap b1 = m.Convolute((Bitmap)b.Clone());

      //   -3  5  5
      //   -3  0  5
      //   -3 -3 -3
      m.Init(-3);
      m.Center = 0;
      m.TopMid = m.TopRight = m.MidRight = 5;
      Bitmap b2 = m.Convolute((Bitmap)b.Clone());

      //   -3 -3  5
      //   -3  0  5
      //   -3 -3  5
      m.Init(-3);
      m.Center = 0;
      m.TopRight = m.MidRight = m.BottomRight = 5;
      Bitmap b3 = m.Convolute((Bitmap)b.Clone());

      //   -3 -3 -3
      //   -3  0  5
      //   -3  5  5
      m.Init(-3);
      m.Center = 0;
      m.MidRight = m.BottomRight = m.BottomMid = 5;
      Bitmap b4 = m.Convolute((Bitmap)b.Clone());

      //   -3 -3 -3
      //   -3  0 -3
      //    5  5  5
      m.Init(-3);
      m.Center = 0;
      m.BottomLeft = m.BottomMid = m.BottomRight = 5;
      Bitmap b5 = m.Convolute((Bitmap)b.Clone());

      //   -3 -3 -3
      //    5  0 -3
      //    5  5 -3
      m.Init(-3);
      m.Center = 0;
      m.MidLeft = m.BottomLeft = m.BottomMid = 5;
      Bitmap b6 = m.Convolute((Bitmap)b.Clone());

      //    5 -3 -3
      //    5  0 -3
      //    5 -3 -3
      m.Init(-3);
      m.Center = 0;
      m.TopLeft = m.MidLeft = m.BottomLeft = 5;
      Bitmap b7 = m.Convolute((Bitmap)b.Clone());

      //    5  5 -3
      //    5  0 -3
      //   -3 -3 -3
      m.Init(-3);
      m.Center = 0;
      m.TopLeft = m.MidLeft = m.TopMid = 5;
      Bitmap b8 = m.Convolute((Bitmap)b.Clone());

      // �ݶ�����
      Bitmap b9 = Gradient(Gradient(b1, b2), Gradient(b3, b4));
      Bitmap b10 = Gradient(Gradient(b5, b6), Gradient(b7, b8));
      b = Gradient(b9, b10);

      b1.Dispose(); b2.Dispose(); b3.Dispose(); b4.Dispose();
      b5.Dispose(); b6.Dispose(); b7.Dispose(); b8.Dispose();
      b9.Dispose(); b10.Dispose();

      return b;
    } // end of Kirsch


    /// <summary>
    /// �� GaussLaplacian ���ӽ��б�Ե���
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public Bitmap GaussLaplacian(Bitmap b)
    {
      int[,] kernel = { 
        {-2, -4, -4, -4, -2},
        {-4,  0,  8,  0, -4},
        {-4,  8, 24,  8, -4},
        {-4,  0,  8,  0, -4},
        {-2, -4, -4, -4, -2}
      };

      MatrixNxN m = new MatrixNxN();
      m.Kernel = kernel;

      return m.Convolute(b);
    } // end of GaussLaplacian


    /// <summary>
    /// ��ˮƽ��Ե������ӽ��б�Ե���
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public Bitmap EdgeDetectHorizontal(Bitmap b)
    {
      int[,] kernel = { 
        { 0,  0,  0,  0,  0,  0,  0},
        { 0,  0,  0,  0,  0,  0,  0},
        {-1, -1, -1, -1, -1, -1, -1},
        { 0,  0,  0,  0,  0,  0,  0},
        { 1,  1,  1,  1,  1,  1,  1},
        { 0,  0,  0,  0,  0,  0,  0},
        { 0,  0,  0,  0,  0,  0,  0},
      };

      MatrixNxN m = new MatrixNxN();
      m.Kernel = kernel;

      return m.Convolute(b);
    } // end of EdgeDetectHorizontal


    /// <summary>
    /// ����ֱ��Ե������ӽ��б�Ե���
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public Bitmap EdgeDetectVertical(Bitmap b)
    {
      int[,] kernel = { 
        { 0,  0, -1,  0,  1,  0,  0},
        { 0,  0, -1,  0,  1,  0,  0},
        { 0,  0, -1,  0,  1,  0,  0},
        { 0,  0, -1,  0,  1,  0,  0},
        { 0,  0, -1,  0,  1,  0,  0},
        { 0,  0, -1,  0,  1,  0,  0},
        { 0,  0, -1,  0,  1,  0,  0},
      };

      MatrixNxN m = new MatrixNxN();
      m.Kernel = kernel;

      return m.Convolute(b);
    } // end of EdgeDetectVertical


    /// <summary>
    /// ��Ե��ǿ
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="threshold">��ֵ</param>
    /// <returns></returns>
    public Bitmap EdgeEnhance(Bitmap b, int threshold)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      // ͼ��ʵ�ʴ�������
      // ���������� 1 �к����� 1 ��
      // ���������� 1 �к����� 1 ��
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
        int maxPixel = 0;


        // ָ��� 1 ��
        src += stride;
        dst += stride;
        for (int y = rectTop; y < rectBottom; y++)
        {
          // ָ��ÿ�е� 1 ������
          src += BPP;
          dst += BPP;

          for (int x = rectLeft; x < rectRight; x++)
          {
            // Alpha
            dst[3] = src[3];

            // ���� B, G, R ������
            for (int i = 0; i < 3; i++)
            {
              // ����-����
              maxPixel = src[i - stride + BPP] - src[i + stride - BPP];
              if (maxPixel < 0) maxPixel = -maxPixel;

              // ����-����
              pixel = src[i - stride - BPP] - src[i + stride + BPP];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // ��-��
              pixel = src[i - stride] - src[i + stride];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // ��-��
              pixel = src[i - BPP] - src[i + BPP];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // ������ֵ�ж�
              if (maxPixel < threshold) maxPixel = 0;
              
              dst[i] = (byte)maxPixel;
            }

            // �����һ����
            src += BPP;
            dst += BPP;
          } // x

          // ������һ��
          // �����ע��Ҫ���� 1 �У������ұ߻��� 1 �в��ش���
          src += offset + BPP;
          dst += offset + BPP;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of EdgeEnhance


    /// <summary>
    /// ��Ե���⻯
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="threshold">��ֵ</param>
    /// <returns></returns>
    public Bitmap EdgeHomogenize(Bitmap b, int threshold)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      // ͼ��ʵ�ʴ�������
      // ���������� 1 �к����� 1 ��
      // ���������� 1 �к����� 1 ��
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
        int maxPixel = 0;

        // ָ��� 1 ��
        src += stride;
        dst += stride;
        for (int y = rectTop; y < rectBottom; y++)
        {
          // ָ��ÿ�е� 1 ������
          src += BPP;
          dst += BPP;

          for (int x = rectLeft; x < rectRight; x++)
          {
            // Alpha
            dst[3] = src[3];

            // ���� B, G, R ������
            for (int i = 0; i < 3; i++)
            {
              // ��
              maxPixel = src[i] - src[i - stride];
              if (maxPixel < 0) maxPixel = -maxPixel;

              // ����
              pixel = src[i] - src[i - stride + BPP];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // ��
              pixel = src[i] - src[i + BPP];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // ����
              pixel = src[i] - src[i + stride + BPP];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // ��
              pixel = src[i] - src[i + stride];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // ����
              pixel = src[i] - src[i + stride - BPP];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // ��
              pixel = src[i] - src[i - BPP];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // ����
              pixel = src[i] - src[i - stride - BPP];
              if (pixel < 0) pixel = -pixel;
              if (pixel > maxPixel) maxPixel = pixel;

              // ������ֵ�ж�
              if (maxPixel < threshold) maxPixel = 0;

              dst[i] = (byte)maxPixel;
            }

            // �����һ����
            src += BPP;
            dst += BPP;
          } // x

          // ������һ��
          // �����ע��Ҫ���� 1 �У������ұ߻��� 1 �в��ش���
          src += offset + BPP;
          dst += offset + BPP;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of EdgeHomogenize


  }
}
