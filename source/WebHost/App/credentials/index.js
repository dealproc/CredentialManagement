/// <reference path="../Scripts/require.js" />
/// <reference path="../Scripts/knockout-2.3.0.js" />
/// <reference path="../Scripts/jquery-2.0.3.js" />
/// <reference path="../Scripts/bootstrap.js" />

define(["plugins/router", "knockout"], function (router, ko) {

    var childRouter = router.createChildRouter()
            .makeRelative({
                moduleId: "credentials",
                fromParent: true
            }).map([
                { route: ["", "users"], moduleId: "user/index", title: "Users", nav: true },
                { route: [":id/edit", "users/:id/edit"], moduleId: "user/editor", title: "Edit User", hash: "#users" },
                { route: "resellers", moduleId: "reseller/index", title: "Resellers", nav: true },
                { route: "resellers/create", moduleId: "reseller/editor", title: "Create Reseller" },
                { route: "resellers/:id/edit", moduleId: "reseller/editor", title: "Edit Reseller" },
                { route: "groups", moduleId: "group/index", title: "Groups", nav: true },
                { route: "groups/:id/edit", moduleId: "group/editor", title: "Edit Group", hash: "#groups" },
                { route: "roles", moduleId: "role/index", title: "Roles", nav: true },
                { route: "roles/:id/edit", moduleId: "role/editor", title: "Edit Role", hash: "#roles" }
            ]).buildNavigationModel();

    return {
        router: childRouter
    };
});