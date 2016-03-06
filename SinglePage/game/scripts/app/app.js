var gameApp = angular.module('gameApp', ['ngRoute']);

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
        .when('/table/:logintoken/:tableid',
        {
            controller: 'TableController',
            templateUrl: 'Views/Table.html'
        })
        .otherwise({ redirectTo: '/' });
});