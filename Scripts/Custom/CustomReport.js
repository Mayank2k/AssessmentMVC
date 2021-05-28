google.load("visualization", "1", { packages: ["corechart"] });

$(document).ready(function () {
    var chartData1;
    var chartData2;
    var chartData3;
    var chartData4;
    var chartData5;
  
    google.setOnLoadCallback(drawChartsGetOverAllScores);
    google.setOnLoadCallback(drawChartsGetSubjectWiseScores);
    google.setOnLoadCallback(drawChartsGetCompetencyWiseScores);
    google.setOnLoadCallback(drawChartsGetDifficultyLevelWiseScores);

    function formatDiv(divName) {
        var svg = $(divName);
        svg.attr("xmlns", "http://www.w3.org/2000/svg");
        //svg.css('position', 'fixed');
        //svg.css('width', 'auto');
    }

    function AddNamespaceHandler() {
        formatDiv('#chart1 svg')
        formatDiv('#chart2 svg')
        formatDiv('#chart3 svg')
        formatDiv('#chartS1 svg')
        formatDiv('#chartS2 svg')
        formatDiv('#chartS3 svg')
        formatDiv('#chartC1 svg')
        formatDiv('#chartC2 svg')
        formatDiv('#chartC3 svg')
        formatDiv('#chartC4 svg')
        formatDiv('#chartC5 svg')
        formatDiv('#chartD1 svg')
        formatDiv('#chartD2 svg')
        formatDiv('#chartD3 svg')
    }

    function drawChartsGetOverAllScores() {
        $.ajax({
            url: $("#myUrl").val() + "GetOverAllScores",
            type: "GET",
            data: { userId: $('#userId').val() },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                chartData1 = data.FirstList;
                chartData2 = data.SecondList;
                chartData3 = data.ThirdList;

                //First Chart
                var data = new google.visualization.DataTable();
                //Adding columns to data table to insert the chart data
                data.addColumn('string', 'User');
                data.addColumn('number', 'Percentage');
                //bind the data to the data table using for loop
                var p = 0;
                for (var i = 0; i < chartData1.length; i++) {
                    data.addRow([chartData1[i].UserId, chartData1[i].Percentage]);
                    p = p + chartData1[i].Percentage;
                }
                data.addRow(['', 100 - p]);

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.PieChart($('#chart1')[0]);
                google.visualization.events.addListener(chart, 'ready', AddNamespaceHandler);
                chart.draw(data,
                      {
                          title: "Subject (" + p + "%)", //title of the pie chart
                          position: "top", fontsize: "14px", pieHole: 0.8, legend: 'none', chma: '1,1,1,1',
                          chartArea: { width: '90%' }, pieSliceText: "none"
                      });

                ////Second Chart
                var data = new google.visualization.DataTable();
                //Adding columns to data table to insert the chart data
                data.addColumn('string', 'User');
                data.addColumn('number', 'Percentage');
                //bind the data to the data table using for loop
                var p = 0;
                for (var i = 0; i < chartData2.length; i++) {
                    data.addRow([chartData2[i].UserId, chartData2[i].Percentage]);
                    p = p + chartData2[i].Percentage;
                }
                data.addRow(['', 100 - p]);

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.PieChart($('#chart2')[0]);
                google.visualization.events.addListener(chart, 'ready', AddNamespaceHandler);
                chart.draw(data,
                      {
                          title: "Compentency (" + p + "%)", //title of the pie chart
                          position: "top", fontsize: "14px", pieHole: 0.8, legend: 'none', chma: '1,1,1,1',
                          chartArea: { width: '90%' }, pieSliceText: "none"
                      });

                ////Third Chart
                var data = new google.visualization.DataTable();
                //Adding columns to data table to insert the chart data
                data.addColumn('string', 'User');
                data.addColumn('number', 'Percentage');
                //bind the data to the data table using for loop
                var p = 0;
                for (var i = 0; i < chartData3.length; i++) {
                    data.addRow([chartData3[i].UserId, chartData3[i].Percentage]);
                    p = p + chartData3[i].Percentage;
                }
                data.addRow(['', 100 - p]);

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.PieChart($('#chart3')[0]);
                google.visualization.events.addListener(chart, 'ready', AddNamespaceHandler);
                chart.draw(data,
                      {
                          title: "Difficulty Level (" + p + "%)", //title of the pie chart
                          position: "top", fontsize: "14px", pieHole: 0.8, legend: 'none', chma: '1,1,1,1',
                          chartArea: { width: '90%' }, pieSliceText: "none"
                      });
            },
            error: function () {
                alert("Error loading data! Please try again.");
            }
        });
    }
  
    function drawChartsGetSubjectWiseScores() {
        $.ajax({
            url: $("#myUrl").val() + "GetSubjectWiseScores",
            type: "GET",
            data: { userId: $('#userId').val() },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                chartData1 = data.FirstList;
                chartData2 = data.SecondList;
                chartData3 = data.ThirdList;

                //First Chart
                var data = new google.visualization.DataTable();
                //Adding columns to data table to insert the chart data
                data.addColumn('string', 'Subject');
                data.addColumn('number', 'Percentage');
                //bind the data to the data table using for loop
                var subject = 'N/A';
                var p = 0;
                for (var i = 0; i < chartData1.length; i++) {
                    data.addRow([chartData1[i].SubjectName, chartData1[i].Percentage]);
                    subject = chartData1[i].SubjectName;
                    p = p + chartData1[i].Percentage;
                }
                data.addRow(['', 100 - p]);

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.PieChart($('#chartS1')[0]);
                google.visualization.events.addListener(chart, 'ready', AddNamespaceHandler);
                chart.draw(data,
                      {
                          title: subject + " (" + p + "%)", //title of the pie chart
                          position: "top", fontsize: "14px", pieHole: 0.8, legend: 'none', chma: '1,1,1,1',
                          chartArea: { width: '90%' }, pieSliceText: "none"
                      });

                ////Second Chart
                var data = new google.visualization.DataTable();
                //Adding columns to data table to insert the chart data
                data.addColumn('string', 'Subject');
                data.addColumn('number', 'Percentage');
                //bind the data to the data table using for loop
                var subject = 'N/A';
                var p = 0;
                for (var i = 0; i < chartData2.length; i++) {
                    data.addRow([chartData2[i].UserId, chartData2[i].Percentage]);
                    subject = chartData2[i].SubjectName;
                    p = p + chartData2[i].Percentage;
                }
                data.addRow(['', 100 - p]);

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.PieChart($('#chartS2')[0]);
                google.visualization.events.addListener(chart, 'ready', AddNamespaceHandler);
                chart.draw(data,
                      {
                          title: subject + " (" + p + "%)", //title of the pie chart
                          position: "top", fontsize: "14px", pieHole: 0.8, legend: 'none', chma: '1,1,1,1',
                          chartArea: { width: '90%' }, pieSliceText: "none"
                      });

                ////Third Chart
                var data = new google.visualization.DataTable();
                //Adding columns to data table to insert the chart data
                data.addColumn('string', 'Subject');
                data.addColumn('number', 'Percentage');
                //bind the data to the data table using for loop
                var subject = 'N/A';
                var p = 0;
                for (var i = 0; i < chartData3.length; i++) {
                    data.addRow([chartData3[i].UserId, chartData3[i].Percentage]);
                    subject = chartData3[i].SubjectName;
                    p = p + chartData3[i].Percentage;
                }
                data.addRow(['', 100 - p]);

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.PieChart($('#chartS3')[0]);
                google.visualization.events.addListener(chart, 'ready', AddNamespaceHandler);
                chart.draw(data,
                      {
                          title: subject + " (" + p + "%)", //title of the pie chart
                          position: "top", fontsize: "14px", pieHole: 0.8, legend: 'none', chma: '1,1,1,1',
                          chartArea: { width: '90%' }, pieSliceText: "none"
                      });
            },
            error: function () {
                alert("Error loading data! Please try again.");
            }
        });
    }

    function drawChartsGetCompetencyWiseScores() {
        $.ajax({
            url: $("#myUrl").val() + "GetCompetencyWiseScores",
            type: "GET",
            data: { userId: $('#userId').val() },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                chartData1 = data.FirstList;
                chartData2 = data.SecondList;
                chartData3 = data.ThirdList;
                chartData4 = data.FourthList;
                chartData5 = data.FifthList;

                //First Chart
                var data = new google.visualization.DataTable();
                //Adding columns to data table to insert the chart data
                data.addColumn('string', 'Competency');
                data.addColumn('number', 'Percentage');
                //bind the data to the data table using for loop
                var Competency = 'N/A';
                var p = 0;
                for (var i = 0; i < chartData1.length; i++) {
                    data.addRow([chartData1[i].Description, chartData1[i].Percentage]);
                    Competency = chartData1[i].Description;
                    p = p + chartData1[i].Percentage;
                }
                data.addRow(['', 100 - p]);

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.PieChart($('#chartC1')[0]);
                google.visualization.events.addListener(chart, 'ready', AddNamespaceHandler);
                chart.draw(data,
                      {
                          title: Competency + " (" + p + "%)", //title of the pie chart
                          position: "top", fontsize: "14px", pieHole: 0.8, legend: 'none', chma: '1,1,1,1',
                          chartArea: { width: '90%' }, pieSliceText: "none"
                      });

                ////Second Chart
                var data = new google.visualization.DataTable();
                //Adding columns to data table to insert the chart data
                data.addColumn('string', 'Competency');
                data.addColumn('number', 'Percentage');
                //bind the data to the data table using for loop
                var Competency = 'N/A';
                var p = 0;
                for (var i = 0; i < chartData2.length; i++) {
                    data.addRow([chartData2[i].Description, chartData2[i].Percentage]);
                    Competency = chartData2[i].Description;
                    p = p + chartData2[i].Percentage;
                }
                data.addRow(['', 100 - p]);

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.PieChart($('#chartC2')[0]);
                google.visualization.events.addListener(chart, 'ready', AddNamespaceHandler);
                chart.draw(data,
                      {
                          title: Competency + " (" + p + "%)", //title of the pie chart
                          position: "top", fontsize: "14px", pieHole: 0.8, legend: 'none', chma: '1,1,1,1',
                          chartArea: { width: '90%' }, pieSliceText: "none"
                      });

                ////Third Chart
                var data = new google.visualization.DataTable();
                //Adding columns to data table to insert the chart data
                data.addColumn('string', 'Competency');
                data.addColumn('number', 'Percentage');
                //bind the data to the data table using for loop
                var Competency = 'N/A';
                var p = 0;
                for (var i = 0; i < chartData3.length; i++) {
                    data.addRow([chartData3[i].Description, chartData3[i].Percentage]);
                    Competency = chartData3[i].Description;
                    p = p + chartData3[i].Percentage;
                }
                data.addRow(['', 100 - p]);

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.PieChart($('#chartC3')[0]);
                google.visualization.events.addListener(chart, 'ready', AddNamespaceHandler);
                chart.draw(data,
                      {
                          title: Competency + " (" + p + "%)", //title of the pie chart
                          position: "top", fontsize: "14px", pieHole: 0.8, legend: 'none', chma: '1,1,1,1',
                          chartArea: { width: '90%' }, pieSliceText: "none"
                      });

                ////Fourth Chart
                var data = new google.visualization.DataTable();
                //Adding columns to data table to insert the chart data
                data.addColumn('string', 'Competency');
                data.addColumn('number', 'Percentage');
                //bind the data to the data table using for loop
                var Competency = 'N/A';
                var p = 0;
                for (var i = 0; i < chartData4.length; i++) {
                    data.addRow([chartData4[i].Description, chartData4[i].Percentage]);
                    Competency = chartData4[i].Description;
                    p = p + chartData4[i].Percentage;
                }
                data.addRow(['', 100 - p]);

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.PieChart($('#chartC4')[0]);
                google.visualization.events.addListener(chart, 'ready', AddNamespaceHandler);
                chart.draw(data,
                      {
                          title: Competency + " (" + p + "%)", //title of the pie chart
                          position: "top", fontsize: "14px", pieHole: 0.8, legend: 'none', chma: '1,1,1,1',
                          chartArea: { width: '90%' }, pieSliceText: "none"
                      });

                ////Third Chart
                var data = new google.visualization.DataTable();
                //Adding columns to data table to insert the chart data
                data.addColumn('string', 'Competency');
                data.addColumn('number', 'Percentage');
                //bind the data to the data table using for loop
                var Competency = 'N/A';
                var p = 0;
                for (var i = 0; i < chartData5.length; i++) {
                    data.addRow([chartData5[i].Description, chartData5[i].Percentage]);
                    Competency = chartData5[i].Description;
                    p = p + chartData5[i].Percentage;
                }
                data.addRow(['', 100 - p]);

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.PieChart($('#chartC5')[0]);
                google.visualization.events.addListener(chart, 'ready', AddNamespaceHandler);
                chart.draw(data,
                      {
                          title: Competency + " (" + p + "%)", //title of the pie chart
                          position: "top", fontsize: "14px", pieHole: 0.8, legend: 'none', chma: '1,1,1,1',
                          chartArea: { width: '90%' }, pieSliceText: "none"
                      });
            },
            error: function () {
                alert("Error loading data! Please try again.");
            }
        });
    }

    function drawChartsGetDifficultyLevelWiseScores() {
        $.ajax({
            url: $("#myUrl").val() + "GetDifficultyLevelWiseScores",
            type: "GET",
            data: { userId: $('#userId').val() },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                chartData1 = data.FirstList;
                chartData2 = data.SecondList;
                chartData3 = data.ThirdList;

                //First Chart
                var data = new google.visualization.DataTable();
                //Adding columns to data table to insert the chart data
                data.addColumn('string', 'DifficultyLevel');
                data.addColumn('number', 'Percentage');
                //bind the data to the data table using for loop
                var DifficultyLevel = '';
                var p = 0;
                for (var i = 0; i < chartData1.length; i++) {
                    data.addRow([chartData1[i].Description, chartData1[i].Percentage]);
                    DifficultyLevel = chartData1[i].Description;
                    p = p + chartData1[i].Percentage;
                }
                data.addRow(['', 100 - p]);

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.PieChart($('#chartD1')[0]);
                google.visualization.events.addListener(chart, 'ready', AddNamespaceHandler);
                chart.draw(data,
                      {
                          title: DifficultyLevel + " (" + p + "%)", //title of the pie chart
                          position: "top", fontsize: "14px", pieHole: 0.8, legend: 'none', chma: '1,1,1,1',
                          chartArea: { width: '90%' }, pieSliceText: "none"
                      });

                ////Second Chart
                var data = new google.visualization.DataTable();
                //Adding columns to data table to insert the chart data
                data.addColumn('string', 'DifficultyLevel');
                data.addColumn('number', 'Percentage');
                //bind the data to the data table using for loop
                var DifficultyLevel = '';
                var p = 0;
                for (var i = 0; i < chartData2.length; i++) {
                    data.addRow([chartData2[i].Description, chartData2[i].Percentage]);
                    DifficultyLevel = chartData2[i].Description;
                    p = p + chartData2[i].Percentage;
                }
                data.addRow(['', 100 - p]);

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.PieChart($('#chartD2')[0]);
                google.visualization.events.addListener(chart, 'ready', AddNamespaceHandler);
                chart.draw(data,
                      {
                          title: DifficultyLevel + " (" + p + "%)", //title of the pie chart
                          position: "top", fontsize: "14px", pieHole: 0.8, legend: 'none', chma: '1,1,1,1',
                          chartArea: { width: '90%' }, pieSliceText: "none"
                      });

                ////Third Chart
                var data = new google.visualization.DataTable();
                //Adding columns to data table to insert the chart data
                data.addColumn('string', 'DifficultyLevel');
                data.addColumn('number', 'Percentage');
                //bind the data to the data table using for loop
                var DifficultyLevel = '';
                var p = 0;
                for (var i = 0; i < chartData3.length; i++) {
                    data.addRow([chartData3[i].Description, chartData3[i].Percentage]);
                    DifficultyLevel = chartData3[i].Description;
                    p = p + chartData3[i].Percentage;
                }
                data.addRow(['', 100 - p]);

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.PieChart($('#chartD3')[0]);
                google.visualization.events.addListener(chart, 'ready', AddNamespaceHandler);
                chart.draw(data,
                      {
                          title: DifficultyLevel + " (" + p + "%)", //title of the pie chart
                          position: "top", fontsize: "14px", pieHole: 0.8, legend: 'none', chma: '1,1,1,1',
                          chartArea: { width: '90%' }, pieSliceText: "none"
                      });                
            },
            error: function () {
                alert("Error loading data! Please try again.");
            }
        });
    }
});