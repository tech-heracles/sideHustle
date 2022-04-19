function drawChart() {
    var hartaType = hfState.Get('type');
    var koordinatat = hfState.Get('geom');
    var etiketa = hfState.Get('names');
    var raporti = hfState.Get('raporti');

    //var data = google.visualization.arrayToDataTable([
    //   ['City', 'Population', 'Area'],
    //   ['Tiranë', 2761477, 1285.31],
    //   ['Berat', 1324110, 181.76],
    //   ['Korce', 959574, 117.27],
    //   ['Durres', 907563, 130.17],
    //   ['Vlore', 655875, 158.9],
    //   ['Mirdite', 607906, 243.60],
    //   ['Shkoder', 380181, 140.7],
    //   ['Sarande', 371282, 102.41],
    //   ['Elbasan', 67370, 213.44],
    //   ['Kukes', 52192, 43.43],
    //   ['Diber', 38262, 11]
    //]);

    var data;
    var googleData;
    if (koordinatat) {
        koordinatat = koordinatat.replace(/\(|\)/g, "").replace(/POINT /g, '');
        koordinatat = JSON.parse(koordinatat);
        etiketa = JSON.parse(etiketa);
        googleData = [['Lat', 'Long']];//, 'pershkrimi', 'sasia e shitur', 'vlefta']];
        for (var key in etiketa[0]) {
            googleData[0].push(key.replace(/_/g, ' '));
        }
        for (var i = 0; i < koordinatat.length; i++) {
            koordinata = koordinatat[i];
            if (!koordinata) {
                continue;
            }
            koordinata = koordinata.split(' ');
            var arr = [parseFloat(koordinata[1]), parseFloat(koordinata[0])];
            for (var key in etiketa[0]) {
                arr.push(etiketa[i][key]);
            }
            googleData.push(arr);
            // googleData.push([parseFloat(koordinata[1]), parseFloat(koordinata[0]), etiketa[i]["pershkrimi"], etiketa[i]["sasia_e_shitur"], etiketa[i]["vlefta"]]);
        }
        data = google.visualization.arrayToDataTable(googleData);
    }

    var options = function () {
        switch (hartaType) {
            case 'geochart': return { region: 'AL', displayMode: 'markers', legend: { textStyle: { color: 'blue', fontSize: 16 } }, colorAxis: { colors: ['#e7711c', '#4374e0'] } };
            case 'map':
                switch (raporti) {
                    case 'klientKoordinata': return { showTooltip: true, showInfoWindow: true };
                    default: return { showTip: true, mapType: 'normal' };
                }
            default: alert('tipi i panjohur harte: ' + hartaType);
        }
    }();

    var infowindow = new google.maps.InfoWindow();
    var map = function () {
        switch (hartaType) {
            case 'geochart': return new google.visualization.GeoChart(document.getElementById('map'));
            case 'map':
                switch (raporti) {
                    case 'klientKoordinata':
                        return new google.maps.Map(document.getElementById('map'), {
                            zoom: 8,
                            center: { lat: 41.325, lng: 20.070 },
                            mapTypeId: 'hybrid'
                        });
                    default:
                        return new google.visualization.Map(document.getElementById('map'));
                }
            default: alert('tipi i panjohur harte: ' + hartaType);
        }
    }();

    if (data) {
        if (raporti == 'klientKoordinata') {
            for (var i = 1; i < googleData.length; i++) {

                var marker = new google.maps.Marker({
                    map: map,
                    position: { lat: googleData[i][0], lng: googleData[i][1] },
                    title: googleData[i][2],
                });

                google.maps.event.addListener(marker, 'click', (function (marker, i) {
                    return function () {
                        infowindow.setContent(marker.title);
                        infowindow.open(map, marker);
                    }
                })(marker, i));
            }
        }
        else
            map.draw(data, options);
    }
    else {
        //if (hartaType=="geochart")  lblMsgbox.SetText('Nuk ka pika ne chart');
        //else if (hartaType == "map")
        lblMsgbox.SetText('Nuk ka te dhena');
        popmsg.SetSize(100, 100);
        popmsg.Show();
    }
}

$(document).on('ready', function () {
    var hartaType = hfState.Get('type');
    google.charts.load('current', { packages: [hartaType] });
    google.charts.setOnLoadCallback(drawChart);
});