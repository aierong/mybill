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

interface IState {
    userselectyear : number,
    userselectmonth : number,

    isout : boolean,

    inlist : IStatBillObj[],
    oulist : IStatBillObj[],
    top5outlist : IBillObj[]
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

        const modeldata = reactive<IState>( {

            // 默认当月
            userselectyear : now.getFullYear() ,
            userselectmonth : 1 + now.getMonth() ,

            inlist : [] ,
            oulist : [] ,
            isout : true ,

            top5outlist : []
        } )

        const list = computed<IStatBillObj[]>( () => {
            if ( modeldata.isout ) {
                return modeldata.oulist;
            }

            return modeldata.inlist;
        } )

        const suminmoney = computed<number>( () => {
            return _.sumBy( modeldata.inlist , function ( item : IStatBillObj ) {
                return item.moneys
            } )
        } )

        const sumoutmoney = computed<number>( () => {
            return _.sumBy( modeldata.oulist , function ( item : IStatBillObj ) {
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
            let status = await billapi.getstatlist( modeldata.userselectyear , modeldata.userselectmonth , isout );

            if ( status.data.Success ) {
                if ( isout ) {
                    modeldata.oulist = status.data.Result;
                }
                else {
                    modeldata.inlist = status.data.Result;
                }

            }
            else {
                if ( isout ) {
                    modeldata.oulist = [];
                }
                else {
                    modeldata.inlist = [];
                }
            }
        }

        const gettoplist = async () => {
            let status = await billapi.gettoplist( modeldata.userselectyear , modeldata.userselectmonth , topnum , true );

            if ( status.data.Success ) {
                modeldata.top5outlist = status.data.Result;
            }
            else {
                modeldata.top5outlist = []
            }
        }

        onBeforeRouteLeave( ( to , from ) => {
            // 导航离开该组件的对应路由时调用
            // 离开时,记录一下,页面参数
            var payload : IStatPageData = {
                year : modeldata.userselectyear ,
                month : modeldata.userselectmonth ,
                isout : modeldata.isout

            };

            store.commit( UserMutationType.updatestatpagedata , payload )

        } )

        return {
            ...toRefs( modeldata ) , topnum ,
            suminmoney , sumoutmoney , list ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style lang="less"
       scoped
       src="./Stat.less">

</style>
