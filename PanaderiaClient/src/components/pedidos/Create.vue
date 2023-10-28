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

        <v-card-title class="text-center mt-4"><strong>Crear pedido</strong></v-card-title>

        <v-form ref="form" v-model="valid" lazy-validation validate-on="submit" 
        @submit.prevent="submitForm">
            <v-card>

                
                <v-container>
                    <v-row>
                    <v-col cols="12" md="4" />
                    <v-col
                        cols="12"
                        md="4"
                    >  
                        <h4>Fecha Pedido</h4>
                        <VDatePicker v-model="form.fechaPedido" mode="dateTime" is24hr />
                    </v-col>
                    </v-row>
                </v-container>

                <v-container>
                    <v-select
                    v-model="form.ruta"
                    :items="itemsRuta"
                    label="Ruta"
                    variant="underlined"
                    item-value="id"
                    item-title="name"
                    prepend-icon="mdi-gavel"
                    ></v-select>
                </v-container>

                <v-container>
                    <v-select
                    v-model="form.estado"
                    :items="itemsEstado"
                    label="Estado"
                    variant="underlined"
                    item-value="id"
                    item-title="name"
                    prepend-icon="mdi-gavel"
                    ></v-select>
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
                    :to="`/sucursal/pedidos/${this.$route.params.id}`"
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
    data: () => ({
      valid: true,
      isSubmitting: false,
        form: {
            fechaPedido: '',
            ruta: '',
            estado: '',
            comentarios: '',
            sucursalId: '',
        },
        itemsRuta: [
            { id: 'Ruta 1', name: 'Ruta 1'},
            { id: 'Ruta 2', name: 'Ruta 2'},
            { id: 'Ruta 3', name: 'Ruta 3'},
            { id: 'Ruta 4', name: 'Ruta 4'},
            { id: 'Ruta 5', name: 'Ruta 5'},
        ],
        itemsEstado: [
            { id: 'Agendado', name: 'Agendado'},
            { id: 'Despachado', name: 'Despachado'},
            { id: 'En Progreso', name: 'En Progreso'},
            { id: 'Recibido', name: 'Recibido'},
            { id: 'Completado', name: 'Completado'},
        ],
    }),
    beforeRouteEnter(to, from, next) {
        // Accede a los parámetros de la ruta y asigna el valor a la propiedad sucursalId
        next(vm => {
        vm.form.sucursalId = to.params.id;
        console.log(to.params.id)
        });
    },
    components: {
      ErrorDialog,
      SuccessDialog
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

                const response = await fetch(`http://localhost:5191/api/pedido/add/${this.$route.params.id}`,
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(this.form)
                });
                if(response.status === 200){
                    //triggering success dialog 
                    this.showErrorDialog(null, '¡Pedido creado con éxito!');
                    this.isSubmitting = false;
                    setTimeout(() => (this.$router.push(`/sucursal/pedidos/${this.$route.params.id}`)), 1000)
                } else{
                    const errorData = await response.json();

                    //triggering error dialog
                    this.showErrorDialog(Object.values(errorData.errors).flat().join(', '), null);
                    this.isSubmitting = false;
                }
            } catch(error){
                alert(error);
            }
        },
    }
};
</script>
  