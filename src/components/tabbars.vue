<!--

作者: chenghao
Date: 2021/4/30
Time: 17:27
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <van-tabbar v-model="active">
        <van-tabbar-item icon="home-o">标签</van-tabbar-item>
        <van-tabbar-item icon="search">标签</van-tabbar-item>
        <van-tabbar-item icon="friends-o">标签</van-tabbar-item>
        <van-tabbar-item icon="setting-o">标签</van-tabbar-item>
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

export default defineComponent( {
    // 子组件
    components : {} ,
    // 声明 props
    props : {} ,
    setup () {
        const active = ref<number>( 0 );

        return {
            active ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style>

</style>
