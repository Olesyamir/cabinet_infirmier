using System.Xml;

namespace CabinetInfirmier;

public class CabinetDOM
{
    private XmlDocument doc;
    private XmlNode root;
    private XmlNamespaceManager nsmgr;
    private string nomXMLDoc;

    public CabinetDOM(String filename)
    {
        nomXMLDoc = filename;
        doc = new XmlDocument();
        doc.Load(filename);
        root = doc.DocumentElement;
        nsmgr = new XmlNamespaceManager(doc.NameTable);  
        nsmgr.AddNamespace(root.Prefix, root.NamespaceURI);
    }
    
    public String GetNSPrefix() {
        return root.Prefix;
    }

    public String GetNSURI() {
        return root.NamespaceURI;
    }
    
    public void AddInfirmier(String nom, String prenom) {
        XmlElement infirmier = doc.CreateElement(root.Prefix, "infirmier", root.NamespaceURI);
        string idInfirmier = CabinetXMLReader.MaxIDInfirmier("./data/xml/cabinet.xml"); // ecrire avec parseur DOM
        int number = int.Parse(idInfirmier);
        number += 1;
        idInfirmier = number.ToString("D3"); 
        
        infirmier.SetAttribute("idI", idInfirmier);
        
        var infirmiersLocation = ((XmlElement)root).GetElementsByTagName("infirmiers").Item(0);
        
        Console.WriteLine(infirmiersLocation);
        
        infirmiersLocation.AppendChild(infirmier);
        
        XmlElement nomInfirmier = doc.CreateElement(root.Prefix, "nom", root.NamespaceURI);
        nomInfirmier.InnerText = nom;
        infirmier.AppendChild(nomInfirmier);
        
        XmlElement prenomInfirmier = doc.CreateElement(root.Prefix, "prenom", root.NamespaceURI);
        prenomInfirmier.InnerText = prenom;
        infirmier.AppendChild(prenomInfirmier);
        
        XmlElement photoInfirmier = doc.CreateElement(root.Prefix, "photo", root.NamespaceURI);
        photoInfirmier.InnerText = prenom+".png";
        infirmier.AppendChild(photoInfirmier);
        
        doc.Save(Console.Out);
        // doc.Save(nomXMLDoc);
    }


}