// Composables
import { createRouter, createWebHistory } from 'vue-router'
import Default from '../layouts/default/Default.vue'
import Home from '../views/Home.vue'

const routes = [
  {
    path: "/",
    name: "default",
    component: Default,
    meta: { auth: false }
  },
  {
    path: "/",
    name: "home",
    component: Home
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

export default router
