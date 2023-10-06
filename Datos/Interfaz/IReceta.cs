using RecetasULTIMO00.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasULTIMO00.Datos.Interfaz
{
    internal interface IReceta
    {
        DataTable ConsultarDB();
        bool EjecutarInsert(Receta oReceta);

        bool EjucutarUpdate(Receta oReceta);

        //bool Borrar(int id);

        List<Receta> ObtenerRecetaPorFiltro(int tipoReceta, string Nombre);


    }
}
