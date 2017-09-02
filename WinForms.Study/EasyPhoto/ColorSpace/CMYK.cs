using System;
using System.Drawing;

namespace EasyPhoto.ColorSpace
{
  /// <summary>
  /// CMYK ɫ�ʿռ�ṹ��
  /// </summary>
  public struct CMYK
  {
    byte c, m, y, k;

    /// <summary>
    /// ��ȡ������ C ����
    /// </summary>
    public byte C
    {
      get
      {
        return c;
      }
      set
      {
        c = value;
      }
    }

    /// <summary>
    /// ��ȡ������ M ����
    /// </summary>
    public byte M
    {
      get
      {
        return m;
      }
      set
      {
        m = value;
      }
    }

    /// <summary>
    /// ��ȡ������ Y ����
    /// </summary>
    public byte Y
    {
      get
      {
        return y;
      }
      set
      {
        y = value;
      }
    }

    /// <summary>
    /// ��ȡ������ K ����
    /// </summary>
    public byte K
    {
      get
      {
        return k;
      }
      set
      {
        k = value;
      }
    }


    public override bool Equals(object obj)
    {
      return this == (CMYK)obj;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }


    /// <summary>
    /// �ж� CMYK �ṹ���Ƿ����
    /// </summary>
    /// <param name="lCmyk">CMYK �ṹ�� 1</param>
    /// <param name="rCmyk">CMYK �ṹ�� 2</param>
    /// <returns></returns>
    public static bool operator ==(CMYK lCmyk, CMYK rCmyk)
    {
      if ((lCmyk.C == rCmyk.C) &&
          (lCmyk.M == rCmyk.M) &&
          (lCmyk.Y == rCmyk.Y) &&
          (lCmyk.K == rCmyk.K))
      {
        return true;
      }
      else
      {
        return false;
      }
    }


    /// <summary>
    /// �ж� CMYK �ṹ���Ƿ����
    /// </summary>
    /// <param name="lCmyk">CMYK �ṹ�� 1</param>
    /// <param name="rCmyk">CMYK �ṹ�� 2</param>
    /// <returns></returns>
    public static bool operator !=(CMYK lCmyk, CMYK rCmyk)
    {
      return !(lCmyk == rCmyk);
    }


    /// <summary>
    /// ���� (c, m, y, k) �������� EasyPhoto.ColorSpace.CMYK �ṹ��
    /// </summary>
    /// <param name="c">C ����</param>
    /// <param name="m">M ����</param>
    /// <param name="y">Y ����</param>
    /// <param name="k">K ����</param>
    public static CMYK FromCmyk(byte c, byte m, byte y, byte k)
    {
      CMYK cmyk = new CMYK();

      cmyk.c = c;
      cmyk.m = m;
      cmyk.y = y;
      cmyk.k = k;

      return cmyk;
    } // end of FromCmyk


    /// <summary>
    /// ���� Color �ṹ�彨�� EasyPhoto.ColorSpace.CMYK �ṹ��
    /// </summary>
    /// <param name="color">RGB ��ɫ�ṹ��</param>
    /// <returns></returns>
    public static CMYK FromColor(Color color)
    {
      return FromRgb((byte)color.R, (byte)color.G, (byte)color.B);
    } // end of FromColor


    /// <summary>
    /// ���� (red, green, blue) ��ɫ�������� EasyPhoto.ColorSpace.CMYK �ṹ��
    /// </summary>
    /// <param name="red">red ����</param>
    /// <param name="green">green ����</param>
    /// <param name="blue">blue ����</param>
    /// <returns></returns>
    public static CMYK FromRgb(byte red, byte green, byte blue)
    {
      byte c, m, y, k;

      c = (byte)(255 - red);
      m = (byte)(255 - green);
      y = (byte)(255 - blue);

      k = Math.Min(c, Math.Min(m, y));
      c -= k;
      m -= k;
      y -= k;

      return FromCmyk(c, m, y, k);
    } // end of FromRgb


    /// <summary>
    /// ��ȡ RGB ��ɫֵ
    /// </summary>
    /// <returns></returns>
    public Color ToRgb()
    {
      int R, G, B;

      R = 255 - c - k;
      G = 255 - m - k;
      B = 255 - y - k;

      return Color.FromArgb(R, G, B);
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
