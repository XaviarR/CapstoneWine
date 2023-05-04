// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Mouse Functionality
document.body.onmousemove = function(e) {
    document.documentElement.style.setProperty (
        '--x', (e.clientX+window.scrollX) + 'px');
    document.documentElement.style.setProperty (
        '--y', (e.clientY+window.scrollY) + 'px');
    }