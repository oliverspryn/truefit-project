(function($) {
	$.fn.compare = function(taskCount, taskColor, bugCount, bugColor) {
		var total = bugCount + taskCount;
		var taskPercent = (taskCount / total) * 100;
    
		$(this).highcharts({
			chart: { type: 'solidgauge' },
			credits: { enabled: false },
			pane: {
				background: {
					backgroundColor: null,
					innerRadius: '60%',
					outerRadius: '100%',
					shape: 'arc'
				},
	    		center: [ '50%', '85%' ],
				endAngle: 90,
	    		size: '140%',
				startAngle: -90
			},
			series: [{
				data: [{
					color: '#77E38C',
					y: 100
				}, {
					color: '#14B9D6',
					y: 100 - taskPercent
				}]
			}],
			title: { text: null },
			tooltip: { enabled: false },
			yAxis: {
				labels: {
					formatter: function() {
						return this.value + '%';
					},
					style: {
						'color': '#313B47',
						'font-size': '12px',
						'font-weight': 'bold'
					},
					y: 16
				},
				lineWidth: 0,
				max: 100,
				min: 0,
				minorTickInterval: null,
				tickPixelInterval: 400,
				tickWidth: 0,
				title: { text: null }    
			}
		});
	}
})(jQuery);