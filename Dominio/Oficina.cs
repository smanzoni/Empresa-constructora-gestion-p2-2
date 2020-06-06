using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Oficina : Apartamento
    {

        #region Atributos
        private int puestosTrabajo;
        private bool equipamiento;
        #endregion

        #region Accesores
        public int PuestosTrabajo
        {
            get
            {
                return puestosTrabajo;
            }

            set
            {
                puestosTrabajo = value;
            }
        }
        public bool Equipamiento
        {
            get
            {
                return equipamiento;
            }

            set
            {
                equipamiento = value;
            }
        }


        #endregion

        #region Constructor
        public Oficina(string idApto, int piso, int numero, int metrajeTotal, decimal precioBaseXm2, string orientacion, Edificio unEdificio, int puestosTrabajo, bool equipamiento, bool disponible)
            : base(idApto, piso, numero, metrajeTotal, precioBaseXm2, orientacion, unEdificio, disponible)
        {
            //Con base ya ejecutamos el constructor de la base, solo queda
            //inicializar lo específico de la cuenta corriente:
            this.PuestosTrabajo = puestosTrabajo;
            this.Equipamiento = equipamiento;
        }

        public Oficina()
        {

        }
        #endregion

        #region Validaciones
        public bool ValidarOficina()
        {
            //trae la validacion de su padre, si validar apartamento le dio true 
            //y el puestosTrabajo es distinto de 0, permite crear la oficina
            return base.ValidarApartamento() && this.puestosTrabajo > 0;
        }
        #endregion

        #region Metodos
        public override decimal calcularPrecio()
        {
            //calcular monto base
            decimal precioTotal = this.PrecioBaseXm2 * this.MetrajeTotal; ;

            //monto fijo x cantidad de oficinas que pueda ocupar en la vivienda
            decimal montoFijoXoficina = 2000;

            precioTotal += (puestosTrabajo * montoFijoXoficina);

            //calculamos porcentaje extra por si la of va equipada o no
            if (this.Equipamiento)
            {
                precioTotal = (10 * precioTotal) / 100;
            }

            return precioTotal;
        }
        #endregion
    }
}
