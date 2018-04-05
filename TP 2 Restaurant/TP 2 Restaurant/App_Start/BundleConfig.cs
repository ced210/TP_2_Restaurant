using System.Web;
using System.Web.Optimization;

namespace TP_2_Restaurant
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                          "~/Scripts/jquery-{version}.js",
                          "~/Scripts/jquery-ui-{version}.js",
                          "~/Scripts/bootbox.js",
                          "~/Scripts/jquery.maskedinput.js",
                          "~/Scripts/AppUtilities.js",
                          "~/Scripts/RatingBar.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap.css",
                     "~/Content/site.css",
                     "~/Content/themes/base/jquery-ui.css",
                     "~/Content/App_Styles.css",
                     "~/Content/RatingBar.css",
                     "~/Content/App_Styles.css"));
        }
    }
}
