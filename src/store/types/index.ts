/*

 作者: chenghao
 Date: 2021/5/2
 Time: 17:21
 功能: TypeScript脚本

 */

import { QueryType } from '@/types'

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
    isout : boolean,
}

export interface IOutDetailPageData {
    year : number,
    month : number,

}

export interface IDetailPageData {
    sourcepagepath : string,
}

export interface IPageData {

    AddPageData : IAddPageData | null,
    StatPageData : IStatPageData | null,
    DetailPageData : IDetailPageData,
    OutDetailPageData : IOutDetailPageData,
}

export interface IState extends IUserState , IPageData {

}
