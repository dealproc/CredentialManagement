define([
    "durandal/app",
    "dataservices/provider",
    "plugins/observable",
    "./assignroles"
], function (app, provider, observable, assignRolesDialog) {

    var ctor = function () { };

    ctor.prototype.dataService = provider.define("GroupRole");
    ctor.prototype.groupId = undefined;
    ctor.prototype.groupRoles = undefined;
    ctor.prototype.selectedRoles = [];
    ctor.prototype.selectAllRoles = false;

    ctor.prototype.canActivate = function (id) {
        var _Self = this;

        var deferred = $.Deferred(function (def) {
            _Self.dataService.get({ id: id })
                .then(function (data) {
                    _Self.groupRoles = data;
                    def.resolve(true);
                })
                .fail(function (xhr, textStatus, errorThrown) {
                    def.reject(xhr.responseText, textStatus, errorThrown);
                });
        });
        return deferred;
    };

    ctor.prototype.activate = function (id) {
        var _Self = this;

        _Self.selectedRoles = [];

        _Self.groupId = id;

        _Self.roleAssignmentsChangedSubscription = app.on("roleassignments:change", function (data) {
            _Self.groupRoles = data;
        });

        observable(_Self, "selectAllRoles").subscribe(function (shouldSelectAll) {
            if (shouldSelectAll) {
                _Self.selectedRoles = $.map(_Self.groupRoles, function (groupRole, index) {
                    return parseFloat(groupRole.Id);
                });
            } else {
                _Self.selectedRoles = []
            }
        });
    };

    ctor.prototype.deactivate = function () {
        this.roleAssignmentsChangedSubscription.off();
    };

    ctor.prototype.AssignRoles = function () {
        assignRolesDialog.show(this.groupId);
    };

    ctor.prototype.UnassignRoles = function () {
        var _Self = this;

        var asssignmentsToRemove = $.grep(_Self.groupRoles, function (gr, index) {
            return _Self.selectedRoles.indexOf(parseFloat(gr.Id)) >= 0;
        });
        var assignmentKeys = $.map(asssignmentsToRemove, function (gr, index) {
            return { RoleId: gr.RoleId, AccountId: gr.AccountId };
        });

        _Self.dataService.action("Unassign", {
            GroupId: _Self.groupId,
            Assignments: assignmentKeys
        })
        .then(function (data) {
            app.trigger("roleassignments:change", data);
        });
    };

    return ctor;

});