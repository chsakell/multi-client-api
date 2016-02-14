(function (app) {
    'use strict';

    app.controller('customersCtrl', customersCtrl);

    customersCtrl.$inject = ['$scope', '$http'];

    function customersCtrl($scope, $http) {
        // api/customers
        $http.get('/api/customers/').
          success(function (data, status, headers, config) {
              $scope.apiCustomers = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });


        //api/customers?props=city,company,firstname,lastname,Invoice
        $http.get('/api/customers?props=city,company,firstname,lastname,Invoice').
          success(function (data, status, headers, config) {
              $scope.apiCustomersPropsOne = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });

        //api/customers?props=address,contact
        $http.get('/api/customers?props=firstname,lastname,address,contact').
          success(function (data, status, headers, config) {
              $scope.apiCustomersPropsAddressContact = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });

        //api/customers?props=firstname,lastname,Invoice[billingcity;total]
        $http.get('/api/customers?props=firstname,lastname,Invoice(billingcity;total)&page=4&pagesize=5').
          success(function (data, status, headers, config) {
              $scope.apiCustomersPropsOnePaged = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });

        //api/customers?props=customerid,city,company,firstname,lastname,Invoice[billingaddress;billingcity;total;invoiceline]
        $http.get('/api/customers?props=customerid,city,company,firstname,lastname,Invoice(billingaddress;billingcity;total;invoiceline)').
          success(function (data, status, headers, config) {
              $scope.apiCustomersPropsTwo = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });

        // /api/customers/1?props=customerid,city,company,firstname,lastname,Invoice[billingaddress;billingcity;total;invoiceline[invoicelineid;quantity;trackid]]
        $http.get('/api/customers/1?props=customerid,city,company,firstname,lastname,Invoice(billingaddress;billingcity;total;invoiceline(invoicelineid;quantity;trackid))').
          success(function (data, status, headers, config) {
              $scope.apiCustomersPropsThree = data;
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });
    }
})(angular.module('shapeApp'));