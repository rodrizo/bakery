<template>
  <div>
    <v-container class="mt-10">
      <v-card-title class="text-center mt-4"><strong>Listado de sucursales</strong></v-card-title>
      <v-btn class="mt-2 ml-4 mb-4" color="teal-darken-2" prepend-icon="mdi-plus-thick" rounded="lg" to="/patients/add"><strong>Nuevo</strong></v-btn>
      <v-text-field
          v-model="search"
          prepend-inner-icon="mdi-magnify"
          label="Buscar"
          single-line
          hide-details
          variant="outlined"
      ></v-text-field>
      <v-data-table
        :headers="headers"
        :items="data"
        :items-per-page="20"
        :search="search"
      >
        <template #item.options="{ item }">
          <v-btn variant="text" :to="`/sucursal/pedidos/${item.sucursalId}`">
            <v-tooltip
              activator="parent"
              location="end"
            >
              Ver Pedidos
            </v-tooltip>
            <v-icon>mdi-list-box</v-icon>
          </v-btn>
        </template>
      </v-data-table>
    </v-container>

  </div>
</template>

<script>
export default {
  data() {
    return {
      search: '',
      headers: [
        { align: 'start', key: 'sucursalId', title: 'Sucursal'},
        { title: 'Nombre', key: 'nombre' },
        { title: 'Dirección', key: 'direccion' },
        { title: 'Número de Teléfono', key: 'numeroTelefono' },
        { title: 'Gerente de Sucursal', key: 'gerenteSucursal' },
        { title: 'Horario de Operación', key: 'horarioOperacion' },
        { key: 'options', title: 'Opciones', sortable: false},
      ],
      data: [],
    };
  },
  mounted() {
    // Realiza una solicitud a la API para obtener los datos
    fetch('http://localhost:5191/api/sucursal')
      .then(response => response.json())
      .then(data => {
        this.data = data; // Asigna los datos a la propiedad 'data' para llenar la tabla
      })
      .catch(error => {
        console.error(error);
      });
  },
};
</script>
