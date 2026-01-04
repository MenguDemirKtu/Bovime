(function ($) {
  "use strict";

	$(document).on('partdoShopPageInit', function () {
		partdoThemeModule.sitescroll();
	});

	partdoThemeModule.sitescroll = function() {
		var container = document.querySelectorAll('.site-scroll');

		  for (var i = 0; i < container.length; i++) {
			var ps = new PerfectScrollbar(container[i], {
			  suppressScrollX: true,
			  wheelPropagation: true
			});
			ps.update();
		  }
	}
	$(document).ready(function() {
		partdoThemeModule.sitescroll();
	});

})(jQuery);
