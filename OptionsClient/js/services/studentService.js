// StudentService.js

(function () {

    var studentService = function ($http, $window, localStorageService, accountService) {

        var baseUrl = 'http://localhost:12853/';

        var _getData = function () {
            var token = accountService.authentication.token;
            console.log(token);

            return $http.get(baseUrl + 'api/options', { headers: { 'Content-Type': 'application/json; charset=utf-8' } })
                .then(function (response) {
                    return response.data;
                })
        }

        var _submitChoice = function (userData) {
            var token = accountService.authentication.token;
            console.log(token);

            var data = "YearTermID=" + userData.yearTermId
                + "&StudentID=" + userData.StudentID
                + "&StudentFirstName=" + userData.firstName
                + "&StudentLastName=" + userData.lastName
                + "&FirstChoiceOptionId=" + userData.firstChoiceOptionId
                + "&SecondChoiceOptionId=" + userData.secondChoiceOptionId
                + "&ThirdChoiceOptionId=" + userData.thirdChoiceOptionId
                + "&FourthChoiceOptionId=" + userData.fourthChoiceOptionId;

            return $http.post(baseUrl + 'api/choices', data, { headers: { 'Authorization': token, 'Content-Type': 'application/x-www-form-urlencoded' } })
                .then(function (response) {
                    return response.data;
                })
        };

        return {
            getData: _getData,
            submitChoice: _submitChoice
        };
    };

    var module = angular.module("choicesApp");
    module.factory("studentService", ['$http', '$q', 'localStorageService', 'accountService', studentService]);

}());