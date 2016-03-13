gameApp.controller('WinnerController', function ($scope, $location, $routeParams, $rootScope, memberFactory, tableFactory) {
    $scope.loginToken = $routeParams.logintoken;
   // $scope.players = $rootScope.finalPlayers;
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
    };

    var postError = function (e) {
        if (typeof $scope.errorMessage == 'undefined') {
            $scope.errorMessage = "";
        }

        $scope.errorMessage += ("\r\n" + e);
    };

    var handleResult = function (d, elseFunction) {
        var result = d.data.result;
        if (result.errorcode == "FAIL") {
            showError("Error occured while loading game state: " + result.message);
        } else if (result.errorcode == "INVALIDLOGINTOKEN") {
            $scope.pollSubscription.dispose();
            $location.path("/login");
        } else if (result.errorcode == "SUCCESS") {
            elseFunction(d);
        } else {
            showError("Message: " + result.message);
        }

        $scope.showOverlay = false;
    }

    // Update Success Handler
    var handleUpdateSuccess = function (data) {
        console.log(data);
        handleResult(data, function (d) {

            var game = d.data.response;
            $scope.players = game.users;
        });
    };
        var ob = Rx.Observable.fromPromise(tableFactory.getGameState($routeParams.logintoken, $routeParams.tableid));

        ob
            .subscribe(handleUpdateSuccess,
                function (d) {
                    showError("Error occurred while calling server to load scores.");
                });

});