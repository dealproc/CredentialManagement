define([
    "durandal/app",
    "plugins/dialog",
    "dataservices/provider",
    "plugins/observable",
    "errorMessage"
], function (app, dialog, provider, observable, errorMessage) {
    var ctor = function () { };


    ctor.prototype.title = "Assign roles";

    ctor.prototype.AccountIds = [];
    ctor.prototype.roles = [];
    ctor.prototype.accounts = [];
    ctor.prototype.ReplaceExistingAssignments = false;
    ctor.prototype.RoleId = -1;

    ctor.prototype.dataService = provider.define("GroupRole");

    ctor.prototype.assignRole = function () {

        var _Self = this;

        _Self.validationModel.errors.showAllMessages();
        if (_Self.validationModel.isValid()) {
            _Self.dataService.action("Assign", {
                    GroupId: _Self.groupId,
                    RoleId: _Self.RoleId,
                    ReplaceExistingAssignments: _Self.ReplaceExistingAssignments,
                    AccountIds: _Self.AccountIds
                })
                .then(function (data) {
                    app.trigger("roleassignments:change", data);
                    dialog.close(_Self);
                });
        } else {
            errorMessage.show(_Self.validationModel.errors(), "Cannot save assignments");
        }
    };

    ctor.prototype.cancel = function () {
        dialog.close(this);
    };

    ctor.show = function (id) {
        var dlg = new ctor();

        var rolesvc = provider.define("Role");
        var acctsvc = provider.define("Account");

        rolesvc.getAll({})
            .then(function (data) {
                dlg.roles = data;
            });

        acctsvc.getAll({})
            .then(function (data) {
                dlg.accounts = data;
            });


        dlg.groupId = id;

        observable(dlg, "RoleId").extend({ required: { message: "Role selection is Required." } });
        observable(dlg, "AccountIds").extend({ minLength: { params: 1, message: "At least one Account must be selected." } });

        dlg["validationModel"] = ko.validatedObservable({
            p1: dlg
        });

        return dialog.show(dlg);
    };

    return ctor;
});