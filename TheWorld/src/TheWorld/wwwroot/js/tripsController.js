(function () {
    "use strict";

    //Getting Module
    angular.module("app-trips").controller("tripsController", tripsController);

    function tripsController() {
        var viewModel = this;

        viewModel.name = "Riyant";
    }

})();