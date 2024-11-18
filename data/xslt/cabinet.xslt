<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
                 xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                 xmlns:act="http://www.univ-grenoble-alpes.fr/l3miage/actes"
                 xmlns:med="http://www.univ-grenoble-alpes.fr/l3miage/medical">

    <xsl:output method="html"/>
    
    
<!--    <xsl:param name ="destinedId" select ="001"/>-->
    <xsl:param name ="destinedId"/>
    
    <!-- Prenom d'infirmier qui a un id  destinedId -->
    <xsl:variable name="prenomInfirmier" select="med:cabinet/med:infirmiers/med:infirmier[@idI=$destinedId]/med:prenom"/>
    
    <!-- Les variables pour l'affichage de patients -->
    <xsl:variable name="nomPatient" select="med:cabinet/med:patients/med:patient/med:nom"/>
    <xsl:variable name="prenomPatient" select="med:cabinet/med:patients/med:patient/med:prenom"/>
    <xsl:variable name="adressePatient" select="med:cabinet/med:patients/med:patient/med:adresse"/>
    <xsl:variable name="idActePatient" select="med:cabinet/med:patients/med:patient/med:visite/med:acte/@idActe"/>
<!--    <xsl:variable name="SoinPatient" select="med:cabinet/med:patients/med:patient/med:visite/med:acte"/>-->
    <xsl:variable name ="actes" select ="document('../xml/actes.xml', /)/act:ngap/act:actes/act:acte[@idActe=$idActePatient]"/>
    
    <!-- Compter le nombre de patients associés à cette infirmière -->
    <xsl:variable name="nombrePatients" select="count(med:cabinet/med:patients/med:patient/med:visite[@intervenant=$destinedId])"/>
    
    <xsl:template match="/med:cabinet">
        <html>
            <head>
                <title>Page de patient 001</title>
                <script type="text/javascript" src="../js/facture.js"/>
            </head>
            <body>
                <p>Debug: destinedId is <xsl:value-of select="$destinedId"/></p>
                <p>Bonjour <xsl:value-of select="$prenomInfirmier"/>,<br/>
                    Aujourd’hui, vous avez
                    <xsl:value-of select="$nombrePatients"/> patients
                </p>
                
                <p>Voici vos patients : <br/>
                    <ul>
                        <xsl:apply-templates select="med:patients/med:patient[med:visite[@intervenant=$destinedId]]">
                            <xsl:sort select="med:patients/med:patient/med:visite/@date" data-type="text" order="ascending"/>
                        </xsl:apply-templates>
                    </ul>
                    
                </p>
            </body>
        </html>
    </xsl:template>

    <!-- Modèle pour afficher les détails de chaque patient -->
    <xsl:template match="med:patient">
        <li>
            <strong>Date de visite : </strong><xsl:value-of select="med:visite/@date"/><br/>
            <strong>Nom : </strong> <xsl:value-of select="med:nom"/><br/>
            <strong>Prénom : </strong> <xsl:value-of select="med:prenom"/><br/>
            <strong>Adresse : </strong>
            <xsl:value-of select="med:adresse/med:numero"/>
            <xsl:text> </xsl:text> <!-- pour ajouter un espace -->2
            <xsl:value-of select="med:adresse/med:rue"/>
            <xsl:text> </xsl:text>
            <xsl:value-of select="med:adresse/med:codePostal"/>
            <xsl:text> </xsl:text>
            <xsl:value-of select="med:adresse/med:ville"/><br/>
            <strong>Liste des soins à effectuer :</strong>
            <!-- Applique des templates pour les actes de chaque patient -->
            <xsl:apply-templates select="med:visite/med:acte"/>

            <button class="tablink">
                <xsl:attribute name="value">Facture</xsl:attribute>
                <xsl:attribute name ="onclick">
                    openFacture ('<xsl:value-of select ="med:prenom"/>',
                    '<xsl:value-of select ="med:nom"/>',
                    '<xsl:value-of select ="med:visite/med:acte/@idActe"/>')
                </xsl:attribute >
                Facture
            </button>


        
        
        </li>
    </xsl:template>

    <!-- Modèle pour afficher les actes d'un patient -->
    <xsl:template match="med:acte">
            <!-- Rechercher la description de l'acte dans le fichier actes.xml -->
            <xsl:value-of select="document('../xml/actes.xml')/act:ngap/act:actes/act:acte[@idActe = current()/@idActe]"/>
        <p></p>
    </xsl:template>
    
    
    
</xsl:stylesheet>