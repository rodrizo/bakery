/**
 * main.js
 *
 * Bootstraps Vuetify and other plugins then mounts the App`
 */

// Components
import App from './App.vue'
import VCalendar from 'v-calendar';
import 'v-calendar/style.css';

// Use plugin with optional defaults
// Composables
import { createApp } from 'vue'

// Plugins
import { registerPlugins } from '@/plugins'

const app = createApp(App)

app.use(VCalendar, {})

registerPlugins(app)

app.mount('#app')
