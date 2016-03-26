
app.controller('loginController', function ($scope, $http, $location, accountService) {
    if (accountService.authentication.isAuth) {
        $location.path('/submit');
    }

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