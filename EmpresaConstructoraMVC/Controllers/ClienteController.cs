using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;

namespace EmpresaConstructoraMVC.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UsuarioLogueado"] == null) return new HttpStatusCodeResult(401);
            if (Sistema.InstanciaSistema.ListaClientes.Count == 0)
            {
                ViewBag.Mensaje = "No hay clientes en el sistema.";
            }
            return View(Sistema.InstanciaSistema.ListaClientes);
        }


        [HttpPost]
        public ActionResult filtrarCliente(DateTime? fechaIni, DateTime? fechaFin)
        {
            if (Session["UsuarioLogueado"] == null) return new HttpStatusCodeResult(401);

            if (fechaIni == null || fechaFin == null)
            {
                ViewBag.Mensaje = "Las fechas no pueden estar vacías";
                return View("Index", new List<Cliente>());
            }

            DateTime fechaIniAux = (DateTime)fechaIni;
            DateTime fechaFinAux = (DateTime)fechaFin;

            ViewBag.FechaIni = fechaIniAux.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = fechaFinAux.ToString("yyyy-MM-dd");

            List<Cliente> listaAux = Sistema.InstanciaSistema.ListadoClientesRango(fechaIniAux, fechaFinAux);

            if (listaAux.Count == 0)
            {
                ViewBag.Mensaje = "No hay clientes buscados en el rango de fechas establecido.";
            }

            return View("Index", listaAux);

            // return View("Index", Sistema.InstanciaSistema.ListaClientes);
        }



        public ActionResult CrearCliente()
        {
            return View(new Cliente());
        }

        [HttpPost]
        public ActionResult CrearCliente(Cliente nuevoCliente, string idApto)
        {
            if (!Sistema.InstanciaSistema.campoVacio(nuevoCliente.Nombre) && !Sistema.InstanciaSistema.campoVacio(nuevoCliente.Apellido) && !Sistema.InstanciaSistema.campoVacio(nuevoCliente.Documento) && !Sistema.InstanciaSistema.campoVacio(nuevoCliente.Direccion) && !Sistema.InstanciaSistema.campoVacio(nuevoCliente.Telefono.ToString()))
            {
                if (!Sistema.InstanciaSistema.esNumerico(nuevoCliente.Apellido) && !Sistema.InstanciaSistema.esNumerico(nuevoCliente.Nombre) && !Sistema.InstanciaSistema.esNumerico(nuevoCliente.Direccion))
                {
                    if (Sistema.InstanciaSistema.esNumerico(nuevoCliente.Telefono.ToString()) && Sistema.InstanciaSistema.esNumerico(nuevoCliente.Documento))
                    {
                        if (Sistema.InstanciaSistema.AgregarCliente(nuevoCliente))
                        {
                            ViewBag.Mensaje = "El cliente se ingreso correctamente";
                            return View(new Cliente());
                        }
                        else
                        {
                            ViewBag.Mensaje = "Verifica tus ingresos.";
                            return View(nuevoCliente);
                        }
                    }
                    else
                    {
                        ViewBag.Mensaje = "Los campos teléfono y documento deben ser numéricos.";
                        return View(nuevoCliente);
                    }
                }
                else
                {
                    ViewBag.Mensaje = "Tanto los campos nombre, apellido, como dirección, no pueden ser numéricos.";
                    return View(nuevoCliente);
                }
            }
            else
            {
                ViewBag.Mensaje = "Alguno de los campos ha quedado sin completar...";
                return View(nuevoCliente);
            }

        }


        public ActionResult BorrarCliente(string idCliente)
        {
            if (Session["UsuarioLogueado"] == null) return new HttpStatusCodeResult(401);

            if (Sistema.InstanciaSistema.BorrarCliente(idCliente))
            {
                ViewBag.Mensaje = "Cliente borrado con éxito.";
                return View("Index", Sistema.InstanciaSistema.ListaClientes);
            }
            else
            {
                ViewBag.Mensaje = "El cliente no ha podido borrarse del sistema ya que tiene aptos comprados a su nombre.";
                return View("Index", Sistema.InstanciaSistema.ListaClientes);
            }
        }

        public ActionResult EditarCliente(string idCliente)
        {
            if (Session["UsuarioLogueado"] == null) return new HttpStatusCodeResult(401);

            Cliente aux = Sistema.InstanciaSistema.BuscarCliente(idCliente);
            //Session["ClienteAeditar"] = aux;

            return View(aux);
        }

        [HttpPost]
        public ActionResult EditarCliente(Cliente clienteEditado)
        {
            if (Session["UsuarioLogueado"] == null) return new HttpStatusCodeResult(401);

            if (clienteEditado != null)
            {
                if (!Sistema.InstanciaSistema.campoVacio(clienteEditado.Nombre) && !Sistema.InstanciaSistema.campoVacio(clienteEditado.Apellido) && !Sistema.InstanciaSistema.campoVacio(clienteEditado.Documento) && !Sistema.InstanciaSistema.campoVacio(clienteEditado.Direccion) && !Sistema.InstanciaSistema.campoVacio(clienteEditado.Telefono.ToString()))
                {
                    if (!Sistema.InstanciaSistema.esNumerico(clienteEditado.Apellido) && !Sistema.InstanciaSistema.esNumerico(clienteEditado.Nombre) && !Sistema.InstanciaSistema.esNumerico(clienteEditado.Direccion))
                    {
                        if (Sistema.InstanciaSistema.esNumerico(clienteEditado.Telefono.ToString()) && Sistema.InstanciaSistema.esNumerico(clienteEditado.Documento))
                        {
                            if (Sistema.InstanciaSistema.ModificarCliente(clienteEditado))
                            {
                                ViewBag.Mensaje = "Cliente editado con éxito.";
                                return View("Index", Sistema.InstanciaSistema.ListaClientes);
                            }
                            else
                            {
                                return View(clienteEditado);
                            }
                        }
                        else
                        {
                            ViewBag.Mensaje = "Los campos teléfono y documento deben ser numéricos.";
                            return View(clienteEditado);
                        }
                    }
                    else
                    {
                        ViewBag.Mensaje = "Tanto los campos nombre, apellido, como dirección, no pueden ser numéricos.";
                        return View(clienteEditado);
                    }
                }
            }
              
                    ViewBag.Mensaje = "Alguno de los campos ha quedado sin completar...";
                    return View(clienteEditado);
               
        }
    }
}