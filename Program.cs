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

bool isNumeroValide(string nomPatient)
{
    XmlDocument xmlDoc = new XmlDocument();
    xmlDoc.Load("./data/xml/cabinet.xml");

    // Configurer le gestionnaire des espaces de noms
    XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
    namespaceManager.AddNamespace("med", "http://www.univ-grenoble-alpes.fr/l3miage/medical");

    // Construire l'expression XPath pour trouver le patient
    string myXpathExpression = "//med:cabinet/med:patients/med:patient[med:nom='" + nomPatient + "']";

    // Récupérer le nœud correspondant au patient
    XmlNode patientNode = xmlDoc.SelectSingleNode(myXpathExpression, namespaceManager);

    // Vérifier si le patient existe
    if (patientNode == null)
    {
        // Patient introuvable
        return false;
    }

    // Récupérer les informations du patient
    XmlNode sexeNode = patientNode.SelectSingleNode("med:sexe", namespaceManager);
    XmlNode numeroNode = patientNode.SelectSingleNode("med:numero", namespaceManager);
    XmlNode naissanceNode = patientNode.SelectSingleNode("med:naissance", namespaceManager);

    if (sexeNode == null || numeroNode == null)
    {
        // Si l'une des informations est manquante
        return false;
    }

    string sexe = sexeNode.InnerText;
    string numero = numeroNode.InnerText;
    string naissance = naissanceNode.InnerText;

    // Extraire les informations de naissance
    string[] dateNaissance = naissance.Split('-');
    string anneeNaissance = dateNaissance[0];
    string moisNaissance = dateNaissance[1].PadLeft(2, '0');  // Ajouter un zéro devant si nécessaire
    string jourNaissance = dateNaissance[2].PadLeft(2, '0');  // Ajouter un zéro devant si nécessaire

    // Extraire les deux derniers chiffres de l'année de naissance
    string derniersChiffresAnnee = anneeNaissance.Substring(2, 2);

    // Vérifier les conditions
    bool conditionSexe = (sexe == "M" && numero[0] == '1') || (sexe == "F" && numero[0] == '2');
    bool conditionDate = numero.Substring(1, 2) == derniersChiffresAnnee;
    bool conditionMois = numero.Substring(3, 2) == moisNaissance;
    bool conditionJour = numero.Substring(5, 2) == jourNaissance;

    // Si toutes les conditions sont remplies, retourner true
    if (conditionSexe && conditionDate && conditionMois && conditionJour)
    {
        return true;
    }

    return false;

}// fin de foction isNumeroValide



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
// Console.WriteLine("Nb d'infirmiers  attendue : 3, nb trouvé : {0}", CountNode(xmlPathCabinet, "//med:cabinet/med:infirmiers/med:infirmier", URICabinet));
    
//      verification de 4 patients
// Console.WriteLine("Nb de patients attendue : 4, nb trouvé : {0}", CountNode(xmlPathCabinet, "//med:cabinet/med:patients/med:patient", URICabinet));

//      une adresse complète pour le cabinet
// Console.WriteLine("Adresse est complet : {0}",HasAdresse(xmlPathCabinet, "//med:cabinet/med:adresse", URICabinet));

//      une adresse complète pour alecole
// Console.WriteLine("Adresse est complet : {0}",HasAdresse(xmlPathCabinet, "//med:cabinet/med:patients/med:patient[med:nom='alecole']/med:adresse", URICabinet));
// 
//      numero securite sociale est valide pour Orouge
// Console.WriteLine(isNumeroValide("Orouge"));
