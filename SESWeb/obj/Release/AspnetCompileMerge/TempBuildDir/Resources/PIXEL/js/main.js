

/* ------------------------------------------------------------------
                		PARALLAX REFRESH COMMAND
------------------------------------------------------------------ */
$(function () {
"use strict";
copyRight(); //copyRights
isNumber ();




});


/*if(!Modernizr.touch){
	$(window).stellar({
		responsive: true,
	    positionProperty: 'position',
	    horizontalScrolling: false
	});
}*/



(function($) {
    "use strict";
    var winwidth1 = jQuery(window).width();
    if (winwidth1 > 300) {
        jQuery(window).scroll(function() {
            if (jQuery(window).scrollTop() > 100) {
                jQuery('header').addClass('site-navbar-bg');
				// jQuery('.brand-logos').addClass('addlogo');
				//jQuery('.navbar a.navbar-brand img').attr('src','images/oriental-lotus-logo-element.png');

            } else {
                jQuery('header').removeClass('site-navbar-bg');
				//jQuery('.brand-logos').removeClass('addlogo');
				//jQuery('.navbar a.navbar-brand img').attr('src','images/oriental-lotus-logo.png');
            }
        });
    }


	$(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.scrollup').fadeIn();
        } else {
            $('.scrollup').fadeOut();
        }
    });

    $('.scrollup').click(function () {
        $("html, body").animate({
            scrollTop: 0
        }, 600);
        return false;
    });











	/* ==================================================================
					WoW Js
================================================================== */
    try {


        var wow = new WOW(
            {
                boxClass: 'appear',      // animated element css class (default is wow)
                animateClass: 'animated', // animation css class (default is animated)
                offset: 100,          // distance to the element when triggering the animation (default is 0)
                mobile: true,       // trigger animations on mobile devices (default is true)
                live: true,       // act on asynchronously loaded content (default is true)
                callback: function (box) {
                    // the callback is fired every time an animation is started
                    // the argument that is passed in is the DOM node being animated
                }
            }
        );
        wow.init();
    } catch (e) {

    }




	/* ==================================================================
					window heights
================================================================== */

	var $animation_elements = $('.effectsview');
	var $window = $(window);

	function check_if_in_view() {
		var window_height = $window.height();
		var window_top_position = $window.scrollTop();
		var window_bottom_position = (window_top_position + window_height);

		$.each($animation_elements, function() {
			var $element = $(this);
			var element_height = $element.outerHeight();
			var element_top_position = $element.offset().top;
			var element_bottom_position = (element_top_position + element_height);

			//check to see if this current container is within viewport
			if ((element_bottom_position >= window_top_position) &&
				(element_top_position <= window_bottom_position - 100)) {
				//(element_top_position <= window_bottom_position)) {
				$element.addClass('in-view');
			}
			else {
				$element.removeClass('in-view');
			}
		});
	}

	$window.on('scroll resize', check_if_in_view);
	$window.trigger('scroll');


	/* ==================================================================
					window heights ends
================================================================== */


	var thisScroll = 0, lastScroll = 0;
$(window).scroll(function(){
  thisScroll = $(window).scrollTop();
  if($('.quicklinks').offset().top - thisScroll <= 50 && !$('#stuckNav').length && thisScroll > lastScroll){
    var newNav = $('.quicklinks').clone();
    newNav.attr('id', 'stuckNav');
    $('body').append(newNav);
  }
  else if($('.quicklinks').offset().top - $(window).scrollTop() > 50 && thisScroll < lastScroll){
    $('#stuckNav').remove();
  }
  lastScroll = thisScroll;
});




	  //  dropdown menu hover show

  $(".dropdown").hover(
            function() {
                $('.dropdown-menu', this).stop( true, true ).fadeIn("fast");
                $(this).toggleClass('open');
                $('b', this).toggleClass("caret caret-up");
           },
            function() {
                $('.dropdown-menu', this).stop( true, true ).fadeOut("fast");
                $(this).toggleClass('open');
                $('b', this).toggleClass("caret caret-up");
            });



	//Submenu Dropdown Toggle
	if($('.main-header .navigation li.dropdown ul').length){
		$('.main-header .navigation li.dropdown').append('<div class="dropdown-btn"><Span class="fa fa-angle-down"></span></div>');

		//Dropdown Button
		$('.main-header li.dropdown .dropdown-btn').on('click', function() {
			$(this).prev('ul').slideToggle(500);
		});


		//Disable dropdown parent link
		//$('.navigation li.dropdown > a').on('click', function(e) {
		//	e. preventDefault();
		//});
	}



    /*$('.collapse').on('shown.bs.collapse', function() {
        $(this).parent().find(".glyphicon-plus").removeClass("glyphicon-plus").addClass("glyphicon-minus");
    }).on('hidden.bs.collapse', function() {
        $(this).parent().find(".glyphicon-minus").removeClass("glyphicon-minus").addClass("glyphicon-plus");
    });*/


$(".collapse.in").each(function(){
        	$(this).siblings(".panel-heading").find(".glyphicon").addClass("glyphicon-minus").removeClass("glyphicon-plus");
        });

        // Toggle plus minus icon on show hide of collapse element
        $(".collapse").on('show.bs.collapse', function(){
        	$(this).parent().find(".glyphicon").removeClass("glyphicon-plus").addClass("glyphicon-minus");
        }).on('hide.bs.collapse', function(){
        	$(this).parent().find(".glyphicon").removeClass("glyphicon-minus").addClass("glyphicon-plus");
        });



	$('.panel-heading a').click(function() {
	//"use strict"; // Start of use strict
    $('.panel-heading').removeClass('actives');
    $(this).parents('.panel-heading').addClass('actives');

    $('.panel-title').removeClass('actives'); //just to make a visual sense
    $(this).parent().addClass('actives'); //just to make a visual sense

   // alert($(this).parents('.panel-heading').attr('class'));
 });



	//"use strict"; // Start of use strict
   /* $('#accordion').on('shown.bs.collapse', function (e) {
		var offset = $(this).find('.collapse.in').prev('.panel-heading');
        if(offset) {
            $('html,body').animate({
                scrollTop: $(offset).offset().top -130
            }, 500);
        }
    });
*/



	/*$(window).scroll(function () {
		if ($(document).scrollTop() > 50){
			$('body').addClass('fixmenu');
		} else {
			$('body').removeClass('fixmenu');
		}
	});*/



	/*$('a.page-scroll').bind('click', function(event) {
		//alert("mani");
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: ($($anchor.attr('href')).offset().top - 70)
        }, 1250, 'easeOutCubic');
        event.preventDefault();
    });*/








})(jQuery);



$(document).ready(function() {
 "use strict";
  $(".js-example-basic-single").select2();
});



function isNumber(evt) {
	"use strict";
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function copyRight() {
	"use strict";
	var date = new Date();
	var year = date.getFullYear();
	document.getElementById('copyRightYear').innerHTML = year;
}


	$(document).ready(function () {



});

	/*page top*/

