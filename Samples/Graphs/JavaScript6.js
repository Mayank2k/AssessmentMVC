google.setOnLoadCallback(drawChart);

function AddNamespaceHandler(){
    var svg = jQuery('#chart_div svg');
    svg.attr("xmlns", "http://www.w3.org/2000/svg");
    svg.css('overflow','visible');
}
function drawChart() {
    var data = google.visualization.arrayToDataTable([
      ['Year', 'Sales', 'Expenses'],
      ['2009',  1000,      600],
      ['2010',  1170,      1460],
      ['2011',  1660,       1220],
      ['2012',  1030,      540],
      ['2013',  1000,      400],
      ['2014',  1170,      460],
      ['2015',  660,       1120],
      ['2016',  1030,      540]
    ]);

    var options = {
        title: 'Company Performance',
        hAxis: {title: 'Year',  titleTextStyle: {color: '#333'}},
        vAxis: {minValue: 0}
    };

    var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));
    google.visualization.events.addListener(chart, 'ready', AddNamespaceHandler); 
    chart.draw(data, options);
}

function PDFClick() {
    xepOnline.Formatter.Format('JSFiddle', { cssStyle:[{align:'center'}], render: 'none', srctype: 'svg' });
    //xepOnline.Formatter.Format('JSFiddle', { pageWidth:'216mm', pageHeight:'279mm', render: 'none', srctype: 'svg' });
    //var nameSave = "Eaxmple-Form";
    return xepOnline.Formatter.Format('JSFiddle', { pageWidth:'216mm', pageHeight:'279mm', render: 'download', filename: 'name', mimeType: 'application/pdf' });
    
}
//function PNGClick() {
//    return xepOnline.Formatter.Format('JSFiddle', {render:'newwin', mimeType:'image/png', resolution:'120', srctype:'svg'});
//}
//function JPGClick() {
//    return xepOnline.Formatter.Format('JSFiddle', { render: 'newwin', mimeType: 'image/jpg', resolution: '300', srctype: 'svg' });
//}

//var click="return xepOnline.Formatter.Format('JSFiddle', {render:'download', srctype:'svg'})";
//jQuery('#buttons').append('<button onclick="'+ click +'">PDF</button>');

//click="return xepOnline.Formatter.Format('JSFiddle', {render:'newwin', mimeType:'image/png', resolution:'120', srctype:'svg'})";
//jQuery('#buttons').append('<button onclick="'+ click +'">PNG @120dpi</button>');

//click = "return xepOnline.Formatter.Format('JSFiddle', {render:'newwin', mimeType:'image/jpg', resolution:'300', srctype:'svg'})";
//jQuery('#buttons').append('<button onclick="'+ click +'">JPG @300dpi</button>');
