<template>
  
  <v-app>
    <v-navigation-drawer
        v-model="open"
        location="left"
      >
        <template v-slot:prepend>
          <v-list-item
            lines="two"
            title="Panadería Walter White"
          ></v-list-item>
        </template>

        <v-divider></v-divider>

        <v-list density="compact" nav>
          <v-list-item prepend-icon="mdi mdi-storefront-outline" title="Sucursales" value="home" to="/"></v-list-item>
          <v-list-item prepend-icon="mdi mdi-inbox-multiple-outline" title="Stock" value="patients" to="/stock"></v-list-item>
          <v-list-item prepend-icon="mdi mdi-folder" title="Reportes" value="reports" to="/reports"></v-list-item>
          <v-list-group value="Configuracion">
            <template v-slot:activator="{props}">
              <v-list-item v-bind="props" prepend-icon="mdi mdi-cog" title="Configuracion"></v-list-item>
            </template>
            <v-list-item prepend-icon="mdi-account-group" title="Usuarios" to="/users"></v-list-item>
          </v-list-group>
        </v-list>
      </v-navigation-drawer>
    <v-app-bar title="Panadería Walter White"
>
<v-btn icon="mdi mdi-theme-light-dark" @click="toggleTheme"></v-btn>
<v-btn icon="mdi-power" @click="logOut"></v-btn>
<v-app-bar-nav-icon @click="open = !open" />
</v-app-bar>

    <v-main class="mt-3 ml-3 mr-3">
      <v-card>
        <router-view></router-view>
      </v-card>
    </v-main>
  </v-app>
</template>

<script>

import { ref} from "vue"
import { useTheme } from "vuetify/lib/framework.mjs";


export default {
  data: () => ({
    loading: true,
  }),
  setup () {
    const theme = useTheme()
    const open = ref(false)
    return {
      theme,
      open,
      toggleTheme: () => {
        localStorage.setItem('theme', theme.global.name.value = theme.global.current.value.dark ? 'light' : 'dark');
      },
    }
  },
  
}
</script>
