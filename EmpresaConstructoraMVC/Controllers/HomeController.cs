using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;

namespace EmpresaConstructoraMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View(new Vendedor());
        }


        [HttpPost]
        public ActionResult Login(string nombre, string clave)
        {
            Vendedor vendedorLogueado = Sistema.InstanciaSistema.VendedorAutorizado(nombre, clave);
            

            if (vendedorLogueado != null)
            {
                Session["UsuarioLogueado"] = vendedorLogueado;

                return Redirect("Index");
            }
            else
            {
                ViewBag.Mensaje = "ERROR. Usuario o contraseña incorrectas.";
                return View(new Vendedor());
            }

        }


        public ActionResult Logout()
        {
            Session.Remove("UsuarioLogueado");
            return RedirectToAction("Login");
        }

    }
}

