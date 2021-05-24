<!--

作者: chenghao
Date: 2021/4/30
Time: 17:48
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>
        <!--        一些汇总栏目-->
        <div>
            <span>总收入¥{{ $FormarMoney( suminmoney ) }}</span>
            <br>
            <span>总支出¥{{ $FormarMoney( sumoutmoney ) }}</span>
        </div>
        <!--        按类型得统计图表-->
        <div>
            <div>
                <span>收支构成</span>
                <span class="itemmoney">
                <span>支出</span> <span>收入</span>
                </span>
            </div>
        </div>

        <!--        tabbar-->
        <mytabbar/>
    </div>

</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">

import { IBillObj , QueryType } from "@/types";

interface IStatBillObj {

    billtypeid : number,
    typename : string,
    avatar : string,
    moneys : number,
    ratio : number,
}

interface IStatBillList {
    inlist : IStatBillObj[],
    oulist : IStatBillObj[],
}

interface IQuery {
    userselectyear : number,
    userselectmonth : number,

    isout : boolean
}

// 引入lodash
import * as _ from "lodash"

// 导入
import {
    defineComponent ,
    ref ,
    reactive ,
    toRefs ,
    computed , onMounted ,
} from "vue";

import * as billapi from '@/http/api/bill'

export default defineComponent( {
    // 子组件
    components : {} ,
    // 声明 props
    props : {} ,
    setup () {

        var now = new Date();

        const querymodeldata = reactive<IQuery>( {
            isout : true ,

            // 默认当月
            userselectyear : now.getFullYear() ,
            userselectmonth : 1 + now.getMonth() ,

        } )

        const billmodeldata = reactive<IStatBillList>( {
            inlist : [] ,
            oulist : []
        } );

        const suminmoney = computed<number>( () => {
            let _sum = _.sumBy( billmodeldata.inlist , function ( item : IStatBillObj ) {
                return item.moneys
            } )

            return _sum;
        } )

        const sumoutmoney = computed<number>( () => {
            return _.sumBy( billmodeldata.oulist , function ( item : IStatBillObj ) {
                return item.moneys
            } )
        } )

        onMounted( async () => {

            await getlist( true );
            await getlist( false );

            //console.log( 'status' , outstatus , instatus )

        } )

        const getlist = async ( isout : boolean ) => {
            let status = await billapi.getstatlist( querymodeldata.userselectyear , querymodeldata.userselectmonth , isout );

            if ( status.data.Success ) {
                if ( isout ) {
                    billmodeldata.oulist = status.data.Result;
                }
                else {
                    billmodeldata.inlist = status.data.Result;
                }

            }
            else {
                if ( isout ) {
                    billmodeldata.oulist = [];
                }
                else {
                    billmodeldata.inlist = [];
                }
            }
        }

        return {
            ...toRefs( billmodeldata ) , ...toRefs( querymodeldata ) ,
            suminmoney , sumoutmoney ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style lang="less"
       scoped
       src="./Stat.less">

</style>
