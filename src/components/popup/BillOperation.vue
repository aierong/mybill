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
            <span @click="changeType(true)"
                  :class="{filterspan:true,outactive:isout}">支出</span>
            <span @click="changeType(false)"
                  :class="{filterspan:true,inactive:!isout}">收入</span>
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
        <br>
        <div>
            <div class="remarktip"
                 @click="openmomedlg"
                 v-if="isdisplaymometip">添加备注
            </div>
            <div v-else>
                <span class="remark">{{ mometxt }}</span><span @click="openmomedlg"
                                                               class="remarkupdate">修改</span>
            </div>
        </div>
    </van-popup>
    <van-dialog v-model:show="showtypedlg"
                title="填写类型"
                show-cancel-button
                :before-close="typebeforeClose">
        <van-field v-model="typetxt"
                   autosize
                   maxlength="4"
                   placeholder="请输入类型"
                   show-word-limit/>
    </van-dialog>
    <van-dialog v-model:show="showmomedlg"
                title="填写备注"
                show-cancel-button>
        <van-field v-model="mometxt"
                   autosize
                   rows="2"
                   type="textarea"
                   maxlength="10"
                   placeholder="请输入备注"
                   show-word-limit/>
    </van-dialog>
</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">
import { IBillType } from "@comp/types";

interface IAllList {
    outlist : IBillType[],
    inlist : IBillType[],
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
import { Toast } from 'vant';

import * as billtypeapi from "@/http/api/billtype";
import { EncryptPassWord } from "@common/util";

export default defineComponent( {
    // 子组件
    components : {} ,
    // 声明 props
    props : {} ,
    setup ( props , { emit } ) {
        const show = ref<boolean>( false )

        const showtypedlg = ref<boolean>( false )
        const typetxt = ref<string>( '' )

        const showmomedlg = ref<boolean>( false )
        const mometxt = ref<string>( '' )

        const isout = ref<boolean>( true );

        const listmodeldata = reactive<IAllList>( {
            outlist : [] ,
            inlist : []
        } );

        const isdisplaymometip = computed( () => {
            if ( mometxt.value == '' ) {
                return true;
            }

            return false;
        } )

        const toggle = () => {
            // console.log( 'toggle' )
            show.value = !show.value
        }

        onMounted( async () => {
            await getoutlist();
            await getinlist();
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

        const onAddType = () => {
            showtypedlg.value = true;
        }

        const typebeforeClose = async ( action : string ) => {
            console.log( 'typebeforeClose' )

            if ( action === "confirm" ) {
                if ( typetxt.value != '' ) {
                    let status = await billtypeapi.add( isout.value , typetxt.value );

                    if ( status.data.Success ) {
                        typetxt.value = '';  //清空一下

                        // 成功了,刷新一下
                        if ( isout.value ) {
                            await getoutlist();
                        }
                        else {
                            await getinlist();
                        }

                        return true;
                    }
                    else {
                        Toast.fail( status.data.Message )

                        return false;
                    }
                }
                else {
                    Toast( '请填写类型' );

                    return false;
                }
            }
            else {
                // 点击了取消按钮

                // return true;
            }

            return true;
        }

        const openmomedlg = () => {
            showmomedlg.value = true;
        }

        const changeType = ( _isout : boolean ) => {
            isout.value = _isout;
        }

        return {
            ...toRefs( listmodeldata ) ,
            isout , show , toggle ,
            showtypedlg , typetxt ,
            showmomedlg , mometxt , isdisplaymometip , openmomedlg ,
            onAddType , typebeforeClose ,
            changeType ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       lang="less"
       src="./BillOperation.less">
</style>
