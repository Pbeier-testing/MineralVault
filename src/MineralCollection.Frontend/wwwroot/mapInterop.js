// Globale Variablen für die Map-Instanz und die Marker-Verwaltung
var map;
var markerClusterGroup;
var markers = {};

window.mapBox = {
    // 1. Initialisierung der Karte
    initialize: function (lat, lon, zoom) {
        if (map) {
            map.remove();
        }

        map = L.map('map', {
            zoomControl: false,
            maxZoom: 18
        }).setView([lat, lon], zoom);

        L.tileLayer('https://{s}.basemaps.cartocdn.com/light_all/{z}/{x}/{y}{r}.png', {
            attribution: '©OpenStreetMap'
        }).addTo(map);

        // Cluster-Gruppe initialisieren und der Karte hinzufügen
        markerClusterGroup = L.markerClusterGroup({
            showCoverageOnHover: false,
            zoomToBoundsOnClick: true
        });
        map.addLayer(markerClusterGroup);

        L.control.zoom({ position: 'bottomright' }).addTo(map);

        console.log("Karte erfolgreich initialisiert.");
    },

    // 2. Marker hinzufügen oder aktualisieren
    addMarkers: function (minerals) {
        if (!markerClusterGroup) {
            console.error("ClusterGroup nicht bereit!");
            return;
        }

        // Bestehende Marker löschen
        markerClusterGroup.clearLayers();
        markers = {};

        if (!minerals || minerals.length === 0) return;

        minerals.forEach(m => {
            const lat = m.breitengrad || m.latitude;
            const lon = m.laengengrad || m.longitude;

            if (lat && lon) {
                const imgPath = m.images && m.images.length > 0
                    ? `images/${m.images[0].fileName}`
                    : 'placeholder.jpg';

                // Custom Icon mit Hintergrundbild
                var customIcon = L.divIcon({
                    html: `<div class="marker-pin" style="background-image: url('${imgPath}')"></div>`,
                    className: 'custom-div-icon',
                    iconSize: [40, 40],
                    iconAnchor: [20, 20],
                    popupAnchor: [0, -20]
                });

                var marker = L.marker([lat, lon], { icon: customIcon });

                // Popup-Inhalt mit Bild und Text
                const location = `${m.region ? m.region + ', ' : ''}${m.land || ''}`;
                const popupHtml = `
                    <div class="custom-popup-content">
                        <img src="${imgPath}" class="popup-img" onerror="this.src='placeholder.jpg'">
                        <div class="popup-text">
                            <h6 style="margin:0; font-weight:bold;">${m.name}</h6>
                            <p style="margin:0; font-size:0.8rem; color:#666;">${location}</p>
                        </div>
                    </div>`;

                marker.bindPopup(popupHtml, {
                    closeButton: false,
                    className: 'clean-popup'
                });

                // Marker in die Liste und die Clustergruppe aufnehmen
                markerClusterGroup.addLayer(marker);
                markers[m.id] = marker;
            }
        });

        console.log(`${Object.keys(markers).length} Marker zur Karte hinzugefügt.`);
    },

    // 3. Zoom auf ein bestimmtes Mineral
    focusMineral: function (id) {
        var marker = markers[id];
        if (marker) {
            // Alle alten Highlights in der UI entfernen
            document.querySelectorAll('.marker-pin').forEach(el => el.classList.remove('active-pin'));

            // Zoomt zum Marker, auch wenn er versteckt im Cluster ist
            markerClusterGroup.zoomToShowLayer(marker, function () {
                marker.openPopup();

                // Pin visuell hervorheben
                const iconElement = marker.getElement()?.querySelector('.marker-pin');
                if (iconElement) {
                    iconElement.classList.add('active-pin');
                }
            });
        }
    },

    // 4. Container-Größe korrigieren
    fixSize: function () {
        if (map) {
            setTimeout(() => {
                map.invalidateSize();
            }, 250);
        }
    }
};