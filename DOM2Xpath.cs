// using System.Xml;
//
// namespace CabinetInfirmier;
//
// public class DOM2Xpath
// {
//     private XmlDocument doc;
//
//     public DOM2Xpath(string filename)
//     {
//         doc = new XmlDocument();
//         doc.Load(filename);
//     }
//
//     public XmlNodeList GetXPath(string nsPrefix, string nsURI, string expression)
//     {
//         XmlNode root = doc.DocumentElement;
//         XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
//         nsmgr.AddNamespace(nsPrefix, nsURI);
//         return root.SelectNodes(expression, nsmgr);
//     }
// }
