﻿(function () {
    "use strict";

    angular.module("app-trips").controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {
        var vm = this;

        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStop = {};

        var url = "/api/trips/" + vm.tripName + "/stops";

        $http.get(url).then(function (response) {
            //Success
            angular.copy(response.data, vm.stops);
            _showMap(vm.stops);
        }, function (error) {
            //Failure
            vm.errorMessage = "Failed to load stops: " + error;
        }).finally(function () {
            vm.isBusy = false;
        });

        vm.addStop = function () {
            vm.isBusy = true;
            vm.errorMessage = "";
            
            $http.post(url, vm.newStop).then(function (response) {
                //Success
                vm.stops.push(response.data);
                _showMap(vm.stops);
                vm.newStop = {};
            }, function (error) {
                //Failure
                vm.errorMessage = "Failed to save new stop: " + error;
            }).finally(function () {
                vm.isBusy = false;
            });
        };
    }

    function _showMap(stops) {
        if (stops && stops.length > 0) {

            var mapStops = _.map(stops,
                function(item) {
                    return {
                        lat: item.latitude,
                        long: item.longitude,
                        info: item.city
                    };
                });

            //Show Map
            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currentStop: 1,
                initialZoom: 3
            });

            //var map = travelMap.createMap({
            //    stops: [
            //        {
            //            lat: 33.748995,
            //            long: -84.387982,
            //            info: "Atlanta, Georgia - Departed Jun 3, 2014"
            //        },
            //        {
            //            lat: 48.856614,
            //            long: 2.352222,
            //            info: "Paris, France - Jun 4-24, 2014"
            //        },
            //        {
            //            lat: 50.850000,
            //            long: 4.350000,
            //            info: "Brussels, Belgium - Jun 25-27, 2014"
            //        }
            //    ],
            //    selector: "#map"
            //});
        }
    }
})();