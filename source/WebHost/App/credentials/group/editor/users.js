define([
    "durandal/app",
    "plugins/router",
    "dataservices/provider",
    "plugins/observable",
    "./assignusers"
], function (app, router, provider, observable, addUnassignedDialog) {

    var ctor = function () { };

    ctor.prototype.dataService = provider.define("GroupUser");

    ctor.prototype.users = undefined;
    ctor.prototype.groupId = undefined;
    ctor.prototype.selectedUsers = [];
    ctor.prototype.selectAllUsers = false;

    ctor.prototype.canActivate = function (id) {
        var _Self = this;

        var deferred = $.Deferred(function (def) {
            _Self.dataService.get({ id: id })
                .then(function (data) {
                    _Self.users = data;
                    def.resolve(true);
                })
                .fail(function (xhr, textStatus, errorThrown) {
                    def.reject(xhr.responseText, textStatus, errorThrown);
                });
        });

        return deferred;
    }

    ctor.prototype.activate = function (id) {

        var _Self = this;

        _Self.groupId = id;

        _Self.userAssignmentsChangedSubscription = app.on("userassignments:change", function (data) {
            _Self.users = data;
        });

        observable(_Self, "selectAllUsers").subscribe(function (shouldSelectAll) {
            if (shouldSelectAll) {
                _Self.selectedUsers = $.map(_Self.users, function (user, index) {
                    return user.Id;
                });
            } else {
                _Self.selectedUsers = [];
            }
        });

    };

    ctor.prototype.deactivate = function () {
        this.userAssignmentsChangedSubscription.off();
    };

    ctor.prototype.Edit = function (user) {
        router.navigate(user.Id.toString() + "/edit", true);
        return false;
    };

    ctor.prototype.AssignUsers = function () {
        addUnassignedDialog.show(this.groupId);
    };

    ctor.prototype.UnassignUsers = function () {

        var _Self = this;

        _Self.dataService.action("Unassign", { GroupId: _Self.groupId, UserIds: _Self.selectedUsers })
            .then(function (data) {
                app.trigger("userassignments:change", data);
            });
            
    };

    return ctor;

});