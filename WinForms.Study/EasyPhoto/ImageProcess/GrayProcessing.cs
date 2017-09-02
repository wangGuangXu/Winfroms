using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace EasyPhoto.ImageProcess
{
  /// <summary>
  /// �Ҷȴ�����
  /// </summary>
  public class GrayProcessing : ImageInfo
  {
    /// <summary>
    /// �Ҷȷ���
    /// </summary>
    public enum GrayMethod
    {
      /// <summary>
      /// ��Ȩƽ����
      /// </summary>
      WeightAveraging,

      /// <summary>
      /// ���ֵ��
      /// </summary>
      Maximum,

      /// <summary>
      /// ƽ��ֵ��
      /// </summary>
      Average
    }


    /************************************************************
     * 
     * ��ʼ����ά���顢λͼת���顢����תλͼ
     * 
     ************************************************************/


    /// <summary>
    /// ��ʼ��һ����ά����
    /// </summary>
    /// <param name="width">�����</param>
    /// <param name="height">�����</param>
    /// <param name="init">��ʼֵ</param>
    /// <returns></returns>
    public byte[,] InitArray(int width, int height, byte init)
    {
      byte[,] dst = new byte[width, height];

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          dst[x, y] = init;
        } // x
      } // y

      return dst;
    } // end of InitArray


    /// <summary>
    /// ���Ҷ�λͼ��ת��Ϊ��ά����
    /// </summary>
    /// <param name="b">�Ҷ�λͼ��</param>
    /// <returns></returns>
    public byte[,] Image2Array(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      // ����һ����ά����
      byte[,] GrayArray = new byte[width, height];

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // ��Ϊ�Ѿ��ǻҶ�ɫ������ֻȡ��ɫ������Ϊ�Ҷ�
            GrayArray[x, y] = p[0];

            p += BPP;
          } //  x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return GrayArray;
    } // end of Image2Array


    /// <summary>
    /// ����ά����ת��Ϊ�Ҷ�λͼ��
    /// </summary>
    /// <param name="GrayArray">�Ҷ�����</param>
    /// <returns></returns>
    public Bitmap Array2Image(byte[,] GrayArray)
    {
      int width = GrayArray.GetLength(0);
      int height = GrayArray.GetLength(1);

      Bitmap b = new Bitmap(width, height);
      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            p[0] = p[1] = p[2] = GrayArray[x, y];
            p[3] = 255;

            p += BPP;
          } //  x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Array2Image


    /************************************************************
     * 
     * λͼת��ά���顢��ֵ����תλͼ����ֵ��������Ӧ��ֵ���Ҷ�
     * 
     ************************************************************/


    /// <summary>
    /// ��λͼ��ת��Ϊ��ֵ����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="threshold">��ֵ</param>
    /// <returns></returns>
    public byte[,] BinaryArray(Bitmap b, byte threshold)
    {
      int width = b.Width;
      int height = b.Height;

      //�Ƚ�λͼ�ҶȻ�
      b = Gray(b, GrayMethod.WeightAveraging);

      // ���Ҷ�ͼת��Ϊ�Ҷ�����
      byte[,] GrayArray = Image2Array(b);

      b.Dispose();

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          // ��ǰ������ɫ�Ҷ�ֵ��ָ����ֵ��Ƚ�
          if (GrayArray[x, y] >= threshold)
            GrayArray[x, y] = 255;
          else
            GrayArray[x, y] = 0;
        } //  x
      } // y

      return GrayArray;
    } // end of BinaryArray


    /// <summary>
    /// ����ֵ����ת��Ϊ˫ɫλͼ��
    /// </summary>
    /// <param name="GrayArray">��ֵ����</param>
    /// <param name="bgColor">����ɫ</param>
    /// <param name="fgColor">ǰ��ɫ</param>
    /// <returns></returns>
    public Bitmap BinaryImage(byte[,] GrayArray, Color bgColor, Color fgColor)
    {
      // ��ȡ��ά����Ŀ��
      int width = GrayArray.GetLength(0);
      int height = GrayArray.GetLength(1);

      // ����ɫ
      byte bgAlpha = (byte)bgColor.A;
      byte bgRed = (byte)bgColor.R;
      byte bgGreen = (byte)bgColor.G;
      byte bgBlue = (byte)bgColor.B;

      // ǰ��ɫ
      byte fgAlpha = (byte)fgColor.A;
      byte fgRed = (byte)fgColor.R;
      byte fgGreen = (byte)fgColor.G;
      byte fgBlue = (byte)fgColor.B;

      Bitmap b = new Bitmap(width, height);
      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // ����ͼ��ǰ����������ɫ
            if (GrayArray[x, y] <128 )
            {
              p[3] = fgAlpha;
              p[2] = fgRed;
              p[1] = fgGreen;
              p[0] = fgBlue;
            }
            else
            {
              p[3] = bgAlpha;
              p[2] = bgRed;
              p[1] = bgGreen;
              p[0] = bgBlue;
            }

            p += BPP;
          } //  x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of BinaryImage


    /// <summary>
    /// ��ֵ��
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="threshold">��ֵ</param>
    /// <returns></returns>
    public Bitmap Thresholding(Bitmap b, byte threshold)
    {
      byte[,] GrayArray = BinaryArray(b, threshold);
      Bitmap dstImage = BinaryImage(GrayArray, Color.White, Color.Black);

      return dstImage;
    } // end of Thresholding 


    /// <summary>
    /// ����Ӧ��ֵ
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap AutoFitThreshold(Bitmap b)
    {
      // ͼ��ҶȻ�
      GrayProcessing gp = new GrayProcessing();
      b = gp.Gray(b, GrayMethod.WeightAveraging);

      // ����ֱ��ͼ������ȡ�Ҷ�ͳ����Ϣ
      Histogram histogram = new Histogram(b);
      int [] GrayLevel = histogram.Red.Value;

      int peak1, peak2, valley;
      int peak1Index, peak2Index, valleyIndex;

      // ȡ˫��
      peak1 = peak2 = GrayLevel[0];
      peak1Index = peak2Index = 0;
      for (int i = 1; i < 256; i++)
      {
        // ��������µĸ߷壬�򽫵�һ���˾ӵڶ��壬�µĸ߷���Ϊ��һ��
        if (GrayLevel[i] > peak1)
        {
          peak2 = peak1;
          peak2Index = peak1Index;

          peak1 = GrayLevel[i];
          peak1Index = i;
        }
      } // i

      // �ж�������ֵ����
      int max = peak1Index;
      int min = peak2Index;
      if (max < min)
      {
        int t = max;
        max = min;
        min = t;
      }

      // ȡ���
      valley = GrayLevel[min];
      valleyIndex = min;
      for (int i = min; i < max; i++)
      {
        if (GrayLevel[i] < valley)
        {
          valley = GrayLevel[i];
          valleyIndex = i;
        }
      } // i

      // �����ҵ��Ĺ�ֵ��ͼ����ж�ֵ��
      return Thresholding(b, (byte)valleyIndex);
    } // end of AutoFitThreshold 


    /// <summary>
    /// ͼ��Ҷȱ任
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="grayMethod">�Ҷȷ���</param>
    /// <returns></returns>
    public Bitmap Gray(Bitmap b, GrayMethod grayMethod)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        byte R, G, B, gray;

        switch (grayMethod)
        {
          case GrayMethod.Maximum:
            for (int y = 0; y < height; y++)
            {
              for (int x = 0; x < width; x++)
              {
                R = p[2];
                G = p[1];
                B = p[0];

                // gray = Max( R, G, B )
                gray = (gray = B >= G ? B : G) >= R ? gray : R;

                p[0] = p[1] = p[2] = gray;

                p += BPP;
              } // x
              p += offset;
            } // y
            break;

          case GrayMethod.Average:
            for (int y = 0; y < height; y++)
            {
              for (int x = 0; x < width; x++)
              {
                R = p[2];
                G = p[1];
                B = p[0];

                // gray = ( R + G + B ) / 3
                gray = (byte)((R + G + B) / 3);

                p[0] = p[1] = p[2] = gray;

                p += BPP;
              } // x
              p += offset;
            } // y
            break;

          case GrayMethod.WeightAveraging:
          default:
            for (int y = 0; y < height; y++)
            {
              for (int x = 0; x < width; x++)
              {
                R = p[2];
                G = p[1];
                B = p[0];

                // gray = 0.3*R + 0.59*G + 0.11*B
                gray = (byte)((19661 * R + 38666 * G + 7209 * B) >> 16);

                p[0] = p[1] = p[2] = gray;

                p += BPP;
              } // x
              p += offset;
            } // y
            break;
        } // switch
      }

      b.UnlockBits(data);

      return b;
    } // end of Gray


  }
}
