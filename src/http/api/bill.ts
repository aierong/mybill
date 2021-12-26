/*

 作者: chenghao
 Date: 2021/5/7
 Time: 15:10
 功能: TypeScript脚本

 */
// import axios from 'axios'
import axios from '@/http/http.ts';
import { IBillDto , IBillType } from '@comp/types'
import { IAxiosResult , IBillObj } from "@/types";

const prefix = '/bills';

export function getlist ( year : number , month : number , billtypeid : number ) {
    return axios.get( `${ prefix }/getlist/${ year }/${ month }/${ billtypeid }` );
}

export function getdatalist ( year : number , month : number , isout : boolean , billtypeid : number , mode : string ) {
    return axios.get( `${ prefix }/getlist/${ year }/${ month }/${ isout }/${ billtypeid }/${ mode }` );
}

export function getstatmonthlist ( year : number , month : number , isout : boolean , monthnum : number ) {
    return axios.get( `${ prefix }/getstatmonthlist/${ year }/${ month }/${ isout }/${ monthnum }` );
}

export function getstatdaylist ( year : number , month : number , isout : boolean ) {
    return axios.get( `${ prefix }/getstatdaylist/${ year }/${ month }/${ isout }` );
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

export function getdatalistnew ( year : number , month : number , isout : boolean , billtypeid : number , mode : string ) {
    console.log( 'getdatalistnew' )

    return axios.get<IAxiosResult<IBillObj[]>>( `${ prefix }/getlist/${ year }/${ month }/${ isout }/${ billtypeid }/${ mode }` );
}

// 下面这样写不行
// export function getdatalistnew2 ( year : number , month : number , isout : boolean , billtypeid : number , mode : string ) : Promise<IAxiosResult<IBillObj[]>> {
//     console.log( 'getdatalistnew2' )
//
//     return axios.get( `${ prefix }/getlist/${ year }/${ month }/${ isout }/${ billtypeid }/${ mode }` );
// }


