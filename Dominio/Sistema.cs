using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Sistema
    {
        #region Singleton instancia de Sistema

        // al ser static la podemos llamar desde cualquier lado sin necesidad de instanciar al sistema
        private static Sistema instanciaSistema;
        public static Sistema InstanciaSistema
        {
            get
            {
                if (instanciaSistema == null)
                {
                    instanciaSistema = new Sistema();
                }
                return instanciaSistema;
            }
        }

        #endregion

        #region Atributos

        private List<Edificio> listaEdificios;
        private List<Vendedor> listaVendedores;
        private List<Cliente> listaClientes;
        private List<CompraVenta> listaVentas;

        #endregion

        #region Accesores
        //Sistema puede acceder a las listas de edificios y apartamentos pero no puede modificarlas
        public List<Edificio> ListaEdificios
        {
            get
            {
                return listaEdificios;
            }

            private set
            {
                listaEdificios = value;
            }
        }

        public List<Vendedor> ListaVendedores
        {
            get
            {
                return listaVendedores;
            }

            private set
            {
                listaVendedores = value;
            }
        }

        public List<Cliente> ListaClientes
        {
            get
            {
                return listaClientes;
            }

            private set
            {
                listaClientes = value;
            }
        }

        public List<CompraVenta> ListaVentas
        {
            get
            {
                return listaVentas;
            }

            private set
            {
                listaVentas = value;
            }
        }
        #endregion

        #region Constructor
        //instancia del sistema, este contiene las listas de Edificios y Apartamentos por ahora
        private Sistema()
        {
            this.ListaEdificios = new List<Edificio>();
            this.ListaClientes = new List<Cliente>();
            this.ListaVendedores = new List<Vendedor>();
            this.ListaVentas = new List<CompraVenta>();

            //Al instanciar y crear un sistema en el aspx.cs cargo la lista de edificios que haya y la de apartamentos
            AgregarDatosPrueba();
        }
        #endregion

        #region Metodos utiles en sistema

        #region Metodos Cliente

        public bool AgregarCliente(Cliente unCliente)
        {
            if (unCliente.ValidarCliente() && !this.ListaClientes.Contains(unCliente))
            {
                unCliente.IdCliente = Utilidades.GeneradorId.ObtenerIdCliente();
                this.ListaClientes.Add(unCliente);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Cliente BuscarCliente(string idCliente)
        {
            int i = 0;
            bool encontreCliente = false;
            Cliente clienteBuscado = null;

            while (i < ListaClientes.Count && !encontreCliente)
            {
                if (ListaClientes[i].IdCliente == idCliente)
                {
                    clienteBuscado = listaClientes[i];
                    encontreCliente = true;
                }
                else
                {
                    i++;
                }
            }

            return clienteBuscado;
        }

        public int NroIndiceCliente(string idClienteBuscado)
        {
            int i = 0;
            bool encontroIndice = false;

            while (i < ListaClientes.Count && !encontroIndice)
            {
                if (ListaClientes[i].IdCliente == idClienteBuscado)
                {
                    encontroIndice = true;
                }
                else
                {
                    i++;
                }
            }
            return i;
        }

        public bool BorrarCliente(string idCliente)
        {
            Cliente clienteAux = BuscarCliente(idCliente);

            if (clienteAux != null)
            {
                if (clienteAux.MisAptosComprados.Count > 0)
                {
                    return false;
                }

            }
            ListaClientes.Remove(clienteAux);
            return true;
        }

        public bool ModificarCliente(Cliente clienteModificado)
        {
            if (clienteModificado != null)
            {
                Cliente clienteAmodificar = BuscarCliente(clienteModificado.IdCliente);

                {
                    clienteAmodificar.Nombre = clienteModificado.Nombre;
                    clienteAmodificar.Apellido = clienteModificado.Apellido;
                    clienteAmodificar.Documento = clienteModificado.Documento;
                    clienteAmodificar.Direccion = clienteModificado.Direccion;
                    clienteAmodificar.Telefono = clienteModificado.Telefono;
                }

                int i = NroIndiceCliente(clienteAmodificar.IdCliente);

                ListaClientes[i] = clienteAmodificar;

                return true;
            }
            else
            {
                return false;
            }

        }

        //Devuelve una lista de los clientes que compraron aptos en determinado rango de fechas
        public List<Cliente> ListadoClientesRango(DateTime? fechaInicial, DateTime? fechaFinal)
        {
            List<Cliente> ListaAuxClientesEnRango = new List<Cliente>();

            if (fechaInicial != null || fechaFinal != null)
            {
                DateTime fechaIniAux = (DateTime)fechaInicial;
                DateTime fechaFinAux = (DateTime)fechaFinal;

                for (int i = 0; ListaClientes.Count > i; i++)
                {
                    Cliente cli = ListaClientes[i];
                    for (int j = 0; cli.MisAptosComprados.Count > j; j++)
                    {
                        
                        if (cli.MisAptosComprados[j].rangoCompras(fechaIniAux, fechaFinAux))
                        {
                            if (!ListaAuxClientesEnRango.Contains(cli))
                            {
                                ListaAuxClientesEnRango.Add(cli);
                            }
                        }
                        //else
                        //{
                        //    ListaAuxClientesEnRango = null;
                        //}
                    }
                }
            }

            ListaAuxClientesEnRango.Sort();

            return ListaAuxClientesEnRango;
        }

        #endregion

        #region Metodos edificio

        public bool AgregarEdificio(Edificio unEdificio)
        {
            if (unEdificio == null) return false;
            //validacion que esta en edificio
            if (!unEdificio.ValidarEdificio()) return false;
            //Verifica que no haya dos objetos edificio con el mismo nombre, lo hicimos con el Equals
            if (this.ListaEdificios.Contains(unEdificio)) return false;
            unEdificio.IdEdificio = Utilidades.GeneradorId.ObtenerIdEdificio();
            ListaEdificios.Add(unEdificio);
            return true;
        }

        public Edificio BuscarEdificio(string idEdificio)
        {
            int i = 0;
            Edificio encontreEdificio = null;

            if(idEdificio != null || idEdificio != "")
            {
                while (i < this.ListaEdificios.Count && encontreEdificio == null)
                {
                    if (ListaEdificios[i].IdEdificio == idEdificio)
                    {
                        encontreEdificio = ListaEdificios[i];
                    }
                    else
                    {
                        i++;
                    }
                }
            }
          
            return encontreEdificio;
        }

        //devuelve la lista de apartamentos de un edificio especifico pasado por parametro
        public List<Apartamento> aptosEdificio(string idEdificio)
        {
            List<Apartamento> listaAptos = new List<Apartamento>();

            if (listaAptos.Count >= 0)
            {
                if (idEdificio != string.Empty)
                {
                    Edificio edificioSeleccionado = BuscarEdificio(idEdificio);
                    foreach (Apartamento apto in edificioSeleccionado.MisApartamentos)
                    {
                        listaAptos.Add(apto);
                    }
                }
            }
            return listaAptos;
        }


        #endregion

        #region Metodos vendedor
        public Vendedor BuscarVendedor(string idVendedor)
        {
            int i = 0;
            bool encontreVendedor = false;
            Vendedor vendedorBuscado = null;

            while (i < listaVendedores.Count && !encontreVendedor)
            {
                if (listaVendedores[i].IdVendedor == idVendedor)
                {
                    vendedorBuscado = listaVendedores[i];
                    encontreVendedor = true;
                }
                else
                {
                    i++;
                }
            }

            return vendedorBuscado;
        }


        //Si el usuario logueado esta autorizado en el sistema va a poder acceder a todas las funcionalidades de vendedor
        public Vendedor VendedorAutorizado(string nombre, string clave)
        {
            int i = 0;
            Vendedor unVendedorLogueado = null;

            while (i < listaVendedores.Count && unVendedorLogueado == null)
            {
                if (listaVendedores[i].Nombre == nombre && listaVendedores[i].Clave == clave)
                {
                    unVendedorLogueado = listaVendedores[i];
                }
                else
                {
                    i++;
                }
            }
            return unVendedorLogueado;
        }

        #endregion

        #region Metodos venta

        public CompraVenta BuscarCompraVenta(string idVenta)
        {
            int i = 0;
            CompraVenta encontreCompraVenta = null;


            while (i < this.ListaVentas.Count && encontreCompraVenta == null)
            {
                if (ListaVentas[i].IdCompraVenta == idVenta)
                {
                    encontreCompraVenta = ListaVentas[i];
                }
                else
                {
                    i++;
                }
            }

            return encontreCompraVenta;

        }
        public bool AgregarVenta(CompraVenta unaVenta)
        {

            if (unaVenta.ValidarVenta())
            {
                unaVenta.IdCompraVenta = Utilidades.GeneradorId.ObtenerIdVenta();

                unaVenta.ElVendedor.MisVentas.Add(unaVenta);
                unaVenta.ElComprador.MisAptosComprados.Add(unaVenta);

                unaVenta.ElApartamento.Disponible = false;

                this.ListaVentas.Add(unaVenta);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #endregion

        #region Datos precargados
        public void AgregarDatosPrueba()
        {
            //PRECARGA CLIENTES
            AgregarCliente(new Cliente("Juan", "Perez", "49996663", "Avenida Sin Nombre 123", 099789678));
            AgregarCliente(new Cliente("Ana", "Ramirez", "49996664", "Avenida Sin Nombre 124", 099789672));
            AgregarCliente(new Cliente("Maria", "Fernandez", "49996665", "Avenida Sin Nombre 125", 099789673));
            AgregarCliente(new Cliente("Lucas", "Gimenez", "49996666", "Avenida Sin Nombre 126", 099789674));
            AgregarCliente(new Cliente("Matias", "Romero", "49996667", "Avenida Sin Nombre 127", 099789675));
            AgregarCliente(new Cliente("Luisa", "Santos", "49996668", "Avenida Sin Nombre 128", 099789676));
            AgregarCliente(new Cliente("Camila", "Martinez", "49996669", "Avenida Sin Nombre 129", 099789677));

            //PRECARGA EDIFICIOS
            AgregarEdificio(new Edificio("Isla Esmeralda", "Avenida_1"));
            AgregarEdificio(new Edificio("Art Boulevard", "Avenida_2"));
            AgregarEdificio(new Edificio("Manhattan", "Avenida_3"));
            AgregarEdificio(new Edificio("Coral Tower", "Avenida_4"));

            //PRECARGA APARTAMENTOS
            Vivienda vi1 = new Vivienda(Utilidades.GeneradorId.ObtenerIdApto(), 1, 107, 120, 500, "S", this.listaEdificios[0], 3, 2, false, true);
            Oficina of1 = new Oficina(Utilidades.GeneradorId.ObtenerIdApto(), 2, 208, 50, 500, "NE", this.listaEdificios[0], 10, true, true);
            Vivienda vi2 = new Vivienda(Utilidades.GeneradorId.ObtenerIdApto(), 3, 302, 90, 500, "N", this.listaEdificios[0], 1, 1, true, true);
            Oficina of2 = new Oficina(Utilidades.GeneradorId.ObtenerIdApto(), 4, 480, 23, 500, "SE", this.listaEdificios[0], 6, true, false);
            Vivienda vi3 = new Vivienda(Utilidades.GeneradorId.ObtenerIdApto(), 5, 501, 120, 500, "OE", this.listaEdificios[0], 3, 2, false, false);
            Oficina of3 = new Oficina(Utilidades.GeneradorId.ObtenerIdApto(), 6, 608, 50, 500, "NE", this.listaEdificios[0], 10, true, true);

            Vivienda vi4 = new Vivienda(Utilidades.GeneradorId.ObtenerIdApto(), 2, 202, 90, 500, "NO", this.listaEdificios[1], 1, 1, true, true);
            Oficina of4 = new Oficina(Utilidades.GeneradorId.ObtenerIdApto(), 7, 709, 80, 500, "O", this.listaEdificios[1], 6, true, true);
            Vivienda vi5 = new Vivienda(Utilidades.GeneradorId.ObtenerIdApto(), 9, 902, 90, 500, "E", this.listaEdificios[1], 1, 1, true, false);
            Oficina of5 = new Oficina(Utilidades.GeneradorId.ObtenerIdApto(), 11, 1101, 80, 500, "SO", this.listaEdificios[1], 6, true, false);
            Vivienda vi6 = new Vivienda(Utilidades.GeneradorId.ObtenerIdApto(), 6, 601, 120, 500, "OE", this.listaEdificios[1], 3, 2, false, true);
            Oficina of6 = new Oficina(Utilidades.GeneradorId.ObtenerIdApto(), 4, 408, 50, 500, "NE", this.listaEdificios[1], 10, true, false);

            Vivienda vi7 = new Vivienda(Utilidades.GeneradorId.ObtenerIdApto(), 4, 407, 120, 500, "S", this.listaEdificios[2], 3, 2, false, true);
            Oficina of7 = new Oficina(Utilidades.GeneradorId.ObtenerIdApto(), 4, 408, 50, 500, "NE", this.listaEdificios[2], 10, true, true);
            Vivienda vi8 = new Vivienda(Utilidades.GeneradorId.ObtenerIdApto(), 2, 202, 90, 500, "N", this.listaEdificios[2], 1, 1, true, true);
            Oficina of8 = new Oficina(Utilidades.GeneradorId.ObtenerIdApto(), 3, 303, 80, 500, "SE", this.listaEdificios[2], 6, true, true);
            Vivienda vi9 = new Vivienda(Utilidades.GeneradorId.ObtenerIdApto(), 6, 601, 120, 500, "OE", this.listaEdificios[2], 3, 2, false, true);
            Oficina of9 = new Oficina(Utilidades.GeneradorId.ObtenerIdApto(), 5, 508, 50, 500, "NE", this.listaEdificios[2], 10, true, true);


            Vivienda vi10 = new Vivienda(Utilidades.GeneradorId.ObtenerIdApto(), 4, 407, 120, 500, "S", this.listaEdificios[3], 3, 2, false, false);
            Oficina of10 = new Oficina(Utilidades.GeneradorId.ObtenerIdApto(), 4, 408, 50, 500, "NE", this.listaEdificios[3], 10, true, true);
            Vivienda vi11 = new Vivienda(Utilidades.GeneradorId.ObtenerIdApto(), 2, 202, 90, 500, "N", this.listaEdificios[3], 1, 1, true, true);
            Oficina of11 = new Oficina(Utilidades.GeneradorId.ObtenerIdApto(), 3, 323, 80, 500, "SE", this.listaEdificios[3], 6, true, true);
            Vivienda vi12 = new Vivienda(Utilidades.GeneradorId.ObtenerIdApto(), 6, 601, 120, 500, "OE", this.listaEdificios[3], 3, 2, false, true);
            Oficina of12 = new Oficina(Utilidades.GeneradorId.ObtenerIdApto(), 6, 608, 50, 500, "NE", this.listaEdificios[3], 10, true, false);


            this.listaEdificios[0].AgregarApartamentoEnEdificio(vi1);
            this.listaEdificios[0].AgregarApartamentoEnEdificio(of1);
            this.listaEdificios[0].AgregarApartamentoEnEdificio(vi2);
            this.listaEdificios[0].AgregarApartamentoEnEdificio(of2);
            this.listaEdificios[0].AgregarApartamentoEnEdificio(vi3);
            this.listaEdificios[0].AgregarApartamentoEnEdificio(of3);

            this.listaEdificios[1].AgregarApartamentoEnEdificio(vi4);
            this.listaEdificios[1].AgregarApartamentoEnEdificio(of4);
            this.listaEdificios[1].AgregarApartamentoEnEdificio(vi5);
            this.listaEdificios[1].AgregarApartamentoEnEdificio(of5);
            this.listaEdificios[1].AgregarApartamentoEnEdificio(vi6);
            this.listaEdificios[1].AgregarApartamentoEnEdificio(of6);

            this.listaEdificios[2].AgregarApartamentoEnEdificio(vi7);
            this.listaEdificios[2].AgregarApartamentoEnEdificio(of7);
            this.listaEdificios[2].AgregarApartamentoEnEdificio(vi8);
            this.listaEdificios[2].AgregarApartamentoEnEdificio(of8);
            this.listaEdificios[2].AgregarApartamentoEnEdificio(vi9);
            this.listaEdificios[2].AgregarApartamentoEnEdificio(of9);

            this.listaEdificios[3].AgregarApartamentoEnEdificio(vi10);
            this.listaEdificios[3].AgregarApartamentoEnEdificio(of10);
            this.listaEdificios[3].AgregarApartamentoEnEdificio(vi11);
            this.listaEdificios[3].AgregarApartamentoEnEdificio(of11);
            this.listaEdificios[3].AgregarApartamentoEnEdificio(vi12);
            this.listaEdificios[3].AgregarApartamentoEnEdificio(of12);

            //PRECARGA VENDEDORES
            Vendedor vend1 = new Vendedor(Utilidades.GeneradorId.ObtenerIdVendedor(), "vend1", "vend1111");
            Vendedor vend2 = new Vendedor(Utilidades.GeneradorId.ObtenerIdVendedor(), "vend2", "vend2222");
            ListaVendedores.Add(vend1);
            ListaVendedores.Add(vend2);

            //PRECARGA COMISIONES
            Comision comision1Vend1 = new Comision(this.listaEdificios[0], 20);
            Comision comision2Vend1 = new Comision(this.listaEdificios[1], 10);
            Comision comision3Vend1 = new Comision(this.listaEdificios[2], 15);
            Comision comision4Vend1 = new Comision(this.listaEdificios[3], 5);

            vend1.agregarComision(comision1Vend1);
            vend1.agregarComision(comision2Vend1);
            vend1.agregarComision(comision3Vend1);
            vend1.agregarComision(comision4Vend1);


            Comision comision1Vend2 = new Comision(this.listaEdificios[0], 5);
            Comision comision2Vend2 = new Comision(this.listaEdificios[1], 15);
            Comision comision3Vend2 = new Comision(this.listaEdificios[2], 20);
            Comision comision4Vend2 = new Comision(this.listaEdificios[3], 10);

            vend2.agregarComision(comision1Vend2);
            vend2.agregarComision(comision2Vend2);
            vend2.agregarComision(comision3Vend2);
            vend2.agregarComision(comision4Vend2);

            //PRECARGA VENTAS
            AgregarVenta(new CompraVenta(new DateTime(2017, 11, 08), this.ListaEdificios[0].MisApartamentos[0], this.ListaVendedores[0], this.ListaClientes[0], this.ListaEdificios[0].MisApartamentos[0].calcularPrecio()));
            AgregarVenta(new CompraVenta(new DateTime(2018, 05, 09), this.ListaEdificios[0].MisApartamentos[4], this.ListaVendedores[1], this.ListaClientes[1], this.ListaEdificios[0].MisApartamentos[4].calcularPrecio()));
            AgregarVenta(new CompraVenta(new DateTime(2016, 06, 12), this.ListaEdificios[0].MisApartamentos[2], this.ListaVendedores[0], this.ListaClientes[2], this.ListaEdificios[0].MisApartamentos[2].calcularPrecio()));

            AgregarVenta(new CompraVenta(new DateTime(2015, 03, 14), this.ListaEdificios[1].MisApartamentos[3], this.ListaVendedores[1], this.ListaClientes[4], this.ListaEdificios[1].MisApartamentos[3].calcularPrecio()));
            AgregarVenta(new CompraVenta(new DateTime(2015, 12, 21), this.ListaEdificios[1].MisApartamentos[4], this.ListaVendedores[0], this.ListaClientes[4], this.ListaEdificios[1].MisApartamentos[4].calcularPrecio()));
            AgregarVenta(new CompraVenta(new DateTime(2017, 10, 24), this.ListaEdificios[1].MisApartamentos[5], this.ListaVendedores[1], this.ListaClientes[5], this.ListaEdificios[1].MisApartamentos[5].calcularPrecio()));

            AgregarVenta(new CompraVenta(new DateTime(2018, 09, 12), this.ListaEdificios[2].MisApartamentos[0], this.ListaVendedores[0], this.ListaClientes[0], this.ListaEdificios[2].MisApartamentos[0].calcularPrecio()));
            AgregarVenta(new CompraVenta(new DateTime(2018, 09, 03), this.ListaEdificios[2].MisApartamentos[1], this.ListaVendedores[1], this.ListaClientes[1], this.ListaEdificios[2].MisApartamentos[1].calcularPrecio()));
            AgregarVenta(new CompraVenta(new DateTime(2014, 01, 15), this.ListaEdificios[2].MisApartamentos[2], this.ListaVendedores[0], this.ListaClientes[2], this.ListaEdificios[2].MisApartamentos[2].calcularPrecio()));

            AgregarVenta(new CompraVenta(new DateTime(2015, 02, 14), this.ListaEdificios[3].MisApartamentos[3], this.ListaVendedores[1], this.ListaClientes[3], this.ListaEdificios[3].MisApartamentos[3].calcularPrecio()));
            AgregarVenta(new CompraVenta(new DateTime(2016, 02, 22), this.ListaEdificios[3].MisApartamentos[4], this.ListaVendedores[0], this.ListaClientes[4], this.ListaEdificios[3].MisApartamentos[4].calcularPrecio()));
            AgregarVenta(new CompraVenta(new DateTime(2018, 11, 10), this.ListaEdificios[3].MisApartamentos[5], this.ListaVendedores[1], this.ListaClientes[5], this.ListaEdificios[3].MisApartamentos[5].calcularPrecio()));

           //Cliente 6 no compro ningun apto






        }

        #endregion

        #region Validaciones de campos
        //Si el campo no quede vacio
        public bool campoVacio(string campoEvaluar)
        {
            if (campoEvaluar != "" || campoEvaluar != string.Empty)
            {
                return false;
            }
            return true;
        }

        //Si el campo es numerico o no
        public bool esNumerico(string campoEvaluar)
        {
            try
            {
                Convert.ToInt32(campoEvaluar);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool esNumericoLong(string campoEvaluar)
        {
            try
            {
                Convert.ToInt64(campoEvaluar);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}

