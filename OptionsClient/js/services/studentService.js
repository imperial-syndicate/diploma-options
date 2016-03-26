// StudentService.js

(function () {

    var studentService = function ($http, $window, localStorageService) {

        var baseUrl = 'http://localhost:12853/';

        var _getData = function (token) {
            console.log(token);
            return $http.get(baseUrl + 'api/options', { headers: { 'Authorization': token, 'Content-Type': 'application/json; charset=utf-8' } })
                .then(function (response) {
                    return response.data;
                })
        }

        var _submitChoice = function (userData) {
            return $http.post(baseUrl + 'api/choice', userData)
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