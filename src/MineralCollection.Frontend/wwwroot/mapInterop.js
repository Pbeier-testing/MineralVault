var map;
var markers = {};

window.mapBox = {
    initialize: function (lat, lon, zoom) {
        map = L.map('map', { zoomControl: false }).setView([lat, lon], zoom);
        L.tileLayer('https://{s}.basemaps.cartocdn.com/light_all/{z}/{x}/{y}{r}.png', {
            attribution: '©OpenStreetMap'
        }).addTo(map);

        // Cluster-Gruppe initialisieren
        markerClusterGroup = L.markerClusterGroup();
        map.addLayer(markerClusterGroup);

        L.control.zoom({ position: 'bottomright' }).addTo(map);
    },

    addMarkers: function (minerals) {
        markerClusterGroup.clearLayers();
        markers = {};

        minerals.forEach(m => {
            if (m.breitengrad && m.laengengrad) {
                const imgPath = m.images && m.images.length > 0 ? `images/${m.images[0].fileName}` : 'placeholder.jpg';

                // Custom Icon mit Hintergrundbild
                var customIcon = L.divIcon({
                    html: `<div class="marker-pin" style="background-image: url('${imgPath}')"></div>`,
                    className: 'custom-div-icon',
                    iconSize: [40, 40],
                    iconAnchor: [20, 20],
                    popupAnchor: [0, -20]
                });

                var marker = L.marker([m.breitengrad, m.laengengrad], { icon: customIcon });

                // Popup-Inhalt mit Bild und Text
                const location = `${m.region ? m.region + ', ' : ''}${m.land || ''}`;
                const popupHtml = `
                    <div class="custom-popup-content">
                        <img src="${imgPath}" class="popup-img">
                        <div class="popup-text">
                            <h6 class="popup-title">${m.name}</h6>
                            <p class="popup-sub">${location}</p>
                        </div>
                    </div>`;

                marker.bindPopup(popupHtml, { closeButton: false, className: 'clean-popup' });

                // Marker zur Gruppe hinzufügen
                markerClusterGroup.addLayer(marker);
                markers[m.id] = marker;
            }
        });
    },

    focusMineral: function (id) {
        var marker = markers[id];
        if (marker) {
            // Highlight Logik: Vorherige Highlights entfernen
            document.querySelectorAll('.marker-pin').forEach(el => el.classList.remove('active-pin'));

            // Marker im Cluster finden und hinzoomen
            markerClusterGroup.zoomToShowLayer(marker, function () {
                marker.openPopup();
                // Das HTML-Element des Icons stylen
                const iconElement = marker.getElement()?.querySelector('.marker-pin');
                if (iconElement) iconElement.classList.add('active-pin');
            });
        }
    },

    fixSize: function () {
        if (map) {
            setTimeout(() => {
                map.invalidateSize();
            }, 200);
        }
    }
};