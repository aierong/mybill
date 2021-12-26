/*

 作者: chenghao
 Date: 2021/4/27
 Time: 16:55
 功能: TypeScript脚本

 */

// import axios from 'axios'
import axios from '@/http/http.ts';
import { IResult , ILoginResult , IAxiosResult } from '@/types/index.ts'

const prefix = '/users';

export function add ( registerUser ) {
    return axios.post<IResult>( `${ prefix }` , registerUser );
}

export function login ( mobile : string , password : string ) {
    return axios.post<ILoginResult>( `${ prefix }/login` , { mobile : mobile , password : password } );
}

/**
 * 修改头像
 * @param avatar
 */
export function updateavatar ( avatar : string ) {
    // return axios.post( `${ prefix }/updateavatar` , avatar );

    var params = new URLSearchParams()
    params.append( "avatar" , avatar )

    return axios.post<IResult>( `${ prefix }/updateavatar` , params );
}

/**
 * 修改密码
 * @param password
 */
export function updatepassword ( password : string ) {
    var params = new URLSearchParams()
    params.append( "password" , password )

    return axios.post<IResult>( `${ prefix }/updatepassword` , params );
}

export function getuser () {
    return axios.get( `${ prefix }` );
}


