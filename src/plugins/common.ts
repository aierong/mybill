/*

 作者: chenghao
 Date: 2021/5/15
 Time: 14:27
 功能: TypeScript脚本

 */

import { IFormarMoney , IFormatPercent } from '@plugins/types'

// 定义类型 ，这样并且在vue页面中proxy调用有语法提示的
declare module '@vue/runtime-core' {
    interface ComponentCustomProperties {
        $FormarMoney : IFormarMoney;
        $FormatPercent : IFormatPercent;
    }
}

/**
 * 格式化小数
 * @param num
 * @param digits
 * @constructor
 */
function FormatNumber ( num : number , digits : number = 2 ) {
    return num.toFixed( digits );
}

let FormatPercent : IFormatPercent = function ( num : number , digits : number = 2 ) {
    return `${ FormatNumber( num * 100 , digits ) }%`;
}

let FormarMoney : IFormarMoney = function ( num , digits ) : string {
    return FormatNumber( num , digits );
}

export default {
    install ( app , options ) {
        // 原型上可以绑定挂靠变量,方法：

        // 1.变量名,方法名推荐使用$开头(避免和本地变量冲突)
        // 2.变量,方法可以在模板中直接使用，或者在代码中调用

        //全局函数
        app.config.globalProperties.$FormarMoney = FormarMoney;
        app.config.globalProperties.$FormatPercent = FormatPercent;

    }
}
