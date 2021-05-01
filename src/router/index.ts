import { createRouter , createWebHistory , RouteRecordRaw } from 'vue-router'

import Home from '@views/Home.vue'

const routes : Array<RouteRecordRaw> = [
    {
        path : '/' ,
        name : 'Home' ,
        component : Home ,
        children : [
            {
                path : '' ,
                redirect : '/bill'
            } ,
            {
                path : '/bill' ,
                name : 'bill' ,
                component : () => import('@views/Bill.vue') ,
                // meta : {
                //     //
                //     tabbarindex : 0
                // }

            } ,
            {
                path : '/stat' ,
                name : 'stat' ,
                component : () => import('@views/Stat.vue') ,
                // meta : {
                //     //
                //     tabbarindex : 1
                // }
            } ,
            {
                path : '/me' ,
                name : 'me' ,
                component : () => import('@views/Me.vue') ,
                // meta : {
                //     //
                //     tabbarindex : 2
                // }
            } ,
        ]
    } ,
    {
        path : '/login' ,
        name : 'login' ,
        component : () => import( '@/views/Login.vue') ,
        props : ( route ) => ( {
            returnmobile : route.query.mobile ,
        } ) ,
        meta : {
            //不需要登录验证
            requiresAuth : false
        }
    } ,
    {
        path : '/register' ,
        name : 'register' ,
        component : () => import( '@/views/Register.vue') ,
        meta : {
            //不需要登录验证
            requiresAuth : false
        }
    } ,

]

const router = createRouter( {
    history : createWebHistory( process.env.BASE_URL ) ,
    routes
} )

export default router


