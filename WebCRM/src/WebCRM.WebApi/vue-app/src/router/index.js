import { createRouter, createWebHistory } from "vue-router";
import Home from "../views/Home.vue";
import CRMAccounts from "../views/CRMAccounts.vue";
import Contracts from "../views/Contracts.vue";
import Members from "../views/Members.vue";
import Totals from "../views/Totals.vue";

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
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
