(function($) {
	$.fn.locker = function(options) {
	//Override plugin defaults
		$.extend($.fn.locker.defaults, options);

		return this.each(function() {
		//Page elements
			$.fn.locker.lock = $(this);
			$.fn.locker.target = $($.fn.locker.defaults.target);

		//Grab the window for scroll events
			$.fn.locker.window = $(window);

		//Bootstrap this plugin
			$.fn.locker.eventsInit();
		});
	};

//Listen for mouse scrolls and activate the locked header when needed
	$.fn.locker.eventsInit = function() {
		$.fn.locker.window.scroll(function() {
			if ($.fn.locker.window.scrollTop() > $.fn.locker.defaults.topOffset) {
				$.fn.locker.target.addClass($.fn.locker.defaults.className);
			} else {
				$.fn.locker.target.removeClass($.fn.locker.defaults.className);
			}
		});
	};

//Default plugin options
	$.fn.locker.defaults = {
		className : 'locked',
		target    : 'body',
		topOffset : 150
	};
})(jQuery);

$(function() {
	$('nav.main-breadcrumb').locker();
});