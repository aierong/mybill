<!--

作者: chenghao
Date: 2021/5/25
Time: 17:37
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>
        <van-nav-bar title="详细数据"
                     left-text="返回"
                     left-arrow
                     @click-left="onClickLeft"/>
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
        // queryyear : {
        //     type : Number ,
        //     default : 0
        // } ,
        // querymonth : {
        //     type : Number ,
        //     default : 0
        // } ,
    } ,
    setup () {

        const onClickLeft = () => {
            // gotoback();
        }

        return {
            onClickLeft ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style lang="less"
       scoped
       src="./OutDetail.less">

</style>
