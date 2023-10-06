using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasULTIMO00.Dominio
{
    internal class Receta
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string cheff { get; set; }
        public int tipoReceta { get; set; }

        public List<DetalleReceta> ListDetalles;

        public Receta()
        {
            ListDetalles = new List<DetalleReceta>();
        }

        public void agregarComida(DetalleReceta comida)
        {
            ListDetalles.Add(comida);
        }

        public void quitarDetalle(int indice)
        {
            ListDetalles.RemoveAt(indice);
        }
    }
}
