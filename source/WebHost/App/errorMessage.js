define(["plugins/dialog", "knockout"], function (dialog, ko) {
    var ctor = function () { };

    ctor.prototype.messages = [];

    ctor.prototype.title = "Errors were reported!";

    ctor.prototype.closeMessageBox = function () {
        dialog.close(this);
    };

    ctor.show = function (errors, title) {

        var dlg = new ctor();
        dlg.messages = errors || dlg.messages;
        dlg.title = title || dlg.title;
        return dialog.show(dlg);
    }

    return ctor;
});