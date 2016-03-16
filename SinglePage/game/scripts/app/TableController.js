gameApp.factory('tableFactory', function ($http) {
    var factory = {};
    var makeUrl = function (method) {
        return tableUrl + method + "/";
    }

    factory.getGameState = function(token, tableId) {
        return $http.post(makeUrl("GetTableUpdate"), { logintoken: token, tableid: tableId });
    }

    factory.startGame = function (token, tableId) {
        return $http.post(makeUrl("BeginTable"), { logintoken: token, tableid: tableId });
    }

    factory.chooseTrump = function (token, tableId, trumpSuit) {
        return $http.post(makeUrl("ChooseTrump"), { logintoken: token, tableid: tableId, trump:trumpSuit});
    }

    factory.submitCard = function (token, tableId, card) {
        return $http.post(makeUrl("SubmitUserCard"), { logintoken: token, tableid: tableId, cardalias: card });
    }
    return factory;
});

gameApp.controller('TableController', function ($scope, $location, $routeParams, $rootScope, tableFactory) {
    $scope.showOverlay = false;

    var postError = function (e) {
        if (typeof $scope.errorMessage == 'undefined') {
            $scope.errorMessage = "";
        }

        $scope.errorMessage += ("\r\n" + e);
    }

    var showError = function (error) {
        postError(error);
        $("#error-dialog").dialog({
            modal: true,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");

                    $scope.errorMessage = "";
                }
            }
        });
    }
    $scope.$on("$destroy", function () {
        if ($scope.pollSubscription) {
            $scope.pollSubscription.dispose();
        }
    });

    var handleResult = function (d, elseFunction) {
        var result = d.data.result;
        if (result.errorcode == "FAIL") {
            showError("Error occured while loading game state: " + result.message);
        } else if (result.errorcode == "INVALIDLOGINTOKEN") {
            $location.path("/login");
        } else if (result.errorcode == "SUCCESS") {
            elseFunction(d);
        } else {
            showError("Message: " + result.message);
        }

        $scope.showOverlay = false;
    }

    // Update Success Handler
    var handleUpdateSuccess = function(data) {
        handleResult(data, function(d) {

            var game = d.data.response;
            $scope.tableName = game.tablename;
            $scope.gameStarted = game.gamestarted;
            $scope.handsAccumulated = game.handsaccumulated;
            $scope.gameId = game.tableid;

            //Arrange Users
            var slice = [];

            for (var i = 0; i < game.users.length; i++) {
                var obj = game.users[i];
                if (obj.isyou == true) {
                    slice = game.users.slice(i);
                    break;
                }
            }

            for (var j = 0; j < i; j++) {
                slice.push(game.users[j]);
            }

            console.log(slice);

            $scope.me = slice[0];
            $scope.leftPlayer = slice[1];
            $scope.topPlayer = slice[2];
            $scope.rightPlayer = slice[3];

            if ($scope.me.userinfo.username == game.Owner.UserName) {
                $scope.iAmOwner = true;
            } else {
                $scope.iAmOwner = false;
            }

            $scope.hasToChooseTrump = false;
            if ($scope.me.turn == true && game.trumpchosen == false) {
                $scope.hasToChooseTrump = true;
            }

            $scope.trump = game.trump;
            $scope.trumpChosen = game.trumpchosen;
            $scope.currentSuit = game.currentsuit;

            console.log(game);
            if (game.tableFinished == true)
            {
                //$rootScope.finalPlayers = slice;
                //$scope.pollSubscription.dispose();
                //$location.path("/winner/" + $routeParams.logintoken + "/" + $routeParams.tableid);
            }

            if (typeof (adjustCards) == "function") {
                adjustCards();
            }
        });
    };

    var handleUpdateError = function(e) {
        showError("Error occurred while trying to retrieve game state. " + e.data.result.errorcode);
        $scope.showOverlay = false;
    };

    var updateFn = function () {
        $scope.showOverlay = true;
        var updateObservable = Rx.Observable.fromPromise(tableFactory.getGameState($routeParams.logintoken, $routeParams.tableid));
        
        updateObservable
            .subscribe(function(d) {
                console.log(d);
                handleUpdateSuccess(d);
                
            }, handleUpdateError);
    };

    $scope.loginToken = $routeParams.logintoken;
    $scope.tableId = $routeParams.tableid;
    $scope.startGame = function (token, tableId) {
        $scope.showOverlay = true;
        var ob = Rx.Observable.fromPromise(tableFactory.startGame(token, tableId));

        ob
            .subscribe(function(d) {
                    handleResult(d, updateFn);
                },
                function(d) {
                    showError("Error occurred while calling server to begin the game.");
                });
    };

    $scope.chooseTrump = function (token, tableId, trump) {
        $scope.showOverlay = true;
        var ob = Rx.Observable.fromPromise(tableFactory.chooseTrump(token, tableId, trump));

        ob
            .subscribe(function (d) {
                console.log(d);
                handleResult(d, updateFn);
            },
                function (d) {
                    showError("Error occurred while calling server to choose Trump.");
                });
    }

    $scope.submitCard = function (token, tableId, card) {
        $scope.showOverlay = true;
        var ob = Rx.Observable.fromPromise(tableFactory.submitCard(token, tableId, card));

        ob
            .subscribe(function (d) {
                console.log(d);
                handleResult(d, updateFn);
            },
                function (d) {
                    showError("Error occurred while calling server to submit a card.");
                });
    }

    var service = Rx.Observable.defer(function () {
        return Rx.Observable.fromPromise(tableFactory.getGameState($routeParams.logintoken, $routeParams.tableid));
    });
    var pollInterval = Rx.Observable.empty().delay(5000);
    $scope.pollSubscription =
   service
    .concat(pollInterval)
    .repeat()
    .subscribe(handleUpdateSuccess, handleUpdateError);


});