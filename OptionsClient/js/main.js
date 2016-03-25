var app = angular.module('choicesApp', ['ngRoute']);

// setup the routing
app.config(function ($routeProvider) {

    $routeProvider
      // display all of the students
      .when('/', {
          templateUrl: 'pages/home.html',
          controller: 'mainController'
      })

});

// controller for the home page
app.controller('mainController', function ($scope, $http) {
    $scope.message = 'Everyone come and look!';
});