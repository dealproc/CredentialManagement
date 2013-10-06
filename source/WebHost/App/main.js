/// <reference path="../Scripts/require.js" />
/// <reference path="../Scripts/knockout-2.3.0.js" />
/// <reference path="../Scripts/jquery-2.0.3.js" />
/// <reference path="../Scripts/bootstrap.js" />

requirejs.config({
    paths: {
        "text": "../Scripts/text",
        "durandal": "../Scripts/durandal",
        "plugins": "../Scripts/durandal/plugins",
        "transitions": "../Scripts/durandal/transitions"
    }
});

define("jquery", function () { return jQuery; });
define("knockout", ko);

define([
    "durandal/system",
    "durandal/app",
    "durandal/viewLocator"],
    function (system, app, viewLocator) {
        //>>excludeStart("build", true);
        system.debug(true);
        //>>excludeEnd("build", true);

        ko.validation.init({
            grouping: { deep: true, observable: true }
        });

        app.Title = "Your Title - Copyright?";

        app.configurePlugins({
            router: true,
            dialog: true,
            widget: true,
            observable: true // allows us to use plain js objects and roll it into knockout automatically... Sweet!
        });

        app.start().then(function () {
            viewLocator.useConvention();

            app.setRoot("shell");
        });
    });