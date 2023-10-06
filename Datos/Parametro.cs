using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasULTIMO00.Datos
{
    public class Parametro
    {
        public string Clave { get; set; } // clave: paramentro de entrada o salida
        public object Valor { get; set; } // el que se ingrese como parametro (del tipo de dato que se indique)

        public Parametro(string clave, object valor)
        {
            Clave = clave;
            Valor = valor;
        }
    }
}
