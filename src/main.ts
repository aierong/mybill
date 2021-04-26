import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'

//导入vant
import Vant from 'vant';
import 'vant/lib/index.css';

const app = createApp( App )
//引入vant
app.use( Vant )
app.use( store ).use( router ).mount( '#app' )
