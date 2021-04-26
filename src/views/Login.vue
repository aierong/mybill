<!--

作者: chenghao
Date: 2021/4/26
Time: 16:40
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>
        <van-nav-bar right-text="注册账号"
                     title="用户登录"
                     @click-right="onClickRight"/>
        <br>
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

import {
    useRouter ,
    useRoute
} from 'vue-router'

export default defineComponent( {

    setup () {
        const router = useRouter()

        //转向注册页面
        const onClickRight = () => {
            router.push( '/register' )

            return;
        }

        return {

            onClickRight
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style>

</style>
