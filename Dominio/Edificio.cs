using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Edificio
    {

        #region Atributos
        private string idEdificio;
        private string nombre;
        private string direccion;
        private List<Apartamento> misApartamentos = new List<Apartamento>();
        #endregion

        #region Accesores
        public string IdEdificio
        {
            get
            {
                return idEdificio;
            }

            set
            {
                idEdificio = value;
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
        public List<Apartamento> MisApartamentos
        {
            get
            {
                return misApartamentos;
            }
            set
            {
                misApartamentos = value;
            }
        }


        #endregion

        #region Constructor
        public Edificio(string nombre, string direccion)
        {
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.MisApartamentos = new List<Apartamento>();
        }

        public Edificio()
        {

        }

        #endregion

        #region Validaciones
        //Validaciones de Edificio

        public bool ValidarEdificio()
        {
            //mientras que el nombre ingresado y la cedula no esten vacios, retorna true
            return this.nombre != string.Empty
                && this.direccion != string.Empty;
        }
        #endregion

        #region Métodos de object redefinidos    

        // Verificamos que no exista otro Edificio con ese nombre a traves de Equals
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Edificio elEdificio = obj as Edificio;
            if (elEdificio == null) return false;
            return this.Nombre == elEdificio.Nombre;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        //Asi vamos a visualizar los edificios cuando se desplieguen en la lista
        public override string ToString()
        {
            return "Nombre: " + this.Nombre + ", Dirección: " + this.Direccion;
        }


        #endregion

        #region Metodos

        public bool AgregarApartamentoEnEdificio(Apartamento unApartamento)
        {
            if (unApartamento.ValidarApartamento() && !this.MisApartamentos.Contains(unApartamento))
            {
                unApartamento.IdApto = Utilidades.GeneradorId.ObtenerIdApto();
                unApartamento.MiEdificio.MisApartamentos.Add(unApartamento);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Apartamento BuscarApartamento(int numero, string orientacion, int piso)
        {
            Apartamento encontreApartamento = null;

            int j = 0;
            while (j < this.MisApartamentos.Count && encontreApartamento == null)
            {
                if (this.MisApartamentos[j].Piso == piso)
                {
                    if (this.MisApartamentos[j].Numero == numero)
                    {
                        if (this.MisApartamentos[j].Orientacion == orientacion)
                        {
                            encontreApartamento = this.MisApartamentos[j];
                        }
                        else
                        {
                            j++;
                        }
                    }
                }
            }
            return encontreApartamento;
        }

        //Buscador de apartamento por id, implemento del nuevo sistema
        public Apartamento BuscarAptoXId(string id)
        {
            int i = 0;
            bool existe = false;
            Apartamento unApto = null;

            if (id != string.Empty || id != null)
            {
                while (i < this.MisApartamentos.Count && !existe)
                {
                    if (this.MisApartamentos[i].IdApto == id)
                    {
                        existe = true;
                        unApto = this.MisApartamentos[i];
                    }
                    else
                    {
                        i++;
                    }

                }
            }

            return unApto;
        }

        
        //Una vez que el apto fue vendido, se le da de baja del sistema
        public bool darDeBajaApto(int? indiceApto)
        {
            if (indiceApto != null)
            {
                this.MisApartamentos[(int)indiceApto].Disponible = false;

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
