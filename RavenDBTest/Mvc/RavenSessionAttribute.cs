using System;
using System.Web.Mvc;
using Raven.Client;
using StructureMap;

namespace RavenDBTest.Mvc
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class RavenSessionAttribute : FilterAttribute, IActionFilter
	{
		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
		}

		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (filterContext.Exception != null)
				return;

			using (var session = ObjectFactory.GetInstance<IDocumentSession>())
				session.SaveChanges();
		}
	}
}
