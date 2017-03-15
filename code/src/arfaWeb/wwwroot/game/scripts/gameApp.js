// JavaScript source code

var gameApp = angular.module('gameApp', []);



function GetTestTableState() {
    return {
        "tableid": 1,
        "tablename": "Test Table 1",
        "gamestarted": false,
        "Owner": { "UserId": 1, "UserName": "fahadash", "Password": null, "Firstname": "fahadash", "Lastname": "fahadash", "Age": 18 },
        "users": [{ "userinfo": { "userid": 1, "username": "fahadash", "firstname": "fahadash", "lastname": "fahadash" }, "gamescore": 0, "currentcard": null, "dominant": false, "lastheartbeat": "2015-01-21T19:07:43.85", "turn": null, "isyou": true, "yourcards": [] }, {}, {}, {}],
        "handsaccumulated": 0
    };
}

gameApp.controller('TableController', function($scope) {
    var state = GetTestTableState();

    $scope.TableState =
    {
        tableName: state.tablename,
        gameStarted: state.gamestarted == false ? "No" : "Yes"
    }
});