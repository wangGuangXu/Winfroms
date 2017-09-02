using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EasyPhoto.ImageProcess
{
  /// <summary>
  /// ����������
  /// </summary>
  public class RegionClip
  {
    private Region region = null;

    /// <summary>
    /// ��ȡ����������
    /// </summary>
    public Region SelectedRegion
    {
      get
      {
        return region;
      }
      set
      {
        region = value;
      }
    }


    /// <summary>
    /// ��ʼ������������
    /// </summary>
    /// <param name="region">��������</param>
    public RegionClip(Region region)
    {
      this.region = region;
    }


    /// <summary>
    /// ȥ����ͼ����ָ������
    /// </summary>
    /// <param name="b">λͼ</param>
    /// <returns></returns>
    public Bitmap Remove(Bitmap b)
    {
      int width = b.Width;
      int height = b.Height;

      Bitmap dstImage = new Bitmap(width, height);
      Graphics g = System.Drawing.Graphics.FromImage(dstImage);
      Region all = new Region(new Rectangle(0, 0, width, height));
      Region validRegion = (Region)this.region.Clone();
      validRegion.Complement(all);
      g.SetClip(validRegion, System.Drawing.Drawing2D.CombineMode.Replace);
      g.DrawImage(b, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);

      b.Dispose();
      return dstImage;
    } // end of Remove


    /// <summary>
    /// ������ͼ����ָ������
    /// </summary>
    /// <param name="b">λͼ</param>
    /// <returns></returns>
    public Bitmap Hold(Bitmap b)
    {
      ImageTransform it = new ImageTransform();
      return it.Crop(b, (Region)this.region.Clone());
    } // end of Hold


    /// <summary>
    /// ��������ָ��������ǰ������Ӧ�����滻
    /// </summary>
    /// <param name="bgImage">����</param>
    /// <param name="fgImage">ǰ��</param>
    /// <returns></returns>
    public Bitmap Replace(Bitmap bgImage, Bitmap fgImage)
    {
      int width = bgImage.Width;
      int height = bgImage.Height;

      Graphics g = System.Drawing.Graphics.FromImage(bgImage);
      g.SetClip(this.region, System.Drawing.Drawing2D.CombineMode.Replace);
      g.DrawImage(fgImage, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);

      fgImage.Dispose();
      return bgImage;
    } // end of Replace


  }
}
