using System;

namespace EasyPhoto.ImageProcess
{
  /// <summary>
  /// ��̬ѧ������
  /// </summary>
  public partial class Morphologic
  {
    /************************************************************
     * 
     * ��̬ѧ����������㷨
     * 
     * ˮƽ����ֱ��ʮ���͸�ʴ
     * ˮƽ����ֱ��ʮ��������
     * ϸ�����ֻ�
     * 
     ************************************************************/


    /// <summary>
    /// ˮƽ��ʴ
    /// </summary>
    /// <param name="src">��ֵ������</param>
    /// <returns></returns>
    public byte[,] ErosionHorz(byte[,] src)
    {
      int width = src.GetLength(0);
      int height = src.GetLength(1);

      // ��ʼ��Ŀ������Ϊ 255������ɫ
      byte[,] dst = InitArray(width, height, 255);

      // 1*3 �ṹԪ��
      // 0 0 0
      // ����ʹ�� 1*3 �ĽṹԪ�أ�Ϊ��ֹԽ�磬���Բ���������ߺ����ұߵ���������
      int topRect = 0;
      int bottomRect = height;
      int leftRect = 1;
      int rightRect = width - 1;

      for (int y = topRect; y < bottomRect; y++)
      {
        for (int x = leftRect; x < rightRect; x++)
        {
          // ����Ŀ��ͼ���е�ǰ��Ϊ��ɫ
          byte c = 0;

          // ���ԭͼ���е� (-1,0), (0,0), (1,0) ������֮һ�а׵㣬
          // ��Ŀ��ͼ���еĵ�ǰ�㣬��(0,0)�㸳���ɫ
          for (int i = -1; i <= 1; i++)
          {
            if (src[x + i, y] > 127)
            {
              c = 255;
              break;
            }
          } // i

          dst[x, y] = c;
        } // x
      } // y

      return dst;
    } // end of ErosionHorz


    /// <summary>
    /// ��ֱ��ʴ
    /// </summary>
    /// <param name="src">��ֵ������</param>
    /// <returns></returns>
    public byte[,] ErosionVert(byte[,] src)
    {
      int width = src.GetLength(0);
      int height = src.GetLength(1);

      // ��ʼ��Ŀ������Ϊ 255������ɫ
      byte[,] dst = InitArray(width, height, 255);

      // 3*1 �ṹԪ��
      // 0
      // 0
      // 0
      // ����ʹ�� 3*1 �ĽṹԪ�أ�Ϊ��ֹԽ�磬���Բ��������ϱߺ����±ߵ���������
      int topRect = 1;
      int bottomRect = height - 1;
      int leftRect = 0;
      int rightRect = width;

      for (int y = topRect; y < bottomRect; y++)
      {
        for (int x = leftRect; x < rightRect; x++)
        {
          // ����Ŀ��ͼ���е�ǰ��Ϊ��ɫ
          byte c = 0;

          // ���ԭͼ���е� (0,-1), (0,0), (0,1) ������֮һ�а׵㣬
          // ��Ŀ��ͼ���еĵ�ǰ�㣬��(0,0)�㸳���ɫ
          for (int j = -1; j <= 1; j++)
          {
            if (src[x, y + j] > 127)
            {
              c = 255;
              break;
            }
          } // j

          dst[x, y] = c;
        } // x
      } // y

      return dst;
    } // end of ErosionVert


    /// <summary>
    /// ʮ���͸�ʴ
    /// </summary>
    /// <param name="src">��ֵ������</param>
    /// <returns></returns>
    public byte[,] ErosionCross(byte[,] src)
    {
      int width = src.GetLength(0);
      int height = src.GetLength(1);

      // ��ʼ��Ŀ������Ϊ 255������ɫ
      byte[,] dst = InitArray(width, height, 255);

      // 3*3 �ṹԪ��
      // 1 0 1
      // 0 0 0
      // 1 0 1
      // ����ʹ�� 3*3 �ĽṹԪ�أ�Ϊ��ֹԽ�磬���Բ��������ϡ��¡������ıߵ�����
      int topRect = 1;
      int bottomRect = height - 1;
      int leftRect = 1;
      int rightRect = width - 1;

      for (int y = topRect; y < bottomRect; y++)
      {
        for (int x = leftRect; x < rightRect; x++)
        {
          // ����Ŀ��ͼ���е�ǰ��Ϊ��ɫ
          byte c = 0;

          // ���ԭͼ���е� (-1,0), (0,0), (1,0), (0,-1), (0, 1) �����֮һ�а׵㣬
          // ��Ŀ��ͼ���еĵ�ǰ�㣬��(0,0)�㸳���ɫ
          for (int i = -1; i <= 1; i++)
          {
            for (int j = -1; j <= 1; j++)
            {
              // ���� 3*3 ʮ���ͽṹԪ�أ�
              // �����жϵ�ǰ�������ϡ����ϡ����¡������Ľ��ϵ��ĵ�
              if ((i + 2) % 2 == 1 && (j + 2) % 2 == 1)
                continue;

              if (src[x + i, y + j] > 127)
              {
                c = 255;
                break;
              }
            } // j
          } // i

          dst[x, y] = c;
        } // x
      } // y

      return dst;
    } // end of ErosionCross


    /// <summary>
    /// ˮƽ����
    /// </summary>
    /// <param name="src">��ֵ������</param>
    /// <returns></returns>
    public byte[,] DilationHorz(byte[,] src)
    {
      int width = src.GetLength(0);
      int height = src.GetLength(1);

      // ��ʼ��Ŀ������Ϊ 255������ɫ
      byte[,] dst = InitArray(width, height, 255);

      // 1*3 �ṹԪ��
      // 0 0 0
      // ����ʹ�� 1*3 �ĽṹԪ�أ�Ϊ��ֹԽ�磬���Բ���������ߺ����ұߵ���������
      int topRect = 0;
      int bottomRect = height;
      int leftRect = 1;
      int rightRect = width - 1;

      for (int y = topRect; y < bottomRect; y++)
      {
        for (int x = leftRect; x < rightRect; x++)
        {
          // ����Ŀ��ͼ���е�ǰ��Ϊ��ɫ
          byte c = 255;

          // ���ԭͼ���е� (-1,0), (0,0), (1,0) ������֮һ�кڵ㣬
          // ��Ŀ��ͼ���еĵ�ǰ�㣬��(0,0)�㸳���ɫ
          for (int i = -1; i <= 1; i++)
          {
            if (src[x + i, y] < 128)
            {
              c = 0;
              break;
            }
          } // i

          dst[x, y] = c;
        } // x
      } // y

      return dst;
    } // end of DilationHorz


    /// <summary>
    /// ��ֱ����
    /// </summary>
    /// <param name="src">��ֵ������</param>
    /// <returns></returns>
    public byte[,] DilationVert(byte[,] src)
    {
      int width = src.GetLength(0);
      int height = src.GetLength(1);

      // ��ʼ��Ŀ������Ϊ 255������ɫ
      byte[,] dst = InitArray(width, height, 255);

      // 3*1 �ṹԪ��
      // 0
      // 0
      // 0
      // ����ʹ�� 3*1 �ĽṹԪ�أ�Ϊ��ֹԽ�磬���Բ��������ϱߺ����±ߵ���������
      int topRect = 1;
      int bottomRect = height - 1;
      int leftRect = 0;
      int rightRect = width;

      for (int y = topRect; y < bottomRect; y++)
      {
        for (int x = leftRect; x < rightRect; x++)
        {
          // ����Ŀ��ͼ���е�ǰ��Ϊ��ɫ
          byte c = 255;

          // ���ԭͼ���е� (-1,0), (0,0), (1,0) ������֮һ�кڵ㣬
          // ��Ŀ��ͼ���еĵ�ǰ�㣬��(0,0)�㸳���ɫ
          for (int j = -1; j <= 1; j++)
          {
            if (src[x, y + j] < 128)
            {
              c = 0;
              break;
            }
          } // j

          dst[x, y] = c;
        } // x
      } // y

      return dst;
    } // end of DilationVert


    /// <summary>
    /// ʮ��������
    /// </summary>
    /// <param name="src">��ֵ������</param>
    /// <returns></returns>
    public byte[,] DilationCross(byte[,] src)
    {
      int width = src.GetLength(0);
      int height = src.GetLength(1);

      // ��ʼ��Ŀ������Ϊ 255������ɫ
      byte[,] dst = InitArray(width, height, 255);

      // 3*3 �ṹԪ��
      // 1 0 1
      // 0 0 0
      // 1 0 1
      // ����ʹ�� 3*3 �ĽṹԪ�أ�Ϊ��ֹԽ�磬���Բ��������ϡ��¡������ıߵ�����
      int topRect = 1;
      int bottomRect = height - 1;
      int leftRect = 1;
      int rightRect = width - 1;

      for (int y = topRect; y < bottomRect; y++)
      {
        for (int x = leftRect; x < rightRect; x++)
        {
          // ����Ŀ��ͼ���е�ǰ��Ϊ��ɫ
          byte c = 255;

          // ���ԭͼ���� (-1,0), (0,0), (1,0), (0,-1), (0, 1) �����֮һ�кڵ㣬
          // ��Ŀ��ͼ���еĵ�ǰ�㣬��(0,0)�㸳���ɫ
          for (int i = -1; i <= 1; i++)
          {
            for (int j = -1; j <= 1; j++)
            {
              // ���� 3*3 �ṹԪ�أ�
              // �����жϵ�ǰ�������ϡ����ϡ����¡������Ľ��ϵ��ĵ�
              if ((i + 2) % 2 == 1 && (j + 2) % 2 == 1)
                continue;

              if (src[x + i, y + j] < 128)
              {
                c = 0;
                break;
              }
            } // j
          } // i

          dst[x, y] = c;
        } // x
      } // y

      return dst;
    } // end of DilationCross


    /// <summary>
    /// ϸ��
    /// </summary>
    /// <param name="src">��ֵ������</param>
    /// <returns></returns>
    public byte[,] Thinning(byte[,] src)
    {
      int width = src.GetLength(0);
      int height = src.GetLength(1);

      // ��Ҫϸ����
      bool needThinning = true;

      byte[,] S = new byte[5, 5];
      int num = 0;

      // 5*5 �ṹԪ��
      // ����ʹ�� 5*5 �ĽṹԪ�أ�Ϊ��ֹԽ�磬���Բ�������Χ�� 2 �С� 2 ������
      int topRect = 2;
      int bottomRect = height - 2;
      int leftRect = 2;
      int rightRect = width - 2;

      while (needThinning)
      {
        needThinning = false;

        // ��ʼ��Ŀ������Ϊ 255������ɫ
        byte[,] dst = InitArray(width, height, 255);

        for (int y = topRect; y < bottomRect; y++)
        {
          for (int x = leftRect; x < rightRect; x++)
          {
            // ���ԭͼ���е�ǰ��Ϊ��ɫ��������
            if (src[x, y] > 127)
              continue;

            // ��õ�ǰ�����ڵ� 5*5 ����������ֵ��
            // ע�⣺��ɫ�� 0 ������ɫ�� 1 ����
            for (int i = -2; i <= 2; i++)
            {
              for (int j = -2; j <= 2; j++)
              {
                if (src[x + j, y + i] > 127)
                  S[i + 2, j + 2] = 0; // ��ɫ
                else
                  S[i + 2, j + 2] = 1; // ��ɫ
              } // j
            } // i


            // �ж����� 1 �Ƿ����
            num = S[1, 1] + S[1, 2] + S[1, 3] +
                  S[2, 1]           + S[2, 3] +
                  S[3, 1] + S[3, 2] + S[3, 3];
            if (!(num >= 2 && num <= 6))
            {
              dst[x, y] = 0;
              continue;
            }


            // �ж����� 2 �Ƿ����
            num = 0;

            if (S[1, 2] == 0 && S[1, 1] == 1) num++;
            if (S[1, 1] == 0 && S[2, 1] == 1) num++;
            if (S[2, 1] == 0 && S[3, 1] == 1) num++;
            if (S[3, 1] == 0 && S[3, 2] == 1) num++;
            if (S[3, 2] == 0 && S[3, 3] == 1) num++;
            if (S[3, 3] == 0 && S[2, 3] == 1) num++;
            if (S[2, 3] == 0 && S[1, 3] == 1) num++;
            if (S[1, 3] == 0 && S[1, 2] == 1) num++;

            if (!(num == 1))
            {
              dst[x, y] = 0;
              continue;
            }


            // �ж����� 3 �Ƿ����
            if (!(S[1, 2] * S[2, 1] * S[2, 3] == 0))
            {
              num = 0;

              if (S[0, 2] == 0 && S[0, 1] == 1) num++;
              if (S[0, 1] == 0 && S[1, 1] == 1) num++;
              if (S[1, 1] == 0 && S[2, 1] == 1) num++;
              if (S[2, 1] == 0 && S[2, 2] == 1) num++;
              if (S[2, 2] == 0 && S[2, 3] == 1) num++;
              if (S[2, 3] == 0 && S[1, 3] == 1) num++;
              if (S[1, 3] == 0 && S[0, 3] == 1) num++;
              if (S[0, 3] == 0 && S[0, 2] == 1) num++;

              if (num == 1)
              {
                dst[x, y] = 0;
                continue;
              }
            }


            // �ж����� 4 �Ƿ����
            if (!(S[1, 2] * S[2, 1] * S[3, 2] == 0))
            {
              num = 0;

              if (S[1, 1] == 0 && S[1, 0] == 1) num++;
              if (S[1, 0] == 0 && S[2, 0] == 1) num++;
              if (S[2, 0] == 0 && S[3, 0] == 1) num++;
              if (S[3, 0] == 0 && S[3, 1] == 1) num++;
              if (S[3, 1] == 0 && S[3, 2] == 1) num++;
              if (S[3, 2] == 0 && S[2, 2] == 1) num++;
              if (S[2, 2] == 0 && S[1, 2] == 1) num++;
              if (S[1, 2] == 0 && S[1, 1] == 1) num++;

              if (num == 1)
              {
                dst[x, y] = 0;
                continue;
              }
            }


            // ������������㣬��ɾ���õ�
            dst[x, y] = 255;

            // ����ϸ����
            needThinning = true;
          } // x
        } // y

        // ����ϸ�����ͼ����Ϊ��һ��ϸ������
        src = (byte[,])dst.Clone();
      } // while

      return src;
    } // end of Thinning


    /// <summary>
    /// �ֻ�
    /// </summary>
    /// <param name="src">��ֵ������</param>
    /// <returns></returns>
    public byte[,] Thickening(byte[,] src)
    {
      int width = src.GetLength(0);
      int height = src.GetLength(1);

      // ���Ѷ�ֵ�����ظ���ɫ����������
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          src[x, y] ^= 255;
        } // x
      } // y

      return Thinning(src);
    } // end of Thickening


  }
}
