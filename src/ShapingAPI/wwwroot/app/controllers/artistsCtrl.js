(function (app) {
    'use strict';

    app.controller('artistsCtrl', artistsCtrl);

    artistsCtrl.$inject = ['$scope', '$http'];

    function artistsCtrl($scope, $http) {
        // api/artists
        $http.get('/api/artists/').
          success(function (data, status, headers, config) {
              $scope.apiArtists = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });


        //api/artists?props=artistname,title,track
        $http.get('/api/artists?props=name,album(albumid;title)').
          success(function (data, status, headers, config) {
              $scope.apiArtistsPropsOne = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });

        //api/artists?props=artistname,title,track
        $http.get('/api/artists?props=name,album(albumid;title;track)').
          success(function (data, status, headers, config) {
              $scope.apiAlbumsPropsTwo = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });

        // api/artists/1
        $http.get('/api/artists/1?props=name,album(albumid;title;track)').
          success(function (data, status, headers, config) {
              $scope.apiArtistsPropsThree = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });

        // api/albums/1?props=bytes,milliseconds,name
        $http.get('/api/artists/1?props=name,album(albumid;title;track(bytes;composer;unitprice))').
          success(function (data, status, headers, config) {
              $scope.apiArtistsPropsFour = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });
    }
})(angular.module('shapeApp'));