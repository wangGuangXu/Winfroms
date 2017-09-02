using System;
using System.Drawing;

namespace EasyPhoto.ImageProcess
{
  public partial class Morphologic : GrayProcessing
  {
    /************************************************************
     * 
     * ˮƽ����ֱ��ʮ���͸�ʴ
     * ˮƽ����ֱ��ʮ��������
     * ����������
     * ϸ�����ֻ�
     * 
     ************************************************************/

    
    /// <summary>
    /// ˮƽ��ʴ
    /// </summary>
    /// <param name="b">��ֵλͼ��</param>
    /// <returns></returns>
    public Bitmap ErosionHorz(Bitmap b)
    {
      // �Ƚ�ԭʼ��ֵͼת��Ϊ��ά����
      byte[,] srcGray = Image2Array(b);

      // ����ˮƽ��ʴ����
      byte[,] dstGray = ErosionHorz(srcGray);

      b.Dispose();

      // ת��Ϊ�Ҷ�ͼ��
      return Array2Image(dstGray);
    } // end of ErosionHorz


    /// <summary>
    /// ��ֱ��ʴ
    /// </summary>
    /// <param name="b">��ֵλͼ��</param>
    /// <returns></returns>
    public Bitmap ErosionVert(Bitmap b)
    {
      // �Ƚ�ԭʼ��ֵͼת��Ϊ��ά����
      byte[,] srcGray = Image2Array(b);

      // ���д�ֱ��ʴ����
      byte[,] dstGray = ErosionVert(srcGray);

      b.Dispose();

      // ת��Ϊ�Ҷ�ͼ��
      return Array2Image(dstGray);
    } // end of ErosionVert


    /// <summary>
    /// ʮ���͸�ʴ
    /// </summary>
    /// <param name="b">��ֵλͼ��</param>
    /// <returns></returns>
    public Bitmap ErosionCross(Bitmap b)
    {
      // �Ƚ�ԭʼ��ֵͼת��Ϊ��ά����
      byte[,] srcGray = Image2Array(b);

      // ����ʮ���͸�ʴ����
      byte[,] dstGray = ErosionCross(srcGray);

      b.Dispose();

      // ת��Ϊ�Ҷ�ͼ��
      return Array2Image(dstGray);
    } // end of ErosionCross


    /// <summary>
    /// ˮƽ����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap DilationHorz(Bitmap b)
    {
      // �Ƚ�ԭʼ��ֵͼת��Ϊ��ά����
      byte[,] srcGray = Image2Array(b);

      // ����ˮƽ���ʹ���
      byte[,] dstGray = DilationHorz(srcGray);

      b.Dispose();

      // ת��Ϊ�Ҷ�ͼ��
      return Array2Image(dstGray);
    } // end of DilationHorz


    /// <summary>
    /// ��ֱ����
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap DilationVert(Bitmap b)
    {
      // �Ƚ�ԭʼ��ֵͼת��Ϊ��ά����
      byte[,] srcGray = Image2Array(b);

      // ���д�ֱ���ʹ���
      byte[,] dstGray = DilationVert(srcGray);

      b.Dispose();

      // ת��Ϊ�Ҷ�ͼ��
      return Array2Image(dstGray);
    } // end of DilationVert


    /// <summary>
    /// ʮ��������
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap DilationCross(Bitmap b)
    {
      // �Ƚ�ԭʼ��ֵͼת��Ϊ��ά����
      byte[,] srcGray = Image2Array(b);

      // ����ʮ�������ʹ���
      byte[,] dstGray = DilationCross(srcGray);

      b.Dispose();

      // ת��Ϊ�Ҷ�ͼ��
      return Array2Image(dstGray);
    } // end of DilationCross


    /// <summary>
    /// ������
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Opening(Bitmap b)
    {
      // �ȸ�ʴ��������
      b = ErosionCross(b);
      b = DilationCross(b);

      return b;
    } // end of Opening


    /// <summary>
    /// ������
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Closing(Bitmap b)
    {
      // �����ͣ���ʴ
      b = DilationCross(b);
      b = ErosionCross(b);

      return b;
    } // end of Closing


    /// <summary>
    /// ϸ��
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Thinning(Bitmap b)
    {
      // �Ƚ�ԭʼ��ֵͼת��Ϊ��ά����
      byte[,] srcGray = Image2Array(b);

      // ����ϸ������
      byte[,] dstGray = Thinning(srcGray);

      b.Dispose();

      // ת��Ϊ�Ҷ�ͼ��
      return Array2Image(dstGray);
    } // end of Thinning


    /// <summary>
    /// �ֻ�
    /// </summary>
    /// <param name="b">λͼ��</param>
    /// <returns></returns>
    public Bitmap Thickening(Bitmap b)
    {
      // �Ƚ�ԭʼ��ֵͼת��Ϊ��ά����
      byte[,] srcGray = Image2Array(b);

      // ���дֻ�����
      byte[,] dstGray = Thickening(srcGray);

      b.Dispose();

      // ת��Ϊ�Ҷ�ͼ��
      return Array2Image(dstGray);
    } // end of Thickening


  }
}
