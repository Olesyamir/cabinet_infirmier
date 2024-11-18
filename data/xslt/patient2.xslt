<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                exclude-result-prefixes="">

    <!-- Définir la sortie HTML -->
    <xsl:output method="html" indent="yes" encoding="UTF-8"/>

    <!-- Template pour transformer l'ensemble du document -->
    <xsl:template match="/">
        <html>
            <head>
                <title>Informations sur le Patient</title>
                <style>
                    body {
                    font-family: Arial, sans-serif;
                    margin: 20px;
                    }
                    h1 {
                    color: #4CAF50;
                    }
                    .patient-info {
                    margin-top: 20px;
                    border: 1px solid #ddd;
                    padding: 10px;
                    background-color: #f9f9f9;
                    }
                    .patient-info table {
                    width: 100%;
                    border-collapse: collapse;
                    }
                    .patient-info th, .patient-info td {
                    border: 1px solid #ddd;
                    padding: 8px;
                    text-align: left;
                    }
                    .patient-info th {
                    background-color: #f2f2f2;
                    }
                </style>
            </head>
            <body>
                <h1>Informations sur le Patient</h1>

                <!-- Affichage des informations du patient -->
                <div class="patient-info">
                    <h2>Détails du Patient</h2>
                    <table>
                        <tr>
                            <th>Nom</th>
                            <td><xsl:value-of select="patient/nom"/></td>
                        </tr>
                        <tr>
                            <th>Prénom</th>
                            <td><xsl:value-of select="patient/prenom"/></td>
                        </tr>
                        <tr>
                            <th>Sexe</th>
                            <td><xsl:value-of select="patient/sexe"/></td>
                        </tr>
                        <tr>
                            <th>Date de Naissance</th>
                            <td><xsl:value-of select="patient/naissance"/></td>
                        </tr>
                        <tr>
                            <th>Numéro de Sécurité Sociale</th>
                            <td><xsl:value-of select="patient/numeroSS"/></td>
                        </tr>
                        <tr>
                            <th>Adresse</th>
                            <td>
                                <xsl:value-of select="patient/adresse/numero"/><br/>
                                <xsl:value-of select="patient/adresse/codePostal"/> <xsl:value-of select="patient/adresse/ville"/>
                            </td>
                        </tr>
                    </table>
                </div>

                <!-- Affichage des informations de la visite -->
                <div class="patient-info">
                    <h2>Visite du <xsl:value-of select="patient/visite/@date"/></h2>
                    <table>
                        <tr>
                            <th>Intervenant</th>
                            <td>
                                <xsl:value-of select="patient/visite/intervenant/nom"/>
                                <xsl:value-of select="patient/visite/intervenant/prenom"/>
                            </td>
                        </tr>
                        <tr>
                            <th>Acte</th>
                            <td><xsl:value-of select="patient/visite/acte"/></td>
                        </tr>
                    </table>
                </div>
            </body>
        </html>
    </xsl:template>

</xsl:stylesheet>
