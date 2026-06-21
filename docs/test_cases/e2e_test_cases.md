# E2E Test Cases

E2E Tests prüfen zentrale Benutzerflüsse im Browser. Diese Testfälle sind für Playwright vorgesehen.

## E2E-NAV-001 - Anwendung startet in der Kartenansicht

**Related Requirements:** R1.1, R1.2  
**Status:** Automated  
**Test Level:** E2E  
**Automation:** Playwright  

**Execution:**
- Der Test verwendet standardmäßig `http://localhost:5119`.
- Falls die Anwendung auf einer anderen URL läuft, kann `MINERALVAULT_E2E_BASE_URL` gesetzt werden.

**Expected Result:**
- Die Kartenansicht ist sichtbar.
- Die Explorer-Liste ist sichtbar.

## E2E-NAV-002 - Wechsel zwischen Kartenansicht und Tabellenansicht

**Related Requirements:** R1.3, R5.1  
**Status:** Automated  
**Test Level:** E2E  
**Automation:** Playwright  

**Execution:**
- Der Benutzer wechselt ueber die Navigation von der Kartenansicht in die Tabellenansicht.
- Der Benutzer wechselt anschliessend zurueck in die Kartenansicht.

**Expected Result:**
- Der Benutzer kann zur Tabellenansicht wechseln.
- Der Benutzer kann wieder zur Kartenansicht wechseln.

## E2E-MAP-001 - Marker-Popup zeigt Mineralinformationen

**Related Requirements:** R2.4, R2.5  
**Status:** Draft  
**Test Level:** E2E  
**Automation:** Playwright  

**Expected Result:**
- Klick auf einen Marker öffnet ein Popup.
- Das Popup zeigt Bild, Mineralname und Fundort.

## E2E-SEARCH-001 - Suche filtert Explorer-Liste und Kartenmarker

**Related Requirements:** R4.1, R4.3, R4.8, R4.10  
**Status:** Automated  
**Test Level:** E2E  
**Automation:** Playwright  

**Execution:**
- Der Benutzer sucht in der Kartenansicht nach `Coelestin`.
- Der Benutzer leert das Suchfeld anschließend wieder.

**Expected Result:**
- Die Explorer-Liste zeigt nur Suchtreffer.
- Die Mineralienanzahl wird aktualisiert.
- Nach dem Leeren der Suche wird die ursprüngliche Mineralienanzahl wieder angezeigt.

## E2E-TABLE-001 - Tabellenansicht zeigt erwartete Spalten

**Related Requirements:** R5.1, R5.2  
**Status:** Automated  
**Test Level:** E2E  
**Automation:** Playwright  

**Execution:**
- Der Benutzer wechselt in die Tabellenansicht.

**Expected Result:**
- Alle erwarteten Tabellenspalten sind sichtbar.

## E2E-CREATE-001 - Neues Mineral wird in der Tabelle angelegt

**Related Requirements:** R6.1, R6.2, R6.3, R6.4  
**Status:** Automated  
**Test Level:** E2E  
**Automation:** Playwright  

**Execution:**
- Der Benutzer wechselt in die Tabellenansicht.
- Der Benutzer klickt auf `Neu hinzufügen`.

**Expected Result:**
- Nach Klick auf `Neu hinzufügen` erscheint eine neue Zeile oben.
- Die neue Zeile hat Standardwerte.
- Die neue Zeile ist optisch hervorgehoben.

## E2E-COORD-001 - Koordinaten-Popup abbrechen

**Related Requirements:** R8.2, R8.5  
**Status:** Automated  
**Test Level:** E2E  
**Automation:** Playwright  

**Execution:**
- Der Benutzer öffnet das Koordinaten-Popup in der Tabellenansicht.
- Der Benutzer bricht das Popup ohne neue Kartenauswahl ab.

**Expected Result:**
- Das Popup kann geöffnet werden.
- Nach Klick auf `Abbrechen` bleiben die ursprünglichen Koordinaten erhalten.

## E2E-COORD-002 - Koordinaten-Popup übernehmen

**Related Requirements:** R8.2, R8.4, R8.6  
**Status:** Automated  
**Test Level:** E2E  
**Automation:** Playwright  

**Execution:**
- Der Benutzer öffnet das Koordinaten-Popup in der Tabellenansicht.
- Der Benutzer wählt Koordinaten über die Karte aus.
- Der Benutzer übernimmt die ausgewählten Koordinaten.

**Expected Result:**
- Ausgewählte Koordinaten werden im Popup angezeigt.
- Nach Klick auf `Übernehmen` stehen die Koordinaten in der Tabellenzeile.

## E2E-DELETE-001 - Löschung abbrechen

**Related Requirements:** R9.4, R9.5, R9.6  
**Status:** Draft  
**Test Level:** E2E  
**Automation:** Playwright  

**Expected Result:**
- Der Warnhinweis erscheint.
- Nach Abbruch bleibt das Mineral sichtbar.

## E2E-DELETE-002 - Löschung bestätigen

**Related Requirements:** R9.4, R9.5, R9.7  
**Status:** Draft  
**Test Level:** E2E  
**Automation:** Playwright  

**Expected Result:**
- Nach Bestätigung verschwindet das Mineral aus der UI.
