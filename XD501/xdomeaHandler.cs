using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Data;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Xsl;

namespace XD501
{
    public class XdomeaHandler 
    {

        public XmlDataProvider X501DocumentDP { get; set; } = new XmlDataProvider() { Document = new XmlDocument() };


        public XdomeaHandler()
        {
            SetNamespaceManager();
        }

        /// <summary>
        /// ProzessID wird aus Dokument ausgelesen
        /// </summary>
        public string pid
        {
            get
            {
                string pid = X501DocumentDP.Document?.SelectSingleNode("//xdomea:Kopf/xdomea:ProzessID", X501DocumentDP.XmlNamespaceManager)?.InnerText ?? "";
                return pid;
            }
        }

        /// <summary>
        /// Speichert das Anbietung.Bewertungsverzeichnis
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveFile(string fileName)
        {

            try
            {
                XmlDocument document = X501DocumentDP.Document;
                if (document == null)
                {
                    return;
                }
                
                using (ZipArchive archive = ZipFile.Open(fileName, ZipArchiveMode.Create))
                {
                    ZipArchiveEntry entry = archive.CreateEntry($"{pid}_Aussonderung.Bewertungsverzeichnis.0502.xml");
                    using (Stream s = entry.Open())
                    {
                        Write502ToStream(document, s);
                    }
                }

            }
            catch (Exception ex)
            {
                errorHandling(ex.Message);
            }
        }

        /// <summary>
        /// Öffnet das Aussonderung.Anbieteverzeichnis
        /// </summary>
        /// <param name="fileName"></param>
        public void OpenFile(string fileName)
        {
            using (ZipArchive archive = ZipFile.OpenRead(fileName))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".0501.xml", StringComparison.OrdinalIgnoreCase))
                    {
                        using (var s = entry.Open())
                        {
                            X501DocumentDP.Document.Load(s);
                            SetNamespaceManager();

                            CheckSchema(X501DocumentDP.Document, Properties.Settings.Default.Schema);
                        }
                        break;
                    }
                }

                InitXMLSelection();
            }
        }

        /// <summary>
        /// Setzt den Namespace der XML
        /// </summary>
        private void SetNamespaceManager()
        {
            XmlNamespaceManager ns = new XmlNamespaceManager(X501DocumentDP.Document.NameTable);
            ns.AddNamespace("xdomea", "urn:xoev-de:xdomea:schema:2.3.0");
            X501DocumentDP.XmlNamespaceManager = ns;
        }

        /// <summary>
        /// Ergänzt die 0501 um Knoten zur Zwischenspeicherung der Bewertung
        /// </summary>
        private void InitXMLSelection()
        {
            var document = X501DocumentDP.Document;
            if (document == null)
            {
                return;
            }

            // füge neuen Hilfsknoten in das geladene XML-Dokument zum festhalten der Entscheidung
            foreach (XmlElement element in document.SelectNodes("//xdomea:Akte/xdomea:AllgemeineMetadaten", X501DocumentDP.XmlNamespaceManager))
            {
                var sel = document.CreateElement("_Selection");
                sel.InnerText = "B";
                element.AppendChild(sel);
            }
            foreach (XmlElement element in document.SelectNodes("//xdomea:Vorgang/xdomea:AllgemeineMetadaten", X501DocumentDP.XmlNamespaceManager))
            {
                var sel = document.CreateElement("_Selection");
                sel.InnerText = "B";
                element.AppendChild(sel);
            }
        }

        /// <summary>
        /// Transformiert die 0501 zur 0502 und speichert die Datei
        /// </summary>
        /// <param name="document"></param>
        /// <param name="s"></param>
        private void Write502ToStream(XmlDocument document, Stream s)
        {

            string xsltName = Properties.Settings.Default.XSTLOutput;
            string iniName = Properties.Settings.Default.OutputConfigFile;
            if (!string.IsNullOrEmpty(xsltName) && File.Exists(xsltName))
            {

                // Übergib Systemparameter an Transformation
                //https://stackoverflow.com/questions/1521064/passing-parameters-to-xslt-stylesheet-via-net
                XsltArgumentList argsList = new XsltArgumentList();
                argsList.AddParam("UserName", "", Environment.UserName);
                argsList.AddParam("MachineName", "", Environment.MachineName);
                argsList.AddParam("Now", "", XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.Unspecified));
                argsList.AddParam("ProzessID", "", pid);


                // Lade zusätzliche XSTL-Paramter aus INI-Datei
                if (!string.IsNullOrEmpty(iniName) && File.Exists(iniName))
                {
                    var ini = new IniParser.FileIniDataParser().ReadFile(iniName, Encoding.UTF8);

                    foreach (var data in ini.Global)
                    {
                        argsList.AddParam(data.KeyName, "", data.Value);
                    }
                    foreach (var section in ini.Sections)
                    {
                        foreach (var data in section.Keys)
                        {
                            argsList.AddParam($"{section.SectionName}.{data.KeyName}", "", data.Value);
                        }
                    }
                }

                XslCompiledTransform myXslTrans = new XslCompiledTransform();
                myXslTrans.Load(xsltName);


                XmlDocument result = new XmlDocument();
                using (XmlWriter xw = result.CreateNavigator().AppendChild())
                {
                    myXslTrans.Transform(document, argsList, xw);
                    xw.Close();
                }

                CheckSchema(result, Properties.Settings.Default.Schema);


                using (var writer = new XmlTextWriter(s, null))
                {
                    writer.Formatting = Formatting.Indented;
                    result.Save(writer);
                }

                return;
            }


        }

        /// <summary>
        /// Überprüft das Schema der XML Dateien. Wird sowohl eingehend als auch ausgehend verwendet.
        /// 
        /// Negative Ergebnisse werden teilweiße als Exception. Teilweiße als Event zurück gegeben.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="schemaUri"></param>
        private void CheckSchema(XmlDocument document, string schemaUri)
        {
            try
            {
                document.Schemas.Add(@"urn:xoev-de:xdomea:schema:2.3.0", schemaUri);
                document.Validate(ValidationEventHandler);
            }
            catch (Exception ex)
            {
                errorHandling(ex.Message);
            }
        }

        /// <summary>
        /// Event zur Auswertung der Schema Validierung.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    errorHandling("Error: " + e.Message);
                    break;
                case XmlSeverityType.Warning:
                    errorHandling("Warning " + e.Message);
                    break;
            }

        }

        /// <summary>
        /// Primitive Fehlerausgabe
        /// </summary>
        /// <param name="text"></param>
        private void errorHandling(string text)
        {
            System.Windows.MessageBox.Show(text);
        }

        /// <summary>
        /// Setzt die Bewertung in allen übergebenen Knoten
        /// </summary>
        /// <param name="target"></param>
        /// <param name="targetValue"></param>
        public void SetAll(IList<XmlNode> target , string targetValue)
        {
            foreach (XmlNode node in target)
            {
                XmlNode targetNode = node.SelectSingleNode("xdomea:AllgemeineMetadaten/_Selection", X501DocumentDP.XmlNamespaceManager);
                if (targetNode != null)
                {
                    targetNode.InnerText = targetValue.Substring(0,1);
                }
            }
        }

        /// <summary>
        /// Gibt an, ob eine Speicherung möglich ist. Prüft aktuell nur ob Bewertungsknoten vorhanden sind. Zukünftig Prüfung des Inhaltes möglich.
        /// </summary>
        public bool canSave
        {
            get
            {
                bool result = this.X501DocumentDP?.Document != null;
                if (result)
                {
                    result = this.X501DocumentDP.Document.SelectNodes("//xdomea:AllgemeineMetadaten/_Selection", this.X501DocumentDP.XmlNamespaceManager).Count > 0;
                }
                return result;
            }
        }
    }
}
