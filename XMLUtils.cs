using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace CabinetInfirmier;

public class XMLUtils
{
    public static async Task ValidateXmlFileAsync(string schemaNamespace, string xsdFilePath, string xmlFilePath)
    {
        var settings = new XmlReaderSettings();
        settings.Schemas.Add(schemaNamespace, xsdFilePath);
        settings.ValidationType = ValidationType.Schema;
        Console.WriteLine("Nombre de schémas utilisés dans la validation : " + settings.Schemas.Count);
        settings.ValidationEventHandler += ValidationCallBack;
        var readItems = XmlReader.Create(xmlFilePath, settings);
        while (readItems.Read()) { }
    }

    private static void ValidationCallBack(object? sender, ValidationEventArgs e)
    {
        if (e.Severity == XmlSeverityType.Warning)
        {
            Console.WriteLine($"WARNING: {e.Message}");
        }
        else if (e.Severity == XmlSeverityType.Error)
        {
            Console.WriteLine($"ERROR: {e.Message}");
            if (sender is XmlReader reader)
            {
                Console.WriteLine($"Error at line {((IXmlLineInfo)reader).LineNumber}, position {((IXmlLineInfo)reader).LinePosition}");
            }
        }
    }
    
    public static void XslTransform (string xmlFilePath, string xsltFilePath, string htmlFilePath) {
        XPathDocument xpathDoc = new XPathDocument(xmlFilePath) ;
        XslCompiledTransform xslt = new XslCompiledTransform();
        
        XsltSettings settings = new XsltSettings(true, true);
        XmlResolver resolver = new XmlUrlResolver();
        xslt.Load(xsltFilePath, settings, resolver);
        
        XsltArgumentList argList = new XsltArgumentList();
        argList.AddParam("destinedName", "", "Orouge");
        // argList.AddParam("destinedId", "", "001");
            
        // XmlTextWriter htmlWriter = new XmlTextWriter(htmlFilePath, Encoding.UTF8);
        // xslt.Transform(xpathDoc, argList, htmlWriter);
        using (StreamWriter htmlWriter = new StreamWriter(htmlFilePath, false, Encoding.UTF8)) {
            xslt.Transform(xpathDoc, argList, htmlWriter);
        }
        // htmlWriter.Close();
    }

}