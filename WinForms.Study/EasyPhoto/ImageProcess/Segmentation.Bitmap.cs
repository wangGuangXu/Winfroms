using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace EasyPhoto.ImageProcess
{
  partial class Segmentation
  {
    /************************************************************
     * 
     * ������ܳ���������ʾ������С�����������١���ȡ��ͶӰ
     * 
     ************************************************************/


    /// <summary>
    /// ��ȡÿ������������Ϣ
    /// </summary>
    /// <param name="b">��ֵλͼ��</param>
    /// <returns></returns>
    public int[] ImageArea(Bitmap b)
    {
      // ��ԭʼ��ֵͼת��Ϊ��ά����
      byte[,] srcGray = Image2Array(b);

      // ����������
      ushort[,] Sign = ImageSign(srcGray);

      // �������
      int[] Area = ImageArea(Sign);

      return Area;
    } // end of ImageArea


    /// <summary>
    /// ��ȡÿ��������ܳ���Ϣ
    /// </summary>
    /// <param name="b">��ֵλͼ��</param>
    /// <returns></returns>
    public int[] ImagePerimeter(Bitmap b)
    {
      // ��ԭʼ��ֵͼת��Ϊ��ά����
      byte[,] srcGray = Image2Array(b);

      // ����������
      ushort[,] Sign = ImageSign(srcGray);

      // �����ܳ�
      int[] Perimeter = ImagePerimeter(Sign);

      return Perimeter;
    } // end of ImagePerimeter


    /// <summary>
    /// ��ָ��������Ż��Ƴ���Ӧ������
    /// </summary>
    /// <param name="b">��ֵλͼ��</param>
    /// <param name="Region">�����</param>
    /// <param name="showContour">ָ��boolֵ������ʾ�����ߣ�����ʾ�����</param>
    /// <returns></returns>
    public Bitmap ImageRegion(Bitmap b, ushort[] Region, bool showContour)
    {
      // ��ԭʼ��ֵͼת��Ϊ��ά����
      byte[,] srcGray = Image2Array(b);

      // ����������
      ushort[,] Sign = ImageSign(srcGray);

      // �������߽�����ʾ
      if (showContour)
        Sign = ContourTrace(Sign);

      int len = Region.Length;

      int width = b.Width;
      int height = b.Height;

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
            ushort sign = Sign[x, y];
            bool showRegion = false;

            for (int i = 0; i < len; i++)
            {
              if (sign == Region[i])
              {
                showRegion = true;
                break;
              }
            } // i

            // ��������
            if (showRegion)
            {
              p[0] = p[1] = p[2] = 0;
            }
            else
            {
              p[0] = p[1] = p[2] = 255;
            }

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of ImageRegion


    /// <summary>
    /// ����С����
    /// </summary>
    /// <param name="b">��ֵλͼ��</param>
    /// <param name="percent">�������ռͼ������İٷֱ�[0, 100]</param>
    /// <returns></returns>
    public Bitmap ClearSmallArea(Bitmap b, int percent)
    {
      // ��ԭʼ��ֵͼת��Ϊ��ά����
      byte[,] srcGray = Image2Array(b);

      // ����������
      ushort[,] Sign = ImageSign(srcGray);

      // �������
      int[] Area = ImageArea(Sign);

      int width = b.Width;
      int height = b.Height;

      // �����ֵ
      int area = width * height * percent / 100;

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
            ushort sign = Sign[x, y];

            // ��ԭʼ������С����ȫ����Ϊ����
            if (sign == 0 || Area[sign] < area)
            {
              p[0] = p[1] = p[2] = 255;
            }
            else
            {
              p[0] = p[1] = p[2] = 0;
            }

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of ClearSmallArea


    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="b">��ֵλͼ��</param>
    /// <returns></returns>
    public Bitmap ContourTrace(Bitmap b)
    {
      // ��ԭʼ��ֵͼת��Ϊ��ά����
      byte[,] srcGray = Image2Array(b);

      // ����������
      ushort[,] Sign = ImageSign(srcGray);

      // ��������
      ushort[,] Boundary = ContourTrace(Sign);

      int width = b.Width;
      int height = b.Height;

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
            if (Boundary[x, y] != 0)
            {
              p[0] = p[1] = p[2] = 0;
            }
            else
            {
              p[0] = p[1] = p[2] = 255;
            }

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of ContourTrace


    /// <summary>
    /// ������ȡ
    /// </summary>
    /// <param name="b">��ֵλͼ��</param>
    /// <returns></returns>
    public Bitmap ContourPick(Bitmap b)
    {
      // ��ԭʼ��ֵͼת��Ϊ��ά����
      byte[,] srcGray = Image2Array(b);

      // ������ȡ
      byte[,] dstGray = ContourPick(srcGray);

      // ת��Ϊ�Ҷ�ͼ��
      return Array2Image(dstGray);
    } // end of ContourPick


    /// <summary>
    /// ͼ��ͶӰ
    /// </summary>
    /// <param name="b">��ֵλͼ��</param>
    /// <param name="isHorz">�Ƿ�ˮƽͶӰ</param>
    /// <returns></returns>
    public Bitmap Project(Bitmap b, bool isHorz)
    {
      // ��ԭʼ��ֵͼת��Ϊ��ά����
      byte[,] srcGray = Image2Array(b);

      // ͼ��ͶӰ
      byte[,] dstGray = Project(srcGray, isHorz);

      return Array2Image(dstGray);
    } // end of Project


  }
}
