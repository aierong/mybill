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
        <!--        {{ queryid }}-->
        <div class="detail"
             v-if="modeldata!=null">
            <div class="card">
                <div class="avatar">
                    <aliicon :iconname="modeldata.avatar"
                             :iconsize="28"
                             :colortypes="modeldata.isout?'out':'in'"/>
                    <span >{{ modeldata.typename }}</span>
                </div>
                <div class="amount">
                    <span>{{ modeldata.isout ? '-' : '+' }}{{ modeldata.moneys }}</span>
                </div>

                <div class="times">
                    记录时间:{{ modeldata.moneydate }}
                </div>
                <div class="sources">
                    来源:{{ modeldata.sources }}
                </div>
                <div class="memo">
                    备注:{{ modeldata.memo }}
                </div>
                <van-divider/>
                <div class="cz">
                    <van-icon color="red"
                              name="delete"
                              size="25"
                              @click="onDelete"/>
                    <span @click="onDelete">删除</span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <van-icon size="25"
                              name="edit"
                              @click="onUpdate"/>
                    <span @click="onUpdate">编辑</span>
                </div>
                <br>

            </div>
            <!--    修改账单弹窗   -->
            <BillOperation :isrunadd="false"
                           :updatedata="modeldata"
                           @runresult="billrunresult"
                           ref="operationRef"/>
        </div>
    </div>

</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">
import { IBillObj } from '@/types'

interface IState {
    modeldata : IBillObj | null,
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

//引入一下
import { Toast , Dialog } from 'vant';

import { useRouter , useRoute } from 'vue-router'
import { useStore } from 'vuex'
import { key } from '@store/index.ts'

import * as billapi from '@/http/api/bill'

import BillOperation from "@comp/popup/BillOperation.vue";

export default defineComponent( {
    // 子组件
    components : {
        BillOperation ,
    } ,
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

        const operationRef = ref<typeof BillOperation | null>( null );

        const state = reactive<IState>( {
            modeldata : null
        } );

        // //数据是否变动
        // const isdatachange = ref<boolean>( false );

        const sourcepagepath = computed( () => {
            return store.getters.getdetailpagesourcepagepath;
        } )

        const onClickLeft = () => {
            gotoback();
        }

        const gotoback = () => {
            // 可能有几个页面到这个详细页面,所以要返回之前页面
            router.push( { path : sourcepagepath.value } );

            return;
        }

        onMounted( async () => {
            await getmodel()
        } )

        const getmodel = async () => {

            var status = await billapi.get( props.queryid );

            // console.log( 'status' , status )

            if ( status.data.Success ) {

                state.modeldata = status.data.Result;
            }
            else {
                state.modeldata = null;
            }

        }

        const onDelete = async () => {
            // 删除

            if ( props.queryid <= 0 ) {
                Toast.fail( 'id错误' )

                return;
            }

            Dialog.confirm( {
                // title : "退出" ,
                message : "确定删除?"
            } ).then( async () => {
                // on confirm
                // console.log( "点确定按钮" )

                var status = await billapi.deletebyid( props.queryid )

                if ( status.data.Success ) {
                    //成功不用刷新模型了,直接跳回之前页面
                    Toast.success( "成功删除" )

                    gotoback();
                }
                else {
                    Toast.fail( status.data.Message )

                    return;
                }

            } ).catch( () => {
                // on cancel
                // console.log( "点取消按钮" )
            } )
        }

        const onUpdate = () => {
            // 修改

            if ( operationRef.value != null ) {
                operationRef.value.toggle();
            }
        }

        const billrunresult = async ( isok : boolean ) => {
            if ( !isok ) {
                //  失败,暂时不处理

                return;
            }
            else {
                //再从新请求 服务器
                await getmodel();

                return;
            }
        }

        return {
            ...toRefs( state ) ,
            sourcepagepath ,
            onClickLeft ,
            onDelete , onUpdate ,
            // isdatachange ,
            operationRef , billrunresult ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       lang="less"
       src="./Detail.less">
</style>
