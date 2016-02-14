(function (app) {
    'use strict';

    app.controller('albumsCtrl', albumsCtrl);

    albumsCtrl.$inject = ['$scope', '$http'];

    function albumsCtrl($scope, $http) {
        // api/albums
        $http.get('/api/albums/').
          success(function (data, status, headers, config) {
              $scope.apiAlbums = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });


        //api/albums?props=artistname,title,track
        $http.get('/api/albums?props=artistname,title,track').
          success(function (data, status, headers, config) {
              $scope.apiAlbumsPropsAll = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });

        //api/albums?props=artistname,title,track&page=2&pagesize=10
        $http.get('/api/albums?props=artistname,title,track&page=2&pagesize=10').
          success(function (data, status, headers, config) {
              $scope.apiAlbumsPropsAllPaged = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });

        //api/albums?props=artistname,title,track
        $http.get('/api/albums?props=artistname,title,track(bytes;name;unitprice)').
          success(function (data, status, headers, config) {
              $scope.apiAlbumsPropsAllParseTrack = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });

        // api/albums/1
        $http.get('/api/albums/1').
          success(function (data, status, headers, config) {
              $scope.apiAlbumsOne = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });

        // api/albums/1?props=bytes,milliseconds,name
        $http.get('/api/albums/1?props=artistname,title,track(composer;name)').
          success(function (data, status, headers, config) {
              $scope.apiAlbumsOneProps = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });
    }
})(angular.module('shapeApp'));