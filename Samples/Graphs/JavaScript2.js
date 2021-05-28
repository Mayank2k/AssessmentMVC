google.setOnLoadCallback(drawDashboard);

function drawDashboard() {

    // Create our data table.
    var data = google.visualization.arrayToDataTable([
      ['Name', 'Donuts eaten'],
      ['Michael', 5],
      ['Elisa', 7],
      ['Robert', 3],
      ['John', 2],
      ['Jessica', 6],
      ['Aaron', 1],
      ['Margareth', 8]
    ]);



    // Create a dashboard.
    var dashboard = new google.visualization.Dashboard(
        document.getElementById('dashboard_div'));

    // Create a range slider, passing some options
    var select = new google.visualization.ControlWrapper({
        'controlType': 'CategoryFilter',
        'containerId': 'filter_div',
        'options': {
            'filterColumnLabel': 'Donuts eaten'
        }
    });

    // Create a pie chart, passing some options
    var chart = new google.visualization.ChartWrapper({
        'chartType': 'ColumnChart',
        'containerId': 'chart_div',
        'options': {
            'width': 500,
            'height': 300,
            'legend': 'right'
        }
    });

    google.visualization.events.addListener(chart, 'ready', function () {
        console.log(chart.getChart().getImageURI());
        document.getElementById('png').innerHTML = '<a href="' + chart.getChart().getImageURI() + '">Printable version</a>';
    });

    // Establish dependencies, declaring that 'filter' drives 'pieChart',
    // so that the pie chart will only display entries that are let through
    // given the chosen slider range.
    dashboard.bind(select, chart);

    // Draw the dashboard.
    dashboard.draw(data);
}