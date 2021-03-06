﻿using System.Web;
using System.Web.Optimization;

namespace MoodPocket.WebUI
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
					  "~/Scripts/materialize/materialize.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/materialize/css/materialize.css",
					  "~/Content/site.css"));

			bundles.Add(new ScriptBundle("~/bundles/account-scripts").Include(
				"~/Scripts/AccountScripts/entry.js"));
			bundles.Add(new ScriptBundle("~/bundles/meme-scripts").Include(
				"~/Scripts/MemeScripts/meme.js"));
			bundles.Add(new ScriptBundle("~/bundles/gallery-scripts").Include(
				"~/Scripts/GalleryScripts/gallery.js"));
			bundles.Add(new ScriptBundle("~/bundles/ajax-notifications-scripts").Include(
				"~/Scripts/ajax-notifications.js"));
		}
	}
}
