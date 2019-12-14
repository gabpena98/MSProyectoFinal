using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPSnippets.FaceBookAPI;
using System.Web.Script.Serialization;

namespace Metodología_de_Software.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
           return View ();
        }
    }
}