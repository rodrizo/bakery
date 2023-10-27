<template>
  <v-app>
    <DefaultBar />
    <Home />
  </v-app>
</template>

<script setup>
  import DefaultBar from './AppBar.vue'
  import DefaultView from './View.vue'
  import Home from '../../views/Home.vue'
</script>
