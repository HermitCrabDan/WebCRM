import { createRouter, createWebHistory } from "vue-router";
import Home from "../views/Home.vue";
import CRMAccounts from "../views/CRMAccounts.vue";
import Contracts from "../views/Contracts.vue";
import Members from "../views/Members.vue";
import Transactions from "../views/Transactions.vue";
import Totals from "../views/Totals.vue";
import About from "../views/About.vue";

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
    path: "/Transactions",
    name: "Transactions",
    component: Transactions,
  },
  {
    path: "/Totals",
    name: "Totals",
    component: Totals,
  },
  {
    path: "/about",
    name: "About",
    component: About,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
