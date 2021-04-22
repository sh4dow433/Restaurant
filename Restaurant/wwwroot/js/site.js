var navbar = document.getElementById("navbar");
var sticky = navbar.offsetTop;

$(document).ready(function () {
    $(document).on("scroll", onScroll);


    $('a[href^="#"]').on('click', function (e) {
        e.preventDefault();
        $(document).off("scroll");

        // $('a').each(function () {
        //     $(this).removeClass('active');
        // })
        // $(this).addClass('active');

        var target = this.hash,
            menu = target;
        $target = $(target);
        $('html, body').stop().animate({
            'scrollTop': $target.offset().top + 2
        }, 500, 'swing', function () {
            window.location.hash = target;
            $(document).on("scroll", onScroll);
        });
    });
});


function onScroll(event) {
    if (window.pageYOffset >= sticky) {
        navbar.classList.add("fixed-top")
    } else {
        navbar.classList.remove("fixed-top");
    }

}
