/*

 作者: chenghao
 Date: 2021/5/4
 Time: 11:42
 功能: TypeScript脚本

 */

import * as types from '@store/mutations/mutation-types'

import { IState } from '@store/types'

export const usermutations = {

    //修改用户信息
    [ types.updateloginuser ] ( state : IState , mobile : string ) {
        state.loginusermobile = mobile;
    } ,

    //清空
    [ types.clearloginuser ] ( state : IState ) {
        state.loginusermobile = ''
    } ,

}
