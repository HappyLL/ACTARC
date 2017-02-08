using System.Collections;
using ACTBase;
using System.Xml;
using System.Collections.Generic;

namespace ACTGame
{
	public enum EntityFootViewType {
		EntityFootViewType_None = 0,
		EntityFootViewType_Reflection = 1,//
		EntityFootViewType_Aura = 2,
	}

	public class EntityFootViewData:XmlData
	{
		public EntityFootViewType m_eType;
		ResItemData m_stResItemData;

		public EntityFootViewData()
		{
			m_eType = EntityFootViewType.EntityFootViewType_None;

			m_stResItemData = new ResItemData ();
		}

		public void Read(XmlElement xml)
		{
			XmlRead.AttrEnum (xml , "eFootViewType" , m_eType);
			XmlRead.Node (xml , "ResItemData" , m_stResItemData);
		}

		public void Write(XmlElement xml)
		{
			XmlWrite.AttrEnum (xml , "eFootViewType" , m_eType);
			XmlWrite.Node (xml , "ResItemData" , m_stResItemData);
		}
	}

	//兵的视图(包含哪些内容)
	public class EntityViewData:XmlData
	{
		public int m_iId;//视图ID
		public string m_sName;//视图名字
		public float m_fScale;//视图压缩比

		public ResItemData m_stResItemData;//默认资源类型
		public string m_sLayerName;

		public List<EntityFootViewData> m_vFootViewData;//脚底视图


		public EntityViewData()
		{
			m_stResItemData = new ResItemData ();

			m_vFootViewData = new List<EntityFootViewData> ();
		}

		public void Read(XmlElement xml)
		{
			XmlRead.Attr (xml , "iId" , m_iId);
			XmlRead.Attr (xml , "m_sName" , m_sName);
			XmlRead.Attr (xml , "fScale" , m_fScale);

			XmlRead.Node (xml , "DefaultResData" , m_stResItemData);
			XmlRead.List (xml , "EntityFootViewData" , m_vFootViewData);
		}

		public void Write(XmlElement xml)
		{
			XmlWrite.Attr (xml , "iId" , m_iId);
			XmlWrite.Attr (xml , "m_sName" , m_sName);
			XmlWrite.Attr (xml , "fScale" , m_fScale);

			XmlWrite.Node (xml , "DefaultResData" , m_stResItemData);
			XmlWrite.List (xml , "EntityFootViewData" , m_vFootViewData);
		}
	}
}
