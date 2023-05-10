﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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
        $('#invertedcursor').css('height', '60px');
        $('#invertedcursor').css('width', '60px');
        $('#invertedcursor').css('transition', 'height .8s, width .8s');


    }, function () {
        // on mouseout, reset the background colour
        $('#invertedcursor').css('height', '100px');
        $('#invertedcursor').css('width', '100px');
        $('#invertedcursor').css('transition', 'height .8s width .8s')
    });
});

// Navbar 1
const hamburger = document.querySelector(".hamburger");
const navMenu = document.querySelector(".nav-menu");

hamburger.addEventListener("click", () => {
    hamburger.classList.toggle("active");
    navMenu.classList.toggle("active");
})

//Navbar 2
function menuOnClick() {
    document.getElementById("menu-bar").classList.toggle("change");
    document.getElementById("nav").classList.toggle("change");
    document.getElementById("menu-bg").classList.toggle("change-bg");
}