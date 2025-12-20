(function ($) {
  "use strict";

	$(document).on('partdoShopPageInit added_to_cart', function () {
		partdoThemeModule.widgetlayerednavDropdown();
	});

	partdoThemeModule.widgetlayerednavDropdown = function() {
		
		$( 'select.woocommerce-widget-layered-nav-dropdown' ).on( 'change', function() {
			var slug = $( this ).val();
			
			var taxonomyfiltername = $(this).closest('form').find('input').attr('name');
			$(this).closest('form').find('input[name='+taxonomyfiltername+']').val( slug  );
			// Submit form on change if standard dropdown.
			if ( ! $( this ).attr( 'multiple' ) ) {
				$( this ).closest( 'form' ).trigger( 'submit' );
			}
		});
		
	}
	
	$(document).ready(function() {
		partdoThemeModule.widgetlayerednavDropdown();
	});

})(jQuery);
