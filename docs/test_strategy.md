# Test Strategy - Mineral Vault App

## Ziel

Diese Teststrategie beschreibt, wie die Mineral Vault App systematisch getestet wird. Sie orientiert sich am ISTQB-Testprozess und verbindet Requirements, Testfälle, automatisierte Tests und CI/CD-Ausführung.

## Testobjekte

- `MineralCollection.Domain`: Mineralmodell und Validierungsregeln
- `MineralCollection.Frontend`: Blazor WebAssembly UI und ViewModel-Logik
- `MineralCollection.API`: REST API, Datenbankzugriffe, CSV-Import und Bildverwaltung
- Datenbank: SQLite-Persistenz, Migrations und Beziehungen zwischen Mineralen und Bildern
- UI-Flows: Kartenansicht, Tabellenansicht, Suche, Sortierung, Bild-Upload, Koordinaten-Auswahl, Speichern und Löschen

## Testlevel

### Unit Tests

Unit Tests prüfen kleine, isolierte Logikeinheiten ohne echte Datenbank, Browser oder Dateisystem.

Geeignet fur:

- Model-Validierung
- Such- und Sortierlogik
- Auswahl- und Abbruchlogik
- Vorbereitung von API-Aufrufen

Werkzeuge:

- xUnit
- Moq oder eigene Fakes
- Fake `HttpMessageHandler` fur kontrollierte HTTP-Antworten

### Integration Tests

Integration Tests prüfen das Zusammenspiel mehrerer technischer Komponenten.

Geeignet fur:

- API-Endpunkte
- EF Core und SQLite-Testdatenbank
- Bild-Upload und Bildlöschung
- Datenbankbeziehungen zwischen Mineral und MineralImage
- CSV-Import

Werkzeuge:

- xUnit
- `Microsoft.AspNetCore.Mvc.Testing`
- isolierte Testdatenbank pro Testlauf
- temporäre Testverzeichnisse fur Bilder

### End-to-End Tests

E2E Tests prüfen zentrale Benutzerflüsse aus Sicht eines Anwenders im Browser.

Geeignet fur:

- Start in der Kartenansicht
- Suche filtert Liste und Karte
- Wechsel zwischen Karten- und Tabellenansicht
- neues Mineral anlegen
- Inline-Bearbeitung in der Tabelle
- Koordinaten-Popup öffnen, abbrechen und übernehmen
- Löschdialog abbrechen und bestatigen

Werkzeuge:

- Playwright
- laufende API und laufendes Frontend
- definierte Testdaten

### Manuelle Tests

Manuelle Tests bleiben sinnvoll, wenn die Bewertung stark visuell oder explorativ ist.

Geeignet fur:

- responsives Layout
- Bedienbarkeit der Karte
- visuelle Darstellung von Clustern, Popups und Markern
- allgemeine Usability

## Testpriorisierung

Die Priorisierung richtet sich nach Risiko und Nutzen:

1. Datenverlust oder falsche Persistenz
2. Löschen inklusive Bild- und Datenbankkonsistenz
3. Speichern neuer und geänderter Daten
4. Suche, Sortierung und Filterung
5. Koordinatenlogik
6. Bildverwaltung
7. UI-Darstellung und Bedienbarkeit

## Automatisierungsstrategie

- Unit Tests und Integrationstests laufen bei jedem lokalen Testlauf und in jeder CI-Ausführung.
- Manuelle Tests werden fur visuelle und responsive Anforderungen dokumentiert.

## CI/CD

Die Pipeline soll langfristig in drei Phasen laufen:

1. Restore, Build und Unit Tests
2. Integration Tests mit isolierter Testdatenbank
3. Playwright E2E Tests mit gestarteter API und Frontend-App

Die aktuelle Pipeline deckt Build, Unit Tests, Integrationstests und einen Playwright E2E Smoke-Test ab. Weitere E2E Tests werden schrittweise ergänzt.

## Testdaten

Testdaten sollen klein, eindeutig und reproduzierbar sein.

Beispiele:

- Mineral mit Koordinaten
- Mineral ohne Koordinaten
- Mineral mit Bild
- Mineral ohne Bild
- neues Mineral mit `Id = 0`
- Mineralnummern `1`, `2`, `10` fur numerische Sortierung


