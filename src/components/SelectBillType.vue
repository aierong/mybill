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
        <span style="color: #222222;background-color: white;">全部</span>
        <div>支出</div>

        <div>收入</div>
    </van-popup>

</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">

interface IList {
    ids : number,
    isout : boolean,
    issystemtype : boolean,
    typename : string,
    avatar : string,
}

interface IAllList {
    outlist : IList[],
    inlist : IList[]
}

// 导入
import {
    defineComponent ,
    ref ,
    reactive ,
    toRefs ,
    computed , onMounted ,
    watch ,
} from "vue";

import { ISelectDateObj } from "@comp/types";

import * as billtypeapi from '@/http/api/billtype'

export default defineComponent( {
    // 定义是事件
    emits : {} ,
    // 声明 props
    props : {
        selectbilltypeid : {
            type : Number ,
            required : true
        } ,
        dialogshow : {
            type : Boolean ,
            required : true
        }
    } ,
    setup ( props , { emit } ) {
        const show = ref( false )
        const billtypeid = ref<number>( 0 )

        const listmodeldata = reactive<IAllList>( {
            outlist : [] ,
            inlist : []
        } );

        watch(
            () => props.selectbilltypeid ,
            ( newval ) => {
                // console.log( '子组件：监听props中num' , newval , old )
                billtypeid.value = newval;

            } ,
            {
                // 这里如果不设置immediate = true,那么最初绑定的时候是不会执行的,要等到num改变时才执行监听计算
                immediate : true
            }
        )

        watch(
            () => props.dialogshow ,
            ( newval ) => {
                show.value = newval;

                if ( newval ) {
                    //重新获取一下,列表
                    getlist();
                }
            } ,
            {
                // 这里如果不设置immediate = true,那么最初绑定的时候是不会执行的,要等到num改变时才执行监听计算
                immediate : true
            }
        )

        const getlist = async () => {
            var outstatus = await billtypeapi.getlist( true , true );

            if ( outstatus.data.Success ) {
                listmodeldata.outlist = outstatus.data.Result;
            }
            else {
                listmodeldata.outlist = [];
            }

            var instatus = await billtypeapi.getlist( false , true );

            if ( instatus.data.Success ) {
                listmodeldata.inlist = instatus.data.Result;
            }
            else {
                listmodeldata.inlist = [];
            }
        }

        return {
            ...toRefs( listmodeldata ) ,
            show , billtypeid ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style>

</style>
