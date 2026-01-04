(function ($) {
  "use strict";

	$(document).on('partdoShopPageInit', function () {
		partdoThemeModule.hoverslider();
	});

	partdoThemeModule.hoverslider = function() {
		hoverSlider.init({});
		hoverSlider.prepareMarkup();
	}

	
	$(document).ready(function() {
		partdoThemeModule.hoverslider();
	});

})(jQuery);