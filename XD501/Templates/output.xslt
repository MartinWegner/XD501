<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="xml" indent="yes" />
  <xsl:param name="UserName"></xsl:param>
  <xsl:param name="MachineName"></xsl:param>
  <xsl:param name="Now"></xsl:param>
  <xsl:param name="ProzessID"></xsl:param>
  <xsl:param name="SendendesSystem.InstanzID"></xsl:param>
  <xsl:param name="SendendesSystem.Produktname"></xsl:param>
  <xsl:param name="SendendesSystem.Version"></xsl:param>
  <xsl:param name="Institution.Name"></xsl:param>
  <xsl:param name="Institution.Kurzbezeichnung"></xsl:param>
  <xsl:param name="Organisationseinheit.Name"></xsl:param>
  <xsl:param name="Name.Anrede"></xsl:param>
  <xsl:param name="Name.Titel"></xsl:param>
  <xsl:param name="Name.Familienname"></xsl:param>
  <xsl:param name="Name.Vorname"></xsl:param>
  <xsl:param name="Zustaendigkeit.Name"></xsl:param>
  <xsl:param name="Anschrift.StaatCode"></xsl:param>
  <xsl:param name="Anschrift.StaatName"></xsl:param>
  <xsl:param name="Anschrift.Strasse"></xsl:param>
  <xsl:param name="Anschrift.Hausnummer"></xsl:param>
  <xsl:param name="Anschrift.Postfach"></xsl:param>
  <xsl:param name="Anschrift.Postleitzahl"></xsl:param>
  <xsl:param name="Anschrift.Ort"></xsl:param>
  <xsl:param name="Anschrift.Zusatz"></xsl:param>
  <xsl:param name="Kommunikation1.KanalCode"></xsl:param>
  <xsl:param name="Kommunikation1.KanalName"></xsl:param>
  <xsl:param name="Kommunikation1.Kennung"></xsl:param>
  <xsl:param name="Kommunikation1.Zusatz"></xsl:param>
  <xsl:param name="Kommunikation2.KanalCode"></xsl:param>
  <xsl:param name="Kommunikation2.KanalName"></xsl:param>
  <xsl:param name="Kommunikation2.Kennung"></xsl:param>
  <xsl:param name="Kommunikation2.Zusatz"></xsl:param>


  <xsl:template match="/">
    <xdomea:Aussonderung.Bewertungsverzeichnis.0502 xmlns:xdomea="urn:xoev-de:xdomea:schema:2.3.0">
      <xdomea:Kopf>
        <xdomea:ProzessID xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
          <xsl:value-of select="$ProzessID"/>
        </xdomea:ProzessID>
        <xdomea:Nachrichtentyp>
          <code>0502</code>
        </xdomea:Nachrichtentyp>
        <xdomea:Erstellungszeitpunkt xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
          <xsl:value-of select="$Now"/>
        </xdomea:Erstellungszeitpunkt>
        <xdomea:Absender>
          <xdomea:Behoerdenkennung xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
            <xdomea:Kennung>
              <code/>
            </xdomea:Kennung>
            <xdomea:Praefix>
              <code/>
            </xdomea:Praefix>
          </xdomea:Behoerdenkennung>
          <xdomea:Institution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
            <xdomea:Name>
              <xsl:value-of select="$Institution.Name"/>
            </xdomea:Name>
            <xdomea:Kurzbezeichnung>
              <xsl:value-of select="$Institution.Kurzbezeichnung"/>
            </xdomea:Kurzbezeichnung>
          </xdomea:Institution>
          <xdomea:Organisationseinheit xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
            <xdomea:Name>
              <xsl:value-of select="$Organisationseinheit.Name"/>
            </xdomea:Name>
          </xdomea:Organisationseinheit>
          <xdomea:Name xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
            <xdomea:Anrede>
              <xsl:value-of select="$Name.Anrede"/>
            </xdomea:Anrede>
            <xdomea:Titel>
              <xsl:value-of select="$Name.Titel"/>
            </xdomea:Titel>
            <xdomea:Familienname>
              <xdomea:Name>
                <xsl:value-of select="$Name.Familienname"/>
              </xdomea:Name>
            </xdomea:Familienname>
            <xdomea:Vorname>
              <xdomea:Name>
                <xsl:value-of select="$Name.Vorname"/>
              </xdomea:Name>
            </xdomea:Vorname>
          </xdomea:Name>
          <xdomea:Zustaendigkeit xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
            <xsl:value-of select="$Zustaendigkeit.Name"/>
          </xdomea:Zustaendigkeit>
          <xdomea:Anschrift xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
            <xdomea:Staat>
              <xdomea:Staat listURI="DIN EN ISO 3166-1" listVersionID="">
                <code>
                  <xsl:value-of select="$Anschrift.StaatCode"/>
                </code>
                <name>
                  <xsl:value-of select="$Anschrift.StaatName"/>
                </name>
              </xdomea:Staat>
            </xdomea:Staat>
            <xdomea:Strasse>
              <xsl:value-of select="$Anschrift.Strasse"/>
            </xdomea:Strasse>
            <xdomea:Hausnummer>
              <xsl:value-of select="$Anschrift.Hausnummer"/>
            </xdomea:Hausnummer>
            <xdomea:Postfach>
              <xsl:value-of select="$Anschrift.Postfach"/>
            </xdomea:Postfach>
            <xdomea:Postleitzahl>
              <xsl:value-of select="$Anschrift.Postleitzahl"/>
            </xdomea:Postleitzahl>
            <xdomea:Ort>
              <xsl:value-of select="$Anschrift.Ort"/>
            </xdomea:Ort>
            <xdomea:Zusatz>
              <xsl:value-of select="$Anschrift.Zusatz"/>
            </xdomea:Zusatz>
            <xdomea:Typ>
              <code>001</code>
            </xdomea:Typ>
          </xdomea:Anschrift>
          <xdomea:Kommunikation xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
            <xdomea:IstDienstlich>true</xdomea:IstDienstlich>
            <xdomea:Kanal listURI="urn:xoev-de:xdomea:codeliste:kommunikationsart"
                      listVersionID="1.0">
              <code>
                <xsl:value-of select="$Kommunikation1.KanalCode"/>
              </code>
              <name>
                <xsl:value-of select="$Kommunikation1.KanalName"/>
              </name>
            </xdomea:Kanal>
            <xdomea:Kennung>
              <xsl:value-of select="$Kommunikation1.Kennung"/>
            </xdomea:Kennung>
            <xdomea:Zusatz>
              <xsl:value-of select="$Kommunikation1.Zusatz"/>
            </xdomea:Zusatz>
          </xdomea:Kommunikation>
          <xdomea:Kommunikation xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
            <xdomea:IstDienstlich>true</xdomea:IstDienstlich>
            <xdomea:Kanal listURI="urn:xoev-de:xdomea:codeliste:kommunikationsart"
                      listVersionID="1.0">
              <code>
                <xsl:value-of select="$Kommunikation2.KanalCode"/>
              </code>
              <name>
                <xsl:value-of select="$Kommunikation2.KanalName"/>
              </name>
            </xdomea:Kanal>
            <xdomea:Kennung>
              <xsl:value-of select="$Kommunikation2.Kennung"/>
            </xdomea:Kennung>
            <xdomea:Zusatz>
              <xsl:value-of select="$Kommunikation2.Zusatz"/>
            </xdomea:Zusatz>
          </xdomea:Kommunikation>
        </xdomea:Absender>
        <xdomea:Empfaenger>
          <xsl:copy-of select="xdomea:Aussonderung.Anbieteverzeichnis.0501/xdomea:Kopf/xdomea:Absender/node()"/>
        </xdomea:Empfaenger>
        <xdomea:SendendesSystem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
          <xdomea:InstanzID>
            <xsl:value-of select="$SendendesSystem.InstanzID"/>
          </xdomea:InstanzID>
          <xdomea:Produktname>
            <xsl:value-of select="$SendendesSystem.Produktname"/>
          </xdomea:Produktname>
          <xdomea:Version>
            <xsl:value-of select="$SendendesSystem.Version"/>
          </xdomea:Version>
        </xdomea:SendendesSystem>
        <xdomea:Hinweis xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"/>
        <xdomea:Empfangsbestaetigung>true</xdomea:Empfangsbestaetigung>
      </xdomea:Kopf>

      <xsl:for-each select="//xdomea:AllgemeineMetadaten">
        <xsl:if test="_Selection='A' or _Selection='V'">
          <xdomea:BewertetesObjekt>
            <xdomea:ID xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
              <xsl:value-of select="../xdomea:Identifikation/xdomea:ID"/>
            </xdomea:ID>
            <xdomea:Aussonderungsart>
              <code>
                <xsl:value-of select="_Selection"/>
              </code>
            </xdomea:Aussonderungsart>
          </xdomea:BewertetesObjekt>
        </xsl:if>
      </xsl:for-each>

    </xdomea:Aussonderung.Bewertungsverzeichnis.0502>
  </xsl:template>




</xsl:stylesheet>
