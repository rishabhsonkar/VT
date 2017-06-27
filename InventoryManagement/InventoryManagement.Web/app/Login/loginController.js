app.controller('loginController', function ($rootScope,$scope,userService) {
    $rootScope.registerData = {};
    $rootScope.loginData = {};
    $scope.userType = ["Owner", "Manager", "Employee"];
    $scope.users = [];
    $scope.LoginFlag = true;
    $scope.showLogin = function () {
        $scope.LoginFlag = true;
    }
    $scope.showSignup = function () {
        $scope.LoginFlag = false;
    }
    $scope.Login = function(){

    }
    $scope.Register = function () {

    }
});