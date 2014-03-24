$(function () {
    var percent = 79.0;

    $('#task-progress').highcharts({
        chart: {
            backgroundColor: null,
            height: 250,
            spacing: [0, 0, 0, 0],
            width: 250
        },
        credits: {
            enabled: false
        },
        exporting: {
            enabled: false
        },
        plotOptions: {
            pie: {
                animation: false,
                borderWidth: 0,
                dataLabels: { enabled: false },
                states: { hover: { enabled: false } }
            }
        },
        series: [{
            data: [{
                color: '#77E38C',
                y: percent
            }, {
                color: '#FFFFFF',
                y: 100.0 - percent
            }],
            innerSize: '80%',
            type: 'pie'
        }, {
            data: [{
                color: '#323A45',
                y: 100.0
            }],
            size: '80%',
            type: 'pie'
        }],
        title: {
            align: 'center',
            text: '<span style="color: #FFFFFF; font-size: 60px;  vertical-align: top;">' + percent + '<span style="color: #AAAAAA; font-size: 24px;">%</span></span>',
            verticalAlign: 'middle',
            y: 20
        },
        tooltip: { enabled: false }
    });
});

$(function () {
    $('#task-burndown').highcharts({
        chart: {
            backgroundColor: '#77E38C',
            borderRadius: 0,
            spacing: [40, 10, 40, 10]
        },
        colors: ['#FFFFFF'],
        credits: {
            enabled: false
        },
        exporting: {
            enabled: false
        },
        title: {
            text: null
        },
        xAxis: {
            categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
                'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            lineColor: '#000000',
            tickColor: '#000000',
        },
        yAxis: {
            gridLineColor: '#000000',
            gridLineDashStyle: 'LongDash',
            title: {
                style: {
                    color: '#FFFFFF'
                },
                text: 'Remaining Hours'
            }
        },
        legend: {
            enabled: false
        },
        series: [{
            name: 'Remaining',
            data: [100.0, 86.9, 73.5, 67.5, 55.2, 47.5, 40.2, 31.5, 27.3, 20.3, 13.9, 9.6]
        }]
    });
});

$(function () {
    $('#bug-burndown').highcharts({
        chart: {
            backgroundColor: '#00D2FF',
            borderRadius: 0,
            spacing: [40, 10, 40, 10]
        },
        colors: ['#FFFFFF'],
        credits: {
            enabled: false
        },
        exporting: {
            enabled: false
        },
        title: {
            text: null
        },
        xAxis: {
            categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
                'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            lineColor: '#000000',
            tickColor: '#000000',
        },
        yAxis: {
            gridLineColor: '#000000',
            gridLineDashStyle: 'LongDash',
            title: {
                style: {
                    color: '#FFFFFF'
                },
                text: 'Remaining Hours'
            }
        },
        legend: {
            enabled: false
        },
        series: [{
            name: 'Remaining',
            data: [100.0, 86.9, 73.5, 67.5, 55.2, 47.5, 40.2, 31.5, 27.3, 20.3, 13.9, 9.6]
        }]
    });
});

$(function () {
    var percent = 76.0;

    $('#bug-progress').highcharts({
        chart: {
            backgroundColor: null,
            height: 250,
            spacing: [0, 0, 0, 0],
            width: 250
        },
        credits: {
            enabled: false
        },
        exporting: {
            enabled: false
        },
        plotOptions: {
            pie: {
                animation: false,
                borderWidth: 0,
                dataLabels: { enabled: false },
                states: { hover: { enabled: false } }
            }
        },
        series: [{
            data: [{
                color: '#00D2FF',
                y: percent
            }, {
                color: '#FFFFFF',
                y: 100.0 - percent
            }],
            innerSize: '80%',
            type: 'pie'
        }, {
            data: [{
                color: '#323A45',
                y: 100.0
            }],
            size: '80%',
            type: 'pie'
        }],
        title: {
            align: 'center',
            text: '<span style="color: #FFFFFF; font-size: 60px;  vertical-align: top;">' + percent + '<span style="color: #AAAAAA; font-size: 24px;">%</span></span>',
            verticalAlign: 'middle',
            y: 20
        },
        tooltip: { enabled: false }
    });
});

$(function () {
    var max = 10;

    $('#bug-recent-chart').highcharts({
        chart: {
            backgroundColor: null,
            spacing: [0, 0, 0, 0],
            type: 'column'
        },
        credits: { enabled: false },
        exporting: { enabled: false },
        legend: { enabled: false },
        title: { text: null },
        tooltip: { enabled: false },
        xAxis: {
            categories: ['S', 'M', 'T', 'W', 'R', 'F', 'S'],
            gridLineColor: 'transparent',
            lineWidth: 0,
            minorGridLineWidth: 0,
            labels: { style: { 'color': '#999999', 'font-size': '14px' } },
            tickColor: 'transparent'
        },
        yAxis: {
            gridLineColor: 'transparent',
            labels: { enabled: false },
            lineWidth: 0,
            minorGridLineWidth: 0,
            title: { text: null }
        },
        plotOptions: {
            column: {
                animation: false,
                borderColor: 'transparent',
                borderWidth: 0,
                stacking: 'normal',
                states: { hover: { enabled: false } }
            }
        },
        series: [{
            color: ['#DDDDDD'],
            data: [5, 3, 4, 7, 2, 1, 6]
        }, {
            color: ['#00D2FF'],
            data: [5, 7, 6, 3, 8, 9, 4]
        }]
    });
});

$(function () {
    var max = 10;

    $('#task-recent-chart').highcharts({
        chart: {
            backgroundColor: null,
            spacing: [0, 0, 0, 0],
            type: 'column'
        },
        credits: { enabled: false },
        exporting: { enabled: false },
        legend: { enabled: false },
        title: { text: null },
        tooltip: { enabled: false },
        xAxis: {
            categories: ['S', 'M', 'T', 'W', 'R', 'F', 'S'],
            gridLineColor: 'transparent',
            lineWidth: 0,
            minorGridLineWidth: 0,
            labels: { style: { 'color': '#999999', 'font-size': '14px' } },
            tickColor: 'transparent'
        },
        yAxis: {
            gridLineColor: 'transparent',
            labels: { enabled: false },
            lineWidth: 0,
            minorGridLineWidth: 0,
            title: { text: null }
        },
        plotOptions: {
            column: {
                animation: false,
                borderColor: 'transparent',
                borderWidth: 0,
                stacking: 'normal',
                states: { hover: { enabled: false } }
            }
        },
        series: [{
            color: ['#DDDDDD'],
            data: [5, 3, 4, 7, 2, 1, 6]
        }, {
            color: ['#77E38C'],
            data: [5, 7, 6, 3, 8, 9, 4]
        }]
    });
});