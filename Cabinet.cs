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
    
    
    
} // fin de la classe 