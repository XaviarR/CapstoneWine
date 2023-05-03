// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.body.onmousemove = function (e) {
    document.documentElement.style.setProperty(
        '--x', (e.clientX + window.scrollX) + 'px');
    document.documentElement.style.setProperty(
        '--y', (e.clientY + window.scrollY) + 'px');
}