# Integration Test Cases

Integration Tests prüfen das Zusammenspiel von API, Datenbank und Dateisystem. 

## ITC-API-001 - Neues Mineral per API speichern

**Related Requirements:** R9.2, R11.3  
**Status:** Draft  
**Test Level:** Integration  
**Automation:** xUnit, WebApplicationFactory, SQLite-Testdatenbank  

**Steps:**
1. Starte die API mit isolierter Testdatenbank.
2. Sende `POST api/minerals` mit gültigem Mineral.
3. Gib das Mineral aus der Datenbank oder per API zurück.

**Expected Result:**
- Die API liefert eine erfolgreiche Antwort.
- Das Mineral ist in der Testdatenbank vorhanden.

## ITC-API-002 - Bestehendes Mineral per API aktualisieren

**Related Requirements:** R9.3, R11.3  
**Status:** Draft  
**Test Level:** Integration  
**Automation:** xUnit, WebApplicationFactory, SQLite-Testdatenbank  

**Expected Result:**
- Ein `PUT api/minerals/{id}` aktualisiert den Datenbankeintrag.

## ITC-API-003 - Gespeichertes Mineral löschen

**Related Requirements:** R9.8, R11.3  
**Status:** Draft  
**Test Level:** Integration  
**Automation:** xUnit, WebApplicationFactory, SQLite-Testdatenbank  

**Expected Result:**
- Eine bestätigter Löschung entfernt den Datenbankeintrag.

## ITC-IMG-001 - Bilddatei speichern

**Related Requirements:** R7.4  
**Status:** Draft  
**Test Level:** Integration  
**Automation:** xUnit, WebApplicationFactory, temporäres Bildverzeichnis  

**Expected Result:**
- Der Upload-Endpunkt speichert eine Datei im vorgegebenen Bildverzeichnis.
- Die API gibt den gespeicherten Dateinamen zurück.

## ITC-IMG-002 - Bildverweis beim Löschen entfernen

**Related Requirements:** R9.9  
**Status:** Draft  
**Test Level:** Integration  
**Automation:** xUnit, WebApplicationFactory, SQLite-Testdatenbank  

**Expected Result:**
- Beim Löschen eines Minerals wird der zugehörige Bildverweis aus der Datenbank entfernt.

## ITC-IMG-003 - Bilddatei beim Löschen entfernen

**Related Requirements:** R9.10  
**Status:** Draft  
**Test Level:** Integration  
**Automation:** xUnit, WebApplicationFactory, temporäres Bildverzeichnis  

**Expected Result:**
- Beim Löschen eines Minerals wird die zugehörige Bilddatei aus dem Bildverzeichnis entfernt.

## ITC-CSV-001 - CSV-Import legt Mineralien mit Bildern und Jahresfeldern an

**Related Requirements:** R5.2, R7.5, R11.3  
**Status:** Draft  
**Test Level:** Integration  
**Automation:** xUnit, WebApplicationFactory, SQLite-Testdatenbank  

**Expected Result:**
- Der CSV-Import legt Mineralien mit Nummer, Name, Fundort, Region, Land, Fundjahr, Erwerbsjahr und Bildreferenzen an.

