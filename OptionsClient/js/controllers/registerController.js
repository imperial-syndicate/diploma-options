
app.controller('registerController', function ($scope, $http, $location, $timeout, accountService) {
    if (accountService.authentication.isAuth) {
        $location.path('/submit');
    }
    
    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.user = {
        username: "",
        password: "",
        email: "",
        confirmPassword: ""
    };

    var onRegisterComplete = function (data) {
        $scope.savedSuccessfully = true;
        $scope.message = "You have been registered successfully, you will be redicted to login page in 2 seconds.";

        startTimer();
        console.log(data);
    };

    var onRegisterError = function (response) {
        console.log(response);
    };

    $scope.register = function () {
        accountService.register($scope.user)
            .then(onRegisterComplete, onRegisterError);
    }

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }

});