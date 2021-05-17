import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { store , key } from '@store/index'

// 这里不要引用，使用时再引用
// import axios from '@https/http.ts';
// import '@/http/http.ts';

//导入vant
import Vant from 'vant';
import 'vant/lib/index.css';

import components from "@comp/globalcomponent";

import common from "@plugins/common";

const app = createApp( App )
//引入vant
app.use( Vant )

//循环注册全局组件
Object.keys( components ).forEach( ( key ) => {

    //注册全局组件
    app.component( `${ key }` , components[ key ] )
} )

app.use( common )

// 注入key
app.use( store , key ).use( router ).mount( '#app' )


