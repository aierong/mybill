import { createStore , Store } from 'vuex'
import createPersistedState from 'vuex-persistedstate'

import user from '@store/user'
import { InjectionKey } from "vue";
import { IState } from "@store/user/types";

// define injection key
export const key : InjectionKey<Store<IState>> = Symbol()

export const store = createStore<IState>( {
    modules : {
        user : user ,
    } ,
} )
