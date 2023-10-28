<template>
    <div class="mb-6">
        
        <v-breadcrumbs divider="/">
            <template v-slot:divider>
                <v-icon icon="mdi-chevron-right"></v-icon>
            </template>
            
            <v-breadcrumbs-item :to="'/sucursal/pedidos/' + $route.params.id">
                Pedidos
            </v-breadcrumbs-item>
            <v-breadcrumbs-item>
                > Crear Pedido
            </v-breadcrumbs-item>
        </v-breadcrumbs>

        <v-card-title class="text-center mt-4"><strong>Crear item del pedido</strong></v-card-title>

        <v-form ref="form" v-model="valid" lazy-validation validate-on="submit" 
        @submit.prevent="submitForm">
            <v-card>
                <v-container>
                    <v-select
                        v-model="form.panId"
                        :items="panes"
                        label="Pan"
                        variant="underlined"
                        item-value="panId"
                        item-title="nombre"
                        prepend-icon="mdi-gavel"
                    ></v-select>
                </v-container>

                <v-container>
                        <v-text-field
                        v-model="form.cantidad"
                        label="Cantidad"
                        type="number"
                        variant="underlined"
                        prepend-icon="mdi-calendar-range"
                        required
                        :rules="[v => !!v || 'Comentarios es requerido']"
                        :value=form.cantidad
                        ></v-text-field>
                </v-container>

                <v-container>
                        <v-textarea
                        v-model="form.comentarios"
                        label="Comentarios"
                        type="text"
                        variant="underlined"
                        prepend-icon="mdi-calendar-range"
                        required
                        :rules="[v => !!v || 'Comentarios es requerido']"
                        :value=form.comentarios
                        ></v-textarea>
                </v-container>
                
                <ErrorDialog ref="errorDialog" />
                <SuccessDialog ref="successDialog" />

                <v-container>
                    <v-btn
                    color="success"
                    class="me-4 mt-4"
                    type="submit"
                    prepend-icon="mdi-plus"
                    rounded="lg"
                    >
                    Crear
                    </v-btn>
                    <v-btn
                    color="yellow-darken-1"
                    class="me-4 mt-4"
                    :to="`/patients/`"
                    prepend-icon="mdi-arrow-left"
                    rounded="lg"
                    >
                    Regresar
                    </v-btn>
                </v-container>
            </v-card>
        </v-form>



    </div>
</template>
  
<script>

  import ErrorDialog from '../dialogs/ErrorDialog.vue';
  import SuccessDialog from '../dialogs/SuccessDialog.vue';
  
export default {
    mounted() {
    //   this.getPanes()
    //   this.getSucursales()
    },
    data: () => ({
        valid: true,
        isSubmitting: false,
        form: {
            id: 0,
            panId: null,
            pedidoId: null,
            cantidad: 0,
            comentarios: '',
            isActive: null
        },
        panes: [], 
        selectedPan: null 
    }),
    beforeRouteEnter(to, from, next) {
        // Accede a los parámetros de la ruta y asigna el valor a la propiedad sucursalId
        next(vm => {
        vm.form.pedidoId = to.params.id;
        console.log(to.params.id)
        });
    },
    components: {
      ErrorDialog,
      SuccessDialog
    },
    created(){
        fetch(`http://localhost:5191/api/pan`)
            .then(response => response.json())
            .then(data => {
            console.log(data.result)
            this.panes = data.result; // Asigna los datos a la variable panes
            })
            .catch(error => {
            console.error('Error al obtener los datos de los panes:', error);
            });
    },
    methods: {
        showErrorDialog(errorMessage, successMessage) {
            if(errorMessage != null){
            this.$refs.errorDialog.openDialog(errorMessage);
            }else{
            this.$refs.successDialog.openDialog(successMessage);
            }
        },
        async submitForm() {
            try{
                this.isSubmitting = true;
                // const { valid } = this.$refs.form.validate()
                console.log(this.form)

                const response = await fetch(`http://localhost:5191/api/pedido/add/item/${this.$route.params.id}`,
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(this.form)
                })
                .then(response => {
                    if (response.ok) {
                        //triggering success dialog 
                        this.showErrorDialog(null, 'Item creado con éxito!');
                        this.isSubmitting = false;
                        setTimeout(() => (this.$router.push(`/pedido/${this.$route.params.id}/items`)), 1000)
                    } else {
                        return response.text()
                        .then(data => {
                            //triggering error dialog
                            this.showErrorDialog(data, null);
                            this.isSubmitting = false;
                        })
                    }
                })
            } catch(error){
                alert(error);
            }
        },
    }
};
</script>
  