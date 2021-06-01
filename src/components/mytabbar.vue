<!--

作者: chenghao
Date: 2021/4/30
Time: 17:27
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>
    <van-tabbar route
                :fixed="true"
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

export default defineComponent( {

    setup () {

        return {
            // active ,
            // onchange ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       lang="less"
       src="./mytabbar.less">
</style>
