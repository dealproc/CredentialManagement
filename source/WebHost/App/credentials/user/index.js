/// <reference path="../Scripts/require.js" />
/// <reference path="../Scripts/knockout-2.3.0.js" />
/// <reference path="../Scripts/jquery-2.0.3.js" />
/// <reference path="../Scripts/bootstrap.js" />

define([
    "dataservices/provider",
    "plugins/observable",
    "./create",
    "durandal/app",
    "plugins/router"
],
    function (provider, observable, createForm, app, router) {
        var ctor = function () { };

        ctor.prototype.users = [];

        ctor.prototype.dataService = provider.define("User");


        ctor.prototype.activate = function () {
            var _Self = this;

            _Self.newUserSubscription = app.on("user:new", function (user) {
                _Self.scrubUser(user);
                _Self.users.push(user);
            });

            _Self.dataService.get({})
            .then(function (data) {

                for (var i = 0; i < data.length; i++) {
                    _Self.scrubUser(data[i]);
                }

                _Self.users = data;
            })
            .fail(function (xhr, textStatus, errorThrown) {
                def.reject(xhr.responseText, textStatus, errorThrown);
            });
        };

        ctor.prototype.scrubUser = function (user) {
            if (user["Groups"] === undefined) {
                user["Groups"] = [];
            }
            if (user["LastActivityUTC"] == undefined) {
                user["LastActivityUTC"] = null;
            }


            observable.defineProperty(user, "rolesLength", function () {
                var rolesCount = 0;

                for (var rol = 0; rol < this.Groups.length; rol++) {
                    rolesCount += this.Groups[i].AccountRoles.length;
                }

                return rolesCount;
            });
        };

        ctor.prototype.canActivate = function () {
            return true;
        };

        ctor.prototype.deactivate = function () {
            this.newUserSubscription.off();
        }

        ctor.prototype.Create = function () {
            createForm.show();
        };

        ctor.prototype.Edit = function (user) {
            router.navigate("credentials/" + user.Id + "/edit", true);
        };

        return ctor;
    });