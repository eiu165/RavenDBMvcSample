﻿using System.Web.Mvc;
using Raven.Client;

namespace RavenDBTest.Controllers
{
	public abstract class BaseController : Controller
	{
		public IDocumentSession RavenSession { get; private set; }

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			RavenSession = MvcApplication.Store.OpenSession();
		}

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (filterContext.IsChildAction)
				return;

			using (RavenSession)
			{
				if (filterContext.Exception != null)
					return;

				if (RavenSession != null)
					RavenSession.SaveChanges();
			}
		}
	}
}
