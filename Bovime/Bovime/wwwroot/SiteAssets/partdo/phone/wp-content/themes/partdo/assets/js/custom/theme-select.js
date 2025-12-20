(function ($) {
  "use strict";

	$(document).on('partdoShopPageInit', function () {
		partdoThemeModule.themeselect();
	});

	partdoThemeModule.themeselect = function() {
      var block = document.querySelectorAll('.theme-select');

      for (var i = 0; i < block.length; i++) {
        var self = block[i];
        var placeholder = self.dataset.placeholder;
        var search = self.dataset.search === "true" ? null : Infinity;
        var searchPlaceholder = self.dataset.searchplaceholder;
        $(self).select2({
          placeholder: placeholder,
          allowClear: true,
          minimumResultsForSearch: search,
          searchInputPlaceholder: searchPlaceholder
        });
      }
	}
	$(document).ready(function() {
		partdoThemeModule.themeselect();
	});

})(jQuery);
