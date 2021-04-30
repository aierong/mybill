<!--

作者: chenghao
Date: 2021/4/30
Time: 17:40
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>

        <van-cell size="large"
                  @click="SetupAvatarClick">
            <template #title>
                <van-icon name="smile-o"
                          size="58"/>

            </template>
        </van-cell>
        <br>
        <van-cell icon="setting-o"
                  title="修改密码"
                  is-link/>
        <br>

        <van-cell icon="user-o"
                  title="关于我们"
                  is-link/>
        <br><br>
        <van-button size="large"
                    hairline
                    plain
                    icon="friends"
                    round
                    @click="exitClick"
                    type="warning">退出

        </van-button>
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
    props : {} ,
    setup () {

        const SetupAvatarClick = () => {
        }

        const exitClick = () => {
        }

        return {
            SetupAvatarClick , exitClick ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style>

</style>
