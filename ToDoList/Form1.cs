using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ToDoList

// Programa: Lista de Tareas (To-Do List) con operaciones de Alta, Baja, Cambio y Consultas
// Descripción: Programa que permite gestionar una lista de tareas conectado a MySQL
// Desarrollado por: Carlos Saul Arenas Maciel
// Fecha: 21/09/2025
{
    public partial class Form1 : Form
    {
        // Crear objeto de la clase CRUD para usar sus métodos
        CRUD crud = new CRUD();
        
        // Variable para guardar el ID de la tarea seleccionada
        private int idTareaSeleccionada = 0;
        
        // Lista para guardar los IDs en el mismo orden que aparecen en el ListBox
        private List<int> listaIDs = new List<int>();

        public Form1()
        {
            InitializeComponent();

            // Conectar eventos manualmente por que tuve problemas con el designer
            btnAgregar.Click += btnAgregar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnModificar.Click += btnModificar_Click;
            btnMostrarCompletas.Click += btnMostrarCompletas_Click;
            btnMostrarPendientes.Click += btnMostrarPendientes_Click;
            btnMostrarTodas.Click += btnMostrarTodas_Click;
            btnSalir.Click += btnSalir_Click;
            lstTareas.SelectedIndexChanged += lstTareas_SelectedIndexChanged;
            
            // Cargar todas las tareas al iniciar
            MostrarTodasLasTareas();
        }

        // Método para limpiar los campos de texto
        private void LimpiarCampos()
        {
            txtTarea.Clear();
            txtDescripcion.Clear();
            chkCompletada.Checked = false;
            idTareaSeleccionada = 0;
        }


        // Método para agregar tarea (Alta)
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTarea.Text))
            {
                MessageBox.Show("Por favor ingrese el nombre de la tarea.", "Error");
                return;
            }

            // Llamar al método AgregarTarea del CRUD
            crud.AgregarTarea(txtTarea.Text, txtDescripcion.Text, chkCompletada.Checked);

            // Limpiar campos
            LimpiarCampos();

            // Actualizar lista
            MostrarTodasLasTareas();
            lstTareas.ClearSelected();

            MessageBox.Show("Tarea agregada correctamente.", "Éxito");
        }


        // Método para eliminar tarea (Baja)
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Revisa si hay una tarea seleccionada.
            if (idTareaSeleccionada == 0)
            {
                // Si no, muestra un error
                MessageBox.Show("Por favor seleccione una tarea para eliminar.", "Error");

                // y sale del método.
                return;
            }

            // Llama al método que borra la tarea de la base de datos.
            crud.EliminarTarea(idTareaSeleccionada);

            // Limpia los campos de texto.
            LimpiarCampos();

            // Actualiza la lista de tareas en pantalla.
            MostrarTodasLasTareas();

            // Quita la selección de la lista.
            lstTareas.ClearSelected();

            // Muestra un mensaje de éxito.
            MessageBox.Show("Tarea eliminada correctamente.", "Éxito");
        }


        // Método para modificar tarea (Cambio)
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idTareaSeleccionada == 0) // Verificar si hay una tarea seleccionada
            {
                // Mostrar mensaje de error
                MessageBox.Show("Por favor seleccione una tarea para modificar.", "Error");
                return; // Salir del método si no hay tarea seleccionada
            }

            // Validar que el campo de tarea no esté vacío
            if (string.IsNullOrEmpty(txtTarea.Text)) 
            {
                // Mostrar mensaje de error si el campo está vacío
                MessageBox.Show("Por favor ingrese el nombre de la tarea.", "Error");
                return; // Salir del método si el campo está vacío
            }

            // Llamar al método ModificarTarea del CRUD
            crud.ModificarTarea(idTareaSeleccionada, txtTarea.Text, txtDescripcion.Text, chkCompletada.Checked);

            // Limpiar campos para dejar listo para la siguiente operación
            LimpiarCampos();

            // Actualizar lista, para que se refleje el cambio en la interfaz
            MostrarTodasLasTareas();
            lstTareas.ClearSelected();

            MessageBox.Show("Tarea modificada correctamente.", "Éxito"); // Mostrar mensaje de éxito
        }

        // Consulta 1: Mostrar todas las tareas
        private void btnMostrarTodas_Click(object sender, EventArgs e)
        {
            MostrarTodasLasTareas(); // Llamar al método para mostrar todas las tareas
            lstTareas.ClearSelected(); // Limpiar la selección del ListBox
        }

        // Método para mostrar todas las tareas desde la base de datos
        private void MostrarTodasLasTareas()
        {
            lstTareas.Items.Clear(); // Limpiar la lista
            listaIDs.Clear(); // Limpiar la lista de IDs

            // Llamar al método ConsultarTareas del CRUD
            MySqlDataReader leer = crud.ConsultarTareas();

            // Verificar que el lector no sea nulo
            if (leer != null)
            {
                // Leer cada tarea de la base de datos
                while (leer.Read())
                {
                    int id = leer.GetInt32(0); // Obtener el ID de la tarea
                    string nombre = leer.GetString(1); // Obtener el nombre de la tarea
                    string descripcion = leer.GetString(2); // Obtener la descripción de la tarea
                    bool completada = leer.GetBoolean(3); // Obtener el estado de la tarea

                    listaIDs.Add(id); // Guardar el ID internamente
                    string estado = completada ? "HECHA" : "PENDIENTE"; // Determinar el estado de la tarea
                    lstTareas.Items.Add(estado + " - " + nombre + " - " + descripcion); // Agregar la tarea al ListBox
                }
                leer.Close(); // Cerrar el lector después de usarlo
            }
        }


        // Consulta 2: Mostrar tareas completas
        private void btnMostrarCompletas_Click(object sender, EventArgs e)
        {
            lstTareas.Items.Clear(); // Limpiar la lista
            listaIDs.Clear(); // Limpiar la lista de IDs

            // Llamar al método ConsultarTareasCompletas del CRUD
            MySqlDataReader leer = crud.ConsultarTareasCompletas();

            if (leer != null) // Verificar que el lector no sea nulo
            {
                // Leer cada tarea de la base de datos, para cada fila en el resultado de la consulta,
                while (leer.Read())
                {
                    int id = leer.GetInt32(0); // Obtener el ID de la tarea
                    string nombre = leer.GetString(1); // Obtener el nombre de la tarea
                    string descripcion = leer.GetString(2); // Obtener la descripción de la tarea

                    listaIDs.Add(id); // Guardar el ID internamente para referencia futura
                    lstTareas.Items.Add("HECHA - " + nombre + " - " + descripcion); // Agregar la tarea al ListBox
                }
                leer.Close(); // Cerrar el lector después de usarlo
            }

            lstTareas.ClearSelected(); // Limpiar la selección del ListBox
        }


        // Consulta 3: Mostrar tareas pendientes
        private void btnMostrarPendientes_Click(object sender, EventArgs e)
        {
            lstTareas.Items.Clear(); // Limpiar la lista
            listaIDs.Clear(); // Limpiar la lista de IDs

            // Llamar al método ConsultarTareasPendientes del CRUD
            MySqlDataReader leer = crud.ConsultarTareasPendientes();

            // Verificar que el lector no sea nulo
            if (leer != null)
            {
                // Leer cada tarea de la base de datos
                while (leer.Read())
                {
                    int id = leer.GetInt32(0); // Obtener el ID de la tarea
                    string nombre = leer.GetString(1); // Obtener el nombre de la tarea
                    string descripcion = leer.GetString(2); // Obtener la descripción de la tarea

                    listaIDs.Add(id); // Guardar el ID internamente
                    lstTareas.Items.Add("PENDIENTE - " + nombre + " - " + descripcion); // Agregar la tarea al ListBox
                }
                leer.Close(); // Cerrar el lector después de usarlo
            }

            lstTareas.ClearSelected(); // Limpiar la selección del ListBox
        }





        // Evento cuando se selecciona una tarea de la lista
        private void lstTareas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTareas.SelectedIndex != -1)
            {
                // Obtener el ID de la lista interna usando el índice seleccionado
                int indice = lstTareas.SelectedIndex;
                idTareaSeleccionada = listaIDs[indice];
                
                // Buscar la tarea en la base de datos para llenar los campos
                MySqlDataReader leer = crud.ConsultarTareas();
                if (leer != null)
                {
                    while (leer.Read())
                    {
                        int id = leer.GetInt32(0);
                        if (id == idTareaSeleccionada)
                        {
                            txtTarea.Text = leer.GetString(1);
                            txtDescripcion.Text = leer.GetString(2);
                            chkCompletada.Checked = leer.GetBoolean(3);
                            break;
                        }
                    }
                    leer.Close();
                }
            }
        }


        // Botón para salir de la aplicación
        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea salir?", "Confirmar salida", 
                MessageBoxButtons.YesNo, // Botones Sí y No
                MessageBoxIcon.Question); // Icono de pregunta

            if (resultado == DialogResult.Yes) // Si el usuario confirma, el form se cierra
            {
                this.Close();
            }
        }
    }
}
