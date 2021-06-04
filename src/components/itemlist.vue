<!--

作者: chenghao
Date: 2021/5/25
Time: 17:30
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div class="item">
        <van-swipe-cell v-for="(mxitem,mxindex) in list"
                        :key="mxindex">
            <van-cell center
                      @click="itemClick(mxitem.ids)">
                <template #title>
                    <span class="notxt">{{ mxindex + 1 }}</span>
                    <aliicon :iconname="mxitem.avatar"
                             :iconsize="36"
                             :colortypes="mxitem.isout?'out':'in'"/>
                    <span class="itemtxt">{{ mxitem.typename }}</span>
                </template>
                <template #default>
                    <span class="money">{{ mxitem.isout ? '-' : '+' }}{{ $FormatMoney( mxitem.moneys ) }}</span>
                    <br>
                    <span class="date">{{ formatdate( mxitem.moneydate ) }}</span>
                </template>
            </van-cell>
            <template #right>
                <van-button square
                            @click="deleteitem(mxitem.ids)"
                            type="danger"
                            text="删除"/>
            </template>
        </van-swipe-cell>
    </div>

</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">

import dayjs from 'dayjs'

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

import { useRouter } from 'vue-router'

//引入一下
import { Toast , Dialog } from 'vant';

import * as billapi from '@/http/api/bill'
import { ISelectDateObj } from "@comp/types";

export default defineComponent( {
    // 定义是事件
    emits : {
        deleteitemresult : ( isok : boolean ) => {
            //上面已经定义了参数类型,系统会验证的参数类型

            return true
        } ,
    } ,
    // 声明 props
    props : {
        list : {
            type : Object as PropType<IBillObj[]> ,
            required : true ,
            default : []
        }
    } ,
    setup ( props , { emit } ) {
        const router = useRouter()

        const itemClick = ( ids : number ) => {
            router.push( `/detail?ids=${ ids }` )

            return;
        }

        const formatdate = ( date : string ) => {
            return dayjs( date ).format( 'MM月DD日' );
        }

        const deleteitem = ( ids : number ) => {
            Dialog.confirm( {
                // title : "退出" ,
                message : "确定删除?"
            } ).then( async () => {
                // on confirm
                // console.log( "点确定按钮" )

                var status = await billapi.deletebyid( ids )

                if ( status.data.Success ) {
                    //成功不用刷新模型了
                    Toast.success( "成功删除" )

                    emit( "deleteitemresult" , true );
                }
                else {
                    Toast.fail( status.data.Message )

                    emit( "deleteitemresult" , false );

                    return;
                }

            } ).catch( () => {
                // on cancel
                // console.log( "点取消按钮" )
            } )
        }

        return {
            itemClick ,
            formatdate ,
            deleteitem ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       lang="less"
       src="./itemlist.less">
</style>
