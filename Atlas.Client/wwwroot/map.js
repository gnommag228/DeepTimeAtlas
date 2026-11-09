window.loadMap = (sites) => {
    var map = L.map('map').setView([30.0, 30.0], 3);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '© OpenStreetMap',
        maxZoom: 18
    }).addTo(map);

    sites.forEach(site => {
        var marker = L.marker([site.latitude, site.longitude]).addTo(map);

        
        var tooltipContent = `
            <div style="text-align: center;">
                <strong style="font-size: 14px; color: #0d6efd;">${site.name}</strong><br/>
                <span style="font-size: 12px; color: #6c757d;">${site.age}</span>
            </div>
        `;
        marker.bindTooltip(tooltipContent, { direction: 'top', offset: [0, -35] });

        
        marker.on('click', function () {
            window.location.href = '/site/' + site.id;
        });
    });
};