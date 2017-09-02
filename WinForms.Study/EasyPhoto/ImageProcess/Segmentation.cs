using System;
using System.Collections;
using System.Drawing;

namespace EasyPhoto.ImageProcess
{
  /// <summary>
  /// ͼ��ָ����
  /// </summary>
  public partial class Segmentation : GrayProcessing
  {
    /************************************************************
     * 
     * ͼ��ָ��������㷨
     * 
     * ��ǡ�������ܳ����������١���ȡ��ͶӰ
     * 
     ************************************************************/


    /// <summary>
    /// ���µı�Ǻ��滻����������оɵı�Ǻ�
    /// </summary>
    /// <param name="Sign">��ֵͼ��������</param>
    /// <param name="srcSign">ԭʼ��Ǻ�</param>
    /// <param name="dstSign">Ŀ���Ǻ�</param>
    private void ReplaceSign(ref ushort[,] Sign, ushort srcSign, ushort dstSign)
    {
      int width = Sign.GetLength(0);
      int height = Sign.GetLength(1);

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if (Sign[x, y] == srcSign)
            Sign[x, y] = dstSign;
        } // x
      } // y
    } // end of ReplaceSign


    /// <summary>
    /// ���һ����ֵͼ��
    /// </summary>
    /// <param name="b">��ֵͼ��������</param>
    /// <returns></returns>
    public ushort[,] ImageSign(byte[,] b)
    {
      int width = b.GetLength(0);
      int height = b.GetLength(1);

      // ��Ǻţ������Ա�� 65536 ����ͬ����ͨ����
      // ע���ǺŴ� 1 ��ʼ���ε�������Ǻ� 0 ������
      ushort signNo = 1;

      // �ö�ջ��¼���пձ��
      Stack Seat = new Stack();

      // ��ֵͼ����ͨ�����ʶ���洢���������ʶ������ͼ������
      ushort[,] Sign = new ushort[width, height];


      // ��ʼ����б��
      for (int x = 0; x < width; x++)
      {
        if (b[x, 0] != 0) continue;

        // �������������ĺڵ�
        while (x < width && b[x, 0] == 0)
        {
          Sign[x, 0] = signNo;
          x++;
        } // while

        signNo++;
      } // x


      // ���������м������б��
      for (int y = 1; y < height; y++)
      {
        // ������
        if (b[0, y] == 0)
        {
          if (b[0, y - 1] == 0)
            Sign[0, y] = Sign[0, y - 1];
          else
            Sign[0, y] = signNo++;
        }

        // ������
        if (b[width - 1, y] == 0)
        {
          if (b[width - 1, y - 1] == 0)
            Sign[width - 1, y] = Sign[width - 1, y - 1];
          else
            Sign[width - 1, y] = signNo++;
        }
      } // y


      // �����Ѿ�������ͼ����С������С������У�
      // ������ֻ�����ų������к����ͼ����
      int topRect = 1;
      int bottomRect = height;
      int leftRect = 1;
      int rightRect = width - 1;

      // �����ҿ�ʼ���
      for (int y = topRect; y < bottomRect; y++)
      {
        for (int x = leftRect; x < rightRect; x++)
        {
          // �����ǰ�㲻Ϊ�ڵ㣬������������
          if (b[x, y] != 0) continue;

          // ����
          if (b[x + 1, y - 1] == 0)
          {
            // ����ǰ����Ϊ�����ϵ���ͬ�ı��
            ushort sign = Sign[x, y] = Sign[x + 1, y - 1];

            // ����ǰ��Ϊ�ڵ㣬����ǰ��ı�������ϵ�ı�ǲ�ͬʱ
            if (b[x - 1, y] == 0 && Sign[x - 1, y] != sign)
            {
              // ��ջ����¼��ǰ���ǣ���Ϊ�ñ�ǽ����滻��
              Seat.Push(Sign[x - 1, y]);

              // �����ϵ�ı�Ǻ��滻����������ǰ���Ǻ���ͬ�ĵ�
              ReplaceSign(ref Sign, Sign[x - 1, y], sign);
            }

            // �����ϵ�Ϊ�ڵ㣬�����ϵ�ı�������ϵ�ı�ǲ�ͬʱ
            else if (b[x - 1, y - 1] == 0 && Sign[x - 1, y - 1] != sign)
            {
              // ��ջ����¼���ϵ��ǣ���Ϊ�ñ�ǽ����滻��
              Seat.Push(Sign[x - 1, y - 1]);

              // �����ϵ�ı�Ǻ��滻�����������ϵ��Ǻ���ͬ�ĵ�
              ReplaceSign(ref Sign, Sign[x - 1, y - 1], sign);
            }
          } // ������

          // ����
          else if (b[x, y - 1] == 0)
          {
            // ����ǰ����Ϊ�����ϵ���ͬ�ı��
            Sign[x, y] = Sign[x, y - 1];
          }

          // ����
          else if (b[x - 1, y - 1] == 0)
          {
            // ����ǰ����Ϊ�����ϵ���ͬ�ı��
            Sign[x, y] = Sign[x - 1, y - 1];
          }

          // ��ǰ
          else if (b[x - 1, y] == 0)
          {
            // ����ǰ����Ϊ����ǰ����ͬ�ı��
            Sign[x, y] = Sign[x - 1, y];
          }

          // ���ϡ����ϡ����ϼ���ǰ�ĵ����Ϊ�ڵ㣬����ʾ������ʼ
          else
          {
            // �������������� 0xFFFF
            if (signNo >= 0xFFFF)
              return Sign;

            // �����ջ���޿ձ�ǣ�
            // ��ʹ���±�ǣ�����ʹ�øÿձ��
            if (Seat.Count == 0)
              Sign[x, y] = signNo++;
            else
              Sign[x, y] = (ushort)Seat.Pop(); // ��ջ
          }

        } // x
      } // y


      // ��ջ����ڿձ�ǣ���ʹ����û�пձ��
      while (Seat.Count > 0)
      {
        ReplaceSign(ref Sign, (ushort)(--signNo), (ushort)Seat.Pop());
      } // while

      return Sign;
    } // end of ImageSign


    /// <summary>
    /// �������
    /// </summary>
    /// <param name="Sign">��ֵͼ��������</param>
    /// <returns></returns>
    public int[] ImageArea(ushort[,] Sign)
    {
      int width = Sign.GetLength(0);
      int height = Sign.GetLength(1);

      // �ҳ�����Ǻţ����ҳ�������
      int max = 0;
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if (Sign[x, y] > max)
            max = Sign[x, y];
        } // x
      } // y

      // ���ͳ������
      int[] Area = new int[max + 1];

      // �����������
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          Area[Sign[x, y]]++;
        } // x
      } // y

      return Area;
    } // end of ImageArea


    /// <summary>
    /// �����ܳ�
    /// </summary>
    /// <param name="Sign">��ֵͼ��������</param>
    /// <returns></returns>
    public int[] ImagePerimeter(ushort[,] Sign)
    {
      // �����������٣�����������
      Sign = ContourTrace(Sign);

      int width = Sign.GetLength(0);
      int height = Sign.GetLength(1);

      // �ҳ�����Ǻţ����ҳ�������
      int max = 0;
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if (Sign[x, y] > max)
            max = Sign[x, y];
        } // x
      } // y

      // �ܳ�ͳ������
      int[] Perimeter = new int[max + 1];

      // ���������ܳ�
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          Perimeter[Sign[x, y]]++;
        } // x
      } // y

      return Perimeter;
    } // end of ImagePerimeter


    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="b">����������</param>
    /// <returns></returns>
    public ushort[,] ContourTrace(ushort[,] Sign)
    {
      int width = Sign.GetLength(0);
      int height = Sign.GetLength(1);

      // �����������
      int[] Area = ImageArea(Sign);
      int areaNumber = Area.Length;

      // ͼ��߽�
      ushort[,] Boundary = new ushort[width, height];

      // �ҵ���ʼ���ص���ʼ�㣿
      bool findStartPoint = false;

      // ɨ�赽һ���߽�㣿
      bool findPoint;

      // ��ʼ�߽�㼰��ǰ�߽��
      Point Start = new Point(0, 0);
      Point Current = new Point(0, 0);

      // ��ʼ�������Ϸ���ɨ�跽��Ϊ��ʱ��
      Point[] Direct = new Point[8];
      Direct[0].X = -1; Direct[0].Y = 1;    // SW
      Direct[1].X = 0;  Direct[1].Y = 1;    // S
      Direct[2].X = 1;  Direct[2].Y = 1;    // SE
      Direct[3].X = 1;  Direct[3].Y = 0;    // E
      Direct[4].X = 1;  Direct[4].Y = -1;   // NE
      Direct[5].X = 0;  Direct[5].Y = -1;   // N
      Direct[6].X = -1; Direct[6].Y = -1;   // NW
      Direct[7].X = -1; Direct[7].Y = 0;    // W

      // ���ҵ������Ϸ��ı߽��
      for (int sign = 1; sign < areaNumber; sign++)
      {
        findStartPoint = false;

        for (int y = 0; y < height && !findStartPoint; y++)
        {
          for (int x = 0; x < width && !findStartPoint; x++)
          {
            if (Sign[x, y] == sign)
            {
              findStartPoint = true;

              // ��¼�µ�ǰ�߽���ʼ��
              Start = new Point(x, y);
              Boundary[x, y] = (ushort)sign;
            }
          } // x
        } // y

        // ��ǰɨ�跽��
        int direction = 0;

        // �ӳ�ʼ�㿪ʼɨ��
        Current = Start;

        // ��ʼ���ٱ߽�
        findStartPoint = false;
        while (!findStartPoint)
        {
          // ɨ�����
          int searchNumber = 0;

          findPoint = false;
          while (!findPoint)
          {
            // ��ɨ�跽��鿴һ������
            int x = Current.X + Direct[direction].X;
            int y = Current.Y + Direct[direction].Y;

            if ((x >= 0 && x < width && y >= 0 && y < height) && (Sign[x, y] == sign))
            {
              // �ҵ��߽��
              findPoint = true;

              Current.X += Direct[direction].X;
              Current.Y += Direct[direction].Y;

              // �ص���ʼ�㣬�߽�ɨ�����
              if (Current.X == Start.X && Current.Y == Start.Y)
              {
                findStartPoint = true;
                break;
              }

              Boundary[Current.X, Current.Y] = (ushort)sign;

              // ɨ�跽��˳ʱ����ת����
              direction -= 2;
              direction += 8; // ������ָ���
              direction %= 8;
            }
            else
            {
              // ɨ�跽����ʱ����תһ��
              direction++;
              direction %= 8;

              searchNumber++;
              if (searchNumber > 8)
              {
                // Ϊ������
                findStartPoint = true;
                findPoint = true;
              }
            }

          } // findPoint
        } // findStartPoint
      } // sign

      return Boundary;
    } // end of ContourTrace


    /// <summary>
    /// ������ȡ
    /// </summary>
    /// <param name="b">��ֵͼ��������</param>
    /// <returns></returns>
    public byte[,] ContourPick(byte[,] b)
    {
      int width = b.GetLength(0);
      int height = b.GetLength(1);

      byte[,] dst = new byte[width, height];

      // ������ͼ������ΧһȦ
      int topRect = 1;
      int bottomRect = height - 1;
      int leftRect = 1;
      int rightRect = width - 1;

      for (int y = topRect; y < bottomRect; y++)
      {
        for (int x = leftRect; x < rightRect; x++)
        {
          if (b[x, y] == 0)
          {
            int sum =
              b[x - 1, y - 1] + b[x, y - 1] + b[x + 1, y - 1] +
              b[x - 1, y    ]               + b[x + 1, y    ] +
              b[x - 1, y + 1] + b[x, y + 1] + b[x + 1, y + 1];

            // �����Χ�˵�ȫΪ�ڵ㣬���õ�ǰ��Ϊ�׵�
            if (sum == 0)
              dst[x, y] = 255;
          }
          else
          {
            dst[x, y] = 255;
          }
        } // x
      } // y

      return dst;
    } // end of ContourPick


    /// <summary>
    /// ͼ��ͶӰ
    /// </summary>
    /// <param name="b">��ֵͼ��������</param>
    /// <param name="isHorz">�Ƿ�ˮƽͶӰ</param>
    /// <returns></returns>
    public byte[,] Project(byte[,] b, bool isHorz)
    {
      int width = b.GetLength(0);
      int height = b.GetLength(1);

      // ��ʼ��Ϊ��ɫ
      byte[,] dst = InitArray(width, height, 255);

      if (isHorz)
      {
        for (int y = 0; y < height; y++)
        {
          int sum = 0;
          for (int x = 0; x < width; x++)
          {
            if (b[x, y] == 0)
              sum++;
          } // x

          for (int x = 0; x < sum; x++)
          {
            dst[x, y] = 0;
          } // x
        } // y
      }
      else
      {
        for (int x = 0; x < width; x++)
        {
          int sum = 0;
          for (int y = 0; y < height; y++)
          {
            if (b[x, y] == 0)
              sum++;
          } // x

          for (int y = height-sum; y < height; y++)
          {
            dst[x, y] = 0;
          } // x
        } // y
      }

      return dst;
    } // end of Project


  }
}
