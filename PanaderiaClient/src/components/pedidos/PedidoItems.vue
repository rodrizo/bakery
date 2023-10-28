
<template>
    <div>
        
        <v-container class="mt-10">
        <v-card-title class="text-center mt-4 mb-4"><strong>Items del pedido {{ this.$route.params.id }}</strong></v-card-title>
        <!-- <v-btn class="mt-2 ml-4 mb-4" color="teal-darken-2" prepend-icon="mdi-plus-thick" rounded="lg" :to="`/pedidos/add/${this.$route.params.id}`"><strong>Nuevo</strong></v-btn> -->
        <v-data-table
          :headers="headers"
          :items="data"
          :items-per-page="20"
        >
          <template #item.options="{ item }">
            <v-btn icon="mdi-delete" variant="text" @click.stop="deleteItem(item.id, item.panId)"></v-btn>
          </template>
        </v-data-table>
      </v-container>
  
    </div>
  </template>
  
  <script>
  export default {
    mounted() {
      this.getPedidoItems()
    },
    data() {
      return {
        nombreSucursal: '',
        headers: [
          { align: 'start', key: 'id', title: 'Id'},
          { title: 'PanId', key: 'panId' },
          { title: 'Pan', key: 'pan' },
          { title: 'Precio Unitario', key: 'precioUnitario' },
          { title: 'Sucursal', key: 'sucursal' },
          { title: 'Cantidad', key: 'cantidad' },
          { title: 'Comentarios', key: 'comentarios' },
          { title: 'Mañana', key: 'tomorrow' },
          { title: 'Resto', key: 'resto' },
          { title: 'Tarde', key: 'tarde' },
          { key: 'options', title: 'Opciones', sortable: false},
        ],
        data: [],
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
        async getPedidoItems(){
            // Realiza una solicitud a la API para obtener los datos
            fetch(`http://localhost:5191/api/pedido/item/${this.$route.params.id}`)
            .then(response => response.json())
            .then(data => {
                console.log(data)
            this.data = data; // Asigna los datos a la propiedad 'data' para llenar la tabla
            })
            .catch(error => {
            console.error(error);
            });
        },

        async deleteItem(id, panId){
          await fetch("http://localhost:5191/api/pedido/item?id="+id,
          {
              method: 'PUT',
              headers: {
                'Content-Type': 'application/json',
              },
              body: JSON.stringify(
                  {
                    'id': id,
                    'panId': panId,
                    'pedidoId': 0,
                    'cantidad': 0,
                    'comentarios': null,
                    'isActive': "0"
                  }
              )
          })
          .then(response => {
            this.getPedidoItems()
            // console.log(response.json(), 'response')
          })
          .catch(error => {
            alert(error)
          })
          // window.location.href = `/patients/${this.$route.params.id}/details`
        },
    }
  };
  </script>
  