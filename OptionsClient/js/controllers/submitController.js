
app.controller('submitController', function ($scope, $http, $location, accountService, studentService) {
    if (!accountService.authentication.isAuth) {
        $location.path('/login');
    }

    $scope.savedSuccessfully = false;
    $scope.message = "";

    studentService.getData(accountService.authentication.token).then(function (response) {
        $scope.diplomaOptions = response.options;
        console.log(response);
    });

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
        $scope.savedSuccessfully = false;
        $scope.message = "Uh oh! Your submission was not successful."
        console.log(response);
    };

    $scope.submitChoice = function () {
        studentService.submitChoice($scope.user)
            .then(onRegisterComplete, onRegisterError);
    }
});