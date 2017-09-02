using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace EasyPhoto.ImageProcess
{
  /// <summary>
  /// EasyPhoto ���ú�����
  /// </summary>
  public class Function
  {
    /**************************************************
     * 
     * �Զ���ö������  
     * 
     * ���뷽ʽ����Ӧģʽ
     * 
     **************************************************/

    /// <summary>
    /// ���뷽ʽ
    /// </summary>
    public enum AlignMode
    {
      /// <summary>
      /// ����
      /// </summary>
      TopLeft,

      /// <summary>
      /// ��
      /// </summary>
      MidLeft,

      /// <summary>
      /// ����
      /// </summary>
      BottomLeft,

      /// <summary>
      /// ��
      /// </summary>
      TopMid,

      /// <summary>
      /// ����
      /// </summary>
      Center,

      /// <summary>
      /// ��
      /// </summary>
      BottomMid,

      /// <summary>
      /// ����
      /// </summary>
      TopRight,

      /// <summary>
      /// ��
      /// </summary>
      MidRight,

      /// <summary>
      /// ����
      /// </summary>
      BottomRight
    }


    /// <summary>
    /// ��Ӧģʽ
    /// </summary>
    public enum FitMode
    {
      /// <summary>
      /// ��Ӧָ�����
      /// </summary>
      Width,

      /// <summary>
      /// ��Ӧָ���߶�
      /// </summary>
      Height,

      /// <summary>
      /// ��ָ����߽�������Ӧ����Լ����߱�
      /// </summary>
      WidthHeight,

      /// <summary>
      /// ��Ӧָ���ٷ���
      /// </summary>
      Percent,

      /// <summary>
      /// ����Ӧ���������û�ָ�����
      /// </summary>
      None
    }


    /************************************************************
     * 
     * ����ͼ�񡢶�λ������ɫת��
     * 
     ************************************************************/

    /// <summary>
    /// ����ָ����Ӧģʽ����ͼ��Ŀ��ߴ�
    /// </summary>
    /// <param name="bgSize">�����ߴ�</param>
    /// <param name="fgSize">ǰ���ߴ�</param>
    /// <param name="percent">���ָ���˰ٷ�����Ӧģʽ����˲�����Ч</param>
    /// <param name="fit">��Ӧģʽ</param>
    /// <returns></returns>
    public Size ScaleToFit(Size bgSize,Size fgSize, double percent, FitMode fit)
    {
      Size dstSize = fgSize;

      switch (fit)
      {
        case FitMode.Width:
          dstSize.Height = fgSize.Width * bgSize.Height / bgSize.Width;
          break;

        case FitMode.Height:
          dstSize.Width = fgSize.Height * bgSize.Width / bgSize.Height;
          break;

        case FitMode.Percent:
          dstSize.Width = Convert.ToInt32(bgSize.Width * percent);
          dstSize.Height = Convert.ToInt32(bgSize.Height * percent);
          break;

        case FitMode.WidthHeight:
          if ((fgSize.Width * bgSize.Height) < (bgSize.Width * fgSize.Height))   // ��ͬ�� ( (fgSize.Width/fgSize.Height) < (bgSize.Width/bgSize.Height) )
          {
            dstSize.Height = fgSize.Width * bgSize.Height / bgSize.Width;
          }
          else
          {
            dstSize.Width = fgSize.Height * bgSize.Width / bgSize.Height;
          }
          break;

        default: // FitMode.None
          break;
      }

      return dstSize;
    } // end of ScaleToFit


    /// <summary>
    /// �����û�ָ���Ķ���ģʽ����ǰ��ͼ��λ������ͼ����
    /// </summary>
    /// <param name="W">����ͼ����</param>
    /// <param name="H">����ͼ��߶�</param>
    /// <param name="w">ǰ��ͼ����</param>
    /// <param name="h">ǰ��ͼ��߶�</param>
    /// <param name="align">����ģʽ</param>
    /// <returns></returns>
    public Point Locate(int W, int H, int w, int h, AlignMode align)
    {
      int x = 0;
      int y = 0;

      switch (align)
      {
        case AlignMode.TopLeft:
          x = 0;
          y = 0;
          break;

        case AlignMode.MidLeft:
          x = 0;
          y = (H - h) / 2;
          break;

        case AlignMode.BottomLeft:
          x = 0;
          y = H - h;
          break;

        case AlignMode.TopMid:
          x = (W - w) / 2;
          y = 0;
          break;

        case AlignMode.Center:
          x = (W - w) / 2;
          y = (H - h) / 2;
          break;

        case AlignMode.BottomMid:
          x = (W - w) / 2;
          y = H - h;
          break;

        case AlignMode.TopRight:
          x = W - w;
          y = 0;
          break;

        case AlignMode.MidRight:
          x = W - w;
          y = (H - h) / 2;
          break;

        case AlignMode.BottomRight:
          x = W - w;
          y = H - h;
          break;
      } // align

      return new Point(x, y);
    } // end of Locate


    /// <summary>
    /// ��ʮ��������ɫ�ַ���ת���ɱ�׼��ɫ�ṹ��
    /// </summary>
    /// <param name="color">ʮ��������ɫ�ַ������� "#FF0000", "#FFFF0000"</param>
    /// <returns></returns>
    public static System.Drawing.Color HexString2Color(string color)
    {
      color = color.ToLower();

      Color returnColor = new Color();

      Regex regexColor = new Regex(@"^#([0-9a-fA-F]{6})|([0-9a-fA-F]{8})$");
      if (regexColor.IsMatch(color))
      {
        int A, R, G, B;
        if (color.Length == 7)
        {
          R = Convert.ToInt32(color.Substring(1, 2), 16);
          G = Convert.ToInt32(color.Substring(3, 2), 16);
          B = Convert.ToInt32(color.Substring(5, 2), 16);
          returnColor = Color.FromArgb(R, G, B);
        }
        else
        {
          A = Convert.ToInt32(color.Substring(1, 2), 16);
          R = Convert.ToInt32(color.Substring(3, 2), 16);
          G = Convert.ToInt32(color.Substring(5, 2), 16);
          B = Convert.ToInt32(color.Substring(7, 2), 16);
          returnColor = Color.FromArgb(A, R, G, B);
        }
      }

      return returnColor;
    } // end of HexString2Color


    /// <summary>
    /// ����׼��ɫ�ṹ��ת����ʮ��������ɫ�ַ���
    /// </summary>
    /// <param name="color">��׼��ɫ�ṹ��</param>
    /// <returns></returns>
    public static string Color2HexString(Color color)
    {
      string a = "0" + Convert.ToString(color.A, 16);
      string r = "0" + Convert.ToString(color.R, 16);
      string g = "0" + Convert.ToString(color.G, 16);
      string b = "0" + Convert.ToString(color.B, 16);

      a = a.Substring(a.Length - 2).ToUpper();
      r = r.Substring(r.Length - 2).ToUpper();
      g = g.Substring(g.Length - 2).ToUpper();
      b = b.Substring(b.Length - 2).ToUpper();

      return a + r + g + b;
    } // end of Color2HexString


    /// <summary>
    /// �������߶εĶ˵㼯����256������ӳ���
    /// </summary>
    /// <param name="Points">���߶ζ˵㼯</param>
    /// <returns></returns>
    public byte[] GetMap(Point[] Points)
    {
      byte[] Map = new byte[256];
      for (int i = 0; i < 256; i++)
      {
        Map[i] = (byte)i;
      } // i

      int count = Points.Length;
      // ������������������
      if (count < 2)
        return Map;

      for (int i = 0; i < count - 1; i++)
      {
        Point point = Points[i];
        Point next = Points[i + 1];

        if (point.X == next.X)
        {
          Map[point.X] = (byte)next.Y;
          continue;
        }

        // б��: k = ( y2 - y1 ) / ( x2 - x1 );
        // y = k * x - k * x1 + y1;
        double k = (double)(next.Y - point.Y) / (double)(next.X - point.X);
        double c = point.Y - k * point.X;
        for (int x = point.X; x <= next.X; x++)
        {
          int y = (int)(k * x + c);
          Map[x] = (byte)y;
        } // x
      } // i

      return Map;
    } // end of GetMap


    /// <summary>
    /// ����ָ�����׵��β�㷵������������ȷ��ֱ���е�һϵ�е㼯
    /// </summary>
    /// <param name="start">�׵�</param>
    /// <param name="end">β��</param>
    /// <returns></returns>
    public static Point[] GetLinePoints(Point start, Point end)
    {
      Point[] coords = null;

      int x1 = start.X;
      int y1 = start.Y;

      int x2 = end.X;
      int y2 = end.Y;

      int dx = x2 - x1;
      int dy = y2 - y1;

      int dxAbs = Math.Abs(dx);
      int dyAbs = Math.Abs(dy);

      int px = x1;
      int py = y1;

      int sdx = Math.Sign(dx);
      int sdy = Math.Sign(dy);

      int x = 0;
      int y = 0;

      if (dxAbs == dyAbs)
      {
        coords = new Point[dxAbs + 1];

        for (int i = 0; i <= dxAbs; i++)
        {
          coords[i] = new Point(px, py);
          px += sdx;
          py += sdy;
        }
      }

      if (dxAbs > dyAbs)
      {
        coords = new Point[dxAbs + 1];

        for (int i = 0; i <= dxAbs; i++)
        {
          y += dyAbs;

          if (y >= dxAbs)
          {
            y -= dxAbs;
            py += sdy;
          }

          coords[i] = new Point(px, py);
          px += sdx;
        } // i
      }

      if (dxAbs < dyAbs)
      {
        coords = new Point[dyAbs + 1];

        for (int i = 0; i <= dyAbs; i++)
        {
          x += dxAbs;

          if (x >= dyAbs)
          {
            x -= dyAbs;
            px += sdx;
          }

          coords[i] = new Point(px, py);
          py += sdy;
        } // i
      }

      return coords;
    } // end of GetLinePoints


    /// <summary>
    /// ���������㽨����һ���������ǵľ���
    /// </summary>
    /// <param name="a">���ε�һ������</param>
    /// <param name="b">���ε���һ������</param>
    /// <returns></returns>
    public static Rectangle PointsToRectangle(Point a, Point b)
    {
      int x = Math.Min(a.X, b.X);
      int y = Math.Min(a.Y, b.Y);
      int width = Math.Abs(a.X - b.X) + 1;
      int height = Math.Abs(a.Y - b.Y) + 1;

      return new Rectangle(x, y, width, height);
    }


    /// <summary>
    /// �ж�һ�����Ƿ��ھ�����
    /// </summary>
    /// <param name="p">��</param>
    /// <param name="r">����</param>
    /// <returns></returns>
    public static bool IsPointInRectangle(Point p, Rectangle r)
    {
      if ((p.X < r.X) || (p.Y < r.Y) || (p.X >= r.Right) || (p.Y >= r.Bottom))
      {
        return false;
      }
      else
      {
        return true;
      }
    }


    /// <summary>
    /// �ж�һ�����Ƿ��ھ����ڣ�������ڣ������õ�������
    /// </summary>
    /// <param name="point">��</param>
    /// <param name="rect">����</param>
    /// <returns></returns>
    public static Point PointInRectangle(Point point, Rectangle rect)
    {
      if (point.X < 0) point.X = 0;
      if (point.Y < 0) point.Y = 0;
      if (point.X >= rect.Width) point.X = rect.Width - 1;
      if (point.Y >= rect.Height) point.Y = rect.Height - 1;

      return point;
    }


    /// <summary>
    /// ����ָ������ɫ�ݲ����ж�������ɫ�Ƿ���ͬ
    /// </summary>
    /// <param name="destination">Ŀ����ɫ</param>
    /// <param name="standard">��׼��ɫ</param>
    /// <param name="tolerance">�ݲ�</param>
    /// <returns></returns>
    public static bool CheckColor(Color destination, Color standard, int tolerance)
    {
      int ds = 0;

      int red = destination.R - standard.R;
      ds += red * red;

      int green = destination.G - standard.G;
      ds += green * green;

      int blue = destination.B - standard.B;
      ds += blue * blue;

      return (ds < tolerance * tolerance);
    }


    /// <summary>
    /// ����ָ�����ĸ���������飬ʹ�����ĸ��㳯һ������
    /// </summary>
    /// <param name="a">�� A ����</param>
    /// <param name="b">�� B ����</param>
    /// <param name="c">�� C ����</param>
    /// <param name="d">�� D ����</param>
    /// <returns></returns>
    public static PointF[] MakePolygon(PointF a, PointF b, PointF c, PointF d)
    {
      PointF dirA = new PointF(a.X - b.X, a.Y - b.Y);
      PointF dirB = new PointF(c.X - d.X, c.Y - d.Y);

      if (dirA.X * dirB.X + dirA.Y * dirB.Y > 0)
      {
        return new PointF[] { a, b, d, c };
      }
      else
      {
        return new PointF[] { a, b, c, d };
      }
    }


  }
}
