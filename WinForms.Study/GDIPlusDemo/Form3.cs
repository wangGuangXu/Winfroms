using System;
using System.Drawing;
using System.Drawing.Text;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GDIPlusDemo
{
	/// <summary>
	/// Form3 ��ժҪ˵����
	/// </summary>
public class Form3 : System.Windows.Forms.Form
{
	private System.Windows.Forms.GroupBox groupBox1;
	private System.Windows.Forms.GroupBox groupBox2;
	private System.Windows.Forms.GroupBox groupBox3;
	private System.Windows.Forms.GroupBox groupBox4;
	private System.Windows.Forms.GroupBox groupBox5;
	private System.Windows.Forms.GroupBox groupBox6;
	private System.Windows.Forms.Label label1;
	//����ϵ�������б�	
	private System.Windows.Forms.ListBox FontlistBox;
	//����������ĸ�ѡ��
	private System.Windows.Forms.CheckBox FontStyle_Strikeout;
	private System.Windows.Forms.CheckBox FontStyle_Underline;
	private System.Windows.Forms.CheckBox FontStyle_Italic;
	private System.Windows.Forms.CheckBox FontStyle_Bold;
	private System.Windows.Forms.CheckBox FontStyle_Regular;
	//����ɫ��ARGB��Ӧ��΢���ؼ�	
	private System.Windows.Forms.NumericUpDown FontColor_A;
	private System.Windows.Forms.NumericUpDown FontColor_R;
	private System.Windows.Forms.NumericUpDown FontColor_G;
	private System.Windows.Forms.NumericUpDown FontColor_B;
	//�����С��λ���߸���ѡ��ť
	private System.Windows.Forms.RadioButton FontUnit_Dot;
	private System.Windows.Forms.RadioButton FontUnit_Pixel;
	private System.Windows.Forms.RadioButton FontUnit_Inch;
	private System.Windows.Forms.RadioButton FontUnit_Millimeter;
	private System.Windows.Forms.RadioButton FontUnit_Display;
	private System.Windows.Forms.RadioButton FontUnit_World;
	private System.Windows.Forms.RadioButton FontUnit_Document;
	//�����С��Ӧ��΢���ؼ�
	private System.Windows.Forms.NumericUpDown FontSize;
	//����Ԥ������
	private System.Windows.Forms.Label FontPreview;

	/// <summary>
	/// ����������������
	/// </summary>
	private System.ComponentModel.Container components = null;

	public Form3()
	{
		//
		// Windows ���������֧���������
		//
		InitializeComponent();

		//
		// TODO: �� InitializeComponent ���ú�����κι��캯������
		//
	}

	/// <summary>
	/// ������������ʹ�õ���Դ��
	/// </summary>
	protected override void Dispose( bool disposing )
	{
		if( disposing )
		{
			if(components != null)
			{
				components.Dispose();
			}
		}
		base.Dispose( disposing );
	}

	#region Windows ������������ɵĴ���
	/// <summary>
	/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
	/// �˷��������ݡ�
	/// </summary>
	private void InitializeComponent()
	{
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.FontStyle_Strikeout = new System.Windows.Forms.CheckBox();
		this.FontStyle_Underline = new System.Windows.Forms.CheckBox();
		this.FontStyle_Italic = new System.Windows.Forms.CheckBox();
		this.FontStyle_Bold = new System.Windows.Forms.CheckBox();
		this.FontStyle_Regular = new System.Windows.Forms.CheckBox();
		this.FontColor_R = new System.Windows.Forms.NumericUpDown();
		this.FontUnit_Dot = new System.Windows.Forms.RadioButton();
		this.FontColor_A = new System.Windows.Forms.NumericUpDown();
		this.FontlistBox = new System.Windows.Forms.ListBox();
		this.FontColor_G = new System.Windows.Forms.NumericUpDown();
		this.FontColor_B = new System.Windows.Forms.NumericUpDown();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.FontUnit_Pixel = new System.Windows.Forms.RadioButton();
		this.FontUnit_Inch = new System.Windows.Forms.RadioButton();
		this.FontUnit_Millimeter = new System.Windows.Forms.RadioButton();
		this.FontUnit_Display = new System.Windows.Forms.RadioButton();
		this.FontUnit_World = new System.Windows.Forms.RadioButton();
		this.FontUnit_Document = new System.Windows.Forms.RadioButton();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.FontSize = new System.Windows.Forms.NumericUpDown();
		this.FontPreview = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.FontColor_R)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.FontColor_A)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.FontColor_G)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.FontColor_B)).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.FontSize)).BeginInit();
		this.SuspendLayout();
		// 
		// groupBox1
		// 
		this.groupBox1.Controls.Add(this.FontStyle_Strikeout);
		this.groupBox1.Controls.Add(this.FontStyle_Underline);
		this.groupBox1.Controls.Add(this.FontStyle_Italic);
		this.groupBox1.Controls.Add(this.FontStyle_Bold);
		this.groupBox1.Controls.Add(this.FontStyle_Regular);
		this.groupBox1.Location = new System.Drawing.Point(160, 72);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(328, 48);
		this.groupBox1.TabIndex = 19;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "����������";
		// 
		// FontStyle_Strikeout
		// 
		this.FontStyle_Strikeout.Location = new System.Drawing.Point(248, 16);
		this.FontStyle_Strikeout.Name = "FontStyle_Strikeout";
		this.FontStyle_Strikeout.Size = new System.Drawing.Size(64, 24);
		this.FontStyle_Strikeout.TabIndex = 4;
		this.FontStyle_Strikeout.Text = "ǿ����";
		this.FontStyle_Strikeout.CheckedChanged += new System.EventHandler(this.FontStyle_Strikeout_CheckedChanged);
		// 
		// FontStyle_Underline
		// 
		this.FontStyle_Underline.Location = new System.Drawing.Point(176, 16);
		this.FontStyle_Underline.Name = "FontStyle_Underline";
		this.FontStyle_Underline.Size = new System.Drawing.Size(64, 24);
		this.FontStyle_Underline.TabIndex = 3;
		this.FontStyle_Underline.Text = "�»���";
		this.FontStyle_Underline.CheckedChanged += new System.EventHandler(this.FontStyle_Underline_CheckedChanged);
		// 
		// FontStyle_Italic
		// 
		this.FontStyle_Italic.Location = new System.Drawing.Point(112, 16);
		this.FontStyle_Italic.Name = "FontStyle_Italic";
		this.FontStyle_Italic.Size = new System.Drawing.Size(48, 24);
		this.FontStyle_Italic.TabIndex = 2;
		this.FontStyle_Italic.Text = "��б";
		this.FontStyle_Italic.CheckedChanged += new System.EventHandler(this.FontStyle_Italic_CheckedChanged);
		// 
		// FontStyle_Bold
		// 
		this.FontStyle_Bold.Location = new System.Drawing.Point(56, 16);
		this.FontStyle_Bold.Name = "FontStyle_Bold";
		this.FontStyle_Bold.Size = new System.Drawing.Size(48, 24);
		this.FontStyle_Bold.TabIndex = 1;
		this.FontStyle_Bold.Text = "�Ӵ�";
		this.FontStyle_Bold.CheckedChanged += new System.EventHandler(this.FontStyle_Bold_CheckedChanged);
		// 
		// FontStyle_Regular
		// 
		this.FontStyle_Regular.Location = new System.Drawing.Point(8, 16);
		this.FontStyle_Regular.Name = "FontStyle_Regular";
		this.FontStyle_Regular.Size = new System.Drawing.Size(48, 24);
		this.FontStyle_Regular.TabIndex = 0;
		this.FontStyle_Regular.Text = "����";
		this.FontStyle_Regular.CheckedChanged += new System.EventHandler(this.FontStyle_Regular_CheckedChanged);
		// 
		// FontColor_R
		// 
		this.FontColor_R.Location = new System.Drawing.Point(256, 40);
		this.FontColor_R.Name = "FontColor_R";
		this.FontColor_R.Size = new System.Drawing.Size(56, 21);
		this.FontColor_R.TabIndex = 15;
		this.FontColor_R.ValueChanged += new System.EventHandler(this.FontColor_R_ValueChanged);
		// 
		// FontUnit_Dot
		// 
		this.FontUnit_Dot.Location = new System.Drawing.Point(168, 144);
		this.FontUnit_Dot.Name = "FontUnit_Dot";
		this.FontUnit_Dot.Size = new System.Drawing.Size(52, 24);
		this.FontUnit_Dot.TabIndex = 12;
		this.FontUnit_Dot.Text = "��";
		this.FontUnit_Dot.CheckedChanged += new System.EventHandler(this.FontUnit_Dot_CheckedChanged);
		// 
		// FontColor_A
		// 
		this.FontColor_A.Location = new System.Drawing.Point(168, 40);
		this.FontColor_A.Name = "FontColor_A";
		this.FontColor_A.Size = new System.Drawing.Size(56, 21);
		this.FontColor_A.TabIndex = 13;
		this.FontColor_A.ValueChanged += new System.EventHandler(this.FontColor_A_ValueChanged);
		// 
		// FontlistBox
		// 
		this.FontlistBox.ItemHeight = 12;
		this.FontlistBox.Location = new System.Drawing.Point(8, 16);
		this.FontlistBox.Name = "FontlistBox";
		this.FontlistBox.Size = new System.Drawing.Size(136, 244);
		this.FontlistBox.TabIndex = 18;
		this.FontlistBox.SelectedIndexChanged += new System.EventHandler(this.FontlistBox_SelectedIndexChanged);
		// 
		// FontColor_G
		// 
		this.FontColor_G.Location = new System.Drawing.Point(336, 40);
		this.FontColor_G.Name = "FontColor_G";
		this.FontColor_G.Size = new System.Drawing.Size(56, 21);
		this.FontColor_G.TabIndex = 16;
		this.FontColor_G.ValueChanged += new System.EventHandler(this.FontColor_G_ValueChanged);
		// 
		// FontColor_B
		// 
		this.FontColor_B.Location = new System.Drawing.Point(400, 40);
		this.FontColor_B.Name = "FontColor_B";
		this.FontColor_B.Size = new System.Drawing.Size(56, 21);
		this.FontColor_B.TabIndex = 20;
		this.FontColor_B.ValueChanged += new System.EventHandler(this.FontColor_B_ValueChanged);
		// 
		// groupBox2
		// 
		this.groupBox2.Location = new System.Drawing.Point(160, 16);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(328, 56);
		this.groupBox2.TabIndex = 21;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "��������ɫ�ʣ�ARGB��";
		// 
		// FontUnit_Pixel
		// 
		this.FontUnit_Pixel.Location = new System.Drawing.Point(320, 144);
		this.FontUnit_Pixel.Name = "FontUnit_Pixel";
		this.FontUnit_Pixel.Size = new System.Drawing.Size(52, 24);
		this.FontUnit_Pixel.TabIndex = 22;
		this.FontUnit_Pixel.Text = "����";
		this.FontUnit_Pixel.CheckedChanged += new System.EventHandler(this.FontUnit_Pixel_CheckedChanged);
		// 
		// FontUnit_Inch
		// 
		this.FontUnit_Inch.Location = new System.Drawing.Point(240, 144);
		this.FontUnit_Inch.Name = "FontUnit_Inch";
		this.FontUnit_Inch.Size = new System.Drawing.Size(52, 24);
		this.FontUnit_Inch.TabIndex = 23;
		this.FontUnit_Inch.Text = "Ӣ��";
		this.FontUnit_Inch.CheckedChanged += new System.EventHandler(this.FontUnit_Inch_CheckedChanged);
		// 
		// FontUnit_Millimeter
		// 
		this.FontUnit_Millimeter.Location = new System.Drawing.Point(392, 144);
		this.FontUnit_Millimeter.Name = "FontUnit_Millimeter";
		this.FontUnit_Millimeter.Size = new System.Drawing.Size(52, 24);
		this.FontUnit_Millimeter.TabIndex = 24;
		this.FontUnit_Millimeter.Text = "����";
		this.FontUnit_Millimeter.CheckedChanged += new System.EventHandler(this.FontUnit_Millimeter_CheckedChanged);
		// 
		// FontUnit_Display
		// 
		this.FontUnit_Display.Location = new System.Drawing.Point(168, 168);
		this.FontUnit_Display.Name = "FontUnit_Display";
		this.FontUnit_Display.Size = new System.Drawing.Size(112, 24);
		this.FontUnit_Display.TabIndex = 25;
		this.FontUnit_Display.Text = "������豸��ͬ";
		this.FontUnit_Display.CheckedChanged += new System.EventHandler(this.FontUnit_Display_CheckedChanged);
		// 
		// FontUnit_World
		// 
		this.FontUnit_World.Location = new System.Drawing.Point(392, 168);
		this.FontUnit_World.Name = "FontUnit_World";
		this.FontUnit_World.Size = new System.Drawing.Size(80, 24);
		this.FontUnit_World.TabIndex = 26;
		this.FontUnit_World.Text = "��������";
		this.FontUnit_World.CheckedChanged += new System.EventHandler(this.FontUnit_World_CheckedChanged);
		// 
		// FontUnit_Document
		// 
		this.FontUnit_Document.Location = new System.Drawing.Point(304, 168);
		this.FontUnit_Document.Name = "FontUnit_Document";
		this.FontUnit_Document.Size = new System.Drawing.Size(80, 24);
		this.FontUnit_Document.TabIndex = 27;
		this.FontUnit_Document.Text = "�ĵ���λ";
		this.FontUnit_Document.CheckedChanged += new System.EventHandler(this.FontUnit_Document_CheckedChanged);
		// 
		// groupBox3
		// 
		this.groupBox3.Controls.Add(this.groupBox4);
		this.groupBox3.Location = new System.Drawing.Point(160, 128);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(328, 72);
		this.groupBox3.TabIndex = 28;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "ѡ���С��λ";
		// 
		// groupBox4
		// 
		this.groupBox4.Location = new System.Drawing.Point(8, 160);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(328, 72);
		this.groupBox4.TabIndex = 29;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "ѡ���С��λ";
		// 
		// groupBox5
		// 
		this.groupBox5.Controls.Add(this.label1);
		this.groupBox5.Controls.Add(this.groupBox6);
		this.groupBox5.Controls.Add(this.FontSize);
		this.groupBox5.Location = new System.Drawing.Point(160, 208);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(328, 56);
		this.groupBox5.TabIndex = 30;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "���������С";
		// 
		// label1
		// 
		this.label1.Location = new System.Drawing.Point(24, 24);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(80, 16);
		this.label1.TabIndex = 32;
		this.label1.Text = "�����С";
		// 
		// groupBox6
		// 
		this.groupBox6.Location = new System.Drawing.Point(8, 160);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(328, 72);
		this.groupBox6.TabIndex = 29;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "ѡ���С��λ";
		// 
		// FontSize
		// 
		this.FontSize.Location = new System.Drawing.Point(120, 24);
		this.FontSize.Name = "FontSize";
		this.FontSize.Size = new System.Drawing.Size(56, 21);
		this.FontSize.TabIndex = 31;
		this.FontSize.Value = new System.Decimal(new int[] {
																1,
																0,
																0,
																0});
		this.FontSize.ValueChanged += new System.EventHandler(this.FontSize_ValueChanged);
		// 
		// FontPreview
		// 
		this.FontPreview.Location = new System.Drawing.Point(8, 272);
		this.FontPreview.Name = "FontPreview";
		this.FontPreview.Size = new System.Drawing.Size(480, 104);
		this.FontPreview.TabIndex = 31;
		this.FontPreview.Text = "����ʾ��";
		// 
		// Form3
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
		this.ClientSize = new System.Drawing.Size(496, 382);
		this.Controls.Add(this.FontPreview);
		this.Controls.Add(this.FontUnit_Document);
		this.Controls.Add(this.FontUnit_World);
		this.Controls.Add(this.FontUnit_Display);
		this.Controls.Add(this.FontUnit_Millimeter);
		this.Controls.Add(this.FontUnit_Inch);
		this.Controls.Add(this.FontUnit_Pixel);
		this.Controls.Add(this.FontColor_B);
		this.Controls.Add(this.groupBox1);
		this.Controls.Add(this.FontColor_R);
		this.Controls.Add(this.FontUnit_Dot);
		this.Controls.Add(this.FontColor_A);
		this.Controls.Add(this.FontlistBox);
		this.Controls.Add(this.FontColor_G);
		this.Controls.Add(this.groupBox2);
		this.Controls.Add(this.groupBox3);
		this.Controls.Add(this.groupBox5);
		this.Name = "Form3";
		this.Text = "��ǿ������ѡ��Ի���";
		this.Load += new System.EventHandler(this.Form3_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)(this.FontColor_R)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.FontColor_A)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.FontColor_G)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.FontColor_B)).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)(this.FontSize)).EndInit();
		this.ResumeLayout(false);

	}
	#endregion
//����Ի������ʱ�ĳ�ʼ������
private void Form3_Load(object sender, System.EventArgs e)
{
	string tmp=string.Empty;
	//��ȡ�����Ѿ���װ������ϵ��
	InstalledFontCollection installedFontCollection=new InstalledFontCollection();
	FontFamily[] fontfamily=installedFontCollection.Families;
	int index=0;
	//����fontfamily�����ÿһ����Ա
	foreach (FontFamily i in fontfamily)
	{
		//�������б�����������ϵ����
		this.FontlistBox.Items.Add(i.Name);
		index++;
	}

	//ɫ��ɫ��΢���ؼ���ȡֵ��Χ�����ֵ
	FontColor_A.Maximum=255;
	FontColor_R.Maximum=255;
	FontColor_G.Maximum=255;
	FontColor_B.Maximum=255;
	//��Сֵ
	FontColor_A.Minimum=0;
	FontColor_R.Minimum=0;
	FontColor_G.Minimum=0;
	FontColor_B.Minimum=0;
	//����ɫ��Ĭ��ֵ��
	this.FontColor_A.Value=255;
	this.FontColor_R.Value=0;
	this.FontColor_G.Value=0;
	this.FontColor_B.Value=0;
	//�����С΢���ؼ�ȡֵ��Χ
	this.FontSize.Minimum=1;
	this.FontSize.Maximum=100;

	//Ĭ�ϵ�������Ϊ����
	this.FontStyle_Regular.Checked=true;
	//Ĭ�ϵ����嵥λΪ��
	this.FontUnit_Dot.Checked=true;
	//Ĭ�ϵ������СΪ12
	this.FontSize.Value=12;
	//Ĭ�ϵ�����ϵ��Ϊ�б���еĵ�һ���б���
	this.FontlistBox.SelectedIndex=0;
	this.RedrawFontPreviewWindow();
}

	private void FontlistBox_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}
//�����û����������õ���Ϣ����Ԥ�����н���������ʾ
public void RedrawFontPreviewWindow()
{
	//���δѡ������ϵ������
	if(this.FontlistBox.SelectedIndex==-1)
		return;
	//ʾ���������Ч������ʾ����
	Rectangle textOut=new Rectangle(0,0,
		this.FontPreview.Width,this.FontPreview.Height);

	Graphics graphics=this.FontPreview.CreateGraphics();
	graphics.Clear(this.FontPreview.BackColor);
	//��ȡ��ǰ�Ѿ�ѡ�������ϵ�����Ƽ������С
	string font_name=this.FontlistBox.Text;
	decimal font_size=this.FontSize.Value;

	//ʹ�û�ɫ�������ƻ�׼�ߣ���10*10����Ϊ��λ
	Pen pen=new Pen(Color.Gray);
	//ˮƽ��
	for(int i=0;i<textOut.Height;i+=10)
		graphics.DrawLine(pen,0,i,textOut.Width,i);
	//��ֱ��
	for(int i=0;i<textOut.Width;i+=10)
		graphics.DrawLine(pen,i,0,i,textOut.Height);

	//��ȡ��ǰ�Ѿ�ѡ��������С��λ
	GraphicsUnit font_uint=GraphicsUnit.Point;
	if(this.FontUnit_Document.Checked)
		font_uint=GraphicsUnit.Document;

	if(this.FontUnit_Inch.Checked)
		font_uint=GraphicsUnit.Inch;

	if(this.FontUnit_Millimeter.Checked)
		font_uint=GraphicsUnit.Millimeter;

	if(this.FontUnit_Pixel.Checked)
		font_uint=GraphicsUnit.Pixel;

	if(this.FontUnit_Dot.Checked)
		font_uint=GraphicsUnit.Point;

	if(this.FontUnit_World.Checked)
		font_uint=GraphicsUnit.World;

	//��ȡ��ǰ�Ѿ�ѡ���������
	FontStyle font_style=FontStyle.Regular;
	if(this.FontStyle_Regular.Checked)
		font_style|=FontStyle.Regular;

	if(this.FontStyle_Bold.Checked)
		font_style|=FontStyle.Bold;

	if(this.FontStyle_Italic.Checked)
		font_style|=FontStyle.Italic;

	if(this.FontStyle_Strikeout.Checked)
		font_style|=FontStyle.Strikeout;

	if(this.FontStyle_Underline.Checked)
		font_style|=FontStyle.Underline;

	//��ȡѡ�������ɫ��
	Color basecolor=Color.FromArgb((int)this.FontColor_R.Value,
		(int)this.FontColor_G.Value,(int)this.FontColor_B.Value);
	//����ѡ���ɫ�ʹ�������ı�ʱʹ�õĻ�ˢ
	SolidBrush   solidBrush=new SolidBrush(Color.FromArgb((int)this.FontColor_A.Value,basecolor));

	//����ѡ���������Ϣ��������
	FontFamily   fontFamily=new FontFamily(font_name);
    try
    {
        Font font = new Font(fontFamily,
            (int)font_size, (FontStyle)font_style, font_uint);
        //�����ı������ʽ������
        StringFormat fmt = new StringFormat();
        fmt.Alignment = StringAlignment.Center;
        fmt.LineAlignment = StringAlignment.Center;

        //������ʾ����������ı�
        graphics.DrawString("GDI+�������", font, solidBrush, textOut, fmt);
    }
    catch { }
	
}

private void FontSize_ValueChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}

	private void FontColor_A_ValueChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}

	private void FontColor_R_ValueChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}

	private void FontColor_G_ValueChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}

	private void FontColor_B_ValueChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}

	private void FontStyle_Regular_CheckedChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}

	private void FontStyle_Bold_CheckedChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}

	private void FontStyle_Italic_CheckedChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}

	private void FontStyle_Underline_CheckedChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}

	private void FontStyle_Strikeout_CheckedChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}

	private void FontUnit_Dot_CheckedChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}

	private void FontUnit_Inch_CheckedChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}

	private void FontUnit_Pixel_CheckedChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}

	private void FontUnit_Millimeter_CheckedChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}

	private void FontUnit_Display_CheckedChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}

	private void FontUnit_Document_CheckedChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}

	private void FontUnit_World_CheckedChanged(object sender, System.EventArgs e)
	{
		this.RedrawFontPreviewWindow();
	}
}
}
