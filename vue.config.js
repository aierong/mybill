/*
 作者: chenghao
 功能: 配置文件
 */
const path = require( 'path' );

function resolve ( dir ) {
    return path.join( __dirname , dir )
}

function addStyleResource ( rule ) {
    rule.use( 'style-resource' )
    .loader( 'style-resources-loader' )
    .options( {
        patterns : [
            path.resolve( __dirname , './src/styles/imports.less' ) ,
        ] ,
    } )
}

module.exports = {
    //关闭eslint
    lintOnSave : false ,

    chainWebpack : config => {
        //vue有一个默认的别名:@ 对应 src
        //下面定义了2个别名
        // 链式，接着可以往下写
        config.resolve.alias.set( '@comp' , resolve( 'src/components' ) )
        .set( '@views' , resolve( 'src/views' ) )
        .set( '@common' , resolve( 'src/common' ) )
        .set( '@https' , resolve( 'src/https' ) )
        .set( '@store' , resolve( 'src/store' ) )
        .set( '@assets' , resolve( 'src/assets' ) )
        .set( '@plugins' , resolve( 'src/plugins' ) )

        const types = [ 'vue-modules' , 'vue' , 'normal-modules' , 'normal' ]

        types.forEach( type => addStyleResource( config.module.rule( 'less' ).oneOf( type ) ) )
    } ,

    devServer : {
        //配置端口
        port : 8338
    } ,

};
