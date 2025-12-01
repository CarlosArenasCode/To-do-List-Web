-- Script SQL para crear la base de datos y tabla del To-Do List
-- Base de datos MySQL para la aplicación web ASP.NET

-- Crear la base de datos (si no existe)
CREATE DATABASE IF NOT EXISTS todolist
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci;

-- Usar la base de datos
USE todolist;

-- Crear la tabla de tareas
CREATE TABLE IF NOT EXISTS tareas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Titulo VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500) NOT NULL,
    FechaLimite DATE NOT NULL,
    Completada BOOLEAN NOT NULL DEFAULT FALSE,
    FechaCreacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FechaModificacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX idx_completada (Completada),
    INDEX idx_fecha_limite (FechaLimite)
) ENGINE=InnoDB;

-- Insertar datos de ejemplo (opcional)
INSERT INTO tareas (Titulo, Descripcion, FechaLimite, Completada) VALUES
('Comprar alimentos', 'Comprar frutas, verduras y productos básicos para la semana', '2025-12-01', 0),
('Estudiar ASP.NET', 'Repasar conceptos de Web Forms y controles de servidor', '2025-11-30', 0),
('Hacer ejercicio', 'Rutina de cardio y pesas en el gimnasio', '2025-11-29', 1),
('Reunión de equipo', 'Presentar avance del proyecto To-Do List', '2025-12-02', 0),
('Backup de archivos', 'Hacer respaldo de documentos importantes en la nube', '2025-11-28', 1);

-- Ver las tareas creadas
SELECT * FROM tareas ORDER BY FechaLimite;
