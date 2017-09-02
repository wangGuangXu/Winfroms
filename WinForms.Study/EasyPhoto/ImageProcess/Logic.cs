using System;
using System.Drawing;

namespace EasyPhoto.ImageProcess
{
  /// <summary>
  /// �߼�������
  /// </summary>
  public class Logic : GrayProcessing
  {
    /// <summary>
    /// �߼����㷽��
    /// </summary>
    public enum LogicMethod
    {
      /// <summary>
      /// ������
      /// </summary>
      And,

      /// <summary>
      /// ������
      /// </summary>
      Or,

      /// <summary>
      /// �������
      /// </summary>
      Xor
    }

    /// <summary>
    /// ��ȡ�����ñ�������
    /// </summary>
    public Region BackgroundRegion
    {
      get
      {
        return bgRegion;
      }
      set
      {
        bgRegion = value;
      }
    }
    private Region bgRegion = new Region();

    /// <summary>
    /// ��ȡ������ǰ������
    /// </summary>
    public Region ForegroundRegion
    {
      get
      {
        return fgRegion;
      }
      set
      {
        fgRegion = value;
      }
    }
    private Region fgRegion = new Region();


    /// <summary>
    /// ͼ���߼�����
    /// </summary>
    /// <param name="bgImage">��ֵ����</param>
    /// <param name="fgImage">��ֵǰ��</param>
    /// <param name="logicMethod">�߼����㷽��</param>
    /// <returns></returns>
    public Bitmap LogicOperate(Bitmap bgImage, Bitmap fgImage, LogicMethod logicMethod)
    {
      Bitmap dstImage = (Bitmap)bgImage.Clone();
      Graphics g = System.Drawing.Graphics.FromImage(dstImage);

      // ������Ч����
      Region validRegion = bgRegion;
      validRegion.Intersect(fgRegion);
      RectangleF validRect = validRegion.GetBounds(g);
      RectangleF fgRect = fgRegion.GetBounds(g);

      RegionClip bgRegionClip = new RegionClip(validRegion);
      Bitmap background = bgRegionClip.Hold((Bitmap)bgImage.Clone());

      RegionClip fgRegionClip = new RegionClip(validRegion);
      validRegion.Translate(-fgRect.X, -fgRect.Y);
      Bitmap foreground = fgRegionClip.Hold((Bitmap)fgImage.Clone());
      validRegion.Translate(fgRect.X, fgRect.Y);

      // �Ƚ�ԭʼ��ֵͼת��Ϊ��ά����
      byte[,] bgGray = Image2Array(background);
      byte[,] fgGray = Image2Array(foreground);

      // �����߼����㴦���ĻҶȶ�ά����
      byte[,] dstGray = null;

      // �����߼�����
      switch (logicMethod)
      {
        case LogicMethod.And:
          dstGray = LogicAnd(bgGray, fgGray);
          break;

        case LogicMethod.Or:
          dstGray = LogicOr(bgGray, fgGray);
          break;

        case LogicMethod.Xor:
          dstGray = LogicXor(bgGray, fgGray);
          break;
      }

      // ����ֵ����ת��Ϊ�Ҷ�ͼ
      Bitmap validImage = Array2Image(dstGray);

      g.DrawImage(validImage, validRect,
        new Rectangle(0, 0, (int)validRect.Width, (int)validRect.Height), GraphicsUnit.Pixel);

      bgImage.Dispose();
      fgImage.Dispose();

      return dstImage;
    } // end of LogicOperate


    /// <summary>
    /// ͼ���߼�������
    /// </summary>
    /// <param name="bg">������ֵ������</param>
    /// <param name="fg">ǰ����ֵ������</param>
    /// <returns></returns>
    private byte[,] LogicAnd(byte[,] bg, byte[,] fg)
    {
      int width = bg.GetLength(0);
      int height = bg.GetLength(1);

      // ��ʼ��Ŀ������Ϊ 255������ɫ
      byte[,] dst = InitArray(width, height, 255);

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          dst[x, y] = (byte)(bg[x, y] & fg[x, y]);
        } // x
      } // y

      return dst;
    } // end of LogicAnd


    /// <summary>
    /// ͼ���߼�������
    /// </summary>
    /// <param name="bg">������ֵ������</param>
    /// <param name="fg">ǰ����ֵ������</param>
    /// <returns></returns>
    private byte[,] LogicOr(byte[,] bg, byte[,] fg)
    {
      int width = bg.GetLength(0);
      int height = bg.GetLength(1);

      // ��ʼ��Ŀ������Ϊ 255������ɫ
      byte[,] dst = InitArray(width, height, 255);

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          dst[x, y] = (byte)(bg[x, y] | fg[x, y]);
        } // x
      } // y

      return dst;
    } // end of LogicOr


    /// <summary>
    /// �߼�������
    /// </summary>
    /// <param name="b">��ֵλͼ��</param>
    /// <returns></returns>
    public Bitmap LogicNot(Bitmap b)
    {
      // �Զ�ֵͼ��ֱ�ӽ��и�����
      Adjustment a = new Adjustment();
      b = a.Invert(b);

      return b;
    } // end of LogicNot


    /// <summary>
    /// ͼ���߼��������
    /// </summary>
    /// <param name="bg">������ֵ������</param>
    /// <param name="fg">ǰ����ֵ������</param>
    /// <returns></returns>
    private byte[,] LogicXor(byte[,] bg, byte[,] fg)
    {
      int width = bg.GetLength(0);
      int height = bg.GetLength(1);

      // ��ʼ��Ŀ������Ϊ 255������ɫ
      byte[,] dst = InitArray(width, height, 255);

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          dst[x, y] = (byte)(bg[x, y] ^ fg[x, y]);
        } // x
      } // y

      return dst;
    } // end of LogicXor


  }
}
