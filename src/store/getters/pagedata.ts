/*

 作者: chenghao
 Date: 2021/5/19
 Time: 17:04
 功能: TypeScript脚本

 */

import { IState } from '@store/types'

const getaddpagerefresh = ( state : IState ) => {
    if ( state.AddPageData != null ) {
        return state.AddPageData.isrefresh;
    }

    return false;
};

export const pagedatagetters = {
    getaddpagerefresh
}
