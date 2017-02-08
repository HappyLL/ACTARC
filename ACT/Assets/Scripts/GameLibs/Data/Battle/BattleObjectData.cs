using System;
using System.Xml;
using System.Collections;
using ACTBase;
namespace ACTGame
{
	public enum BattleObjectType
	{
		BattleObjectType_None, //空
		BattleObjectType_Character, //角色
		BattleObjectType_Effect, //特效
		BattleObjectType_Trigger, //触发器
	};

	public class BattleActionData:XmlData
	{
		public double m_fMoveSpeedX;//移动速度
		public double m_fCoaxialRangeY;//Y在同轴的范围
		public double m_fCoaxialRangeX;//X在同轴的范围

		//感觉是Ai的数值属性
		public double m_fWanderRangeX;//这个范围内，概率闲荡
		public double m_fHorizonRangeX;//这个范围内，则靠近移动
		public double m_fAttackRangeX;//这个范围内，则攻击

		//技能
		public int m_iSkillCount;//技能个数最多支持5个
		public int m_iControlCharacterMaxCount;//最多控制多少个角色

		public void Read(XmlElement xml)
		{
			XmlRead.Attr (xml , "fMoveSpeedX" , m_fMoveSpeedX);
			XmlRead.Attr (xml , "fCoaxialRangeX" , m_fCoaxialRangeX);
			XmlRead.Attr (xml , "fCoaxialRangeY" , m_fCoaxialRangeY);
			XmlRead.Attr (xml , "fWanderRangeX" , m_fWanderRangeX);
			XmlRead.Attr (xml , "fHorizonRangeX" , m_fHorizonRangeX);
			XmlRead.Attr (xml , "fAttackRangeX" , m_fAttackRangeX);

			XmlRead.Attr (xml , "iSkillCount" , m_iSkillCount);
			XmlRead.Attr (xml , "iControlCharacterMaxCount" , m_iControlCharacterMaxCount);
		}

		public void Write(XmlElement xml)
		{
			XmlWrite.Attr (xml , "fMoveSpeedX" , m_fMoveSpeedX);
			XmlWrite.Attr (xml , "fCoaxialRangeX" , m_fCoaxialRangeX);
			XmlWrite.Attr (xml , "fCoaxialRangeY" , m_fCoaxialRangeY);
			XmlWrite.Attr (xml , "fWanderRangeX" , m_fWanderRangeX);
			XmlWrite.Attr (xml , "fHorizonRangeX" , m_fHorizonRangeX);
			XmlWrite.Attr (xml , "fAttackRangeX" , m_fAttackRangeX);

			XmlWrite.Attr (xml , "iSkillCount" , m_iSkillCount);
			XmlWrite.Attr (xml , "iControlCharacterMaxCount" , m_iControlCharacterMaxCount);
		}
	};

	public class BattleObjectData:XmlData
	{
		public BattleObjectType m_eObjectType;//当前数据类型

		public float m_fScale;//全局缩放大小？全局是啥
		public bool m_bCanHurt;//能否被攻击
		public BoundingBox m_bOriginBox;//原始包围盒
		public BattleActionData m_stBattleActionData;//战斗行为数据

		public BattleObjectData()
		{
			m_fScale = 1.0;
			m_bCanHurt = true;
			m_eObjectType = BattleObjectType.BattleObjectType_None;

			m_bOriginBox = new BoundingBox ();
			m_stBattleActionData = new BattleActionData ();
		}

		public void Read(XmlElement xml)
		{
			XmlRead.AttrEnum (xml , "eObjectType" , m_eObjectType);
			XmlRead.Attr (xml , "fScale" , m_fScale);
			XmlRead.Attr (xml , "bCanHurt" , m_bCanHurt);

			if (m_bCanHurt) 
			{
				XmlRead.Node (xml, "BoundingBox", m_bOriginBox);
			}

			if (m_eObjectType == BattleObjectType.BattleObjectType_Character) 
			{
				XmlRead.Node (xml, "BattleActionData", m_stBattleActionData);
			}
		}

		public void Write(XmlElement xml)
		{
			XmlWrite.AttrEnum (xml , "eObjectType" , m_eObjectType);
			XmlWrite.Attr (xml , "fScale" , m_fScale);
			XmlWrite.Attr (xml , "bCanHurt" , m_bCanHurt);

			if (m_bCanHurt) 
			{
				XmlWrite.Node (xml , "BoundingBox" , m_bOriginBox);
			}

			if (m_eObjectType == BattleObjectType.BattleObjectType_Character) 
			{
				XmlWrite.Node (xml, "BattleActionData", m_stBattleActionData);
			}
		}
	}
}
