const wines = [wine1, wine2, wine3, wine4];
const boxes = [box1, box2, box3, box4];

for (let i = 0; i < wines.length; i++) {
    gsap.to(wines[i], {
        scrollTrigger: {
            trigger: boxes[i],
            start: "top bottom",
            end: "center center",
            //markers: true,   //uncomment to visualize
            scrub: true,
        },
        x: "50%",
    });
}