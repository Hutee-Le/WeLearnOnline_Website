// Lấy tham chiếu đến toast
var courseSuccessToast = document.querySelector(".bs-toast.show");

// Tự động thêm class "fadeout-toast" sau 2 giây
setTimeout(function () {
    courseSuccessToast.classList.add("fadeout-toast");
    courseSuccessToast.classList.remove("show");
}, 2000);