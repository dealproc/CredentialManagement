/// <reference path="../Scripts/require.js" />
/// <reference path="../Scripts/knockout-2.3.0.js" />
/// <reference path="../Scripts/jquery-2.0.3.js" />
/// <reference path="../Scripts/bootstrap.js" />

define([
    "durandal/app",
    "dataservices/provider",
    "./Create"],
    function (app, provider, createForm) {
        var ctor = function () { };

        ctor.prototype.groups = [];

        ctor.prototype.dataService = provider.define("Group");

        ctor.prototype.activate = function () {
            var _Self = this;

            _Self.newGroupSubscription = app.on("group:add", function (group) {
                _Self.groups.push(group);
            });
            _Self.updateGroupSubscription = app.on("group:update", function (group) {
                var idx = -1;
                for (var i = 0; i < _Self.groups.length; i++) {
                    if (_Self.groups[i].Id == group.Id) {
                        idx = i;
                        break;
                    };
                };
                if (idx >= 0) {
                    _Self.groups.splice(idx, 1, group);
                };
            });
            _Self.removeGroupSubscription = app.on("group:remove", function (group) {
                var idx = -1;
                for (var i = 0; i < _Self.groups.length; i++) {
                    if (_Self.groups[i].Id == group.Id) {
                        idx = i;
                        break;
                    };
                };
                if (idx >= 0) {
                    _Self.groups.splice(idx, 1);
                };
            });

            _Self.dataService.get({})
            .then(function (data) {
                _Self.groups = data;
            })
            .fail(function (xhr, textStatus, errorThrown) {
                def.reject(xhr.responseText, textStatus, errorThrown);
            });
        };

        ctor.prototype.canActivate = function () {
            return true;
        };

        ctor.prototype.deactivate = function () {
            this.newGroupSubscription.off();
            this.updateGroupSubscription.off();
            this.removeGroupSubscription.off();
        }

        ctor.prototype.Create = function () {
            createForm.show();
        };

        ctor.prototype.Edit = function (group) {
            alert("Hello: " + group.Name);
        };

        return ctor;
    });