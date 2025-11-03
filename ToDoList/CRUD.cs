using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ToDoList
{
    internal class CRUD // Clase para manejar las operaciones CRUD en la base de datos
    {
        // Método para AGREGAR una tarea a la base de datos
        public void AgregarTarea(string nombre, string descripcion, bool completada) // No retorna ningún valor, solo realiza la operación
        {
            // Lammamos al metodo conexion() de la clase Conexion para obtener la conexión a la base de datos
            MySqlConnection conexionBD = Conexion.conexion();

            // Manejo de errores con try-catch
            try
            {
                // Establecemo la conexión a la base de datos con el método Open()
                conexionBD.Open();

                // Crear el comando SQL para insertar
                // My sql almacena booleanos como 0 (falso) y 1 (verdadero)
                string query = "INSERT INTO tareas (nombre, descripcion, completada) VALUES ('" + nombre + "', '" + descripcion + "', " + (completada ? 1 : 0) + ")";

                // Crear el objeto comando con la consulta(query) y la conexión(conexionBD) para la conexión abierta a la base de datos
                MySqlCommand comando = new MySqlCommand(query, conexionBD);

                // Ejecutar el comando con ExecuteNonQuery() ya que no esperamos resultados de retorno solo hace la inserción
                comando.ExecuteNonQuery(); // Ejecutar el comando
                
                conexionBD.Close(); // Cerrar la conexión
            }
            catch (Exception ex) // Si hay error
            {
                Console.WriteLine("Error al agregar: " + ex.Message);
            }
        }


        // Método para ELIMINAR una tarea de la base de datos
        public void EliminarTarea(int id)
        {
            // Obtiene la conexión a la base de datos MySQL.
            MySqlConnection conexionBD = Conexion.conexion();

            // Inicia un bloque 'try' para capturar posibles errores.
            try
            {
                // Abre la conexión con la base de datos.
                conexionBD.Open();

                // Prepara la consulta SQL para borrar usando el ID.
                string query = "DELETE FROM tareas WHERE id = " + id;

                // Crea el comando SQL (la consulta + la conexión).
                MySqlCommand comando = new MySqlCommand(query, conexionBD);

                // Ejecuta el comando (DELETE), que no devuelve datos.
                comando.ExecuteNonQuery();

                // Cierra la conexión.
                conexionBD.Close();
            }
            // Captura cualquier excepción que ocurra durante el proceso.
            catch (Exception ex)
            {
                // Muestra el error en la consola para depuración.
                Console.WriteLine("Error al eliminar: " + ex.Message);
            }
        }


        // Método para MODIFICAR una tarea en la base de datos
        public void ModificarTarea(int id, string nombre, string descripcion, bool completada)
        {
            MySqlConnection conexionBD = Conexion.conexion(); // Llamamos al método conexion() para obtener la conexión

            try
            {
                conexionBD.Open(); // Abrir la conexión

                // Crear el comando SQL para actualizar con los nuevos valores
                string query = "UPDATE tareas SET nombre = '" + nombre + "', descripcion = '" + descripcion + "', completada = " + (completada ? 1 : 0) + " WHERE id = " + id;

                // Crear el objeto comando con la consulta(query) y la conexión(conexionBD)
                MySqlCommand comando = new MySqlCommand(query, conexionBD);
                comando.ExecuteNonQuery(); // Ejecutar el comando
                
                conexionBD.Close(); // Cerrar la conexión
            }
            catch (Exception ex) // Si hay error
            {
                Console.WriteLine("Error al modificar: " + ex.Message);
            }
        }


        // Método para CONSULTAR todas las tareas
        public MySqlDataReader ConsultarTareas()
        {
            // Llamamos al método estático para obtener el objeto de conexión a MySQL
            MySqlConnection conexionBD = Conexion.conexion();
            
            // Usamos try-catch para manejar errores de BD sin que el programa se cierre
            try
            {
                // Abrimos la conexión física con el servidor MySQL para poder ejecutar comandos
                conexionBD.Open();
                
                // Construimos la consulta SELECT para obtener todos los registros de la tabla
                string query = "SELECT * FROM tareas";

                // Creamos el objeto comando vinculando la query con la conexión activa
                MySqlCommand comando = new MySqlCommand(query, conexionBD);

                // Ejecutamos ExecuteReader porque esperamos múltiples filas de datos
                // Retorna un DataReader que lee los datos fila por fila bajo demanda
                MySqlDataReader leer = comando.ExecuteReader();
                
                // Devolvemos el DataReader para que Form1 pueda iterar sobre los resultados
                return leer;

                // NO cerramos la conexión aquí porque el DataReader la necesita abierta
                // Quien reciba el DataReader debe cerrar la conexión después de leer
            }
            // Si ocurre algún error (conexión, sintaxis SQL, etc.) lo capturamos aquí
            catch (Exception ex)
            {
                // Mostramos el error en consola para debugging sin detener el programa
                Console.WriteLine("Error al consultar: " + ex.Message);
                
                // Retornamos null para que Form1 sepa que hubo un error
                return null;
            }
        }


        // Método para CONSULTAR tareas completadas
        public MySqlDataReader ConsultarTareasCompletas()
        {
            // Llamamos al método estático para obtener el objeto de conexión a MySQL
            MySqlConnection conexionBD = Conexion.conexion();
            
            // Usamos try-catch para manejar errores de BD sin que el programa se cierre
            try
            {
                // Abrimos la conexión física con el servidor MySQL para poder ejecutar comandos
                conexionBD.Open();
                
                // Construimos SELECT con WHERE para filtrar solo registros con completada = 1
                string query = "SELECT * FROM tareas WHERE completada = 1";
                
                // Creamos el objeto comando vinculando la query con la conexión activa
                MySqlCommand comando = new MySqlCommand(query, conexionBD);
                
                // Ejecutamos ExecuteReader porque esperamos múltiples filas filtradas
                MySqlDataReader leer = comando.ExecuteReader();
                
                // Devolvemos el DataReader para que Form1 pueda iterar sobre los resultados
                return leer;
                
                // NO cerramos la conexión aquí porque el DataReader la necesita abierta
            }
            // Si ocurre algún error lo capturamos sin detener el programa
            catch (Exception ex)
            {
                // Mostramos el error en consola para debugging
                Console.WriteLine("Error al consultar completas: " + ex.Message);
                
                // Retornamos null para indicar que la consulta falló
                return null;
            }
        }


        // Método para CONSULTAR tareas pendientes
        public MySqlDataReader ConsultarTareasPendientes()
        {
            // Llamamos al método estático para obtener el objeto de conexión a MySQL
            MySqlConnection conexionBD = Conexion.conexion();

            // Usamos try-catch para manejar errores de BD sin que el programa se cierre
            try
            {
                // Abrimos la conexión física con el servidor MySQL para poder ejecutar comandos
                conexionBD.Open();
                
                // Construimos SELECT con WHERE para filtrar solo registros con completada = 0
                string query = "SELECT * FROM tareas WHERE completada = 0";
                
                // Creamos el objeto comando vinculando la query con la conexión activa
                MySqlCommand comando = new MySqlCommand(query, conexionBD);
                
                // Ejecutamos ExecuteReader porque esperamos múltiples filas filtradas
                MySqlDataReader leer = comando.ExecuteReader();
                
                // Devolvemos el DataReader para que Form1 pueda iterar sobre los resultados
                return leer;
                
                // NO cerramos la conexión aquí porque el DataReader la necesita abierta
            }
            // Si ocurre algún error lo capturamos sin detener el programa
            catch (Exception ex)
            {
                // Mostramos el error en consola para debugging
                Console.WriteLine("Error al consultar pendientes: " + ex.Message);
                
                // Retornamos null para indicar que la consulta falló
                return null;
            }
        }
    }
}
