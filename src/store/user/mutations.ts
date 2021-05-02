/*

 作者: chenghao
 Date: 2021/5/2
 Time: 17:14
 功能: TypeScript脚本

 */

import * as types from '@store/user/mutation-types'

import { IState } from '@store/user/types'

export default {

    //修改用户信息
    [ types.updateloginuser ] ( state : IState , mobile : string ) {
        state.loginusermobile = mobile;
    } ,

    //清空
    [ types.clearloginuser ] ( state : IState ) {
        state.loginusermobile = ''
    } ,

}
