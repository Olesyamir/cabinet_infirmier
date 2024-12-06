using System.Xml;

namespace CabinetInfirmier;

public class CabinetXMLReader {

    // public static bool isNumeroValide0(string sexe, string numero, string naissance)
    // {
    //     // Extraire les informations de naissance
    //     string[] dateNaissance = naissance.Split('-');
    //     string anneeNaissance = dateNaissance[0];
    //     string moisNaissance = dateNaissance[1].PadLeft(2, '0');  // Ajouter un zéro devant si nécessaire
    //     string jourNaissance = dateNaissance[2].PadLeft(2, '0');  // Ajouter un zéro devant si nécessaire
    //
    //     // Extraire les deux derniers chiffres de l'année de naissance
    //     string derniersChiffresAnnee = anneeNaissance.Substring(2, 2);
    //
    //     // Vérifier les conditions
    //     bool conditionSexe = (sexe == "M" && numero[0] == '1') || (sexe == "F" && numero[0] == '2');
    //     bool conditionDate = numero.Substring(1, 2) == derniersChiffresAnnee;
    //     bool conditionMois = numero.Substring(3, 2) == moisNaissance;
    //     bool conditionJour = numero.Substring(5, 2) == jourNaissance;
    //     
    //     if (conditionSexe && conditionDate && conditionMois && conditionJour)
    //     {
    //         return true;
    //     }
    //
    //     return false;
    //     
    // } // fin fonction isNumeroValide0
    //
    // public static bool isNumeroValide(string nomPatient)
    // {
    //     XmlDocument xmlDoc = new XmlDocument();
    //     xmlDoc.Load("./data/xml/cabinet.xml");
    //
    //     // Configurer le gestionnaire des espaces de noms
    //     XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
    //     namespaceManager.AddNamespace("med", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
    //
    //     // Construire l'expression XPath pour trouver le patient
    //     string myXpathExpression = "//med:cabinet/med:patients/med:patient[med:nom='" + nomPatient + "']";
    //
    //     // Récupérer le nœud correspondant au patient
    //     XmlNode patientNode = xmlDoc.SelectSingleNode(myXpathExpression, namespaceManager);
    //
    //     // Vérifier si le patient existe
    //     if (patientNode == null)
    //     {
    //         // Patient introuvable
    //         return false;
    //     }
    //
    //     // Récupérer les informations du patient
    //     XmlNode sexeNode = patientNode.SelectSingleNode("med:sexe", namespaceManager);
    //     XmlNode numeroNode = patientNode.SelectSingleNode("med:numero", namespaceManager);
    //     XmlNode naissanceNode = patientNode.SelectSingleNode("med:naissance", namespaceManager);
    //
    //     if (sexeNode == null || numeroNode == null)
    //     {
    //         // Si l'une des informations est manquante
    //         return false;
    //     }
    //
    //     string sexe = sexeNode.InnerText;
    //     string numero = numeroNode.InnerText;
    //     string naissance = naissanceNode.InnerText;
    //     
    //     return (isNumeroValide0(sexe, numero, naissance));
    //
    // }// fin de foction isNumeroValide

    static public string MaxIDInfirmier(String filename) {
        string maxID = "000";
        XmlReader reader = XmlReader.Create(filename);
        reader.MoveToContent();
        while (reader.Read()) {
            switch (reader.NodeType) {
                case XmlNodeType.Element :
                    if (reader.IsStartElement("infirmier"))
                    {
                        reader.MoveToFirstAttribute();
                        if (String.Compare(reader.Value, maxID) > 0)
                        {
                            maxID = reader.Value;
                        }
                    }
                    break;
            }
        }
        return maxID;
    } //fin MaxIdInfirmier
    
    public static void AnalyseGlobale(string filepath) {
        var settings = new XmlReaderSettings();

        using var reader = XmlReader.Create(filepath, settings);
        reader.MoveToContent();

        while (reader.Read()) {
            switch (reader.NodeType) {
                case XmlNodeType.XmlDeclaration:
                    // Instructions à exécuter quand une déclaration XML est détectée
                    Console.Write("Found XML declaration (<?xml version='1.0'?>)");
                    break;

                case XmlNodeType.Document:
                    // Instructions à exécuter au début du document
                    Console.Write("Entering the document");
                    break;

                case XmlNodeType.Comment:
                    // Instructions à exécuter quand un commentaire est trouvé
                    Console.Write("Comment = <!--{0}-->", reader.Value);
                    break;

                case XmlNodeType.Element:
                    Console.WriteLine("Starts the element {0}", reader.Name);

                    // Lire et afficher les attributs de l'élément
                    if (reader.HasAttributes)
                    {
                        Console.WriteLine("Attributes of {0}:", reader.Name);
                        while (reader.MoveToNextAttribute())
                        {
                            Console.WriteLine(" - {0} = {1}", reader.Name, reader.Value);
                        }

                        // Revenir à l'élément après avoir lu les attributs
                        reader.MoveToElement();
                    }
                    break;

                case XmlNodeType.CDATA:
                    // Instructions à exécuter quand on trouve un bloc CDATA
                    Console.Write("Found CData part: <![CDATA[{0}]]>", reader.Value);
                    break;

                case XmlNodeType.Text:
                    // Instructions à exécuter quand on trouve du texte
                    Console.WriteLine("Text node value = {0}", reader.Value);
                    break;

                case XmlNodeType.EndElement:
                    // Instructions à exécuter quand on sort d’un élément
                    Console.WriteLine("Ends the element {0}", reader.Name);
                    break;

                default:
                    // Instructions à exécuter dans les autres cas
                    Console.WriteLine("Other node of type {0} with value {1}", reader.NodeType, reader.Value);
                    break;
            }
        }
    } //fin de AnalyseGlobale

    public static void GetAllElements(string filepath, string element, string domaine) {
        var settings = new XmlReaderSettings();

        using var reader = XmlReader.Create(filepath, settings);
        reader.MoveToContent();

        bool insideDomaine = false; // Tracks if we're inside the specified parent element

        while (reader.Read()) {
            switch (reader.NodeType) {
                case XmlNodeType.Element:
                    if (reader.Name == domaine) {
                        insideDomaine = true; // Entering the desired parent element
                    }

                    if (insideDomaine && reader.Name == element) {
                        Console.Write("Found element: {0} ", reader.Name);
                        if (reader.Read() && reader.NodeType == XmlNodeType.Text) {
                            Console.WriteLine("Text: {0}", reader.Value);
                        }
                    }
                    break;

                case XmlNodeType.EndElement:
                    if (reader.Name == domaine) {
                        insideDomaine = false; // Exiting the desired parent element
                    }
                    break;
            }
        }
    } //fin fonction GetAllElemets

    public static int GetNbActes(string filepath)
    {
        var settings = new XmlReaderSettings();

        using var reader = XmlReader.Create(filepath, settings);
        reader.MoveToContent();

        List<string> liste_actes = new List<string>();

        while (reader.Read())
        {
            if (reader.Name == "acte")
            {
                reader.MoveToFirstAttribute();
                if (!liste_actes.Contains(reader.Value))
                    liste_actes.Add(reader.Value);
            }
        }

        return liste_actes.Count;
    }// fin de fonction GetNbActes
    
    
} // fin classe 