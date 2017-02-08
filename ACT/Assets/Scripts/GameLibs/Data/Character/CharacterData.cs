using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using ACTBase;
using System.Collections.Generic;

namespace ACTGame
{
    //单个技能数据
    public class CharacterSkillCellData : XmlData
    {
        public int m_iId;//技能id
        public string m_sName;//技能名字

        public int m_iXpCost;//技能消耗的xp

        public CountDownItemData m_stCountDownItem;//每一个技能只对应一个技能cd

        public CharacterSkillCellData()
        {
            m_stCountDownItem = new CountDownItemData();
        }

        public void Read(XmlElement xml)
        {
            XmlRead.Attr(xml , "iId" , ref m_iId);
            XmlRead.Attr(xml , "sName" , ref m_sName);
   
            XmlRead.Attr(xml , "iXpCost" , ref m_iXpCost);
            XmlRead.Node(xml, "CountDownItemData" , m_stCountDownItem);
        }

        public void Write(XmlElement xml)
        {
            XmlWrite.Attr(xml, "iId", ref m_iId);
            XmlWrite.Attr(xml, "sName", ref m_sName);

            XmlWrite.Attr(xml, "iXpCost", ref m_iXpCost);
            XmlWrite.Node(xml, "CountDownItemData", m_stCountDownItem);
        }
    }

    //人物-技能组(xp状态 , 各个技能状态)
    public class CharacterSkillGroupData : XmlData
    {
        public bool m_bAble;//技能组是否有效

        public CountDownItemData m_stXpCdItemData;//代表当前人物的xp信息
        public List<CharacterSkillCellData> m_vSkillData;

        public CharacterSkillGroupData()
        {
            m_bAble = false;

            m_stXpCdItemData = new CountDownItemData();
            m_vSkillData = new List<CharacterSkillCellData>();
        }

        public void Read(XmlElement xml)
        {
            XmlRead.Attr(xml , "bAble" , ref m_bAble);
            if (!m_bAble)
                return;

            XmlRead.Node(xml, "XpCdItemData", m_stXpCdItemData);
            XmlRead.List(xml , "CharacterSkillCellData" , m_vSkillData);
        }

        public void Write(XmlElement xml)
        {
            XmlWrite.Attr(xml, "bAble", ref m_bAble);
            if (!m_bAble)
                return;

            XmlWrite.Node(xml, "XpCdItemData", m_stXpCdItemData);
            XmlWrite.List(xml, "CharacterSkillCellData", m_vSkillData);
        }
    }

    //配置characterlist.xml
    public class CharacterData : XmlData
    {
        public int m_iId;//characteid
        public string m_sName;//cha-name
        public int m_iAiId;//ai-id?

        CharacterSkillGroupData m_stSKillGroupData;

		BattleObjectData m_stBattleObjectData;

		EntityViewData m_stViewData;

        public CharacterData()
        {
            m_stSKillGroupData = new CharacterSkillGroupData();

			m_stBattleObjectData = new BattleObjectData ();

			m_stViewData = new EntityViewData ();
        }

        public void Read(XmlElement xml)
        {
            XmlRead.Attr(xml , "iId", ref m_iId);
            XmlRead.Attr(xml , "sName", ref m_sName);
            XmlRead.Attr(xml , "iAiId", ref m_iAiId);

            XmlRead.Node(xml , "CharacterSkillGroupData", m_stSKillGroupData);

			XmlRead.Node(xml , "BattleActionData" , m_stBattleObjectData);

			XmlRead.Node(xml , "EntityViewData" , m_stViewData);
        }

        public void Write(XmlElement xml)
        {
            XmlWrite.Attr(xml, "iId", ref m_iId);
            XmlWrite.Attr(xml, "sName", ref m_sName);
            XmlWrite.Attr(xml, "iAiId", ref m_iAiId);

            XmlWrite.Node(xml, "CharacterSkillGroupData", m_stSKillGroupData);

			XmlWrite.Node(xml , "EntityViewData" , m_stViewData);
        }
    }
}
