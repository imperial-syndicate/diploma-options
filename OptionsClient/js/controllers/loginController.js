
app.controller('loginController', function ($scope, $http, $location, accountService) {
    if (accountService.authentication.isAuth) {
        $location.path('/submit');
    }

    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.user = {
        username: "",
        password: ""
    }

    var onAddComplete = function (data) {
        $location.path('/home');
        console.log(data);
    };

    var onAddError = function (response) {
        $scope.savedSuccessfully = false;
        $scope.message = "Uh oh! We were unable to log you in.";

        console.log(response.statusText + ', error code: ' + response.status);
    };

    $scope.loginUser = function () {
        var username = $scope.user.username;
        var password = $scope.user.password;
        accountService.login(username, password)
        .then(onAddComplete, onAddError);
    }
});