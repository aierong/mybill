/*

 作者: chenghao
 Date: 2021/5/2
 Time: 18:19
 功能: TypeScript脚本

 */
import { IState } from '@store/user/types'

export default {

    //getters实际就是一个计算属性

    //得手机号码
    usermobile : ( state : IState ) => {
        if ( state.loginusermobile ) {
            return state.loginusermobile;
        }

        return "";
    } ,

}
