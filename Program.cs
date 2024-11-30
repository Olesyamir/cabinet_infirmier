// See https://aka.ms/new-console-template for more information

using System.Xml.Serialization;
using CabinetInfirmier;

string xmlPathCabinet = "./data/xml/cabinet.xml";
string URICabinet = "http://www.univ-grenoble-alpes.fr/l3miage/medical";
string xmlPathAdresse = "./data/xml/adresse.xml";
string xmlPathInfirmier = "./data/xml/infirmier.xml";
string xmlPathInfirmiers = "./data/xml/infirmiers.xml";

// validations de schemas
// XMLUtils.ValidateXmlFileAsync ("http://www.univ-grenoble-alpes.fr/l3miage/medical", "./data/xsd/cabinet.xsd", "./data/xml/cabinet.xml");
// XMLUtils.ValidateXmlFileAsync ("http://www.univ-grenoble-alpes.fr/l3miage/medical", "./data/xsd/patient.xsd", "./data/xml/patient.xml");
XMLUtils.ValidateXmlFileAsync ("http://www.univ-grenoble-alpes.fr/l3miage/actes", "./data/xsd/actes.xsd", "./data/xml/actes.xml");
AppContext.SetSwitch("Switch.System.Xml.AllowDefaultResolver", true);

// execution de XSLT
// XMLUtils.XslTransform(xmlPathCabinet, "./data/xslt/cabinet.xslt", "./data/html/cabinet_TEST.html");
// XMLUtils.XslTransform(xmlPathCabinet, "./data/xslt/patient.xslt", "./data/xml/patient_TEST.xml");

// parser
Cabinet.AnalyseGlobale(xmlPathCabinet);
Cabinet.GetAllElements(xmlPathCabinet, "nom", "patient");
Console.WriteLine(Cabinet.GetNbActes(xmlPathCabinet));

// Vérification de présence de valeurs particulières avec DOM
//      verification de 3 infirmiers
Console.WriteLine("Nb d'infirmiers  attendue : 3, nb trouvé : {0}", CabinetXMLReader.CountNode(xmlPathCabinet, "//med:cabinet/med:infirmiers/med:infirmier", URICabinet));
    
//      verification de 4 patients
Console.WriteLine("Nb de patients attendue : 4, nb trouvé : {0}", CabinetXMLReader.CountNode(xmlPathCabinet, "//med:cabinet/med:patients/med:patient", URICabinet));

//      une adresse complète pour le cabinet
Console.WriteLine("Adresse est complet : {0}", CabinetXMLReader.HasAdresse(xmlPathCabinet, "//med:cabinet/med:adresse", URICabinet));

//      une adresse complète pour alecole
Console.WriteLine("Adresse est complet : {0}", CabinetXMLReader.HasAdresse(xmlPathCabinet, "//med:cabinet/med:patients/med:patient[med:nom='alecole']/med:adresse", URICabinet));
// 
//      numero securite sociale est valide pour Orouge
Console.WriteLine(CabinetXMLReader.isNumeroValide("Orouge"));

// 7.3.3 Modification de l’arbre DOM et de l’instance XML.
// ajouter un infirmier dont l’id sera 005. Il s’appelle Némard Jean.
CabinetDOM cabinetDom = new CabinetDOM(xmlPathCabinet);
cabinetDom.AddInfirmier("Nemard", "Jean");
cabinetDom.AddPatient("Niskotch", "Nicole", "F", "1999-03-01", "299030105545853", "","6", "Universite", "38400", "Grenoble");

// 7.4 SERIALISATION
// 7.4.1 Adresse , Infirmier

Adresse a;
AdresseRO a2;
Infirmiers i;
Cabinet c;

// using (TextReader reader = new StreamReader(xmlPathAdresse)) {
//     var xmlA = new XmlSerializer(typeof(Adresse));
//     a = (Adresse)xmlA.Deserialize(reader);
//     
// }
// using (TextReader reader = new StreamReader(xmlPathAdresse)) {
//     var xmlARO = new XmlSerializer(typeof(AdresseRO));
//     a2 = (AdresseRO)xmlARO.Deserialize(reader);
// }
//
// using (TextReader reader = new StreamReader(xmlPathInfirmier)) {
//     var xmlI = new XmlSerializer(typeof(Infirmier));
//     i = (Infirmier)xmlI.Deserialize(reader);
// }

// using (TextReader reader = new StreamReader(xmlPathInfirmiers)) {
//     var xmlI = new XmlSerializer(typeof(Infirmiers));
//     i = (Infirmiers)xmlI.Deserialize(reader);
// }
using (TextReader reader = new StreamReader(xmlPathCabinet)) {
    var xmlCb = new XmlSerializer(typeof(Cabinet));
    c = (Cabinet)xmlCb.Deserialize(reader);
}

Console.WriteLine(c);

