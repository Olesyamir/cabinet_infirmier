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
    
    public XmlNodeList GetXPath(string nsPrefix, string nsURI, string expression)
    {
        XmlNode root = doc.DocumentElement;
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
        nsmgr.AddNamespace(nsPrefix, nsURI);
        return root.SelectNodes(expression, nsmgr);
    }
    
    public String GetNSPrefix() {
        return root.Prefix;
    }

    public String GetNSURI() {
        return root.NamespaceURI;
    }
    
    public void AddInfirmier(String nom, String prenom) {
        XmlElement infirmier = doc.CreateElement(root.Prefix, "infirmier", root.NamespaceURI);
        // trouver un ID max existant et calculer un ID nouveau 
        string idInfirmier = CabinetXMLReader.MaxIDInfirmier(nomXMLDoc); // ecrire avec parseur DOM
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

    public void AddPatient(String nom, String prenom, string sexe, string naissance, string numeroSS, String? etage, String? numeroRue, String? rue, String codePostal,
        String ville)
    {
        XmlElement patient = doc.CreateElement(root.Prefix, "patient", root.NamespaceURI);
        var patientsLocation = ((XmlElement)root).GetElementsByTagName("patients").Item(0);
        patientsLocation.AppendChild(patient);
        
        XmlElement nomPatient = doc.CreateElement(root.Prefix, "nom", root.NamespaceURI);
        nomPatient.InnerText = nom;
        patient.AppendChild(nomPatient);
        
        XmlElement prenomPatient = doc.CreateElement(root.Prefix, "prenom", root.NamespaceURI);
        prenomPatient.InnerText = prenom;
        patient.AppendChild(prenomPatient);
        
        XmlElement sexePatient = doc.CreateElement(root.Prefix, "sexe", root.NamespaceURI);
        sexePatient.InnerText = sexe;
        patient.AppendChild(sexePatient);
        
        XmlElement naissancePatient = doc.CreateElement(root.Prefix, "naissance", root.NamespaceURI);
        naissancePatient.InnerText = naissance;
        patient.AppendChild(naissancePatient);

        if (CabinetXMLReader.isNumeroValide0(sexe, numeroSS, naissance))
        {
            XmlElement numeroSSPatient = doc.CreateElement(root.Prefix, "numero", root.NamespaceURI);
            numeroSSPatient.InnerText = numeroSS;
            patient.AppendChild(numeroSSPatient);
        }
        else
        {
            throw new Exception("Numero secu n'est pas valide");
        }
        
        
        XmlElement adresse = doc.CreateElement(root.Prefix, "adresse", root.NamespaceURI);
        patient.AppendChild(adresse);
        
        if (!string.IsNullOrEmpty(etage) || !string.IsNullOrEmpty(numeroRue) || !string.IsNullOrEmpty(rue))
        {
            // ajouter etage si il n'est pas nulle
            if (!string.IsNullOrEmpty(etage))
            {
                XmlElement etageElement = doc.CreateElement(root.Prefix, "etage", root.NamespaceURI);
                etageElement.InnerText = etage;
                adresse.AppendChild(etageElement);
            }

            // ajouter numero si n'est pas vide
            if (!string.IsNullOrEmpty(numeroRue))
            {
                XmlElement numeroRueElement = doc.CreateElement(root.Prefix, "numero", root.NamespaceURI);
                numeroRueElement.InnerText = numeroRue;
                adresse.AppendChild(numeroRueElement);
            }

            // ajoute rue si pas vide
            if (!string.IsNullOrEmpty(rue))
            {
                XmlElement rueElement = doc.CreateElement(root.Prefix, "rue", root.NamespaceURI);
                rueElement.InnerText = rue;
                adresse.AppendChild(rueElement);
            }
        }
        XmlElement codePostalElement = doc.CreateElement(root.Prefix, "codePostal", root.NamespaceURI);
        codePostalElement.InnerText = codePostal;
        adresse.AppendChild(codePostalElement);
        
        XmlElement villeElement = doc.CreateElement(root.Prefix, "ville", root.NamespaceURI);
        villeElement.InnerText = ville;
        adresse.AppendChild(villeElement);
        
        
        doc.Save(Console.Out);
    } // fin fonction AddPatient
    
    // Fonction pour savoir si un intervenant existe parmi les infirmiers
    public void Isintervenantininfirmier(string idIntervenant)
    {
       // Vérifier si l'intervenant existe
       // XPath pour récupérer les infirmiers avec le bon espace de noms
       // XmlNodeList nlinfirmiers = GetXPath("med","http://www.univ-grenoble-alpes.fr/l3miage/medical","med:cabinet/med:infirmiers");
       XmlNodeList nlinfirmiers = ((XmlElement)root).GetElementsByTagName("infirmier");
       bool intervenantExists = false;
       foreach (XmlElement infirmier in nlinfirmiers)
       {
           // Vérifier l'attribut idI de chaque infirmier
           Console.WriteLine(infirmier.Attributes["idI"].Value);
           if (infirmier.GetAttribute("idI") == idIntervenant)
           {
               intervenantExists = true;
               break;
           }
       }
       
       // Si l'infirmier n'existe pas, une exception est levée
       if (!intervenantExists)
       {
           throw new Exception($"Aucun infirmier trouvé avec l'ID {idIntervenant}.");
       }
    }
    
   //fonction pour rajouter une visite pour un patient en connaissance de son nom
   public void AddVisiteToPatientByName(string patientName, string dateVisite, string idIntervenant, string idActe)
   {
       // Vérifier si l'intervenant existe
       Isintervenantininfirmier(idIntervenant);
      
       // Recherche le patient par son nom
       //string patientXPath = $"{GetNSPrefix()}:patient";
       XmlNodeList nlpatients = ((XmlElement)root).GetElementsByTagName("patient");
      
       XmlElement targetPatient = null;
       foreach (XmlElement patient in nlpatients)
       {
           // Console.WriteLine(patient.OuterXml);
           XmlNode nameNode = patient.SelectSingleNode("nom", nsmgr);
           
           String nom_patient_current = patient.GetElementsByTagName("nom")[0].InnerText;
           
           // patient.GetElementsByTagName("nom")[0].InnerText
           
           if (nom_patient_current == patientName)
           {
               targetPatient = patient;
               break;
           }
       }
       if (targetPatient == null)
       {
           throw new Exception($"Aucun patient trouvé avec le nom {patientName}.");
       }
       
       // Ajouter un nouvel élément "visite" au patient
       XmlElement visite = doc.CreateElement(GetNSPrefix(), "visite", GetNSURI());
       visite.SetAttribute("date", dateVisite);
       visite.SetAttribute("intervenant", idIntervenant);
       
       // Ajouter un sous-élément "acte" à la visite
       XmlElement acte = doc.CreateElement(GetNSPrefix(), "acte", GetNSURI());
       acte.SetAttribute("idActe", idActe);
       visite.AppendChild(acte);
       
       // Ajouter la visite au patient
       targetPatient.AppendChild(visite);
       
       // Sauvegarder les modifications
       doc.Save(Console.Out); // Affiche les modifications dans la console pour vérification
       doc.Save(nomXMLDoc); // Sauvegarde dans un fichier XML si nécessaire
       Console.WriteLine($"Visite ajoutée pour le patient {patientName}.");
   } //fin de la fonction qui rajoute une visite
   
} // fin classe