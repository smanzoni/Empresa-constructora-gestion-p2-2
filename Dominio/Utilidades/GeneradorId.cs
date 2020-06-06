using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Utilidades
{
    internal class GeneradorId
    {
        private static int ultimoNroApto = 10;

        private static int ultimoNroEdificio = 5;

        private static int ultimoNroVendedor = 1000;

        private static int ultimoNroCliente = 1000;

        private static int ultimoNroVenta = 1000;


        internal static string ObtenerIdApto()
        {
            return "APTO" + (++ultimoNroApto).ToString();
        }

        internal static string ObtenerIdEdificio()
        {
            return "EDI" + (++ultimoNroEdificio).ToString();
        }

        internal static string ObtenerIdVendedor()
        {
            return "VEND" + (++ultimoNroVendedor).ToString();
        }

        internal static string ObtenerIdCliente()
        {
          return "CLI" +  (++ultimoNroCliente).ToString();
        }

        internal static string ObtenerIdVenta()
        {
            return "VTA" + (++ultimoNroVenta).ToString();
        }
    }
}
