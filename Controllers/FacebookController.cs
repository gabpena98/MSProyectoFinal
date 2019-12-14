using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using ASPSnippets.FaceBookAPI;
using Metodología_de_Software.Models;

namespace Metodología_de_Software.Controllers
{
    public class FacebookController : Controller
    {
        METOSOFTEntities bd = new METOSOFTEntities();

        // GET: Facebook
        public ActionResult Index()
        {
            OAuthUserModel faceBookUser = new OAuthUserModel();
            if (Request.QueryString["error"] == "access_denied")
            {
                ViewBag.Message = "User has denied access.";
            }
            else
            {
                string code = Request.QueryString["code"];
                if (!string.IsNullOrEmpty(code))
                {
                    string data = FaceBookConnect.Fetch(code, "me?fields=id,name,email");
                    faceBookUser = new JavaScriptSerializer().Deserialize<OAuthUserModel>(data);
                    
                }
            }
            Usuario us = (from u in bd.Usuario
                          where u.email == faceBookUser.Email
                          select u).FirstOrDefault();
            if (us == null)
            {
                return RedirectToAction("Edit", "Facebook", faceBookUser);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(faceBookUser.Name, true);
                return RedirectToAction("Index", "Home", new { });
            }
        }

        public EmptyResult Login()
        {
            FaceBookConnect.API_Key = "3251252588481445";
            FaceBookConnect.API_Secret = "566803077f7238c8254feaf0ad87b7e8";
            FaceBookConnect.Authorize("user_photos,email", string.Format("{0}://{1}/{2}", Request.Url.Scheme, Request.Url.Authority, "Facebook/Index/"));
            return new EmptyResult();
        }

        public ActionResult Edit(Models.OAuthUserModel model)
        {
            Models.UsuariosModel user = new Models.UsuariosModel();
            user.NombreCompleto = model.Name;
            user.Correo = model.Email;
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Models.UsuariosModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario us = new Usuario();
                us.email = model.Correo;
                us.contraseña = model.Contraseña;
                us.nombrePersona = model.NombreCompleto;
                us.nombreUsuario = model.NombreUsuario;
                bd.Usuario.Add(us);
                bd.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(model);
        }

    }
}