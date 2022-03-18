/*

 作者: chenghao
 Date: 2021/4/27
 Time: 16:55
 功能: TypeScript脚本

 */

import service from '@/http/http.ts';
import { HttpResponse , IToken } from '@/types/index.ts'

const prefix = '/users';


export function add ( registerUser ) : Promise<HttpResponse<string>> {
    return service.post( `${ prefix }` , registerUser );
}

// export function login ( mobile : string , password : string ) {
//     return service.post<ILoginResult>( `${ prefix }/login` , { mobile : mobile , password : password } );
// }

export function login ( mobile : string , password : string ) : Promise<HttpResponse<IToken>> {
    return service.post( `${ prefix }/login` , { mobile : mobile , password : password } );
}

/**
 * 修改头像
 * @param avatar
 */
// export function updateavatar ( avatar : string ) {
//     // return axios.post( `${ prefix }/updateavatar` , avatar );
//
//     var params = new URLSearchParams()
//     params.append( "avatar" , avatar )
//
//     return service.post<IResult>( `${ prefix }/updateavatar` , params );
// }

export function updateavatar ( avatar : string ) : Promise<HttpResponse<string>> {
    // return axios.post( `${ prefix }/updateavatar` , avatar );

    var params = new URLSearchParams()
    params.append( "avatar" , avatar )

    return service.post( `${ prefix }/updateavatar` , params );
}

/**
 * 修改密码
 * @param password
 */
export function updatepassword ( password : string ) : Promise<HttpResponse<string>> {
    var params = new URLSearchParams()
    params.append( "password" , password )

    return service.post( `${ prefix }/updatepassword` , params );
}

export function getuser () {
    return service.get( `${ prefix }` );
}


