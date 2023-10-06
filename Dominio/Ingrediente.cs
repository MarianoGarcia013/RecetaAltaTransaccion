using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasULTIMO00.Dominio
{
    internal class Ingrediente
    {       
        public int idIngrediente { get; set; }
        public string nombre { get; set; }


        public Ingrediente(int idIngrediente, string nombre)
        {
            this.idIngrediente = idIngrediente;
            this.nombre = nombre;
        }
    }
}
