using System;
using System.Collections;
using System.Xml;
using ACTBase;

namespace ACTGame
{
    //技能对应的cd单元
    public class CountDownItemData : XmlData
    {
        public int m_iId;
        public string m_sName;

        public int m_iMaxValue;//cd最大值(最大帧数)
        public int m_iMinValue;//cd最小值(最小帧数)
        public int m_iInitValue;//cd初始值

        public int m_iChangeIntervalTick;//每次cd值改变的间隔帧数
        public int m_iChangeValue;//每次改变的值，可以增加或减少

        public void Read(XmlElement xml)
        {
            XmlRead.Attr(xml , "iId" , ref m_iId);
            XmlRead.Attr(xml , "sName" , ref m_sName);
            XmlRead.Attr(xml , "iMaxValue" , ref m_iMaxValue);
            XmlRead.Attr(xml , "iMinValue" , ref m_iInitValue);
            XmlRead.Attr(xml , "iChangeIntervalTick" , ref m_iChangeIntervalTick);
            XmlRead.Attr(xml , "iChangeValue" , ref m_iChangeValue);
        }

        public void Write(XmlElement xml)
        {
            XmlWrite.Attr(xml, "iId", ref m_iId);
            XmlWrite.Attr(xml, "sName", ref m_sName);
            XmlWrite.Attr(xml, "iMaxValue", ref m_iMaxValue);
            XmlWrite.Attr(xml, "iMinValue", ref m_iInitValue);
            XmlWrite.Attr(xml, "iChangeIntervalTick", ref m_iChangeIntervalTick);
            XmlWrite.Attr(xml, "iChangeValue", ref m_iChangeValue);
        }
    }
}
