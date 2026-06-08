# Test Cases - MineralCollection App

Diese Testcase-Sammlung basiert auf den aktuellen Requirements und der `docs/test_matrix.md`.

## Struktur

- `unit_test_cases.md`: schnelle Tests für Domain- und ViewModel-Logik
- `integration_test_cases.md`: API, Datenbank, Dateisystem und CSV-Import
- `e2e_test_cases.md`: zentrale Benutzerflüsse im Browser mit Playwright
- `manual_test_cases.md`: visuelle, responsive und explorative Prüfungen

## Statuswerte

- `Draft`: Testfall ist formuliert, aber noch nicht umgesetzt
- `Automated`: Testfall ist automatisiert
- `Failing`: Testfall ist automatisiert, aber die Anwendung erfüllt das Soll-Verhalten noch nicht
- `Manual`: Testfall ist als manueller Test vorgesehen

## Namensschema

- Unit: `UTC-<Bereich>-<Nummer>`
- Integration: `ITC-<Bereich>-<Nummer>`
- E2E: `E2E-<Bereich>-<Nummer>`
- Manual: `MTC-<Bereich>-<Nummer>`

