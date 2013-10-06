/// <reference path="../Scripts/require.js" />
/// <reference path="../Scripts/knockout-2.3.0.js" />
/// <reference path="../Scripts/jquery-2.0.3.js" />
/// <reference path="../Scripts/bootstrap.js" />

define([
    "dataservices/provider",
    "plugins/router"
], function (provider, router) {
    var ctor = function () { };

    ctor.prototype.resellers = [];

    ctor.prototype.dataService = provider.define("Reseller");

    ctor.prototype.activate = function () {
        var _Self = this;

        _Self.dataService.get({})
        .then(function (data) {
            _Self.resellers = data;
        })
        .fail(function (xhr, textStatus, errorThrown) {
            def.reject(xhr.responseText, textStatus, errorThrown);
        });
    };

    ctor.prototype.canActivate = function () {
        return true;
    };

    ctor.prototype.Create = function () {
        router.navigate("credentials/resellers/create", true);
    };

    ctor.prototype.Edit = function (reseller) {
        router.navigate("credentials/resellers/" + reseller.Id + "/edit", true);
    };

    return ctor;
});