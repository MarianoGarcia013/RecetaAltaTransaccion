using RecetasULTIMO00.Datos.Interfaz;
using RecetasULTIMO00.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasULTIMO00.Datos.Implementaciones
{
    internal class RecetaDAO : IReceta
    {
        public DataTable ConsultarDB()
        {
            return Helper.ObtenerInstancia().ConsultarDB("[dbo].[SP_CONSULTAR_INGREDIENTES]");
        }

        public bool EjecutarInsert(Receta oReceta)
        {
            return Helper.ObtenerInstancia().ejecutarInsert(oReceta);
        }

        public bool EjucutarUpdate(Receta oReceta)
        {
            return Helper.ObtenerInstancia().EjecutarUpdate(oReceta);
        }

        public List<Receta> ObtenerRecetaPorFiltro(int tipoReceta, string Nombre)
        {
            return Helper.ObtenerInstancia().ObtenerRecetaPorFilitros(tipoReceta, Nombre);
        }
    }
}
