// Composables
import { createRouter, createWebHistory } from 'vue-router'
import Default from '../layouts/default/Default.vue'
import Index from '../components/pedidos/Index.vue'
import Create from '../components/pedidos/Create.vue'
import CreateItem from '../components/pedidos/CreateItem.vue'
import PedidoItems from '../components/pedidos/PedidoItems.vue'
import Stock from '../components/stock/Index.vue'
import UpdateStock from '../components/stock/UpdateStock.vue'

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
  {
    path: "/pedido/add/item/:id",
    name: "itemAdd",
    component: CreateItem,
    meta: { auth: false }
  },
  {
    path: "/pedido/:id/items",
    name: "pedidoItems",
    component: PedidoItems,
    meta: { auth: false }
  },
  {
    path: "/stock",
    name: "stock",
    component: Stock,
    meta: { auth: false }
  },
  {
    path: "/stock/:id",
    name: "updateStock",
    component: UpdateStock,
    meta: { auth: false }
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

export default router
