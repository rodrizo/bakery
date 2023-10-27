<template>
    <v-dialog v-model="dialogVisible" transition="dialog-top-transition" max-width="500px">
      <v-card>
        <div class="text-center bg-green">
          <p class="text-h4 mt-2 mb-2">✔</p>
        </div>
        <v-card-text>
          <p class="text-center text-h6">
            <strong>{{ successMessage }}</strong>
          </p>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn variant="tonal" @click="closeDialog">OK</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </template>
  
  <script>
  export default {
    data() {
      return {
        dialogVisible: false,
        successMessage: ''
      };
    },
    methods: {
      openDialog(successMessage) {
        this.dialogVisible = true;
        this.successMessage = successMessage;
      },
      closeDialog() {
        this.dialogVisible = false;
        this.successMessage = '';
      }
    }
  };
  </script>
  