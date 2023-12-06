///*--------------------------------------------------------------------
//        ********** Page Loader
//---------------------------------------------------------------------- */
//window.addEventListener("load", () => {
//    document.querySelector(".js-page-loader").classList.add("fade-out");
//    setTimeout(() => {
//        document.querySelector(".js-page-loader").style.display = "none";
//    }, 2000);
//});

////// Đường dẫn mặc định cho CSS
////const defaultCssPath = "~/css/colors/color-1.css";

////// Tìm phần tử <link> với đường dẫn CSS mặc định
////const link = document.querySelector(`link[href="${defaultCssPath}"]`);

////// Kiểm tra xem phần tử link có tồn tại không
////if (link) {
////  // Thêm lớp (class) mới vào phần tử <link>
////  link.classList.add('js-color-style');
////} else {
////  // Tạo một phần tử <link> mới nếu không tìm thấy
////  const newLink = document.createElement('link');
////  newLink.rel = 'stylesheet';
////  newLink.href = defaultCssPath;
////  newLink.classList.add('js-color-style');
////  document.head.appendChild(newLink);
////}

//// Đường dẫn mặc định cho CSS
//const defaultCssPath = "/css/colors/color-1.css";

//// Tìm phần tử <link> với đường dẫn CSS mặc định
//const link = document.querySelector(`link[href="${defaultCssPath}"]`);

//// Kiểm tra xem phần tử link có tồn tại không
//if (link) {
//    // Thêm lớp (class) mới vào phần tử <link>
//    link.classList.add('js-color-style');
//} else {
//    // Tạo một phần tử <link> mới nếu không tìm thấy
//    const newLink = document.createElement('link');
//    newLink.rel = 'stylesheet';
//    newLink.href = defaultCssPath;
//    newLink.classList.add('js-color-style');
//    document.head.appendChild(newLink);
//}


///*--------------------------------------------------------------------
//        ********** Style switcher
//---------------------------------------------------------------------- */
//function styleSwitcherToggle() {
//  const styleSwitcher = document.querySelector('.js-style-switcher'),
//    styleSwitcherToggler = document.querySelector('.js-style-switcher-toggler');

//  styleSwitcherToggler.addEventListener("click", function () {
//    styleSwitcher.classList.toggle("open");
//    this.querySelector("i").classList.toggle("fa-times");
//    this.querySelector("i").classList.toggle("fa-cog");
//  })
//}
//styleSwitcherToggle();

///*--------------------------------------------------------------------
//        ********** Theme colors
//---------------------------------------------------------------------- */
//function themeColors() {
//    const colorStyle = document.querySelector(".js-color-style"),
//        themeColorsContainer = document.querySelector(".js-theme-colors");

//    themeColorsContainer.addEventListener("click", ({ target }) => {
//        if (target.classList.contains("js-theme-color-item")) {
//            localStorage.setItem("color", target.getAttribute("data-js-theme-color"));
//            setColor();
//        }
//    });

//    function setColor() {
//        let path = colorStyle.getAttribute("href").split("/");
//        path = path.slice(0, path.length - 1);
//        colorStyle.setAttribute("href", path.join("/") + "/" + localStorage.getItem("color") + ".css");

//        if (document.querySelector(".js-theme-color-item.active")) {
//            document.querySelector(".js-theme-color-item.active").classList.remove("active");
//        }
//        document.querySelector("[data-js-theme-color=" + localStorage.getItem("color") + "]").classList.add("active");
//    }

//    if (localStorage.getItem("color") != null) {
//        setColor();
//    }
//    else {
//        const defaultColor = colorStyle.getAttribute("href");
//        document.querySelector("[data-js-theme-color=" + defaultColor + "]").classList.add("active");
//    }
//}
//themeColors();


///*--------------------------------------------------------------------
//        ********** Theme light & dark mode
//---------------------------------------------------------------------- */
//function themeLightDark() {
//  const darkModeCheckbox = document.querySelector(".js-dark-mode");

//  darkModeCheckbox.addEventListener("click", function () {
//    if (this.checked) {
//      localStorage.setItem("theme-dark", "true");
//    } else {
//      localStorage.setItem("theme-dark", "false");
//    }
//    themeMode();
//  });

//  function themeMode() {
//    if (localStorage.getItem("theme-dark") === "false") {
//      document.body.classList.remove("t-dark");
//    } else {
//      document.body.classList.add("t-dark");
//    }
//  }

//  if (localStorage.getItem("theme-dark") !== null) {
//    themeMode();
//  }
//  if (document.body.classList.contains("t-dark")) {
//    darkModeCheckbox.checked = true;
//  }
//}
//themeLightDark();

///*--------------------------------------------------------------------
//    ********** Auto increase
//---------------------------------------------------------------------- */
////let nums = document.querySelectorAll(".box .fun-facts-item .counter");
////let section = document.querySelector(".fun-facts-section");
////let windowHeight = window.innerHeight;
////let started = false;

////window.addEventListener('scroll', function () {
////    if (!started && isElementInViewport(section)) {
////        nums.forEach((num) => startCount(num));
////        started = true;
////    }
////});

////function isElementInViewport(element) {
////    let rect = element.getBoundingClientRect();
////    return (
////        rect.top >= 0 &&
////        rect.bottom <= (window.innerHeight || document.documentElement.clientHeight)
////    );
////}

////function startCount(el) {
////    let goal = parseInt(el.dataset.goal);
////    let currentCount = 0;
////    let increment = Math.ceil(goal / 70);  // Adjust the increment as needed

////    let countInterval = setInterval(() => {
////        currentCount += increment;
////        if (currentCount > goal) {
////            currentCount = goal;
////            clearInterval(countInterval);
////        }

////        // Extract the existing content and spans
////        let existingContent = el.innerHTML;
////        let existingSpan = existingContent.match(/<span>[^<]*<\/span>/);

////        // Update the numerical count and reinsert the spans
////        el.innerHTML = currentCount + (existingSpan ? existingSpan[0] : '');
////    }, 100);
////}
