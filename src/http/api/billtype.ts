/*

 作者: chenghao
 Date: 2021/5/10
 Time: 15:48
 功能: TypeScript脚本

 */
// import axios from 'axios'
import axios from '@/http/http.ts';
import { IResult , IBillTypesListResult } from '@/types/index.ts'

const prefix = '/billtypes';

export function getlist ( isout : boolean , isrefresh : boolean = true ) {
    return axios.get<IBillTypesListResult>( `${ prefix }/getallbilltype/${ isout }/${ isrefresh }` );
}

export function add ( isout : boolean , typename : string = '' ) {
    return axios.post<IResult>( `${ prefix }` , { isout , typename } );
}


