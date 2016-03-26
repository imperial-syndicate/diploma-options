// AccountService.js

(function () {

    var accountService = function ($http, $window, localStorageService) {

        var baseUrl = 'http://localhost:12853/';
        var _authentication = {
            token: "",
            isAuth: false,
            username: ""
        };

        var _login = function (username, password) {
            var data = "grant_type=password&username=" + username + "&password=" + password;

            return $http.post(baseUrl + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .then(function (response) {

                    localStorageService.set('authorizationData', { token: "Bearer " + response.data.access_token, username: username });

                    _authentication.isAuth = true;
                    _authentication.username = username;
                    _authentication.token = response.access_token;
                    return response.data;
                });
        };

        var _register = function (userData) {
            _logout();

            var data = "username=" + userData.username + "&email=" + userData.email + "&password=" + userData.password + "&confirmPassword=" + userData.confirmPassword;

            return $http.post(baseUrl + 'api/account/register', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .then(function (response) {
                return response.data;
            });
        };

        var _logout = function () {
            localStorageService.remove('authorizationData');
            _authentication.isAuth = false;
            _authentication.userName = "";
        };

        var _fillAuthData = function () {
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                _authentication.isAuth = true;
                _authentication.username = authData.username;
                _authentication.token = authData.token;
            }
        }

        return {
            login: _login,
            register: _register,
            logout: _logout,
            fillAuthData: _fillAuthData,
            authentication: _authentication
        };
    };

    var module = angular.module("choicesApp");
    module.factory("accountService", ['$http', '$q', 'localStorageService', accountService]);

}());