import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { store , key } from '@store/index'

// import axios from '@https/http.ts';
import '@/http/http.ts';

//导入vant
import Vant from 'vant';
import 'vant/lib/index.css';

const app = createApp( App )
//引入vant
app.use( Vant )
app.use( store , key ).use( router ).mount( '#app' )
