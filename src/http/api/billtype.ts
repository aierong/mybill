/*

 作者: chenghao
 Date: 2021/5/10
 Time: 15:48
 功能: TypeScript脚本

 */
// import axios from 'axios'
import axios from '@/http/http.ts';

import { HttpResponse , IAxiosResult } from '@/types/index.ts'
import { IBillType } from "@comp/types";

const prefix = '/billtypes';

export function getlist ( isout : boolean , isrefresh : boolean = true ) : Promise<HttpResponse<IBillType[]>> {
    return axios.get( `${ prefix }/getallbilltype/${ isout }/${ isrefresh }` );
}

export function add ( isout : boolean , typename : string = '' ) : Promise<HttpResponse<string>> {
    return axios.post( `${ prefix }` , { isout , typename } );
}

// export function getlistnew ( isout : boolean , isrefresh : boolean = true ) {
//     return axios.get<IAxiosResult<IBillType[]>>( `${ prefix }/getallbilltype/${ isout }/${ isrefresh }` );
// }


