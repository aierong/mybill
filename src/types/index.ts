/*

 作者: chenghao
 Date: 2021/5/20
 Time: 11:18
 功能: TypeScript脚本

 */

import { IBillType } from "@comp/types";

export interface IBillObj {
    ids : number;

    mobile : string;
    billtypeid : number;
    typename : string;
    avatar : string;
    isout : boolean;
    moneys : number;
    moneydate : string;

    moneyyear : number;
    moneymonth : number;
    moneyday : number;
    memo : string;
    sources : string;
    adddate : string;
    updatedate : string;
    deletedate : string;
    delmark : string;

}

/**
 * 查询类型
 */
export type QueryType = "all" | "out" | "in";

export type querymode = "money" | "date";

export interface IResult {
    Code : number;
    Message : string;
    Success : boolean;
    TimestampUtc : number;
    Timestamp : number;
}

// 定义接口继承
export interface ILoginResult extends IResult {
    Result? : {
        token : string;
        mobile : string;
    };
}

// 定义接口继承
export interface IBillTypesListResult extends IResult {
    Result : IBillType[];
}
