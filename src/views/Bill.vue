<!--

作者: chenghao
Date: 2021/4/30
Time: 17:52
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>

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
    computed ,
    onMounted ,
} from "vue";

import * as billapi from '@/http/api/bill'

export default defineComponent( {
    // 子组件
    components : {} ,

    setup () {

        onMounted( async () => {
            var result = await billapi.getlist( 2021 , 1 , 0 );

            console.log( 'result' , result )
        } )

        return {};
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style>

</style>
