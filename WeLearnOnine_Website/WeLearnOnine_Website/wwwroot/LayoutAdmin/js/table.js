
    document.addEventListener("DOMContentLoaded", function () {
        var tdElements = document.querySelectorAll("td.hidetext");

    tdElements.forEach(function (td) {
            var p = td.querySelector("p");
    var button = td.querySelector(".expand-button");
            
            if (p.scrollHeight > p.clientHeight) {
        button.style.display = "block";
    button.addEventListener("click", function () {
                    if (td.classList.contains("expanded")) {
        td.classList.remove("expanded");
    button.innerHTML = "&#9660;";
                    } else {
        td.classList.add("expanded");
    button.innerHTML = "&#9650;";
                    }
                });
            }
        });
    });

