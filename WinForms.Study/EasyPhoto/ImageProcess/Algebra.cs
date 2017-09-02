using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace EasyPhoto.ImageProcess
{
  /// <summary>
  /// ͼ�����������
  /// </summary>
  public class Algebra : ImageInfo
  {
    /// <summary>
    /// �������㷽��
    /// </summary>
    public enum AlgebraMethod
    {
      /// <summary>
      /// �ӷ����㣬���ȡ����֮��
      /// </summary>
      Add,

      /// <summary>
      /// �������㣬���ȡ����֮��
      /// </summary>
      Subtract,

      /// <summary>
      /// �˷����㣬���ȡ����֮��
      /// </summary>
      Multiply,

      /// <summary>
      /// �������㣬���ȡ����֮��
      /// </summary>
      Divide,

      /// <summary>
      /// ƽ�����㣬���ȡ����֮ƽ��ֵ
      /// </summary>
      Average,

      /// <summary>
      /// �������㣬�ҳ�����ͼ���еĲ�ͬ����
      /// </summary>
      Differ,

      /// <summary>
      /// Max ���㣬���ȡ����֮���ֵ
      /// </summary>
      Maximize,

      /// <summary>
      /// Min ���㣬���ȡ����֮��Сֵ
      /// </summary>
      Minimum
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
    /// ͼ���������
    /// </summary>
    /// <param name="bgImage">����</param>
    /// <param name="fgImage">ǰ��</param>
    /// <param name="algebraMethod">�������㷽��</param>
    /// <returns></returns>
    public Bitmap AlgebraOperate(Bitmap bgImage, Bitmap fgImage, AlgebraMethod algebraMethod)
    {
      // ������Ч����
      Region validRegion = bgRegion;
      validRegion.Intersect(fgRegion);

      RegionClip rc = new RegionClip(validRegion);
      Bitmap background = rc.Hold((Bitmap)bgImage.Clone());

      Bitmap dstImage = rc.Remove((Bitmap)bgImage.Clone());
      Graphics g = System.Drawing.Graphics.FromImage(dstImage);

      RectangleF validRect = validRegion.GetBounds(g);
      RectangleF fgRect = fgRegion.GetBounds(g);

      validRegion.Translate(-fgRect.X, -fgRect.Y);
      Bitmap foreground = rc.Hold((Bitmap)fgImage.Clone());
      validRegion.Translate(fgRect.X, fgRect.Y);

      Bitmap validImage = null;
      switch (algebraMethod)
      {
        case AlgebraMethod.Add:
          validImage = Add(background, foreground);
          break;

        case AlgebraMethod.Subtract:
          validImage = Subtract(background, foreground);
          break;

        case AlgebraMethod.Multiply:
          validImage = Multiply(background, foreground);
          break;

        case AlgebraMethod.Divide:
          validImage = Divide(background, foreground);
          break;

        case AlgebraMethod.Average:
          validImage = Average(background, foreground);
          break;

        case AlgebraMethod.Differ:
          validImage = Differ(background, foreground);
          break;

        case AlgebraMethod.Maximize:
          validImage = Maximize(background, foreground);
          break;

        case AlgebraMethod.Minimum:
          validImage = Minimum(background, foreground);
          break;
      }

      g.DrawImage(validImage, validRect,
        new Rectangle(0, 0, (int)validRect.Width, (int)validRect.Height), GraphicsUnit.Pixel);

      bgImage.Dispose();
      fgImage.Dispose();

      return dstImage;
    } // end of AlgebraOperate


    /// <summary>
    /// ͼ��ӷ�����
    /// </summary>
    /// <param name="bgImage">����</param>
    /// <param name="fgImage">ǰ��</param>
    /// <returns></returns>
    private Bitmap Add(Bitmap bgImage, Bitmap fgImage)
    {
      int width = bgImage.Width;
      int height = bgImage.Height;

      BitmapData bgData = bgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
      BitmapData fgData = fgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* bg = (byte*)bgData.Scan0;
        byte* fg = (byte*)fgData.Scan0;

        int offset = bgData.Stride - width * BPP;

        int pixel = 0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            for (int i = 0; i < 3; i++)
            {
              pixel = bg[i] + fg[i];
              if (pixel > 255) pixel = 255;

              bg[i] = (byte)pixel;
            } // i

            bg += BPP;
            fg += BPP;
          } // x

          bg += offset;
          fg += offset;
        } // y
      }

      bgImage.UnlockBits(bgData);
      fgImage.UnlockBits(fgData);

      Bitmap dstImage = (Bitmap)bgImage.Clone();

      bgImage.Dispose();
      fgImage.Dispose();

      return dstImage;
    } // end of Add


    /// <summary>
    /// ͼ���������
    /// </summary>
    /// <param name="bgImage">����</param>
    /// <param name="fgImage">ǰ��</param>
    /// <returns></returns>
    private Bitmap Subtract(Bitmap bgImage, Bitmap fgImage)
    {
      int width = bgImage.Width;
      int height = bgImage.Height;

      BitmapData bgData = bgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
      BitmapData fgData = fgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* bg = (byte*)bgData.Scan0;
        byte* fg = (byte*)fgData.Scan0;

        int offset = bgData.Stride - width * BPP;

        int pixel = 0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            for (int i = 0; i < 3; i++)
            {
              pixel = bg[i] - fg[i];
              if (pixel < 0) pixel = -pixel;

              bg[i] = (byte)pixel;
            } // i

            bg += BPP;
            fg += BPP;
          } // x

          bg += offset;
          fg += offset;
        } // y
      }

      bgImage.UnlockBits(bgData);
      fgImage.UnlockBits(fgData);

      Bitmap dstImage = (Bitmap)bgImage.Clone();

      bgImage.Dispose();
      fgImage.Dispose();

      return dstImage;
    } // end of Subtract


    /// <summary>
    /// ͼ��˷�����
    /// </summary>
    /// <param name="bgImage">����</param>
    /// <param name="fgImage">ǰ��</param>
    /// <returns></returns>
    private Bitmap Multiply(Bitmap bgImage, Bitmap fgImage)
    {
      int width = bgImage.Width;
      int height = bgImage.Height;

      BitmapData bgData = bgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
      BitmapData fgData = fgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* bg = (byte*)bgData.Scan0;
        byte* fg = (byte*)fgData.Scan0;

        int offset = bgData.Stride - width * BPP;

        int pixel = 0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            for (int i = 0; i < 3; i++)
            {
              pixel = (bg[i] * fg[i]) / 255;

              bg[i] = (byte)pixel;
            } // i

            bg += BPP;
            fg += BPP;
          } // x

          bg += offset;
          fg += offset;
        } // y
      }

      bgImage.UnlockBits(bgData);
      fgImage.UnlockBits(fgData);

      Bitmap dstImage = (Bitmap)bgImage.Clone();

      bgImage.Dispose();
      fgImage.Dispose();

      return dstImage;
    } // end of Multiply


    /// <summary>
    /// ͼ���������
    /// </summary>
    /// <param name="bgImage">����</param>
    /// <param name="fgImage">ǰ��</param>
    /// <returns></returns>
    private Bitmap Divide(Bitmap bgImage, Bitmap fgImage)
    {
      int width = bgImage.Width;
      int height = bgImage.Height;

      BitmapData bgData = bgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
      BitmapData fgData = fgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* bg = (byte*)bgData.Scan0;
        byte* fg = (byte*)fgData.Scan0;

        int offset = bgData.Stride - width * BPP;

        int pixel = 0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            for (int i = 0; i < 3; i++)
            {
              int divisor = fg[i];

              // �������Ϊ 0
              if (divisor == 0) divisor = 1;

              pixel = (bg[i] * 255) / divisor;

              bg[i] = (byte)pixel;
            } // i

            bg += BPP;
            fg += BPP;
          } // x

          bg += offset;
          fg += offset;
        } // y
      }

      bgImage.UnlockBits(bgData);
      fgImage.UnlockBits(fgData);

      Bitmap dstImage = (Bitmap)bgImage.Clone();

      bgImage.Dispose();
      fgImage.Dispose();

      return dstImage;
    } // end of Divide


    /// <summary>
    /// ͼ��ƽ������
    /// </summary>
    /// <param name="bgImage">����</param>
    /// <param name="fgImage">ǰ��</param>
    /// <returns></returns>
    private Bitmap Average(Bitmap bgImage, Bitmap fgImage)
    {
      int width = bgImage.Width;
      int height = bgImage.Height;

      BitmapData bgData = bgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
      BitmapData fgData = fgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* bg = (byte*)bgData.Scan0;
        byte* fg = (byte*)fgData.Scan0;

        int offset = bgData.Stride - width * BPP;

        int pixel = 0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            for (int i = 0; i < 3; i++)
            {
              pixel = (bg[i] + fg[i]) / 2;

              bg[i] = (byte)pixel;
            } // i

            bg += BPP;
            fg += BPP;
          } // x

          bg += offset;
          fg += offset;
        } // y
      }

      bgImage.UnlockBits(bgData);
      fgImage.UnlockBits(fgData);

      Bitmap dstImage = (Bitmap)bgImage.Clone();

      bgImage.Dispose();
      fgImage.Dispose();

      return dstImage;
    } // end of Average


    /// <summary>
    /// ͼ����������
    /// ���ǰ������ͼ������ͬ�Ĳ��֣���ʹ֮͸��
    /// </summary>
    /// <param name="bgImage">����</param>
    /// <param name="fgImage">ǰ��</param>
    /// <returns></returns>
    private Bitmap Differ(Bitmap bgImage, Bitmap fgImage)
    {
      int width = bgImage.Width;
      int height = bgImage.Height;

      BitmapData bgData = bgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
      BitmapData fgData = fgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* bg = (byte*)bgData.Scan0;
        byte* fg = (byte*)fgData.Scan0;

        int offset = bgData.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // ֻҪRGB��������ͬ�����Ӹ����ص���ͬ������͸����
            if (bg[0] == fg[0] && bg[1] == fg[1] && bg[2] == fg[2])
            {
              bg[3] = 0;
              bg[2] = bg[1] = bg[0] = 255;
            }

            bg += BPP;
            fg += BPP;
          } // x

          bg += offset;
          fg += offset;
        } // y
      }

      bgImage.UnlockBits(bgData);
      fgImage.UnlockBits(fgData);

      fgImage.Dispose();

      return bgImage;
    } // end of Differ


    /// <summary>
    /// Max ����
    /// </summary>
    /// <param name="bgImage">����</param>
    /// <param name="fgImage">ǰ��</param>
    /// <returns></returns>
    private Bitmap Maximize(Bitmap bgImage, Bitmap fgImage)
    {
      int width = bgImage.Width;
      int height = bgImage.Height;

      BitmapData bgData = bgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
      BitmapData fgData = fgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* bg = (byte*)bgData.Scan0;
        byte* fg = (byte*)fgData.Scan0;

        int offset = bgData.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            for (int i = 0; i < 3; i++)
            {
              bg[i] = (bg[i] > fg[i] ? bg[i] : fg[i]);
            } // i

            bg += BPP;
            fg += BPP;
          } // x

          bg += offset;
          fg += offset;
        } // y
      }

      bgImage.UnlockBits(bgData);
      fgImage.UnlockBits(fgData);

      Bitmap dstImage = (Bitmap)bgImage.Clone();

      bgImage.Dispose();
      fgImage.Dispose();

      return dstImage;
    } // end of Maximize


    /// <summary>
    /// Min ����
    /// </summary>
    /// <param name="bgImage">����</param>
    /// <param name="fgImage">ǰ��</param>
    /// <returns></returns>
    private Bitmap Minimum(Bitmap bgImage, Bitmap fgImage)
    {
      int width = bgImage.Width;
      int height = bgImage.Height;

      BitmapData bgData = bgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
      BitmapData fgData = fgImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* bg = (byte*)bgData.Scan0;
        byte* fg = (byte*)fgData.Scan0;

        int offset = bgData.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            for (int i = 0; i < 3; i++)
            {
              bg[i] = (bg[i] < fg[i] ? bg[i] : fg[i]);
            } // i

            bg += BPP;
            fg += BPP;
          } // x

          bg += offset;
          fg += offset;
        } // y
      }

      bgImage.UnlockBits(bgData);
      fgImage.UnlockBits(fgData);

      Bitmap dstImage = (Bitmap)bgImage.Clone();

      bgImage.Dispose();
      fgImage.Dispose();

      return dstImage;
    } // end of Minimum


  }
}
