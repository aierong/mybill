<!--

作者: chenghao
Date: 2021/4/30
Time: 17:27
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <!--    <van-tabbar v-model="active"-->
    <!--                @change="onchange"-->
    <!--                active-color="#ee0a24"-->
    <!--                inactive-color="#000">-->

    <!--        <van-tabbar-item icon="orders-o">明细</van-tabbar-item>-->
    <!--        <van-tabbar-item icon="points">统计</van-tabbar-item>-->
    <!--        <van-tabbar-item icon="setting-o">我</van-tabbar-item>-->
    <!--    </van-tabbar>-->

    <van-tabbar route
                active-color="#ee0a24"
                inactive-color="#000">
        <van-tabbar-item replace
                         to="/bill"
                         icon="orders-o">明细
        </van-tabbar-item>
        <van-tabbar-item replace
                         to="/stat"
                         icon="points">统计
        </van-tabbar-item>
        <van-tabbar-item replace
                         to="/me"
                         icon="setting-o">我
        </van-tabbar-item>
    </van-tabbar>
</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">

// 导入
import {
    defineComponent ,
    ref ,
    reactive ,
    toRefs ,
    computed ,
    onMounted ,
} from "vue";

// import { useRouter , useRoute } from 'vue-router'

export default defineComponent( {
    // 声明 props
    props : {} ,
    setup () {
        // const router = useRouter()
        // const route = useRoute()
        //
        // const active = ref<number>( 0 );

        // const onchange = ( index : number ) : void => {
        //     // console.log( 'index' , index )
        //
        //     if ( index == 0 ) {
        //         // 页面跳转
        //         router.push( "/bill" )
        //     }
        //     else if ( index == 1 ) {
        //         // 页面跳转
        //         router.push( "/stat" );
        //     }
        //     else if ( index == 2 ) {
        //         // 页面跳转
        //         router.push( "/me" )
        //     }
        // }

        // const setuptab = ( path : string ) : void => {
        //     if ( path == "/bill" ) {
        //         active.value = 0;
        //
        //         return;
        //     }
        //     else if ( path == '/stat' ) {
        //         active.value = 1;
        //
        //         return;
        //     }
        //     else if ( path == '/me' ) {
        //         active.value = 2;
        //
        //         return;
        //     }
        //     else {
        //         active.value = 0;
        //
        //         return;
        //     }
        // }

        // onMounted( () => {
        //     // var path = route.path;
        //
        //     // console.log( 'path' , path )
        //
        //     // 从新设置一下tab,要不刷新页面,会丢失tab
        //     // setuptab( path );
        //     var meta : any = route.meta;
        //     active.value = parseInt( meta.tabbarindex );
        // } )

        return {
            // active ,
            // onchange ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style>

</style>
