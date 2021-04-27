/*
 作者: chenghao
 功能: 配置文件
 */
const path = require('path');

function resolve(dir) {
    return path.join(__dirname, dir)
}

module.exports = {
    //关闭eslint
    lintOnSave : false ,

    chainWebpack : ( config ) => {
        //vue有一个默认的别名:@ 对应 src
        //下面定义了2个别名
        config.resolve.alias
        // 链式，接着可以往下写
        .set( '@comp' , resolve( 'src/components' ) )
        .set( '@views' , resolve( 'src/views' ) )
        .set( '@common' , resolve( 'src/common' ) )
        .set( '@https' , resolve( 'src/https' ) )

    } ,
    devServer : {
        //配置端口
        port : 8338
    } ,



};
