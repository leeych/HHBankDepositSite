using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Common
{
    public static class XmlUtility
    {
        /// <summary>
        /// 载入.xml文件
        /// </summary>
        /// <param name="fileName">xml文件名称</param>
        public static void LoadXml(string fileName)
        {
            XmlDocument document = new XmlDocument();
            document.Load(fileName);
        }

        /// 获取parentNode下名为nodeName的节点的内容
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="nodeName"></param>
        /// <param name="nm"></param>
        /// <returns></returns>
        public static string ParseElementContent(XmlNode parentNode, string nodeName, XmlNamespaceManager nm = null)
        {
            string content = null;
            XmlNode node = (nm != null) ? parentNode.SelectSingleNode(nodeName, nm) : parentNode.SelectSingleNode(nodeName);
            if (node != null)
            {
                content = node.InnerText;
            }
            return content;
        }

        /// <summary>
        /// 添加一个包含文本的元素
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="parentNode"></param>
        /// <param name="elementName"></param>
        /// <param name="nodeConent"></param>
        public static void AddContentElement(XmlDocument xmlDocument, XmlNode parentNode, string elementName, string nodeConent)
        {
            if (nodeConent != null)
            {
                XmlElement element = xmlDocument.CreateElement(elementName);
                element.InnerText = nodeConent;
                parentNode.AppendChild(element);
            }
        }

        /// <summary>
        /// 获取节点的属性值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static string GetNodeAttribute(XmlNode node, string attributeName)
        {
            string data = null;
            if (node != null && node.Attributes != null && node.Attributes[attributeName] != null)
            {
                data = node.Attributes[attributeName].InnerText;
            }
            return data;
        }
    }
}
