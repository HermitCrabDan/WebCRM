import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import axios from 'axios';
import VueAxios from 'vue-axios';
import WaveUI from 'wave-ui';
import 'wave-ui/dist/wave-ui.css';


const app = createApp(App);
app.use(store).use(router).use(VueAxios, axios);
new WaveUI(app,{});


app.mount("#app");
