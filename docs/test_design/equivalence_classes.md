# Äquivalenzklassenanalyse

Diese Analyse dokumentiert ausgewählte Eingabewerte der MineralVault App nach gültigen und ungültigen Äquivalenzklassen. Sie dient als Testentwurfsgrundlage für Unit Tests, Integrationstests, E2E Tests und mögliche Ergänzungen der Requirements.

## Ziel

Äquivalenzklassen helfen dabei, Eingabewerte systematisch zu gruppieren. Statt alle möglichen Werte zu testen, wird je Klasse mindestens ein repräsentativer Wert geprüft. Grenzwerte werden zusätzlich betrachtet, weil Fehler häufig an Bereichsgrenzen auftreten.

## Breitengrad

**Feld:** `Breitengrad`  
**Requirement:** R10.2  
**Regel:** Falls ein Breitengrad angegeben ist, muss er zwischen -90 und 90 liegen. Leere Werte sind erlaubt.

| Klasse | Wertebereich / Beispiel | Gültigkeit | Repräsentative Werte | Abdeckung |
| --- | --- | --- | --- | --- |
| leer | `null` | gültig | `null` | UTC-VAL-002 |
| südliche Grenze | exakt `-90` | gültig | `-90` | UTC-VAL-002 |
| nördliche Grenze | exakt `90` | gültig | `90` | UTC-VAL-002 |
| gültiger Innenbereich | größer `-90` und kleiner `90` | gültig | `0`, `51.1657` | offen |
| unterhalb Minimum | kleiner `-90` | ungültig | `-90.1` | UTC-VAL-003 |
| oberhalb Maximum | größer `90` | ungültig | `90.1` | UTC-VAL-003 |
| nicht numerisch | z. B. Texteingabe | ungültig | `abc` | UI/E2E offen |

## Längengrad

**Feld:** `Laengengrad`  
**Requirement:** R10.3  
**Regel:** Falls ein Längengrad angegeben ist, muss er zwischen -180 und 180 liegen. Leere Werte sind erlaubt.

| Klasse | Wertebereich / Beispiel | Gültigkeit | Repräsentative Werte | Abdeckung |
| --- | --- | --- | --- | --- |
| leer | `null` | gültig | `null` | UTC-VAL-004 |
| westliche Grenze | exakt `-180` | gültig | `-180` | UTC-VAL-004 |
| östliche Grenze | exakt `180` | gültig | `180` | UTC-VAL-004 |
| gültiger Innenbereich | größer `-180` und kleiner `180` | gültig | `0`, `10.4515` | offen |
| unterhalb Minimum | kleiner `-180` | ungültig | `-180.1` | UTC-VAL-005 |
| oberhalb Maximum | größer `180` | ungültig | `180.1` | UTC-VAL-005 |
| nicht numerisch | z. B. Texteingabe | ungültig | `abc` | UI/E2E offen |

## Fundjahr

**Feld:** `Fundjahr`  
**Requirement:** noch nicht explizit definiert  
**Vorgeschlagene Regel:** Fundjahr darf leer sein. Wenn ein Wert angegeben ist, muss er größer als 1800 und kleiner oder gleich dem aktuellen Kalenderjahr sein.

Für das aktuelle Testdesign gilt als Bezugsjahr `2026`.

| Klasse | Wertebereich / Beispiel | Gültigkeit | Repräsentative Werte | Abdeckung |
| --- | --- | --- | --- | --- |
| leer | `null` | gültig | `null` | offen |
| unterhalb Untergrenze | kleiner `1800` | ungültig | `1799` | offen |
| Untergrenze | exakt `1800` | ungültig | `1800` | offen |
| erster gültiger Wert | exakt `1801` | gültig | `1801` | offen |
| gültiger Innenbereich | `1801` bis aktuelles Kalenderjahr | gültig | `1984`, `2020` | offen |
| aktuelle Obergrenze | aktuelles Kalenderjahr | gültig | `2026` | offen |
| Zukunft | größer als aktuelles Kalenderjahr | ungültig | `2027` | offen |
| nicht numerisch | z. B. Texteingabe | ungültig | `abc` | UI/E2E offen |

## Erwerbsjahr

**Feld:** `Erwerbsjahr`  
**Requirement:** noch nicht explizit definiert  
**Vorgeschlagene Regel:** Erwerbsjahr darf leer sein. Wenn ein Wert angegeben ist, muss er größer als 1800 und kleiner oder gleich dem aktuellen Kalenderjahr sein.

Für das aktuelle Testdesign gilt als Bezugsjahr `2026`.

| Klasse | Wertebereich / Beispiel | Gültigkeit | Repräsentative Werte | Abdeckung |
| --- | --- | --- | --- | --- |
| leer | `null` | gültig | `null` | offen |
| unterhalb Untergrenze | kleiner `1800` | ungültig | `1799` | offen |
| Untergrenze | exakt `1800` | ungültig | `1800` | offen |
| erster gültiger Wert | exakt `1801` | gültig | `1801` | offen |
| gültiger Innenbereich | `1801` bis aktuelles Kalenderjahr | gültig | `1984`, `2020` | offen |
| aktuelle Obergrenze | aktuelles Kalenderjahr | gültig | `2026` | offen |
| Zukunft | größer als aktuelles Kalenderjahr | ungültig | `2027` | offen |
| nicht numerisch | z. B. Texteingabe | ungültig | `abc` | UI/E2E offen |

