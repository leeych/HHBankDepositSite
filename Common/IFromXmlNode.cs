using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Common
{
    public interface IFromXmlNode
    {
        /// <summary>
        /// 从一个xml节点构造对象
        /// </summary>
        /// <param name="xmlNode"></param>
        void FromXmlNode(XmlNode xmlNode);
    }
}
