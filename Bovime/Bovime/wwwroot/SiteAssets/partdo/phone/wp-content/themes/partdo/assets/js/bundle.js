"use strict";

function _toConsumableArray(arr) { return _arrayWithoutHoles(arr) || _iterableToArray(arr) || _unsupportedIterableToArray(arr) || _nonIterableSpread(); }

function _nonIterableSpread() { throw new TypeError("Invalid attempt to spread non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."); }

function _unsupportedIterableToArray(o, minLen) { if (!o) return; if (typeof o === "string") return _arrayLikeToArray(o, minLen); var n = Object.prototype.toString.call(o).slice(8, -1); if (n === "Object" && o.constructor) n = o.constructor.name; if (n === "Map" || n === "Set") return Array.from(o); if (n === "Arguments" || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)) return _arrayLikeToArray(o, minLen); }

function _iterableToArray(iter) { if (typeof Symbol !== "undefined" && iter[Symbol.iterator] != null || iter["@@iterator"] != null) return Array.from(iter); }

function _arrayWithoutHoles(arr) { if (Array.isArray(arr)) return _arrayLikeToArray(arr); }

function _arrayLikeToArray(arr, len) { if (len == null || len > arr.length) len = arr.length; for (var i = 0, arr2 = new Array(len); i < len; i++) { arr2[i] = arr[i]; } return arr2; }

(function ($) {
  "use strict";

  var PARTDO_APP = {
    init: function init() {
      this.dom();
      this.menuDescription();
      this.categoryMenu();
      this.select2Placeholder();
      this.themeModal();
      this.mobileMenu();
      this.searchHolder();
      this.categoriesHolder();
      this.minicartmobile();
    },
    dom: function dom() {
      this.overlay = document.querySelector('.site-mask'); // Tooltip

      var tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');

      var tooltipList = _toConsumableArray(tooltipTriggerList).map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
      });
    },
    menuDescription: function menuDescription() {
      var block = document.querySelectorAll('.menu-item-description');

      for (var i = 0; i < block.length; i++) {
        var self = block[i];
        var width = self.clientWidth;
        var parent = self.closest('li');
        var sub = parent.querySelector('.sub-menu');
        sub.style.left = "".concat(width + 5 - 21, "px");
      }
    },
    categoryMenu: function categoryMenu() {
      var container = document.querySelector('.dropdown-menu #category-menu');

      if (container !== null) {
        var hasChildren = container.querySelectorAll('.menu-item-has-children');

        for (var i = 0; i < hasChildren.length; i++) {
          var self = hasChildren[i];
          var subMenu = self.lastElementChild;

          if (subMenu.tagName === 'UL') {
            var subTitle = subMenu.dataset.title;
            var subParent = subMenu.parentNode;

            if (subParent.classList.contains('has-image')) {
              var menuWidth = subMenu.offsetWidth;
              subMenu.style.width = menuWidth + menuWidth / 1.5 + 'px';
            }
          }
        }
      }
    },
    select2Placeholder: function select2Placeholder() {
      var Defaults = $.fn.select2.amd.require('select2/defaults');

      $.extend(Defaults.defaults, {
        searchInputPlaceholder: ''
      });

      var SearchDropdown = $.fn.select2.amd.require('select2/dropdown/search');

      var _renderSearchDropdown = SearchDropdown.prototype.render;

      SearchDropdown.prototype.render = function (decorated) {
        // invoke parent method
        var $rendered = _renderSearchDropdown.apply(this, Array.prototype.slice.apply(arguments));

        this.$search.attr('placeholder', this.options.get('searchInputPlaceholder'));
        return $rendered;
      };
    },

    themeModal: function themeModal() {
      var modals = document.querySelectorAll('.klbth-modal-holder');
      var modalButton = document.querySelectorAll('[data-klbth-modal]');
      var body = document.body;

      var _loop = function _loop() {
        var self = modalButton[i];
        var modalId = self.dataset.klbthModal;

        var modal = _toConsumableArray(modals).find(function (el) {
          return el.id === modalId;
        });

        var modalClose = modal.querySelector('.site-close');
        var overlay = modal.querySelector('.klbth-modal-overlay');
        var tl = gsap.timeline({
          paused: true,
          reversed: true
        });
        tl.to(modal, .2, {
          autoAlpha: 1,
          ease: 'power2.inOut'
        }).to(modal.querySelector('.klbth-modal-inner'), .15, {
          autoAlpha: 1,
          y: 0,
          ease: 'ease.inOut'
        }, "-=.15");

        if (self !== null) {
          self.addEventListener('click', function (e) {
            e.preventDefault();
            body.classList.add('modal-active');
            tl.play();
          });
        }

        if (modalClose !== null) {
          modalClose.addEventListener('click', function (e) {
            e.preventDefault();
            body.classList.remove('modal-active');
            tl.reverse(1.5);
          });
        }

        if (overlay !== null) {
          overlay.addEventListener('click', function (e) {
            e.preventDefault();
            body.classList.remove('modal-active');
            tl.reverse(1.5);
          });
        }
      };

      for (var i = 0; i < modalButton.length; i++) {
        _loop();
      }
    },
    mobileMenu: function mobileMenu() {
      var _this = this;

      var canvasMenu = document.querySelector('.site-drawer');

      if (canvasMenu != null || this.overlay !== null) {
        var i;
        var i;

        (function () {
          var tl = gsap.timeline({
            paused: true,
            reversed: true
          });
          tl.set(canvasMenu, {
            autoAlpha: 1
          }).to(canvasMenu, .5, {
            x: 0,
            ease: 'power4.inOut'
          }).to(_this.overlay, .5, {
            autoAlpha: 1,
            ease: 'power4.inOut'
          }, "-=.5");
          var button = document.querySelectorAll('.toggle-button');
		  const categoryButton = $( '.mobile-bottom-menu a.categories' );
          var close = document.querySelector('.site-drawer .site-close');
          /* const mask = document.querySelector( '.site-mask' ); */

          if (button !== null) {
            for (i = 0; i < button.length; i++) {
              var self = button[i];
              self.addEventListener('click', function (e) {
                e.preventDefault();
                tl.play();
              });
            }
          }

		  if ( categoryButton !== null ) {
			for ( var i = 0; i < categoryButton.length; i++ ) {
				const self = categoryButton[i];

				self.addEventListener( 'click', ( e ) => {
				  e.preventDefault();
				  tl.play();
				});
			}
		  }
		  
          close.addEventListener('click', function (e) {
            e.preventDefault();
            tl.reverse(1.5);
          });

          if(_this.overlay != null){
	          _this.overlay.addEventListener('click', function (e) {
	            e.preventDefault();
	            tl.reverse(1.5);
	          });
		  }

          var hasChildren = document.querySelectorAll('.site-drawer .menu-item-has-children');

          var subMenu = function subMenu(e) {
            var subUl;

            if (e.target.tagName === 'A') {
              e.preventDefault();
              subUl = e.target.nextElementSibling;
            } else {
              subUl = e.target.previousElementSibling;
            }

            var parentUl = e.target.closest('ul');
            var parentLi = e.target.closest('li');
            var activeLi = parentUl.querySelectorAll('.menu-item-has-children.active');

            var closeSubs = function closeSubs() {
              for (var i = 0; i < activeLi.length; i++) {
                var activeSub = activeLi[i].children[1];
                var childSubs = activeSub.querySelectorAll('.sub-menu');

                for (var i = 0; i < childSubs.length; i++) {
                  if (childSubs[i] != null) {
                    gsap.set(childSubs[i], {
                      height: 0
                    });
                  }
                }
              }
            };

            var subAnimation = function subAnimation(element, event) {
              gsap.to(element, {
                duration: .4,
                height: event,
                ease: 'power4.inOut',
                onComplete: closeSubs
              });
            };

            if (!parentLi.classList.contains('active')) {
              for (var i = 0; i < activeLi.length; i++) {
                activeLi[i].classList.remove('active');
                var sub = activeLi[i].children[1];
                subAnimation(sub, 0);
              }

              parentLi.classList.add('active');
              subAnimation(subUl, 'auto');
            } else {
              parentLi.classList.remove('active');
              subAnimation(subUl, 0);
            }
          };

          for (i = 0; i < hasChildren.length; i++) {
            var dropdown = document.createElement('span');
            dropdown.innerHTML = '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-chevron-down"><polyline points="6 9 12 15 18 9"></polyline></svg>';
            dropdown.className = 'menu-dropdown';
            hasChildren[i].appendChild(dropdown);
            var link = hasChildren[i].querySelector('a[href*="#"]');
            var sub = hasChildren[i].children[1];
            gsap.set(sub, {
              height: 0
            });
			
			
            dropdown.addEventListener('click', subMenu);
            if(link != null){
			link.addEventListener('click', subMenu);
			}
          }
        })();
      }
    },
    searchHolder: function searchHolder() {
      var holder = document.querySelector('.search-holder');
      var button = document.querySelectorAll('.search-button');

      if (holder !== null) {
        var close = holder.querySelector('.search-holder .site-close');
        var forward = true;

        for (var i = 0; i < button.length; i++) {
          var self = button[i];
          self.addEventListener('click', function (e) {
            e.preventDefault();
            document.body.classList.toggle('search-active');
            holder.classList.add('active');
            forward = false;
          }, false);
        }

        close.addEventListener('click', function (e) {
          e.preventDefault();

          if (!forward) {
            if (document.body.classList.contains('search-active')) {
              document.body.classList.remove('search-active');
              holder.classList.remove('active');
            }
          }

          forward = true;
        }, false);
      }
    },
    categoriesHolder: function categoriesHolder() {
      var button = document.querySelectorAll('.categories-button');
      var container = document.querySelector('.categories-holder');
      var forward = true;

      if (container != null) {
        if (forward) {
          if (button !== null) {
            for (var i = 0; i < button.length; i++) {
              button[i].addEventListener('click', function (e) {
                e.preventDefault();
                document.body.classList.toggle('categories-active');
                forward = false;
              }, false);
            }
          }
        }
      }
    },


    minicartmobile: function() {
	  if($(window).width() < 601){
		  var button = $( '.site-header .cart-button > a.quick-button-inner' );

		  button.on( 'click', function(e) {
			e.preventDefault();
			if($( '.site-header .cart-button .cart-dropdown' ).hasClass('hide')){
				$( '.site-header .cart-button .cart-dropdown' ).removeClass( 'hide' );
			} else {
				$( '.site-header .cart-button .cart-dropdown' ).addClass( 'hide' );
			}
		  });
	  }
    },


  };
  PARTDO_APP.init();
  
	$(document).ready(function() {
		$('.dropdown-cats a.dropdown-toggle').on('click', function (e) {
			e.preventDefault();
			$(".dropdown-menu").collapse('toggle');
		});

		if (window.innerHeight > document.body.offsetHeight) {
			  $('.site-footer').addClass('sticky-footer');
		}
	});
	
	$(window).load(function(){
		$('.site-loading').fadeOut('slow',function(){$(this).remove();});
	});
	
	$(window).scroll(function() {
        $(this).scrollTop() > 135 ? $("header.site-header").addClass("sticky-header") : $("header.site-header").removeClass("sticky-header")
    });
  
})(jQuery);