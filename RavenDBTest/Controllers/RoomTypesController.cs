using System.Web.Mvc;
using RavenDBTest.Model;


namespace RavenDBTest.Controllers
{
    public class RoomTypesController : BaseController
    {

        public ActionResult Index()
        {
            return View(RavenSession.Query<RoomType>());

        }
        public ActionResult New()
        {
            return View();
        }

        public ActionResult Create(RoomType room )
        {
            RavenSession.Store(room);

            return RedirectToAction("Index");
        }

    }
}
