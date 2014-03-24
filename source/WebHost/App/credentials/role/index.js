/// <reference path="../Scripts/require.js" />
/// <reference path="../Scripts/knockout-2.3.0.js" />
/// <reference path="../Scripts/jquery-2.0.3.js" />
/// <reference path="../Scripts/bootstrap.js" />

define([
    "dataservices/provider",
    "durandal/app",
    "plugins/router",
    "./create",
    "./assignpermissions",
    "./includerole"], function (provider, app, router, createForm, assignPermissionsForm, includeRoleForm) {
        var ctor = function () { };

        ctor.prototype.roles = [];

        ctor.prototype.dataService = provider.define("Role");

        ctor.prototype.activate = function () {
            var _Self = this;

            _Self.newRoleSubscription = app.on("role:new", function (role) {
                _Self._ScrubRole(role);
                _Self.roles.push(role);
            });
            _Self.updateRoleSubscription = app.on("role:update", function (role) {
                _Self._ScrubRole(role);
                var idx = -1;
                for (var i = 0; i < _Self.roles.length; i++) {
                    if (_Self.roles[i].Id == role.Id) {
                        idx = i;
                        break;
                    };
                };
                if (idx >= 0) {
                    _Self.roles.splice(idx, 1, role);
                }
            });
            _Self.removeRoleSubscription = app.on("role:remove", function (role) {
                var idx = -1;
                for (var i = 0; i < _Self.roles.length; i++) {
                    if (_Self.roles[i].Id == role.Id) {
                        idx = i;
                        break;
                    };
                };
                if (idx >= 0) {
                    _Self.roles.splice(idx, 1);
                }
            });

            _Self.dataService.get({})
            .then(function (data) {
                for (var i = 0; i < data.length; i++) {
                    _Self._ScrubRole(data[i]);
                };
                _Self.roles = data;
            })
            .fail(function (xhr, textStatus, errorThrown) {
                alert(xhr.textStatus + " " + errorThrown);
            });
        };

        ctor.prototype._ScrubRole = function (role) {
            if (role["IncludedRoles"] === undefined) {
                role["IncludedRoles"] = [];
            }
            if (role["Permissions"] === undefined) {
                role["Permissions"] = []
            }
        };

        ctor.prototype.canActivate = function () {
            return true;
        };

        ctor.prototype.deactivate = function () {
            this.newRoleSubscription.off();
            this.updateRoleSubscription.off();
            this.removeRoleSubscription.off();
        }

        ctor.prototype.Create = function () {
            createForm.show();
        };

        ctor.prototype.IncludeRoles = function (role) {
            includeRoleForm.show(role);
        };

        ctor.prototype.ExcludeRole = function (inheritedRole, role) {
            _Self = this;

            app.showMessage("Really Exclude '" + inheritedRole.Name + "' from '" + role.Name + "'?", "Exclude Role", ["Confirm", "Cancel"])
            .then(function (dialogResult) {
                if (dialogResult == "Confirm") {
                    var svc = provider.define("InheritRole");
                    svc.remove({ RoleId: role.Id, IncludeRoleIds: [inheritedRole.Id] }, false)
                    .then(function (data) {
                        app.trigger("role:update", data);
                    })
                    .fail(function (data) {
                        alert("Something bad happened.");
                    });
                };
            });
        };

        ctor.prototype.AssignPermissions = function (role) {
            assignPermissionsForm.show(role);
        };

        ctor.prototype.DeleteRole = function (role) {
            _Self = this;
            app.showMessage("Please confirm you wish to remove '" + role.Name + "' from the system.", "Delete Role", ["Confirm", "Cancel"])
            .then(function (dialogResult) {
                if (dialogResult === "Confirm") {
                    _Self.dataService.remove({ id: role.Id })
                    .then(function (data) {
                        app.trigger("role:remove", data);
                    })
                    .fail(function (xhr, textStatus, errorThrown) {
                        alert("Something bad happened.");
                    });
                }
            });
        };

        ctor.prototype.DeletePermission = function (permission, role) {
            var _Self = this;
            app.showMessage("Are you sure we should remove '" + permission.Name + "' from role '" + role.Name + "'.", "Remove Permission", ["Confirm", "Cancel"])
            .then(function (dialogResult) {
                var svc = provider.define("RolePermission");
                if (dialogResult === "Confirm") {
                    svc.remove({ RoleId: role.Id, PermissionIds: [permission.Id] }, false)
                    .then(function (data) {
                        app.trigger("role:update", data);
                    })
                    .fail(function (xhr, textStatus, errorThrown) {
                        alert("Something bad happened.");
                    });
                }
            });
        };

        return ctor;
    });