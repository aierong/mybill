/*

 作者: chenghao
 Date: 2021/5/15
 Time: 14:27
 功能: TypeScript脚本

 */

import {
    IFormatMoney,
    IFormatPercent, 
    IFormatStatMoney
} from '@plugins/types'

// 定义类型 ，这样并且在vue页面中proxy调用有语法提示的
declare module '@vue/runtime-core' {
    interface ComponentCustomProperties {
        $FormatMoney: IFormatMoney;
        $FormatStatMoney: IFormatStatMoney;
        $FormatPercent: IFormatPercent;
    }
}

/**
 * 格式化小数
 * @param num
 * @param digits
 * @constructor
 */
function FormatNumber(num: number, digits: number = 2) {
    return num.toFixed(digits);
}

/*
格式化百分数
*/
let FormatPercent: IFormatPercent = function (num: number, digits: number = 2) {
    return `${FormatNumber(num * 100, digits)}%`;
}

/*
格式化金额
*/
let FormatMoney: IFormatMoney = function (num, digits): string {
    return FormatNumber(num, digits);
}

/*
格式化统计金额
*/
let FormatStatMoney: IFormatStatMoney = function (num, digits): string {
    var y: number = 100000000;

    if (num >= y) {
        return FormatNumber(num / y, digits) + '亿';
    }

    var w: number = 10000;
    if (num >= w) {
        return FormatNumber(num / w, digits) + '万';
    }

    return FormatNumber(num, digits);
}

/*
导出方法
*/
export default {
    install(app, options) {
        // 原型上可以绑定挂靠变量,方法：

        // 1.变量名,方法名推荐使用$开头(避免和本地变量冲突)
        // 2.变量,方法可以在模板中直接使用，或者在代码中调用

        //全局函数
        app.config.globalProperties.$FormatMoney = FormatMoney;
        app.config.globalProperties.$FormatStatMoney = FormatStatMoney;
        app.config.globalProperties.$FormatPercent = FormatPercent;

    }
}
