define(["dataservices/provider", "plugins/dialog", "plugins/observable", "errorMessage", "durandal/app"],
    function (provider, dialog, observable, errorMessage, app) {
        var ctor = function () { };

        ctor.prototype.dataService = provider.define("Role");

        ctor.prototype.title = "Create Role";

        ctor.prototype.cancel = function () {
            dialog.close(this);
        };

        ctor.prototype.save = function () {

            var _Self = this;

            if (_Self.model.errors.length === 0) {
                _Self.dataService.save(_Self.model)
                    .then(function (data) {
                        app.trigger("role:new", data);
                        dialog.close(_Self);
                    })
                    .fail(function (data) {
                        alert("Something bad happened.");
                    });
            } else {
                errorMessage.show(_Self.model.errors, "Cannot save user.");
            }
        };

        ctor.show = function () {
            var dlg = new ctor();

            dlg.model = {
                Name: ""
            };
            observable(dlg.model, "Name").extend({ required: { message: "Role Name is Required." } });

            observable.defineProperty(dlg.model, "errors", ko.validation.group(dlg.model));

            return dialog.show(dlg);
        };

        return ctor;
    });