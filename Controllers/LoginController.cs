using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Metodología_de_Software.Controllers
{
    public class LoginController : Controller
    {
        METOSOFTEntities bd = new METOSOFTEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View(new Models.UsuariosModel());
        }

        [HttpPost]
        public ActionResult Login(Models.UsuariosModel model)
        {
            Usuario us = (from u in bd.Usuario
                          where u.nombreUsuario == model.NombreUsuario &&
                          u.contraseña == model.Contraseña
                          select u).FirstOrDefault();
            if(us == null)
            {
                FormsAuthentication.SignOut();
                model.Mensaje = "Usuario o contraseña incorrectos.";
                return View(model);
            }
            FormsAuthentication.SetAuthCookie(model.NombreCompleto, true);
            return RedirectToAction("Index", "Home", new { });
        }
    }
}