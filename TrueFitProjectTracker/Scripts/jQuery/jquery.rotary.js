(function($) {
	$.fn.rotary = function(percent, color) {
		$(this).highcharts({
			chart: {
				backgroundColor: null,
				height: 250,
				spacing: [0, 0, 0, 0]
			},
			credits: { enabled: false },
			exporting: { enabled: false	},
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
					color: color,
					y: percent
				}, {
					color: '#313B47',
					y: 100.0 - percent
				}],
				innerSize: '85%',
				type: 'pie'
			}],
			title: { text: null },
			tooltip: { enabled: false }
		});
	};
})(jQuery);