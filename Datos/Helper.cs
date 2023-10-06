using RecetasULTIMO00.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecetasULTIMO00.Datos.Interfaz;
using RecetasULTIMO00.Datos.Implementaciones;
using System.Runtime.InteropServices;

namespace RecetasULTIMO00.Datos
{
    internal class Helper
    {

        private SqlConnection conexion;
        private static Helper instacia;

        private Helper()
        {
            conexion = new SqlConnection($"Data Source=DESKTOP-35HLPC7\\SQLEXPRESS;Initial Catalog=recetas_db;Integrated Security=True");

        }

        public static Helper ObtenerInstancia()
        {
            if (instacia == null)
                instacia = new Helper();
            return instacia;
        }

        public DataTable ListarIngredientes()
        {
            conexion.Open();
            DataTable resultado = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;

            cmd.CommandText = "[dbo].[SP_CONSULTAR_INGREDIENTES]";
            cmd.CommandType = CommandType.StoredProcedure;

            resultado.Load(cmd.ExecuteReader());
            conexion.Close();

            return resultado;
        }

        public DataTable ConsultarDB(string NombreSP)
        {
            DataTable tabla = new DataTable();
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = NombreSP;

            tabla.Load(comando.ExecuteReader());
            conexion.Close();
            return tabla;
        }

        public bool ejecutarInsert(Receta oReceta)
        {
            bool ok = true;
            SqlTransaction TRAN = null;
            SqlCommand cmd = new SqlCommand();

            try
            {
                conexion.Open();
                TRAN = conexion.BeginTransaction();
                cmd.Connection = conexion;
                cmd.Transaction = TRAN;
                cmd.CommandText = "[dbo].[SP_INSERTAR_RECETA]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("tipo_receta", oReceta.tipoReceta);
                cmd.Parameters.AddWithValue("nombre", oReceta.nombre);
                cmd.Parameters.AddWithValue("cheff", oReceta.cheff);

                SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = "@id"; //Porque va ID?
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pOut);
                cmd.ExecuteNonQuery();

                int recetaNro = (int)pOut.Value; //Se castea en int xq es el id
                SqlCommand cmdDetalle;
                int detalleNro = 1;

                cmdDetalle = new SqlCommand("[dbo].[SP_INSERTAR_DETALLES]", conexion, TRAN); // xq los parametros?
                cmdDetalle.CommandType = CommandType.StoredProcedure;

                foreach (DetalleReceta item in oReceta.ListDetalles)
                {
                    cmdDetalle.Parameters.AddWithValue("@id_receta", recetaNro); // Viene del pOut de arriba como ID
                    cmdDetalle.Parameters.AddWithValue("@id_ingrediente", item.ingrediente.idIngrediente);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", item.cantidad);  // van los item xq el detalle viene como una lista
                    cmd.ExecuteNonQuery();

                    detalleNro++;
                }
                TRAN.Commit();
            }

            catch (Exception)
            {
                if (TRAN != null)
                    TRAN.Rollback();
                ok = false;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
            return ok;
        }

        public bool EjecutarUpdate(Receta oReceta)
        {
            bool ok = true;
            SqlTransaction TRAN = null;
            SqlCommand cmd = new SqlCommand();

            try
            {
                conexion.Open();
                TRAN = conexion.BeginTransaction();
                cmd.Connection = conexion;
                cmd.Transaction = TRAN;
                cmd.CommandText = "[dbo].[SP_MODIFICAR_RECETA]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("tipo_receta", oReceta.tipoReceta);
                cmd.Parameters.AddWithValue("nombre", oReceta.nombre);
                cmd.Parameters.AddWithValue("cheff", oReceta.cheff);
                cmd.Parameters.AddWithValue("id_receta", oReceta.id);
                cmd.ExecuteNonQuery();
                
                
                TRAN.Commit();
            }

            catch (Exception)
            {
                if (TRAN != null)
                    TRAN.Rollback();
                ok = false;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
            return ok;

        }

        public List<Receta> ObtenerRecetaPorFilitros(int TipoReceta, string Nombre)
        {
            List<Receta> Receta = new List<Receta>();
            string sp = "SP_CONSULTAR_RECETA";
            List<Parametro> lst = new List<Parametro>();
            lst.Add(new Parametro("@TipoReceta", TipoReceta));
            lst.Add(new Parametro("@Nombre", Nombre));
            DataTable dt =ConsultarSQL(sp, lst);

            foreach(DataRow row in dt.Rows)
            {
                Receta receta = new Receta();
                //receta.id = (int)row["id"];
                receta.nombre = row["Nombre"].ToString();
                receta.tipoReceta = (int)row["Tipo_Receta"];
                receta.cheff = row["Cheff"].ToString();
                Receta.Add(receta);
            }
            return Receta;
        }

        public DataTable ConsultarSQL(string spNombre, List<Parametro> lista)
        {
            DataTable tabla = new DataTable();

            conexion.Open();
            SqlCommand cmd = new SqlCommand(spNombre, conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            if(lista != null)
            {
                foreach(Parametro oParamentro in lista)
                {
                    cmd.Parameters.AddWithValue(oParamentro.Clave, oParamentro.Valor);
                }
            }
            tabla.Load(cmd.ExecuteReader());
            conexion.Close();

            return tabla;            
        }
    }
}
