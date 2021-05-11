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
               :close-on-click-overlay="false"
               v-model:show="show"
               closeable
               close-icon-position="top-left"
               position="bottom"
               @click-close-icon="closedlg">
        <br><br><br>
        <!--        <span style="color: #222222;background-color: white;">全部</span>-->
        <span class="allbor"
              :style="gettxtstyle(0)"
              @click="onAllClick">全部</span>
        <br>
        <!--        <div style="margin-left: 10px;">支出</div>-->
        <van-divider content-position="left">支出类型</van-divider>
        <van-grid>
            <van-grid-item v-for="(item,index) in outlist"
                           :key="index"
                           @click="onItemClick(item)">
                <template #text>
                    <span :style="gettxtstyle(item.ids)">{{ item.typename }}</span>
                </template>
            </van-grid-item>
        </van-grid>
        <br>
        <!--        <div style="margin-left: 10px;">收入</div>-->
        <van-divider content-position="left">收入类型</van-divider>
        <van-grid>
            <van-grid-item v-for="(item,index) in inlist"
                           :key="index"
                           @click="onItemClick(item)">
                <template #text>
                    <span :style="gettxtstyle(item.ids)">{{ item.typename }}</span>
                </template>
            </van-grid-item>
        </van-grid>
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

import { ISelectBillTypeObj } from "@comp/types";

import * as billtypeapi from '@/http/api/billtype'

export default defineComponent( {
    // 定义是事件
    emits : {
        dialogclose : null ,
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
            async ( newval ) => {
                show.value = newval;

                if ( newval ) {
                    //重新获取一下,列表
                    await getlist();

                    // console.log( 'newval' )
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

            // console.log( 'getlist' )
        }

        const isbilltype = ( id : number ) => {
            return billtypeid.value == id;
        }

        const gettxtstyle = ( id : number ) => {
            var _isbilltype = isbilltype( id );

            return {
                color : _isbilltype ? '#55a532' : '#0A0A0AFF'
            }
        }

        const onAllClick = () => {
            let payload : ISelectBillTypeObj = {
                id : 0 ,
                name : '全部' ,
                // 这个值,无用,随便搞一个
                isout : false
            }

            emit( "selectbilltype" , payload )
            emit( "dialogclose" )
        }

        const onItemClick = ( item : IList ) : void => {
            // console.log( 'item' , item )

            let payload : ISelectBillTypeObj = {
                id : item.ids ,
                name : item.typename ,
                isout : item.isout
            }

            emit( "selectbilltype" , payload )
            emit( "dialogclose" )
        }

        const closedlg = () => {
            // console.log( 'click-close-icon' )
            emit( "dialogclose" )
        }

        return {
            ...toRefs( listmodeldata ) ,
            show , billtypeid , gettxtstyle ,
            onAllClick , onItemClick , closedlg ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style scoped>
.allbor {
    border: 1px solid black;
}
</style>
