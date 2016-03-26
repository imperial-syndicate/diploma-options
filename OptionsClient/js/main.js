var app = angular.module('choicesApp', ['ngRoute', 'LocalStorageModule']);

// Setup the routing
app.config(function ($routeProvider) {

    $routeProvider
      .when('/home', {
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

    $routeProvider.otherwise({ redirectTo: "/home" });

});

// Controls the rootscope
app.run(function ($rootScope, $route, accountService) {
    $rootScope.$on("$routeChangeSuccess", function (currentRoute, previousRoute) {
        //Change page title, based on Route information
        $rootScope.title = $route.current.title;

        // Set some data based on user authentication
        accountService.fillAuthData();
        $rootScope.authentication = accountService.authentication;
    });
});

// Maintains the header (changes based on user authenticated or not)
app.controller('headerController', function ($scope, $window, accountService) {
    
});

// Controller for the home page
app.controller('mainController', function ($scope, $http) {
    $scope.message = 'Everyone come and look!';
});

// Controller for the login page
app.controller('loginController', function($scope, $http, $location, accountService) {
    $scope.message = 'Login Page';

    var onAddComplete = function (data) {
        $location.path('/home');
        console.log(data);
    };

    var onAddError = function (response) {
        alert(response.statusText + ', error code: ' + response.status);
    };

    $scope.loginUser = function () {
        var username = $scope.user.username;
        var password = $scope.user.password;
        accountService.login(username, password)
        .then(onAddComplete, onAddError);
    }
});

// Controller for the register page
app.controller('registerController', function ($scope, $http) {
    $scope.message = 'Register Page';
});

// Controller for the submission page
app.controller('submitController', function ($scope, $http) {
    $scope.message = 'Submit Choice';
});