/// <reference path="../../../Scripts/toastr.js" />
define([
    "dataservices/provider",
    "errorMessage",
    "plugins/router"
], function (provider, errorMessage, router) {

    var ctor = function () { };

    ctor.prototype.dataService = provider.define("Group");

    ctor.prototype.model = undefined;
    ctor.prototype.parentGroups = undefined;

    ctor.prototype.canActivate = function (id) {
        var _Self = this;

        var deferred = $.Deferred(function (def) {

            _Self.dataService.get({ id: id })
                .then(function (groupToEdit) {

                    _Self.dataService.action("TopLevel")
                        .then(function (allGroups) {
                            _Self.parentGroups = $.grep(allGroups, function (g, idx) {
                                return g.Id !== groupToEdit.Id;
                            });

                            _Self.model = groupToEdit;
                            def.resolve(true);
                        });

                })
                .fail(function (xhr, textStatus, errorThrown) {
                    def.reject(xhr.responseText, textStatus, errorThrown);
                });
        });

        return deferred;
    };

    ctor.prototype.activate = function (id) {
    };

    ctor.prototype.ClearParentGroups = function () {
        this.model.ParentGroups = [];
    };

    ctor.prototype.save = function () {
        var _Self = this;

        _Self.dataService.save(_Self.model)
        .then(function (data) {
            _Self.data = data;
            toastr.success("Group has been updated.");
        })
        .fail(function (xhr, textStatus, errorThrown) {

        });
        
    };

    ctor.prototype.cancel = function () {
        router.navigate("groups", true);
    };

    return ctor;

});