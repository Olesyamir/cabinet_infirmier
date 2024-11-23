// See https://aka.ms/new-console-template for more information

using System.Xml;
using CabinetInfirmier;

int CountNode(string xmlFilePath, string myXpathExpression, string nsURI)
{
    DOM2Xpath cabinetDOM = new DOM2Xpath(xmlFilePath);
    XmlNodeList nlElementsDOM = cabinetDOM.GetXPath ("med", nsURI, myXpathExpression);
    return nlElementsDOM.Count;
} // fin de foction CountNode

bool HasAdresse(string xmlFilePath, string myXpathExpression, string nsURI)
{
    DOM2Xpath cabinetDOM = new DOM2Xpath(xmlFilePath);
    XmlNodeList nlAdresseDOM = cabinetDOM.GetXPath ("med", nsURI, myXpathExpression );
    
    // 4 est le nb d'elements obligatoires dans adresse : 1) numero, 2) rue, 3) codePostal, 4) ville
    // etage est optionelle
    
    Console.WriteLine (nlAdresseDOM[0].ChildNodes.Count);
    return nlAdresseDOM[0].ChildNodes.Count > 3;
}// fin de foction HasAdresse

Boolean isNumeroValide(string nomPatient)
{
    DOM2Xpath patientDOM = new DOM2Xpath("./data/xml/cabinet.xml");
    string myXpathExpression = "//med:cabinet/med:patients/med:patient[med:nom='" + nomPatient + "']/med:numero";
    XmlNodeList nlNumeroDOM = patientDOM.GetXPath ("med", "http://www.univ-grenoble-alpes.fr/l3miage/medical", myXpathExpression);
    
    string numero = nlNumeroDOM[0].InnerText;
    // logique
    return false;

}// fin de foction isNumeroValide



string xmlPathCabinet = "./data/xml/cabinet.xml";
string URICabinet = "http://www.univ-grenoble-alpes.fr/l3miage/medical";

// validations de schemas
// XMLUtils.ValidateXmlFileAsync ("http://www.univ-grenoble-alpes.fr/l3miage/medical", "./data/xsd/cabinet.xsd", "./data/xml/cabinet.xml");
// XMLUtils.ValidateXmlFileAsync ("http://www.univ-grenoble-alpes.fr/l3miage/medical", "./data/xsd/patient.xsd", "./data/xml/patient.xml");
// XMLUtils.ValidateXmlFileAsync ("http://www.univ-grenoble-alpes.fr/l3miage/actes", "./data/xsd/actes.xsd", "./data/xml/actes.xml");
AppContext.SetSwitch("Switch.System.Xml.AllowDefaultResolver", true);

// execution de XSLT
// XMLUtils.XslTransform("./data/xml/cabinet.xml", "./data/xslt/cabinet.xslt", "./data/html/cabinet_TEST.html");
// XMLUtils.XslTransform("./data/xml/cabinet.xml", "./data/xslt/patient.xslt", "./data/xml/patient_TEST.xml");

// parser
// Cabinet.AnalyseGlobale("./data/xml/cabinet.xml");
// Cabinet.GetAllElements("./data/xml/cabinet.xml", "nom", "patient");
// Console.WriteLine(Cabinet.GetNbActes("./data/xml/cabinet.xml"));

// Vérification de présence de valeurs particulières avec DOM
//      verification de 3 infirmiers
// Console.WriteLine("Nb d'infirmiers  attendue : 3, nb trouvé : {0}", CountNode(xmlPathCabinet, "//med:cabinet/med:infirmiers/med:infirmier", URICabinet));
    
//      verification de 4 patients
// Console.WriteLine("Nb de patients attendue : 4, nb trouvé : {0}", CountNode(xmlPathCabinet, "//med:cabinet/med:patients/med:patient", URICabinet));

//      une adresse complète pour le cabinet
// Console.WriteLine("Adresse est complet : {0}",HasAdresse(xmlPathCabinet, "//med:cabinet/med:adresse", URICabinet));

//      une adresse complète pour alecole
// Console.WriteLine("Adresse est complet : {0}",HasAdresse(xmlPathCabinet, "//med:cabinet/med:patients/med:patient[med:nom='alecole']/med:adresse", URICabinet));
// 

// isNumeroValide("alecole");
