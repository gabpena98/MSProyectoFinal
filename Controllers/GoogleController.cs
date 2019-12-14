using System.Data;
using ASPSnippets.GoogleAPI;
using System.Linq;
using System.Web.Mvc;
using Metodología_de_Software.Models;
using System.Web.Security;
using System.Web.Script.Serialization;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.People.v1;
using Google.Apis.People.v1.Data;
using Google.Apis.Services;
using System.Threading;
using System.Collections.Generic;
using System;
using Google.Apis.Auth.OAuth2.Responses;

namespace Metodología_de_Software.Controllers
{
    public class GoogleController : Controller
    {
        METOSOFTEntities bd = new METOSOFTEntities();

        public ActionResult Index(string nombre, string email)
        {
            OAuthUserModel model = new OAuthUserModel();
            model.Email = email;
            model.Name = nombre;
            if (model.Email != null)
            {
                Usuario us = (from u in bd.Usuario
                              where u.email == model.Email
                              select u).FirstOrDefault();
                if (us == null)
                {
                    return RedirectToAction("Edit", "Google", model);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home", new { });
                }
            }
            return RedirectToAction("Login","Login");
        }
        public ActionResult Login()
        {
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "335225904765-np9rbn4gfa59k1fgq2vi18ej7jg2kq52.apps.googleusercontent.com",
                    ClientSecret = "OUTdJ6KoWQHkApc3kYmCNk7Z"
                },
                new[] { "profile", "email" }, "user", CancellationToken.None).Result;

            var service = new PeopleService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Test",
            });

            PeopleResource.GetRequest peopleRequest = service.People.Get("people/me");
            peopleRequest.RequestMaskIncludeField = new List<string>()
            {
            "person.names",
            "person.EmailAddresses"
            };
            peopleRequest.Key = "AIzaSyDlwYIixAZwk9pekDp6u4ylRL3r3v3TPwM";
            Person people = peopleRequest.Execute();
            string nombre = "";
            string email = "";
            foreach (var person in people.Names)
            {
                nombre = person.DisplayName + nombre;
            }
            foreach (var person in people.EmailAddresses)
            {
                email = person.Value + email;
            }
            credential.RevokeTokenAsync(CancellationToken.None);
            return RedirectToAction("Index", new { nombre, email });
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
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}