import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { store , key } from '@store/index'

//导入vant
import Vant from 'vant';
import 'vant/lib/index.css';

import components from "@plugins/components";

import common from "@plugins/common";

const app = createApp( App )
//引入vant
app.use( Vant )

//注册全局组件
app.use( components )
//注册全局方法
app.use( common )

// 注入key
app.use( store , key ).use( router ).mount( '#app' )


