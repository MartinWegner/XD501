# XD501 - XDOMEA 2 Anbietungsbewerter
 
Der **XDOMEA 2 Anbietungsbewerter** ist ein von [PDV-Systeme GmbH](https://www.pdv.de/) entwickeltes Tool zur Bewertung von [XDOMEA konformen](http://www.xdomea.de/) Aussonderungslisten.
 
Ziel des Programmes ist es grundlegende Funktionalitäten zur Bewertung von Lieferlisten bereitzustellen. Es dient also dem Einlesen von Lieferlisten (Nachrichtentyp 0501), der Bewertung der Akten und Akteninhalte und der anschließenden Ausgabe der bewertenden Lieferlisten (Nachrichtentyp 0502).

Das Programm zeigt eine übersichtliche Menge an Informationen aus der XDOMEA Spezifikation an. Diese können nach individuellen Anforderungen erweitert werden. Mehr dazu finden Sie im Abschnitt *Technische Ideen*
 
# Download

Den Download der aktuellen Version finden sie [hier](https://github.com/MartinWegner/XD501/releases).
 
# Kontext
 
Die VIS-SUITE der [PDV-Systeme GmbH](https://www.pdv.de/) ist eine Produktlinie zur ganzheitlichen digitalen Aktenführung und Vorgangsbearbeitung, dass die XDOMEA konforme
Gestaltung des Aussonderungsprozesses unterstützt. Die Bewertung der Lieferlisten kann mit dem **XDOMEA 2 Anbietungsbewerter** erfolgen. Dieser ist aber auch unabhängig von der VIS-SUITE verwendbar und setzt weder einen Zugriff auf einen VIS Mandanten, noch einen VIS Clienten voraus.
 
# Technische Ideen
Die Applikation verfolgt folgende Design-Ideen:

1. Es existiert kein Datenmodell im klassischen Sinne. Alle Bindings erfolgen mit XPath direkt auf den XML Eingangsdaten. Auch die temporären Daten werden innerhalb dieser Struktur zwischengespeichert. Die Ausgabe erfolgt über eine XSL Transformation. Die Regeln für die Transformation liegen in \XD501\Templates\output.xslt und können zur Laufzeit angepasst werden.

1. Zusätzliche individuelle statische Felder können in der \XD501\Templates\output.ini festgelegt und in der \XD501\Templates\output.xslt verwendet werden. Dadurch sind Anpassungen an statischen Daten ohne Programmanpassung zur Laufzeit möglich.

1. Die Oberfläche ist dynamisch und nicht fest vorgegeben. Diese liegt in \XD501\Templates\MainView.xaml und wird bei Programmstart geladen. Dadurch können auch die Oberflächen von installierten Instanzen schnell an die individuellen Anforderungen des Nutzers angepasst werden.

