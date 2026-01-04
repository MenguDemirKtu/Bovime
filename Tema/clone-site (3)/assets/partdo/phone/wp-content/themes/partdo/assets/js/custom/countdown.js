(function ($) {
  "use strict";

  $(document).on('partdoShopPageInit', function () {
    partdoThemeModule.countdown();
  });

  partdoThemeModule.countdown = function() {
    var block = document.querySelectorAll('.kblth-countdown');

    var _loop4 = function _loop4() {
      var self = block[i];

      if (self !== null) {

        var dateStr = self.dataset.date;
        var tz      = self.dataset.timezone || 'UTC';


        var m = moment.tz(dateStr, [
          'YYYY/MM/DD HH:mm:ss',
          'YYYY-MM-DD HH:mm:ss',
          'YYYY/MM/DD',
          'YYYY-MM-DD'
        ], tz);

        var finalDate = m.isValid() ? m.toDate() : dateStr;

        $(self).countdown(finalDate, function (event) {
          var d = self.querySelector('.days');
          var h = self.querySelector('.hours');
          var m = self.querySelector('.minutes');
          var s = self.querySelector('.second');

          d.innerHTML = event.strftime('%D');
          h.innerHTML = event.strftime('%H');
          m.innerHTML = event.strftime('%M');
          s.innerHTML = event.strftime('%S');
        });
      }
    };

    for (var i = 0; i < block.length; i++) {
      _loop4();
    }
  }

  $(document).ready(function() {
    partdoThemeModule.countdown();
  });

})(jQuery);
