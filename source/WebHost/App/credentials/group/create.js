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

        ctor.prototype.title = "Create Group";

        ctor.prototype.cancel = function () {
            dialog.close(this);
        };

        ctor.prototype.save = function () {
            var _Self = this;

            if (_Self.model.errors.length === 0) {
                _Self.dataService.save(_Self.model)
                .then(function (data) {
                    app.trigger("group:add", data);
                    dialog.close(_Self);
                })
                .fail(function (data) {
                    alert("Something bad happened!");
                });
            } else {
                errorMessage.show(_Self.model.errors, "Cannot save Group.");
            }
        };

        ctor.show = function () {
            var dlg = new ctor();

            dlg.model = {
                Id: 0,
                Name: "",
                GroupKey: "",
                Description: "",
                ParentGroups: []
            }

            observable(dlg.model, "Name").extend({ required: { message: "Group Name is Required." } });

            observable.defineProperty(dlg.model, "errors", ko.validation.group(dlg.model));

            return dialog.show(dlg);
        };

        return ctor;
    });