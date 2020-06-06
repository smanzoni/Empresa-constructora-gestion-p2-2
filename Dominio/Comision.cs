using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Comision
    {
        #region Atributos
        private Edificio unEdificio;
        private decimal porcentaje;

        #endregion

        #region Accesores
        public Edificio UnEdificio
        {
            get
            {
                return unEdificio;
            }

            set
            {
                unEdificio = value;
            }
        }

        public decimal Porcentaje
        {
            get
            {
                return porcentaje;
            }

            set
            {
                porcentaje = value;
            }
        }
        #endregion

        #region Constructor
        public Comision(Edificio unEdificio, decimal porcentaje)
        {
            this.UnEdificio = unEdificio;
            this.Porcentaje = porcentaje;
        }

        public Comision()
        {

        }
        #endregion

        #region Metodos object
        public override string ToString()
        {
            return "Edificio: " + this.UnEdificio.Nombre +
                ", Porcentaje: %" + this.porcentaje;

        }

        #endregion
    }
}
