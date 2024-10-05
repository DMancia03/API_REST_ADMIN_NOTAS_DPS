# API_REST_ADMIN_NOTAS_DPS
REST API desarrollada con C# y encapsulada en un contenedor, para poder administrar notas de usuarios almacenadas en una base de datos en SQL Server.

## Consideraciones
1. Configuración de la base de datos
2. Configuración de la cadena de conexión
3. Configuración de visual studio (solo en local)

## Pasos para ejecutar contenedor en local
1. Iniciar proyecto en visual studio
2. Ejecutar proyecto utilizando docker

## Pasos para desplegar contenedor en producción
1. Generar imagen con docker
2. Desplegar a imagen

## Pruebas
``
//Registrar usuario
{host}/Usuarios/signup
//Iniciar sesion a un usuario
{host}/Usuarios/login
//Ver o crear etiquetas
{host}/Etiquetas
//Ver etiquetas del usuario
{host}/Etiquetas/id_usuario/{IdUsuario}
//Ver o crear notas
{host}/Notas
//Ver notas del usuario
{host}/Notas/usuario/{IdUsuario}
``

## Integrantes
- Yensy Alejandra Cruz Barahona | CB121442
- Diego Fernando Mancía Hernández | MH212532
- Johan Anthony Menjivar Girón | MG182330
- Javier Ernesto Pérez Joaquín | PJ211152
- Alinson Javier Meléndez Torres | MT191530
