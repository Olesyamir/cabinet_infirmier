// See https://aka.ms/new-console-template for more information

using CabinetInfirmier;

Console.WriteLine("Hello, World!");
// validations de schemas
// XMLUtils.ValidateXmlFileAsync ("http://www.univ-grenoble-alpes.fr/l3miage/medical", "./data/xsd/cabinet.xsd", "./data/xml/cabinet.xml");
// XMLUtils.ValidateXmlFileAsync ("http://www.univ-grenoble-alpes.fr/l3miage/medical", "./data/xsd/patient.xsd", "./data/xml/patient.xml");
// XMLUtils.ValidateXmlFileAsync ("http://www.univ-grenoble-alpes.fr/l3miage/actes", "./data/xsd/actes.xsd", "./data/xml/actes.xml");
AppContext.SetSwitch("Switch.System.Xml.AllowDefaultResolver", true);

// execution de XSLT
// XMLUtils.XslTransform("./data/xml/cabinet.xml", "./data/xslt/cabinet.xslt", "./data/html/cabinet_TEST.html");
// XMLUtils.XslTransform("./data/xml/cabinet.xml", "./data/xslt/patient.xslt", "./data/xml/patient_TEST.xml");

// parser
// Cabinet.AnalyseGlobale("./data/xml/cabinet.xml");
// Cabinet.GetAllElements("./data/xml/cabinet.xml", "nom", "patient");