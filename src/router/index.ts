import { createRouter , createWebHistory , RouteRecordRaw } from 'vue-router'
import Home from '../views/Home.vue'

const routes : Array<RouteRecordRaw> = [
    {
        path : '/' ,
        name : 'Home' ,
        component : Home
    } ,
    {
        path : '/login' ,
        name : 'login' ,
        component : () => import( '@/views/Login.vue') ,
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
