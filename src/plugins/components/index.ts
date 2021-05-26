/*

 作者: chenghao
 Date: 2021/5/26
 Time: 10:58
 功能: TypeScript脚本

 */

//引入公共组件
import aliicon from '@comp/aliicon.vue'
import mytabbar from '@comp/mytabbar.vue'

export default {
    install ( app , options ) {

        //全局组件
        app.component( 'aliicon' , aliicon )
        app.component( 'mytabbar' , mytabbar )
    }
}
