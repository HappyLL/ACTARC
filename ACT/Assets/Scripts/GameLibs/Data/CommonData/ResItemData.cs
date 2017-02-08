using System.Collections;
using ACTBase;
using System.Xml;


namespace ACTGame
{
	public enum ResItemType
	{
		ResItemType_None,
		ResItemType_Spine, //spine资源
		ResItemType_3DAnimation, //3d动画
		ResItemType_Sprite, //图片
	}

	public class ResItemData:XmlData
	{
		public ResItemType m_eResItemType;
		public string m_sResPathName;
		public string m_sClassName;

		public float m_fAlpha;
		public float m_fPositionX;
		public float m_fPositionY;

		public ResItemData()
		{
			m_fAlpha = 1;

			m_fPositionX = 0;
			m_fPositionY = 0;
		}

		public void Read(XmlElement xml)
		{
			XmlRead.Attr (xml , "eResItemType" , m_eResItemType);
			XmlRead.Attr (xml , "sResPathName" , m_sResPathName);
			XmlRead.Attr (xml , "sClassName" , m_sClassName);

			XmlRead.Attr (xml , "fAlpha" , m_fAlpha);
			XmlRead.Attr (xml , "fPositionX" , m_fPositionX);
			XmlRead.Attr (xml , "fPositionY" , m_fPositionY);
		}

		public void Write(XmlElement xml)
		{
			XmlWrite.Attr (xml , "eResItemType" , m_eResItemType);
			XmlWrite.Attr (xml , "sResPathName" , m_sResPathName);
			XmlWrite.Attr (xml , "sClassName" , m_sClassName);

			XmlWrite.Attr (xml , "fAlpha" , m_fAlpha);
			XmlWrite.Attr (xml , "fPositionX" , m_fPositionX);
			XmlWrite.Attr (xml , "fPositionY" , m_fPositionY);
		}
	}
}
