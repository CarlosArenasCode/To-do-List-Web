<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDoList.Default" %>

<!--
    Página principal de la aplicación ToDoList.
    Permite agregar, modificar, eliminar y filtrar tareas.
    Desarrollado por Carlos Saul Arenas Maciel | 2025
-->
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--
        Sección de metadatos y estilos.
        Incluye Bootstrap para el diseño visual y estilos personalizados.
    -->
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Lista de Tareas - To-Do List</title>
    
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.0/font/bootstrap-icons.css" rel="stylesheet"/>
    
    <style>
        body {
            background: #181a20;
            min-height: 100vh;
            padding: 10px 0;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        
        .main-container {
            max-width: 900px;
            margin: 0 auto;
            padding: 0 10px;
        }
        
        .card {
            border-radius: 12px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.06);
            border: 1px solid #e3e6ed;
            margin-bottom: 18px;
            background: #fff;
        }
        
        .card-header {
            background: #f8fafc;
            color: #333;
            border-radius: 12px 12px 0 0 !important;
            padding: 12px 20px;
            border-bottom: 1px solid #e3e6ed;
        }
        
        .card-header h2 {
            margin: 0;
            font-weight: 600;
            font-size: 1.15rem;
            color: #3a3a3a;
        }
        
        /* Botones principales del formulario */
        .btn-agregar {
            background: #2E7D32 !important;
            color: #fff !important;
            border: none !important;
        }
        .btn-agregar:hover {
            background: #256026 !important;
            color: #fff !important;
        }
        .btn-modificar {
            background: #EF6C00 !important;
            color: #fff !important;
            border: none !important;
        }
        .btn-modificar:hover {
            background: #b35300 !important;
            color: #fff !important;
        }
        .btn-limpiar {
            background: #757575 !important;
            color: #fff !important;
            border: none !important;
        }
        .btn-limpiar:hover {
            background: #5a5a5a !important;
            color: #fff !important;
        }
        /* Filtros */
        .btn-mostrar-todas {
            background: #1565C0 !important;
            color: #fff !important;
            border: none !important;
        }
        .btn-mostrar-todas:hover {
            background: #0d3c75 !important;
            color: #fff !important;
        }
        .btn-completadas {
            background: #2E7D32 !important;
            color: #fff !important;
            border: none !important;
        }
        .btn-completadas:hover {
            background: #256026 !important;
            color: #fff !important;
        }
        .btn-pendientes {
            background: #EF6C00 !important;
            color: #fff !important;
            border: none !important;
        }
        .btn-pendientes:hover {
            background: #b35300 !important;
            color: #fff !important;
        }
        /* Acciones en la lista */
        .acciones-btn {
            display: block;
            width: 100%;
            margin-bottom: 6px;
            min-width: 110px;
            max-width: 140px;
            text-align: center;
            font-size: 1rem;
            padding: 0.45rem 0;
            border-radius: 7px;
        }
        .acciones-btn:last-child {
            margin-bottom: 0;
        }
        .btn-azul {
            background: #1565C0 !important;
            color: #fff !important;
            border: none !important;
        }
        .btn-azul:hover {
            background: #0d3c75 !important;
            color: #fff !important;
        }
        .btn-rojo {
            background: #C62828 !important;
            color: #fff !important;
            border: none !important;
        }
        .btn-rojo:hover {
            background: #8e1c1c !important;
            color: #fff !important;
        }
        
        /* Badge para estado Completada */
        .badge-completada {
            background-color: #43a047 !important; /* Verde */
            color: #fff !important;
            padding: 0.35em 0.8em;
            border-radius: 0.7em;
            font-weight: 600;
            font-size: 1em;
            display: inline-block;
        }
        .badge-pendiente {
            background-color: #e0e0e0 !important;
            color: #333 !important;
            padding: 0.35em 0.8em;
            border-radius: 0.7em;
            font-weight: 600;
            font-size: 1em;
            display: inline-block;
        }
        
        @media (max-width: 991px) {
            .main-container {
                max-width: 98vw;
                padding: 0 2vw;
            }
            .card-header h2 {
                font-size: 1.05rem;
            }
        }
        
        @media (max-width: 600px) {
            .main-container {
                max-width: 100vw;
                padding: 0 2vw;
            }
            .card-header h2 {
                font-size: 0.98rem;
            }
            .btn-lg, .btn {
                font-size: 0.95rem;
                padding: 0.4rem 0.7rem;
            }
            .form-label, label, .form-control, .alert {
                font-size: 0.93rem;
            }
            .table {
                font-size: 0.93rem;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-container">
            <!-- Encabezado principal -->
            <div class="text-center mb-4">
                <h1 class="text-white">Lista de Tareas</h1>
                <p class="text-white">Gestiona tus tareas de manera eficiente</p>
            </div>
            
            <!-- Panel de mensajes de estado -->
            <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert" role="alert">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </asp:Panel>
            
            <!-- Formulario para agregar o modificar tareas -->
            <div class="card">
                <div class="card-header">
                    <h2>Agregar / Modificar Tarea</h2>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label fw-bold">ID (automático al agregar)</label>
                            <asp:TextBox ID="txtId" runat="server" CssClass="form-control" ReadOnly="true" placeholder="Se genera automáticamente"></asp:TextBox>
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label fw-bold">Título <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Ej: Comprar alimentos" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" 
                                ControlToValidate="txtTitulo" 
                                ErrorMessage="El título es obligatorio" 
                                CssClass="text-danger small"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-12 mb-3">
                            <label class="form-label fw-bold">Descripción <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" 
                                TextMode="MultiLine" Rows="3" 
                                placeholder="Describe los detalles de la tarea..." 
                                MaxLength="500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" 
                                ControlToValidate="txtDescripcion" 
                                ErrorMessage="La descripción es obligatoria" 
                                CssClass="text-danger small"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                        
                        <div class="col-md-6 mb-3">
                            <label class="form-label fw-bold">Estado</label>
                            <asp:DropDownList ID="ddlCompletada" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0" Selected="True">Pendiente</asp:ListItem>
                                <asp:ListItem Value="1">Completada</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    
                    <div class="row mt-3">
                        <div class="col-12">
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar Tarea" CssClass="btn btn-agregar btn-lg me-2" OnClick="btnAgregar_Click" />
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar Tarea" CssClass="btn btn-modificar btn-lg me-2" OnClick="btnModificar_Click" />
                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar Campos" CssClass="btn btn-limpiar btn-lg" OnClick="btnLimpiar_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Filtros para mostrar tareas -->
            <div class="card">
                <div class="card-header">
                    <h2>Filtrar Tareas</h2>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-4 mb-2">
                            <asp:Button ID="btnMostrarTodas" runat="server" Text="Mostrar Todas" CssClass="btn btn-mostrar-todas w-100" OnClick="btnMostrarTodas_Click" CausesValidation="false" />
                        </div>
                        
                        <div class="col-md-4 mb-2">
                            <asp:Button ID="btnMostrarCompletas" runat="server" Text="Tareas Completadas" CssClass="btn btn-completadas w-100" OnClick="btnMostrarCompletas_Click" CausesValidation="false" />
                        </div>
                        
                        <div class="col-md-4 mb-2">
                            <asp:Button ID="btnMostrarPendientes" runat="server" Text="Tareas Pendientes" CssClass="btn btn-pendientes w-100" OnClick="btnMostrarPendientes_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- GridView para mostrar la lista de tareas -->
            <div class="card">
                <div class="card-header">
                    <h2>Lista de Tareas</h2>
                </div>
                <div class="card-body gridview-container">
                    <asp:GridView ID="gvTareas" runat="server" 
                        CssClass="table table-striped table-hover" 
                        AutoGenerateColumns="False"
                        DataKeyNames="id"
                        OnSelectedIndexChanged="gvTareas_SelectedIndexChanged"
                        EmptyDataText="No hay tareas para mostrar">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-Width="50px" />
                            <asp:BoundField DataField="nombre" HeaderText="Título" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                            <asp:TemplateField HeaderText="Estado" ItemStyle-Width="120px">
                                <ItemTemplate>
                                    <span class='<%# Convert.ToBoolean(Eval("completada")) ? "badge badge-completada" : "badge badge-pendiente" %>'>
                                        <%# Convert.ToBoolean(Eval("completada")) ? "Completada" : "Pendiente" %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" 
                                        CommandName="Select" CssClass="acciones-btn btn-azul" CausesValidation="false" />
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="acciones-btn btn-rojo" 
                                        OnClick="btnEliminar_Click"
                                        CommandArgument='<%# Eval("id") %>'
                                        OnClientClick="return confirm('¿Está seguro de eliminar esta tarea?');" CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                        <HeaderStyle BackColor="#667eea" ForeColor="White" Font-Bold="true" />
                    </asp:GridView>
                </div>
            </div>
            
            <!-- Pie de página -->
            <div class="text-center mt-4">
                <p class="text-white">
                    Desarrollado por: Carlos Saul Arenas Maciel | 2025
                </p>
            </div>
        </div>
    </form>
    
    <!-- Scripts de Bootstrap -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
