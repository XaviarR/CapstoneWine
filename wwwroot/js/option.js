// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

let listItems = [...document.querySelectorAll('li')];

let options = {
    rootMargin: '-10%',
    threshold: 0.0
}

let observer = new IntersectionObserver(showItem, options);

function showItem(entries) {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.children[0].classList.add('active')
        }
    })
}

listItems.forEach(item => {
    let newString = '';
    let itemText = item.children[0].innerText.split('');

    //itemText.map(letter => (newString += letter ==' ' ? '<span class="gap"></span>' : '<span>${letter}</span>'))
    //item.innerHTML = newString;
    observer.observe(item);
})