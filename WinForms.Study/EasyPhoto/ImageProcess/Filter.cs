using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace EasyPhoto.ImageProcess
{
  /// <summary>
  /// �˲����࣬���˾���
  /// </summary>
  public class Filter : ImageInfo
  {
    /// <summary>
    /// �˲�����
    /// </summary>
    public enum FilteringMethod
    {
      /// <summary>
      /// ��ֵ�˲�
      /// </summary>
      Mean,

      /// <summary>
      /// ��ֵ�˲�
      /// </summary>
      Median,

      /// <summary>
      /// ���ֵ�˲�
      /// </summary>
      Maximum,

      /// <summary>
      /// ��Сֵ�˲�
      /// </summary>
      Minimum
    } // FilteringMethod


    /// <summary>
    /// ͳ��һ���������в�����һ������ֵ
    /// </summary>
    /// <param name="sequence">��������</param>
    /// <param name="filteringMethod">�˲�����</param>
    /// <returns></returns>
    public int Count(int[] sequence, FilteringMethod filteringMethod)
    {
      int count = 0;
      int len = sequence.Length;

      switch (filteringMethod)
      {
        case FilteringMethod.Mean:
          int sum = 0;
          for (int i = 0; i < len; i++)
          {
            sum += sequence[i];
          } // i
          count = sum / len;
          break;

        case FilteringMethod.Median:
          bool isMovable = true;
          int t = 0;
          while (isMovable)
          {
            isMovable = false;
            for (int i = 1; i < len; i++)
            {
              if (sequence[i - 1] > sequence[i])
              {
                // ���� sequence[i-1], sequence[i]
                t = sequence[i - 1];
                sequence[i - 1] = sequence[i];
                sequence[i] = t;

                isMovable = true;
              }
            } // i
          } // isMovable

          // ȡ��ֵ
          count = sequence[len / 2];
          break;

        case FilteringMethod.Maximum:
          int max = sequence[0];
          for (int i = 1; i < len; i++)
          {
            if (sequence[i] > max)
              max = sequence[i];
          } // i
          count = max;
          break;

        case FilteringMethod.Minimum:
          int min = sequence[0];
          for (int i = 1; i < len; i++)
          {
            if (sequence[i] < min)
              min = sequence[i];
          } // i
          count = min;
          break;
      } // switch

      return count;
    } // end of Count


    /// <summary>
    /// N��N �����˲�
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="N">�˲����ڴ�С��N Ϊ����</param>
    /// <param name="filteringMethod">�˲�����</param>
    /// <returns></returns>
    public Bitmap FilterNxN(Bitmap b, int N, FilteringMethod filteringMethod)
    {
      // ��� N Ϊż������� N Ϊ����
      if (N % 2 == 0) N++;

      // N��N ������������
      int[] sequence = new int[N * N];

      // ���ڰ뾶
      int radius = N / 2;

      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
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
            // Alpha
            dst[3] = src[3];

            // ���� B, G, R ������
            for (int i = 0; i < 3; i++)
            {
              // �ռ� N��N ��������������
              for (int m = -radius; m <= radius; m++)
              {
                for (int n = -radius; n <= radius; n++)
                {
                  sequence[(m + radius) * N + (n + radius)] = src[i + m * stride + n * BPP];
                } // n
              } // m

              // �����û�ָ����ͳ�Ʒ���������������
              dst[i] = (byte)Count(sequence, filteringMethod);
            } // i


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

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of FilterNxN


    /// <summary>
    /// ʮ�����˲�
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <param name="N">ʮ�ּܴ�С��NΪ����</param>
    /// <param name="filteringMethod">�˲�����</param>
    /// <returns></returns>
    public Bitmap FilterCross(Bitmap b, int N, FilteringMethod filteringMethod)
    {
      // ��� N Ϊż������� N Ϊ����
      if (N % 2 == 0) N++;

      // ʮ�ּ���������
      int[] sequence = new int[N * 2 - 1];

      // ʮ�ּܰ뾶
      int radius = N / 2;

      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = (Bitmap)b.Clone();

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
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
            // Alpha
            dst[3] = src[3];

            // ���� B, G, R ������
            for (int i = 0; i < 3; i++)
            {
              int k = 0;
              // �ռ�ʮ�ּܵ���������
              for (int n = -radius; n <= radius; n++)
              {
                // �ռ�����
                sequence[k++] = src[i + n * BPP];

                // �ռ�����
                if (n == 0) continue; // ���ռ����ĵ�
                sequence[k++] = src[i + n * stride];
              } // n

              // �����û�ָ����ͳ�Ʒ���������������
              dst[i] = (byte)Count(sequence, filteringMethod);
            } // i


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

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of FilterCross


    /// <summary>
    /// ����Ӧƽ���˲�
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap FilterAutoFit(Bitmap b)
    {
      // 7 ������������sequence
      int[] S = new int[7];

      // ��ֵ
      int[] A = new int[9];

      // ����
      int[] K = new int[9];

      // ���ڰ뾶
      int radius = 2;

      int width = b.Width;
      int height = b.Height;

      // ���þ�ֵ�˲�������ʼ��Ŀ��λͼ
      Bitmap dstImage = FilterNxN((Bitmap)b.Clone(), 3, FilteringMethod.Mean);

      BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
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
        int stride2 = stride * 2;
        int offset = stride - width * BPP;

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
            // Alpha
            dst[3] = src[3];

            // ���� B, G, R ������
            for (int i = 0; i < 3; i++)
            {
              // ���� 7 ����
              S[0] = src[i-stride2-BP2P]; S[1] = src[i-stride2-BPP];
              S[2] = src[i-stride -BP2P]; S[3] = src[i-stride -BPP]; S[4] = src[i-stride];
                                          S[5] = src[i - BPP];       S[6] = src[i];
              A[0] = Count(S, FilteringMethod.Mean);

              // �� 7 ����
              S[0] = src[i-stride2-BPP]; S[1] = src[i-stride2]; S[2] = src[i-stride2+BPP];
              S[3] = src[i-stride -BPP]; S[4] = src[i- stride]; S[5] = src[i-stride +BPP];
                                         S[6] = src[i];
              A[1] = Count(S, FilteringMethod.Mean);

              // ���� 7 ����
                                    S[0] = src[i-stride2+BPP]; S[1] = src[i-stride2+BP2P];
              S[2] = src[i-stride]; S[3] = src[i-stride +BPP]; S[4] = src[i-stride +BP2P];
              S[5] = src[i];        S[6] = src[i+BPP];
              A[2] = Count(S, FilteringMethod.Mean);

              // �� 7 ����
              S[0] = src[i-stride-BP2P]; S[1] = src[i-stride-BPP];
              S[2] = src[i-BP2P];        S[3] = src[i-BPP];        S[4] = src[i];
              S[5] = src[i+stride-BP2P]; S[6] = src[i+stride-BPP];
              A[3] = Count(S, FilteringMethod.Mean);

              // ��Χ 9 ����
              // ��ΪĿ��λͼ�Ѿ��þ�ֵ�˲�����������ˣ��ʲ�����ȡ��Χ���������
              A[4] = dst[i];

              // �� 7 ����
                             S[0] = src[i-stride+BPP]; S[1] = src[i-stride+BP2P];
              S[2] = src[i]; S[3] = src[i+BPP];        S[4] = src[i+BP2P];
                             S[5] = src[i+stride+BPP]; S[6] = src[i+stride+BP2P];
              A[5] = Count(S, FilteringMethod.Mean);

              // ���� 7 ����
                                          S[0] = src[i-BPP];         S[1] = src[i];
              S[2] = src[i+stride -BP2P]; S[3] = src[i+stride -BPP]; S[4] = src[i+stride];
              S[5] = src[i+stride2-BP2P]; S[6] = src[i+stride2-BPP];
              A[6] = Count(S, FilteringMethod.Mean);

              // �� 7 ����
                                         S[0] = src[i];
              S[1] = src[i+stride -BPP]; S[2] = src[i+stride];  S[3] = src[i+stride +BPP];
              S[4] = src[i+stride2-BPP]; S[5] = src[i+stride2]; S[6] = src[i+stride2+BPP];
              A[7] = Count(S, FilteringMethod.Mean);

              // ���� 7 ����
              S[0] = src[i];        S[1] = src[i+BPP];
              S[2] = src[i+stride]; S[3] = src[i+stride +BPP]; S[4] = src[i+stride +BP2P];
                                    S[5] = src[i+stride2+BPP]; S[6] = src[i+stride2+BP2P];
              A[8] = Count(S, FilteringMethod.Mean);

              // �󷽲�
              for (int k = 0; k < 9; k++)
              {
                K[k] = src[i] * src[i] - A[k] * A[k];
              } // k

              // ����С����
              int min = K[0]; // ��С����
              int index = 0;  // ��С�������������
              for (int k = 0; k < 9; k++)
              {
                if (K[k] < min)
                {
                  min = K[k];
                  index = k;
                }
              } // k

              // ȡ��С����
              dst[i] = (byte)A[index];
            } // i


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

      b.UnlockBits(srcData);
      dstImage.UnlockBits(dstData);

      b.Dispose();

      return dstImage;
    } // end of FilterAutoFit


  }
}
