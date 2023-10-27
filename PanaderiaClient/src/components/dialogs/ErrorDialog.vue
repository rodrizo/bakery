<template>
    <v-dialog v-model="dialogVisible" transition="dialog-top-transition" max-width="500px">
      <v-card>
        <div class="text-center bg-red-darken-4">
          <p class="text-h5 mt-2 mb-1">❗Error❗</p>
        </div>
        <v-card-text>
          <p class="text-h7">
            <strong>{{ errorMessage }}</strong>
          </p>
            <v-divider class="border-opacity-0 mb-6"></v-divider>
            <p class="text-subtitle-1">Por favor, intente nuevamente.</p>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn variant="tonal"  @click="closeDialog">Intentar de nuevo</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </template>
  
  <script>
  export default {
    data() {
      return {
        dialogVisible: false,
        errorMessage: ''
      };
    },
    methods: {
      openDialog(errorMessage) {
        this.dialogVisible = true;
        this.errorMessage = errorMessage;
      },
      closeDialog() {
        this.dialogVisible = false;
        this.errorMessage = '';
      }
    }
  };
  </script>
  