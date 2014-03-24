define([
    "durandal/app",
    "dataservices/provider",
    "plugins/dialog",
    "plugins/observable",
    "errorMessage"
],
    function (app, provider, dialog, observable, errorMessage) {
        var ctor = function () { };

        ctor.prototype.dataService = provider.define("Group");
        ctor.prototype.parentGroups = undefined;

        ctor.prototype.title = "Create Group";

        ctor.prototype.cancel = function () {
            dialog.close(this);
        };

        ctor.prototype.save = function () {
            var _Self = this;

            _Self.validationModel.errors.showAllMessages();
            if (_Self.validationModel.isValid()) {
                _Self.dataService.save(_Self.model)
                .then(function (data) {
                    app.trigger("group:add", data);
                    dialog.close(_Self);
                })
                .fail(function (data) {
                    alert("Something bad happened!");
                });
            } else {
                errorMessage.show(_Self.validationModel.errors(), "Cannot save Group.");
            }
        };

        ctor.prototype.ClearParentGroups = function () {
            this.model.ParentGroups = [];
        }

        ctor.show = function () {
            var dlg = new ctor();

            dlg.model = {
                Id: 0,
                Name: "",
                GroupKey: "",
                Description: "",
                ParentGroups: []
            }

            dlg.dataService.getAll({})
                .then(function (allGroups) {
                    dlg.parentGroups = $.grep(allGroups, function (g, idx) {
                        return g.ParentGroupCount === 0;
                    });
                });

            observable(dlg.model, "Name").extend({ required: { message: "Group Name is Required." } });

            dlg["validationModel"] = ko.validatedObservable({
                p1: dlg.model
            });

            return dialog.show(dlg);
        };

        return ctor;
    });