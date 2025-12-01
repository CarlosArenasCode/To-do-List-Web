using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ToDoList
{
    /// <summary>
    /// Clase para manejar las operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en la base de datos de tareas.
    /// </summary>
    internal class CRUD
    {
        /// <summary>
        /// Agrega una nueva tarea a la base de datos.
        /// </summary>
        /// <param name="nombre">Nombre o título de la tarea.</param>
        /// <param name="descripcion">Descripción de la tarea.</param>
        /// <param name="completada">Indica si la tarea está completada (true) o pendiente (false).</param>
        /// <returns>True si la tarea se agregó correctamente; false en caso contrario.</returns>
        public bool AgregarTarea(string nombre, string descripcion, bool completada)
        {
            // 1. Obtiene la conexión y prepara la consulta de inserción.
            MySqlConnection conexionBD = Conexion.conexion();
            try
            {
                conexionBD.Open();
                string query = "INSERT INTO tareas (nombre, descripcion, completada) VALUES (@nombre, @descripcion, @completada)";
                MySqlCommand comando = new MySqlCommand(query, conexionBD);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@descripcion", descripcion);
                comando.Parameters.AddWithValue("@completada", completada ? 1 : 0);
                // 2. Ejecuta la consulta.
                comando.ExecuteNonQuery();
                conexionBD.Close();
                return true;
            }
            catch (Exception ex)
            {
                // 3. Maneja cualquier error de inserción.
                Console.WriteLine("Error al agregar: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Elimina una tarea de la base de datos por su ID.
        /// </summary>
        /// <param name="id">Identificador único de la tarea.</param>
        /// <returns>True si la tarea se eliminó correctamente; false en caso contrario.</returns>
        public bool EliminarTarea(int id)
        {
            // 1. Obtiene la conexión y prepara la consulta de eliminación.
            MySqlConnection conexionBD = Conexion.conexion();
            try
            {
                conexionBD.Open();
                string query = "DELETE FROM tareas WHERE id = @id";
                MySqlCommand comando = new MySqlCommand(query, conexionBD);
                comando.Parameters.AddWithValue("@id", id);
                // 2. Ejecuta la consulta.
                comando.ExecuteNonQuery();
                conexionBD.Close();
                return true;
            }
            catch (Exception ex)
            {
                // 3. Maneja cualquier error de eliminación.
                Console.WriteLine("Error al eliminar: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Modifica una tarea existente en la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la tarea.</param>
        /// <param name="nombre">Nuevo nombre o título de la tarea.</param>
        /// <param name="descripcion">Nueva descripción de la tarea.</param>
        /// <param name="completada">Nuevo estado de la tarea (true si está completada).</param>
        /// <returns>True si la tarea se modificó correctamente; false en caso contrario.</returns>
        public bool ModificarTarea(int id, string nombre, string descripcion, bool completada)
        {
            // 1. Obtiene la conexión y prepara la consulta de actualización.
            MySqlConnection conexionBD = Conexion.conexion();
            try
            {
                conexionBD.Open();
                string query = "UPDATE tareas SET nombre = @nombre, descripcion = @descripcion, completada = @completada WHERE id = @id";
                MySqlCommand comando = new MySqlCommand(query, conexionBD);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@descripcion", descripcion);
                comando.Parameters.AddWithValue("@completada", completada ? 1 : 0);
                comando.Parameters.AddWithValue("@id", id);
                // 2. Ejecuta la consulta.
                comando.ExecuteNonQuery();
                conexionBD.Close();
                return true;
            }
            catch (Exception ex)
            {
                // 3. Maneja cualquier error de actualización.
                Console.WriteLine("Error al modificar: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Consulta todas las tareas de la base de datos.
        /// </summary>
        /// <returns>Un MySqlDataReader con todas las tareas.</returns>
        public MySqlDataReader ConsultarTareas()
        {
            MySqlConnection conexionBD = Conexion.conexion();
            try
            {
                conexionBD.Open();
                string query = "SELECT * FROM tareas";
                MySqlCommand comando = new MySqlCommand(query, conexionBD);
                // 1. Ejecuta la consulta y retorna el DataReader.
                MySqlDataReader leer = comando.ExecuteReader();
                return leer;
            }
            catch (Exception ex)
            {
                // 2. Maneja cualquier error de consulta.
                Console.WriteLine("Error al consultar: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Consulta solo las tareas completadas de la base de datos.
        /// </summary>
        /// <returns>Un MySqlDataReader con las tareas completadas.</returns>
        public MySqlDataReader ConsultarTareasCompletas()
        {
            MySqlConnection conexionBD = Conexion.conexion();
            try
            {
                conexionBD.Open();
                string query = "SELECT * FROM tareas WHERE completada = 1";
                MySqlCommand comando = new MySqlCommand(query, conexionBD);
                // 1. Ejecuta la consulta y retorna el DataReader.
                MySqlDataReader leer = comando.ExecuteReader();
                return leer;
            }
            catch (Exception ex)
            {
                // 2. Maneja cualquier error de consulta.
                Console.WriteLine("Error al consultar completas: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Consulta solo las tareas pendientes de la base de datos.
        /// </summary>
        /// <returns>Un MySqlDataReader con las tareas pendientes.</returns>
        public MySqlDataReader ConsultarTareasPendientes()
        {
            MySqlConnection conexionBD = Conexion.conexion();
            try
            {
                conexionBD.Open();
                string query = "SELECT * FROM tareas WHERE completada = 0";
                MySqlCommand comando = new MySqlCommand(query, conexionBD);
                // 1. Ejecuta la consulta y retorna el DataReader.
                MySqlDataReader leer = comando.ExecuteReader();
                return leer;
            }
            catch (Exception ex)
            {
                // 2. Maneja cualquier error de consulta.
                Console.WriteLine("Error al consultar pendientes: " + ex.Message);
                return null;
            }
        }
    }
}
