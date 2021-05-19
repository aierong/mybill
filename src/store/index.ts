import { createStore , Store } from 'vuex'
import createPersistedState from 'vuex-persistedstate'
import * as Cookies from 'js-cookie'

import * as constant from '@common/constant.ts'

import { InjectionKey } from "vue";
import { IState } from "@store/types";

import { userstate } from '@store/state/user'
import { pagedatastate } from '@store/state/pagedata'

import { usergetters } from '@store/getters/user'
import { pagedatagetters } from '@store/getters/pagedata'

import { usermutations } from '@store/mutations/user'
import { pagedatamutations } from '@store/mutations/pagedata'

// define injection key
export const key : InjectionKey<Store<IState>> = Symbol()

// 解构出来，构成state
const state : IState = {
    ...userstate ,
    ...pagedatastate ,
}

// 解构出来，构成getters
const getters = {
    ...usergetters ,
    ...pagedatagetters ,
}

// 解构出来，构成mutations
const mutations = {
    ...usermutations ,
    ...pagedatamutations ,
};

const vuexLoginPersisted = createPersistedState( {
    //key是给持久化状态起个名字，默认:vuex
    key : constant.PersistedName.LoginUserMobile ,

    // window.sessionStorage 是存储在会话
    // window.localStorage   是本地存储
    // storage : window.localStorage ,
    storage : {
        getItem : key => Cookies.get( key ) ,

        setItem : ( key , value ) => Cookies.set( key , value , {
            expires : constant.CookieExpires
        } ) ,

        removeItem : key => Cookies.remove( key ) ,
    } ,

    //reducer是设置要持久化的变量,不设置就是默认是全部变量
    reducer ( val : any ) {
        return {
            // 只储存state中的loginusermobile
            loginusermobile : val.loginusermobile ,

        }
    }
} );

const vuexPersisted = createPersistedState( {
    //key是给持久化状态起个名字，默认:vuex
    key : 'mybillvuex' ,

    storage : window.sessionStorage ,

    //reducer是设置要持久化的变量,不设置就是默认是全部变量
    //这里设置:count 和 myname ,所以这2个会持久化
    //其它的就不会持久化,页面刷新会丢失值
    reducer ( val : any ) {
        return {
            //只储存
            AddPageData : val.AddPageData ,

        }
    }
} );

export const store = createStore<IState>( {
    state ,
    getters ,

    mutations ,
    // actions
    plugins : [
        vuexLoginPersisted ,
        vuexPersisted ,
    ] ,
} )
