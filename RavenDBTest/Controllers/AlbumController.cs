using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RavenDBTest.Model;

namespace RavenDBTest.Controllers
{
    public class AlbumController :    BaseController
    {

        public ActionResult Index()
        {
            return View(RavenSession.Query<Album>());
        }
        public ActionResult Index2()
        {
            return View(RavenSession.Query<dynamic>());
        }
    }
}
