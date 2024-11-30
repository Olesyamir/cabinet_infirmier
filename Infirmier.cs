using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
[XmlRoot("infirmier", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
public class Infirmier
{
    [XmlAttribute("idI")] public String _ID { init; get; }
    [XmlElement("nom")] public string _Nom { init; get; }
    [XmlElement("prenom")] public string _Prenom { init; get; }
    private string _photo;
    [XmlElement("photo")] public string _Photo { set
        {
            _photo = value;
            if (!_photo.EndsWith(".png") && !_photo.EndsWith(".jpg")) _photo += ".png";
        }
        get { return _photo; }
        
    }
    
    public override string ToString() {
        var s = String.Empty;
        s += "Infirmier "+_ID+" : \n";
        s += "\t* Nom : " + _Nom;
        s += "\n \t* Prenom : " + _Prenom;
        s += "\n \t* Photo : " + _Photo+"\n";
        return s;
    }
}