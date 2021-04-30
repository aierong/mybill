<!--

作者: chenghao
Date: 2021/4/30
Time: 17:27
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <van-tabbar v-model="active"
                @change="onchange"
                active-color="#ee0a24"
                inactive-color="#000">
        <van-tabbar-item icon="balance-list-o">记账</van-tabbar-item>
        <van-tabbar-item icon="points">统计</van-tabbar-item>
        <van-tabbar-item icon="setting-o">我</van-tabbar-item>
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
    computed
} from "vue";

import { useRouter , useRoute } from 'vue-router'

export default defineComponent( {
    // 子组件
    components : {} ,
    // 声明 props
    props : {} ,
    setup () {
        const router = useRouter()

        const active = ref<number>( 0 );

        const onchange = ( index : number ) => {
            // console.log( 'index' , index )

            if ( index == 0 ) {
                // 页面跳转
                router.push( "/chats" )
            }
            else if ( index == 1 ) {

                router.push( "/contacts" );
            }
            else if ( index == 2 ) {
                router.push( "/me" )
            }
        }

        return {
            active ,
            onchange ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style>

</style>
