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
import { IBillObj } from '@/types'

interface IState {
    model : IBillObj | null,
}

// 导入
import {
    defineComponent ,
    ref ,
    reactive ,
    toRefs ,
    computed ,
    onMounted ,
} from "vue";

import { useRouter , useRoute } from 'vue-router'
import { useStore } from 'vuex'
import { key } from '@store/index.ts'

import * as billapi from '@/http/api/bill'

export default defineComponent( {
    // 子组件
    components : {} ,
    // 声明 props
    props : {
        queryid : {
            type : Number ,
            default : 0
        } ,
    } ,
    setup ( props , { emit } ) {
        const router = useRouter()
        const store = useStore( key )

        const state = reactive<IState>( {
            model : null
        } );

        const sourcepagepath = computed( () => {
            return store.getters.getdetailpagesourcepagepath;
        } )

        const onClickLeft = () => {
            // 可能有几个页面到这个详细页面,所以要返回之前页面
            router.push( { path : sourcepagepath.value } );

            return;
        }

        onMounted( async () => {
            await getmodel()
        } )

        const getmodel = async () => {

            var status = await billapi.get( props.queryid );

            console.log( 'status' , status )

            if ( status.data.Success ) {

                state.model = status.data.Result;
            }
            else {
                state.model = null;
            }

        }

        return {
            ...toRefs( state ) ,
            sourcepagepath ,
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
