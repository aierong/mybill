/*

 作者: chenghao
 Date: 2021/4/27
 Time: 16:55
 功能: TypeScript脚本

 */

import axios from 'axios'

const prefix = '/users';

export function add ( registerUser ) {
    return axios.post( `${ prefix }` , registerUser );
}
