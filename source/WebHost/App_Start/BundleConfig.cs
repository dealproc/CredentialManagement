﻿using System.Web;
using System.Web.Optimization;

namespace WebHost
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/vendor.js")
                .Include(
                    "~/Scripts/jquery-{version}.js", 
                    "~/Scripts/jquery-ui-{version}.js",
                    "~/Scripts/knockout-{version}.js",
                    "~/Scripts/knockout.mapping-latest.js",
                    "~/Scripts/knockout.validation.js",
                    "~/Scripts/bootstrap.js"
                )
            );

            bundles.Add(new StyleBundle("~/Content/css")
                .Include(
                    "~/Content/ie10mobile.css",
                    "~/Content/durandal.css",
                    "~/Content/bootstrap.css",
                    "~/Content/bootstrap-responsive.css"
                )
            );

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}