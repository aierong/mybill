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

/**
 * 修改头像
 * @param avatar
 */
export function updateavatar ( avatar : string ) {
    return axios.post( `${ prefix }/updateavatar/${ avatar }` );
}

/**
 * 修改密码
 * @param password
 */
export function updatepassword ( password : string ) {
    return axios.post( `${ prefix }/updatepassword/${ password }` );
}

export function getuser () {
    return axios.get( `${ prefix }` );
}


