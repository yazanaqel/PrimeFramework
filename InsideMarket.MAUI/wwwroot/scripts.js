window.goToSlide = function (index) {
    const slides = document.querySelectorAll('.slide');

    slides.forEach(s => s.classList.remove('active'));
    slides[index].classList.add('active');
};

window.sliderIndex = 0;

window.nextSlide = function () {
    const slides = document.querySelectorAll('.slide');
    window.sliderIndex = (window.sliderIndex + 1) % slides.length;
    window.goToSlide(window.sliderIndex);
};

window.prevSlide = function () {
    const slides = document.querySelectorAll('.slide');
    window.sliderIndex = (window.sliderIndex - 1 + slides.length) % slides.length;
    window.goToSlide(window.sliderIndex);
};


window.enableSwipe = function () {
    let startX = 0;

    document.addEventListener("touchstart", e => {
        startX = e.touches[0].clientX;
    });

    document.addEventListener("touchend", e => {
        const endX = e.changedTouches[0].clientX;
        const diff = endX - startX;

        if (diff > 50) window.prevSlide();
        if (diff < -50) window.nextSlide();
    });
};
