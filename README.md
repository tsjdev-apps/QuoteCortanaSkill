# Random Quote - Cortana Skill

## Einführung
Dieses GitHub-Repository beinhaltet einen Cortana-Skill, welcher ein zufälliges Zitat über eine Webschnittstelle ermittelt und dieses in textueller Form und Sprache den Nutzer mitteilt.

## Verwenden des Skills
Um den Skill selbst zu verwenden bzw. zu erweitern, empfiehlt es sich dieses als *kostenlosen* App-Service in [Azure](http://www.azure.com) zu hosten. Für die Einrichtung eines Azure-Kontos wird nach einer 30tägigen Testphase eine Kreditkarte benötigt.

## Luis-Integration
Um nicht jedes Mal einfach nur ein zufälliges Zitat zu ermittelt, wird der Webdienst [luis.ai](http://luis.ai) verwendet. Bis zu einem gewissen Kontikent ist die Verwendung kostenlos, trotzdem habe ich den Skill so konzipiert, dass dieser mit jeder beliebigen Luis-Instanz verwendet werden kann, da die beiden notwendigen Werte *AppId* und *AppSecret* per **Application settings** ausgetauscht werden können. Im Ordner *Luis* kann meine verwendete Version von Intents als `.json`-Datei herunterladen werden und in dem eigenen Luis-Account importiert werden.