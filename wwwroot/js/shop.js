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
	let counter = 0;
	console.log(conWidth);

	next.addEventListener('click', () => {
		counter++;
		if (counter >= itemCount) counter = 0;
		slide.style.transform = `translateX(-${(boxWidth * counter) + (counter * 47)}px)`;
		console.log(counter);
	});
	prev.addEventListener('click', () => {
		counter--;
		if (counter < 0) counter = 0;
		slide.style.transform = `translateX(-${(boxWidth * counter) + (counter * 47)}px)`;
		console.log(counter);
	});
}

const popout = document.querySelector(".filter-popout");
const toggler = document.querySelector(".toggler");

toggler.addEventListener("click", () => {
	popout.classList.toggle("show");
	console.log("work pls");
});
