(function ($) {
  "use strict";

	$(document).on('partdoShopPageInit', function () {
		partdoThemeModule.stickysidebar();
	});

	partdoThemeModule.stickysidebar = function() {
      var sticky = document.querySelectorAll('.sticky');

		if (sticky !== null) {
			for (var i = 0; i < sticky.length; i++) {
			  var self = sticky;
			  $(self).theiaStickySidebar({
				// Settings
				additionalMarginTop: 30
			  });
			}
		}
	}
	$(document).ready(function() {
		partdoThemeModule.stickysidebar();
	});

})(jQuery);
