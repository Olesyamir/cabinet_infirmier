// See https://aka.ms/new-console-template for more information

using CabinetInfirmier;

string xmlPathCabinet = "./data/xml/cabinet.xml";
string URICabinet = "http://www.univ-grenoble-alpes.fr/l3miage/medical";

// validations de schemas
// XMLUtils.ValidateXmlFileAsync ("http://www.univ-grenoble-alpes.fr/l3miage/medical", "./data/xsd/cabinet.xsd", "./data/xml/cabinet.xml");
// XMLUtils.ValidateXmlFileAsync ("http://www.univ-grenoble-alpes.fr/l3miage/medical", "./data/xsd/patient.xsd", "./data/xml/patient.xml");
// XMLUtils.ValidateXmlFileAsync ("http://www.univ-grenoble-alpes.fr/l3miage/actes", "./data/xsd/actes.xsd", "./data/xml/actes.xml");
// AppContext.SetSwitch("Switch.System.Xml.AllowDefaultResolver", true);

// execution de XSLT
// XMLUtils.XslTransform("./data/xml/cabinet.xml", "./data/xslt/cabinet.xslt", "./data/html/cabinet_TEST.html");
// XMLUtils.XslTransform("./data/xml/cabinet.xml", "./data/xslt/patient.xslt", "./data/xml/patient_TEST.xml");

// parser
// Cabinet.AnalyseGlobale("./data/xml/cabinet.xml");
// Cabinet.GetAllElements("./data/xml/cabinet.xml", "nom", "patient");
// Console.WriteLine(Cabinet.GetNbActes("./data/xml/cabinet.xml"));

// Vérification de présence de valeurs particulières avec DOM
//      verification de 3 infirmiers
// Console.WriteLine("Nb d'infirmiers  attendue : 3, nb trouvé : {0}", CabinetXMLReader.CountNode(xmlPathCabinet, "//med:cabinet/med:infirmiers/med:infirmier", URICabinet));
    
//      verification de 4 patients
// Console.WriteLine("Nb de patients attendue : 4, nb trouvé : {0}", CountNode(xmlPathCabinet, "//med:cabinet/med:patients/med:patient", URICabinet));

//      une adresse complète pour le cabinet
// Console.WriteLine("Adresse est complet : {0}",HasAdresse(xmlPathCabinet, "//med:cabinet/med:adresse", URICabinet));

//      une adresse complète pour alecole
// Console.WriteLine("Adresse est complet : {0}",HasAdresse(xmlPathCabinet, "//med:cabinet/med:patients/med:patient[med:nom='alecole']/med:adresse", URICabinet));
// 
//      numero securite sociale est valide pour Orouge
// Console.WriteLine(isNumeroValide("Orouge"));

// 7.3.3 Modification de l’arbre DOM et de l’instance XML.
// ajouter un infirmier dont l’id sera 005. Il s’appelle Némard Jean.
CabinetDOM cabinetDom = new CabinetDOM(xmlPathCabinet);
cabinetDom.AddInfirmier("Nemard", "Jean");
