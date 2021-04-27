<!--

作者: chenghao
Date: 2021/4/26
Time: 16:39
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>
        <van-nav-bar title="注册新用户"
                     left-text="返回"
                     right-text="说明"
                     left-arrow
                     @click-left="onClickLeft"
                     @click-right="onClickRight"/>
        <br>
        <van-form @submit="onSubmit"
                  :validate-first="true"
                  @failed="onFailed">

        </van-form>
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

//引入一下
import { Toast } from 'vant';

export default defineComponent( {
    // 子组件
    components : {} ,

    setup () {
        const router = useRouter()

        const onClickLeft = () => {
            gotologin( '' );
        }

        const onClickRight = () => {
            Toast( "这里可以注册新用户" )

            return;
        }

        const gotologin = ( mobile : string = '' ) => {
            router.push( `/login?mobile=${ mobile }` )

            return;
        }

        // 提交
        const onSubmit = () => {
        }

        //failed	提交表单且验证不通过后触发	errorInfo: { values: object, errors: object[] }
        const onFailed = () => {
        }

        return {

            onClickLeft ,
            onClickRight ,
            onSubmit , onFailed ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style>

</style>
