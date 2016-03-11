var accountUrl = "http://localhost:30034/UserAccount/";

gameApp.factory('accountFactory', function ($http) {
    var factory = {};
    var makeUrl = function(method) {
        return accountUrl + method + "/";
    }

    factory.login = function(user, pass) {
        return $http.post(makeUrl("LogIn"), { username: user, password: pass });
    }

    return factory;
});

gameApp.controller('AccountController', function ($scope, $location, accountFactory) {

    $scope.login = function() {
        var promise = accountFactory.login($scope.username, $scope.password);

        var observable = Rx.Observable.fromPromise(promise);

        observable.subscribe(
            
        function (data) {
            if (data.data.result.errorcode == "FAIL") {
                $scope.errorMessage = "Error occured: " + data.data.result.message;
            } else {
                //   alert(data.data.response.logintoken);
                $scope.successMessage = "Login successful";
                $scope.errorMessage = String.empty;

                $location.path("/member/" +
                    data.data.response.logintoken +
                    "/" +
                    data.data.response.firstname + " " + data.data.response.lastname);
            }
        },
       function (data) {
           $scope.errorMessage = "Unknown error occurred. Try again.";
        });
    }
});

