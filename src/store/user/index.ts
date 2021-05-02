/*

 作者: chenghao
 Date: 2021/5/1
 Time: 7:56
 功能: TypeScript脚本

 */
import { InjectionKey } from 'vue'
import { createStore , Store } from 'vuex'

import { IState } from '@store/user/types'


import mutations from '@store//user/mutations'

const state : IState = {
    //登录用户帐号
    loginusermobile : '' ,

    //登录用户信息
    // loginuser : null ,

}

export default {
    state : state ,

    // getters : getters ,
    mutations : mutations ,

    //打开 命名空间
    namespaced : true
}
