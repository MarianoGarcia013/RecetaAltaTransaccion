using RecetasULTIMO00.Datos.Implementaciones;
using RecetasULTIMO00.Dominio;
using RecetasULTIMO00.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasULTIMO00.Servicios.Implementaciones
{
    internal class Servicio : IServicio
    {
        RecetaDAO DAO;

        public Servicio()
        {
            DAO = new RecetaDAO();
        }
        public DataTable ConsultarDB()
        {
            return DAO.ConsultarDB();
        }

        public bool EjecutarInsert(Receta oReceta)
        {
            return DAO.EjecutarInsert(oReceta);
        }

       public bool EjecutarUpdate(Receta oReceta)
        {
            return DAO.EjucutarUpdate(oReceta);
        }

        public List<Receta> ObtenerRecetaPorFiltro(int tipoReceta, string Nombre)
        {
            return DAO.ObtenerRecetaPorFiltro(tipoReceta, Nombre);
        }
    }
}
