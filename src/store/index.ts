import { createStore , Store } from 'vuex'
import createPersistedState from 'vuex-persistedstate'

// import user from '@store/user'
import { InjectionKey } from "vue";
import { IState } from "@store/types";

import { userstate } from '@store/state/user'

import { usergetters } from '@store/getters/user'

import { usermutations } from '@store/mutations/user'

// define injection key
export const key : InjectionKey<Store<IState>> = Symbol()

// 解构出来，构成state
const state : IState = {
    ...userstate ,

}

// 解构出来，构成getters
const getters = {
    ...usergetters ,
}

// 解构出来，构成mutations
const mutations = {
    ...usermutations ,
};

export const store = createStore<IState>( {
    state ,
    getters ,

    mutations ,
    // actions
} )
