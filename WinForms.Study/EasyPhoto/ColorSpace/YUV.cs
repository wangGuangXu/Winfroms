using System;
using System.Drawing;

namespace EasyPhoto.ColorSpace
{
  /// <summary>
  /// YUV ɫ�ʿռ�ṹ��
  /// </summary>
  public struct YUV
	{
    byte y, u, v;

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
    /// ��ȡ������ U ����
    /// </summary>
    public byte U
    {
      get
      {
        return u;
      }
      set
      {
        u = value;
      }
    }

    /// <summary>
    /// ��ȡ������ V ����
    /// </summary>
    public byte V
    {
      get
      {
        return v;
      }
      set
      {
        v = value;
      }
    }


    public override bool Equals(object obj)
    {
      return this == (YUV)obj;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }


    /// <summary>
    /// �ж� YUV �ṹ���Ƿ����
    /// </summary>
    /// <param name="lYuv">YUV �ṹ�� 1</param>
    /// <param name="rYuv">YUV �ṹ�� 2</param>
    /// <returns></returns>
    public static bool operator ==(YUV lYuv, YUV rYuv)
    {
      if ((lYuv.Y == rYuv.Y) &&
          (lYuv.U == rYuv.U) &&
          (lYuv.V == rYuv.V))
      {
        return true;
      }
      else
      {
        return false;
      }
    }


    /// <summary>
    /// �ж� YUV �ṹ���Ƿ����
    /// </summary>
    /// <param name="lYuv">YUV �ṹ�� 1</param>
    /// <param name="rYuv">YUV �ṹ�� 2</param>
    /// <returns></returns>
    public static bool operator !=(YUV lYuv, YUV rYuv)
    {
      return !(lYuv == rYuv);
    }


    /// <summary>
    /// ���� (Y, U, V) �������� EasyPhoto.ColorSpace.YUV �ṹ��
    /// </summary>
    /// <param name="y">Y ����</param>
    /// <param name="u">U ����</param>
    /// <param name="v">V ����</param>
    /// <returns></returns>
    public static YUV FromYuv(byte y, byte u, byte v)
		{
      YUV yuv = new YUV();

      yuv.Y = y;
      yuv.U = u;
      yuv.V = v;

      return yuv;
		} // end of FromYuv


    /// <summary>
    /// ���� Color �ṹ�彨�� EasyPhoto.ColorSpace.YUV �ṹ��
    /// </summary>
    /// <param name="color">RGB ��ɫ�ṹ��</param>
    /// <returns></returns>
    public static YUV FromColor(Color color)
		{
      return FromRgb((byte)color.R, (byte)color.G, (byte)color.B);
    } // end of FromColor


    /// <summary>
    /// ���� (red, green, blue) ��ɫ�������� EasyPhoto.ColorSpace.YUV �ṹ��
    /// </summary>
    /// <param name="red">red ����</param>
    /// <param name="green">green ����</param>
    /// <param name="blue">blue ����</param>
    /// <returns></returns>
    public static YUV FromRgb(byte red, byte green, byte blue)
    {
      byte y, u, v;

      y = (byte)(((66 * red + 129 * green + 25 * blue + 128) >> 8) + 16);
      u = (byte)(((-38 * red - 74 * green + 112 * blue + 128) >> 8) + 128);
      v = (byte)(((112 * red - 94 * green - 18 * blue + 128) >> 8) + 128);

      return FromYuv(y, u, v);
    } // end of FromRgb


    /// <summary>
    /// ��ȡ RGB ��ɫֵ
    /// </summary>
    /// <returns></returns>
    public Color ToRgb()
    { 
			int C = y - 16;
			int D = u - 128;
			int E = v - 128;

			int R, G, B;

			R = Math.Max(Math.Min((( 298 * C           + 409 * E + 128) >> 8), 255), 0);
			G = Math.Max(Math.Min((( 298 * C - 100 * D - 208 * E + 128) >> 8), 255), 0);
			B = Math.Max(Math.Min((( 298 * C + 516 * D           + 128) >> 8), 255), 0);

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
