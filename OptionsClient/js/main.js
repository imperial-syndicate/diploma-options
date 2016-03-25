var app = angular.module('choicesApp', ['ngRoute']);

// Setup the routing
app.config(function ($routeProvider) {

    $routeProvider
      .when('/', {
          templateUrl: 'pages/home.html',
          controller: 'mainController',
          title: 'Home'
      })
      .when('/login', {
          templateUrl: 'pages/login.html',
          controller: 'loginController',
          title: 'Login'
      })
      .when('/register', {
          templateUrl: 'pages/register.html',
          controller: 'registerController',
          title: 'Register'
      })
      .when('/submit', {
          templateUrl: 'pages/submit.html',
          controller: 'submitController',
          title: 'Submit Choice'
      });

});

// Controls the rootscope
app.run(function ($rootScope, $route) {
    $rootScope.$on("$routeChangeSuccess", function (currentRoute, previousRoute) {
        //Change page title, based on Route information
        $rootScope.title = $route.current.title;
    });
});

// Maintains the header (changes based on user authenticated or not)
app.controller('headerController', function ($scope) {
    $scope.hello = "world";
});

// Controller for the home page
app.controller('mainController', function ($scope, $http) {
    $scope.message = 'Everyone come and look!';
});

// Controller for the login page
app.controller('loginController', function($scope, $http) {
    $scope.message = 'Login Page';
});

// Controller for the register page
app.controller('registerController', function ($scope, $http) {
    $scope.message = 'Register Page';
});

// Controller for the submission page
app.controller('submitController', function ($scope, $http) {
    $scope.message = 'Submit Choice';
});