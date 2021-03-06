import { createRouter , createWebHistory , RouteRecordRaw } from 'vue-router'

//可以通过扩展 RouteMeta 接口来定义meta字段： 这样就可以在代码中有提示了
declare module 'vue-router' {
    interface RouteMeta {
        // 是可选的
        requiresAuth? : boolean
    }
}

import * as Cookies from 'js-cookie'

import Home from '@views/Home.vue'

import * as constant from '@common/constant'

import { store } from '@/store'
import * as UserMutationType from '@store/mutations/mutation-types.ts'

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
            {
                path : '/datalist' ,
                name : 'datalist' ,
                component : () => import('@views/DataList.vue') ,

                // props : ( route ) => ( {
                //     queryyear : ( route.query.year != null && route.query.year != '' ) ? parseInt( route.query.year.toString() ) : 0 ,
                //     querymonth : ( route.query.month != null && route.query.month != '' ) ? parseInt( route.query.month.toString() ) : 0 ,
                // } ) ,
            } ,
            {
                path : '/detail' ,
                name : 'detail' ,
                component : () => import('@views/Detail.vue') ,

                props : ( route ) => ( {
                    queryid : ( route.query.ids != null && route.query.ids != '' ) ? parseInt( route.query.ids.toString() ) : 0 ,
                } ) ,

                //路由独享的守卫
                beforeEnter : ( to , from ) => {

                    // console.log( "beforeEnter 路由独享守卫,进入之前to:" , to );
                    // console.log( "beforeEnter 路由独享守卫,进入之前from:" , from , from.path );

                    // const store = useStore( key )
                    if ( from != null && from.path != null ) {
                        // 当前页面手动刷新,from的
                        if ( from.path != '/' ) {
                            store.commit( UserMutationType.updatedetailpagedata , from.path );
                        }
                    }
                } ,
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

    if ( to.meta != null && to.meta.requiresAuth != null && !to.meta.requiresAuth ) {
        // 没有问题,继续运行
        // return;
    }
    else {
        let isLogin : boolean = false;

        // token取回来
        let loginusertoken = localStorage.getItem( constant.tokenname );
        // console.log( 'beforeEach token' , loginusertoken )

        // let data : string | null = localStorage.getItem( constant.PersistedName.LoginUserMobile );
        //是存的一个对象,这里我们取对象
        let data = Cookies.getJSON( constant.PersistedName.LoginUserMobile );

        // console.log( 'beforeEach data' , data )

        if ( data && loginusertoken && data.loginusermobile ) {

            // 这里判断了Cookies中的用户数据和token都存在，才可以

            isLogin = true;

        }

        // console.log( 'beforeEach isLogin' , isLogin )

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


