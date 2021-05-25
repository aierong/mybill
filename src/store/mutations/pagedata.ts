/*

 作者: chenghao
 Date: 2021/5/19
 Time: 17:08
 功能: TypeScript脚本

 */

import * as types from '@store/mutations/mutation-types'

import {
    IState ,
    IAddPageData ,
    IStatPageData ,
    IOutListPageData ,
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

    [ types.updateoutlistpagedata ] ( state : IState , payload : IOutListPageData ) {
        state.OutListPageData = payload;
    } ,
}
