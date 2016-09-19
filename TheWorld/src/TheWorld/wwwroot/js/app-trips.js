(function() {
    "use strict";

    //Creating Module
    angular.module("app-trips", ["simpleControls", "ngRoute"])
        .config(function($routeProvider) {
            $routeProvider.when("/",
            {
                controller: "tripsController",
                controllerAs: "viewModel",
                templateUrl: "/views/tripsView.html"
            });

            $routeProvider.when("/editor/:tripName",
            {
                controller: "tripEditorController",
                controllerAs: "vm",
                templateUrl: "/views/tripEditorView.html"
            });

            $routeProvider.otherwise({ redirectTo: "/" });
        });
})();