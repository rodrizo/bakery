<template>
    <div>
        <v-container class="mt-10">
        <v-card-title class="text-center mt-4 mb-4"><strong>Stock</strong></v-card-title>
        <!-- <v-btn class="mt-2 ml-4 mb-4" color="teal-darken-2" prepend-icon="mdi-plus-thick" rounded="lg" :to="`/pedidos/add/${this.$route.params.id}`"><strong>Nuevo</strong></v-btn> -->
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
            <!-- <v-btn icon="mdi-plus" variant="text" :to="`/pedido/add/item/${item.pedidoId}`"></v-btn> -->
            <v-btn icon="mdi-pencil" variant="text" @click.stop="openDialog(item)"></v-btn>
          </template>
        </v-data-table>
        <!-- START OF DIALOG -->
        <template>
            <v-row justify="center">
                <v-dialog
                v-model="dialog"
                persistent
                width="600"
                >
                <template v-slot:activator="{ props }">
                    <v-btn
                    color="primary"
                    v-bind="props"
                    >
                    Open Dialog
                    </v-btn>
                </template>
                <v-form @submit.prevent="updateStock(this.selectedItem.id)">
                    <v-card>
                        <v-card-title>
                        <span class="text-h5">Actualizar Stock de {{ this.selectedItem.pan }}</span>
                        </v-card-title>
                        <v-card-text>
                        <v-container>
                            <v-row>
                                <v-col cols="12">
                                    <v-text-field
                                        v-model="stockUpdate.notas"
                                        label="Notas"
                                        required
                                    ></v-text-field>
                                </v-col>
                                <v-col cols="12">
                                    <v-text-field
                                        v-model="stockUpdate.cantidad"
                                        label="Cantidad"
                                        type="number"
                                        required
                                    ></v-text-field>
                                </v-col>
                            </v-row>
                        </v-container>
                        </v-card-text>
                        <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn
                            color="blue-darken-1"
                            variant="text"
                            @click="dialog = false"
                        >
                            Close
                        </v-btn>
                        <v-btn
                            color="blue-darken-1"
                            variant="text"
                            type="submit"
                            @click="dialog = false"
                        >
                            Save
                        </v-btn>
                        </v-card-actions>
                    </v-card>
                </v-form>
                </v-dialog>
            </v-row>
        </template>
        <!-- END OF DIALOG -->
      </v-container>
  
    </div>
  </template>
  
  <script>
  export default {
    mounted() {
      this.getStock()
    },
    data() {
      return {
        search: '',
        dialog: false,
        nombreSucursal: '',
        headers: [
          { align: 'start', key: 'id', title: 'Id'},
          { title: 'Pan', key: 'pan' },
          { title: 'Notas', key: 'notas' },
          { title: 'Cantidad', key: 'cantidad' },
          { title: 'Fecha De Creacion', key: 'fechaCreacion' },
          { title: 'Fecha De Modificacion', key: 'fechaModificacion' },
          { key: 'options', title: 'Opciones', sortable: false},
        ],
        data: [],
        stockUpdate: {
            cantidad: 0,
            notas: ''
        },
        sucursales: [],
      };
    },
    
    computed: {
        nombreSucursal(){
            const sucursal = this.sucursales.find
            (s => s.sucursalId === Number.parseInt(this.$route.params.id));
            return sucursal ? sucursal.nombre : "No encontrada";
        }
    },
    methods:{
        openDialog(item) {
          this.selectedItem = item;
          this.dialog = true
          console.log(this.selectedItem)
        },
        async getStock(){
            // Realiza una solicitud a la API para obtener los datos
            fetch(`http://localhost:5191/api/stock`)
            .then(response => response.json())
            .then(data => {
                console.log(data)
            this.data = data; // Asigna los datos a la propiedad 'data' para llenar la tabla
            })
            .catch(error => {
            console.error(error);
            });
        },

        async updateStock(id){
            // console.log(id, '->', this.stockUpdate)
          await fetch("http://localhost:5191/api/stock?id="+id,
          {
              method: 'PUT',
              headers: {
                'Content-Type': 'application/json',
              },
              body: JSON.stringify(
                  {
                    'stockId': id,
                    'panId': 0,
                    'cantidad': this.stockUpdate.cantidad,
                    'notas': this.stockUpdate.notas,
                    'fechaCreacion': null,
                    'fechaModificacion': null,
                    'isActive': null
                  }
              )
          })
          .then(response => {
            this.getStock()
            // console.log(response.json(), 'response')
          })
          .catch(error => {
            alert(error)
          })
        //   window.location.href = `/patients/${this.$route.params.id}/details`
        },
    }
  };
  </script>
  