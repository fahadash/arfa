var gameApp = angular.module('gameApp', ['ngRoute']);
var serviceUrlBase = "http://localhost:46197/api";
gameApp.config(function($routeProvider) {
    $routeProvider
        .when('/',
        {
            controller: 'AccountController',
            templateUrl: 'Views/Login.html'
        })
        .when('/member/:logintoken/:membername',
        {
            controller: 'MemberController',
            templateUrl: 'Views/Member.html'
        })
        .when('/winner/:logintoken',
        {
            controller: 'WinnerController',
            templateUrl: 'Views/Winner.html'
        })
        .when('/table/:logintoken/:tableid',
        {
            controller: 'TableController',
            templateUrl: 'Views/Table.html'
        })
        .otherwise({ redirectTo: '/' });
});