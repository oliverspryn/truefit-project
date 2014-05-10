function getDayNamesFromDayOfWeek(number) { // 0 is sunday, return ['S', 'M', ...]
	var days = ['S', 'M', 'T', 'W', 'R', 'F', 'S'];
	var result = [];
	for (var i = number; i < number + 7; i++) {
		result.push(days[i % 7]);
	}
	return result;
}

function getMonthNamesFromMonthAndYear(month, year, monthEnd, yearEnd) {
	var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
				'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
	var result = [];
	while (month <= monthEnd || year <= yearEnd) {
		// 1st time, put year with month
		if (month == 0 || result.length == 0)
			result.push(months[month] + ', ' + year.toString()); // 2014
		else
			result.push(months[month]);
		month++;
		if (month == 12) { month = 0; year++; } // reset
	}
	return result;
}

$(function() {
//Submit the bug reporter across frames
	$('button#bug-submit').click(function() {
		var frame = document.getElementById('bug-form');
		var content = frame.contentWindow || frame.contentDocument;
		if (content.document) content = content.document;

		if (frame.contentWindow.$('form').validationEngine('validate')) {
			var forms = content.getElementsByTagName('form');
			forms[0].submit();
		}
	});

//Open the task details pane
	var oldOpen = null;

	$('ul.task-list li div').click(function() {
		//Close the old pane
		if (oldOpen != null) {
			oldOpen.removeClass('open');
		}

		//Open the new pane
		oldOpen = $(this);
		oldOpen.addClass('open');
	});

//Buttons can also close task detail panes
	$('button.pane-close').click(function() {
		if (oldOpen != null) {
			oldOpen.removeClass('open');
			return false;
		}
	});

//Some key events
	$(document).keyup(function(e) {
	//Close any open task detail panes
		if (e.keyCode == 27 && oldOpen != null) { // ESC
			oldOpen.removeClass('open');
			return false;
		}

	//Scroll the task list left
		if (e.keyCode == 37) { // Left arrow key
			$('div.carousel').carousel('prev');
			return false;
		}

	//Scroll the task list right
		if (e.keyCode == 39) { // Right arrow key
			$('div.carousel').carousel('next');
			return false;
		}
	});
});