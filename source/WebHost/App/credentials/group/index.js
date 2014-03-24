/// <reference path="../Scripts/require.js" />
/// <reference path="../Scripts/knockout-2.3.0.js" />
/// <reference path="../Scripts/jquery-2.0.3.js" />
/// <reference path="../Scripts/bootstrap.js" />

define([
    "durandal/app",
    "./create",
    "dataservices/provider"],
    function (app, createForm, provider) {
        var ctor = function () { };

        ctor.grid = undefined;

        ctor.prototype.compositionComplete = function () {
            var _Self = this;
            _Self.grid = $("#tblGroups");
            _Self.grid.dataTable({
                bProcessing: true,
                bServerSide: true,
                bJQueryUI: false,
                bSortable: true,
                aoColumnDefs: [
                    {
                        sTitle: "Name", mDataProp: "Name", aTargets: [0], sClass: "col-md-2",
                        mRender: function (data, type, full) {
                            if (data !== undefined && data !== null) {
                                return "<a href=\"#groups/edit/" + full.Id.toString() + "\">" + data + "</a>";
                            } else {
                                return "";
                            }
                        }
                    },
                    { sTitle: "Description", mDataProp: "Description", aTargets: [1], sClass: "col-md-3" },
                    {
                        sTile: "Parent Groups", mDataProp: "Id", aTargets: [2], sClass: "col-md-2",
                        mRender: function (data, type, full) {
                            if (data !== undefined && data !== null) {
                                return "<a href=\"#groups/edit/" + full.Id.toString() + "\">View Groups (" + full.ParentGroupCount.toString() + ")</a>";
                            } else {
                                return "";
                            }
                        }
                    },
                    {
                        sTitle: "Roles", mDataProp: "Id", aTargets: [3], sClass: "col-md-2",
                        mRender: function (data, type, full) {
                            if (data !== undefined && data !== null) {
                                var editGroupLink = "<a href=\"#groups/edit/" + full.Id.toString() + "/roles\">View Roles (" + full.RoleCount.toString() + ")</a>";
                                return editGroupLink;
                            } else {
                                return "";
                            }
                        }
                    },
                    {
                        sTitle: "Users", mDataProp: "Id", aTargets: [4], sClass: "col-md-2",
                        mRender: function (data, type, full) {
                            if (data !== undefined && data !== null) {
                                var editGroupLink = "<a href=\"#groups/edit/" + full.Id.toString() + "/users\">View Users (" + full.UserCount.toString() + ")</a>";
                                return editGroupLink;
                            } else {
                                return "";
                            }
                        }
                    },
                    {
                        sTitle: "", mDataProp: "Id", aTargets: [5], sClass: "col-md-1",
                        mRender: function (data, type, full) {
                            if (!full.IsSystem) {
                                return "<a href=\"#\" onclick=\"javascript: { var ctx = ko.contextFor($('#tblGroups')[0]); ctx.$data.remove(" + full.Id + "); return false; }\">Remove</a>";
                            }
                            return "";
                        }
                    }
                ],
                sAjaxSource: "/api/Group"
            });

            _Self.newGroupSubscription = app.on("group:add", function (group) {
                _Self.grid.fnDraw();
            });
            _Self.updateGroupSubscription = app.on("group:update", function (group) {
                _Self.grid.fnDraw();
            });
            _Self.removeGroupSubscription = app.on("group:remove", function (group) {
                _Self.grid.fnDraw();
            });
        }

        ctor.prototype.deactivate = function () {
            this.newGroupSubscription.off();
            this.updateGroupSubscription.off();
            this.removeGroupSubscription.off();
        }


        ctor.prototype.Create = function () {
            createForm.show();
        }
        ctor.prototype.remove = function (id) {
            var svc = provider.define("Group");
            svc.get({ id: id })
                .then(function (data) {
                    app.showMessage("Delete '" + data.Name + "'?", "Confirmation", ["Yes", "No"])
                        .then(function (response) {
                            if (response == "Yes") {
                                svc.remove({ id: id })
                                    .then(function (data) {
                                        app.trigger("group:remove", data);
                                    });
                            }
                        });
                });
        }
        return ctor;
    });