// Composables
import { createRouter, createWebHistory } from 'vue-router'
import Default from '../layouts/default/Default.vue'
import Index from '../components/pedidos/Index.vue'
import Create from '../components/pedidos/Create.vue'

const routes = [
  {
    path: "/",
    name: "default",
    component: Default,
    meta: { auth: false }
  },
  {
    path: "/sucursal/pedidos/:id",
    name: "pedidos",
    component: Index,
    meta: { auth: false }
  },
  {
    path: "/pedidos/add/:id",
    name: "pedidosAdd",
    component: Create,
    meta: { auth: false }
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

export default router
