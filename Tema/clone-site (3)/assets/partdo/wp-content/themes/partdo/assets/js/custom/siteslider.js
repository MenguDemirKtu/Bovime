(function ($) {
  "use strict";

	$(document).on('partdoShopPageInit', function () {
		partdoThemeModule.siteslider();
	});

	partdoThemeModule.siteslider = function() {
		var block = document.querySelectorAll('.klbth-slider');

		var _loop2 = function _loop2() {
			var self = block[i];
			var itemsBy = self.dataset.items;
			var itemsByMobile = self.dataset.mobileitems || 2;
			var itemsByTablet = self.dataset.tabletitems || 2;
			var slideByItem = self.dataset.slidebyitem || 1;
			var slideSpeed = parseInt(self.dataset.speed) || 300;
			var slideInfinite = self.dataset.infinite === 'true' ? true : false || true;
			var slideArrows = self.dataset.arrows === 'true' ? true : false;
			var slideDots = self.dataset.dots === 'true' ? true : false; // Focus select settings

			var slideForNav = self.dataset.asfornav;
			var slideFocusOnSelect = self.dataset.focus === 'true' ? true : false || false; // Autoplay settings

			var slideAutoPlay = self.dataset.autoplay === 'true' ? true : false;
			var slideAutoSpeed = parseInt(self.dataset.autospeed) || 5000; // Centered slide

			var slideCenter = self.dataset.center === 'true' ? true : false || false;
			var slideCenterPadding = self.dataset.centerpadding; // Css slide animation

			var slideCssEase = self.dataset.css; // Fade effect

			var slideFade = self.dataset.fade === 'true' ? true : false || false; // Rtl settings

			var slideRtl = self.dataset.rtl === 'true' ? true : false || false; // Vertical

			var slideVertical = self.dataset.vertical === 'true' ? true : false || false;
			var slideVerticalSwip = self.dataset.verticalswip === 'true' ? true : false || false;
			$(self).on('init', function (event, slick) {
			  var items = document.querySelectorAll('.slider-item');
			  imagesLoaded(items, function () {
				self.parentNode.classList.add('slider-loaded');

				if (self.classList.contains('arrows-center')) {
				  var centerItems = self.querySelectorAll('.slider-item');

				  var _loop3 = function _loop3() {
					var that = centerItems[i];
					var mediaHeight = void 0,
						arrowHeight = void 0;
					var media = that.querySelectorAll('.entry-media');
					var arrows = self.querySelectorAll('.slick-arrow');

					var setArrowHeight = function setArrowHeight() {
					  for (var i = 0; i < media.length; i++) {
						mediaHeight = media[i].clientHeight / 2;
					  }

					  for (var i = 0; i < arrows.length; i++) {
						if (arrows[i] != null) {
						  arrowHeight = arrows[i].clientHeight / 2;
						  arrows[i].style.top = "".concat(mediaHeight - arrowHeight, "px");
						}
					  }
					};

					window.addEventListener('load', setArrowHeight);
					window.addEventListener('resize', setArrowHeight);
				  };

				  for (var i = 0; i < centerItems.length; i++) {
					_loop3();
				  }
				}
			  });
			});
			var args = {
			  arrows: slideArrows,
			  dots: slideDots,
			  asNavFor: slideForNav,
			  autoplay: slideAutoPlay,
			  autoplaySpeed: slideAutoSpeed,
			  centerMode: slideCenter,
			  centerPadding: slideCenterPadding,
			  cssEase: slideCssEase,
			  fade: slideFade && slideRtl,
			  focusOnSelect: slideFocusOnSelect,
			  slidesToShow: itemsBy,
			  slidesToScroll: slideByItem,
			  speed: slideSpeed,
			  infinite: slideInfinite,
			  prevArrow: '<button type="button" class="slick-nav slick-prev slick-button"><?xml version="1.0" encoding="UTF-8"?><svg width="24px" height="24px" stroke-width="0.97" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" color="#000000"><path d="M15 6l-6 6 6 6" stroke="#000000" stroke-width="0.97" stroke-linecap="round" stroke-linejoin="round"></path></svg></button>',
			  nextArrow: '<button type="button" class="slick-nav slick-next slick-button"><?xml version="1.0" encoding="UTF-8"?><svg width="24px" height="24px" stroke-width="0.97" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" color="#000000"><path d="M9 6l6 6-6 6" stroke="#000000" stroke-width="0.97" stroke-linecap="round" stroke-linejoin="round"></path></svg></button>',
			  rtl: slideRtl,
			  vertical: slideVertical,
			  verticalSwiping: slideVerticalSwip,
			  responsive: [{
				breakpoint: 1440,
				settings: {
				  slidesToShow: itemsBy <= 6 ? itemsBy : 5
				}
			  }, {
				breakpoint: 1360,
				settings: {
				  slidesToShow: itemsBy <= 5 ? itemsBy : 4
				}
			  }, {
				breakpoint: 1200,
				settings: {
				  slidesToShow: itemsBy <= 4 ? itemsBy : 3
				}
			  }, {
				breakpoint: 992,
				settings: {
				  slidesToShow: itemsByTablet ? itemsByTablet : itemsBy <= 3 ? itemsBy : 2
				}
			  }, {
				breakpoint: 768,
				settings: {
				  arrows: false,
				  slidesToShow: itemsByTablet ? itemsByTablet : itemsBy <= 3 ? itemsBy : 2,
				  vertical: false
				}
			  }, {
				breakpoint: 480,
				settings: {
				  arrows: false,
				  slidesToShow: itemsByMobile ? itemsByMobile : itemsBy <= 2 ? itemsBy : 1,
				  vertical: false
				}
			  }, {
				breakpoint: 320,
				settings: {
				  arrows: false,
				  slidesToShow: 1,
				  vertical: false
				}
			  }]
			};
			$(self).not('.slick-initialized').slick(args);
		};
		
		for (var i = 0; i < block.length; i++) {
			_loop2();
		}
	}
	$(document).ready(function() {
		partdoThemeModule.siteslider();
	});

})(jQuery);
