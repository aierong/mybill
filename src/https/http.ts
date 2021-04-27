/*

 作者: chenghao
 Date: 2021/4/27
 Time: 16:55
 功能: TypeScript脚本

 */

import axios from 'axios';
import { tokenname } from '@common/constant'

import {
    useRouter ,
    useRoute
} from 'vue-router'

// console.log( 'http.ts' )

let _url = process.env.VUE_APP_serverurl;
// console.log( '_url' , _url )
axios.defaults.baseURL = `${ _url }/api`

// 请求拦截
axios.interceptors.request.use(
    config => {
        let loginusertoken = localStorage.getItem( tokenname );

        if ( loginusertoken ) {
            // Bearer是JWT的认证头部信息
            // 注意要加:'Bearer '  有一个空格
            // 我们后端是用koa-jwt自动验证，必须要加上'Bearer ',如果是自己写验证还得把'Bearer '去掉再调用jwt.verify验证
            config.headers.common[ 'Authorization' ] = 'Bearer ' + loginusertoken;
        }

        return config;
    } ,
    error => {
        return Promise.reject( error );
    }
);

// 响应拦截
axios.interceptors.response.use(
    response => {
        return response;
    } ,
    error => {
        // 错误提醒

        const { status } = error.response;

        if ( status == 401 ) {
            alert( 'token过期, 请重新登录!' );
            // 清楚token
            localStorage.removeItem( tokenname );
            // 页面跳转
            const router = useRouter()
            router.push( '/login' );
        }
        if ( status == 403 ) {
            alert( '无权限,请重新登录!' );
            // 清楚token
            localStorage.removeItem( tokenname );
            // 页面跳转
            const router = useRouter()
            router.push( '/login' );
        }
        else {
            alert( error.response.data );
        }
        return Promise.reject( error );
    }
);

export default axios;


