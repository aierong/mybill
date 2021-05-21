/*

 作者: chenghao
 Date: 2021/5/19
 Time: 17:04
 功能: TypeScript脚本

 */

import { IState } from '@store/types'

// const getaddpagerefresh = ( state : IState ) => {
//     if ( state.AddPageData != null ) {
//         return state.AddPageData.isrefresh;
//     }
//
//     return false;
// };

const getdetailpagesourcepagepath = ( state : IState ) => {
    if ( state.DetailPageData != null ) {
        return state.DetailPageData.sourcepagepath;
    }

    return '';
};

// const gettabbarshow = ( state : IState ) => {
//
//     return state.tabbarshow;
// };

export const pagedatagetters = {
    // getaddpagerefresh ,
    getdetailpagesourcepagepath ,
    // gettabbarshow ,
}
