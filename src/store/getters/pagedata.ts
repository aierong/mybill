/*

 作者: chenghao
 Date: 2021/5/19
 Time: 17:04
 功能: TypeScript脚本

 */

import { IState } from '@store/types'

const getdetailpagesourcepagepath = ( state : IState ) => {
    if ( state.DetailPageData != null ) {
        return state.DetailPageData.sourcepagepath;
    }

    return '';
};

export const pagedatagetters = {

    getdetailpagesourcepagepath ,

}
