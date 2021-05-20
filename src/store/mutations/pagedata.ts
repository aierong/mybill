/*

 作者: chenghao
 Date: 2021/5/19
 Time: 17:08
 功能: TypeScript脚本

 */

import * as types from '@store/mutations/mutation-types'

import { IState , IAddPageData } from '@store/types'

export const pagedatamutations = {

    //修改添加页面刷新状态
    [ types.updateaddpagedatarefresh ] ( state : IState , isrefresh : boolean ) {
        state.AddPageData.isrefresh = isrefresh;
    } ,

    //修改
    [ types.updateaddpagedata ] ( state : IState , payload : IAddPageData ) {
        state.AddPageData = payload;
    } ,

    [ types.updatedetailpagedata ] ( state : IState , payload : string ) {
        state.DetailPageData.sourcepagepath = payload;
    } ,

    [ types.updatetabbarshow ] ( state : IState , payload : boolean ) {
        state.tabbarshow = payload;
    } ,

}
