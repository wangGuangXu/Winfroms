using System;
using System.Reflection;

namespace EasyPhoto.ImageProcess
{
  /// <summary>
  /// EasyPhoto ͼ����Ϣ����
  /// </summary>
  public class ImageInfo
  {
    /// <summary>
    /// ÿ�����ֽ��� BytesPerPixel
    /// </summary>
    public const int BPP = 4;

    /// <summary>
    /// ÿ�������ֽ��� BytesPer2Pixels
    /// </summary>
    public const int BP2P = 8;


    /**************************************************
     * 
     * �����Ϣ
     * 
     * ��Ʒ�����汾��
     * 
     **************************************************/

    /// <summary>
    /// ��ȡ EasyPhoto ��Ʒ��
    /// </summary>
    public string Product
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
        if (attributes.Length == 0)
          return "";

        return ((AssemblyProductAttribute)attributes[0]).Product;
      }
    }

    /// <summary>
    /// ��ȡ EasyPhoto ��ǰ�汾��
    /// </summary>
    public string Version
    {
      get
      {
        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
      }
    }


  }
}
