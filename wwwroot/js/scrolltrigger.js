const wines = [wine1, wine2, wine3, wine4, wine5, wine6, wine7, wine8];
const boxes = [box1, box2, box3, box4, box5, box6, box7, box8];

for (let i = 0; i < wines.length; i++) {
    gsap.to(wines[i], {
        scrollTrigger: {
            trigger: boxes[i],
            start: "top bottom",
            end: "center center",
            //markers: true,   //uncomment to visualize
            scrub: true,
        },
        x: "0%",
    });
}

