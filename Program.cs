// See https://aka.ms/new-console-template for more information

using CabinetInfirmier;

Console.WriteLine("Hello, World!");
// validations de schemas
// XMLUtils.ValidateXmlFileAsync ("http://www.univ-grenoble-alpes.fr/l3miage/medical", "./data/xsd/cabinet.xsd", "./data/xml/cabinet.xml");
// XMLUtils.ValidateXmlFileAsync ("http://www.univ-grenoble-alpes.fr/l3miage/medical", "./data/xsd/patient.xsd", "./data/xml/patient.xml");
// XMLUtils.ValidateXmlFileAsync ("http://www.univ-grenoble-alpes.fr/l3miage/actes", "./data/xsd/actes.xsd", "./data/xml/actes.xml");
AppContext.SetSwitch("Switch.System.Xml.AllowDefaultResolver", true);

XMLUtils.XslTransform("./data/xml/cabinet.xml", "./data/xslt/cabinet.xslt", "./data/html/cabinet_TEST.html");
XMLUtils.XslTransform("./data/xml/patient.xml", "./data/xslt/patient2.xslt", "./data/html/patient_TEST.html");