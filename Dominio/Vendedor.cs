using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Vendedor
    {
        #region Atributos
        private string idVendedor;
        private string nombre;
        private string clave;
        private List<CompraVenta> misVentas = new List<CompraVenta>();
        private List<Comision> misComisiones = new List<Comision>();



        #endregion

        #region Accesores
        public string IdVendedor
        {
            get
            {
                return idVendedor;
            }

            set
            {
                idVendedor = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }
        public string Clave
        {
            get
            {
                return clave;
            }

            set
            {
                clave = value;
            }
        }
        public List<CompraVenta> MisVentas
        {
            get
            {
                return misVentas;
            }
            private set
            {
                misVentas = value;
            }
        }
        public List<Comision> MisComisiones
        {
            get
            {
                return misComisiones;
            }
            private set
            {
                misComisiones = value;
            }
        }


        #endregion

        #region Constructor
        public Vendedor(string idVendedor, string nombre, string clave)
        {
            this.IdVendedor = idVendedor;
            this.Nombre = nombre;
            this.Clave = clave;
        }
        public Vendedor()
        {

        }
        #endregion

        #region Metodos

        //Cada Vendedor tiene una comision asignada por edificio
        public bool agregarComision(Comision unaComision)
        {
            if (unaComision != null)
            {
                this.MisComisiones.Add(unaComision);
                return true;
            }
            else
            {
                return false;
            }
        }

   
        #endregion

        #region Metodos object
        public override string ToString()
        {
            return "Nombre: " + this.nombre;

        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Vendedor unVendedor = obj as Vendedor;
            if (unVendedor == null) return false;
            return this.Nombre == unVendedor.Nombre;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

    }
}
