# Manual Test Cases

Manuelle Tests decken visuelle, responsive und explorative Aspekte ab, die nur begrenzt sinnvoll automatisierbar sind.

## MTC-MAP-001 - Marker-Clustering visuell prüfen

**Related Requirements:** R2.2, R2.3  
**Status:** Manual  
**Test Level:** Manual  

**Steps:**
1. Öffne die Kartenansicht mit mehreren Mineralien in nahen Regionen.
2. Zoome heraus.
3. Zoome wieder hinein.

**Expected Result:**
- Marker werden beim Herauszoomen zu Clustern zusammengefasst.
- Cluster werden beim Hineinzoomen wieder aufgelöst.

## MTC-UI-001 - Scrollbare Listen prüfen

**Related Requirements:** R3.1, R11.1  
**Status:** Manual  
**Test Level:** Manual  

**Steps:**
1. Öffne eine Sammlung mit vielen Mineralien.
2. Scrolle die Explorer-Liste.
3. Scrolle die Tabellenansicht.

**Expected Result:**
- Listen bleiben bedienbar.
- Die Anwendung bleibt stabil und übersichtlich.

## MTC-UI-002 - Bedienbarkeit per Maus prüfen

**Related Requirements:** R11.2  
**Status:** Manual  
**Test Level:** Manual  

**Expected Result:**
- Navigation, Suche, Sortierung, Bildauswahl, Koordinatenpicker, Speichern und Löschen sind per Maus bedienbar.

## MTC-UI-003 - Responsive Darstellung prüfen

**Related Requirements:** R11.1, R11.2  
**Status:** Manual  
**Test Level:** Manual  

**Steps:**
1. Prüfe die App auf Desktop-Breite.
2. Prüfe die App auf Tablet-Breite.
3. Prüfe die App auf schmaler Breite.

**Expected Result:**
- Die Hauptfunktionen bleiben erreichbar.
- Inhalte überlappen nicht unkontrolliert.

