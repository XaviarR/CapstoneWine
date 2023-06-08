const prevButtons = document.querySelectorAll(".slide-button#prv");
const nextButtons = document.querySelectorAll(".slide-button#nxt");
const sliders = document.querySelectorAll("#slider");
const boxWidth = document.querySelector(".product-box").offsetWidth;
const conWidth = document.getElementById("container").clientWidth;

for (let i = 0; i < sliders.length; i++) {
    const prev = prevButtons[i];
    const next = nextButtons[i];
    const slide = sliders[i];
    const itemCount = slide.children.length-1;
    let margin = 47;
    let counter = 0;

    next.addEventListener('click', () => {
        counter++;
        if (counter >= itemCount) counter = 0;
        slide.style.transform = `translateX(-${(boxWidth * counter) + (counter * margin)}px)`;
        console.log(counter);
    });
    prev.addEventListener('click', () => {
        counter--;
        if (counter < 0) counter = 0;
        slide.style.transform = `translateX(-${(boxWidth * counter) + (counter * margin)}px)`;
        console.log(counter);
    });
}

const filterLinks = document.querySelectorAll(".filter-link");
const hiders = document.querySelectorAll(".hider");

filterLinks.forEach((link, index) => {
    link.addEventListener('click', () => {
        hiders[index].classList.toggle("show");
    });

    document.addEventListener('click', (event) => {
        const isClickedInside = link.contains(event.target);
        if (!isClickedInside) {
            hiders[index].classList.remove("show");
        }
    });
});


document.querySelector(".theme-toggle-btn").addEventListener('click', () => {
    document.body.classList.toggle("light");
});