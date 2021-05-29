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

const getdatalistpagebilltypeid = ( state : IState ) => {
    if ( state.DataListPageData != null ) {
        return state.DataListPageData.billtypeid;
    }

    return 0;
};

const getdatalistpagebilltypetxt = ( state : IState ) => {
    if ( state.DataListPageData != null ) {
        return state.DataListPageData.billtypetxt;
    }

    return '';
};

export const pagedatagetters = {

    getdetailpagesourcepagepath ,

    getdatalistpagebilltypeid ,
    getdatalistpagebilltypetxt ,

}
