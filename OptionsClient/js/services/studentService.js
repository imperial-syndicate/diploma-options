// StudentService.js

(function () {

    var studentService = function ($http, $window, localStorageService) {

        var baseUrl = 'http://localhost:12853/';

        var _getData = function (token) {
            return $http.get(baseUrl + 'api/options', { headers: { 'Authorization': token, 'Content-Type': 'application/json; charset=utf-8' } })
                .then(function (response) {
                    return response.data;
                })
        }

        var _submitChoice = function (userData) {
            var data = "YearTermID=" + userData.yearTermId
                + "&StudentID=" + userData.studentID
                + "&StudentFirstName="
                + userData.firstName
                + "&StudentLastName="
                + userData.lastName
                + "&FirstChoiceOptionId="
                + userData.firstChoiceOptionId
                + "&SecondChoiceOptionId="
                + userData.secondChoiceOptionId
                + "&ThirdChoiceOptionId="
                + userData.thirdChoiceOptionId
                + "&FourthChoiceOptionId="
                + userData.fourthChoiceOptionId;

            return $http.post(baseUrl + 'api/choices', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
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
    module.factory("studentService", ['$http', '$q', 'localStorageService', studentService]);

}());