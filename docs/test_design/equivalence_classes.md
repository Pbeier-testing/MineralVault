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
**Requirement:** R10.5  
**Regel:** Fundjahr darf leer sein. Wenn ein Wert angegeben ist, muss er größer als 1800 und kleiner oder gleich dem aktuellen Kalenderjahr sein.

Für das aktuelle Testdesign gilt als Bezugsjahr `2026`.

| Klasse | Wertebereich / Beispiel | Gültigkeit | Repräsentative Werte | Abdeckung |
| --- | --- | --- | --- | --- |
| leer | `null` | gültig | `null` | UTC-VAL-006 |
| unterhalb Untergrenze | kleiner `1800` | ungültig | `1799` | UTC-VAL-007 |
| Untergrenze | exakt `1800` | ungültig | `1800` | UTC-VAL-007 |
| erster gültiger Wert | exakt `1801` | gültig | `1801` | UTC-VAL-006 |
| gültiger Innenbereich | `1801` bis aktuelles Kalenderjahr | gültig | `1984`, `2020` | UTC-VAL-006 |
| aktuelle Obergrenze | aktuelles Kalenderjahr | gültig | `2026` | UTC-VAL-006 |
| Zukunft | größer als aktuelles Kalenderjahr | ungültig | `2027` | UTC-VAL-007 |
| nicht numerisch | z. B. Texteingabe | ungültig | `abc` | UI/E2E offen |

## Erwerbsjahr

**Feld:** `Erwerbsjahr`  
**Requirement:** R10.6  
**Regel:** Erwerbsjahr darf leer sein. Wenn ein Wert angegeben ist, muss er größer als 1800 und kleiner oder gleich dem aktuellen Kalenderjahr sein.

Für das aktuelle Testdesign gilt als Bezugsjahr `2026`.

| Klasse | Wertebereich / Beispiel | Gültigkeit | Repräsentative Werte | Abdeckung |
| --- | --- | --- | --- | --- |
| leer | `null` | gültig | `null` | UTC-VAL-008 |
| unterhalb Untergrenze | kleiner `1800` | ungültig | `1799` | UTC-VAL-009 |
| Untergrenze | exakt `1800` | ungültig | `1800` | UTC-VAL-009 |
| erster gültiger Wert | exakt `1801` | gültig | `1801` | UTC-VAL-008 |
| gültiger Innenbereich | `1801` bis aktuelles Kalenderjahr | gültig | `1984`, `2020` | UTC-VAL-008 |
| aktuelle Obergrenze | aktuelles Kalenderjahr | gültig | `2026` | UTC-VAL-008 |
| Zukunft | größer als aktuelles Kalenderjahr | ungültig | `2027` | UTC-VAL-009 |
| nicht numerisch | z. B. Texteingabe | ungültig | `abc` | UI/E2E offen |
