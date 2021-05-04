import { createRouter , createWebHistory , RouteRecordRaw } from 'vue-router'

import Home from '@views/Home.vue'

import * as constant from '@common/constant'

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

            } ,
            {
                path : '/stat' ,
                name : 'stat' ,
                component : () => import('@views/Stat.vue') ,

            } ,
            {
                path : '/me' ,
                name : 'me' ,
                component : () => import('@views/Me.vue') ,

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

router.beforeEach( ( to , from ) => {
    if ( to.meta != null
        && to.meta.requiresAuth != null
        && !to.meta.requiresAuth ) {
        // 没有问题,继续运行
        // return;
    }
    else {
        let isLogin : boolean = false;

        // token取回来
        let loginusertoken = localStorage.getItem( constant.tokenname );
        // console.log( 'beforeEach token' , loginusertoken )

        let data : string | null = localStorage.getItem( constant.PersistedName.LoginUserMobile );
        // console.log( 'beforeEach data' , data )
        if ( data && loginusertoken ) {
            var obj : any = JSON.parse( data );

            // console.log( 'beforeEach data obj' , obj )

            // 这里判断了Cookies中的用户数据和token都存在，才可以
            if ( obj.loginusermobile ) {
                isLogin = true;
            }
        }

        console.log( 'beforeEach isLogin' , isLogin )

        // 没有登录,要处理一下
        if ( !isLogin ) {
            // 重新转向另一个页面
            return {
                name : 'login' ,
                query : {
                    redirectTo : to.fullPath ,
                } ,
            }
        }
    }
} )

export default router


