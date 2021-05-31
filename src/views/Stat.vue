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
            <div class="typehead">
                <span style="margin-left: 10px;">收支构成</span>
                <span class="itemmoney">
                    <span :class="{ outactive:typedata_isout , itemtxt:true  }"
                          @click="typeClick(true)">支出</span>
                    <span :class="{ inactive:!typedata_isout   ,itemtxt:true }"
                          @click="typeClick(false)">收入</span>
                </span>
            </div>
            <div class="typeitem">
                <div v-for="(item,index) in typedata_list"
                     :key="index">
                    <van-row gutter="0">
                        <van-col span="10">
                            <aliicon :iconname="item.avatar"
                                     :iconsize="22"
                                     :colortypes="typedata_isout?'out':'in'"/>
                            <span style="margin-left: 5px;">{{ item.typename }}</span>
                            <span style="margin-left: 5px;">{{ $FormatPercent( item.ratio , 2 ) }}</span></van-col>
                        <van-col span="8">
                            <div style="margin-top: 8px;">
                                <van-progress :percentage="item.ratio*100"
                                              stroke-width="6px"
                                              :show-pivot="false"
                                              track-color="#ffffff"
                                              color="#39be77"></van-progress>
                            </div>
                        </van-col>
                        <van-col span="6">
                            <div @click="typeitemClick(item.billtypeid,item.typename)">
                                <span class="typeitemmoney">¥{{ $FormatStatMoney( item.moneys ) }}</span>
                                <van-icon name="arrow"/>
                            </div>
                        </van-col>
                    </van-row>

                </div>
            </div>
        </div>
        <br>
        <!--        月度对比-->
        <div>
            <div>
                <span>月度对比</span>
                <span>
                    <span @click="monthtypeClick(true)">支出</span>
                    <span @click="monthtypeClick(false)">收入</span>
                </span>
            </div>
        </div>
        <br>
        <!--         支出排行-->
        <div>
            <div>{{ userselectmonth }}月份支出排行</div>
            <itemlist :list="topoutlist"
                      @deleteitemresult="deleteitemresult"/>
            <!--            <br>-->
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

interface IMonthStatObj {
    moneymonth : number,
    moneyyear : number,
    moneys : number,
}

interface IDayStatObj {
    moneyday : number,
    moneymonth : number,
    moneyyear : number,
    moneys : number,
}

interface IState {
    userselectyear : number,
    userselectmonth : number,

    typedata_isout : boolean,

    typedata_inlist : IStatBillObj[],
    typedata_outlist : IStatBillObj[],

    topoutlist : IBillObj[],
    //支出记录数量
    outlistcounts : number,

    monthstat_isout : boolean,
    monthstat_inlist : IMonthStatObj[],
    monthstat_outlist : IMonthStatObj[],

    daystat_isout : boolean,
    daystat_inlist : IDayStatObj[],
    daystat_outlist : IDayStatObj[],
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

import { IStatPageData , IDataListPageData } from "@store/types";

import itemlist from "@comp/itemlist.vue";

import SelectYearMonthDialog from "@comp/popup/SelectYearMonthDialog.vue";
import { ISelectDateObj , ISelectBillTypeObj } from "@comp/types";
import { getstatdaylist } from "@/http/api/bill";

export default defineComponent( {
    // 子组件
    components : {
        itemlist ,
        SelectYearMonthDialog ,
    } ,
    // 声明 props
    props : {} ,
    setup () {
        //支出排行榜最大数量
        const outmaxtopnum : number = 10;

        const monthmaxnum : number = 6;

        const router = useRouter()
        const store = useStore( key )

        const selectdateRef = ref<typeof SelectYearMonthDialog | null>( null )

        var now = new Date();

        const modeldata = reactive<IState>( {

            // 默认当月
            userselectyear : now.getFullYear() ,
            userselectmonth : 1 + now.getMonth() ,

            typedata_inlist : [] ,
            typedata_outlist : [] ,

            typedata_isout : true ,

            topoutlist : [] ,
            outlistcounts : 0 ,

            monthstat_isout : true ,
            monthstat_inlist : [] ,
            monthstat_outlist : [] ,

            daystat_isout : true ,
            daystat_inlist : [] ,
            daystat_outlist : [] ,
        } )

        const MonthYVal = computed<number[]>( () => {
            if ( modeldata.monthstat_isout ) {
                return modeldata.monthstat_outlist.map( item => item.moneys )
            }
            else {
                return modeldata.monthstat_inlist.map( item => item.moneys )
            }
        } )

        const MonthXVal = computed<string[]>( () => {
            if ( modeldata.monthstat_isout ) {
                return modeldata.monthstat_outlist.map( item => item.moneymonth + '月' )
            }
            else {
                return modeldata.monthstat_inlist.map( item => item.moneymonth + '月' )
            }
        } )

        const selectyyyymm = computed( () => {
            return `${ modeldata.userselectyear }年${ modeldata.userselectmonth }月`
        } )

        const isdisplayoutmore = computed( () => {
            return modeldata.outlistcounts > outmaxtopnum;
        } )

        const typedata_list = computed<IStatBillObj[]>( () => {
            if ( modeldata.typedata_isout ) {
                return modeldata.typedata_outlist;
            }

            return modeldata.typedata_inlist;
        } )

        const suminmoney = computed<number>( () => {
            return _.sumBy( modeldata.typedata_inlist , function ( item : IStatBillObj ) {
                return item.moneys
            } )
        } )

        const sumoutmoney = computed<number>( () => {
            return _.sumBy( modeldata.typedata_outlist , function ( item : IStatBillObj ) {
                return item.moneys
            } )
        } )

        const RefreshAll = async () => {
            await gettypedatalist( true );
            await gettypedatalist( false );

            await gettoplist();

            await getstatmonthlist( true );
            await getstatmonthlist( false );

            await getstatdaylist( true );
            await getstatdaylist( false );
        }

        const getstatdaylist = async ( isout : boolean ) => {
            const { data } = await billapi.getstatdaylist( modeldata.userselectyear ,
                modeldata.userselectmonth ,
                isout );

            if ( data.Success ) {
                if ( isout ) {
                    modeldata.daystat_outlist = data.Result;
                }
                else {
                    modeldata.daystat_inlist = data.Result;
                }

            }
            else {
                if ( isout ) {
                    modeldata.daystat_outlist = [];
                }
                else {
                    modeldata.daystat_inlist = [];
                }
            }
        }

        const getstatmonthlist = async ( isout : boolean ) => {
            const { data } = await billapi.getstatmonthlist( modeldata.userselectyear ,
                modeldata.userselectmonth ,
                isout , monthmaxnum );

            if ( data.Success ) {
                if ( isout ) {
                    modeldata.monthstat_outlist = data.Result;
                }
                else {
                    modeldata.monthstat_inlist = data.Result;
                }

            }
            else {
                if ( isout ) {
                    modeldata.monthstat_outlist = [];
                }
                else {
                    modeldata.monthstat_inlist = [];
                }
            }
        }

        const gettypedatalist = async ( isout : boolean ) => {
            const { data } = await billapi.getstatlist( modeldata.userselectyear ,
                modeldata.userselectmonth , isout );

            if ( data.Success ) {
                if ( isout ) {
                    modeldata.typedata_outlist = data.Result;
                }
                else {
                    modeldata.typedata_inlist = data.Result;
                }

            }
            else {
                if ( isout ) {
                    modeldata.typedata_outlist = [];
                }
                else {
                    modeldata.typedata_inlist = [];
                }
            }
        }

        const gettoplist = async () => {
            const { data } = await billapi.gettopoutlist( modeldata.userselectyear ,
                modeldata.userselectmonth ,
                outmaxtopnum );

            if ( data.Success ) {
                modeldata.topoutlist = data.Result.list;
                modeldata.outlistcounts = data.Result.counts;
            }
            else {
                modeldata.topoutlist = []
                modeldata.outlistcounts = 0;
            }
        }

        const onClickMore = () => {
            //记录一下,当时的类型和类型名称, 这里记录没有
            store.commit( UserMutationType.updatedatalistpagebilltype , {
                billtypetxt : '' ,
                billtypeid : 0
            } )

            //true
            store.commit( UserMutationType.updatedatalistpageisout , true );

            router.push( '/datalist' )

            return;
        }

        const typeitemClick = ( billtypeid : number , billtypetxt : string ) => {
            //记录一下,当时的类型和类型名称
            store.commit( UserMutationType.updatedatalistpagebilltype , {
                billtypetxt ,
                billtypeid
            } )

            store.commit( UserMutationType.updatedatalistpageisout , modeldata.typedata_isout );

            router.push( '/datalist' )

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

        const deleteitemresult = async ( isok : boolean ) => {
            await RefreshAll();
        }

        const typeClick = ( isout : boolean ) => {
            if ( modeldata.typedata_isout != isout ) {
                modeldata.typedata_isout = isout;
            }
        }

        const monthtypeClick = ( isout : boolean ) => {
            if ( modeldata.monthstat_isout != isout ) {
                modeldata.monthstat_isout = isout;
            }
        }

        onMounted( async () => {
            await RefreshAll();
        } )

        onBeforeRouteLeave( ( to , from ) => {
            // 导航离开该组件的对应路由时调用
            // 离开时,记录一下,页面参数
            var payload : IStatPageData = {
                year : modeldata.userselectyear ,
                month : modeldata.userselectmonth ,
                typedata_isout : modeldata.typedata_isout

            };

            store.commit( UserMutationType.updatestatpagedata , payload )

            // 这里 默认按金额
            store.commit( UserMutationType.updatedatalistpagemode , 'money' )

            return;
        } )

        return {
            ...toRefs( modeldata ) ,
            selectdateRef ,
            suminmoney , sumoutmoney , typedata_list , isdisplayoutmore , selectyyyymm ,
            MonthXVal , MonthYVal ,
            onClickMore ,
            SelectYearMonth , userselectdate ,
            deleteitemresult , typeitemClick , typeClick ,
            monthtypeClick ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style lang="less"
       scoped
       src="./Stat.less">
</style>
