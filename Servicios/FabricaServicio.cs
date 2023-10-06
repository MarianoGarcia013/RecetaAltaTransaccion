using RecetasULTIMO00.Servicios.Implementaciones;
using RecetasULTIMO00.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasULTIMO00.Servicios
{
    internal abstract class FabricaServicio
    {
        public abstract IServicio ObtenerServicio();
    }
}
