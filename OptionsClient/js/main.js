var app = angular.module('choicesApp', ['ngRoute', 'LocalStorageModule']);

// Setup the routing
app.config(function ($routeProvider) {

    $routeProvider
      .when('/home', {
          templateUrl: 'pages/home.html',
          controller: 'homeController',
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
app.run(function ($rootScope, $route, $location, accountService) {
    $rootScope.$on("$routeChangeSuccess", function (currentRoute, previousRoute) {
        //Change page title, based on Route information
        $rootScope.title = $route.current.title;

        // Set some data based on user authentication
        accountService.fillAuthData();
        $rootScope.authentication = accountService.authentication;

        // Allow users to logout
        $rootScope.logout = function () {
            accountService.logout();
            $location.path('/home');
        }
    });
});
