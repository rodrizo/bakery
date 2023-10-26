<!-- eslint-disable vue/multi-word-component-names -->
<template>
    <div>
      <v-card-title class="text-center mt-4"><strong>Listado de pacientes</strong></v-card-title>
      <v-btn class="mt-2 ml-4 mb-4" color="teal-darken-2" prepend-icon="mdi-plus-thick" rounded="lg" to="/patients/add"><strong>Nuevo</strong></v-btn>
  
      <v-container>
        <v-text-field
          v-model="search"
          prepend-inner-icon="mdi-magnify"
          label="Buscar"
          single-line
          hide-details
          variant="outlined"
        ></v-text-field>
        <v-data-table>
            :headers="headers"
            :items="panes"
            :search="search"
        </v-data-table>
      </v-container>
    </div>
  </template>
  <script>
  export default {
    mounted() {
      this.getPan()
    },
    data: () => ({
        search: '',
        headers: [
            { align: 'start', key: 'panId', title: 'PanId'},
            { key: 'nombre', title: 'Nombre' },
            { key: 'options', title: 'Opciones', sortable: false},
        ],
        dialog: false,
        panes: [
            {
                panId: '',
                nombre: '',
                precioUnitario: '',
                descripcion: '',
                tiempoPreparacion: '',
                isActive: ''
            }
        ],
        selectedItem: null
    }),
    methods: {
      async getPan() {
        await fetch('http://localhost:5191/api/pan')
          .then(response => response.json())
          .then(x => {
              console.log(x.result);

          this.panes = x.result;
        })
          .catch(error => {
            alert(error)
        })
      }
    }
  }
  </script>
  