<!--

作者: chenghao
Date: 2021/5/10
Time: 15:30
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <van-popup :close-on-click-overlay="false"
               v-model:show="show"
               position="bottom">

    </van-popup>

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
    watch ,
} from "vue";
import { ISelectDateObj } from "@comp/types";

export default defineComponent( {
    // 定义是事件
    emits : {} ,
    // 声明 props
    props : {
        selectbilltypeid : {
            type : Number ,
            required : true
        } ,
    } ,
    setup ( props , { emit } ) {
        const show = ref( false )
        const billtypeid = ref<number>( 0 )

        watch(
            () => props.selectbilltypeid ,
            ( newval ) => {
                // console.log( '子组件：监听props中num' , newval , old )
                billtypeid.value=newval;
            } ,
            {
                // 这里如果不设置immediate = true,那么最初绑定的时候是不会执行的,要等到num改变时才执行监听计算
                immediate : true
            }
        )

        return {
            show , billtypeid ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style>

</style>
