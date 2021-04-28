<!--

作者: chenghao
Date: 2021/4/27
Time: 11:50
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>
        <van-dialog :before-close="beforeClose"
                    show-cancel-button
                    v-model:show="avatarobj.showdialog">
            <van-cell title="请选择头像">
            </van-cell>
            <van-radio-group v-model="selectavatar">
                <van-cell-group>
                    <van-cell v-for="(item,index) in avatariconlist"
                              :key="index"
                              clickable
                              @click="selectavatar = item">
                        <template #title>
                            <van-icon :name="item"
                                      size="36"/>
                        </template>
                        <template #right-icon>
                            <van-radio :name="item"/>
                        </template>
                    </van-cell>
                </van-cell-group>
            </van-radio-group>
        </van-dialog>
    </div>
</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">

// 导入
import {
    defineComponent ,
    // 注意导入PropType
    PropType ,
    ref ,
    reactive ,
    toRefs ,
    toRef ,
    computed
} from "vue";

import { avatariconlist } from '@common/constant.ts'
import { IAvatarObj } from '@comp/types'

export default defineComponent( {
    // 声明 props
    props : {
        avatarobj : {
            type : Object as PropType<IAvatarObj> ,
            required : true ,
        }
    } ,
    emits : {
        // 验证userselectavatar事件
        userselectavatar : ( value : string ) => {
            //上面已经定义了参数类型,系统会验证的参数类型

            return true
        } ,
    } ,
    setup ( props , { emit } ) {
        const selectavatar = ref<string>( props.avatarobj.avatar );

        // console.log( 'props' , props , props.avatarobj.avatar )

        const beforeClose = ( action ) => {
            if ( action === "confirm" ) {
                emit( "userselectavatar" , selectavatar.value );
            }
            else {
                // 点击了取消按钮
                emit( "userselectavatar" , '' );

                // return true;
            }
        }

        return {
            selectavatar ,

            avatariconlist ,

            beforeClose ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style>

</style>
