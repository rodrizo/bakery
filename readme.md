# Proyecto de Panadería - Desarrollo Web

## Resumen

Este informe presenta una descripción general del proyecto de desarrollo de una aplicación web para una panadería. La aplicación se compone de un backend basado en una API REST y un frontend desarrollado en Vue.js. El objetivo principal del proyecto es proporcionar una solución de gestión de panadería eficaz que permita a los usuarios realizar tareas como crear panes, gestionar ingredientes, administrar recetas, realizar pedidos y llevar un control del stock.

## Objetivos del Proyecto

Los objetivos clave de este proyecto son:

- **Gestión de Panes**: Permitir a la panadería crear y mantener un catálogo de panes disponibles, que incluye detalles como el nombre, precio unitario, descripción y tiempo de preparación.

- **Gestión de Ingredientes**: Ofrecer la capacidad de agregar, modificar y eliminar ingredientes que se utilizan en la elaboración de panes.

- **Recetas para Panes**: Habilitar la creación y asignación de recetas a los panes, especificando los ingredientes y cantidades requeridas.

- **Pedidos de Pan**: Permitir a los clientes realizar pedidos de pan, indicando la cantidad deseada y, opcionalmente, proporcionando comentarios.

- **Control de Stock**: Mantener un seguimiento en tiempo real del stock de ingredientes y panes disponibles para garantizar un flujo de producción eficiente.

- **Transacciones de Pedidos**: Registrar y gestionar las transacciones de pedidos para controlar la disponibilidad del stock y generar informes útiles en un futuro.

- **Frontend Vue.js**: Proporcionar una interfaz de usuario moderna y amigable desarrollada en Vue.js que permita a los usuarios realizar las tareas mencionadas de manera intuitiva.

## Tecnologías Utilizadas

- **Backend**: ASP.NET Core para desarrollar la API REST.
- **Frontend**: Vue.js y Vuetify para crear la interfaz de usuario.
- **Base de Datos**: Oracle Database para almacenar datos críticos del sistema y realizar lógicas de negocio requeridas mediante paquetes, procedimientos almacenados, etcétera.
- **Control de Versiones**: Git para gestionar el control de versiones del proyecto.

## Funcionalidades Clave

### Backend (API REST)

[API Endpoints](https://interstellar-crescent-177552.postman.co/workspace/Team-Workspace~4fb8d703-35b9-4fa3-80b9-bd795110bab1/collection/17019555-12e1c8fd-b76a-44c8-9a76-9c5fd5bdfcc8?action=share&creator=17019555)

- **Base de Datos**: Utilización de Oracle Database para el almacenamiento de datos.

### Frontend (Vue.js)

- **Vistas**:
  - Página de sucursales para poder realizar los pedidos a estas.
  - Página de realización de pedidos.
  - Página de control de stock.
  - Página para añadir items a un pedido.
  - Opciones de modificación para stock en caso de inexistencias.
  - Opciones de eliminar items de un pedido en caso de equivocación por parte del usuario.

- **Componentes**: Uso de componentes de Vuetify para crear una interfaz de usuario atractiva y funcional.

- **Enrutamiento** con Vue Router para la navegación entre páginas.
