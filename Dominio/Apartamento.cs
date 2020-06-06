using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Apartamento
    {

        #region Atributos
        private string idApto;
        private int piso;
        private int numero;
        private int metrajeTotal;
        private decimal precioBaseXm2;
        private string orientacion;
        //Asosiacion: Apartamento conoce a edificio
        //No se crea como lista ya que le pertenece un solo edificio, 
        //con esto le estamos permitiendo conocer a la clase
        private Edificio miEdificio;
        private bool disponible;
        #endregion

        #region Accesores

        public string IdApto
        {
            get
            {
                return idApto;
            }

            set
            {
                idApto = value;
            }
        }

        public int Piso
        {
            get
            {
                return piso;
            }

            set
            {
                piso = value;
            }
        }

        public int Numero
        {
            get
            {
                return numero;
            }

            set
            {
                numero = value;
            }
        }

        public int MetrajeTotal
        {
            get
            {
                return metrajeTotal;
            }

            set
            {
                metrajeTotal = value;
            }
        }

        public decimal PrecioBaseXm2
        {
            get
            {
                return precioBaseXm2;
            }

            set
            {
                precioBaseXm2 = value;
            }
        }

        public string Orientacion
        {
            get
            {
                return orientacion;
            }

            set
            {
                orientacion = value;
            }
        }

        public Edificio MiEdificio
        {
            get
            {
                return miEdificio;
            }
            set
            {
                miEdificio = value;
            }
        }

        public bool Disponible
        {
            get
            {
                return disponible;
            }

            set
            {
                disponible = value;
            }
        }

        #endregion

        #region Constructor
        //Le ponemos al edificio como parte del constructor ya que la letra dice que para crear el objeto apartamento se debe
        //seleccionar un edificio
        public Apartamento(string idApto, int piso, int numero, int metrajeTotal, decimal precioBaseXm2, string orientacion, Edificio unEdificio, bool disponible)
        {
            this.IdApto = idApto;
            this.Piso = piso;
            this.Numero = numero;
            this.MetrajeTotal = metrajeTotal;
            this.PrecioBaseXm2 = precioBaseXm2;
            this.Orientacion = orientacion;
            this.MiEdificio = unEdificio;
            this.Disponible = disponible;
        }

        public Apartamento()
        {

        }
        #endregion

        #region Validaciones
        //Valido que los datos ingresados sean correctos
        public bool ValidarApartamento()
        {
            return this.piso > 0
                && this.numero > 0
                && this.metrajeTotal > 0
                && this.precioBaseXm2 > 0
                && this.orientacion != null;
        }

        #endregion

        #region Métodos de object redefinidos  
        //Con este metodo voy a visualizar el Objeto Apartamento con todos los datos que le pida
        //El metodo getType me devuelve el tipo de apartamento (vivienda, oficina)
        public override string ToString()
        {
            return "Tipo: " + this.GetType().Name + ", Piso: " + this.Piso + ", Numero: "
                 + this.Numero + ", Metraje Total: " + this.MetrajeTotal
                 + ", Precio xM2: " + this.PrecioBaseXm2 + ", Orientacion: "
                 + this.Orientacion;
        }

        public override bool Equals(object obj)
        {
            Apartamento elApto = obj as Apartamento;
            if (elApto == null) return false;
            if (elApto.Piso == this.Piso && elApto.Numero == this.Numero)
            {
                return true;
            }
            else if (elApto.Piso == this.Piso && elApto.Orientacion == this.Orientacion)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region Metodo especial de la clase
        //metodo abstracto para calcular precio
        //al ser clase abstract y metodo abstract, hay que redefinirlo especificamente en sus clases derivadas
        //OVERRIDE en oficina y vivienda

        public abstract decimal calcularPrecio();
        #endregion


    }
}
