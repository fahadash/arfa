var tableUrl = serviceUrlBase + "/Table/";
gameApp.factory('memberFactory', function ($http) {
    var factory = {};
    var makeUrl = function (method) {
        return tableUrl + method + "/";
    }

    factory.getMyTables = function (token) {
        return $http.post(makeUrl("ListUserTables"), { logintoken: token });
    }

    factory.getTablesIAmOn = function (token) {
        return $http.post(makeUrl("ListTablesIAmOn"), { logintoken: token });
    }

    factory.getJoinableTables = function (token) {
        return $http.post(makeUrl("ListJoinableTables"), { logintoken: token });
    }

    factory.joinTable = function (token, tableId) {
        return $http.post(makeUrl("JoinTable"), { logintoken: token, tableid:tableId });
    }

    factory.createTable = function (token, tableName) {
        return $http.post(makeUrl("CreateTable"), { logintoken: token, tablename: tableName });
    }

    return factory;
});

gameApp.controller('MemberController', function ($scope, $location, $routeParams, $rootScope, memberFactory) {

    var postError = function(e) {
        if (typeof $scope.errorMessage == 'undefined') {
            $scope.errorMessage = "";
        }

        $scope.errorMessage += e;
    }

    $scope.loginToken = $routeParams.logintoken;
    $scope.memberName = $routeParams.membername;

    $scope.myGames = [];
    $scope.otherGames = [];

    var updateMemberArea = function() {
        var myGamesObservable = Rx.Observable.fromPromise(memberFactory.getMyTables($scope.loginToken));

        var iAmOnObservable = Rx.Observable.fromPromise(memberFactory.getTablesIAmOn($scope.loginToken));

        var tablesToJoinObservable = Rx.Observable.fromPromise(memberFactory.getJoinableTables($scope.loginToken));


        myGamesObservable
            .subscribe(function(data) {
                    if (data.data.result.errorcode == "FAIL") {
                        $scope.errorMessage = "Error occured while loading your tables: " + data.data.result.message;
                    } else if (data.data.result.errorcode == "INVALIDLOGINTOKEN") {
                        $location.path("/login");
                    } else {

                        $scope.myGames = data.data.response.tables;
                        console.log(data.data.response.tables);
                    }
                },
                function(data) {
                    console.log(data);
                    postError("Error loading user games");
                });


        iAmOnObservable
            .subscribe(function(data) {
                    if (data.data.result.errorcode == "FAIL") {
                        $scope.errorMessage = "Error occured while loading your tables: " + data.data.result.message;
                    } else if (data.data.result.errorcode == "INVALIDLOGINTOKEN") {
                        $location.path("/login");
                    } else {
                        $scope.otherGames = data.data.response.tables;
                    }
                },
                function(data) {
                    console.log(data);
                    postError("Error loading user games");
                });


        tablesToJoinObservable
            .subscribe(function(data) {
                    if (data.data.result.errorcode == "FAIL") {
                        $scope.errorMessage = "Error occured while loading your tables: " + data.data.result.message;
                    } else if (data.data.result.errorcode == "INVALIDLOGINTOKEN") {
                        $location.path("/login");
                    } else {
                        $scope.gamesToJoin = data.data.response.tables;
                    }
                },
                function(data) {
                    console.log(data);
                    postError("Error loading user games");
                });
    };

    updateMemberArea();
    $scope.createButton = {};
    $scope.createButton.enabled = true;
    $scope.createButton.caption = "Create Game";

    $scope.createGame = function (token, tableName) {
        $scope.createButton.enabled = false;
        $scope.createButton.caption = "Creating...";
        var ob = Rx.Observable.fromPromise(memberFactory.createTable(token, tableName));

        ob
            .subscribe(function (d) {
                if (d.data.result.errorcode == "FAIL") {
                    $scope.errorMessage = "Error occured while loading your tables: " + data.data.result.message;
                } else if (d.data.result.errorcode == "INVALIDLOGINTOKEN") {
                    $location.path("/login");
                } else {

                    updateMemberArea();

                    $scope.createButton.enabled = true;
                    $scope.createButton.caption = "Create";
                }
            },
                function(d) {

                    postError("Error creating game");
                });
    };

    $scope.joinTable = function(token, tableId) {
        var ob = Rx.Observable.fromPromise(memberFactory.joinTable(token, tableId));

        ob
            .subscribe(function(d) {
                console.log(d);
                if (d.data.result.errorcode == "FAIL") {
                    $scope.errorMessage = "Error occured while joining table: " + d.data.result.message;
                } else if (d.data.result.errorcode == "INVALIDLOGINTOKEN") {
                    $location.path("/login");
                }
                else if (d.data.result.errorcode == "SUCCESS") {

                    $location.path("/table/" + $scope.loginToken + "/" + tableId.toString());
                } else {
                    postError("Unknown status returned :" + d.data.result.errorcode +
                        " " + d.data.result.errormessage);
                }
                },
                function(d) {
                    postError("Error occurred while joining table");
                });

    };
});