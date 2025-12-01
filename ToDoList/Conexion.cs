using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace ToDoList
{
    /// <summary>
    /// Clase para manejar la conexión a la base de datos MySQL.
    /// </summary>
    internal class Conexion
    {
        /// <summary>
        /// Obtiene una nueva conexión a la base de datos MySQL.
        /// </summary>
        /// <returns>Objeto MySqlConnection listo para usarse.</returns>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error al obtener la conexión.</exception>
        public static MySqlConnection conexion()
        {
            // Hacemos un manejo de errores con try-catch
            try 
            {
                // Leer la cadena de conexión desde Web.config
                string cadenaConexion = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                // Crear el objeto de conexión con la cadena de conexión
                MySqlConnection conexionBD = new MySqlConnection(cadenaConexion); 

                // Retornar la conexión
                return conexionBD;
            }
            catch (MySqlException ex)
            {
                // En caso de que no se pueda conectar a MySQL, se muestra el mensaje de error
                throw new Exception("Error de conexión a MySQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Cualquier otro error
                throw new Exception("Error al obtener la conexión: " + ex.Message);
            }
        }
    }
}
