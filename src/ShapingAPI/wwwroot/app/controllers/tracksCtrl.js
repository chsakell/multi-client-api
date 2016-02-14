(function (app) {
    'use strict';

    app.controller('tracksCtrl', tracksCtrl);

    tracksCtrl.$inject = ['$scope', '$http'];

    function tracksCtrl($scope, $http) {
        // api/tracks
        $http.get('api/tracks/').
          success(function (data, status, headers, config) {
              $scope.apiTracks = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });

        // api/tracks?page=2&pagesize=100
        $http.get('api/tracks?page=2&pagesize=100').
          success(function (data, status, headers, config) {
              $scope.apiTracksPaged = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });

        // api/tracks?props=bytes,composer,milliseconds
        $http.get('api/tracks?props=bytes,composer,milliseconds').
          success(function (data, status, headers, config) {
              $scope.apiTracksAll = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });

        // api/tracks/1
        $http.get('api/tracks/1').
          success(function (data, status, headers, config) {
              $scope.apiTracksOne = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });

        // api/tracks/1?props=bytes,milliseconds,name
        $http.get('api/tracks/1?props=bytes,milliseconds,name').
          success(function (data, status, headers, config) {
              $scope.apiTracksOneProps = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });
    }
})(angular.module('shapeApp'));