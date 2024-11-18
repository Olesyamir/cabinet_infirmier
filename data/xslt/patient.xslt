<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ns="http://www.univ-grenoble-alpes.fr/l3miage/medical"
                xmlns:act='http://www.univ-grenoble-alpes.fr/l3miage/actes'
                exclude-result-prefixes="ns act">
    
    <xsl:output method="xml" indent="yes" encoding="UTF-8"/>
    
    <!-- Paramètre pour le nom du patient -->
    <xsl:param name="destinedName"/>

    <!-- Clé pour rechercher un acte en fonction de l'idActe dans ngap.xml -->
    <xsl:key name="acteById" match="act:acte" use="@idActe" />
<!--    <xsl:key name="acteById" match="act:ngap/act:actes/act:acte" use="@idActe"/>-->

    <!-- Template pour le patient -->
    <xsl:template match="/">
        <patient>
            <xsl:call-template name="patient-info">
                <xsl:with-param name="name" select="$destinedName"/>
            </xsl:call-template>
        </patient>
    </xsl:template>

    <!-- Template pour extraire les informations du patient -->
    <xsl:template name="patient-info">
        <xsl:param name="name"/>
        <xsl:choose>
            <xsl:when test="//ns:patient[ns:nom = $name]">
                <xsl:variable name="currentPatient" select="//ns:patient[ns:nom = $name]"/>

                <nom><xsl:value-of select="$currentPatient/ns:nom"/></nom>
                <prenom><xsl:value-of select="$currentPatient/ns:prenom"/></prenom>
                <sexe><xsl:value-of select="$currentPatient/ns:sexe"/></sexe>
                <naissance><xsl:value-of select="$currentPatient/ns:naissance"/></naissance>
                <numeroSS><xsl:value-of select="$currentPatient/ns:numero"/></numeroSS>

                <adresse>
                    <numero>
                        <xsl:value-of select="$currentPatient/ns:adresse/ns:numero"/>
                    </numero>
                    <codePostal>
                        <xsl:value-of select="$currentPatient/ns:adresse/ns:codePostal"/>
                    </codePostal> 
                    <ville>
                        <xsl:value-of select="$currentPatient/ns:adresse/ns:ville"/>
                    </ville>
                </adresse>

                <!-- Appel de la template pour les visites -->
                <xsl:call-template name="visite-info">
                    <xsl:with-param name="visits" select="$currentPatient/ns:visite"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:text>Aucun patient trouvé.</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <!-- Template pour extraire les informations des visites -->
    <xsl:template name="visite-info">
        <xsl:param name="visits"/>
        <xsl:for-each select="$visits">
            <visite date="{@date}">
                <intervenant>
                    <xsl:variable name="infirmier"
                                  select="//ns:infirmier[@idI = current()/@intervenant]" />
                    <nom><xsl:value-of select="$infirmier/ns:nom"/></nom>
                    <prenom><xsl:value-of select="$infirmier/ns:prenom"/></prenom>
                </intervenant>
<!--                <acte>-->
<!--                    <xsl:variable name="acte"-->
<!--                                  select="//act:ngap/act:actes/act:acte[@idActe = current()/@idActe]" />-->
<!--                    <xsl:value-of select="$acte/text()"/>-->
<!--                </acte>-->
                <acte>
                    <!-- Utilise la clé pour récupérer directement le texte de l'acte correspondant à l'idActe -->
                    <xsl:variable name="acteDescription"
                                  select="key('acteById', current()/ns:acte/@idActe)/text()" />

                    <xsl:choose>
                        <xsl:when test="$acteDescription">
                            <!-- Affiche la description de l'acte -->
                            <xsl:value-of select="$acteDescription" />
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:text>Aucun acte trouvé pour cet idActe.</xsl:text>
                        </xsl:otherwise>
                    </xsl:choose>
            </acte>
            </visite>
        </xsl:for-each>
    </xsl:template>
</xsl:stylesheet>
