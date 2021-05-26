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
            <span>总收入¥{{ $FormatMoney( suminmoney ) }}</span>
            <br>
            <span>总支出¥{{ $FormatMoney( sumoutmoney ) }}</span>
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
            <outitemlist :list="topoutlist"/>
            <br>
            <van-divider v-if="isdisplayoutmore"
                         :style="{  padding: '0 16px' }">
                <span @click="onClickMore"
                      style="font-weight: bold;">全部排行 &gt;&gt;</span>
            </van-divider>
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
    outlist : IStatBillObj[],

    topoutlist : IBillObj[],
    outlistcounts : number,
}

// 引入lodash
import * as _ from "lodash"

// 导入
import {
    defineComponent ,
    ref ,
    reactive ,
    toRefs ,
    computed ,
    onMounted ,
} from "vue";

import { useRouter , useRoute , onBeforeRouteLeave } from 'vue-router'
import { useStore } from 'vuex'
import { key } from '@store/index.ts'
import * as UserMutationType from '@store/mutations/mutation-types.ts'

import * as billapi from '@/http/api/bill'

import { IStatPageData } from "@store/types";

import outitemlist from "@comp/outitemlist.vue";

export default defineComponent( {
    // 子组件
    components : {
        outitemlist ,
    } ,
    // 声明 props
    props : {} ,
    setup () {
        const router = useRouter()
        const store = useStore( key )

        const topnum : number = 10;

        var now = new Date();

        const modeldata = reactive<IState>( {

            // 默认当月
            userselectyear : now.getFullYear() ,
            userselectmonth : 1 + now.getMonth() ,

            inlist : [] ,
            outlist : [] ,
            isout : true ,

            topoutlist : [] ,
            outlistcounts : 0 ,
        } )

        const isdisplayoutmore = computed( () => {
            return modeldata.outlistcounts > topnum;
        } )

        const list = computed<IStatBillObj[]>( () => {
            if ( modeldata.isout ) {
                return modeldata.outlist;
            }

            return modeldata.inlist;
        } )

        const suminmoney = computed<number>( () => {
            return _.sumBy( modeldata.inlist , function ( item : IStatBillObj ) {
                return item.moneys
            } )
        } )

        const sumoutmoney = computed<number>( () => {
            return _.sumBy( modeldata.outlist , function ( item : IStatBillObj ) {
                return item.moneys
            } )
        } )

        onMounted( async () => {

            await getlist( true );
            await getlist( false );
            await gettoplist();

        } )

        const getlist = async ( isout : boolean ) => {
            let status = await billapi.getstatlist( modeldata.userselectyear , modeldata.userselectmonth , isout );

            if ( status.data.Success ) {
                if ( isout ) {
                    modeldata.outlist = status.data.Result;
                }
                else {
                    modeldata.inlist = status.data.Result;
                }

            }
            else {
                if ( isout ) {
                    modeldata.outlist = [];
                }
                else {
                    modeldata.inlist = [];
                }
            }
        }

        const gettoplist = async () => {
            let status = await billapi.gettopoutlist( modeldata.userselectyear , modeldata.userselectmonth , topnum );

            if ( status.data.Success ) {
                modeldata.topoutlist = status.data.Result.list;
                modeldata.outlistcounts = status.data.Result.counts;
            }
            else {
                modeldata.topoutlist = []
                modeldata.outlistcounts = 0;
            }
        }

        const onClickMore = () => {
            router.push( '/outlist' )

            return;
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

            return;
        } )

        return {
            ...toRefs( modeldata ) , topnum ,
            suminmoney , sumoutmoney , list , isdisplayoutmore ,
            onClickMore ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style lang="less"
       scoped
       src="./Stat.less">
</style>
