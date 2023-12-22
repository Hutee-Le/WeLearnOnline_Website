/*--------------------------------------------------------------------
        ********** Page Loader
---------------------------------------------------------------------- */
window.addEventListener("load", () => {
    document.querySelector(".js-page-loader").classList.add("fade-out");
    setTimeout(() => {
        document.querySelector(".js-page-loader").style.display = "none";
    }, 2000);
});

// Đường dẫn mặc định cho CSS
const defaultCssPath = "/css/colors/color-1.css";

// Tìm phần tử <link> với đường dẫn CSS mặc định
const link = document.querySelector(`link[href="${defaultCssPath}"]`);

// Kiểm tra xem phần tử link có tồn tại không
if (link) {
    // Thêm lớp (class) mới vào phần tử <link>
    link.classList.add('js-color-style');
} else {
    // Tạo một phần tử <link> mới nếu không tìm thấy
    const newLink = document.createElement('link');
    newLink.rel = 'stylesheet';
    newLink.href = defaultCssPath;
    newLink.classList.add('js-color-style');
    document.head.appendChild(newLink);
}


/*--------------------------------------------------------------------
        ********** Style switcher
---------------------------------------------------------------------- */
function styleSwitcherToggle() {
    const styleSwitcher = document.querySelector('.js-style-switcher'),
        styleSwitcherToggler = document.querySelector('.js-style-switcher-toggler');

    styleSwitcherToggler.addEventListener("click", function () {
        styleSwitcher.classList.toggle("open");
        this.querySelector("i").classList.toggle("fa-times");
        this.querySelector("i").classList.toggle("fa-cog");
    })
}
styleSwitcherToggle();

/*--------------------------------------------------------------------
        ********** Theme colors
---------------------------------------------------------------------- */
function themeColors() {
    const colorStyle = document.querySelector(".js-color-style"),
        themeColorsContainer = document.querySelector(".js-theme-colors");

    themeColorsContainer.addEventListener("click", ({ target }) => {
        if (target.classList.contains("js-theme-color-item")) {
            localStorage.setItem("color", target.getAttribute("data-js-theme-color"));
            setColor();
        }
    });

    function setColor() {
        let path = colorStyle.getAttribute("href").split("/");
        path = path.slice(0, path.length - 1);
        colorStyle.setAttribute("href", path.join("/") + "/" + localStorage.getItem("color") + ".css");

        if (document.querySelector(".js-theme-color-item.active")) {
            document.querySelector(".js-theme-color-item.active").classList.remove("active");
        }
        document.querySelector("[data-js-theme-color=" + localStorage.getItem("color") + "]").classList.add("active");
    }

    if (localStorage.getItem("color") != null) {
        setColor();
    }
    else {
        const defaultColor = colorStyle.getAttribute("href");
        document.querySelector("[data-js-theme-color=" + defaultColor + "]").classList.add("active");
    }
}
themeColors();


/*--------------------------------------------------------------------
        ********** Theme light & dark mode
---------------------------------------------------------------------- */
function themeLightDark() {
    const darkModeCheckbox = document.querySelector(".js-dark-mode");

    darkModeCheckbox.addEventListener("click", function () {
        if (this.checked) {
            localStorage.setItem("theme-dark", "true");
        } else {
            localStorage.setItem("theme-dark", "false");
        }
        themeMode();
    });

    function themeMode() {
        if (localStorage.getItem("theme-dark") === "false") {
            document.body.classList.remove("t-dark");
        } else {
            document.body.classList.add("t-dark");
        }
    }

    if (localStorage.getItem("theme-dark") !== null) {
        themeMode();
    }
    if (document.body.classList.contains("t-dark")) {
        darkModeCheckbox.checked = true;
    }
}
themeLightDark();

/*--------------------------------------------------------------------
    ********** Auto increase
---------------------------------------------------------------------- */
//let nums = document.querySelectorAll(".box .fun-facts-item .counter");
//let section = document.querySelector(".fun-facts-section");
//let windowHeight = window.innerHeight;
//let started = false;

//window.addEventListener('scroll', function () {
//    if (!started && isElementInViewport(section)) {
//        nums.forEach((num) => startCount(num));
//        started = true;
//    }
//});

//function isElementInViewport(element) {
//    let rect = element.getBoundingClientRect();
//    return (
//        rect.top >= 0 &&
//        rect.bottom <= (window.innerHeight || document.documentElement.clientHeight)
//    );
//}

//function startCount(el) {
//    let goal = parseInt(el.dataset.goal);
//    let currentCount = 0;
//    let increment = Math.ceil(goal / 70);  // Adjust the increment as needed

//    let countInterval = setInterval(() => {
//        currentCount += increment;
//        if (currentCount > goal) {
//            currentCount = goal;
//            clearInterval(countInterval);
//        }

//        // Extract the existing content and spans
//        let existingContent = el.innerHTML;
//        let existingSpan = existingContent.match(/<span>[^<]*<\/span>/);

//        // Update the numerical count and reinsert the spans
//        el.innerHTML = currentCount + (existingSpan ? existingSpan[0] : '');
//    }, 100);
//}

/*--------------------------------------------------------------------
        ********** Testimonial slider
---------------------------------------------------------------------- */
function TestimonialSlider() {
    const carouselOne = document.getElementById('carouselOne');
    if (carouselOne) {
        carouselOne.addEventListener('slide.bs.carousel', function () {
            const activeItem = this.querySelector(".active");
            document.querySelector(".js-testimonial-img").src = activeItem.getAttribute("data-js-testimonial-img");
        })
    }
}
TestimonialSlider();

/*--------------------------------------------------------------------
        ********** Course preview video
---------------------------------------------------------------------- */
function coursePreviewVideo() {
    const coursePreviewModal = document.querySelector('.js-course-preview-modal');
    if (coursePreviewModal) {
        coursePreviewModal.addEventListener('shown.bs.modal', function () {
            this.querySelector(".js-course-preview-video").play();
            this.querySelector(".js-course-preview-video").currentTime = 0;
        });

        coursePreviewModal.addEventListener('hide.bs.modal', function () {
            this.querySelector(".js-course-preview-video").pause();
        });
    }
}


coursePreviewVideo()


/*--------------------------------------------------------------------
        ********** Tooltip user Profile
---------------------------------------------------------------------- */
const subMenuUser = document.getElementById("subMenuUser");
function menuToggle() {
    console.log("hi")
    subMenuUser.classList.toggle('open-menu');
}
// menuToggle();


// -----------------------------------------------------------------------------------\
/*--------------------------------------------------------------------
        ********** Swiper slider: COURSES
---------------------------------------------------------------------- */
// -----------------------------------------------------------------------------------/
var swiper = new Swiper(".content", {
    slidesPerView: 6,
    spaceBetween: 10,
    freeMode: true,
    pagination: {
        el: ".swiper-pagination",
        clickable: true,
    },
});
/*--------------------------------------------------------------------
        ********** Video Play list
---------------------------------------------------------------------- */
//const main_video = document.querySelector('.main-video video');
//const main_video_title = document.querySelector('.main-video .title');
//const video_playlist = document.querySelector('.video-playlist .videos');

//let data = [
//    {
//        'id': 'a1',
//        'title': 'manipulate text background',
//        'name': 'manipulate text background.mp4',
//        'duration': '2:47',
//    },
//    {
//        'id': 'a2',
//        'title': 'build gauge with css',
//        'name': 'build gauge with css.mp4',
//        'duration': '2:45',
//    },
//    {
//        'id': 'a3',
//        'title': '3D popup card',
//        'name': '3D popup card.mp4',
//        'duration': '24:49',
//    },

//    {
//        'id': 'a4',
//        'title': 'customize HTML5 form elements',
//        'name': 'customize HTML5 form elements.mp4',
//        'duration': '3:59',
//    },
//    {
//        'id': 'a5',
//        'title': 'custom select box',
//        'name': 'custom select box.mp4',
//        'duration': '4:25',
//    },
//    {
//        'id': 'a6',
//        'title': 'embed google map to contact form',
//        'name': 'embed google map to contact form.mp4',
//        'duration': '5:33',
//    },
//    {
//        'id': 'a7',
//        'title': 'password strength checker javascript web app',
//        'name': 'password strength checker javascript web app.mp4',
//        'duration': '0:29',
//    },

//    {
//        'id': 'a8',
//        'title': 'custom range slider',
//        'name': 'custom range slider.mp4',
//        'duration': '1:12',
//    },
//    {
//        'id': 'a9',
//        'title': 'animated shopping cart',
//        'name': 'animated shopping cart.mp4',
//        'duration': '3:38',
//    },

//];

//data.forEach((video, i) => {
//    let video_element = `
//                <div class="video" data-id="${video.id}">
//                    <img src="img/play.svg" alt="">
//                    <p class="vid-p">${i + 1 > 9 ? i + 1 : '0' + (i + 1)}. </p>
//                    <h3 class="title">${video.title}</h3>
//                    <p class="time">${video.duration}</p>
//                </div>
//    `;
//    video_playlist.innerHTML += video_element;
//})

//let videos = document.querySelectorAll('.video');
//videos[0].classList.add('active');
//videos[0].querySelector('img').src = 'img/pause.svg';

//videos.forEach(selected_video => {
//    selected_video.onclick = () => {

//        for (all_videos of videos) {
//            all_videos.classList.remove('active');
//            all_videos.querySelector('img').src = 'img/play.svg';

//        }

//        selected_video.classList.add('active');
//        selected_video.querySelector('img').src = 'img/pause.svg';

//        let match_video = data.find(video => video.id == selected_video.dataset.id);
//        main_video.src = 'videos/' + match_video.name;
//        main_video_title.innerHTML = match_video.title;
//    }
//});

///*--------------------------------------------------------------------
//        ********** Testimonial slider
//---------------------------------------------------------------------- */
//function TestimonialSlider() {
//    const carouselOne = document.getElementById('carouselOne');
//    if (carouselOne) {
//        carouselOne.addEventListener('slide.bs.carousel', function () {
//            const activeItem = this.querySelector(".active");
//            document.querySelector(".js-testimonial-img").src = activeItem.getAttribute("data-js-testimonial-img");
//        })
//    }
//}
//TestimonialSlider();

///*--------------------------------------------------------------------
//        ********** Course preview video
//---------------------------------------------------------------------- */
//function coursePreviewVideo() {
//    const coursePreviewModal = document.querySelector('.js-course-preview-modal');
//    if (coursePreviewModal) {
//        coursePreviewModal.addEventListener('shown.bs.modal', function () {
//            this.querySelector(".js-course-preview-video").play();
//            this.querySelector(".js-course-preview-video").currentTime = 0;
//        });

//        coursePreviewModal.addEventListener('hide.bs.modal', function () {
//            this.querySelector(".js-course-preview-video").pause();
//        });
//    }
//}


//coursePreviewVideo()


///*--------------------------------------------------------------------
//        ********** Tooltip user Profile
//---------------------------------------------------------------------- */
//const subMenuUser = document.getElementById("subMenuUser");
//function menuToggle() {
//    console.log("hi")
//    subMenuUser.classList.toggle('open-menu');
//}
//// menuToggle();


//// -----------------------------------------------------------------------------------\
///*--------------------------------------------------------------------
//        ********** Swiper slider: COURSES
//---------------------------------------------------------------------- */
//// -----------------------------------------------------------------------------------/
//var swiper = new Swiper(".content", {
//    slidesPerView: 6,
//    spaceBetween: 10,
//    freeMode: true,
//    pagination: {
//        el: ".swiper-pagination",
//        clickable: true,
//    },
//});
//// -----------------------------------------------------------------------------------\
///*--------------------------------------------------------------------
//        ********** Slick slider: COURSES
//---------------------------------------------------------------------- */
//// -----------------------------------------------------------------------------------/
