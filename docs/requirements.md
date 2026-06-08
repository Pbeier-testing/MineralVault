# Requirements - Mineral Vault App

## 1. Navigation und Ansichten

### R1.1 - Zwei Hauptansichten
Die Anwendung muss eine Kartenansicht und eine Tabellenansicht bereitstellen.

### R1.2 - Standardansicht beim Start
Beim Start der Anwendung muss standardmäßig die Kartenansicht angezeigt werden.

### R1.3 - Wechsel zwischen Ansichten
Der Benutzer muss zwischen Kartenansicht und Tabellenansicht wechseln können.

---

## 2. Kartenansicht

### R2.1 - Mineralien als Marker anzeigen
In der Kartenansicht müssen alle Mineralien mit gültigen Koordinaten als Marker auf der Karte angezeigt werden.

### R2.2 - Marker-Clustering
Wenn mehrere Marker in einer Region liegen, müssen diese beim Herauszoomen zu einem Cluster zusammengefasst werden.

### R2.3 - Cluster-Auflösung
Beim Hineinzoomen müssen Cluster wieder in einzelne Mineral-Marker aufgeteilt werden.

### R2.4 - Marker-Popup anzeigen
Beim Klick auf einen Mineral-Marker muss ein Popup angezeigt werden.

### R2.5 - Popup-Inhalte
Das Popup muss ein Bild, den Mineralnamen und den Fundort des Minerals anzeigen.

### R2.6 - Mineralien ohne Koordinaten
Mineralien ohne gültige Koordinaten dürfen nicht als Marker auf der Karte angezeigt werden.

---

## 3. Explorer-Liste in der Kartenansicht

### R3.1 - Mineralienliste anzeigen
Neben der Karte muss eine scrollbare Liste aller angezeigten Mineralien dargestellt werden.

### R3.2 - Darstellung als Kacheln
Jedes Mineral muss in der Liste als Kachel dargestellt werden.

### R3.3 - Kachel-Inhalte
Jede Kachel muss Bild, Mineralname, Fundort und Bemerkungen anzeigen, sofern diese Daten vorhanden sind.

### R3.4 - Anzahl anzeigen
Über der Liste muss die Anzahl der aktuell angezeigten Mineralien angezeigt werden.

### R3.5 - Sortierung Standardwert
Beim Start muss die Liste standardmäßig nach Name sortiert sein.

---

## 4. Suche und Sortierung

### R4.1 - Suchfeld in Kartenansicht
Die Kartenansicht muss ein Suchfeld bereitstellen.

### R4.2 - Suchfeld in Tabellenansicht
Die Tabellenansicht muss ein Suchfeld bereitstellen.

### R4.3 - Suchfeld in der Kartenansicht
Die Suche muss Mineralname und Fundort berücksichtigen.

### R4.4 - Suchfeld in der Tabellenansicht
Die Suche in der Tabellenansicht muss Hauptmineral, Fundort, Region und Land berücksichtigen.

### R4.5 - Teiltreffer
Die Suche muss Teiltreffer unterstützen.

### R4.6 - Groß-/Kleinschreibung
Die Suche muss unabhängig von Groß- und Kleinschreibung funktionieren.

### R4.7 - Suchergebnisse in Kartenansicht
Bei aktiver Suche dürfen in der Kartenansicht nur Marker für passende Suchergebnisse angezeigt werden.

### R4.8 - Suchergebnisse in Kartenansicht
Bei aktiver Suche darf die Mineralienliste nur passende Mineralien anzeigen.

### R4.9 - Suchergebnisse in Tabellenansicht
Bei aktiver Suche darf die Tabelle nur passende Mineralien anzeigen.

### R4.10 - Aktualisierte Anzahl
Die angezeigte Mineralienanzahl muss sich entsprechend der Suchergebnisse aktualisieren.

### R4.11 - Sortieroptionen Kartenansicht
Die Mineralienliste muss nach Name, Region und Nummer sortierbar sein.

### R4.12 - Sortieroptionen Tabellenansicht
Die Tabellenansicht muss nach Name, Region und Nummer sortierbar sein.

### R4.13 - Numerische Nummernsortierung
Die Sortierung nach Nummer muss numerisch korrekt erfolgen, sodass `2` vor `10` steht.

---

## 5. Tabellenansicht

### R5.1 - Tabellenansicht anzeigen
Die Tabellenansicht muss Mineralien tabellarisch anzeigen.

### R5.2 - Tabellenspalten
Die Tabelle muss die Spalten Nr, Bild, Hauptmineral, Begleitmineral, Fundort, Region, Land, Fundjahr, Erwerbjahr, Koordinaten und Aktionen enthalten.

### R5.3 - Inline-Bearbeitung
Der Benutzer muss die editierbaren Felder direkt in der Tabellenzeile bearbeiten können.

### R5.4 - Jahresfelder bearbeiten
Fundjahr und Erwerbjahr müssen manuell editierbar sein.

### R5.5 - Jahresfelder per Stepper bearbeiten
Fundjahr und Erwerbjahr müssen über Pfeile erhöht und verringert werden können.

---

## 6. Mineral anlegen

### R6.1 - Neues Mineral hinzufügen
Der Benutzer muss über die Schaltfläche "Neu hinzufügen" ein neues Mineral zur Tabelle hinzufügen können.

### R6.2 - Neue Zeile oben einfügen
Ein neu hinzugefügtes Mineral muss in der Tabelle an erster Position angezeigt werden.

### R6.3 - Standardwerte für neues Mineral
Ein neu hinzugefügtes Mineral muss den Namen "Neues Mineral", die Nummer "0" und ein Platzhalterbild erhalten.

### R6.4 - Neue Zeile hervorheben
Ein neu hinzugefügtes, noch nicht gespeichertes Mineral muss optisch hervorgehoben werden.

---

## 7. Bildverwaltung

### R7.1 - Thumbnail anzeigen
In der Tabellenansicht muss für jedes Mineral ein Thumbnail angezeigt werden.

### R7.2 - Platzhalterbild anzeigen
Wenn kein Bild vorhanden ist, muss ein Platzhalterbild angezeigt werden.

### R7.3 - Bild ändern
Der Benutzer muss durch Klick auf das Bild ein neues Bild für das Mineral auswählen können.

### R7.4 - Bild speichern
Ein hochgeladenes Bild muss serverseitig gespeichert werden.

### R7.5 - Genau ein Bild pro Mineral
Ein Mineral darf in der Anwendung genau ein aktives Bild besitzen.

---

## 8. Koordinatenverwaltung

### R8.1 - Koordinaten manuell bearbeiten
Koordinaten müssen in der Tabellenansicht manuell editierbar sein.

### R8.2 - Koordinaten-Popup öffnen
Der Benutzer muss über ein Kartensymbol ein Popup zur Koordinatenauswahl öffnen können.

### R8.3 - Koordinaten temporär auswählen
Im Koordinaten-Popup muss der Benutzer per Klick auf eine Karte Koordinaten temporär auswählen können.

### R8.4 - Ausgewählte Koordinaten anzeigen
Nach Auswahl auf der Karte müssen die temporär ausgewählten Koordinaten im Popup angezeigt werden.

### R8.5 - Koordinatenauswahl abbrechen
Wenn der Benutzer das Koordinaten-Popup über "Abbrechen" schließt, dürfen die bisherigen Koordinaten des Minerals nicht geändert werden.

### R8.6 - Koordinatenauswahl übernehmen
Wenn der Benutzer das Koordinaten-Popup über "Übernehmen" schließt, müssen die temporär ausgewählten Koordinaten in das Mineral übernommen werden.

---

## 9. Speichern und Löschen

### R9.1 - Änderungen speichern
Der Benutzer muss Änderungen über eine globale Schaltfläche "Speichern" speichern können.

### R9.2 - Neue Mineralien speichern
Neu angelegte Mineralien müssen beim Speichern in der Datenbank angelegt werden.

### R9.3 - Geänderte Mineralien speichern
Geänderte bestehende Mineralien müssen beim Speichern in der Datenbank aktualisiert werden.

### R9.4 - Mineral löschen
Der Benutzer muss ein Mineral über ein Mülleimer-Symbol löschen können.

### R9.5 - Löschbestätigung
Vor dem Löschen muss eine Bestätigung angezeigt werden.

### R9.6 - Löschung abbrechen
Wenn der Benutzer die Löschung nicht bestätigt, darf das Mineral nicht gelöscht werden.

### R9.7 - Löschung bestätigen
Wenn der Benutzer die Löschung bestätigt, muss das Mineral aus der Oberfläche entfernt werden.

### R9.8 - Gespeichertes Mineral aus Datenbank löschen
Wenn ein bereits gespeichertes Mineral gelöscht wird, muss der zugehörige Datenbankeintrag gelöscht werden.

### R9.9 - Bildverweis beim Löschen entfernen
Wenn ein Mineral gelöscht wird, muss der zugehörige Bildverweis aus der Datenbank entfernt werden.

### R9.10 - Bilddatei beim Löschen entfernen
Wenn ein Mineral gelöscht wird, muss die zugehörige Bilddatei aus dem Bildverzeichnis entfernt werden.

---

## 10. Validierung

### R10.1 - Pflichtfeld Hauptmineral
Das Feld Hauptmineral darf nicht leer gespeichert werden.

### R10.2 - Breitengrad validieren
Falls ein Breitengrad angegeben ist, muss er zwischen -90 und 90 liegen.

### R10.3 - Längengrad validieren
Falls ein Längengrad angegeben ist, muss er zwischen -180 und 180 liegen.

### R10.4 - Verständliche Fehlermeldungen
Bei ungültigen Eingaben muss dem Benutzer eine verständliche Fehlermeldung angezeigt werden.

---

## 11. Nicht-funktionale Anforderungen

### R11.1 - Scrollbare Listen
Lange Mineralienlisten müssen scrollbar bleiben, ohne die gesamte Anwendung unbenutzbar zu machen.

### R11.2 - Bedienbarkeit
Die Hauptfunktionen der Anwendung müssen per Maus bedienbar sein.

### R11.3 - Datenkonsistenz
Nach dem Speichern müssen die angezeigten Daten dem gespeicherten Datenbankstand entsprechen.