/*

 作者: chenghao
 Date: 2021/5/20
 Time: 11:18
 功能: TypeScript脚本

 */

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

// 泛型的
export interface IAxiosResult<T> {
    // Code : number;
    Message : string;
    Success : boolean;
    TimestampUtc : number;
    Timestamp : number;
    //Result是返回数据,这里定义为泛型
    Result : T;
}

// 接口响应通过格式
export interface HttpResponse<T> {
    status : number
    statusText : string
    data : IAxiosResult<T>
}

export interface IToken {
    mobile : string
    token : string
}


