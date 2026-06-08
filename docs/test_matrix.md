# Test Matrix - Mineral Vault App

Diese Matrix verbindet Requirements mit Testfällen, Testlevel, Automatisierung und aktuellem Status.

Statuswerte: 

- `Open`: noch nicht umgesetzt oder noch nicht automatisiert
- `Covered`: durch Tests nach aktueller Matrix abgedeckt
- `Failing`: Test existiert, aber die Anwendung erfüllt das Soll-Verhalten noch nicht
- `Manual`: bewusst als manueller Test vorgesehen

| Requirement | Test Case IDs | Test Level | Automation | Status | Notes |
|---|---|---|---|---|---|
| R1.1 Zwei Hauptansichten | E2E-NAV-001 | E2E | Playwright | Open | Karten- und Tabellenansicht sichtbar |
| R1.2 Standardansicht beim Start | E2E-NAV-001 | E2E | Playwright | Open | App startet in Kartenansicht |
| R1.3 Wechsel zwischen Ansichten | E2E-NAV-002 | E2E | Playwright | Open | Navigation zwischen Ansichten |
| R2.1 Mineralien als Marker anzeigen | E2E-NAV-001 | E2E | Playwright | Open | Marker für Mineralien mit Koordinaten |
| R2.2 Marker-Clustering | MTC-MAP-001 | Manual | Manual | Manual | Visuelle Kartenfunktion |
| R2.3 Cluster-Auflösung | MTC-MAP-001 | Manual | Manual | Manual | Zoom-Verhalten |
| R2.4 Marker-Popup anzeigen | E2E-MAP-001 | E2E | Playwright | Open | Klick auf Marker öffnet Popup |
| R2.5 Popup-Inhalte | E2E-MAP-001 | E2E | Playwright | Open | Bild, Name, Fundort |
| R2.6 Mineralien ohne Koordinaten | Open | Unit/E2E | xUnit, Playwright | Open | Testfall noch formulieren |
| R3.1 Mineralienliste anzeigen | E2E-NAV-001, MTC-UI-001 | E2E/Manual | Playwright, Manual | Open | Liste neben Karte |
| R3.2 Darstellung als Kacheln | Open | E2E/Manual | Playwright optional | Open | Testfall noch formulieren |
| R3.3 Kachel-Inhalte | Open | E2E | Playwright | Open | Testfall noch formulieren |
| R3.4 Anzahl anzeigen | Open | E2E | Playwright | Open | Testfall noch formulieren |
| R3.5 Sortierung Standardwert | UTC-SORT-001 | Unit/E2E | xUnit, Playwright | Covered | Unit-Test für Standardsortierung vorhanden |
| R4.1 Suchfeld in Kartenansicht | E2E-SEARCH-001 | E2E | Playwright | Open | Suchfeld sichtbar |
| R4.2 Suchfeld in Tabellenansicht | Open | E2E | Playwright | Open | Testfall noch formulieren |
| R4.3 Suchfeld in der Kartenansicht | UTC-SEARCH-001, UTC-SEARCH-002, E2E-SEARCH-001 | Unit/E2E | xUnit, Playwright | Covered | Unit-Tests für Mineralname und Fundort vorhanden; E2E offen |
| R4.4 Suchfeld in der Tabellenansicht | UTC-SEARCH-004, UTC-SEARCH-005 | Unit/E2E | xUnit, Playwright | Failing | Land wird aktuell nicht durchsucht |
| R4.5 Teiltreffer | UTC-SEARCH-001, UTC-SEARCH-002 | Unit | xUnit | Covered | Unit-Tests vorhanden |
| R4.6 Gross-/Kleinschreibung | UTC-SEARCH-003 | Unit | xUnit | Covered | Unit-Test vorhanden |
| R4.7 Suchergebnisse in Kartenansicht | E2E-SEARCH-001 | E2E | Playwright | Open | Karte zeigt nur gefilterte Marker |
| R4.8 Suchergebnisse in Kartenansicht | E2E-SEARCH-001 | E2E | Playwright | Open | Requirement beschreibt faktisch die Explorer-Liste |
| R4.9 Suchergebnisse in Tabellenansicht | Open | Unit/E2E | xUnit, Playwright | Open | Testfall noch formulieren |
| R4.10 Aktualisierte Anzahl | E2E-SEARCH-001 | E2E | Playwright | Open | UI-Anzahl nach Suche |
| R4.11 Sortieroptionen Kartenansicht | UTC-SORT-001, UTC-SORT-002, UTC-SORT-003 | Unit/E2E | xUnit, Playwright | Covered | Unit-Tests für Name, Region und Nummer vorhanden; E2E offen |
| R4.12 Sortieroptionen Tabellenansicht | Open | Unit/E2E | xUnit, Playwright | Open | Testfall noch formulieren |
| R4.13 Numerische Nummernsortierung | UTC-SORT-003 | Unit | xUnit | Covered | `2` vor `10` |
| R5.1 Tabellenansicht anzeigen | E2E-NAV-002, E2E-TABLE-001 | E2E | Playwright | Open | Tabelle sichtbar |
| R5.2 Tabellenspalten | E2E-TABLE-001, ITC-CSV-001 | E2E/Integration | Playwright, xUnit | Open | Erwartete Spalten und CSV-Felder |
| R5.3 Inline-Bearbeitung | Open | E2E | Playwright | Open | Testfall noch formulieren |
| R5.4 Jahresfelder bearbeiten | Open | E2E | Playwright | Open | Testfall noch formulieren |
| R5.5 Jahresfelder per Stepper bearbeiten | Open | E2E/Manual | Playwright optional | Open | Testfall noch formulieren |
| R6.1 Neues Mineral hinzufügen | UTC-CREATE-001, UTC-CREATE-003, E2E-CREATE-001 | Unit/E2E | xUnit, Playwright | Covered | Unit-Tests vorhanden |
| R6.2 Neue Zeile oben einfügen | UTC-CREATE-001, E2E-CREATE-001 | Unit/E2E | xUnit, Playwright | Covered | Unit-Test vorhanden |
| R6.3 Standardwerte für neues Mineral | UTC-CREATE-002, E2E-CREATE-001 | Unit/E2E | xUnit, Playwright | Covered | Name und Nummer getestet; Platzhalterbild UI offen |
| R6.4 Neue Zeile hervorheben | E2E-CREATE-001 | E2E/Manual | Playwright optional | Open | Visuelle Hervorhebung |
| R7.1 Thumbnail anzeigen | Open | E2E | Playwright | Open | Testfall noch formulieren |
| R7.2 Platzhalterbild anzeigen | Open | E2E | Playwright | Open | Testfall noch formulieren |
| R7.3 Bild ändern | Open | E2E | Playwright | Open | Testfall noch formulieren |
| R7.4 Bild speichern | ITC-IMG-001 | Integration | xUnit | Open | API und Dateisystem |
| R7.5 Genau ein Bild pro Mineral | UTC-IMAGE-001, UTC-IMAGE-002, ITC-CSV-001 | Unit/Integration | xUnit | Covered | Unit-Tests für Bildersetzung vorhanden; Integration offen |
| R8.1 Koordinaten manuell bearbeiten | Open | E2E | Playwright | Open | Testfall noch formulieren |
| R8.2 Koordinaten-Popup öffnen | E2E-COORD-001, E2E-COORD-002 | E2E | Playwright | Open | Popup sichtbar |
| R8.3 Koordinaten temporär auswählen | UTC-COORD-001, E2E-COORD-001, E2E-COORD-002 | Unit/E2E | xUnit, Playwright | Failing | Koordinaten werden aktuell sofort ins Mineral geschrieben |
| R8.4 Ausgewählte Koordinaten anzeigen | E2E-COORD-002 | E2E | Playwright | Open | Popup-Anzeige |
| R8.5 Koordinatenauswahl abbrechen | UTC-COORD-002, E2E-COORD-001 | Unit/E2E | xUnit, Playwright | Failing | Abbrechen behält aktuell nicht die Originalkoordinaten |
| R8.6 Koordinatenauswahl übernehmen | E2E-COORD-002 | E2E | Playwright | Open | Übernahme testen |
| R9.1 Änderungen speichern | UTC-SAVE-001, UTC-SAVE-002, UTC-SAVE-003, UTC-SAVE-004 | Unit/Integration/E2E | xUnit, Playwright | Covered | Unit-Tests fürs Speichern vorhanden; Integration offen |
| R9.2 Neue Mineralien speichern | UTC-SAVE-001, ITC-API-001 | Unit/Integration | xUnit | Covered | POST im ViewModel und API-Persistenz getestet |
| R9.3 Geänderte Mineralien speichern | UTC-SAVE-002, ITC-API-002 | Unit/Integration | xUnit | Covered | PUT im ViewModel und API-Persistenz getestet |
| R9.4 Mineral löschen | UTC-DELETE-002, UTC-DELETE-003, E2E-DELETE-001, E2E-DELETE-002 | Unit/E2E | xUnit, Playwright | Covered | Unit-Tests fürs Löschen vorhanden; E2E offen |
| R9.5 Löschbestätigung | E2E-DELETE-001, E2E-DELETE-002 | E2E | Playwright | Open | Dialog erscheint |
| R9.6 Löschung abbrechen | UTC-DELETE-001, E2E-DELETE-001 | Unit/E2E | xUnit, Playwright | Covered | Unit-Test vorhanden; E2E offen |
| R9.7 Löschung bestätigen | UTC-DELETE-002, UTC-DELETE-003, E2E-DELETE-002 | Unit/E2E | xUnit, Playwright | Covered | Unit-Test für UI-Entfernung vorhanden; E2E offen |
| R9.8 Gespeichertes Mineral aus Datenbank löschen | UTC-DELETE-002, ITC-API-003 | Unit/Integration | xUnit | Covered | DELETE im ViewModel und API-Persistenz getestet |
| R9.9 Bildverweis beim Löschen entfernen | ITC-IMG-002 | Integration | xUnit | Open | DB-Beziehung |
| R9.10 Bilddatei beim Löschen entfernen | UTC-DELETE-003, ITC-IMG-003 | Unit/Integration | xUnit | Open | Dateisystem-Integration offen |
| R10.1 Pflichtfeld Hauptmineral | UTC-VAL-001 | Unit/E2E | xUnit, Playwright | Covered | Model-Validierung getestet; UI offen |
| R10.2 Breitengrad validieren | UTC-VAL-002, UTC-VAL-003 | Unit/E2E | xUnit, Playwright | Covered | Model-Grenzwerte getestet; UI offen |
| R10.3 Längengrad validieren | UTC-VAL-004, UTC-VAL-005 | Unit/E2E | xUnit, Playwright | Covered | Model-Grenzwerte getestet; UI offen |
| R10.4 Verständliche Fehlermeldungen | UTC-VAL-001, UTC-VAL-003, UTC-VAL-005 | Unit/E2E | xUnit, Playwright | Covered | Model-Fehlermeldungen getestet; UI offen |
| R11.1 Scrollbare Listen | MTC-UI-001, MTC-UI-003 | Manual | Manual | Manual | Visuelle Bedienbarkeit |
| R11.2 Bedienbarkeit | MTC-UI-002, MTC-UI-003 | Manual | Manual | Manual | Mausbedienung |
| R11.3 Datenkonsistenz | ITC-API-001, ITC-API-002, ITC-API-003 | Integration/E2E | xUnit, Playwright | Covered | API-Datenkonsistenz für Speichern, Aktualisieren und Löschen getestet |

