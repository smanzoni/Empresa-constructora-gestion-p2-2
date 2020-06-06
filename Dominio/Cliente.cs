using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cliente:IComparable<Cliente>
    {
        #region Atributos
        private string idCliente;
        private string nombre;
        private string apellido;
        private string documento;
        private string direccion;
        private long telefono;
        private List<CompraVenta> misAptosComprados = new List<CompraVenta>();
        #endregion

        #region Accesores
        public string IdCliente
        {
            get
            {
                return idCliente;
            }

            set
            {
                idCliente = value;
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

        public string Apellido
        {
            get
            {
                return apellido;
            }

            set
            {
                apellido = value;
            }
        }

        public string Documento
        {
            get
            {
                return documento;
            }

            set
            {
                documento = value;
            }
        }

        public string Direccion
        {
            get
            {
                return direccion;
            }

            set
            {
                direccion = value;
            }
        }

        public long Telefono
        {
            get
            {
                return telefono;
            }

            set
            {
                telefono = value;
            }
        }

        public List<CompraVenta> MisAptosComprados
        {
            get
            {
                return misAptosComprados;
            }

            private set
            {
                misAptosComprados = value;
            }
        }


        #endregion

        #region Constructor

        public Cliente(string nombre, string apellido, string documento, string direccion, long telefono)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Documento = documento;
            this.Direccion = direccion;
            this.Telefono = telefono;
        }
        public Cliente()
        {

        }
        #endregion

        #region Validaciones
        public bool ValidarCliente()
        {
            return this.nombre != string.Empty
                && this.apellido != string.Empty
                && this.documento.Length >= 8
                && this.direccion != string.Empty
                && this.telefono >= 000000001L & this.telefono <= 999999999L;
        }
        #endregion
        
        #region Metodos Object redefinidos
        public override string ToString()
        {
            string estado = "";
            estado = "Nombre: " + this.nombre + Environment.NewLine;
            estado += "Apellido: " + this.apellido + Environment.NewLine;
            estado += "Documento: " + this.documento + Environment.NewLine;
            estado += "Dirección: " + this.direccion + Environment.NewLine;
            estado += "Teléfono: " + this.telefono + Environment.NewLine;
            return estado;
        }

        public override bool Equals(object obj)
        {
            Cliente unCliente = obj as Cliente;
            //Validamos que no se repita el documento
            return this.Documento == unCliente.Documento;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region Metodo IComparer

        public int CompareTo(Cliente other)
        {
            return this.Nombre.CompareTo(other.Nombre);
        }
        #endregion
    }
}
