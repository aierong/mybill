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

export function login ( mobile : string , password : string ) {
    return axios.post( `${ prefix }/login` , { mobile : mobile , password : password } );
}

export function getuser () {
    return axios.get( `${ prefix }` );
}
