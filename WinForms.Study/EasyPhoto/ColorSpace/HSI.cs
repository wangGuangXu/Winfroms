using System;
using System.Drawing;

namespace EasyPhoto.ColorSpace
{
  /// <summary>
  /// HSI ɫ�ʿռ�ṹ��
  /// </summary>
  public struct HSI
  {
    double h, s, i;

    /// <summary>
    /// ��ȡ������ɫ��[0, 360]
    /// </summary>
    public double H
    {
      get
      {
        return h;
      }
      set
      {
        h = (float)((int)(value) % 360);
      }
    }

    /// <summary>
    /// ��ȡ�����ñ��Ͷ�[0, 1]
    /// </summary>
    public double S
    {
      get
      {
        return s;
      }
      set
      {
        s = value;
        if (s < 0.0) s = 0.0f;
        if (s > 1.0) s = 1.0f;
      }
    }

    /// <summary>
    /// ��ȡ����������[0, 1]
    /// </summary>
    public double I
    {
      get
      {
        return i;
      }
      set
      {
        i = value;
        if (i < 0.0) i = 0.0;
        if (i > 1.0) i = 1.0;
      }
    }


    public override bool Equals(object obj)
    {
      return this == (HSI)obj;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }


    /// <summary>
    /// �ж� HSI �ṹ���Ƿ����
    /// </summary>
    /// <param name="lHsi">HSI �ṹ�� 1</param>
    /// <param name="rHsi">HSI �ṹ�� 2</param>
    /// <returns></returns>
    public static bool operator ==(HSI lHsi, HSI rHsi)
    {
      if ((lHsi.H == rHsi.H) &&
          (lHsi.S == rHsi.S) &&
          (lHsi.I == rHsi.I))
      {
        return true;
      }
      else
      {
        return false;
      }
    }


    /// <summary>
    /// �ж� HSI �ṹ���Ƿ����
    /// </summary>
    /// <param name="lHsi">HSI �ṹ�� 1</param>
    /// <param name="rHsi">HSI �ṹ�� 2</param>
    /// <returns></returns>
    public static bool operator !=(HSI lHsi, HSI rHsi)
    {
      return !(lHsi == rHsi);
    }


    /// <summary>
    /// ���� (h, s, i) �������� EasyPhoto.ColorSpace.HSI �ṹ��
    /// </summary>
    /// <param name="h">ɫ��[0, 360]</param>
    /// <param name="s">���Ͷ�[0, 1]</param>
    /// <param name="i">����[0, 1]</param>
    /// <returns></returns>
    public static HSI FromHsi(double h, double s, double i)
    {
      HSI hsi = new HSI();

      hsi.H = h;
      hsi.S = s;
      hsi.I = i;

      return hsi;
    } // end of FromHsi


    /// <summary>
    /// ���� Color �ṹ�彨�� EasyPhoto.ColorSpace.HSI �ṹ��
    /// </summary>
    /// <param name="color">RGB ��ɫ�ṹ��</param>
    /// <returns></returns>
    public static HSI FromColor(Color color)
    {
      return FromRgb((byte)color.R, (byte)color.G, (byte)color.B);
    } // end of FromColor


    /// <summary>
    /// ���� (red, green, blue) ��ɫ�������� EasyPhoto.ColorSpace.HSI �ṹ��
    /// </summary>
    /// <param name="red">red ����</param>
    /// <param name="green">green ����</param>
    /// <param name="blue">blue ����</param>
    /// <returns></returns>
    public static HSI FromRgb(byte red, byte green, byte blue)
    {
      double h, s, i;

      double v1 = 0.7071 * (green - blue);
      double v2 = 0.81650 * red - 0.40824 * (green + blue);

      h = Math.Atan2(v1, v2);
      s = Math.Sqrt(v1 * v1 + v2 * v2);
      i = 0.57735 * (red + green + blue);

      return FromHsi(h, s, i);
    } // end of FromRgb


    /// <summary>
    /// ��ȡ RGB ��ɫֵ
    /// </summary>
    /// <returns></returns>
    public Color ToRgb()
    {
      double cos = s * Math.Cos(h);
      double sin = s * Math.Sin(h);

      int r = (int)(0.81650 * sin + 0.57735 * i);
      int g = (int)(0.7071 * cos - 0.40824 * sin + 0.57735 * i);
      int b = (int)(-0.7071 * cos - 0.40824 * sin + 0.57735 * i);

      return Color.FromArgb(r, g, b);
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


  } // end of class HSI

}
