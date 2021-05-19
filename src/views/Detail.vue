<!--

作者: chenghao
Date: 2021/5/18
Time: 17:11
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>
        <van-nav-bar title="详细数据"
                     left-text="返回"
                     left-arrow
                     @click-left="onClickLeft"/>
        {{ queryid }}
    </div>

</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">

interface IState {
    userselectyear : number,

    sourcepagepath : string,
}

// 导入
import {
    defineComponent ,
    ref ,
    reactive ,
    toRefs ,
    computed ,
} from "vue";

import { useRouter , useRoute } from 'vue-router'

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
        const router = useRouter()

        // const state = reactive<IState>( {
        //
        //     sourcepagepath : '' ,
        //     userselectyear : 0
        // } )

        // const sourcepagepath = ref<string>( '' )

        // router.afterEach( ( to , from ) => {
        //     // 进入 和 离开页面都会触发
        //
        //     console.log( "afterEach,to:" , to );
        //     console.log( "afterEach,from:" , from , from.path );
        //
        //     if ( from.path != 'detail' ) {
        //         // sourcepagepath.value = from.path;
        //         state.sourcepagepath = from.path;
        //     }
        //
        // } )

        const onClickLeft = () => {
            router.push( `/bill` );

            return;
        }

        return {
            // sourcepagepath ,
            // ...toRefs( state ) ,
            onClickLeft ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       lang="less"
       src="./Detail.less">
</style>
