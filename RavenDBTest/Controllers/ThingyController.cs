using System.Web.Mvc;
using Raven.Client;
using RavenDBTest.Model;

namespace RavenDBTest.Controllers
{
	public class ThingyController : Controller
	{
		private readonly IDocumentSession _documentSession;

		public ThingyController(IDocumentSession documentStore)
		{
			_documentSession = documentStore;
		}

		public ActionResult Index()
		{
			return View(_documentSession.Query<Thingy>());
		}

		public ActionResult New()
		{
			return View();
		}

		public ActionResult Create(Thingy thingy)
		{
			_documentSession.Store(thingy);

			return RedirectToAction("Index");
		}
	}
}
