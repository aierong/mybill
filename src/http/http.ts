/*
 作者: chenghao
 Date: 2021/4/27
 Time: 16:55
 功能: TypeScript脚本
*/

import axios , { AxiosRequestConfig , AxiosResponse } from 'axios';
import { tokenname } from '@common/constant'
import router from '@/router'

//引入一下
import { Toast } from 'vant';

let _url = process.env.VUE_APP_serverurl;

// axios.defaults.baseURL = `${ _url }/api`
// axios.defaults.timeout = 50000;

const service = axios.create( {
    baseURL : `${ _url }/api` ,
    timeout : 50000 ,
} );

// 请求拦截
service.interceptors.request.use( ( config : AxiosRequestConfig ) => {
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
service.interceptors.response.use( ( response : AxiosResponse ) => {
        return response;
    } ,
    ( error ) => {
        // 错误提醒

        const { status } = error.response;

        if ( status == 401 ) {
            // alert( 'token过期, 请重新登录!' );
            Toast.fail( 'token过期,请重新登录' )
            // 清楚token
            localStorage.removeItem( tokenname );
            // 页面跳转
            router.push( '/login' );
        }
        if ( status == 403 ) {
            // alert( '无权限,请重新登录!' );
            Toast.fail( '无权限,请重新登录' )
            // 清楚token
            localStorage.removeItem( tokenname );
            // 页面跳转
            router.push( '/login' );
        }
        else {
            alert( error.response.data );
        }
        return Promise.reject( error );
    }
);

// export default axios;
export default service


