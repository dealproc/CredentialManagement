define([
    "plugins/router",
    "dataservices/provider",
    "plugins/observable",
    "errorMessage"],
    function (router, provider, observable, errorMessage) {

        var ctor = function () { };

        ctor.prototype.model = {};

        ctor.prototype.dataService = provider.define("Reseller");

        ctor.prototype.canActivate = function (id) {

            var _Self = this;

            if (id === undefined) {
                _Self.model = {
                    Id: 0,
                    LegalName: "",
                    DBAName: "",
                    BillingAccountNumber: "",
                    UseBillToForShipTo: true,
                    BillTo: {
                        Street: "",
                        City: "",
                        StateIntlOther: "",
                        ZipPostal: "",
                        Country: ""
                    },
                    ShipTo: {
                        Street: " ",
                        City: " ",
                        StateIntlOther: " ",
                        ZipPostal: " ",
                        Country: " "
                    }
                };
                return true;
            } else {
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
        };

        ctor.prototype.activate = function (id) {
            var _Self = this;

            if (_Self["BillTo"] === undefined) {
                _Self["BillTo"] = {
                    Street: "",
                    City: "",
                    StateIntlOther: "",
                    ZipPostal: "",
                    Country: ""
                };
            }
            if (_Self["ShipTo"] === undefined) {
                _Self["ShipTo"] = {
                    Street: "",
                    City: "",
                    StateIntlOther: "",
                    ZipPostal: "",
                    Country: ""
                };
            }

            observable(_Self.model.BillTo, "Street").extend({ required: { message: "Bill To Street is Required." } });
            observable(_Self.model.BillTo, "City").extend({ required: { message: "Bill To City is Required." } });
            observable(_Self.model.BillTo, "StateIntlOther").extend({ required: { message: "Bill To State / Intl. Other is Required." } });
            observable(_Self.model.BillTo, "ZipPostal").extend({ required: { message: "Bill To Zip / Postal Code is Required." } });
            observable(_Self.model.BillTo, "Country").extend({ required: { message: "Bill To Country is Required." } });

            observable(_Self.model.ShipTo, "Street").extend({ required: { message: "Ship To Street is Required.", onlyIf: function () { return !_Self.model.UseBillingForShipTo; } } });
            observable(_Self.model.ShipTo, "City").extend({ required: { message: "Ship To City is Required.", onlyIf: function () { return !_Self.model.UseBillingForShipTo; } } });
            observable(_Self.model.ShipTo, "StateIntlOther").extend({ required: { message: "Ship To State / Intl. Other is Required.", onlyIf: function () { return !_Self.model.UseBillingForShipTo; } } });
            observable(_Self.model.ShipTo, "ZipPostal").extend({ required: { message: "Ship To Zip / Postal Code is Required.", onlyIf: function () { return !_Self.model.UseBillingForShipTo; } } });
            observable(_Self.model.ShipTo, "Country").extend({ required: { message: "Ship To Country is Required.", onlyIf: function () { return !_Self.model.UseBillingForShipTo; } } });

            observable(_Self.model, "LegalName").extend({ required: { message: "Legal Name is Required." } });
            observable(_Self.model, "DBAName").extend({ required: { message: "D.B.A. Name is Required." } });

            observable.defineProperty(_Self, "errors", ko.validation.group(_Self.model));

            observable(_Self.model, "UseBillingForShipTo").subscribe(function (value) {
                if (!value) {
                    _Self.model.ShipTo.Street = "";
                    _Self.model.ShipTo.City = "";
                    _Self.model.ShipTo.StateIntlOther = "";
                    _Self.model.ShipTo.ZipPostal = "";
                    _Self.model.ShipTo.Country = "";
                } else {
                    _Self.model.ShipTo.Street = _Self.model.BillTo.Street;
                    _Self.model.ShipTo.City = _Self.model.BillTo.City;
                    _Self.model.ShipTo.StateIntlOther = _Self.model.BillTo.StateIntlOther;
                    _Self.model.ShipTo.ZipPostal = _Self.model.BillTo.ZipPostal;
                    _Self.model.ShipTo.Country = _Self.model.BillTo.Country;
                }
            });
        };

        ctor.prototype.save = function () {
            var _Self = this;

            if (_Self.errors.length === 0) {

                _Self.dataService.save(_Self.model)
                    .then(function (data) {
                        router.navigate("credentials/resellers", true);
                    })
                    .fail(function (xhr, textStatus, errorThrown) {
                        errorMessage.show([xhr.responseText], "Cannot save Reseller.");
                    });

            } else {
                errorMessage.show(_Self.errors, "Cannot save Reseller.");
            }

        };

        ctor.prototype.cancel = function () {
            router.navigate("credentials/resellers", true);
        };

        return ctor;

    });