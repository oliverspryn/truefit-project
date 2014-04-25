(function($) {
	$.fn.burndown = function(data, months, color) {
		$(this).highcharts({
			chart: {
				backgroundColor: null,
				borderRadius: 0,
				spacing: [ 40, 10, 40, 10 ]
			},
			colors: [ color ],
			credits: { enabled: false },
			exporting: { enabled: false },
			legend: { enabled: false },
			title: { text: null },
			tooltip: {
				formatter: function() {
					return '<b>' + this.y + '</b> ' + (this.y == 1.0 ? 'hour' : 'hours') + ' remaining for <b>' + this.x + '</b>';
				}
			},
			series: [ { data: data } ],
			xAxis: {
				categories: months,
				labels: {
					style: {
						'color': '#313B47',
						'font-size': '12px',
						'font-weight': 'bold'
					}
				},
				lineColor: '#313B47',
				tickColor: '#CCCCCC',
			},
			yAxis: {
				allowDecimals: false,
				gridLineColor: '#CCCCCC',
				labels: {
					style: {
						'color': '#313B47',
						'font-size': '12px',
						'font-weight': 'bold'
					}
				},
				lineColor: '#313B47',
				lineWidth: 1,
				min: 0,
				title: {
					style: { color: '#313B47' },
					text: 'Remaining Hours'
				}
			}
		});
	}
})(jQuery);