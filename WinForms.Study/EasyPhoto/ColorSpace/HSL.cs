using System;
using System.Drawing;

namespace EasyPhoto.ColorSpace
{
  /// <summary>
  /// HSL ɫ�ʿռ�ṹ��
  /// </summary>
  public struct HSL
  {
    float h, s, l;

    /// <summary>
    /// ��ȡ������ɫ��[0, 360]
    /// </summary>
    public float Hue
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
    public float Saturation
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
    public float Luminance
    {
      get
      {
        return l;
      }
      set
      {
        l = value;
        if (l < 0.0f) l = 0.0f;
        if (l > 1.0f) l = 1.0f;
      }
    }


    public override bool Equals(object obj)
    {
      return this == (HSL)obj;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }


    /// <summary>
    /// �ж� HSL �ṹ���Ƿ����
    /// </summary>
    /// <param name="lHsl">HSL �ṹ�� 1</param>
    /// <param name="rHsl">HSL �ṹ�� 2</param>
    /// <returns></returns>
    public static bool operator ==(HSL lHsl, HSL rHsl)
    {
      if ((lHsl.Hue == rHsl.Hue) &&
          (lHsl.Saturation == rHsl.Saturation) &&
          (lHsl.Luminance == rHsl.Luminance))
      {
        return true;
      }
      else
      {
        return false;
      }
    }


    /// <summary>
    /// �ж� HSL �ṹ���Ƿ����
    /// </summary>
    /// <param name="lHsl">HSL �ṹ�� 1</param>
    /// <param name="rHsl">HSL �ṹ�� 2</param>
    /// <returns></returns>
    public static bool operator !=(HSL lHsl, HSL rHsl)
    {
      return !(lHsl == rHsl);
    }


    /// <summary>
    /// ���� (hue, saturation, luminance) �������� EasyPhoto.ColorSpace.HSL �ṹ��
    /// </summary>
    /// <param name="hue">ɫ��[0, 360]</param>
    /// <param name="saturation">���Ͷ�[0, 1]</param>
    /// <param name="luminance">����[0, 1]</param>
    /// <returns></returns>
    public static HSL FromHsl(float hue, float saturation, float luminance)
    {
      HSL hsl = new HSL();

      hsl.Hue = hue;
      hsl.Saturation = saturation;
      hsl.Luminance = luminance;

      return hsl;
    } // end of FromHsl


    /// <summary>
    /// ���� Color �ṹ�彨�� EasyPhoto.ColorSpace.HSL �ṹ��
    /// </summary>
    /// <param name="color">RGB ��ɫ�ṹ��</param>
    /// <returns></returns>
    public static HSL FromColor(Color color)
    {
      HSL hsl = new HSL();

      hsl.Hue = color.GetHue();
      hsl.Saturation = color.GetSaturation();
      hsl.Luminance = color.GetBrightness();

      return hsl;
    } // end of FromColor


    /// <summary>
    /// ���� (red, green, blue) ��ɫ�������� EasyPhoto.ColorSpace.HSL �ṹ��
    /// </summary>
    /// <param name="red">red ����</param>
    /// <param name="green">green ����</param>
    /// <param name="blue">blue ����</param>
    /// <returns></returns>
    public static HSL FromRgb(byte red, byte green, byte blue)
    {
      return FromColor(Color.FromArgb(red, green, blue));
    } // end of FromRgb


    /// <summary>
    /// ��ȡ RGB ��ɫֵ
    /// </summary>
    /// <returns></returns>
    public Color ToRgb()
    {
      double r = 0, g = 0, b = 0;

      double t1, t2;

      double normalisedH = h / 360.0;

      if (l == 0)
      {
        r = g = b = 0;
      }
      else
      {
        if (s == 0)
        {
          r = g = b = l;
        }
        else
        {
          t2 = ((l <= 0.5) ? l * (1.0 + s) : l + s - l * s);
          t1 = 2.0 * l - t2;

          double[] T3 = new double[] { 
              normalisedH + 1.0 / 3.0, 
              normalisedH, 
              normalisedH - 1.0 / 3.0 };

          double[] C = new double[3];

          for (int i = 0; i < 3; i++)
          {
            if (T3[i] < 0) T3[i] += 1.0;
            if (T3[i] > 1) T3[i] -= 1.0;

            if (6.0 * T3[i] < 1.0)
              C[i] = t1 + (t2 - t1) * T3[i] * 6.0;
            else if (2.0 * T3[i] < 1.0)
              C[i] = t2;
            else if (3.0 * T3[i] < 2.0)
              C[i] = (t1 + (t2 - t1) * ((2.0 / 3.0) - T3[i]) * 6.0);
            else
              C[i] = t1;
          } // i

          r = C[0];
          g = C[1];
          b = C[2];
        } // s==0
      } // l==0

      return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));
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


  } // end of class HSL

}
