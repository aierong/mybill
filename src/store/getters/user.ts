/*

 作者: chenghao
 Date: 2021/5/4
 Time: 11:41
 功能: TypeScript脚本

 */

import { IState } from '@store/types'

const usermobile = ( state : IState ) => {
    if ( state.loginusermobile ) {
        return state.loginusermobile;
    }

    return "";
};

export const usergetters = {
    usermobile
}
