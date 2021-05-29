<!--

作者: chenghao
Date: 2021/4/30
Time: 17:48
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>
        <!--       日期选择-->
        <div class="selectdate">
            <span style="font-size: 28px"
                  @click="SelectYearMonth">{{ selectyyyymm }}</span>
            <van-icon size="25"
                      name="notes-o"
                      @click="SelectYearMonth"/>

            <!--    选择日期弹窗   -->
            <SelectYearMonthDialog ref="selectdateRef"
                                   @selectdate="userselectdate"
                                   :year="userselectyear"
                                   :month="userselectmonth"/>

        </div>

        <!--        一些汇总栏目-->
        <div class="sumouttxt">
            共支出
        </div>
        <div class="sunoutmoney">
            ¥{{ $FormatMoney( sumoutmoney ) }}
        </div>
        <div class="suninmoney">
            共收入¥{{ $FormatMoney( suminmoney ) }}
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
            <div class="typeitem">
                <!--                list-->
                <div v-for="(item,index) in list"
                     :key="index">
                    <van-row gutter="1">
                        <van-col span="8">
                            <aliicon :iconname="item.avatar"
                                     :iconsize="22"
                                     :colortypes="isout?'out':'in'"/>
                            <span style="margin-left: 10px;">{{ item.typename }}</span>
                            <span style="margin-left: 10px;">{{ $FormatPercent( item.ratio , 2 ) }}</span></van-col>
                        <van-col span="10">
                            <div style="margin-top: 8px;">
                                <van-progress :percentage="item.ratio*100"
                                              stroke-width="6px"
                                              :show-pivot="false"
                                              track-color="#ffffff"
                                              color="#39be77"></van-progress>
                            </div>
                        </van-col>
                        <van-col span="6">
                            <div>
                                <span class="typeitemmoney">¥{{ $FormatStatMoney( item.moneys ) }}</span>

                                <van-icon name="arrow"/>
                            </div>
                        </van-col>
                    </van-row>

                </div>
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
        <br><br><br>
        <mytabbar/>
    </div>

</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">

import { IBillObj , querymode } from "@/types";

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

import SelectYearMonthDialog from "@comp/popup/SelectYearMonthDialog.vue";
import { ISelectDateObj , ISelectBillTypeObj } from "@comp/types";

export default defineComponent( {
    // 子组件
    components : {
        outitemlist , SelectYearMonthDialog ,
    } ,
    // 声明 props
    props : {} ,
    setup () {
        const router = useRouter()
        const store = useStore( key )

        const selectdateRef = ref<typeof SelectYearMonthDialog | null>( null )

        const topnum : number = 5;

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

        const selectyyyymm = computed( () => {
            return `${ modeldata.userselectyear }年${ modeldata.userselectmonth }月`
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
            await RefreshAll();
        } )

        const RefreshAll = async () => {
            await getlist( true );
            await getlist( false );
            await gettoplist();
        }

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

        const SelectYearMonth = () => {

            if ( selectdateRef.value != null ) {
                selectdateRef.value.toggle();
            }
        }

        /**
         * 选择日期后,确定
         * @param val
         */
        const userselectdate = async ( val : ISelectDateObj ) => {
            // console.log( val )

            var isrefresh = false;

            //有可能选择的和之前一样的,这里判断一下
            if ( modeldata.userselectyear != val.year || modeldata.userselectmonth != val.month ) {
                modeldata.userselectyear = val.year;
                modeldata.userselectmonth = val.month;

                isrefresh = true;
            }

            // console.log( 'isrefresh' , isrefresh )

            if ( isrefresh ) {
                //再从新请求 服务器
                await RefreshAll();
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

            // 这里 默认按金额
            store.commit( UserMutationType.updateoutlistpagedata , 'money' )

            return;
        } )

        return {
            ...toRefs( modeldata ) , selectdateRef , topnum ,
            suminmoney , sumoutmoney , list , isdisplayoutmore , selectyyyymm ,
            onClickMore ,
            SelectYearMonth , userselectdate ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style lang="less"
       scoped
       src="./Stat.less">
</style>
