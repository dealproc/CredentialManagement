define([
    "dataservices/provider",
    "plugins/dialog",
    "plugins/observable",
    "errorMessage",
    "durandal/app"
], function (provider, dialog, observable, errorMessage, app) {
    var ctor = function () { };

    ctor.prototype.dataService = provider.define("InheritRole");

    ctor.prototype.title = "Include Role(s)";

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

        dlg.Role = role;
        dlg.roles = [];
        dlg.model = {
            RoleId: role.Id,
            IncludeRoleIds: []
        };

        var svc = provider.define("Role");

        svc.getAll({})
        .then(function (data) {
            var masterRoles = $.grep(data, function (obj, idx) {
                if (obj.Id == role.Id) {
                    return false; // cannot include self in list.
                } else if (obj["IncludedRoles"] === undefined) {
                    return true; // if the includedroles collection is empty, it's master.
                } else if (obj.IncludedRoles === null) {
                    return true; // if the includedroles collection is null, it's master.
                } else {
                    return (obj.IncludedRoles.length === 0); // if the includedroles collection has no elements, it's master.
                }
            });
            dlg.roles = masterRoles;
        });

        return dialog.show(dlg);
    };

    return ctor;
});