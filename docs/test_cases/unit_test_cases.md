# Unit Test Cases

Unit Tests prüfen isolierte Logik ohne echte Datenbank, Browser oder Dateisystem.

## UTC-VAL-001 - Pflichtfeld Hauptmineral validieren

**Related Requirements:** R10.1, R10.4  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Preconditions:**
- Ein `Mineral`-Objekt kann per DataAnnotations validiert werden.

**Test Data:**
- `Name = ""`

**Steps:**
1. Erstelle ein Mineral mit leerem Hauptmineral-Namen.
2. Führe die Model-Validierung aus.

**Expected Result:**
- Die Validierung liefert einen Fehler für das Pflichtfeld Hauptmineral.

## UTC-VAL-002 - Gültige Breitengrad-Grenzwerte akzeptieren

**Related Requirements:** R10.2  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Test Data:**
- `Breitengrad = -90`
- `Breitengrad = 90`
- `Breitengrad = null`

**Steps:**
1. Erstelle je Testwert ein gültiges Mineral.
2. Führe die Model-Validierung aus.

**Expected Result:**
- Es wird kein Validierungsfehler für den Breitengrad erzeugt.

## UTC-VAL-003 - Ungültige Breitengrade ablehnen

**Related Requirements:** R10.2, R10.4  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Test Data:**
- `Breitengrad = -90.1`
- `Breitengrad = 90.1`

**Steps:**
1. Erstelle je Testwert ein Mineral.
2. Führe die Model-Validierung aus.

**Expected Result:**
- Es wird ein Validierungsfehler für den Breitengrad erzeugt.

## UTC-VAL-004 - Gültige Längengrad-Grenzwerte akzeptieren

**Related Requirements:** R10.3  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Test Data:**
- `Längengrad = -180`
- `Längengrad = 180`
- `Längengrad = null`

**Steps:**
1. Erstelle je Testwert ein gültiges Mineral.
2. Führe die Model-Validierung aus.

**Expected Result:**
- Es wird kein Validierungsfehler für den Längengrad erzeugt.

## UTC-VAL-005 - Ungültige Längengrade ablehnen

**Related Requirements:** R10.3, R10.4  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Test Data:**
- `Längengrad = -180.1`
- `Längengrad = 180.1`

**Steps:**
1. Erstelle je Testwert ein Mineral.
2. Führe die Model-Validierung aus.

**Expected Result:**
- Es wird ein Validierungsfehler für den Längengrad erzeugt.

## UTC-VAL-006 - Gültige Fundjahr-Werte akzeptieren

**Related Requirements:** R10.5  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Test Data:**
- `Fundjahr = null`
- `Fundjahr = 1801`
- `Fundjahr = 1984`
- `Fundjahr = aktuelles Kalenderjahr`

**Expected Result:**
- Es wird kein Validierungsfehler für das Fundjahr erzeugt.

## UTC-VAL-007 - Ungültige Fundjahr-Werte ablehnen

**Related Requirements:** R10.5, R10.4  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Test Data:**
- `Fundjahr = 1800`
- `Fundjahr = 1799`
- `Fundjahr = aktuelles Kalenderjahr + 1`

**Expected Result:**
- Es wird ein Validierungsfehler für das Fundjahr erzeugt.

## UTC-VAL-008 - Gültige Erwerbjahr-Werte akzeptieren

**Related Requirements:** R10.6  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Test Data:**
- `Erwerbjahr = null`
- `Erwerbjahr = 1801`
- `Erwerbjahr = 1984`
- `Erwerbjahr = aktuelles Kalenderjahr`

**Expected Result:**
- Es wird kein Validierungsfehler für das Erwerbjahr erzeugt.

## UTC-VAL-009 - Ungültige Erwerbjahr-Werte ablehnen

**Related Requirements:** R10.6, R10.4  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Test Data:**
- `Erwerbjahr = 1800`
- `Erwerbjahr = 1799`
- `Erwerbjahr = aktuelles Kalenderjahr + 1`

**Expected Result:**
- Es wird ein Validierungsfehler für das Erwerbjahr erzeugt.

## UTC-SEARCH-001 - Suche findet Mineralname als Teiltreffer

**Related Requirements:** R4.3, R4.5  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Test Data:**
- Mineral: `Flussspat`
- Suchbegriff: `fluss`

**Steps:**
1. Befülle das ViewModel mit Testmineralien.
2. Setze den Suchbegriff.
3. Lies die gefilterte Mineralienliste aus.

**Expected Result:**
- Genau das Mineral `Flussspat` wird zurückgegeben.

## UTC-SEARCH-002 - Suche findet Fundort als Teiltreffer

**Related Requirements:** R4.3, R4.5  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Test Data:**
- Mineral mit `Fundort = Dresden`
- Suchbegriff: `dres`

**Expected Result:**
- Das passende Mineral wird zurückgegeben.

## UTC-SEARCH-003 - Suche ignoriert Gross- und Kleinschreibung

**Related Requirements:** R4.6  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Test Data:**
- Mineral mit `Fundort = Freiberg`
- Suchbegriff: `FREIBERG`

**Expected Result:**
- Das passende Mineral wird gefunden.

## UTC-SEARCH-004 - Tabellensuche berücksichtigt Region

**Related Requirements:** R4.4  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Test Data:**
- Zwei Minerale mit `Region = Sachsen`
- Suchbegriff: `Sachsen`

**Expected Result:**
- Beide Minerale aus Sachsen werden zurückgegeben.

## UTC-SEARCH-005 - Tabellensuche berücksichtigt Land

**Related Requirements:** R4.4  
**Status:** Failing  
**Test Level:** Unit  
**Automation:** xUnit  

**Test Data:**
- Drei Minerale mit `Land = Deutschland`
- Suchbegriff: `Deutschland`

**Expected Result:**
- Alle drei Minerale werden zurückgegeben.

**Current Result:**
- Der Test schlägt fehl, weil `Land` aktuell nicht durchsucht wird.

## UTC-SORT-001 - Standardsortierung erfolgt nach Name

**Related Requirements:** R3.5, R4.11  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Expected Result:**
- Die Mineralien werden alphabetisch nach Name sortiert.

## UTC-SORT-002 - Sortierung nach Region

**Related Requirements:** R4.11  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Expected Result:**
- Die Mineralien werden nach Region sortiert.

## UTC-SORT-003 - Sortierung nach Nummer erfolgt numerisch

**Related Requirements:** R4.13  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Test Data:**
- Nummern: `10`, `2`, `1`

**Expected Result:**
- Die Sortierung ergibt `1`, `2`, `10`.

## UTC-CREATE-001 - Neues Mineral wird oben eingefügt

**Related Requirements:** R6.1, R6.2  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Steps:**
1. Befülle das ViewModel mit vorhandenen Mineralien.
2. Führe `CreateNewMineral()` aus.

**Expected Result:**
- Das neue Mineral steht an erster Position.

## UTC-CREATE-002 - Neues Mineral erhält Standardwerte

**Related Requirements:** R6.3  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Expected Result:**
- Name ist `NEUES MINERAL`.
- Nummer ist `0`.
- Die Bildliste ist leer und kann in der UI als Platzhalterbild dargestellt werden.

## UTC-CREATE-003 - Neues Mineral wird nicht erstellt, wenn Mineralien noch nicht geladen sind

**Related Requirements:** R6.1  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Preconditions:**
- Die Mineralienliste im ViewModel ist noch nicht geladen.

**Steps:**
1. Erstelle ein ViewModel mit `Minerals = null`.
2. Führe `CreateNewMineral()` aus.

**Expected Result:**
- Es wird kein Mineral erstellt.
- Die Mineralienliste bleibt `null`.

## UTC-DELETE-001 - Abgebrochene Löschung entfernt Mineral nicht

**Related Requirements:** R9.6  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Expected Result:**
- Das Mineral bleibt in der ViewModel-Liste.
- Es wird kein HTTP-Request gesendet.

## UTC-DELETE-002 - Bestätigte Löschung eines gespeicherten Minerals sendet DELETE

**Related Requirements:** R9.4, R9.7, R9.8  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Expected Result:**
- Das Mineral wird aus der ViewModel-Liste entfernt.
- Es wird ein `DELETE api/minerals/{id}` Request gesendet.

## UTC-DELETE-003 - Bestätigte Löschung eines ungespeicherten Minerals mit Bild löscht Upload

**Related Requirements:** R9.4, R9.7, R9.10  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Expected Result:**
- Das Mineral wird aus der ViewModel-Liste entfernt.
- Es wird ein `DELETE api/image/{fileName}` Request gesendet.

## UTC-SAVE-001 - Neues Mineral wird per POST vorbereitet

**Related Requirements:** R9.1, R9.2  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Expected Result:**
- Für ein Mineral mit `Id = 0` wird ein `POST api/minerals` Request gesendet.
- Die zurückgegebene ID wird in das Mineral übernommen.

## UTC-SAVE-002 - Bestehendes Mineral wird per PUT vorbereitet

**Related Requirements:** R9.1, R9.3  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Expected Result:**
- Für ein Mineral mit vorhandener ID wird ein `PUT api/minerals/{id}` Request gesendet.

## UTC-SAVE-003 - Speichern ohne geladene Mineralien sendet keinen Request

**Related Requirements:** R9.1  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Preconditions:**
- Die Mineralienliste im ViewModel ist noch nicht geladen.

**Expected Result:**
- Es wird kein HTTP-Request gesendet.
- `IsSaving` bleibt oder wird `false`.

## UTC-SAVE-004 - Speichern während laufendem Speichervorgang sendet keinen Request

**Related Requirements:** R9.1  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Preconditions:**
- `IsSaving = true`

**Expected Result:**
- Es wird kein weiterer HTTP-Request gesendet.
- `IsSaving` bleibt `true`.

## UTC-IMAGE-001 - Erfolgreicher Upload ersetzt vorhandenes Bild

**Related Requirements:** R7.5  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Expected Result:**
- Die vorhandene Bildreferenz wird entfernt.
- Genau eine neue Bildreferenz wird gesetzt.

## UTC-IMAGE-002 - Fehlgeschlagener Upload behält vorhandenes Bild

**Related Requirements:** R7.5  
**Status:** Automated  
**Test Level:** Unit  
**Automation:** xUnit  

**Expected Result:**
- Die vorhandene Bildreferenz bleibt unverändert.

## UTC-COORD-001 - Koordinatenauswahl verändert Mineral nicht sofort

**Related Requirements:** R8.3  
**Status:** Failing  
**Test Level:** Unit  
**Automation:** xUnit  

**Expected Result:**
- Eine Koordinatenauswahl wird nur temporär gehalten.
- Das aktive Mineral bleibt unverändert.

**Current Result:**
- Der Test schlägt fehl, weil die Koordinaten aktuell sofort ins Mineral geschrieben werden.

## UTC-COORD-002 - Abbrechen behält Originalkoordinaten

**Related Requirements:** R8.5  
**Status:** Failing  
**Test Level:** Unit  
**Automation:** xUnit  

**Expected Result:**
- Nach `Abbrechen` bleiben die ursprünglichen Koordinaten erhalten.

**Current Result:**
- Der Test schlägt fehl, weil die Koordinaten aktuell bereits vor dem Abbrechen geändert wurden.
