(function ($) {
  "use strict";

	$(document).on('partdoShopPageInit added_to_cart', function () {
		partdoThemeModule.productHover();
	});

	partdoThemeModule.productHover = function() {
		var product = document.querySelectorAll('.product');
			product.forEach(function (event) {
				var fadeBlock = event.querySelector('.product-footer');
				var contentBlock = event.querySelector('.product-content-fade');
				var outerHeight = 0;

				if (fadeBlock !== null && contentBlock !== null) {
				  var parent = contentBlock.closest('.product');
				  parent.classList.add('custom-hover');
				  outerHeight += fadeBlock.clientHeight;
				  contentBlock.style.marginBottom = "-".concat(outerHeight, "px");
				}
			});
	}
	
	$(document).ready(function() {
		partdoThemeModule.productHover();
	});

})(jQuery);
