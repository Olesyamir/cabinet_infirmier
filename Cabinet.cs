using System.Xml;
using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
[XmlRoot("cabinet", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
public class Cabinet
{
    
    [XmlElement("infirmiers")] public Infirmiers _Infirmiers { set; get; }
    [XmlElement("adresse")] public Adresse _AdresseCabinet { set; get; }
    [XmlElement("patients")] public Patients _Patients { set; get; }
    [XmlElement("nom")] public string _NomCabinet { set; get; }

    public class Patients
    {
        [XmlElement("patient")]
        public List<Patient> _ListePatients { get; set; } = new List<Patient>();
    }

    public class Patient
    {
        [XmlElement("nom")] public string _Nom { init; get; }
        [XmlElement("prenom")] public string _Prenom { init; get; }
        [XmlElement("sexe")] public string _Sexe { init; get; }
        [XmlElement("naissance")] public string _Naissance { init; get; }
        [XmlElement("numero")] public string _NumeroSecu { init; get; }
        [XmlElement("visite")] public Visite _Visite { set; get; }
        [XmlElement("adresse")] public Adresse _AdressePatient { set; get; }

        public class Visite
        {
            [XmlAttribute("date")] public DateTime _DateVisite { init; get; }
            [XmlAttribute("intervenant")] public string _Intervenant { set; get; }
            
            [XmlElement("acte")] public Acte _Acte { init; get; }

            public class Acte
            {
                [XmlAttribute("idActe")] public string _IdActe { init; get; }

                public override string ToString()
                {
                    var s = String.Empty;
                    s += "\n \t \t* Acte : "+_IdActe;
                    return s;
                }
            }

            public override string ToString()
            {
                var s = String.Empty;
                s += "\n \t* Visite : \n \t \t* Date: " + _DateVisite.ToString("dd/MM/yyyy");
                s+= " \n \t \t* Intervenant: " + _Intervenant;
                s+= _Acte;
                return s;
            }
        }

        public override string ToString()
        {
            var s = String.Empty;
            s += "\nPatient : ";
            s += "\n \t* Nom : " + _Nom;
            s += "\n \t* Prenom : " + _Prenom;
            s += "\n \t* Sexe : " + _Sexe;
            s += "\n \t* Naissance : " + _Naissance;
            s += "\n \t* Numero : " + _NumeroSecu;
            s += _Visite;
            s += "\n \t* Adresse : " + _AdressePatient.ToString();
            return s;
        }
    }
    
    public override string ToString() {
        var s = String.Empty;
        s += _Infirmiers.ToString();
        s += _AdresseCabinet.ToString();
        foreach (var pat in _Patients._ListePatients)
        {
            s += pat.ToString();
        }
        s += "\n Nom du cabinet : "+_NomCabinet;
        return s;
    }
    
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
    
    
    
} // fin de la classe 