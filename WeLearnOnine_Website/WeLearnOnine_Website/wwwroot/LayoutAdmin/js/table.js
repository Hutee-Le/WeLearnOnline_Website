// Lấy tham chiếu đến toast
var courseSuccessToast = document.querySelector(".bs-toast.show");

// Tự động thêm class "fadeout-toast" sau 2 giây
setTimeout(function () {
    courseSuccessToast.classList.add("fadeout-toast");
    courseSuccessToast.classList.remove("show");
}, 2000);



document.querySelector("[name='Price'], [name='DiscountPrice']").addEventListener("input", function (e) {
    var priceInput = e.target;
    var priceValue = priceInput.value;

    // Loại bỏ tất cả các ký tự không phải là số và dấu phẩy
    priceValue = priceValue.replace(/[^0-9,]/g, '');

    // Loại bỏ dấu phẩy cũ và thêm dấu phẩy sau mỗi 3 số
    priceValue = priceValue.replace(/,/g, "");
    priceValue = addCommas(priceValue);

    // Cập nhật giá trị trên input
    priceInput.value = priceValue;

    // Nếu là DiscountPrice, cập nhật giá trị model để không có dấu phẩy
    if (priceInput.name === 'DiscountPrice') {
        var modelProperty = priceInput.getAttribute('asp-for');
        var modelValue = priceValue.replace(/,/g, '');
        document.querySelector("[asp-for='" + modelProperty + "']").value = modelValue;
    }
});

// Hàm thêm dấu phẩy sau mỗi 3 số
function addCommas(value) {
    return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
