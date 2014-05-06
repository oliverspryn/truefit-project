(function($) {
	$.fn.narrow = function(targetList) {
		$(this).keyup(function() {
			var projects = $(targetList);
			var list = projects.find('li');
			var search = $(this).val();
			var item = new Array();
			var itemSort = new Array();
			for (var i = 0; i < list.length; i++) {
				item[i] = $(targetList).children('li').eq(i);
				itemSort[i] = list[i].textContent.toString();
				$(targetList).children('li').eq(i).show();
			}
			var counter = new Array();
			for (var i = 0; i < item.length; i++) {
				counter[i] = list[i].textContent.toString().toLowerCase().indexOf(search.toString().toLowerCase());
			}


			if (search.toString() == "") {
				itemSort = itemSort.sort();
				var itemDisplay = new Array();
				for (var j = 0; j < itemSort.length; j++) {
					for (var k = 0; k < itemSort.length; k++) {
						if (itemSort[j].toString().toLowerCase() == list[k].textContent.toString().toLowerCase()) {
							itemDisplay[j] = item[k];
							break;
						}
					}
				}
				for (var i = itemDisplay.length - 1; i >= 0; i--) {
					$(targetList).prepend(itemDisplay[i]);

				}
				return;



			}
			for (var i = 0; i < list.length; i++) {
				$(targetList).children('li').eq(i).remove();
			}
			//HOLD FOR REFERENCE
			// if (list[i].textContent.toString().toLowerCase().indexOf(search.toString().toLowerCase()) >= 0) {
			//$(targetList).children('li').eq(i).show("fast");
			//$(targetList).children('li').eq(i).remove();
			//$(targetList).prepend(item);
			// }
			// else{
			//$(targetList).children('li').eq(i).remove();
			//$(targetList).prepend(item);
			//$(targetList).children('li').eq(i).hide("slow");
			//}



			var i = -1;
			var max = 0;
			for (var i = 0; i < counter.length; i++) {
				if (counter[i] >= max) max = counter[i];
			}


			for (var i = 0; i <= max; i++) {
				for (var j = 0; j < item.length; j++) {
					if (counter[j] == i) {
						if (counter[j] == -1) {
							continue;
						}
						else {
							$(targetList).append(item[j]);
						}

					}

				}
			}
			for (var j = 0; j < item.length; j++) {
				if (counter[j] == -1) {
					$(targetList).append(item[j]);
					//$(targetList).children('li').eq(i).hide();
				}
			}
		});
	}
})(jQuery);

// when changed:
// use item to hold the elements and counter to store the indexOf computations
//remove all elements
//swap elements in item, reappend. 
/*for later: http://www.w3schools.com/jquery/jquery_slide.asp */