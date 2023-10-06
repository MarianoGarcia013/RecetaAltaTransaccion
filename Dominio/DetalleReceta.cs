using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasULTIMO00.Dominio
{
    internal class DetalleReceta
    {
        public Ingrediente ingrediente { get; set; }
        public decimal cantidad { get; set; }

        public DetalleReceta(Ingrediente ingrediente, decimal cantidad)
        {
            this.ingrediente = ingrediente;
            this.cantidad = cantidad;
        }
    }
}
