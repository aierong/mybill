<!--

作者: chenghao
Date: 2021/5/11
Time: 17:35
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <van-popup :style="{ height: '40%' }"
               v-model:show="show"
               closeable
               position="bottom">
        <br><br><br>
        <div>
            <span>支出</span><span>收入</span>
        </div>
        <br>
        <div>
            <div class="divhx"
                 v-if="isout">
                <template v-for="(mxitem,mxindex) in outlist">
                    <span style="margin-left: 5px;"><aliicon :iconname="mxitem.avatar"
                                                             :iconsize="26"/>{{ mxitem.typename }}</span>
                </template>
                <span style="margin-left: 15px;margin-right: 15px;"><van-icon @click="onAddType"
                                                                              name="add-o"
                                                                              size="26"/></span>
            </div>
            <div class="divhx"
                 v-else>
                <template v-for="(mxitem,mxindex) in inlist">
                    <aliicon :iconname="mxitem.avatar"
                             :iconsize="22"/>
                    <span>{{ mxitem.typename }}</span>
                </template>
            </div>

        </div>

    </van-popup>
    <van-dialog v-model:show="showtypedlg"
                title="填写类型"
                show-cancel-button>
        <van-field v-model="typetxt"
                   autosize
                   maxlength="6"
                   placeholder="请输入类型"
                   show-word-limit
                   @confirm="typeconfirm"/>
    </van-dialog>
</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">
import { IBillType } from "@comp/types";

interface IAllList {
    outlist : IBillType[],
    inlist : IBillType[],
    isout : boolean
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
import * as billtypeapi from "@/http/api/billtype";

export default defineComponent( {
    // 子组件
    components : {} ,
    // 声明 props
    props : {} ,
    setup ( props , { emit } ) {
        const show = ref<boolean>( false )

        const showtypedlg = ref<boolean>( false )
        const typetxt = ref<string>( '' )

        const listmodeldata = reactive<IAllList>( {
            outlist : [] ,
            inlist : [] ,
            isout : true
        } );

        const toggle = () => {
            // console.log( 'toggle' )
            show.value = !show.value
        }

        onMounted( async () => {
            await getlist();
        } );

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

        const onAddType = () => {
            showtypedlg.value = true;
        }

        const typeconfirm = () => {
        }

        return {
            ...toRefs( listmodeldata ) ,
            show , toggle ,
            showtypedlg , typetxt ,
            onAddType , typeconfirm ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       src="./BillOperation.css">
</style>
