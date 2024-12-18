<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ns="http://www.univ-grenoble-alpes.fr/l3miage/medical"
                xmlns:act="http://www.univ-grenoble-alpes.fr/l3miage/actes"
                exclude-result-prefixes="ns act">
    
    <xsl:output method="xml" indent="yes" encoding="UTF-8"/>
    
    <!-- Paramètre pour le nom du patient -->
    <!--    appliquer ce destinedName when "run patient.xslt"-->
    <xsl:param name="destinedName" select="'Orouge'"/>
<!--    <xsl:param name="destinedName"/>-->

    <xsl:key name="acteById" match="act:acte" use="@idActe"/>


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
<!--        <xsl:for-each select="$visits">-->
<!--            <visite date="{@date}">-->
<!--                <intervenant>-->
<!--                    <xsl:variable name="infirmier"-->
<!--                                  select="//ns:infirmier[@idI = current()/@intervenant]" />-->
<!--                    <nom><xsl:value-of select="$infirmier/ns:nom"/></nom>-->
<!--                    <prenom><xsl:value-of select="$infirmier/ns:prenom"/></prenom>-->
<!--                </intervenant>-->
<!--                <acte>-->
<!--                    <xsl:variable name="externalDoc" select="document('../xml/actes.xml')"/>-->

<!--                    &lt;!&ndash; Поиск элемента в загруженном документе &ndash;&gt;-->
<!--                    <xsl:variable name="acte"-->
<!--                                  select="$externalDoc//act:acte[@idActe = current()/ns:acte/@idActe]"/>-->

<!--                    <xsl:choose>-->
<!--                        <xsl:when test="$acte">-->
<!--                            <description>-->
<!--                                <xsl:value-of select="$acte"/>-->
<!--                            </description>-->
<!--                        </xsl:when>-->
<!--                        <xsl:otherwise>-->
<!--                            <description>Aucun acte trouvé pour cet idActe.</description>-->
<!--                        </xsl:otherwise>-->
<!--                    </xsl:choose>-->
<!--                </acte>-->
<!--            </visite>-->
<!--        </xsl:for-each>-->
        <xsl:apply-templates select="$visits"/>

    </xsl:template>
    
    

    <!-- Template pour chaque <visite> -->
    <xsl:template match="ns:visite">
        <visite date="{@date}">
            <intervenant>
                <!-- Trouver info de l'intervenant -->
                <xsl:variable name="infirmier"
                              select="//ns:infirmier[@idI = @intervenant]" />
                <nom><xsl:value-of select="$infirmier/ns:nom"/></nom>
                <prenom><xsl:value-of select="$infirmier/ns:prenom"/></prenom>
            </intervenant>
            <acte>
                <!-- Chargement de doc ACTES.XML -->
                <xsl:variable name="externalDoc" select="document('../xml/actes.xml')"/>

                <!-- Recherche de l'element dans doc  -->
                <xsl:variable name="acte"
                              select="$externalDoc//act:acte[@idActe = current()/ns:acte/@idActe]"/>

                <xsl:choose>
                    <xsl:when test="$acte">
                        <description>
                            <xsl:value-of select="$acte"/>
                        </description>
                    </xsl:when>
                    <xsl:otherwise>
                        <description>Aucun acte trouvé pour cet idActe.</description>
                    </xsl:otherwise>
                </xsl:choose>
            </acte>
        </visite>
    </xsl:template>
    
</xsl:stylesheet>
