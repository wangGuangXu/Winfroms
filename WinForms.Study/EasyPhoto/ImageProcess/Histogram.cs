using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace EasyPhoto.ImageProcess
{
  /// <summary>
  /// ֱ��ͼ��
  /// </summary>
  public class Histogram : ImageInfo
  {
    /// <summary>
    /// ֧�ֻ��Ƶ�ɫ��ģʽ
    /// </summary>
    public enum ColorMode
    {
      /// <summary>
      /// ��ɫ
      /// </summary>
      Red,

      /// <summary>
      /// ��ɫ
      /// </summary>
      Green,

      /// <summary>
      /// ��ɫ
      /// </summary>
      Blue
    }

    private Statistics red;
    private Statistics green;
    private Statistics blue;

    private Bitmap b;
    private int width = 0;
    private int height = 0;

    /// <summary>
    /// ��ȡ��ɫ����ͳ������
    /// </summary>
    public Statistics Red
    {
      get
      {
        return red;
      }
    }

    /// <summary>
    /// ��ȡ��ɫ����ͳ������
    /// </summary>
    public Statistics Green
    {
      get
      {
        return green;
      }
    }

    /// <summary>
    /// ��ȡ��ɫ����ͳ������
    /// </summary>
    public Statistics Blue
    {
      get
      {
        return blue;
      }
    }


    /// <summary>
    /// ����ͼ��ֱ��ͼ
    /// </summary>
    /// <param name="b">λͼ��</param>
    public Histogram(Bitmap b)
    {
      this.b = b;

      width = b.Width;
      height = b.Height;

      CountRgb();
    } // end of Histogram


    /// <summary>
    /// ͳ��ɫ�� R��G��B ������Ƶ��
    /// </summary>
    private void CountRgb()
    {
      int[] Red = new int[256];
      int[] Green = new int[256];
      int[] Blue = new int[256];

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            Red[p[2]]++;
            Green[p[1]]++;
            Blue[p[0]]++;

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      // ���� R��G��B ������ͳ����
      this.red = new Statistics(Red);
      this.green = new Statistics(Green);
      this.blue = new Statistics(Blue);
    } // end of CountRgb


    /// <summary>
    /// ����ֱ��ͼ
    /// </summary>
    /// <param name="diagramHeight">ͼ��߶�</param>
    /// <param name="viewByLog">�� Log ��������ֱ��ͼ</param>
    /// <param name="colorMode">ɫ��ģʽ</param>
    /// <returns></returns>
    public Bitmap DrawDiagram(int diagramHeight, bool viewByLog, ColorMode colorMode)
    {
      // ��ȡ���ȸ���
      double[] Probability = this.Red.Probability;

      // ���ȸ������ֵ
      double maxProbability = Probability[this.Red.MaxIndex];

      // ���ڻ���ֱ��ͼ����ɫ
      Color color = Color.Red;

      switch (colorMode)
      {
        case ColorMode.Red:
          Probability = this.Red.Probability;
          maxProbability = Probability[this.Red.MaxIndex];
          color = Color.Red;
          break;

        case ColorMode.Green:
          Probability = this.Green.Probability;
          maxProbability = Probability[this.Green.MaxIndex];
          color = Color.Green;
          break;

        case ColorMode.Blue:
          Probability = this.Blue.Probability;
          maxProbability = Probability[this.Blue.MaxIndex];
          color = Color.Blue;
          break;
      } // switch

      Pen pen = new Pen(color, 1);
      Bitmap dstImage = new Bitmap(256, diagramHeight);
      System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dstImage);
      int y = 0;

      // ����ֱ��ͼ
      for (int i = 0; i < 256; i++)
      {
        // ��ǰ���ȼ���������ȼ��ı���
        double percent = Probability[i] / maxProbability;

        if (viewByLog)
        {
          // ��Ϊ��ͬɫ�����ȼ������ܶȷֲ���ͬ���ֲ����߿���ƽ����Ҳ���ܶ��ͣ�
          // �������ֲ��죬���Ƕ�ɫ�ʸ��ʽ��ж��������������ֲ���������ڲ���
          y = (int)(diagramHeight * (1 - Math.Log(100 * percent + 1, 101)));
        }
        else
        {
          y = (int)(diagramHeight * (1 - percent));
        }

        g.DrawLine(pen, i, y, i, diagramHeight);
      } // i

      g.Save();
      g.Dispose();

      return dstImage;
    } // end of DrawDiagram


    /// <summary>
    /// ���ƾ���ֱ��ͼ���⻯�����ͼ��
    /// </summary>
    /// <returns></returns>
    public Bitmap Equalizer()
    {
      // ͼ���ͨ�����⻯��ӳ���
      byte[] RedMap = this.Red.Equalizer;
      byte[] GreenMap = this.Green.Equalizer;
      byte[] BlueMap = this.Blue.Equalizer;

      Bitmap dstImage = (Bitmap)b.Clone();

      // ͨ��ӳ��
      Adjustment a = new Adjustment();
      dstImage = a.Mapping(dstImage, RedMap, Adjustment.ChannelMode.Red);
      dstImage = a.Mapping(dstImage, GreenMap, Adjustment.ChannelMode.Green);
      dstImage = a.Mapping(dstImage, BlueMap, Adjustment.ChannelMode.Blue);

      return dstImage;
    } // end of Equalizer


  }
}

