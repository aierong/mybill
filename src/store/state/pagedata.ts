/*

 作者: chenghao
 Date: 2021/5/19
 Time: 17:00
 功能: TypeScript脚本

 */

import { IPageData } from "@store/types";

export const pagedatastate : IPageData = {

    /**
     * 添加页面数据
     */
    AddPageData : null ,

    /**
     * 统计页面
     */
    StatPageData : null ,

    /**
     * 详细页面数据
     */
    DetailPageData : {
        sourcepagepath : ''
    } ,

    OutListPageData : {
        mode : 'money'
    }
};
