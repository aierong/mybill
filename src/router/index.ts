import { createRouter , createWebHistory , RouteRecordRaw } from 'vue-router'
import * as Cookies from 'js-cookie'

import Home from '@views/Home.vue'

import * as constant from '@common/constant'

// import { useStore } from 'vuex'
// import { key } from '@store/index.ts'
import { store } from '@store/index'
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
                path : '/detail' ,
                name : 'detail' ,
                component : () => import('@views/Detail.vue') ,
                props : ( route ) => ( {
                    queryid : ( route.query.ids != null && route.query.ids != '' ) ? parseInt( route.query.ids.toString() ) : 0 ,
                } ) ,
                // meta : {
                //     //不需要显示tabbar
                //     notabbar : true
                // } ,
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
    // console.log( 'beforeEach to' , to )
    // console.log( 'beforeEach from' , from )

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

// router.afterEach( ( to , from ) => {
//     // 这里做一个判断,判断tabbar是否显示
//     if ( to != null && to.meta != null ) {
//         //先默认是显示
//         store.commit( UserMutationType.updatetabbarshow , true );
//
//         //取到路由中配置的meta
//         if ( to.meta.notabbar != null && to.meta.notabbar ) {
//             store.commit( UserMutationType.updatetabbarshow , false );
//         }
//     }
// } )

export default router


