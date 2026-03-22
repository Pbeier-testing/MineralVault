var map;
var markers = {};

window.mapBox = {
    // 1. Karte initialisieren
    initialize: function (lat, lon, zoom) {
        map = L.map('map').setView([lat, lon], zoom);
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '© OpenStreetMap contributors'
        }).addTo(map);
    },

    // 2. Pins setzen
    addMarkers: function (minerals) {
        // Alte Marker entfernen, falls vorhanden
        for (var id in markers) { map.removeLayer(markers[id]); }
        markers = {};

        minerals.forEach(m => {
            if (m.laengengrad && m.breitengrad) {
                var marker = L.marker([m.breitengrad, m.laengengrad])
                    .addTo(map)
                    .bindPopup(`<b>${m.name}</b><br>${m.fundort}`);

                markers[m.id] = marker;
            }
        });
    },

    // 3. Zu einem Pin springen (für das Hover-Feature)
    focusMineral: function (id) {
        var marker = markers[id];
        if (marker) {
            marker.openPopup();
            map.panTo(marker.getLatLng());
        }
    }
};