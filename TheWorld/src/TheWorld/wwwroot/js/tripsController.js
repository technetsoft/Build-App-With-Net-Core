﻿(function () {
    "use strict";

    //Getting Module
    angular.module("app-trips").controller("tripsController", tripsController);

    function tripsController($http) {
        var viewModel = this;

        //viewModel.trips = [
        //{
        //    name: "US Trip",
        //    created: new Date()
        //}, {
        //    name: "World Trip",
        //    created: new Date()
        //}];

        viewModel.trips = [];

        viewModel.errorMessage = "";
        viewModel.isBusy = true;

        $http.get("/api/trips").then(function(response) {
            //Success
            angular.copy(response.data, viewModel.trips);
        }, function(error) {
            //Failure
            viewModel.errorMessage = "Failed to load data: " + error;
        }).finally(function() {
            viewModel.isBusy = false;
        });

        viewModel.newTrip = {};

        viewModel.addTrip = function() {
            viewModel.isBusy = true;
            viewModel.errorMessage = "";

            $http.post("/api/trips", viewModel.newTrip).then(function (response) {
                //Success
                viewModel.trips.push(response.data);
                viewModel.newTrip = {};
            }, function (error) {
                //Failure
                viewModel.errorMessage = "Failed to save new trip: " + error;
            }).finally(function () {
                viewModel.isBusy = false;
            });
        };
    }

})();