/*

 作者: chenghao
 Date: 2021/5/19
 Time: 17:08
 功能: TypeScript脚本

 */

import * as types from '@store/mutations/mutation-types'

import { querymode } from '@/types'

import {
    IState ,
    IAddPageData ,
    IStatPageData ,
} from '@store/types'

export const pagedatamutations = {

    //修改
    [ types.updateaddpagedata ] ( state : IState , payload : IAddPageData ) {
        state.AddPageData = payload;
    } ,

    [ types.updatestatpagedata ] ( state : IState , payload : IStatPageData ) {
        state.StatPageData = payload;
    } ,

    [ types.updatedetailpagedata ] ( state : IState , payload : string ) {
        state.DetailPageData.sourcepagepath = payload;
    } ,

    [ types.updatedatalistpagemode ] ( state : IState , payload : querymode ) {
        state.DataListPageData.mode = payload;
    } ,

    [ types.updatedatalistpagebilltype ] ( state : IState , payload : { billtypetxt : string, billtypeid : number } ) {
        state.DataListPageData.billtypetxt = payload.billtypetxt;
        state.DataListPageData.billtypeid = payload.billtypeid;
    } ,

    [ types.updatedatalistpageisout ] ( state : IState , payload : boolean ) {
        state.DataListPageData.isout = payload;
    } ,

}
