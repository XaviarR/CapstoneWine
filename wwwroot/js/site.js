// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Mouse Functionality
document.body.onmousemove = function(e) {
    document.documentElement.style.setProperty (
        '--x', (e.clientX+window.scrollX) + 'px');
    document.documentElement.style.setProperty (
        '--y', (e.clientY+window.scrollY) + 'px');
}

$(function () {
    $('a').hover(function () {
        $('#invertedcursor').css('height', '40px');
        $('#invertedcursor').css('width', '40px');
        $('#invertedcursor').css('transition', 'height .5s, width .5s');

        


    }, function () {
        // on mouseout, reset the cursor size
        $('#invertedcursor').css('height', '70px');
        $('#invertedcursor').css('width', '70px');
        $('#invertedcursor').css('transition', 'height .5s width .5s')

        
    });
    $('button').hover(function () {
        $('#invertedcursor').css('height', '40px');
        $('#invertedcursor').css('width', '40px');
        $('#invertedcursor').css('transition', 'height .5s, width .5s');

    }, function () {
        // on mouseout, reset the cursor size
        $('#invertedcursor').css('height', '70px');
        $('#invertedcursor').css('width', '70px');
        $('#invertedcursor').css('transition', 'height .5s width .5s')
    });
});

// Navbar 1
const hamburger = document.querySelector(".hamburger");
const navMenu = document.querySelector(".nav-menu");

hamburger.addEventListener("click", () => {
    hamburger.classList.toggle("active");
    navMenu.classList.toggle("active");
})

document.addEventListener("click", (event) => {
    const isClickInsideNav = navMenu.contains(event.target);
    const isClickOnHamburger = hamburger.contains(event.target);

    if (!isClickInsideNav && !isClickOnHamburger) {
        hamburger.classList.remove("active");
        navMenu.classList.remove("active");
    }
});

