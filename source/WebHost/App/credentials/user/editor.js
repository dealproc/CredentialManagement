define([
    "durandal/app",
    "plugins/router",
    "dataservices/provider",
    "plugins/observable",
    "errorMessage"],
    function (app, router, provider, observable, errorMessage) {
        var ctor = function () { };

        ctor.prototype.dataService = provider.define("User");

        ctor.prototype.model = {
        };

        ctor.prototype.canActivate = function (id) {

            var _Self = this;

            var deferred = $.Deferred(function (def) {
                _Self.dataService
                    .get({ id: id })
                    .then(function (data) {
                        _Self.model = data;
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

            observable(_Self.model, "Username").extend({ required: { message: "Username is Required." } });
            observable(_Self.model, "Email").extend({ required: { message: "Email is Required", email: true } });

            observable.defineProperty(_Self, "errors", ko.validation.group(_Self.model));

        };

        ctor.prototype.save = function () {
            var _Self = this;

            if (_Self.errors.length === 0) {

                _Self.dataService.save(_Self.model)
                .then(function (data) {
                    router.navigate("credentials/", true);
                })
                .fail(function (xhr, textStatus, errorThrown) {
                    errorMessage.show([xhr.responseText], "Cannot save user.");
                });

            } else {
                errorMessage.show(_Self.errors, "Cannot save user.");
            }
        };

        ctor.prototype.cancel = function () {
            router.navigate("credentials/", true);
        };

        return ctor;
    });