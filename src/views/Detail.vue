<!--

作者: chenghao
Date: 2021/5/18
Time: 17:11
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>
        {{ queryid }}
    </div>

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
    props : {
        queryid : {
            type : String ,
            default : ''
        } ,
    } ,
    setup () {

        return {};
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       lang="less"
       src="./Detail.less">
</style>
