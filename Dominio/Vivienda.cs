using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Vivienda : Apartamento
    {

        #region Atributos
        private int cantDormitorios;
        private int cantBanio;
        private bool garage;

        #endregion

        #region Accesores
        public int CantDormitorios
        {
            get
            {
                return cantDormitorios;
            }

            set
            {
                cantDormitorios = value;
            }
        }
        public int CantBanio
        {
            get
            {
                return cantBanio;
            }

            set
            {
                cantBanio = value;
            }
        }
        public bool Garage
        {
            get
            {
                return garage;
            }

            set
            {
                garage = value;
            }
        }
        #endregion

        #region Constructor
        public Vivienda(string idApto, int piso, int numero, int metrajeTotal, decimal precioBaseXm2, string orientacion, Edificio unEdificio, int cantDormitorios, int cantidadBanio, bool garage, bool disponible)
            : base(idApto, piso, numero, metrajeTotal, precioBaseXm2, orientacion, unEdificio, disponible)
        {
            this.CantDormitorios = cantDormitorios;
            this.CantBanio = cantidadBanio;
            this.Garage = garage;
        }

        public Vivienda()
        {

        }
        #endregion

        #region Validaciones
        public bool ValidarVivienda()
        {
            return base.ValidarApartamento() && this.cantDormitorios > 0 && this.cantBanio > 0;
        }
        #endregion

        #region Metodos
        public override decimal calcularPrecio()
        {
            //calculamos el precio total tomando como datos el precioBaseM2 y su metraje total
            // esto sucede asi xq se supone que primero declaramos el objeto y le asignamos sus atributos al instanciarlo
            // luego, recien despues de declarado e instanciado el objeto, podemos pedirle que utilice su funcion calcularPrecio()
            // al ya tener los atributos con valores asignados, usamos esos valores del mismo objeto diciendole this.Atributo

            decimal precioTotal = this.PrecioBaseXm2 * this.MetrajeTotal;

            //evaluamos los porcentajes por cantidad de dormitorios
            if (this.CantDormitorios < 3)
            {
                precioTotal += (5 * precioTotal) / 100;
            }
            else if (this.CantDormitorios > 2 && this.CantDormitorios < 5)
            {
                precioTotal += (10 * precioTotal) / 100;
            }
            else if (this.CantDormitorios > 4)
            {
                precioTotal += (20 * precioTotal) / 100;
            }

            //evaluamos si se le agrega el porcentaje extra por tener garage
            if (this.Garage)
            {
                decimal montoFijoComunXgarage = 5000;
                precioTotal += montoFijoComunXgarage;
            }

            //evaluamos si se le agrega el extra por orientacion privilegiada
            if (this.Orientacion.ToUpper() == "N" || this.Orientacion.ToUpper() == "NE" || this.Orientacion.ToUpper() == "NO")
            {
                precioTotal = precioTotal = (15 * precioTotal) / 100;
            }

            return precioTotal;
        }
        #endregion
    }
}
