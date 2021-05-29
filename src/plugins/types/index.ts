/*

 作者: chenghao
 Date: 2021/5/15
 Time: 14:30
 功能: TypeScript脚本

 */

export interface IFormatMoney {
    ( num : number , digits : number ) : string;
}

export interface IFormatStatMoney {
    ( num : number , digits : number ) : string;
}

export interface IFormatPercent {
    ( num : number , digits : number ) : string;
}
