<!--

作者: chenghao
Date: 2021/5/10
Time: 11:38
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <van-popup :close-on-click-overlay="false"
               v-model:show="show"
               position="bottom">
        <van-datetime-picker type="year-month"
                             :formatter="formatter"
                             title="选择年月"
                             v-model="currentDate"
                             @cancel="onCancel"
                             @confirm="onConfirm"/>
    </van-popup>

</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">

// 导入
import {
    defineComponent ,
    ref ,
    reactive ,
    toRefs ,
    computed , watch ,
} from "vue";

import { ISelectDateObj } from "@comp/types";

export default defineComponent( {
    // 定义是事件
    emits : {
        selectdate : ( val : ISelectDateObj ) => {

            //上面已经定义了参数类型,系统会验证的参数类型

            return true
        } ,
        dialogclose : null
    } ,
    // 声明 props
    props : {
        year : {
            type : Number ,
            required : true
        } ,
        month : {
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

        const currentDate = ref( new Date() );

        watch(
            () => props.dialogshow ,
            ( newval ) => {

                show.value = newval;
            } ,
            {
                // 这里如果不设置immediate = true,那么最初绑定的时候是不会执行的,要等到num改变时才执行监听计算
                immediate : true
            }
        )

        watch(
            [ () => props.year , () => props.month ] ,
            ( [ newyaer , newmonth ] ) => {

                currentDate.value = new Date( newyaer , newmonth - 1 , 1 )
            } ,
            {
                // 这里如果不设置immediate = true,那么最初绑定的时候是不会执行的,要等到num改变时才执行监听计算
                immediate : true
            }
        )

        const formatter = ( type : string , val : string ) => {
            if ( type === 'year' ) {
                return `${ val }年`;
            }

            if ( type === 'month' ) {
                return `${ val }月`;
            }

            return val;
        };

        const onConfirm = ( value : Date ) => {
            // console.log( 'val' , value )

            currentDate.value = value;

            let payload : ISelectDateObj = {
                year : currentDate.value.getFullYear() ,
                month : 1 + currentDate.value.getMonth() ,
            }

            // 通知父窗体
            emit( "selectdate" , payload );
            emit( "dialogclose" )

        }

        const onCancel = () => {

            emit( "dialogclose" )
        }

        return {
            show , currentDate , formatter ,
            onConfirm , onCancel ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style>

</style>
