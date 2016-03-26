
app.controller('submitController', function ($scope, $http, $location, accountService, studentService) {
    if (!accountService.authentication.isAuth) {
        $location.path('/login');
    }

    $scope.savedSuccessfully = false;
    $scope.message = "";

    var data = studentService.getData();

    $scope.user = {
        username: accountService.authentication.username,
        firstName: "",
        lastName: "",
        firstChoiceOptionId: "0",
        secondChoiceOptionId: "0",
        thirdChoiceOptionId: "0",
        fourthChoiceOptionId: "0",
    };

    var onRegisterComplete = function (data) {
        $scope.savedSuccessfully = true;
        $scope.message = "Your choices have been submitted successfully";

        console.log(data);
    };

    var onRegisterError = function (response) {
        console.log(response);
    };

    $scope.submitChoice = function () {
        studentService.submitChoice($scope.user)
            .then(onRegisterComplete, onRegisterError);
    }
});