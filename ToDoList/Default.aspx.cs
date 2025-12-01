using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ToDoList
{
    /// <summary>
    /// Página principal de la aplicación ToDoList. Permite gestionar tareas (agregar, modificar, eliminar y filtrar).
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        private CRUD crud = new CRUD();

        /// <summary>
        /// Evento que se ejecuta al cargar la página. Carga todas las tareas si no es postback.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar todas las tareas al inicio
                CargarTareas();
            }
        }

        /// <summary>
        /// Limpia todos los campos del formulario de tarea.
        /// </summary>
        private void LimpiarCampos()
        {
            // 1. Borra el ID, título y descripción.
            txtId.Text = "";
            txtTitulo.Text = "";
            txtDescripcion.Text = "";
            // 2. Restablece el estado a "Pendiente".
            ddlCompletada.SelectedIndex = 0;
        }

        /// <summary>
        /// Muestra un mensaje al usuario en el panel superior.
        /// </summary>
        /// <param name="mensaje">Texto del mensaje a mostrar.</param>
        /// <param name="tipo">Tipo de mensaje: success, error, warning, info.</param>
        private void MostrarMensaje(string mensaje, string tipo)
        {
            // 1. Hace visible el panel de mensaje.
            pnlMensaje.Visible = true;
            // 2. Establece el texto del mensaje.
            lblMensaje.Text = mensaje;
            // 3. Aplica la clase CSS según el tipo de mensaje.
            pnlMensaje.CssClass = "alert";
            switch (tipo.ToLower())
            {
                case "success":
                    pnlMensaje.CssClass += " alert-success";
                    break;
                case "error":
                    pnlMensaje.CssClass += " alert-danger";
                    break;
                case "warning":
                    pnlMensaje.CssClass += " alert-warning";
                    break;
                case "info":
                    pnlMensaje.CssClass += " alert-info";
                    break;
            }
        }

        /// <summary>
        /// Carga las tareas en el GridView.
        /// </summary>
        /// <param name="dt">DataTable opcional con las tareas a mostrar. Si es null, consulta todas las tareas.</param>
        protected void CargarTareas(DataTable dt = null)
        {
            try
            {
                // 1. Si no se recibe un DataTable, consulta todas las tareas desde la base de datos.
                if (dt == null)
                {
                    using (var reader = crud.ConsultarTareas())
                    {
                        if (reader != null)
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            dt = dataTable;
                        }
                    }
                }
                // 2. Asigna los datos al GridView y lo actualiza.
                if (dt != null && dt.Rows.Count > 0)
                {
                    gvTareas.DataSource = dt;
                    gvTareas.DataBind();
                }
                else
                {
                    gvTareas.DataSource = null;
                    gvTareas.DataBind();
                }
            }
            catch (Exception ex)
            {
                // 3. Si ocurre un error, muestra un mensaje de error.
                MostrarMensaje("Error al cargar tareas: " + ex.Message, "error");
            }
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón "Agregar Tarea".
        /// </summary>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Valida el formulario.
                if (!Page.IsValid)
                    return;

                // 2. Obtiene los datos ingresados por el usuario (título, descripción, estado).
                string titulo = txtTitulo.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                bool completada = ddlCompletada.SelectedValue == "1";

                // 3. Llama al método CRUD para insertar la nueva tarea en la base de datos.
                bool resultado = crud.AgregarTarea(titulo, descripcion, completada);

                if (resultado)
                {
                    // 4. Si la inserción es exitosa, muestra un mensaje, limpia el formulario y recarga la lista.
                    MostrarMensaje("✓ Tarea agregada exitosamente", "success");
                    LimpiarCampos();
                    CargarTareas();
                }
                else
                {
                    // 5. Si falla, muestra un mensaje de error.
                    MostrarMensaje("✗ Error al agregar la tarea", "error");
                }
            }
            catch (Exception ex)
            {
                // 6. Captura y muestra cualquier excepción ocurrida.
                MostrarMensaje("Error: " + ex.Message, "error");
            }
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón "Modificar Tarea".
        /// </summary>
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Valida el formulario.
                if (!Page.IsValid)
                    return;

                // 2. Verifica que se haya seleccionado una tarea.
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    MostrarMensaje("Seleccione una tarea de la lista para modificar", "warning");
                    return;
                }

                // 3. Valida el ID.
                int id;
                if (!int.TryParse(txtId.Text, out id))
                {
                    MostrarMensaje("ID inválido", "error");
                    return;
                }

                // 4. Obtiene los datos ingresados por el usuario.
                string titulo = txtTitulo.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                bool completada = ddlCompletada.SelectedValue == "1";

                // 5. Llama al método CRUD para modificar la tarea.
                bool resultado = crud.ModificarTarea(id, titulo, descripcion, completada);

                if (resultado)
                {
                    // 6. Si la modificación es exitosa, muestra un mensaje, limpia el formulario y recarga la lista.
                    MostrarMensaje("✓ Tarea modificada exitosamente", "success");
                    LimpiarCampos();
                    CargarTareas();
                }
                else
                {
                    // 7. Si falla, muestra un mensaje de error.
                    MostrarMensaje("✗ Error al modificar la tarea", "error");
                }
            }
            catch (Exception ex)
            {
                // 8. Captura y muestra cualquier excepción ocurrida.
                MostrarMensaje("Error: " + ex.Message, "error");
            }
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón "Eliminar" de una tarea.
        /// </summary>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Obtiene el ID de la tarea a eliminar.
                Button btn = (Button)sender;
                int id = Convert.ToInt32(btn.CommandArgument);

                // 2. Llama al método CRUD para eliminar la tarea.
                bool resultado = crud.EliminarTarea(id);

                if (resultado)
                {
                    // 3. Si la eliminación es exitosa, muestra un mensaje, limpia el formulario y recarga la lista.
                    MostrarMensaje("✓ Tarea eliminada exitosamente", "success");
                    LimpiarCampos();
                    CargarTareas();
                }
                else
                {
                    // 4. Si falla, muestra un mensaje de error.
                    MostrarMensaje("✗ Error al eliminar la tarea", "error");
                }
            }
            catch (Exception ex)
            {
                // 5. Captura y muestra cualquier excepción ocurrida.
                MostrarMensaje("Error: " + ex.Message, "error");
            }
        }

        /// <summary>
        /// Evento que limpia los campos del formulario y oculta el mensaje.
        /// </summary>
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            pnlMensaje.Visible = false;
        }

        /// <summary>
        /// Evento que muestra todas las tareas en el GridView.
        /// </summary>
        protected void btnMostrarTodas_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Consulta todas las tareas.
                using (var reader = crud.ConsultarTareas())
                {
                    DataTable dt = new DataTable();
                    if (reader != null)
                    {
                        dt.Load(reader);
                    }
                    CargarTareas(dt);
                }
                // 2. Muestra mensaje informativo.
                MostrarMensaje("Mostrando todas las tareas", "info");
            }
            catch (Exception ex)
            {
                // 3. Muestra mensaje de error si ocurre una excepción.
                MostrarMensaje("Error: " + ex.Message, "error");
            }
        }

        /// <summary>
        /// Evento que muestra solo las tareas completadas en el GridView.
        /// </summary>
        protected void btnMostrarCompletas_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Consulta solo tareas completadas.
                using (var reader = crud.ConsultarTareasCompletas())
                {
                    DataTable dt = new DataTable();
                    if (reader != null)
                    {
                        dt.Load(reader);
                    }
                    CargarTareas(dt);
                }
                // 2. Muestra mensaje informativo.
                MostrarMensaje("Mostrando tareas completadas", "info");
            }
            catch (Exception ex)
            {
                // 3. Muestra mensaje de error si ocurre una excepción.
                MostrarMensaje("Error: " + ex.Message, "error");
            }
        }

        /// <summary>
        /// Evento que muestra solo las tareas pendientes en el GridView.
        /// </summary>
        protected void btnMostrarPendientes_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Consulta solo tareas pendientes.
                using (var reader = crud.ConsultarTareasPendientes())
                {
                    DataTable dt = new DataTable();
                    if (reader != null)
                    {
                        dt.Load(reader);
                    }
                    CargarTareas(dt);
                }
                // 2. Muestra mensaje informativo.
                MostrarMensaje("Mostrando tareas pendientes", "info");
            }
            catch (Exception ex)
            {
                // 3. Muestra mensaje de error si ocurre una excepción.
                MostrarMensaje("Error: " + ex.Message, "error");
            }
        }

        /// <summary>
        /// Evento que se ejecuta al seleccionar una fila del GridView.
        /// </summary>
        protected void gvTareas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // 1. Obtiene los valores de la fila seleccionada y los coloca en el formulario.
                GridViewRow row = gvTareas.SelectedRow;
                txtId.Text = row.Cells[0].Text;
                txtTitulo.Text = row.Cells[1].Text;
                txtDescripcion.Text = row.Cells[2].Text;
                // 2. Marca el estado correspondiente.
                string estadoTexto = row.Cells[3].Text;
                if (estadoTexto.Contains("Completada"))
                {
                    ddlCompletada.SelectedValue = "1";
                }
                else
                {
                    ddlCompletada.SelectedValue = "0";
                }
                // 3. Muestra un mensaje informativo.
                MostrarMensaje("Tarea seleccionada. Puede modificarla o eliminarla.", "info");
            }
            catch (Exception ex)
            {
                // 4. Si ocurre un error, muestra un mensaje de error.
                MostrarMensaje("Error al seleccionar tarea: " + ex.Message, "error");
            }
        }
    }
}
