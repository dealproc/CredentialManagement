define([
    "durandal/app",
    "plugins/dialog",
    "dataservices/provider"
], function (app, dialog, provider) {
    var ctor = function () { };


    ctor.prototype.title = "Assign Users to Groups";

    ctor.prototype.searchTerm = "";
    ctor.prototype.searchResults = undefined;
    ctor.prototype.selectedUsers = [];

    ctor.prototype.dataService = provider.define("GroupUser");

    ctor.prototype.searchUsers = function () {
        var _Self = this;

        _Self.dataService.action("Unassigned", { Id: _Self.groupId, SearchText: _Self.searchTerm === "" ? "*" : _Self.searchTerm })
            .then(function (data) {
                _Self.selectedUsers = [];
                _Self.searchResults = data;
            });
    };

    ctor.prototype.addUsersToGroup = function () {

        var _Self = this;

        _Self.dataService.action("Assign", {GroupId: _Self.groupId, UserIds: _Self.selectedUsers})
            .then(function (data) {
                app.trigger("userassignments:change", data);
                dialog.close(_Self);
            });
    };

    ctor.prototype.cancel = function () {
        dialog.close(this);
    };

    ctor.show = function (id) {
        var dlg = new ctor();

        dlg.groupId = id;

        return dialog.show(dlg);
    };

    return ctor;
});