using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

namespace ACTBase
{
    public interface XmlData
    {
        void Read(XmlElement xml);
        void Write(XmlElement xml);
    }

    //xml的读
    public static class XmlRead
    {
        //取某一个xml字段的属性(转化成枚举类型T)
        public static void AttrEnum<T>(XmlElement xml, string name, ref T value)
        {
            int temp;
            var sValue = xml.GetAttribute(name);
            if (int.TryParse(sValue, out temp))
            {
                value = (T)Enum.ToObject(typeof(T), temp);
            }
        }

        //取某一个xml字段的属性(转化bool类型)
        public static void Attr(XmlElement xml, string name, ref bool value)
        {
            bool temp;
            var sValue = xml.GetAttribute(name);
            if (bool.TryParse(sValue, out temp))
            {
                value = temp;
            }
        }

        //取某一个xml字段的属性(转化成int类型)
        public static void Attr(XmlElement xml, string name, ref int value)
        {
            int temp;
            var sValue = xml.GetAttribute(name);
            if (int.TryParse(sValue, out temp))
            {
                value = temp;
            }
        }

        //取某一个xml字段的属性(转化成double类型)
        public static void Attr(XmlElement xml, string name, ref double value)
        {
            double temp;
            var sValue = xml.GetAttribute(name);
            if (double.TryParse(sValue, out temp))
            {
                value = temp;
            }
        }

        //取某一个xml字段的属性(转化成string类型)
        public static void Attr(XmlElement xml, string name, ref string value)
        {
            var sValue = xml.GetAttribute(name);
            if (sValue != "")
                value = sValue;
        }

        public static bool HasNode(XmlElement xml, string name)
        {
            foreach (XmlNode node in xml.ChildNodes)
            {
                XmlElement ele = node as XmlElement;
                if (ele != null && name == ele.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public static void Node<T>(XmlElement xml, string name, T data) where T : class, XmlData
        {
            foreach (XmlNode node in xml.ChildNodes)
            {
                XmlElement ele = node as XmlElement;
                if (ele != null && name == ele.Name)
                {
                    data.Read(ele);
                    break;
                }
            }
        }

        public static void Node<T>(XmlElement xml, T data) where T : class, XmlData
        {
            string name = data.GetType().Name;
            foreach (XmlNode node in xml.ChildNodes)
            {
                XmlElement ele = node as XmlElement;
                if (ele != null && name == ele.Name)
                {
                    data.Read(ele);
                    break;
                }
            }
        }

        public static void Node<T>(XmlElement xml, ref T data) where T : struct, XmlData
        {
            string name = data.GetType().Name;
            foreach (XmlNode node in xml.ChildNodes)
            {
                XmlElement ele = node as XmlElement;
                if (ele != null && name == ele.Name)
                {
                    data.Read(ele);
                    break;
                }
            }
        }

        public static void List<T>(XmlElement xml, string name, List<T> datas) where T : XmlData, new()
        {
            datas.Clear();

            foreach (XmlNode node in xml.ChildNodes)
            {
                XmlElement ele = node as XmlElement;
                if (ele == null || name != ele.Name)
                    continue;

                var data = new T();
                data.Read(ele);
                datas.Add(data);
            }
        }

        //对应的element含有类的名字
        public static void List<T>(XmlElement xml, List<T> datas) where T : XmlData, new()
        {
            datas.Clear();

            string name = typeof(T).Name;

            foreach (XmlNode node in xml.ChildNodes)
            {
                XmlElement ele = node as XmlElement;
                if (ele == null || name != ele.Name)
                    continue;

                var data = new T();
                data.Read(ele);
                datas.Add(data);
            }
        }
    }

    //xml的写
    public static class XmlWrite
    {
        private static XmlDocument m_doc = new XmlDocument();

        public static void InitDoc(XmlDocument doc)
        {
            m_doc = doc;
        }

        //写某一个xml字段的属性(转化成枚举类型T)
        public static void AttrEnum<T>(XmlElement xml, string name, ref T value)
        {
            int temp = Convert.ToInt32(value);
            xml.SetAttribute(name , temp.ToString());
        }

        public static void Attr(XmlElement xml, string name, ref bool value)
        {
            xml.SetAttribute(name, value.ToString());
        }

        public static void Attr(XmlElement xml, string name, ref int value)
        {
            xml.SetAttribute(name, value.ToString());
        }

        public static void Attr(XmlElement xml, string name, ref double value)
        {
            xml.SetAttribute(name, value.ToString());
        }

        public static void Attr(XmlElement xml, string name, ref string value)
        {
            xml.SetAttribute(name, value);
        }

        public static void Node<T>(XmlElement xml, string name, T data) where T : class, XmlData
        {
            XmlElement newEle = m_doc.CreateElement(name);
            data.Write(newEle);
            xml.AppendChild(newEle);
        }

        public static void Node<T>(XmlElement xml, string name, ref T data) where T : struct, XmlData
        {
            XmlElement newEle = m_doc.CreateElement(name);
            data.Write(newEle);
            xml.AppendChild(newEle);
        }

        public static void Node<T>(XmlElement xml, T data) where T : class, XmlData
        {
            XmlElement newEle = m_doc.CreateElement(data.GetType().Name);
            data.Write(newEle);
            xml.AppendChild(newEle);
        }

        public static void Node<T>(XmlElement xml, ref T data) where T : struct, XmlData
        {
            XmlElement newEle = m_doc.CreateElement(data.GetType().Name);
            data.Write(newEle);
            xml.AppendChild(newEle);
        }

        public static void List<T>(XmlElement xml, string name, List<T> datas) where T : XmlData, new()
        {
            foreach (T data in datas)
            {
                XmlElement newEle = m_doc.CreateElement(name);
                data.Write(newEle);
                xml.AppendChild(newEle);
            }
        }

        public static void List<T>(XmlElement xml, List<T> datas) where T : XmlData, new()
        {
            foreach (T data in datas)
            {
                XmlElement newEle = m_doc.CreateElement(data.GetType().Name);
                data.Write(newEle);
                xml.AppendChild(newEle);
            }
        }

    }
}
