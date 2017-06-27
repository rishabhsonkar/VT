app.factory('userService', function ($http) {
    var userServiceFactory = {};
    //table 1
    userServiceFactory.getUsers = function () {
        return $http.get("http://localhost/InventoryManagement.Api/odata/Users");
    }
    userServiceFactory.createUsers = function (model) {
        return $http.post("http://localhost/InventoryManagement.Api/odata/Users", model);
    }
    //table 2
    userServiceFactory.getItems = function () {
        return $http.get("http://localhost/InventoryManagement.Api/odata/Items");
    }
    userServiceFactory.createItems = function (model) {
        return $http.post("http://localhost/InventoryManagement.Api/odata/Items", model);
    }
    //table 3
    userServiceFactory.getTransactions = function () {
        return $http.get("http://localhost/InventoryManagement.Api/odata/Transactions");
    }
    userServiceFactory.createTransactions = function (model) {
        return $http.post("http://localhost/InventoryManagement.Api/odata/Transactions", model);
    }
    //table 4
    userServiceFactory.getInventories = function () {
        return $http.get("http://localhost/InventoryManagement.Api/odata/Inventories");
    }
    userServiceFactory.createInventories = function (model) {
        return $http.post("http://localhost/InventoryManagement.Api/odata/Inventories", model);
    }
    //table 5
    userServiceFactory.getLocations = function () {
        return $http.get("http://localhost/InventoryManagement.Api/odata/Locations");
    }
    userServiceFactory.createLocations = function (model) {
        return $http.post("http://localhost/InventoryManagement.Api/odata/Locations", model);
    }
    //table 6
    userServiceFactory.getOrganisations = function () {
        return $http.get("http://localhost/InventoryManagement.Api/odata/Organisations");
    }
    userServiceFactory.createOrganisations = function (model) {
        return $http.post("http://localhost/InventoryManagement.Api/odata/Organisations", model);
    }
    return userServiceFactory;
});