/* jQuery Star Rating Plugin
 * 
 * @Author
 * Copyright Nov 02 2010, Irfan Durmus - http://irfandurmus.com/
 *
 * @Version
 * 0.3b
 *
 * @License
 * Dual licensed under the MIT or GPL Version 2 licenses.
 *
 * Visit the plugin page for more information.
 * http://irfandurmus.com/projects/jquery-star-rating-plugin/
 *
 */

(function ($) {
    $.fn.rating = function (callback) {

        callback = callback || function () { };

        // each for all item
        this.each(function (i, v) {
            $(v).data('rating', { callback: callback })
                .bind('init.rating', $.fn.rating.init)
                .bind('set.rating', $.fn.rating.set)
                .bind('hover.rating', $.fn.rating.hover)
                .trigger('init.rating');
        });
    };

    $.extend($.fn.rating, {
        init: function (e) {
            var el = $(this),
                list = '',
                isChecked = null,
                childs = el.children(),
                i = 0,
                l = childs.length;

            for (; i < l; i++) {
                list = list + '<a class="star" title="' + $(childs[i]).val() + '" />';
                if ($(childs[i]).is(':checked')) {
                    isChecked = $(childs[i]).val();
                };
            };

            childs.hide();

            el
                .append('<div class="stars">' + list + '</div>')
                .trigger('set.rating', isChecked);

            $('a', el).bind('click', $.fn.rating.click);
            el.trigger('hover.rating');
        },
        set: function (e, val) {
            var el = $(this),
                item = $('a', el),
                input = undefined;

            if (val) {
                item.removeClass('fullStar');

                input = item.filter(function (i) {
                    if ($(this).attr('title') == val)
                        return $(this);
                    else
                        return false;
                });

                input
                    .addClass('fullStar')
                    .prevAll()
                    .addClass('fullStar');
            }

            return;
        },
        hover: function (e) {
            var el = $(this),
                stars = $('a', el);

            stars.bind('mouseenter', function (e) {
                // add tmp class when mouse enter
                $(this)
                    .addClass('tmp_fs')
                    .prevAll()
                    .addClass('tmp_fs');

                $(this).nextAll()
                    .addClass('tmp_es');
            });


            stars.bind('mouseleave', function (e) {
                // remove all tmp class when mouse leave
                $(this)
                    .removeClass('tmp_fs')
                    .prevAll()
                    .removeClass('tmp_fs');

                $(this).nextAll()
                    .removeClass('tmp_es');
            });
        },
        click: function (e) {

            e.preventDefault();
            var el = $(e.target),
                container = el.parent().parent(),
                inputs = container.children('input'),
                rate = el.attr('title');

            matchInput = inputs.filter(function (i) {
                if ($(this).val() == rate)
                    return true;
                else
                    return false;
            });

            matchInput.attr('checked', true);

            container
                .trigger('set.rating', matchInput.val())
                .data('rating').callback(rate, e);

        }
    });

})(jQuery);
///////////////////////
// 
/*
    <section class="container">
        <input type="radio" name="example" class="rating" value="1" />
        <input type="radio" name="example" class="rating" value="2" />
        <input type="radio" name="example" class="rating" value="3" />
        <input type="radio" name="example" class="rating" value="4" />
        <input type="radio" name="example" class="rating" value="5" />
		<input type="radio" name="example" class="rating" value="6" />
    </section>
*/
function MakeRatingBar(name, nbStars, value, disable) {
    var nStars = (nbStars != undefined ? nbStars : 5);
    var ratingBar = null;
    if (!disable) {
        ratingBar = $("<section class='RatingBarContainer' style='min-width:105px'></section>");
        for (var starIndex = 1; starIndex <= nStars; starIndex++) {
            if (value == starIndex) {
                ratingBar.append($("<input type='radio' name='" + name + "' class='rating' value='" + starIndex + "' checked />"));
            }
            else {
                ratingBar.append($("<input type='radio' name='" + name + "' class='rating' value='" + starIndex + "' />"));
            }
        }
    }
    else {
        ratingBar = $("<div style='min-width:105px'></div>");
        var fullStar = (value > 0);
        for (var starIndex = 1; starIndex <= nStars; starIndex++) {
            if (fullStar) {
                ratingBar.append($("<a class='star fullStar'></a>"));
            }
            else {
                ratingBar.append($("<a class='star'></a>"));
            }
            if (value == starIndex)
                fullStar = false;
        }
    }
    return ratingBar;
}

$(document).ready(
    function () {
        $(".RatingBar").each(
            function () {
                var name = $(this).attr('name');
                var nstars = $(this).attr('nstars');
                var disable = $(this).attr('disabled');
                var value = $(this).val();
                var RatingBar = MakeRatingBar(name, nstars, value, disable);
                $(this).replaceWith(RatingBar);
            }
        )
        $('.RatingBarContainer').rating();
    }
)
