import { createRouter, createWebHistory } from "vue-router";
import Home from "../views/Home.vue";
import CRMAccounts from "../views/CRMAccounts.vue";
import Contracts from "../views/Contracts.vue";
import Members from "../views/Members.vue";
import Totals from "../views/Totals.vue";
import Remittance from '../views/Remittance.vue';
import UserManagement from '../views/UserManagement.vue';
import Forecast from '../views/Forecast.vue';

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/CRMAccounts",
    name: "CRMAccounts",
    component: CRMAccounts,
  },
  {
    path: "/Contracts",
    name: "Contracts",
    component: Contracts,
  },
  {
    path: "/Members",
    name: "Members",
    component: Members,
  },
  {
    path: "/Totals",
    name: "Totals",
    component: Totals,
  },
  {
    path:"/Remittance",
    name:"Remittance",
    component: Remittance
  },
  {
    path:"/Forecast",
    name:"Forecast",
    component: Forecast
  },
  {
    path:"/UserManagement",
    name:"UserManagement",
    component: UserManagement
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
