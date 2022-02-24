using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using Modelos;
using System.Data;
using Datos;


namespace Datos
{
    public class DAOInv
    {
        public List<Inv> consultarTodos()
        {
            //Armar la consulta a ejecutar
            String consulta = "SELECT * FROM inv;";

            //Encapsular la consulta en un command
            MySqlCommand select = new MySqlCommand(consulta);
            //select.CommandText = consulta;

            //Mandar a ejecutar la consulta
            DataTable resultado = Conexion.ejecutarSelect(select);

            List<Inv> lista = new List<Inv>();
            //Verificar si hubo respuesta exitósa
            if (resultado != null)
            {
                //Recorrer cada renglon de la tabla de resultados
                //y llenar la lista
                for (int i = 0; i < resultado.Rows.Count; i++)
                {
                    Inv obj = new Inv();

                    obj.IdInventario = int.Parse(resultado.Rows[i]["idInventario"].ToString());
                    obj.NombreCorto = resultado.Rows[i]["nombreCorto"].ToString();
                    obj.Descripcion = resultado.Rows[i]["descripcion"].ToString();
                    obj.Serie = resultado.Rows[i]["serie"].ToString();
                    obj.Color = resultado.Rows[i]["color"].ToString();
                    obj.Fecha = resultado.Rows[i]["fecha"].ToString();
                    obj.TipoAdquisio = resultado.Rows[i]["tipoAdquision"].ToString();
                    obj.obserbaciones = resultado.Rows[i]["obserbaciones"].ToString();



                    lista.Add(obj);
                }
            }

            return lista;
        }

        public bool incetarProducto(Inv inv)
        {
            String insert = "INSERT INTO inv (NombreCorto, Descripcion, Serie, Color, fecha, TipoAdquision, Obserbaciones, idArea)" +
                " VALUES ( @nombreCorto , @descripcion , @serie , @color , @fecha , @tipoAdquisio , @obserbaciones , @idArea);";
            
            MySqlCommand comando = new MySqlCommand(insert);

            //Enviar los valores que reemplazarán a cada parámetro



            comando.Parameters.AddWithValue("@nombreCorto", inv.NombreCorto);

            comando.Parameters.AddWithValue("@descripcion", inv.Descripcion);
            comando.Parameters.AddWithValue("@serie", inv.Serie);
            comando.Parameters.AddWithValue("@color", inv.Color);
            
            comando.Parameters.AddWithValue("@fecha", inv.Fecha);
            comando.Parameters.AddWithValue("@tipoAdquisio", inv.TipoAdquisio);
           
           
            comando.Parameters.AddWithValue("@obserbaciones", inv.obserbaciones);
            comando.Parameters.AddWithValue("@idArea", inv.IdArea);


            //comando.Parameters.AddWithValue("@categoria", producto.Categoria);
            int filas = Conexion.ejecutarSentencia(comando);
            
            if (filas > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool eliminarProducto(int id)
        {
            //Indicar la sentencia y los parámetros
            String sentencia = "DELETE FROM inv WHERE idInventario=@id;";

            MySqlCommand comando = new MySqlCommand(sentencia);

            //Enviar los valores que reemplazarán a cada parámetro
            comando.Parameters.AddWithValue("@id", id);

            int filas = Conexion.ejecutarSentencia(comando);
            if (filas > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool editarProducto(Inv inv)
        {
            //Indicar la sentencia y los parámetros
            String sentencia = "UPDATE inv SET  NombreCorto=@nombreCorto , Descripcion=@descripcion ,Serie=@serie, Color=@color," +
                "fecha=@fecha, TipoAdquision=@tipoAdquisio, Obserbaciones=@obserbaciones ,idArea=@idArea  WHERE idInventario = @idInventario;";

            //String sentencia = "UPDATE `proyecto1`.`inv` SET `NombreCorto` = '@nombreCorto', `Descripcion` = '@descripcion', `Serie` = '@serie', `Color` = '@color', " +
            //     "`fecha` = '@fecha',`TipoAdquision` = '@tipoAdquisio', `Obserbaciones` = '@obserbaciones', `idArea` = 'idArea' WHERE(`idInventario` = 'idInventario');";

            MySqlCommand comando = new MySqlCommand(sentencia);
           
            comando.Parameters.AddWithValue("@nombreCorto", inv.NombreCorto);
            comando.Parameters.AddWithValue("@descripcion", inv.Descripcion);
            comando.Parameters.AddWithValue("@serie", inv.Serie);
            comando.Parameters.AddWithValue("@color", inv.Color);

            comando.Parameters.AddWithValue("@fecha", inv.Fecha);
            comando.Parameters.AddWithValue("@tipoAdquisio", inv.TipoAdquisio);
            comando.Parameters.AddWithValue("@obserbaciones", inv.obserbaciones);
            comando.Parameters.AddWithValue("@idArea", inv.IdArea);
            comando.Parameters.AddWithValue("@idinventario", inv.IdInventario);
            //Enviar los valores que reemplazarán a cada parámetro

            int filas = Conexion.ejecutarSentencia(comando);
            if (filas > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Inv solicitarProducto(int id)
        {
            //Armar la consulta a ejecutar
            String consulta = "SELECT * FROM inv WHERE idInventario=@id";

            //Encapsular la consulta en un command
            MySqlCommand select = new MySqlCommand(consulta);
            select.Parameters.AddWithValue("@id", id);

            //select.CommandText = consulta;

            //Mandar a ejecutar la consulta
            DataTable resultado = Conexion.ejecutarSelect(select);

            //Verificar si hubo respuesta exitósa
            if (resultado != null)
            {
                //Recorrer cada renglon de la tabla de resultados
                //y llenar la lista
                if (resultado.Rows.Count > 0)
                {
                    Inv obj = new Inv();
                    obj.IdInventario = int.Parse(resultado.Rows[0]["idInventario"].ToString());
                    obj.NombreCorto = resultado.Rows[0]["nombreCorto"].ToString();
                    obj.Descripcion = resultado.Rows[0]["descripcion"].ToString();
                    obj.Serie = resultado.Rows[0]["serie"].ToString();
                    obj.Color = resultado.Rows[0]["color"].ToString();
                    obj.Fecha = resultado.Rows[0]["fecha"].ToString();
                    obj.TipoAdquisio = resultado.Rows[0]["tipoAdquision"].ToString();
                    obj.obserbaciones = resultado.Rows[0]["obserbaciones"].ToString();

                    return obj;
                }
            }
            return null;
        }
    }
}
