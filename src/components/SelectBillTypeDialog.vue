<!--

作者: chenghao
Date: 2021/5/10
Time: 15:30
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <!-- 可以指定弹窗高度   :style="{ height: '60%' }"
    -->
    <van-popup :style="{ height: '40%' }"
               v-model:show="show"
               closeable
               position="bottom">
        <br><br><br>

        <span :class="{ active:isactive(0)  }"
              style="margin-left: 20px;"
              @click="onAllClick">全部</span>
        <br>

        <van-divider content-position="left">支出类型</van-divider>
        <van-grid>
            <van-grid-item v-for="(item,index) in outlist"
                           :key="index"
                           @click="onItemClick(item)">
                <template #text>
                    <span :class="{ active:isactive(item.ids)  }">{{ item.typename }}</span>
                </template>
            </van-grid-item>
        </van-grid>
        <br>

        <van-divider content-position="left">收入类型</van-divider>
        <van-grid>
            <van-grid-item v-for="(item,index) in inlist"
                           :key="index"
                           @click="onItemClick(item)">
                <template #text>

                    <span :class="{ active:isactive(item.ids)  }">{{ item.typename }}</span>
                </template>
            </van-grid-item>
        </van-grid>
    </van-popup>

</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">

import { IBillType , ISelectBillTypeObj } from "@comp/types";

interface IAllList {
    outlist : IBillType[],
    inlist : IBillType[]
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

import * as billtypeapi from '@/http/api/billtype'

export default defineComponent( {
    // 定义是事件
    emits : {
        selectbilltype : ( val : ISelectBillTypeObj ) => {
            //上面已经定义了参数类型,系统会验证的参数类型
            return true
        } ,
    } ,
    // 声明 props
    props : {
        selectbilltypeid : {
            type : Number ,
            required : true
        } ,
    } ,
    setup ( props , { emit } ) {
        const show = ref<boolean>( false )

        const listmodeldata = reactive<IAllList>( {
            outlist : [] ,
            inlist : []
        } );

        const getinlist = async () => {

            var instatus = await billtypeapi.getlist( false , true );

            if ( instatus.data.Success ) {
                listmodeldata.inlist = instatus.data.Result;
            }
            else {
                listmodeldata.inlist = [];
            }

        }

        const getoutlist = async () => {
            var outstatus = await billtypeapi.getlist( true , true );

            if ( outstatus.data.Success ) {
                listmodeldata.outlist = outstatus.data.Result;
            }
            else {
                listmodeldata.outlist = [];
            }

        }




        const isactive = ( id : number ) => {
            return props.selectbilltypeid == id;
        }

        const onAllClick = () => {
            let payload : ISelectBillTypeObj = {
                id : 0 ,
                name : '全部' ,
                // 这个值,无用,随便搞一个
                isout : false
            }

            emit( "selectbilltype" , payload )

            show.value = false;
        }

        const onItemClick = ( item : IBillType ) : void => {

            let payload : ISelectBillTypeObj = {
                id : item.ids ,
                name : item.typename ,
                isout : item.isout
            }

            emit( "selectbilltype" , payload )

            show.value = false;
        }

        const toggle = () => {
            show.value = !show.value
        }

        onMounted( async () => {
            await getoutlist();
            await getinlist();
        } )

        return {
            ...toRefs( listmodeldata ) ,
            show , isactive ,
            onAllClick , onItemClick , toggle ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       src="./SelectBillTypeDialog.css">
</style>
