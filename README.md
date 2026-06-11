# MineralVault

[![CI](https://github.com/Pbeier-testing/MineralVault/actions/workflows/pipeline.yml/badge.svg)](https://github.com/Pbeier-testing/MineralVault/actions)
![.NET](https://img.shields.io/badge/.NET-10.0-blue.svg)
![License](https://img.shields.io/badge/license-MIT-green.svg)

MineralVault ist eine Fullstack-Anwendung zur Verwaltung einer privaten Mineraliensammlung. Die App kombiniert eine interaktive Kartenansicht, eine tabellarische Verwaltungsansicht, Bildverwaltung, Koordinatenpflege und eine ASP.NET Core Web API mit SQLite-Datenbank.

Das Projekt dient gleichzeitig als Portfolio-Projekt, um praktische Fullstack-Entwicklung mit einem strukturierten, ISTQB-orientierten Testprozess zu verbinden. Ziel ist nicht nur eine funktionierende Anwendung, sondern auch nachvollziehbare Qualitätssicherung durch Requirements, Teststrategie, Testfälle, Testmatrix, automatisierte Tests und CI/CD.

---

## Funktionsumfang

- Kartenansicht mit Mineral-Markern auf Basis gespeicherter Koordinaten
- Marker-Clustering beim Herauszoomen und Auflösen der Cluster beim Hineinzoomen
- Popups mit Bild, Mineralname und Fundort
- Explorer-Liste neben der Karte mit Bild, Name, Fundort und Bemerkungen
- Suche und Sortierung der Mineralien
- Tabellenansicht zur Verwaltung der Sammlung
- Inline-Bearbeitung von Mineral-Daten
- Bild-Upload und Bildvorschau
- Koordinatenpflege per Eingabefeld und Karten-Popup
- Anlegen neuer Mineralien
- Löschen mit Bestätigungsdialog
- CSV-Import für bestehende Sammlungsdaten

---

## Testprozess

Ein zentraler Bestandteil des Projekts ist der Aufbau eines nachvollziehbaren Testprozesses. Die Testdokumentation liegt im Ordner [docs](docs/).

Aktuell vorhanden:

- [Requirements](docs/requirements.md)
- [Teststrategie](docs/test_strategy.md)
- [Testmatrix](docs/test_matrix.md)
- [Testfälle](docs/test_cases/README.md)
- Unit Tests mit xUnit
- Integrationstests mit xUnit und Testdatenbank
- Playwright E2E Smoke-Test
- GitHub Actions Pipeline für Build und Tests

Die Testmatrix verknüpft Requirements mit Testfällen, Testlevel, Automatisierung und Status. Dadurch wird sichtbar, welche Anforderungen bereits abgedeckt sind, welche Tests noch offen sind und wo Tests bewusst Soll/Ist-Abweichungen dokumentieren.

Verwendete Testlevel:

- **Unit Tests:** Domain-Validierung, Such- und Sortierlogik, ViewModel-Verhalten
- **Integration Tests:** API, SQLite-Testdatenbank, Bilddateien, CSV-Import
- **E2E Tests:** zentrale UI-Flows mit Playwright
- **Manuelle Tests:** visuelle Prüfung, Bedienbarkeit, responsive Darstellung

Hinweis: Einige Unit Tests sind aktuell bewusst rot, weil sie gewünschtes Soll-Verhalten beschreiben, das noch nicht vollständig implementiert ist.

---

## Technologie

**Backend**

- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger/OpenAPI

**Frontend**

- Blazor WebAssembly
- Leaflet
- Bootstrap

**Testing**

- xUnit
- Test Doubles für HTTP, JS Interop und Datei-Uploads
- Playwright E2E Smoke-Test

**CI/CD**

- GitHub Actions
- Restore, Build, Unit Tests, Integrationstests und E2E Smoke-Test

---

## Projektstruktur

```text
src/
  MineralCollection.Domain      Domain-Modelle und Validierung
  MineralCollection.API         REST API, EF Core, SQLite, Bild- und CSV-Funktionen
  MineralCollection.Frontend    Blazor WebAssembly UI

tests/
  MineralCollection.Tests.Unit  Unit Tests
  MineralCollection.Tests.Integration  Integration Tests
  MineralCollection.Tests.E2E   Playwright E2E Tests

docs/
  requirements.md               Anforderungen
  test_strategy.md              Teststrategie
  test_matrix.md                Traceability-Matrix
  test_cases/                   Testfälle nach Testlevel
```

---

## Lokales Setup

### Voraussetzungen

- .NET 10 SDK
- EF Core Tools

```bash
dotnet tool install --global dotnet-ef
```

### Datenbank vorbereiten

```bash
dotnet ef database update --project src/MineralCollection.API
```

### API starten

```bash
dotnet run --project src/MineralCollection.API
```

Swagger ist anschließend unter der im Terminal angezeigten URL erreichbar, z. B.:

```text
http://localhost:5247/swagger
```

### Frontend starten

```bash
dotnet run --project src/MineralCollection.Frontend
```

Das Frontend ist aktuell auf die API-Adresse `http://localhost:5247` ausgelegt.

Für reproduzierbare E2E-Tests kann die API mit einer separaten Testdatenbank gestartet werden:

```bash
$env:MINERALVAULT_DB_PATH="tests/MineralCollection.Tests.E2E/TestData/minerals.e2e.db"
dotnet run --project src/MineralCollection.API
```

---

## Tests ausführen

Alle Tests:

```bash
dotnet test
```

Nur Unit Tests über Traits:

```bash
dotnet test --filter "TestLevel=Unit"
```

Einzelnes Requirement testen:

```bash
dotnet test --filter "Requirement=R4.13"
```

Einzelnen Test Case testen:

```bash
dotnet test --filter "TestCase=UTC-SORT-003"
```

E2E gegen eine laufende lokale App starten:

```bash
dotnet test tests/MineralCollection.Tests.E2E/MineralCollection.Tests.E2E.csproj --filter "TestLevel=E2E"
```

Falls das Frontend nicht unter `http://localhost:5119` läuft, kann die URL überschrieben werden:

```bash
$env:MINERALVAULT_E2E_BASE_URL="http://localhost:5119"
dotnet test tests/MineralCollection.Tests.E2E/MineralCollection.Tests.E2E.csproj --filter "TestLevel=E2E"
```

---

## Roadmap

- [x] ASP.NET Core API mit SQLite
- [x] Blazor WebAssembly Frontend
- [x] Kartenansicht mit Leaflet
- [x] Tabellenansicht mit Inline-Bearbeitung
- [x] Bildverwaltung
- [x] Requirements und Teststrategie
- [x] Testmatrix und neue Testcase-Struktur
- [x] erste neue Unit Tests mit Traceability-Traits
- [x] Integrationstestprojekt mit API/DB/Bilddatei-Tests
- [x] Playwright E2E-Testprojekt
- [x] CI Pipeline um Integration und E2E erweitert
- [ ] bekannte Soll/Ist-Abweichungen beheben

---

## Ziel des Projekts

Das Projekt soll zeigen, wie eine fachliche Anwendung schrittweise entwickelt und gleichzeitig testbar aufgebaut werden kann. Der Fokus liegt auf sauberer Struktur, nachvollziehbaren Anforderungen, automatisierbaren Tests und transparenter Weiterentwicklung.
