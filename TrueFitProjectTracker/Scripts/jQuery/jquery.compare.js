(function($) {
	$.fn.compare = function(taskCount, taskColor, bugCount, bugColor) {
		$(this).highcharts({
			chart: {
				backgroundColor: null,
				type: 'bar'
			},
			credits: { enabled: false },
			exporting: { enabled: false },
			legend: { enabled: false },
			plotOptions: {
				bar: {
					animation: false,
					dataLabels: { enabled: false },
					states: { hover: { enabled: false } }
				}
			},
			series: [{
				data: [{
					color: taskColor,
					y: taskCount
				}, {
					color: bugColor,
					y: bugCount
				}]
			}],
			title: { text: null },
			tooltip: { enabled: false },
			xAxis: {
				categories: ['Stories', 'Bugs'],
				labels: {
					style: {
						'color': '#313B47',
						'font-size': '12px',
						'font-weight': 'bold'
					}
				},
				lineColor: '#313B47',
				lineWidth: 1,
				tickLength: 0,
				tickWidth: 0,
				title: { text: null }
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
				title: { text: null }
			}
		});
	}
})(jQuery);