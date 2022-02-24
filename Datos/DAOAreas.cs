using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos;
using MySql.Data.MySqlClient;
using System.Data;

namespace Datos
{
    public class DAOAreas
    {
        public bool agregar(Area area)
        {
            //Indicar la sentencia y los parámetros
            String insert = "INSERT INTO Categorias(nombre) " +
                "VALUES(@nombre);";

            MySqlCommand comando = new MySqlCommand(insert);

            //Enviar los valores que reemplazarán a cada parámetro
            comando.Parameters.AddWithValue("@nombre", area.Nombre);
            comando.Parameters.AddWithValue("@nombre", area.Ubicacion);

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

        public bool editar(Area area)
        {
            //Indicar la sentencia y los parámetros
            String sentencia = "UPDATE Categorias SET nombre=@nombre" +
                " WHERE id=@id;";

            MySqlCommand comando = new MySqlCommand(sentencia);

            //Enviar los valores que reemplazarán a cada parámetro
            comando.Parameters.AddWithValue("@nombre", area.Nombre);
            comando.Parameters.AddWithValue("@id", area.IDArea);

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

        public bool eliminar(int id)
        {
            //Indicar la sentencia y los parámetros
            String sentencia = "DELETE FROM Categorias WHERE id=@id;";

            MySqlCommand comando = new MySqlCommand(sentencia);

            //Enviar los valores que reemplazarán a cada parámetro
            comando.Parameters.AddWithValue("@idArea", id);

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

        public Area consultarUna(int id)
        {
            //Armar la consulta a ejecutar
            String consulta = "SELECT * FROM Categorias WHERE id=@id";

            //Encapsular la consulta en un command
            MySqlCommand select = new MySqlCommand(consulta);
            select.Parameters.AddWithValue("@idArea", id);

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
                    Area obj = new Area();

                    obj.IDArea = int.Parse(resultado.Rows[0]["idArea"].ToString());
                    obj.Nombre = resultado.Rows[0]["nombre"].ToString();
                    obj.Ubicacion= resultado.Rows[0]["ubicacion"].ToString();


                    return obj;
                }
            }

            return null;
        }
        public List<Area> consultarTodas()
        {
            //Armar la consulta a ejecutar
            String consulta = "SELECT * FROM area ORDER BY nombre;";

            //Encapsular la consulta en un command
            MySqlCommand select = new MySqlCommand(consulta);
            //select.CommandText = consulta;

            //Mandar a ejecutar la consulta
            DataTable resultado = Conexion.ejecutarSelect(select);

            List<Area> lista = new List<Area>();
            //Verificar si hubo respuesta exitósa
            if (resultado != null)
            {
                //Recorrer cada renglon de la tabla de resultados
                //y llenar la lista
                for (int i = 0; i < resultado.Rows.Count; i++)
                {
                    Area obj = new Area(
                    );
                    obj.IDArea = int.Parse(resultado.Rows[i]["idArea"].ToString());
                    obj.Nombre = resultado.Rows[i]["nombre"].ToString();
                    obj.Ubicacion = resultado.Rows[i]["ubicacion"].ToString();

                    lista.Add(obj);
                }
            }

            return lista;
        }



    }
}

