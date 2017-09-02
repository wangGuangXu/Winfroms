using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using EasyPhoto.ColorSpace;

namespace EasyPhoto.ImageProcess
{
  /// <summary>
  /// ͼ�������
  /// </summary>
  public class Adjustment : ImageInfo
  {
    /************************************************************
     * 
     * ɫ��ƽ�⡢���ȡ��Աȶȡ�HSL ������Gamma ����
     * 
     ************************************************************/


    /// <summary>
    /// ͼ��ɫ��ƽ��
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="red">��ɫ����[-255, 255]</param>
    /// <param name="green">��ɫ����[-255, 255]</param>
    /// <param name="blue">��ɫ����[-255, 255]</param>
    /// <returns></returns>
    public Bitmap ColorBalance(Bitmap b, int red, int green, int blue)
    {
      if (red < -255) red = -255;
      if (red > 255) red = 255;
      if (green < -255) green = -255;
      if (green > 255) green = 255;
      if (blue < -255) blue = -255;
      if (blue > 255) blue = 255;

      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int pixel;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            pixel = p[2] + red;
            if (pixel < 0) pixel = 0;
            if (pixel > 255) pixel = 255;
            p[2] = (byte)pixel;

            pixel = p[1] + green;
            if (pixel < 0) pixel = 0;
            if (pixel > 255) pixel = 255;
            p[1] = (byte)pixel;

            pixel = p[0] + blue;
            if (pixel < 0) pixel = 0;
            if (pixel > 255) pixel = 255;
            p[0] = (byte)pixel;

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of ColorBalance


    /// <summary>
    /// ͼ�����ȵ���
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="degree">����ֵ[-255, 255]</param>
    /// <returns></returns>
    public Bitmap Brightness(Bitmap b, int degree)
    {
      if (degree < -255) degree = -255;
      if (degree > 255) degree = 255;

      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int pixel = 0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // �������� B, G, R ����������
            for (int i = 0; i < 3; i++)
            {
              pixel = p[i] + degree;

              if (pixel < 0) pixel = 0;
              if (pixel > 255) pixel = 255;

              p[i] = (byte)pixel;
            } // i

            p += BPP;
          }  // x
          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Brightness


    /// <summary>
    /// ͼ��Աȶȵ���
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="degree">�Աȶ�[-100, 100]</param>
    /// <returns></returns>
    public Bitmap Contrast(Bitmap b, int degree)
    {
      if (degree < -100) degree = -100;
      if (degree > 100) degree = 100;

      double pixel = 0;
      double contrast = (100.0 + degree) / 100.0;
      contrast *= contrast;

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
            // ����ָ��λ�����صĶԱȶ�
            for (int i = 0; i < 3; i++)
            {
              pixel = ((p[i] / 255.0 - 0.5) * contrast + 0.5) * 255;
              if (pixel < 0) pixel = 0;
              if (pixel > 255) pixel = 255;
              p[i] = (byte)pixel;
            } // i

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Contrast


    /// <summary>
    /// ��ָ����ɫ�������Ͷȡ����ȶ�ͼ����е���
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="hue">ɫ��[-180, 180]</param>
    /// <param name="saturation">���Ͷ�[-1, 1]</param>
    /// <param name="luminance">����[-1, 1]</param>
    /// <returns></returns>
    public Bitmap AdjustHsl(Bitmap b, float hue, float saturation, float luminance)
    {
      if (hue < -180.0f) hue = -180.0f;
      if (hue > 180.0f) hue = 180f;
      if (saturation < -1.0f) saturation = -1.0f;
      if (saturation > 1.0f) saturation = 1.0f;
      if (luminance < -1.0f) luminance = -1.0f;
      if (luminance > 1.0f) luminance = 1.0f;

      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = new Bitmap(width, height, b.PixelFormat);

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
            HSL hsl = HSL.FromRgb(p[2], p[1], p[0]);
            hsl.Hue += hue;
            hsl.Saturation += saturation;
            hsl.Luminance += luminance;

            p[0] = hsl.GetBlue();
            p[1] = hsl.GetGreen();
            p[2] = hsl.GetRed();

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of AdjustHsl


    /// <summary>
    /// ͼ�� Gamma ����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="degree">Gamma ������[0.1, 5.0]</param>
    /// <returns></returns>
    public Bitmap GammaCorrect(Bitmap b, double degree)
    {
      if (degree < 0.1) degree = 0.1;
      if (degree > 5.0) degree = 5.0;

      byte[] Gamma = new byte[256];
      double g = 1 / degree;

      // ���� Gamma ����ӳ���
      for (int i = 0; i < 256; i++)
      {
        int pixel = (int)((255.0 * Math.Pow(i / 255.0, g)) + 0.5);
        Gamma[i] = (byte)((pixel > 255) ? 255 : pixel);
      } // i

      // ���� Gamma ����ӳ����ͼ��ɫ�ʽ���ӳ�䴦��
      return Mapping(b, Gamma, ChannelMode.White);
    } // end of GammaCorrect


    /************************************************************
     * 
     * ���񡢽�����α��ɫ
     * 
     ************************************************************/


    /// <summary>
    /// ��ת������
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Invert(Bitmap b)
    {
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
            // ��255����������㣬�൱�ڰ�λ��
            p[2] ^= 0xFF;
            p[1] ^= 0xFF;
            p[0] ^= 0xFF;

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Invert


    /// <summary>
    /// ���淴ת
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Interleaving(Bitmap b)
    {
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
            // ��ָ��λ�õ�������ɫ��ת
            if ((x + y) % 2 == 0)
            {
              p[0] ^= 0xFF;
              p[1] ^= 0xFF;
              p[2] ^= 0xFF;
            }

            p += BPP;
          } // x

          p += offset;
        } // y
      }
      b.UnlockBits(data);

      return b;
    } // end of Interleaving


    /// <summary>
    /// ������������ʽӳ��α��ɫ
    /// </summary>
    /// <param name="b">�Ҷ�λͼ��</param>
    /// <returns></returns>
    public Bitmap PseudoColor(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B, gray;
        R = G = B = gray = 0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // ��Ϊ�Ѿ��ǻҶ�ɫ��
            // ����ֻȡ��ɫ������Ϊ�ҶȽ�������
            gray = p[0];

            // α��ɫ����
            switch (gray / 64)
            {
              case 0:
                R = 0;
                G = 4 * gray;
                B = 255;
                break;

              case 1:
                R = 0;
                G = 255;
                B = 511 - 4 * gray;
                break;

              case 2:
                R = 4 * gray - 511;
                G = 255;
                B = 0;
                break;

              case 3:
                R = 255;
                G = 1023 - 4 * gray;
                B = 0;
                break;
            }

            p[0] = (byte)B;
            p[1] = (byte)G;
            p[2] = (byte)R;

            p += BPP;
          } // x
          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of PseudoColor


    /// <summary>
    /// ��ɫ�ʱ���ʽӳ��α��ɫ
    /// </summary>
    /// <param name="b">�Ҷ�λͼ��</param>
    /// <param name="colorTable">ɫ��ӳ���</param>
    /// <returns></returns>
    public Bitmap PseudoColor(Bitmap b, Color[] colorTable)
    {
      int width = b.Width;
      int height = b.Height;

      // ��ɫ���ձ�
      int lenColorTable = colorTable.Length;
      uint[] ColorTable = new uint[lenColorTable];

      // ����ɫת��Ϊ����
      for (int i = 0; i < lenColorTable; i++)
      {
        ColorTable[i] = (uint)((colorTable[i].A << 24) |
          (colorTable[i].R << 16) |
          (colorTable[i].G << 8) |
          (colorTable[i].B << 0));
      } // i

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int gray = 0;
        uint color = 0;

        // �����������ѭ���У�����ɫ�ʱ�����Խ��
        lenColorTable--;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // ��Ϊ�Ѿ��ǻҶ�ɫ��
            // ����ֻȡ��ɫ������Ϊ�ҶȽ�������
            gray = p[0];

            // ת���Ҷȼ�����ӳ�䵽ɫ�ʱ�
            color = ColorTable[lenColorTable * gray / 255];

            p[3] = (byte)(color >> 24); // A
            p[2] = (byte)(color >> 16); // R
            p[1] = (byte)(color >> 8);  // G
            p[0] = (byte)(color);       // B

            p += BPP;
          } // x
          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of PseudoColor


    /************************************************************
     * 
     * �ֻ�ͨ������ȡͨ��������ͨ��
     * 
     ************************************************************/


    /// <summary>
    /// �ֻ�ͨ��
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap RotateChannel(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* src = (byte*)srcData.Scan0;
        byte* dst = (byte*)dstData.Scan0;
        int offset = srcData.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            dst[2] = src[1]; // R <- G
            dst[1] = src[0]; // G <- B
            dst[0] = src[2]; // B <- R

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
    } // end of RotateChannel


    /// <summary>
    /// ͨ��ģʽ
    /// </summary>
    public enum ChannelMode : int
    { 
      /// <summary>
      /// ��ɫͨ��
      /// </summary>
      Blue = 1,

      /// <summary>
      /// ��ɫͨ��
      /// </summary>
      Green = 2,

      /// <summary>
      /// ��ɫͨ��
      /// </summary>
      Red = 4,

      /// <summary>
      /// Alpha ͨ��
      /// </summary>
      Alpha = 8,

      /// <summary>
      /// ��ɫ = ��ɫ + ��ɫ
      /// </summary>
      Cyan = 3,

      /// <summary>
      /// Ʒ�� = ��ɫ + ��ɫ
      /// </summary>
      Megenta = 5,

      /// <summary>
      /// ��ɫ = ��ɫ + ��ɫ
      /// </summary>
      Yellow = 6,

      /// <summary>
      /// ��ɫ = ��ɫ + ��ɫ + ��ɫ
      /// </summary>
      White = 7
    }
    

    /// <summary>
    /// ��ȡͨ��
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="channelMode">ͨ��ģʽ[A, R, G, B]</param>
    /// <returns></returns>
    public Bitmap ExtractChannel(Bitmap b, ChannelMode channelMode)
    {
      int channel = (int)Math.Log((double)channelMode,2.0);
       
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* src = (byte*)srcData.Scan0;
        byte* dst = (byte*)dstData.Scan0;
        int offset = srcData.Stride - width * BPP;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            dst[0] = dst[1] = dst[2] = src[channel];

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
    } // end of ExtractChannel


    /// <summary>
    /// ����ͨ��
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="channelMode">ͨ��ģʽ</param>
    /// <returns></returns>
    public Bitmap FilterChannel(Bitmap b, ChannelMode channelMode)
    {
      int channel = (int)channelMode;

      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int i = 0; i < 3; i++)
        {
          if (((int)Math.Pow(2, i) & channel) > 0)
            continue;

          p = (byte*)data.Scan0;
          for (int y = 0; y < height; y++)
          {
            for (int x = 0; x < width; x++)
            {
              p[i] = 0;

              p += BPP;
            } // x

            p += offset;
          } // y
        } // i
      }

      b.UnlockBits(data);

      return b;
    } // end of FilterChannel


    /************************************************************
     * 
     * ӳ��
     * 
     ************************************************************/


    /// <summary>
    /// ͼ��ɫ��ӳ��
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="Map">ӳ���</param>
    /// <param name="channelMode">ͨ��ģʽ</param>
    /// <returns></returns>
    public Bitmap Mapping(Bitmap b, byte[] Map, ChannelMode channelMode)
    {
      int channel = (int)channelMode;

      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        for (int i = 0; i < 3; i++)
        {
          if (((int)Math.Pow(2, i) & channel) == 0)
            continue;

          p = (byte*)data.Scan0;
          for (int y = 0; y < height; y++)
          {
            for (int x = 0; x < width; x++)
            {
              p[i] = Map[p[i]];

              p += BPP;
            } // x

            p += offset;
          } // y
        } // i
      }

      b.UnlockBits(data);

      return b;
    } // end of Mapping


  }
}
