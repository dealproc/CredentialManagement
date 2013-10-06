/// <reference path="../Scripts/require.js" />
/// <reference path="../Scripts/knockout-2.3.0.js" />
/// <reference path="../Scripts/jquery-2.0.3.js" />
/// <reference path="../Scripts/bootstrap.js" />

define(["plugins/router"], function (router) {

    router.map([
        { route: ["", "dashboard"], moduleId: "index", title: "Dashboard", nav: true },
        { route: ["credentials*details"], moduleId: "credentials/index", title: "User Maintenance", nav: true, hash: "#credentials" },
    ])
    .buildNavigationModel()
    .mapUnknownRoutes("user/index", "not-found")
    .activate();

    var shell = {
        router: router
    };

    return shell;
});