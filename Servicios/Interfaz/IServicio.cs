using RecetasULTIMO00.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasULTIMO00.Servicios.Interfaz
{
    internal interface IServicio
    {
        DataTable ConsultarDB();
        bool EjecutarInsert(Receta oReceta);

        bool EjecutarUpdate(Receta oReceta);

        List<Receta> ObtenerRecetaPorFiltro(int tipoReceta, string Nombre);
    }
}
