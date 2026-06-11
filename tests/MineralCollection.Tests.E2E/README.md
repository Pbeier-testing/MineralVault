# MineralCollection E2E Tests

Dieses Projekt enthält Playwright-basierte End-to-End-Tests.

Der Smoke-Test wird lokal gegen eine laufende Anwendung ausgeführt und ist in der CI Pipeline eingebunden.

## Voraussetzungen

1. API mit E2E-Testdatenbank starten:

   ```bash
   $env:MINERALVAULT_DB_PATH="tests/MineralCollection.Tests.E2E/TestData/minerals.e2e.db"
   dotnet run --project src/MineralCollection.API
   ```

2. Frontend starten:

   ```bash
   dotnet run --project src/MineralCollection.Frontend
   ```

3. Playwright Browser installieren, falls noch nicht vorhanden:

   ```bash
   pwsh tests/MineralCollection.Tests.E2E/bin/Debug/net10.0/playwright.ps1 install
   ```

4. E2E Tests ausführen:

   ```bash
   dotnet test tests/MineralCollection.Tests.E2E/MineralCollection.Tests.E2E.csproj --filter "TestLevel=E2E"
   ```

Standardmäßig verwendet der Smoke-Test `http://localhost:5119`. Falls die Anwendung auf einer anderen URL läuft, kann sie ueber `MINERALVAULT_E2E_BASE_URL` überschrieben werden:

```bash
$env:MINERALVAULT_E2E_BASE_URL="http://localhost:5119"
dotnet test tests/MineralCollection.Tests.E2E/MineralCollection.Tests.E2E.csproj --filter "TestLevel=E2E"
```
