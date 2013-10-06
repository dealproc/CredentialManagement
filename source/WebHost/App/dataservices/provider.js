/// <reference path="../../Scripts/jquery-2.0.3.intellisense.js" />
/// <reference path="../../Scripts/jquery-2.0.3.js" />
/// 

define(["jquery", "knockout"],
    function ($, ko) {
        return function () {

            var get = function (params) {
                var resource = this.resource;

                var deferred = $.Deferred(function (def) {

                    if (!params) {
                        def.reject();
                    }

                    $.ajax({
                        url: "/api/" + resource, // may need to be "/api/" + resource "/" + params.Id
                        type: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=UTF-8",
                        data: params
                    })
                    .success(function (data, textStatus, xhr) {
                        def.resolve(data);
                    })
                    .error(function (xhr, textStatus, errorThrown) {
                        def.reject(xhr, textStatus, errorThrown);
                    });
                });

                return deferred;
            };

            var getAll = function (params, options) {
                var resource = this.resource;

                var deferred = $.Deferred(function (def) {

                    $.ajax({
                        url: "/api/" + resource,
                        type: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=UTF-8",
                        data: params
                    })
                    .success(function (data, textStatus, xhr) {
                        def.resolve(data);
                    })
                    .error(function (xhr, textStatus, errorThrown) {
                        def.reject(xhr, textStatus, errorThrown);
                    });
                });

                return deferred;
            };

            var create = function (item) {
                var data = ko.toJSON(item);
                var resource = this.resource;

                var deferred = $.Deferred(function (def) {
                    $.ajax({
                        url: "/api/" + resource,
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=UTF-8",
                        data: data
                    })
                    .success(function (data, textStatus, xhr) {
                        def.resolve(data);
                    })
                    .error(function (xhr, textStatus, errorThrown) {
                        def.reject(xhr, textStatus, errorThrown);
                    });
                });

                return deferred;
            };

            var update = function (item) {
                var data = ko.toJSON(item);
                var resource = this.resource;

                var deferred = $.Deferred(function (def) {
                    $.ajax({
                        url: "/api/" + resource,
                        type: "PUT",
                        dataType: "json",
                        contentType: "application/json; charset=UTF-8",
                        data: data
                    })
                    .success(function (data, textStatus, xhr) {
                        def.resolve(data);
                    })
                    .error(function (xhr, textStatus, errorThrown) {
                        def.reject(xhr, textStatus, errorThrown);
                    });
                });

                return deferred;
            };

            var save = function (item) {
                var resource = this.resource;
                var self = this;
                var deferred;
                var updated = false;

                var unwrapped = ko.toJS(item);
                delete unwrapped["__observable__"];

                if (unwrapped && unwrapped.Id && unwrapped.Id > 0) {
                    updated = true;
                    deferred = self.update(unwrapped);
                } else {
                    deferred = self.create(unwrapped);
                }

                deferred.updated = function (callback) {
                    if (updated) {
                        deferred.then(callback);
                    }
                    return deferred;
                };

                deferred.created = function (callback) {
                    if (!updated) {
                        deferred.then(callback);
                    }
                    return deferred;
                };

                return deferred;
            };

            var remove = function (item, byId) {
                var data = ko.toJSON(item);
                var resource = this.resource;
                var deferred;

                if (byId || byId === undefined) {
                    deferred = $.Deferred(function (def) {
                        $.ajax({
                            url: "/api/" + resource + "/Delete?" + $.param(item),
                            type: "DELETE",
                            dataType: "json",
                            contentType: "application/json; charset=UTF8"
                        })
                        .success(function (data, textStatus, xhr) {
                            def.resolve(data);
                        })
                        .error(function (xhr, textStatus, errorThrown) {
                            def.reject(xhr, textStatus, errorThrown);
                        });
                    });
                } else {
                    deferred = $.Deferred(function (def) {
                        $.ajax({
                            url: "/api/" + resource + "/Delete",
                            type: "DELETE",
                            dataType: "json",
                            contentType: "application/json; charset=UTF8",
                            data: data
                        })
                        .success(function (data, textStatus, xhr) {
                            def.resolve(data);
                        })
                        .error(function (xhr, textStatus, errorThrown) {
                            def.reject(xhr, textStatus, errorThrown);
                        });
                    });
                }

                return deferred;
            };

            var defineRequests = function (resource) {

                return {
                    resource: resource,
                    get: get,
                    getAll: getAll,
                    save: save,
                    update: update,
                    create: create,
                    remove: remove
                }

            };

            return {
                define: defineRequests
            };
        }();
    });