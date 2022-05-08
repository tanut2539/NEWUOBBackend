$('.commitment-brief-history-1').css({ height: '280px', overflow: 'hidden' });
$('#show-less-1').on('click', function () {
    var $this = $(this);
    var c = $('.commitment-brief-history-1');
    if ($this.data('open')) {
        c.animate({ height: '280px' });
        $this.data('open', 0);
        $('#show-less-1').text("More than >>");
    }
    else {
        c.animate({ height: '830px' });
        $this.data('open', 1);
        $('#show-less-1').text("Show Less >>");
    }
});

$('.commitment-brief-history-2').css({ height: '280px', overflow: 'hidden' });
$('#show-less-2').on('click', function () {
    var $this = $(this);
    var d = $('.commitment-brief-history-2');
    if ($this.data('open')) {
        d.animate({ height: '280px' });
        $this.data('open', 0);
        $('#show-less-2').text("More than >>");
    }
    else {
        d.animate({ height: '820px' });
        $this.data('open', 1);
        $('#show-less-2').text("Show Less >>");
    }
});

var swiper = new Swiper(".swiper-banner-desktop, .swiper-banner-mobile", {
    pagination: {
        el: ".swiper-pagination",
    },
});

function FacebookButton(t) {
    var e = "";
    e = void 0 === t ? $("#full_urlscheme").val() : t;
    var o = $(window).width() / 2 - 300,
        n = $(window).height() / 2 - 200,
        i = window.open(
            "https://www.facebook.com/sharer/sharer.php?u=" + e,
            "popupWindow",
            "width=600, height=400, top=" + n + ", left=" + o + ", scrollbars=yes"
        );
    i.document.body;
}
function TwitterButton(t) {
    var e = "";
    if (
        ((e = void 0 === t ? $("#full_urlscheme").val() : t),
            /Android|webOS|iPhone|iPad|Mac|Macintosh|iPod|BlackBerry|IEMobile|Opera Mini/i.test(
                navigator.userAgent
            ))
    )
        window.location.href = "https://twitter.com/intent/tweet?url=" + e;
    else {
        var o = $(window).width() / 2 - 300,
            n = $(window).height() / 2 - 200,
            i = window.open(
                "https://twitter.com/intent/tweet?url=" + e,
                "popupWindow",
                "width=600, height=400, top=" + n + ", left=" + o + ", scrollbars=yes"
            );
        i.document.body;
    }
}
function LineButton(t) {
    var e = "";
    if (
        ((e = void 0 === t ? $("#full_urlscheme").val() : t),
            /Android|webOS|iPhone|iPad|Mac|Macintosh|iPod|BlackBerry|IEMobile|Opera Mini/i.test(
                navigator.userAgent
            ))
    )
        window.location.href = "line://msg/text/" + e;
    else {
        var o = $(window).width() / 2 - 300,
            n = $(window).height() / 2 - 200,
            i = window.open(
                "https://social-plugins.line.me/lineit/share?url=" + e,
                "popupWindow",
                "width=600, height=400, top=" + n + ", left=" + o + ", scrollbars=yes"
            );
        i.document.body;
    }
}


$(document).ready(function() {
    $('.gallery-item').each(function() {
      var me = $(this);
  
      me.attr('href', me.data('image'));
      me.attr('title', me.data('title'));
      if (me.parent().hasClass('gallery-fw')) {
        me.css({
          height: me.parent().data('item-height'),
        });
        me.find('div').css({
          lineHeight: me.parent().data('item-height') + 'px',
        });
      }
      me.css({
        backgroundImage: 'url("' + me.data('image') + '")',
      });
    });
    if (jQuery().Chocolat) {
      $('.gallery').Chocolat({
        className: 'gallery',
        imageSelector: '.gallery-item',
      });
    }
  });