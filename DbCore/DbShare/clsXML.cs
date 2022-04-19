using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace DbCore.DbShare
{
    public class clsXML
    {
        private string pathXML;
        private XmlTextReader readerXML;
        private XmlDocument docXML;
        private XmlNamespaceManager nsMgr;
        public clsXML(String pathxml)
        {
            pathXML = pathxml;                        
        }
        public clsXML(MemoryStream stream)
        {
            this.docXML = new XmlDocument();
            this.docXML.Load(stream);
            nsMgr = new XmlNamespaceManager(docXML.NameTable);
            nsMgr.AddNamespace("sm", "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition");
        }

        public string PathXML
        {
            get { return pathXML; }
            set { pathXML = value; }
        }

        public XmlTextReader ReaderXML
        {
            get { return readerXML; }
            set { readerXML = value; }
        }
        public XmlNode gjejNodeSipasKodit(string xpath)
        {
            XmlNode node = this.docXML.SelectSingleNode(xpath, nsMgr);
            return node;
        }

        public XmlNode gjejNodeRelative(XmlNode nodeBase, string xpath)
        {
            XmlNode node = nodeBase.SelectSingleNode(xpath, nsMgr);
            return node;
        }

        public XmlNodeList gjejListeNodeRelative(XmlNode nodeBase, string xpath)
        {
            XmlNodeList nodeList = nodeBase.SelectNodes(xpath, nsMgr);
            return nodeList;
        }

        public XmlNodeList gjejListeNode(string xpath)
        {
            XmlNodeList nodeList = this.docXML.SelectNodes(xpath,nsMgr);
            return nodeList;
        }
        public void boshatisNodeText(XmlNode node)
        {
            node.InnerText = "";
        }
        public void vendosNodeText(XmlNode node, string text)
        {
            node.InnerText = text;
        }
        public void ndryshoVlereAtributiNode(XmlNode node,string kodAtributi, string newValue)
        {
            node.Attributes[kodAtributi].Value = newValue;
        }
        public void replaceNodes(XmlNode baseNode, XmlNode newNode, XmlNode oldNode)
        {
            baseNode.ReplaceChild(newNode, oldNode);            
        }
        public void ndryshoVendeNodes(XmlNode baseNode, XmlNode newNode, XmlNode oldNode)
        {
            if (!newNode.Equals(oldNode))
            {
                XmlNode tempNodeOld = oldNode.Clone();
                XmlNode tempNodeNew = newNode.Clone();
                replaceNodes(baseNode, tempNodeNew, oldNode);
                replaceNodes(baseNode, tempNodeOld, newNode);
            }
        }


        public void fshiNode(XmlNode baseNode, XmlNode node)
        {
            baseNode.RemoveChild(node);
        }
    }
}
