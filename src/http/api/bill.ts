/*

 作者: chenghao
 Date: 2021/5/7
 Time: 15:10
 功能: TypeScript脚本

 */
import axios from 'axios'

const prefix = '/bills';

export function getlist ( year : number , month : number , billtypeid : number ) {
    return axios.get( `${ prefix }/getlist/${ year }/${ month }/${ billtypeid }` );
}
