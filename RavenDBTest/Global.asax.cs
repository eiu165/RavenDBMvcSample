using System.Web.Mvc;
using System.Web.Routing;
using Raven.Client.Document;
using RavenDBTest.Mvc;

namespace RavenDBTest
{
	public class MvcApplication : System.Web.HttpApplication
	{

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);
		}

		protected void Application_Start()
		{
			GlobalFilters.Filters.Add(new RavenSessionAttribute());

			ControllerBuilder.Current.SetControllerFactory(typeof(StructureMapControllerFactory));

			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);
		}
	}
}
