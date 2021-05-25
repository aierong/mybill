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
        <br>
        <!--        按类型得统计图表-->
        <div>
            <div>
                <span>收支构成</span>
                <span class="itemmoney">
                <span>支出</span> <span>收入</span>
                </span>
            </div>
        </div>
        <br>
        <!--         支出排行-->
        <div>
            <div>{{ userselectmonth }}月份支出排行</div>
        </div>

        <!--        tabbar-->
        <mytabbar/>
    </div>

</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">

import { IBillObj } from "@/types";

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
    top5outlist : IBillObj[]
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

import { useRouter , useRoute , onBeforeRouteLeave } from 'vue-router'
import { useStore } from 'vuex'
import { key } from '@store/index.ts'
import * as UserMutationType from '@store/mutations/mutation-types.ts'

import * as billapi from '@/http/api/bill'
import { gettoplist } from "@/http/api/bill";
import { IStatPageData } from "@store/types";

export default defineComponent( {
    // 子组件
    components : {} ,
    // 声明 props
    props : {} ,
    setup () {
        const store = useStore( key )

        const topnum : number = 5;

        var now = new Date();

        const querymodeldata = reactive<IQuery>( {
            isout : true ,

            // 默认当月
            userselectyear : now.getFullYear() ,
            userselectmonth : 1 + now.getMonth() ,

        } )

        const billmodeldata = reactive<IStatBillList>( {
            inlist : [] ,
            oulist : [] ,
            top5outlist : []
        } );

        const suminmoney = computed<number>( () => {
            return _.sumBy( billmodeldata.inlist , function ( item : IStatBillObj ) {
                return item.moneys
            } )
        } )

        const sumoutmoney = computed<number>( () => {
            return _.sumBy( billmodeldata.oulist , function ( item : IStatBillObj ) {
                return item.moneys
            } )
        } )

        onMounted( async () => {

            await getlist( true );
            await getlist( false );
            await gettoplist();
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

        const gettoplist = async () => {
            let status = await billapi.gettoplist( querymodeldata.userselectyear , querymodeldata.userselectmonth , topnum , true );

            if ( status.data.Success ) {
                billmodeldata.top5outlist = status.data.Result;
            }
            else {
                billmodeldata.top5outlist = []
            }
        }

        onBeforeRouteLeave( ( to , from ) => {
            // 导航离开该组件的对应路由时调用
            // 离开时,记录一下,页面参数
            var payload : IStatPageData = {
                year : querymodeldata.userselectyear ,
                month : querymodeldata.userselectmonth ,
                isout : querymodeldata.isout

            };

            store.commit( UserMutationType.updatestatpagedata , payload )

        } )

        return {
            ...toRefs( billmodeldata ) , ...toRefs( querymodeldata ) , topnum ,
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
