<!--

作者: chenghao
Date: 2021/5/25
Time: 17:30
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>

    </div>

</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">

import { IBillObj } from '@/types'

// 导入
import {
    defineComponent ,
    // 注意导入PropType
    PropType ,
    ref ,
    reactive ,
    toRefs ,
    computed
} from "vue";
import { IAvatarObj } from "@comp/types";

export default defineComponent( {

    // 声明 props
    props : {
        list : {
            type : Object as PropType<IBillObj[]> ,
            required : true ,
        }
    } ,
    setup () {

        return {};
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       lang="less"
       src="./outitemlist.less">
</style>
