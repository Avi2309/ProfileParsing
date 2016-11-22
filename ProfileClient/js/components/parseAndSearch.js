var app = angular.module('profiles', []);

app.controller('profilesCtrl', function ($scope, $http) {

    $scope.profiles = [];

    $scope.parseProfileUri = function () {
        var data = {profileUri: $scope.linkedinProfileUrl};
        httpCall("SetProfile", data,
            function(){window.alert("Parsing profile succeeded")},
            function(){window.alert("Parsing failed")});
    };

    $scope.searchProfileByName = function () {
        var data = {profileUri: $scope.name};
        httpCall("SearchByName", data,
            function(resObj){$scope.profiles = resObj},
            function(error){window.alert("Parsing failed")});
    };
    
    $scope.searchProfileBySkills = function () {
        var skillsList = $scope.skills.replace(/\s/g, "").split(",");
        httpCall("SearchBySkills", skillsList,
            function(resObj){$scope.profiles = resObj},
            function(error){window.alert("Parsing failed")});
    };

    var httpCall = function(func, dataObj, success, error) {
        $http({
            url: 'http://localhost:84/api/' + func,
            method: "POST",
            data: dataObj,
            headers: {'Content-Type': 'application/json'},

        })
            .success(success(JSON.parse(res)))
            .error(error(res));
    };

});

