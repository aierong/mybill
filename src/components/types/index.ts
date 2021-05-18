/*

 作者: chenghao
 Date: 2021/4/27
 Time: 14:14
 功能: TypeScript脚本

 */

export interface IAvatarObj {
    showdialog : boolean,
    avatar : string
}

export interface ISelectDateObj {
    year : number,
    month : number
}

export interface ISelectBillTypeObj {
    id : number,
    name : string,
    isout : boolean
}

export interface IBillType {
    ids : number,
    isout : boolean,
    issystemtype : boolean,
    typename : string,
    avatar : string,
}

export interface IBillDto {
    /**
     * 是添加
     */
    isadd : boolean,

    /**
     * 记录id
     */
    ids : number,

    /**
     * 类型id
     */
    billtypeid : number,
    isout : boolean,
    moneys : number,
    moneydate : string,
    memo : string,
}

export type colortype = 'in' | 'out' | 'no';

