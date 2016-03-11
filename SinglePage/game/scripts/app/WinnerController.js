gameApp.controller('WinnerController', function ($scope, $location, $routeParams, $rootScope, memberFactory) {
    $scope.loginToken = $routeParams.logintoken;
    $scope.players = $rootScope.finalPlayers;

});