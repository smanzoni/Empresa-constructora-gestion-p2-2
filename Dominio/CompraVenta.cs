using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class CompraVenta
    {
        #region Atributos
        private string idCompraVenta;
        private DateTime fechaCompra;
        private Apartamento elApartamento;
        private Vendedor elVendedor;
        private Cliente elComprador;
        private decimal precio;
        #endregion

        #region Accesores
        public string IdCompraVenta
        {
            get
            {
                return idCompraVenta;
            }

            set
            {
                idCompraVenta = value;
            }
        }

        public DateTime FechaCompra
        {
            get
            {
                return fechaCompra;
            }

            set
            {
                fechaCompra = value;
            }
        }

        public Apartamento ElApartamento
        {
            get
            {
                return elApartamento;
            }

            set
            {
                elApartamento = value;
            }
        }

        public Vendedor ElVendedor
        {
            get
            {
                return elVendedor;
            }

            set
            {
                elVendedor = value;
            }
        }

        public Cliente ElComprador
        {
            get
            {
                return elComprador;
            }

            set
            {
                elComprador = value;
            }
        }

        public decimal Precio
        {
            get
            {
                return precio;
            }

            set
            {
                precio = value;
            }
        }

        #endregion

        #region Constructor
        public CompraVenta(DateTime fechaCompra, Apartamento elApartamento, Vendedor elVendedor, Cliente elComprador, decimal precio)
        {
            this.FechaCompra = fechaCompra;
            this.ElApartamento = elApartamento;
            this.ElVendedor = elVendedor;
            this.ElComprador = elComprador;
            this.Precio = precio;
        }

        public CompraVenta()
        {

        }
        #endregion

        #region Metodos

        //Devuelve el valor total de la venta, sumando el valor del apartamento mas la comision del vendedor
        public decimal CalcularPrecio(string idEdificio)
        {
            decimal porcentajeComision = 0;

            if (idEdificio != null)
            {
                int i = 0;
                bool encontreComision = false;
                while (i < elVendedor.MisComisiones.Count && !encontreComision)
                {
                    if (idEdificio == this.elVendedor.MisComisiones[i].UnEdificio.IdEdificio)
                    {
                        porcentajeComision = elVendedor.MisComisiones[i].Porcentaje;
                        encontreComision = true;
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            if (this.ElApartamento != null)
            {
                decimal precioApto = this.ElApartamento.calcularPrecio();
                decimal precioTotal = precioApto + (precioApto * porcentajeComision) / 100;
                return precioTotal;
            }
            else
            {
                return porcentajeComision;
            }

        }

        //Devuelve el porcentaje de la comision de un vendedor en un edificio especifico
        public decimal valorComision(Apartamento elApto, string idVendedor)
        {
            bool encontreComision = false;
            decimal laComision = 0;
            int i = 0;
            while (this.ElVendedor.MisComisiones.Count > i && encontreComision == false)
            {
                if (this.ElVendedor.MisComisiones[i].UnEdificio.IdEdificio == elApto.MiEdificio.IdEdificio)
                {

                    laComision = this.ElVendedor.MisComisiones[i].Porcentaje;
                    encontreComision = true;

                }
                else
                {
                    i++;
                }
            }

            return laComision;
        }

        //Devuelve true o false, dependiendo si la compra fue realizada en un rango de fechas seleccionado por el usuario
        public bool rangoCompras(DateTime fechaInicial, DateTime fechaFinal)
        {
            return this.fechaCompra.Date >= fechaInicial.Date && this.fechaCompra.Date <= fechaFinal.Date;
        }

        public bool ValidarVenta()
        {
            return this.fechaCompra != null
                && this.elApartamento != null
                && this.elVendedor != null
                && this.elComprador != null
                && this.precio >= 0;

        }


        #endregion

        #region Metodos object
        public override string ToString()
        {
            return "Fecha compra: " + this.fechaCompra
                + ", Edificio: " + this.elApartamento.MiEdificio.Nombre
                + ", Numero Apartamento: " + this.elApartamento.Numero
                + ", Comprador: " + this.elComprador.Nombre
                + ", Vendedor: " + this.elVendedor.Nombre
                + ", Precio: $" + this.precio;

        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            CompraVenta unaCompra = obj as CompraVenta;

            if (unaCompra == null) return false;
            return this.ElApartamento == unaCompra.ElApartamento;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
