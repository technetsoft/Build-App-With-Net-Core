//site.js

(function () {

    var $sidebarAndWrapper = $("#sidebar,#wrapper");

    $("#sidebarToggle").on("click", function() {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
    });


    //var ele = $("#username");
    //ele.text("Riyant Kurniawan");

    //var main = $("#main");
    //main.on("mouseenter", function () {
    //    main.style = "background-color: #888;";
    //});

    //main.on("mouseleave", function () {
    //    main.style = "";
    //});

    //var menuItems = $("ul.menu li a");
    //menuItems.on("click", function() {
    //    var me = $(this);
    //    alert(me.text());
    //});

    //var ele = document.getElementById("username");
    //ele.innerHTML = "Riyant K";

    //var main = document.getElementById("main");

    //main.onmouseenter = function() {
    //    //main.style.backgroundColor = "#888";
    //    main.style = "background-color: #888;";
    //};

    //main.onmouseleave = function() {
    //    //main.style.backgroundColor = "";
    //    main.style = "";
    //};
})();