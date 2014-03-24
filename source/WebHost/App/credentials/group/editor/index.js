define([
    "plugins/router",
    "knockout",
    "dataservices/provider"],
    function (router, ko, provider) {

        var ctor = function () { };

        ctor.prototype.group = {
            Name: "",
            GroupKey: "",
            Description: ""
        };

        ctor.prototype.router = router.createChildRouter()
            .makeRelative({
                moduleId: "group/editor/index",
                fromParent: true
            })
            .map([{ route: ":id/:tab", moduleId: "general", title: "General", nav: true, hash: "#groups/edit" }]);

        ctor.prototype.canActivate = function (id) {
            var _Self = this;
            var splits = id.split("/", 2);
            id = splits[1];


            _Self.router.reset();

            _Self.router.makeRelative({
                moduleId: "group/editor",
                fromParent: true
            })
                .map([
                    { route: [":id", "/:id/general"], moduleId: "general", title: "General", nav: true, hash: "#groups/edit/" + id },
                    { route: ":id/users", moduleId: "users", title: "Users", nav: true, hash: "#groups/edit/" + id + "/users" },
                    { route: ":id/roles", moduleId: "roles", title: "Roles", nav: true, hash: "#groups/edit/" + id + "/roles" }
                ]).buildNavigationModel();


            var groupsvc = provider.define("Group");

            groupsvc.get({ id: id })
                .then(function (data) {
                    _Self.group = data;
                });

            return true;
        };

        ctor.prototype.activate = function (id) {

        };

        return ctor;
    });