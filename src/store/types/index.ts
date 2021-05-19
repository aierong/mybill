/*

 作者: chenghao
 Date: 2021/5/2
 Time: 17:21
 功能: TypeScript脚本

 */

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
    isrefresh : boolean
}

export interface IDetailPageData {
    sourcepagepath : string,
}

export interface IPageData {

    AddPageData : IAddPageData,
    DetailPageData : IDetailPageData

}

export interface IState extends IUserState , IPageData {

}
