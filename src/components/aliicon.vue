<!--

作者: chenghao
Date: 2021/5/7
Time: 18:55
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>
    <i :class="[{ iniconcolor:!isout,outiconcolor:isout  },iconfont, iconname]"
       :style="{  fontSize: iconsize + 'px' }"></i>
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

import '@assets/ali/iconfont/iconfont.css'

export default defineComponent( {
    // 声明 props
    props : {
        iconname : {
            type : String ,
            required : true
        } ,
        iconsize : {
            type : Number ,
            required : true
        } ,
        isout : {
            type : Boolean ,
            required : true
        } ,
    } ,
    setup () {
        const iconfont : string = 'iconfont'

        return {
            iconfont ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style lang="less"
       scoped
       src="./aliicon.less">
</style>
