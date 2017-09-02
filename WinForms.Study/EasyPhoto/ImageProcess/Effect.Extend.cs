using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace EasyPhoto.ImageProcess
{
  partial class Effect
  {
    /************************************************************
     * 
     * ��ֽ��Ǧ�ʻ������衢������
     * ���̡��غ֡���Ⱦ
     * ����������
     * �������Ե����ֵ�
     * 
     ************************************************************/


    /// <summary>
    /// ��ֽ
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="threshold">��ֵ[0�� 255]</param>
    /// <param name="bgColor">����ɫ</param>
    /// <param name="fgColor">ǰ��ɫ</param>
    /// <returns></returns>
    public Bitmap PaperCut(Bitmap b, byte threshold, Color bgColor, Color fgColor)
    {
      GrayProcessing gp = new GrayProcessing();

      // ��ͼ��ָ����ֵ���ж�ֵ��
      byte[,] GrayArray = gp.BinaryArray(b, threshold);

      // ��ͼ�������ɫ
      return gp.BinaryImage(GrayArray, bgColor, fgColor);
    } // end of PaperCut


    /// <summary>
    /// Ǧ�ʻ�
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="threshold">��ɫ��ֵ[0, 100]</param>
    /// <returns></returns>
    public Bitmap Pencil(Bitmap b, int threshold)
    {
      if (threshold < 0) threshold = 0;
      if (threshold > 100) threshold = 100;

      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = new Bitmap(width, height);

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
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

        // Դͼ����Χ�˵�ָ��
        byte* nw; byte* n; byte* ne;
        byte* w; byte* e;
        byte* sw; byte* s; byte* se;

        int R, G, B;

        // �����һ��
        src += stride;
        dst += stride;
        for (int y = rectTop; y < rectBottom; y++)
        {
          // ����ÿ�е� 1 ��
          src += BPP;
          dst += BPP;

          for (int x = rectLeft; x < rectRight; x++)
          {
            // ȡ��Χ�˸������ɫֵ
            nw = src - stride - BPP;   // (x-1, y-1)
            n = src - stride;          // (x  , y-1)
            ne = src - stride + BPP;   // (x+1, y-1)

            w = src - BPP;             // (x-1, y)
            e = src + BPP;             // (x+1, y)

            sw = src + stride - BPP;   // (x-1, y+1)
            s = src + stride;          // (x  , y+1)
            se = src + stride + BPP;   // (x+1, y+1)

            // ���㵱ǰ���ص�����Χ�˵�ƽ��ֵ�Ĳ�
            R = nw[2] + n[2] + ne[2] + w[2] + e[2] + sw[2] + s[2] + se[2];
            R = src[2] - R / 8;
            G = nw[1] + n[1] + ne[1] + w[1] + e[1] + sw[1] + s[1] + se[1];
            G = src[1] - G / 8;
            B = nw[0] + n[0] + ne[0] + w[0] + e[0] + sw[0] + s[0] + se[0];
            B = src[0] - B / 8;

            // ȡ��ֵ�ľ���ֵ
            if (R < 0) R = -R;
            if (G < 0) G = -G;
            if (B < 0) B = -B;

            // �����ֵ����ָ����ֵ�����ڵ�ǰλ�����
            if (R >= threshold || G >= threshold || B >= threshold)
            {
              dst[0] = dst[1] = dst[2] = 0;
            }
            else
            {
              dst[0] = dst[1] = dst[2] = 255;
            }

            dst[3] = src[3];  // A

            // �����һ����
            src += BPP;
            dst += BPP;
          } // x

          // ������һ��
          // �����ע��Ҫ���� 1 �У��������в��ش���
          src += offset + BPP;
          dst += offset + BPP;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Pencil


    /// <summary>
    /// ����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="threshold">��ɫ��ֵ[0, 100]</param>
    /// <returns></returns>
    public Bitmap Sketch(Bitmap b, int threshold)
    {
      if (threshold < 0) threshold = 0;
      if (threshold > 100) threshold = 100;

      // ����Ҷ�
      GrayProcessing gp = new GrayProcessing();
      b = gp.Gray(b, GrayProcessing.GrayMethod.WeightAveraging);

      int width = b.Width;
      int height = b.Height;

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      // ͼ��ʵ�ʴ�������
      int rectTop = 0;
      int rectBottom = height - 1;
      int rectLeft = 0;
      int rectRight = width - 1;

      unsafe
      {
        byte* dst = (byte*)srcData.Scan0;
        // ȡ��ǰ���ص����½�����
        byte* src = dst + srcData.Stride + BPP;

        int offset = srcData.Stride - width * BPP;

        int diff;

        for (int y = rectTop; y < rectBottom; y++)
        {
          for (int x = rectLeft; x < rectRight; x++)
          {
            // ������������ĻҶȲ�
            diff = dst[0] - src[0];
            if (diff < 0)
              diff = -diff;

            // ����ҶȲ����ָ����ֵ������ɫ�������أ�������������
            // ���Ѹõ���Ϊ��ɫ��������Ϊ��ɫ
            if (diff >= threshold)
              dst[0] = dst[1] = dst[2] = 0;
            else
              dst[0] = dst[1] = dst[2] = 255;

            // �����һ����
            src += BPP;
            dst += BPP;
          } // x

          // ������һ��
          // �����ע��Ҫ���� 1 �У��������в��ش���
          src += offset + BPP;
          dst += offset + BPP;
        } // y
      }

      b.UnlockBits(srcData);

      return b;
    } // end of Sketch


    /// <summary>
    /// ������
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Comic(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B, pixel;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            R = p[2];
            G = p[1];
            B = p[0];

            // R = |g �C b + g + r| * r / 256;
            pixel = G - B + G + R;
            if (pixel < 0) pixel = -pixel;
            pixel = pixel * R / 256;
            if (pixel > 255) pixel = 255;
            p[2] = (byte)pixel;

            // G = |b �C g + b + r| * r / 256;
            pixel = B - G + B + R;
            if (pixel < 0) pixel = -pixel;
            pixel = pixel * R / 256;
            if (pixel > 255) pixel = 255;
            p[1] = (byte)pixel;

            // B = |b �C g + b + r| * g / 256;
            pixel = B - G + B + R;
            if (pixel < 0) pixel = -pixel;
            pixel = pixel * G / 256;
            if (pixel > 255) pixel = 255;
            p[0] = (byte)pixel;

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      // ��ͼ����лҶȻ�
      GrayProcessing gp = new GrayProcessing();
      return gp.Gray(b, GrayProcessing.GrayMethod.WeightAveraging);
    } // end of Comic


    /// <summary>
    /// ����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Aqua(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B, pixel;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // R = (g - b) * (g - b) / 128;
            // G = (r - b) * (r - b) / 128;
            // B = (r - g) * (r - g) / 128;

            R = p[2];
            G = p[1];
            B = p[0];

            pixel = (G - B) * (G - B) / 128;
            if (pixel > 255) pixel = 255;
            p[2] = (byte)pixel;

            pixel = (R - B) * (R - B) / 128;
            if (pixel > 255) pixel = 255;
            p[1] = (byte)pixel;

            pixel = (R - G) * (R - G) / 128;
            if (pixel > 255) pixel = 255;
            p[0] = (byte)pixel;

            p += BPP;
          }  // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Aqua


    /// <summary>
    /// �غ�ɫ������Ƭ
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Sepia(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // R = 0.393 * r + 0.769 * g + 0.189 * b;
            // G = 0.349 * r + 0.686 * g + 0.168 * b;
            // B = 0.272 * r + 0.534 * g + 0.131 * b;
            R = (25756 * p[2] + 50397 * p[1] + 12386 * p[0]) >> 16;
            G = (22872 * p[2] + 44958 * p[1] + 11010 * p[0]) >> 16;
            B = (17826 * p[2] + 34996 * p[1] + 8585 * p[0]) >> 16;

            if (R < 0) R = 0;
            if (R > 255) R = 255;

            if (G < 0) G = 0;
            if (G > 255) G = 255;

            if (B < 0) B = 0;
            if (B > 255) B = 255;

            p[2] = (byte)R;
            p[1] = (byte)G;
            p[0] = (byte)B;

            p += BPP;
          } // x
          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Sepia


    /// <summary>
    /// ��ɫ��Ⱦ
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="red">��ɫ����[0, 255]</param>
    /// <param name="green">��ɫ����[0, 255]</param>
    /// <param name="blue">��ɫ����[0, 255]</param>
    /// <returns></returns>
    public Bitmap Colorize(Bitmap b, int red, int green, int blue)
    {
      if (red < 0) red = 0;
      if (red > 255) red = 255;
      if (green < 0) green = 0;
      if (green > 255) green = 255;
      if (blue < 0) blue = 0;
      if (blue > 255) blue = 255;

      // �ҶȻ�
      GrayProcessing gp = new GrayProcessing();
      b = gp.Gray(b, GrayProcessing.GrayMethod.WeightAveraging);

      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            R = p[2] * red / 255;
            G = p[1] * green / 255;
            B = p[0] * blue / 255;

            p[2] = (byte)R;
            p[1] = (byte)G;
            p[0] = (byte)B;

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Colorize


    /// <summary>
    /// ����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Ice(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B, pixel;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            R = p[2];
            G = p[1];
            B = p[0];

            // R = |r - g - b| * 3 / 2;
            pixel = R - G - B;
            pixel = pixel * 3 / 2;
            if (pixel < 0) pixel = -pixel;
            if (pixel > 255) pixel = 255;
            p[2] = (byte)pixel;

            // G = |g - b - r| * 3 / 2;
            pixel = G - B - R;
            pixel = pixel * 3 / 2;
            if (pixel < 0) pixel = -pixel;
            if (pixel > 255) pixel = 255;
            p[1] = (byte)pixel;

            // B = |b - r - g| * 3 / 2;
            pixel = B - R - G;
            pixel = pixel * 3 / 2;
            if (pixel < 0) pixel = -pixel;
            if (pixel > 255) pixel = 255;
            p[0] = (byte)pixel;

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Ice


    /// <summary>
    /// ����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Molten(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B, pixel;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            R = p[2];
            G = p[1];
            B = p[0];

            pixel = R * 128 / (G + B + 1);
            if (pixel < 0) pixel = 0;
            if (pixel > 255) pixel = 255;
            p[2] = (byte)pixel;

            pixel = G * 128 / (B + R + 1);
            if (pixel < 0) pixel = 0;
            if (pixel > 255) pixel = 255;
            p[1] = (byte)pixel;

            pixel = B * 128 / (R + G + 1);
            if (pixel < 0) pixel = 0;
            if (pixel > 255) pixel = 255;
            p[0] = (byte)pixel;

            p += BPP;
          }  // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Molten


    /// <summary>
    /// ����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Darkness(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            R = p[2];
            G = p[1];
            B = p[0];

            p[2] = (byte)(R * R / 255);
            p[1] = (byte)(G * G / 255);
            p[0] = (byte)(B * B / 255);

            p += BPP;
          }  // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Darkness


    /// <summary>
    /// �Ե�
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Subtense(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int R, G, B;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            R = p[2];
            G = p[1];
            B = p[0];

            p[2] = (byte)(G * B / 255); // R
            p[1] = (byte)(B * R / 255); // G
            p[0] = (byte)(R * G / 255); // B

            p += BPP;
          }  // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Subtense


    /// <summary>
    /// �ֵ�
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Whim(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        double R, G, B, pixel;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            R = p[2];
            G = p[1];
            B = p[0];

            pixel = Math.Sin(Math.Atan2(G, B)) * 255;
            p[2] = (byte)pixel;

            pixel = Math.Sin(Math.Atan2(B, R)) * 255;
            p[1] = (byte)pixel;

            pixel = Math.Sin(Math.Atan2(R, G)) * 255;
            p[0] = (byte)pixel;

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Whim


    /************************************************************
     * 
     * ��ѹ�����桢���С�����
     * Ħ����
     * 
     ************************************************************/


    /// <summary>
    /// ��ͼ����м�ѹ��Ч����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="degree">��ѹ����[1, 32]</param>
    /// <returns></returns>
    public Bitmap Pinch(Bitmap b, int degree)
    {
      if (degree < 1) degree = 1;
      if (degree > 32) degree = 32;

      int width = b.Width;
      int height = b.Height;
      int midX = width / 2;
      int midY = height / 2;

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

        int X, Y;
        int offsetX, offsetY;

        // ���ȡ��뾶
        double radian, radius;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // ��ǰ����ͼ�����ĵ��ƫ����
            offsetX = x - midX;
            offsetY = y - midY;

            // ����
            radian = Math.Atan2(offsetY, offsetX);

            // �뾶
            radius = Math.Sqrt(offsetX * offsetX + offsetY * offsetY);
            radius = Math.Sqrt(radius) * degree;

            // ӳ��ʵ�����ص�
            X = (int)(radius * Math.Cos(radian)) + midX;
            Y = (int)(radius * Math.Sin(radian)) + midY;

            // �߽�Լ��
            if (X < 0) X = 0;
            if (X >= width) X = width - 1;
            if (Y < 0) Y = 0;
            if (Y >= height) Y = height - 1;

            src = (byte*)srcScan0 + Y * stride + X * BPP;

            dst[3] = src[3]; // A
            dst[2] = src[2]; // R
            dst[1] = src[1]; // G
            dst[0] = src[0]; // B

            dst += BPP;
          } // x
          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Pinch


    /// <summary>
    /// ��ͼ�����������Ч����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Spherize(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;
      int midX = width / 2;
      int midY = height / 2;
      // Max(midX, midY)
      double maxMidXY = (midX > midY ? midX : midY);

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

        int X, Y;
        int offsetX, offsetY;

        // ���ȡ��뾶
        double radian, radius;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // ��ǰ����ͼ�����ĵ��ƫ����
            offsetX = x - midX;
            offsetY = y - midY;

            // ����
            radian = Math.Atan2(offsetY, offsetX);

            // ע�⣬���ﲢ��ʵ�ʰ뾶
            radius = (offsetX * offsetX + offsetY * offsetY) / maxMidXY;

            // ӳ��ʵ�����ص�
            X = (int)(radius * Math.Cos(radian)) + midX;
            Y = (int)(radius * Math.Sin(radian)) + midY;

            // �߽�Լ��
            if (X < 0) X = 0;
            if (X >= width) X = width - 1;
            if (Y < 0) Y = 0;
            if (Y >= height) Y = height - 1;

            src = (byte*)srcScan0 + Y * stride + X * BPP;

            dst[3] = src[3]; // A
            dst[2] = src[2]; // R
            dst[1] = src[1]; // G
            dst[0] = src[0]; // B

            dst += BPP;
          } // x
          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Spherize


    /// <summary>
    /// ��ͼ�����������Ч����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="degree">���з���[1, 100]</param>
    /// <returns></returns>
    public Bitmap Swirl(Bitmap b, int degree)
    {
      if (degree < 1) degree = 1;
      if (degree > 100) degree = 100;
      double swirlDegree = degree / 1000.0;

      int width = b.Width;
      int height = b.Height;
      int midX = width / 2;
      int midY = height / 2;

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

        int X, Y;
        int offsetX, offsetY;

        // ���ȡ��뾶
        double radian, radius;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // ��ǰ����ͼ�����ĵ��ƫ����
            offsetX = x - midX;
            offsetY = y - midY;

            // ����
            radian = Math.Atan2(offsetY, offsetX);

            // �뾶���������ľ���
            radius = Math.Sqrt(offsetX * offsetX + offsetY * offsetY);

            // ӳ��ʵ�����ص�
            X = (int)(radius * Math.Cos(radian + swirlDegree * radius)) + midX;
            Y = (int)(radius * Math.Sin(radian + swirlDegree * radius)) + midY;

            // �߽�Լ��
            if (X < 0) X = 0;
            if (X >= width) X = width - 1;
            if (Y < 0) Y = 0;
            if (Y >= height) Y = height - 1;

            src = (byte*)srcScan0 + Y * stride + X * BPP;

            dst[3] = src[3]; // A
            dst[2] = src[2]; // R
            dst[1] = src[1]; // G
            dst[0] = src[0]; // B

            dst += BPP;
          } // x
          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Swirl


    /// <summary>
    /// ��ͼ����в�����Ч����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="degree">�������[1, 32]</param>
    /// <returns></returns>
    public Bitmap Wave(Bitmap b, int degree)
    {
      if (degree < 1) degree = 1;
      if (degree > 32) degree = 32;

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

        int X, Y;
        // Բ���� 2*pi
        double PI2 = Math.PI * 2.0;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            X = (int)(degree * Math.Sin(PI2 * y / 128.0)) + x;
            Y = (int)(degree * Math.Cos(PI2 * x / 128.0)) + y;

            // �߽�Լ��
            if (X < 0) X = 0;
            if (X >= width) X = width - 1;
            if (Y < 0) Y = 0;
            if (Y >= height) Y = height - 1;

            src = (byte*)srcScan0 + Y * stride + X * BPP;

            dst[3] = src[3]; // A
            dst[2] = src[2]; // R
            dst[1] = src[1]; // G
            dst[0] = src[0]; // B

            dst += BPP;
          } // x
          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Wave


    /// <summary>
    /// ��ͼ�����Ħ������Ч����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="degree">ǿ��[1, 16]</param>
    /// <returns></returns>
    public Bitmap MoireFringe(Bitmap b, int degree)
    {
      if (degree < 1) degree = 1;
      if (degree > 16) degree = 16;

      int width = b.Width;
      int height = b.Height;
      int midX = width / 2;
      int midY = height / 2;

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

        int X, Y;
        int offsetX, offsetY;

        // ���ȡ��뾶
        double radian, radius;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // ��ǰ����ͼ�����ĵ��ƫ����
            offsetX = x - midX;
            offsetY = y - midY;

            // ����
            radian = Math.Atan2(offsetX, offsetY);

            // �뾶
            radius = Math.Sqrt(offsetX * offsetX + offsetY * offsetY);

            // ӳ��ʵ�����ص�
            X = (int)(radius * Math.Sin(radian + degree * radius));
            Y = (int)(radius * Math.Sin(radian + degree * radius));

            // �߽�Լ��
            if (X < 0) X = 0;
            if (X >= width) X = width - 1;
            if (Y < 0) Y = 0;
            if (Y >= height) Y = height - 1;

            src = (byte*)srcScan0 + Y * stride + X * BPP;

            dst[3] = src[3]; // A
            dst[2] = src[2]; // R
            dst[1] = src[1]; // G
            dst[0] = src[0]; // B

            dst += BPP;
          } // x
          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      // ������������ͼ��ԭͼ�����ɫ�ʻ��
      return Inosculate(b, dstImage, 128);
    } // end of MoireFringe


    /************************************************************
     * 
     * ��ɢ�����ұ�Ե��������Ե���ƹ⡢�����ˡ��ͻ����ع�
     * 
     ************************************************************/


    /// <summary>
    /// ��ɢ
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="degree">��ɢ����[1, 32]</param>
    /// <returns></returns>
    public Bitmap Diffuse(Bitmap b, int degree)
    {
      if (degree < 1) degree = 1;
      if (degree > 32) degree = 32;

      int width = b.Width;
      int height = b.Height;

      // Ŀ��ͼ��
      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      int stride = srcData.Stride;
      System.IntPtr srcScan0 = srcData.Scan0;
      System.IntPtr dstScan0 = dstData.Scan0;
      int offset = stride - width * BPP;

      unsafe
      {
        byte* src = (byte*)srcScan0;
        byte* dst = (byte*)dstScan0;

        Random myRand = new Random();
        int X, Y;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // ��ȡ��ǰ���ص���Χ��һ�����
            X = x + myRand.Next(-degree, degree);
            Y = y + myRand.Next(-degree, degree);

            // ���Խ�磬����ͼ��߽��ϵĵ����
            if (X < 0) X = 0;
            if (X >= width) X = width - 1;
            if (Y < 0) Y = 0;
            if (Y >= height) Y = height - 1;

            // ָ��Դͼ��������
            src = (byte*)srcScan0 + (Y * stride) + (X * BPP);

            dst[3] = src[3];
            dst[2] = src[2];
            dst[1] = src[1];
            dst[0] = src[0];

            dst += BPP;
          } // x
          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Diffuse


    /// <summary>
    /// ��ͼ����в��ұ�Ե����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="angle">�Ƕ�[0, 360]</param>
    /// <returns></returns>
    public Bitmap FindEdge(Bitmap b, int angle)
    {
      // �Ƕ�ת����
      double radian = (double)angle * 2.0 * Math.PI / 360.0;

      // ÿһȨ�Ļ��ȼ��
      double pi4 = Math.PI / 4.0;

      // ��ͼ����о���任
      Matrix3x3 m = new Matrix3x3();

      m.TopLeft = Convert.ToInt32(Math.Cos(radian + pi4) * 256);
      m.TopMid = Convert.ToInt32(Math.Cos(radian + 2.0 * pi4) * 256);
      m.TopRight = Convert.ToInt32(Math.Cos(radian + 3.0 * pi4) * 256);

      m.MidLeft = Convert.ToInt32(Math.Cos(radian) * 256);
      m.Center = 0;
      m.MidRight = Convert.ToInt32(Math.Cos(radian + 4.0 * pi4) * 256);

      m.BottomLeft = Convert.ToInt32(Math.Cos(radian - pi4) * 256);
      m.BottomMid = Convert.ToInt32(Math.Cos(radian - 2.0 * pi4) * 256);
      m.BottomRight = Convert.ToInt32(Math.Cos(radian - 3.0 * pi4) * 256);

      m.Scale = 256;

      b = m.Convolute(b);

      // ��ͼ����и�����
      Adjustment a = new Adjustment();
      b = a.Invert(b);

      return b;
    } // end of FindEdge


    /// <summary>
    /// ������Ե
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap GlowingEdge(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      // ͼ��ʵ�ʴ�������
      // ���������� 1 �У������� 1 ��
      int rectTop = 0;
      int rectBottom = height - 1;
      int rectLeft = 0;
      int rectRight = width - 1;

      unsafe
      {
        int stride = data.Stride;
        int offset = stride - width * BPP;

        byte* p = (byte*)data.Scan0;
        byte* bottom = p + stride;
        byte* right = p + BPP;

        int pixel;

        for (int y = rectTop; y < rectBottom; y++)
        {
          for (int x = rectLeft; x < rectRight; x++)
          {
            // ָ��ǰ���·����ص�
            bottom = p + stride;

            // ָ��ǰ���ұ����ص�
            right = p + BPP;

            for (int i = 0; i < 3; i++)
            {
              pixel = 
                (p[i] - bottom[i]) * (p[i] - bottom[i]) + 
                (p[i] - right[i]) * (p[i] - right[i]);
              pixel = (int)(Math.Sqrt(pixel) * 2);

              if (pixel < 0) pixel = 0;
              if (pixel > 255) pixel = 255;

              p[i] = (byte)pixel;
            } // i

            // �����һ����
            p += BPP;
          } // x

          // ������һ��
          // �����ע��Ҫ���� 1 �У��������в��ش���
          p += offset + BPP;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of GlowingEdge


    /// <summary>
    /// �ƹ�
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="power">����ǿ��</param>
    /// <returns></returns>
    public Bitmap Lighting(Bitmap b, int power)
    {
      int width = b.Width;
      int height = b.Height;

      // ��Դ���ĵ� X, Y ����
      int x0 = width / 2;
      int y0 = height / 2;

      // ��Դ����뾶
      int radius = (int)Math.Sqrt(x0 * x0 + y0 * y0);

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        int pixel, brightness;

        int distance, squareDistance;
        int squareRadius = radius * radius;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            // ��ǰ�����Դ���ĵ�ľ���
            squareDistance = (x - x0) * (x - x0) + (y - y0) * (y - y0);

            if (squareDistance < squareRadius)
            {
              distance = (int)Math.Sqrt(squareDistance);

              // ��ǰ��Ĺ���ǿ��
              brightness = power * (radius - distance) / radius;

              for (int i = 0; i < 3; i++)
              { 
                pixel = p[i] + brightness;
                if (pixel > 255) pixel = 255;
                p[i] = (byte)pixel;
              } // i
            }

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Lighting


    /// <summary>
    /// ��ͼ�������������Ч����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="degree">���С[1, 32]</param>
    /// <returns></returns>
    public Bitmap Mosaic(Bitmap b, int degree)
    {
      if (degree < 1) degree = 1;
      if (degree > 32) degree = 32;

      // ���С
      int block = degree;

      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

      int srcStride = srcData.Stride;
      int dstStride = dstData.Stride;

      System.IntPtr srcScan0 = srcData.Scan0;
      System.IntPtr dstScan0 = dstData.Scan0;

      unsafe
      {
        byte* src = (byte*)srcScan0;
        byte* dst = (byte*)dstScan0;

        // �����ۼӺ�
        int sum = 0;

        // �����
        int product = block * block;

        byte brightness = 0;

        for (int y = 0; y < height; y += block)
        {
          for (int x = 0; x < width; x += block)
          {
            // ���� B, G, R �������������� A ����
            for (int i = 0; i < 3; i++)
            {
              // ��С���ڵ�����ƽ��ֵ
              sum = 0;
              for (int yB = 0; yB < block && (y + yB) < height; yB++)
              {
                for (int xB = 0; xB < block && (x + xB) < width; xB++)
                {
                  src = (byte*)srcScan0 + ((y + yB) * srcStride) + ((x + xB) * BPP);

                  sum += src[i];
                } // xB
              } // yB
              brightness = (byte)(sum / product);

              // ������ֵ����Ŀ��ͼ��
              for (int yB = 0; yB < block && (y + yB) < height; yB++)
              {
                for (int xB = 0; xB < block && (x + xB) < width; xB++)
                {
                  dst = (byte*)dstScan0 + ((y + yB) * dstStride) + ((x + xB) * BPP);

                  dst[i] = brightness;
                } // xB
              } // yB
            } // i
          } // x
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of Mosaic


    /// <summary>
    /// �ͻ�
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="brushSize">��ˢ�ߴ�[1, 8]</param>
    /// <param name="coarseness">�ֲڶ�[1, 255]</param>
    /// <returns></returns>
    public Bitmap OilPainting(Bitmap b, int brushSize, byte coarseness)
    {
      if (brushSize < 1) brushSize = 1;
      if (brushSize > 8) brushSize = 8;

      if (coarseness < 1) coarseness = 1;
      if (coarseness > 255) coarseness = 255;

      int width = b.Width;
      int height = b.Height;

      int lenArray = coarseness + 1;
      int[] CountIntensity = new int[lenArray];
      uint[] RedAverage = new uint[lenArray];
      uint[] GreenAverage = new uint[lenArray];
      uint[] BlueAverage = new uint[lenArray];
      uint[] AlphaAverage = new uint[lenArray];

      // �����Ҷ�����
      GrayProcessing gp = new GrayProcessing();
      Bitmap grayImage = gp.Gray( (Bitmap)b.Clone(), GrayProcessing.GrayMethod.WeightAveraging );
      byte[,] Gray = gp.Image2Array(grayImage);
      grayImage.Dispose();

      // Ŀ��ͼ��
      Bitmap dstImage = new Bitmap(width, height);

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      int stride = srcData.Stride;
      System.IntPtr srcScan0 = srcData.Scan0;
      System.IntPtr dstScan0 = dstData.Scan0;
      int offset = stride - width * BPP;

      unsafe
      {
        byte* src = (byte*)srcScan0;
        byte* dst = (byte*)dstScan0;

        for (int y = 0; y < height; y++)
        {
          // �ͻ���Ⱦ��Χ���±߽�
          int top = y - brushSize;
          int bottom = y + brushSize + 1;

          if (top < 0) top = 0;
          if (bottom >= height) bottom = height - 1;

          for (int x = 0; x < width; x++)
          {
            // �ͻ���Ⱦ��Χ���ұ߽�
            int left = x - brushSize;
            int right = x + brushSize + 1;

            if (left < 0) left = 0;
            if (right >= width) right = width - 1;

            // ��ʼ������
            for (int i = 0; i < lenArray; i++)
            {
              CountIntensity[i] = 0;
              RedAverage[i] = 0;
              GreenAverage[i] = 0;
              BlueAverage[i] = 0;
              AlphaAverage[i] = 0;
            } // i


            // ���������ѭ������������Ĵ�ѭ��
            // Ҳ���ͻ���Ч����Ĺؼ�����
            src = (byte*)srcScan0 + top * stride + left * BPP;
            int offsetBlock = stride - (right - left) * BPP;

            for (int j = top; j < bottom; j++)
            {
              for (int i = left; i < right; i++)
              {
                byte intensity = (byte)(coarseness * Gray[i, j] / 255);
                CountIntensity[intensity]++;

                AlphaAverage[intensity] += src[3];
                RedAverage[intensity] += src[2];
                GreenAverage[intensity] += src[1];
                BlueAverage[intensity] += src[0];

                src += BPP;
              } // i

              src += offsetBlock;
            } // j

            // �����ֵ������¼����������
            byte chosenIntensity = 0;
            int maxInstance = CountIntensity[0];
            for (int i = 1; i < lenArray; i++)
            {
              if (CountIntensity[i] > maxInstance)
              {
                chosenIntensity = (byte)i;
                maxInstance = CountIntensity[i];
              }
            } // i

            dst[3] = (byte)(AlphaAverage[chosenIntensity] / maxInstance);
            dst[2] = (byte)(RedAverage[chosenIntensity] / maxInstance);
            dst[1] = (byte)(GreenAverage[chosenIntensity] / maxInstance);
            dst[0] = (byte)(BlueAverage[chosenIntensity] / maxInstance);

            dst += BPP;
          } // x

          dst += offset;
        } // y
      }

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of OilPainting


    /// <summary>
    /// �ع�
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Solarize(Bitmap b)
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
            if (p[0] < 128) p[0] = (byte)(p[0] ^ 0xFF); // B
            if (p[1] < 128) p[1] = (byte)(p[1] ^ 0xFF); // G
            if (p[2] < 128) p[2] = (byte)(p[2] ^ 0xFF); // R

            p += BPP;
          }// x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of Solarize


    /************************************************************
     * 
     * ͼ���ں�
     * 
     ************************************************************/


    /// <summary>
    /// ͼ���ں�
    /// </summary>
    /// <param name="bgImage">����ͼ��</param>
    /// <param name="fgImage">ǰ��ͼ��</param>
    /// <param name="transparency">ǰ��͸����[0, 255]</param>
    /// <returns></returns>
    public Bitmap Inosculate(Bitmap bgImage, Bitmap fgImage, byte transparency)
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
            if (fg[3] != 0)
            {
              // pixel = FG * transparency / 255 + BG * (255 - transparency) / 255
              bg[0] = (byte)((fg[0] - bg[0]) * transparency / 255 + bg[0]);
              bg[1] = (byte)((fg[1] - bg[1]) * transparency / 255 + bg[1]);
              bg[2] = (byte)((fg[2] - bg[2]) * transparency / 255 + bg[2]);
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

      Bitmap dstImage = (Bitmap)bgImage.Clone();

      bgImage.Dispose();
      fgImage.Dispose();

      return dstImage;
    } // end of Inosculate


    /// <summary>
    /// ������ͼ��ϳ�Ϊħ��ͼ
    /// </summary>
    /// <param name="bgImage">����ͼ��</param>
    /// <param name="fgImage">ǰ��ͼ��</param>
    /// <param name="transparency">͸����[0, 255]��������״��</param>
    /// <param name="contrast">ǰ��ͼ�뱳��ͼ�ĶԱȶ�[-100, 100]</param>
    public Bitmap Magic(Bitmap bgImage, Bitmap fgImage, byte transparency, int contrast)
    {
      Adjustment a = new Adjustment();
      Effect e = new Effect();

      // �����ؽ���ת
      bgImage = a.Interleaving(bgImage);

      // ����Աȶ�
      bgImage = a.Contrast(bgImage, contrast);

      // ǰ���ͱ������л�ϴ���
      Bitmap dstImage = e.Inosculate(bgImage, fgImage, transparency);

      bgImage.Dispose();
      fgImage.Dispose();

      return dstImage;
    } // end of Magic


    /************************************************************
     * 
     * �Զ��塢ȥ���ۡ������ַ�
     * 
     ************************************************************/


    /// <summary>
    /// �Զ���
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="sequence">�����Ԫ������</param>
    /// <returns></returns>
    public Bitmap Custom(Bitmap b, int[] sequence)
    {
      MatrixNxN m = new MatrixNxN();
      m.Sequence = sequence;

      return m.Convolute(b);
    } // end of Custom


    /// <summary>
    /// �Զ���
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="kernel">�����</param>
    /// <param name="scale">���ű���</param>
    /// <param name="offset">ƫ����</param>
    /// <returns></returns>
    public Bitmap Custom(Bitmap b, int[,] kernel, int scale, int offset)
    {
      MatrixNxN m = new MatrixNxN();
      m.Kernel = kernel;
      m.Scale = scale;
      m.Offset = offset;

      return m.Convolute(b);
    } // end of Custom


    /// <summary>
    /// ȥ������
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="tolerence">�ݲ�[0, 100]</param>
    /// <param name="red">��ɫ����[0, 255]</param>
    /// <param name="green">��ɫ����[0, 255]</param>
    /// <param name="blue">��ɫ����[0, 255]</param>
    /// <returns></returns>
    public Bitmap RedEyeRemoval(Bitmap b, int tolerence, int red, int green, int blue)
    {
      if (tolerence < 0) tolerence = 0;
      if (tolerence > 100) tolerence = 100;

      if (red < 0) red = 0;
      if (red > 255) red = 255;
      if (green < 0) green = 0;
      if (green > 255) green = 255;
      if (blue < 0) blue = 0;
      if (blue > 255) blue = 255;

      int width = b.Width;
      int height = b.Height;

      BitmapData data = b.LockBits(new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

      unsafe
      {
        byte* p = (byte*)data.Scan0;
        int offset = data.Stride - width * BPP;

        byte R, G, B;
        int gray;

        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            R = p[2];
            G = p[1];
            B = p[0];

            // R ������ G, B ���������Խ�����ɫ�ɷ�Խ��
            int difference = R - (G > B ? G : B);

            // �����ǰ������ɫ���������ָ���ݲΧ�ڣ����Һ�ɫ�ɷ��㹻��
            // ����Ϊ��ǰ������Ҫ���к���ȥ������
            if ((difference > tolerence) && (R > 100))
            {
              // ��Ҷ�
              gray = (19661 * R + 38666 * G + 7209 * B) >> 16;

              // ��ָ������ɫ�滻����ɫ
              p[2] = (byte)(gray * red / 255);
              p[1] = (byte)(gray * green / 255);
              p[0] = (byte)(gray * blue / 255);
            }

            p += BPP;
          } // x

          p += offset;
        } // y
      }

      b.UnlockBits(data);

      return b;
    } // end of RedEyeRemoval


    /// <summary>
    /// �������֣���ͼƬת��Ϊ�ַ�
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="text">����ʾ������</param>
    /// <param name="blockWidth">���</param>
    /// <param name="blockHeight">���</param>
    /// <returns></returns>
    public string Image2Text(Bitmap b, string text, int blockWidth, int blockHeight)
    {
      int width = b.Width;
      int height = b.Height;

      // ���Ƚ�ͼ��ҶȻ�
      GrayProcessing gp = new GrayProcessing();
      b = gp.Gray(b, GrayProcessing.GrayMethod.WeightAveraging);
      byte[,] Gray = gp.Image2Array(b);

      // ������ʾ�ĻҶ��ַ�
      char[] ArtChar = text.ToCharArray();
      int len = ArtChar.Length;

      // ͳ��ÿ���ַ��ĵ���
      int[] CharDots = new int[len];
      Watermark w = new Watermark();
      for (int i = 0; i < len; i++)
      {
        CharDots[i] = w.CountTextDots(ArtChar[i]);
      } // i

      // ���ַ���������ð�ݷ�����
      for (int i = 0; i < len - 1; i++)
      {
        for (int j = i + 1; j < len; j++)
        {
          if (CharDots[j] < CharDots[i])
          {
            // ��������
            int t = CharDots[j];
            CharDots[j] = CharDots[i];
            CharDots[i] = t;

            // �����ַ�
            char c = ArtChar[j];
            ArtChar[j] = ArtChar[i];
            ArtChar[i] = c;
          }
        } // j
      } // i

      // ������
      int maxGray = CharDots[len - 1];

      // �ۼӿ�Ҷ�
      int sum = 0;

      // ���
      double product = maxGray / (double)(blockWidth * blockHeight * 255);

      // ͼ��������ַ������������ַ���
      string artString = "";

      for (int y = 0; y < height; y += blockHeight)
      {
        for (int x = 0; x < width; x += blockWidth)
        {
          sum = 0;

          for (int yB = 0; yB < blockHeight && (y + yB) < height; yB++)
          {
            for (int xB = 0; xB < blockWidth && (x + xB) < width; xB++)
            {
              // �ۼ�С���ڵĻҶ�
              sum += Gray[x + xB, y + yB];
            } // xB
          } // yB

          // gray = maxGray * (sum / (blockWidth * blockHeight)) / 255
          int gray = (int)(sum * product);

          // Ѱ�һҶ���ӽ����ַ�
          int k = 0;
          while (CharDots[k] < (maxGray - gray))
            k++;

          // ���лҶ�ӳ��
          artString += ArtChar[k];
        } // x

        artString += "\r\n";
      } // y

      return artString;
    } // end of Image2Text


  }
}
