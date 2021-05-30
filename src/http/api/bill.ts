/*

 作者: chenghao
 Date: 2021/5/7
 Time: 15:10
 功能: TypeScript脚本

 */
// import axios from 'axios'
import axios from '@/http/http.ts';

const prefix = '/bills';

import { IBillDto } from '@comp/types'

export function getlist ( year : number , month : number , billtypeid : number ) {
    return axios.get( `${ prefix }/getlist/${ year }/${ month }/${ billtypeid }` );
}

export function getdatalist ( year : number , month : number , isout : boolean , billtypeid : number , mode : string ) {
    return axios.get( `${ prefix }/getlist/${ year }/${ month }/${ isout }/${ billtypeid }/${ mode }` );
}

export function getstatmonthlist ( isout : boolean , monthnum : number ) {
    return axios.get( `${ prefix }/getstatmonthlist/${ isout }/${ monthnum }` );
}

export function getstatlist ( year : number , month : number , isout : boolean ) {
    return axios.get( `${ prefix }/getstatlist/${ year }/${ month }/${ isout }` );
}

export function gettopoutlist ( year : number , month : number , topnum : number ) {
    return axios.get( `${ prefix }/gettopoutlist/${ year }/${ month }/${ topnum }` );
}

export function add ( BillDto : IBillDto ) {
    return axios.post( `${ prefix }/add` , BillDto );
}

export function update ( BillDto : IBillDto ) {
    return axios.post( `${ prefix }/update` , BillDto );
}

export function get ( id : number ) {
    return axios.get( `${ prefix }/get/${ id }` );
}

export function deletebyid ( id : number ) {
    var params = new URLSearchParams()
    params.append( "id" , id.toString() )

    return axios.post( `${ prefix }/delete` , params );
}
