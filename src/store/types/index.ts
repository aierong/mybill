/*

 作者: chenghao
 Date: 2021/5/2
 Time: 17:21
 功能: TypeScript脚本

 */

import { QueryType , querymode } from '@/types'

export interface IUserState {

    /**
     * 登录用户帐号
     */
    loginusermobile : string
}

export interface IAddPageData {
    year : number,
    month : number,
    billtypeid : number,
    billtypetxt : string,
    querytype : QueryType,

}

export interface IStatPageData {
    year : number,
    month : number,
    typedata_isout : boolean,
}

export interface IDetailPageData {
    sourcepagepath : string,
}

export interface IDataListPageData {
    mode : querymode,
    billtypeid : number,
    billtypetxt : string,

    isout : boolean,
}

export interface IPageData {

    AddPageData : IAddPageData | null,
    StatPageData : IStatPageData | null,
    DetailPageData : IDetailPageData,
    DataListPageData : IDataListPageData,
}

export interface IState extends IUserState , IPageData {

}
