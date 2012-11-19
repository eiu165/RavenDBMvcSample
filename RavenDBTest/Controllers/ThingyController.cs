using System.Web.Mvc;
using RavenDBTest.Model;

namespace RavenDBTest.Controllers
{
	public class ThingyController : BaseController
	{
        public ActionResult Index()
        {
            return View(RavenSession.Query<Thingy>());
        }
        public ActionResult Index2()
        {
            return View(RavenSession.Query<dynamic>());
        }
        public ActionResult New2()
        {
            return View();
        }
        public ActionResult Create2(FormCollection fc)
        {

            RavenSession.Store(new {Name = fc["name"]});

            return RedirectToAction("Index");
        }

		public ActionResult New()
		{
			return View();
		}

		public ActionResult Create(Thingy thingy)
		{
			RavenSession.Store(thingy);

			return RedirectToAction("Index");
		}
	}
}
