using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
[XmlRoot("adresse", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
public class AdresseRO
{
    private int _etage;
    [XmlElement("etage")] public int _Etage {
        init
        {
            _etage = value;
            if (_etage < 0) _etage = int.Abs(value);
        }
        get { return _etage; }
    }

    private int _numero;
    [XmlElement("numero")] public int _Numero { 
        init
        {
            _numero = value;
            if (_numero < 0) _numero = int.Abs(value);
        }
        get { return _numero; }}
    [XmlElement("rue")] public String _Rue { set; get; }
    private int _codepostal;
    [XmlElement("codePostal")] public int _CodePostal { 
        init
        {
            _codepostal = value;
            if (_codepostal.ToString().Length != 5) _codepostal = 38000;
            else if (_codepostal < 0) _codepostal = int.Abs(value);
        }
        get { return _codepostal; }
        
    }
    [XmlElement("ville")] public String _Ville { init; get; }
    
    
    public override string ToString() {
        var s = String.Empty;
        s += "Adresse : \n";
        s += "\t* Etage : " + _Etage;
        s += "\n \t* Numero : "+_Numero; 
        s += "\n \t* Rue : " + _Rue;
        s += "\n \t* CodePostal : " + _CodePostal;
        s += "\n \t* Ville : " + _Ville;
        return s;
    }
    
}