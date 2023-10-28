/**
 * plugins/vuetify.js
 *
 * Framework documentation: https://vuetifyjs.com`
 */

// Styles
import '@mdi/font/css/materialdesignicons.css'
import 'vuetify/styles'
// Composables
import { createVuetify } from 'vuetify'
import { VDataTable } from 'vuetify/labs/VDataTable'

const defaultTheme = localStorage.getItem('theme') || 'light';
console.log(defaultTheme);

// https://vuetifyjs.com/en/introduction/why-vuetify/#feature-guides
export default createVuetify({
  theme: {
    themes: {
      light: {
        colors: {
          primary: '#1867C0',
          secondary: '#5CBBF6',
        },
      },
    },
  },
  components: {
    VDataTable,
  },
})
