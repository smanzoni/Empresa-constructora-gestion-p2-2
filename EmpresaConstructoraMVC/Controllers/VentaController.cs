using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;

namespace EmpresaConstructoraMVC.Controllers
{
    public class VentaController : Controller
    {
        // GET: Venta
        public ActionResult CrearVenta()
        {
            if (Session["UsuarioLogueado"] == null) return new HttpStatusCodeResult(401);

            ViewBag.ListaEdificios = Sistema.InstanciaSistema.ListaEdificios;
            ViewBag.ListaClientes = Sistema.InstanciaSistema.ListaClientes;

            List<Apartamento> listaAptos = new List<Apartamento>();
            ViewBag.ListaAptosEdificio = listaAptos;

            ViewBag.id = null;

            return View(new CompraVenta());
        }

        [HttpPost]
        public ActionResult DesplegarAptos(string idEdificio, string idCliente)
        {

            if (idEdificio != null)
            {
                ViewBag.ListaEdificios = Sistema.InstanciaSistema.ListaEdificios;
                ViewBag.ListaClientes = Sistema.InstanciaSistema.ListaClientes;
                //guarda id edificio en viewBag
                ViewBag.idEdificio = idEdificio;
                //guarda id cliente en viewBag
                ViewBag.idCliente = idCliente;


                List<Apartamento> listaAptos = Sistema.InstanciaSistema.aptosEdificio(idEdificio);
                ViewBag.ListaAptosEdificio = listaAptos;
            }
            return View("CrearVenta");
        }

        [HttpPost]
        public ActionResult IngresarVenta(string idCliente, string idEdificio, string idApto)
        {
            if (Session["UsuarioLogueado"] == null) return new HttpStatusCodeResult(401);

            CompraVenta nuevaVenta = new CompraVenta();
            //Vendedor
            nuevaVenta.ElVendedor = Session["UsuarioLogueado"] as Dominio.Vendedor;
            ViewBag.ListaEdificios = Sistema.InstanciaSistema.ListaEdificios;
            ViewBag.ListaClientes = Sistema.InstanciaSistema.ListaClientes;
            List<Apartamento> listaAptos = Sistema.InstanciaSistema.aptosEdificio(idEdificio);
            ViewBag.ListaAptosEdificio = listaAptos;

            if (ViewBag.ListaClientes != null || ViewBag.ListaCliente.Count > 0)
            {
                if (ViewBag.ListaEdificios != null || ViewBag.ListaEdificios.Count > 0)
                {
                    if (ViewBag.ListaAptosEdificio != null || ViewBag.ListaAptosEdificio.Count > 0)
                    {
                        if (idCliente != null || idCliente != "")
                        {
                            //Cliente
                            nuevaVenta.ElComprador = Sistema.InstanciaSistema.BuscarCliente(idCliente);

                            if (idEdificio != null || idEdificio != "")
                            {
                                //edificioAux
                                Edificio edificioVentaAux = Sistema.InstanciaSistema.BuscarEdificio(idEdificio);

                                if (edificioVentaAux != null)
                                {
                                    //Apartamento
                                    nuevaVenta.ElApartamento = edificioVentaAux.BuscarAptoXId(idApto);

                                    if (idApto != null || idApto != "")
                                    {
                                        //precio
                                        nuevaVenta.Precio = nuevaVenta.CalcularPrecio(idEdificio);
                                        //Fecha
                                        nuevaVenta.FechaCompra = DateTime.Now;

                                        if (Sistema.InstanciaSistema.AgregarVenta(nuevaVenta))
                                        {
                                            ViewBag.Mensaje = "Venta ingresada con éxito.";
                                            return View("IngresarVenta", nuevaVenta);

                                        }
                                    }
                                }
                            }

                        }
                    }

                }
            }

            ViewBag.Mensaje = "Error, revisar los campos ingresados";
            return View("CrearVenta", nuevaVenta);
        }

        public ActionResult ListarVentas()
        {
            if (Session["UsuarioLogueado"] == null) return new HttpStatusCodeResult(401);
            Vendedor vendedorLogueado = Session["UsuarioLogueado"] as Dominio.Vendedor;
            ViewBag.NombreUsuario = vendedorLogueado.Nombre;
            ViewBag.CompraVentaSeleccionada = null;

            List<CompraVenta> misVentas = vendedorLogueado.MisVentas;

            return View(misVentas);
        }

        public ActionResult ListarVentas2(string IdCompraVenta)
        {
            Vendedor vendedorLogueado = Session["UsuarioLogueado"] as Dominio.Vendedor;
            ViewBag.NombreUsuario = vendedorLogueado.Nombre;
            List<CompraVenta> misVentas = vendedorLogueado.MisVentas;


            CompraVenta laVenta = Sistema.InstanciaSistema.BuscarCompraVenta(IdCompraVenta);
            Apartamento elApto = laVenta.ElApartamento;

            ViewBag.Comision = laVenta.valorComision(elApto, vendedorLogueado.IdVendedor);
            ViewBag.TotalComision = (ViewBag.Comision * laVenta.ElApartamento.calcularPrecio()) / 100;

            //CompraVenta seleccionada
            ViewBag.CompraVentaSeleccionada = Sistema.InstanciaSistema.BuscarCompraVenta(IdCompraVenta);

            return View("ListarVentas", misVentas);
        }

    }
}




//         if (Session["UsuarioLogueado"] == null) return new HttpStatusCodeResult(401);

//CompraVenta nuevaVenta = new CompraVenta();

////Vendedor
//nuevaVenta.ElVendedor = Session["UsuarioLogueado"] as Dominio.Vendedor;
//            //Datos del sistema
//            ViewBag.ListaClientes = Sistema.InstanciaSistema.ListaClientes;
//            ViewBag.ListaEdificios = Sistema.InstanciaSistema.ListaEdificios;
//            //obtiene los apartamentos del edificio seleccionado

//            if (ViewBag.ListaClientes != null || ViewBag.ListaCliente.Count > 0)
//            {
//                if (idCliente != null && idCliente != "")
//                {
//                    //Cliente
//                    nuevaVenta.ElComprador = Sistema.InstanciaSistema.BuscarCliente(idCliente);

//                    if (ViewBag.ListaEdificios != null || ViewBag.ListaEdificios.Count > 0)
//                    {
//                        if (idEdificio != null && idEdificio != "")
//                        {
//                            List<Apartamento> listaAptos = Sistema.InstanciaSistema.BuscarEdificio(idEdificio).MisApartamentos;
////lista de aptos del edificio
//ViewBag.ListaAptosEdificio = listaAptos;

//                            if (ViewBag.ListaAptosEdificio != null || ViewBag.ListaAptosEdificio.Count > 0)
//                            {
//                            //edificioAux
//                                Edificio edificioVentaAux = Sistema.InstanciaSistema.BuscarEdificio(idEdificio);

//                                if (edificioVentaAux != null && idApto != null || idApto != "")
//                                {
//                                    //Apartamento
//                                    nuevaVenta.ElApartamento = edificioVentaAux.BuscarAptoXId(idApto);

//                                    if (nuevaVenta.ElApartamento != null)
//                                    {
//                                        //precio
//                                        nuevaVenta.Precio = nuevaVenta.CalcularPrecio(idEdificio);
//                                        //Fecha
//                                        nuevaVenta.FechaCompra = DateTime.Now;

//                                        if (Sistema.InstanciaSistema.AgregarVenta(nuevaVenta))
//                                        {
//                                            ViewBag.Mensaje = "Venta ingresada con éxito.";
//                                            return View("IngresarVenta", nuevaVenta);

//                                        }
//                                    }
//                                }
//                            }

//                        }
//                    }
//                }
//            }

//            ViewBag.Mensaje = "Error, revisar los campos ingresados";
//            return View("CrearVenta", nuevaVenta);
