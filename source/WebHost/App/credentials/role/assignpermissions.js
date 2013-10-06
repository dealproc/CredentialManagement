define([
    "dataservices/provider",
    "plugins/dialog",
    "plugins/observable",
    "errorMessage",
    "durandal/app"
], function (provider, dialog, observable, errorMessage, app) {
    var ctor = function () { };

    ctor.prototype.dataService = provider.define("RolePermission");

    ctor.prototype.title = "Permissions";

    ctor.prototype.cancel = function () {
        dialog.close(this);
    }
    ctor.prototype.save = function () {
        var _Self = this;

        _Self.dataService.save(_Self.model)
            .then(function (data) {
                app.trigger("role:update", data);
                dialog.close(_Self);
            }).fail(function (data) {
                alert("Something bad happened.");
            });

    };

    ctor.show = function (role) {

        var dlg = new ctor();

        dlg.model = {
            RoleId: role.Id,
            PermissionIds: []
        };
        dlg.RoleName = role.Name;
        dlg.permissions = [];

        var svc = provider.define("Permission");
        svc.getAll({})
        .then(function (data) {
            dlg.permissions = data;
        });

        return dialog.show(dlg);
    };

    return ctor;
});