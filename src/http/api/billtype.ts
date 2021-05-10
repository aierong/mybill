/*

 作者: chenghao
 Date: 2021/5/10
 Time: 15:48
 功能: TypeScript脚本

 */
import axios from 'axios'

const prefix = '/billtypes';

export function getlist ( isout : boolean , isrefresh : boolean = true ) {
    return axios.get( `${ prefix }/getallbilltype/${ isout }/${ isrefresh }` );
}
