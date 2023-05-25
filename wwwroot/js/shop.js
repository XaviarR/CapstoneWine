const prevButtons = document.querySelectorAll(".slide-button#prv");
const nextButtons = document.querySelectorAll(".slide-button#nxt");
const sliders = document.querySelectorAll("#slider");
const boxWidth = document.querySelector(".product-box").offsetWidth;
const conWidth = document.getElementById("container").clientWidth;

for (let i = 0; i < sliders.length; i++) {
    const prev = prevButtons[i];
    const next = nextButtons[i];
    const slide = sliders[i];
    const itemCount = slide.children.length;
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

const popout = document.querySelector(".filter-popout");
const fBox = document.querySelector(".filter-box");
const filterBtn = document.querySelector(".toggler");

filterBtn.addEventListener("click", () => {
    fBox.classList.toggle("show");
    popout.classList.toggle("show");
});

const filterLink = document.querySelectorAll(".filter-title");
const listBox = document.querySelectorAll(".list-box");

for (var i = 0; i < filterLink.length; i++) {
    const link = filterLink[i];
    const list = listBox[i];

    link.addEventListener("click", () => {
        list.classList.toggle("show");
    });

}

