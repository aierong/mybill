/*

 作者: chenghao
 Date: 2021/5/7
 Time: 15:10
 功能: TypeScript脚本

 */
import axios from 'axios'

const prefix = '/bills';

import { IBillDto } from '@comp/types'

export function getlist ( year : number , month : number , billtypeid : number ) {
    return axios.get( `${ prefix }/getlist/${ year }/${ month }/${ billtypeid }` );
}

export function add ( BillDto : IBillDto ) {
    return axios.post( `${ prefix }/add` , BillDto );
}

export function update ( BillDto : IBillDto ) {
    return axios.post( `${ prefix }/update` , BillDto );
}
