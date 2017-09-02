using System;
using System.Drawing;

namespace EasyPhoto.ColorSpace
{
  /// <summary>
  /// HSV ɫ�ʿռ�ṹ��
  /// </summary>
  public struct HSV
  {
    int h, s, v;

    /// <summary>
    /// ��ȡ������ɫ��[0, 360]
    /// </summary>
    public int Hue
    {
      get
      {
        return h;
      }
      set
      {
        h = value % 360;
      }
    }

    /// <summary>
    /// ��ȡ�����ñ��Ͷ�[0, 100]
    /// </summary>
    public int Saturation
    {
      get
      {
        return s;
      }
      set
      {
        s = value;
        if (s < 0) s = 0;
        if (s > 100) s = 100;
      }
    }

    /// <summary>
    /// ��ȡ����������ֵ[0, 100]
    /// </summary>
    public int Value
    {
      get
      {
        return v;
      }
      set
      {
        v = value;
        if (v < 0) v = 0;
        if (v > 100) v = 100;
      }
    }


    public override bool Equals(object obj)
    {
      return this == (HSV)obj;
    }

    public override int GetHashCode()
    {
      //(Hue + (Saturation << 8) + (Value << 16)).GetHashCode();
      return base.GetHashCode();
    }


    /// <summary>
    /// �ж� HSV �ṹ���Ƿ����
    /// </summary>
    /// <param name="lHsv">HSV �ṹ�� 1</param>
    /// <param name="rHsv">HSV �ṹ�� 2</param>
    /// <returns></returns>
    public static bool operator ==(HSV lHsv, HSV rHsv)
    {
      if ((lHsv.Hue == rHsv.Hue) &&
          (lHsv.Saturation == rHsv.Saturation) &&
          (lHsv.Value == rHsv.Value))
      {
        return true;
      }
      else
      {
        return false;
      }
    }


    /// <summary>
    /// �ж� HSV �ṹ���Ƿ����
    /// </summary>
    /// <param name="lHsv">HSV �ṹ�� 1</param>
    /// <param name="rHsv">HSV �ṹ�� 2</param>
    /// <returns></returns>
    public static bool operator !=(HSV lHsv, HSV rHsv)
    {
      return !(lHsv == rHsv);
    }


    /// <summary>
    /// ���� (h, s, v) �������� EasyPhoto.ColorSpace.HSV �ṹ��
    /// </summary>
    /// <param name="h">ɫ��[0, 360]</param>
    /// <param name="s">���Ͷ�[0, 100]</param>
    /// <param name="v">����[0, 100]</param>
    /// <returns></returns>
    public static HSV FromHsv(int h, int s, int v)
    {
      HSV hsv = new HSV();

      hsv.Hue = h;
      hsv.Saturation = s;
      hsv.Value = v;

      return hsv;
    } // end of FromHsv


    /// <summary>
    /// ���� Color �ṹ�彨�� EasyPhoto.ColorSpace.HSV �ṹ��
    /// </summary>
    /// <param name="color">RGB ��ɫ�ṹ��</param>
    /// <returns></returns>
    public static HSV FromColor(Color color)
    {
      return FromRgb((byte)color.R, (byte)color.G, (byte)color.B);
    } // end of FromColor


    /// <summary>
    /// ���� (red, green, blue) ��ɫ�������� EasyPhoto.ColorSpace.HSV �ṹ��
    /// </summary>
    /// <param name="red">red ����</param>
    /// <param name="green">green ����</param>
    /// <param name="blue">blue ����</param>
    /// <returns></returns>
    public static HSV FromRgb(byte red, byte green, byte blue)
    {
      double r = (double)red / 255.0;
      double g = (double)green / 255.0;
      double b = (double)blue / 255.0;

      double min, max, delta;
      double h, s, v;

      min = Math.Min(Math.Min(r, g), b);
      max = Math.Max(Math.Max(r, g), b);
      delta = max - min;
      v = max;

      if (max == 0 || delta == 0)
      {
        s = 0;
        h = 0;
      }
      else
      {
        s = delta / max;
        if (r == max)
        {
          h = (g - b) / delta;
        }
        else if (g == max)
        {
          h = 2 + (b - r) / delta;
        }
        else
        {
          h = 4 + (r - g) / delta;
        }
      }

      h *= 60;
      if (h < 0)
      {
        h += 360;
      }

      return FromHsv( (int)h, (int)(s * 100), (int)(v * 100));
    } // end of FromRgb


    /// <summary>
    /// ��ȡ RGB ��ɫֵ
    /// </summary>
    /// <returns></returns>
    public Color ToRgb()
    {
      double h = Hue % 360;
      double s = (double)Saturation / 100.0;
      double v = (double)Value / 100.0;

      double r = 0, g = 0, b = 0;

      if (s == 0)
      {
        r = v;
        g = v;
        b = v;
      }
      else
      {
        double p, q, t;

        double fractionalSector;
        int sectorNumber;
        double sectorPos;

        sectorPos = h / 60;
        sectorNumber = (int)(Math.Floor(sectorPos));

        fractionalSector = sectorPos - sectorNumber;

        p = v * (1 - s);
        q = v * (1 - (s * fractionalSector));
        t = v * (1 - (s * (1 - fractionalSector)));

        switch (sectorNumber)
        {
          case 0:
            r = v;
            g = t;
            b = p;
            break;

          case 1:
            r = q;
            g = v;
            b = p;
            break;

          case 2:
            r = p;
            g = v;
            b = t;
            break;

          case 3:
            r = p;
            g = q;
            b = v;
            break;

          case 4:
            r = t;
            g = p;
            b = v;
            break;

          case 5:
            r = v;
            g = p;
            b = q;
            break;
        }
      }

      return Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
    } // end of ToRgb


    /// <summary>
    /// ��ȡ RGB �ṹ�� red ����ֵ
    /// </summary>
    /// <returns></returns>
    public byte GetRed()
    {
      return (byte)ToRgb().R;
    }


    /// <summary>
    /// ��ȡ RGB �ṹ�� green ����ֵ
    /// </summary>
    /// <returns></returns>
    public byte GetGreen()
    {
      return (byte)ToRgb().G;
    }


    /// <summary>
    /// ��ȡ RGB �ṹ�� blue ����ֵ
    /// </summary>
    /// <returns></returns>
    public byte GetBlue()
    {
      return (byte)ToRgb().B;
    }


  }
}
