# input-action-controller

Forma modular de crear un controller para unity.

La idea es que en lugar de tener un componente Controller que abarque todo, tener un componente para cada acción que pueda realizar el jugador. 

## Cómo se usa

1. Crear un componente que herede de InputAction.
2. Asignar el componente a nuestro gameObject del player
3. En los campos del componente, asignar los nombres de los inputs que va a esperar recibir (Deben estar definidos en los axis del Input Manager).
4. Dentro del componente acceder a los axis de la siguiente manera: `_inputs["Nombre del axis"]`
