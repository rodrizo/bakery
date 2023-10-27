
<template>
    <div>
        
        <v-container class="mt-10">
        <v-card-title class="text-center mt-4"><strong>Listado de pedidos de la sucursal {{ nombreSucursal }}</strong></v-card-title>
        <v-btn class="mt-2 ml-4 mb-4" color="teal-darken-2" prepend-icon="mdi-plus-thick" rounded="lg" :to="`/pedidos/add/${this.$route.params.id}`"><strong>Nuevo</strong></v-btn>
        <v-data-table
          :headers="headers"
          :items="data"
          :items-per-page="20"
        >
          <template #item.options="{ item }">
            <v-btn icon="mdi-pencil" variant="text" @click.stop="deletePedido(item.pedidoId)"></v-btn>
          </template>
        </v-data-table>
      </v-container>
  
    </div>
  </template>
  
  <script>
  export default {
    mounted() {
      this.getPedidos(),
      this.getSucursales()
    },
    data() {
      return {
        nombreSucursal: '',
        headers: [
          { align: 'start', key: 'pedidoId', title: 'PedidoId'},
          { title: 'Fecha Pedido', key: 'fechaPedido' },
          { title: 'Ruta', key: 'ruta' },
          { title: 'Estado', key: 'estado' },
          { title: 'Comentarios', key: 'comentarios' },
          { title: 'Sucursal', key: 'sucursal' },
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
        async getPedidos(){
            // Realiza una solicitud a la API para obtener los datos
            fetch(`http://localhost:5191/api/pedido/getAll/${this.$route.params.id}`)
            .then(response => response.json())
            .then(data => {
                console.log(data)
            this.data = data; // Asigna los datos a la propiedad 'data' para llenar la tabla
            })
            .catch(error => {
            console.error(error);
            });
        },

        async getSucursales(){
            fetch(`http://localhost:5191/api/sucursal`)
            .then(response => response.json())
            .then(data => {
                console.log(data)
                this.sucursales = data; // Asigna los datos a la propiedad 'data' para llenar la tabla
            })
            .catch(error => {
                console.error(error);
            });
        },

        async deletePedido(id){
        await fetch("http://localhost:5191/api/pedido?id="+id,
        {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify(
                {
                    'pedidoId': id,
                    "fechaPedido": '1000-01-01',
                    'ruta': '',
                    'estado': '',
                    'comentarios': '',
                    'isActive': '0'
                }
            )
        })
        .then(response => {
          this.getPedidos()
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
  