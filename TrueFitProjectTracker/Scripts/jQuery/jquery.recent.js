(function($) {
	$.fn.recent = function(data, days, color) {
	//Find the maximum data value
		var max = 1;

		for(var i = 0; i < data.length; ++i) {
			if (data[i] > max) max = data[i];
		}

	//Generate the chart
		$(this).highcharts({
			chart: {
				backgroundColor: null,
				spacing: [0, 0, 10, 0],
				type: 'column'
			},
			credits: { enabled: false },
			exporting: { enabled: false },
			legend: { enabled: false },
			title: { text: null },
			tooltip: { enabled: false },
			xAxis: {
				categories: days,
				gridLineColor: 'transparent',
				lineWidth: 0,
				minorGridLineWidth: 0,
				labels: {
					style: {
						'color': '#313B47',
						'font-size': '12px'
					},
					y: 20
				},
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
				color: [ '#F2F3F5' ],
				data: function() {
					var subElements = [];

					for (var i = 0; i < data.length; ++i) {
						subElements[i] = max - data[i];
					}

					return subElements;
				}()
			}, {
				color: [ color ],
				data: data
			}]
		});
	}
})(jQuery);