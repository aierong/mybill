<!--

作者: chenghao
Date: 2021/4/30
Time: 17:48
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>
        <div class="pagehead">
            <!--       日期选择-->
            <div class="selectdate">

                    <span style="font-size: 25px"
                          @click="SelectYearMonth">{{ selectyyyymm }}
                    </span>
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
        </div>

        <br>
        <!--        按类型得统计图表-->
        <div class="typestat">
            <div class="head">
                <span class="title">分类构成</span>
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
        <van-divider dashed
                     :style="{ color: '#010d18', borderColor: '#010d18',   padding: '0 16px' }">
        </van-divider>
        <br>
        <!--        每日对比-->
        <div class="daystat">
            <div class="head">
                <span class="title">每日对比</span>
                <span class="itemmoney">
                    <span :class="{ outactive:daystat_isout , itemtxt:true  }"
                          @click="daytypeClick(true)">支出</span>
                    <span :class="{ inactive:!daystat_isout   ,itemtxt:true }"
                          @click="daytypeClick(false)">收入</span>
                </span>
            </div>
            <!-- 为ECharts准备一个具备大小（宽高）的Dom -->
            <div style="overflow-x: auto;">
                <div id="daychart"
                     ref="daychart"
                     style="height: 300px;width: 1300px;">
                </div>
            </div>
        </div>
        <van-divider dashed
                     :style="{ color: '#010d18', borderColor: '#010d18',   padding: '0 16px' }">
        </van-divider>
        <br>
        <!--        月度对比-->
        <div class="monthstat">
            <div class="head">
                <span class="title">月度对比</span>
                <span class="itemmoney">
                    <span :class="{ outactive:monthstat_isout , itemtxt:true  }"
                          @click="monthtypeClick(true)">支出</span>
                    <span :class="{ inactive:!monthstat_isout   ,itemtxt:true }"
                          @click="monthtypeClick(false)">收入</span>
                </span>
            </div>
            <!-- 为ECharts准备一个具备大小（宽高）的Dom -->
            <div>
                <div id="monthchart"
                     ref="monthchart"
                     style="height: 300px;">
                </div>
            </div>
        </div>
        <van-divider dashed
                     :style="{ color: '#010d18', borderColor: '#010d18',   padding: '0 16px' }">
        </van-divider>
        <br>
        <!--         支出排行-->
        <div class="outtop">
            <div class="title">{{ userselectmonth }}月份支出排行</div>
            <itemlist :list="topoutlist"
                      @deleteitemresult="deleteitemresult"/>

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
    getCurrentInstance , ComponentInternalInstance ,
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

import * as echarts from 'echarts';

export default defineComponent( {
    // 子组件
    components : {
        itemlist ,
        SelectYearMonthDialog ,
    } ,
    // 声明 props
    props : {} ,
    setup () {

        const { proxy } = getCurrentInstance() as ComponentInternalInstance;

        //支出排行榜最大数量
        const outmaxtopnum : number = 10;

        const monthmaxnum : number = 6;

        const router = useRouter()
        const store = useStore( key )

        const selectdateRef = ref<typeof SelectYearMonthDialog | null>( null )

        const monthchart = ref( null );

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

        let MonthChart;

        var monthoption = {
            xAxis : {
                type : 'category' ,
                //data : [ 'Mon' , 'Tue' , 'Wed' , 'Thu' , 'Fri' , 'Sat' , 'Sun' ]
                data : []
            } ,
            yAxis : {
                type : 'value' ,
                show : false ,
            } ,
            series : [
                {
                    //data : [ 120 , 200 , 150 , 80 , 70 , 110 , 130 ] ,
                    data : [] ,
                    type : 'bar' ,
                    label : {
                        show : true ,
                        position : 'top' ,
                        formatter : function ( params ) {
                            // console.log( 'params' , params )
                            //0不显示
                            if ( parseFloat( params.value ) <= 0 ) {
                                return '';
                            }

                            if ( proxy != null ) {
                                return proxy.$FormatStatMoney( params.value , 2 );
                            }
                            else {
                                return params.value;
                            }

                        }

                    } ,
                    itemStyle : {
                        color : '#ECBE25'
                    }
                }

            ]
        };

        let DayChart;

        var dayoption = {
            xAxis : {
                type : 'category' ,
                //data : [ 'Mon' , 'Tue' , 'Wed' , 'Thu' , 'Fri' , 'Sat' , 'Sun' ]
                data : []
            } ,
            yAxis : {
                type : 'value' ,
                show : false ,
            } ,
            series : [
                {
                    //data : [ 120 , 200 , 150 , 80 , 70 , 110 , 130 ] ,
                    data : [] ,
                    type : 'bar' ,
                    label : {
                        show : true ,
                        position : 'top' ,
                        formatter : function ( params ) {
                            // console.log( 'params' , params )
                            //0不显示
                            if ( parseFloat( params.value ) <= 0 ) {
                                return '';
                            }

                            if ( proxy != null ) {
                                return proxy.$FormatStatMoney( params.value , 2 );
                            }
                            else {
                                return params.value;
                            }

                        }

                    } ,
                    itemStyle : {
                        color : '#ECBE25'
                    }
                }

            ]
        };

        const MonthChartList = computed<IMonthStatObj[]>( () => {
            if ( modeldata.monthstat_isout ) {
                return modeldata.monthstat_outlist;
            }
            else {
                return modeldata.monthstat_inlist;
            }
        } )

        // 判断是否跨年,例如:同时有12月和1月
        const IsMonthChartStrideYear = computed<boolean>( () => {

            return MonthChartList.value.some( item => item.moneymonth == 1 ) && modeldata.monthstat_inlist.some( item => item.moneymonth == 12 )
        } )

        const MonthChartYVal = computed<number[]>( () => {

            return MonthChartList.value.map( item => item.moneys )
        } )

        const MonthChartXVal = computed<string[]>( () => {

            return MonthChartList.value.map( ( item ) => {
                if ( IsMonthChartStrideYear.value ) {
                    if ( item.moneymonth == 1 || item.moneymonth == 12 ) {
                        return item.moneymonth + '月' + '\n' + item.moneyyear;
                    }

                }

                return item.moneymonth + '月'
            } )
        } )

        const DayChartList = computed<IDayStatObj[]>( () => {
            if ( modeldata.daystat_isout ) {
                return modeldata.daystat_outlist;
            }
            else {
                return modeldata.daystat_inlist;
            }
        } )

        // 判断是否跨月,例如:同时有2个月
        const IsDayChartStrideMonth = computed<boolean>( () => {

            var isevery : boolean = true;

            if ( DayChartList.value.length > 0 ) {
                // 取第一个出来
                var _month : number = DayChartList.value[ 0 ].moneymonth;

                isevery = DayChartList.value.every( item => item.moneymonth == _month );
            }

            return !isevery;
        } )

        // 最小那个月份的,最大那天(也就是该月最后一天)
        const DayMinMonthData = computed<IDayStatObj | null>( () => {
            if ( DayChartList.value.length > 0 ) {
                let _minmonth : IDayStatObj = _.minBy( DayChartList.value , item => item.moneymonth );

                let _max : IDayStatObj = _.maxBy( DayChartList.value , ( item : IDayStatObj ) => {
                    if ( item.moneyyear == _minmonth.moneyyear && item.moneymonth == _minmonth.moneymonth ) {
                        return item.moneyday;
                    }

                    return 0;
                } )

                return _max;
            }

            return null;
        } )

        const DayChartYVal = computed<number[]>( () => {
            return DayChartList.value.map( item => item.moneys )
        } )

        const DayChartXVal = computed<string[]>( () => {

            return DayChartList.value.map( ( item : IDayStatObj ) => {

                var _date : Date = new Date( item.moneyyear , item.moneymonth - 1 , item.moneyday );
                var _week : number = _date.getDay();

                var _weekstr : string = "";

                switch ( _week ) {
                    case 1:
                        _weekstr = '一'
                        break;
                    case 2:
                        _weekstr = '二'
                        break;
                    case 3:
                        _weekstr = '三'
                        break;
                    case 4:
                        _weekstr = '四'
                        break;
                    case 5:
                        _weekstr = '五'
                        break;
                    case 6:
                        _weekstr = '六'
                        break;
                    default:
                        _weekstr = '日'
                }

                if ( IsDayChartStrideMonth.value ) {
                    var isbrdate : boolean = false;

                    //是第1天
                    if ( item.moneyday == 1 ) {
                        isbrdate = true;
                    }

                    //是该月最后一天
                    if ( DayMinMonthData.value != null ) {
                        if ( DayMinMonthData.value.moneyday == item.moneyday
                            && DayMinMonthData.value.moneymonth == item.moneymonth
                            && DayMinMonthData.value.moneyyear == item.moneyyear ) {
                            isbrdate = true;
                        }
                    }

                    if ( isbrdate ) {

                        return _weekstr + '\n' + `${ item.moneymonth }.${ item.moneyday }`;
                    }

                }

                return _weekstr + '\n' + item.moneyday;
            } )
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

                setupmonthchartdata();
                setupdaychartdata();
            }
        }

        const deleteitemresult = async ( isok : boolean ) => {
            await RefreshAll();

            setupmonthchartdata();
            setupdaychartdata();
        }

        const typeClick = ( isout : boolean ) => {
            if ( modeldata.typedata_isout != isout ) {
                modeldata.typedata_isout = isout;
            }
        }

        const setupmonthchartdata = () => {
            MonthChart.setOption( {
                series : [
                    {
                        // series中其他属性我没有动，没有变化，所有我就修改data
                        data : MonthChartYVal.value ,
                        itemStyle : {
                            color : modeldata.monthstat_isout ? '#39be77' : '#ECBE25'
                        }
                    }
                ] ,
                // x轴数据如果变化，就调整这里
                xAxis : {
                    data : MonthChartXVal.value
                } ,
            } );
        }

        const setupdaychartdata = () => {
            DayChart.setOption( {
                series : [
                    {
                        // series中其他属性我没有动，没有变化，所有我就修改data
                        data : DayChartYVal.value ,
                        itemStyle : {
                            color : modeldata.daystat_isout ? '#39be77' : '#ECBE25'
                        }
                    }
                ] ,
                // x轴数据如果变化，就调整这里
                xAxis : {
                    data : DayChartXVal.value
                } ,
            } );
        }

        const monthtypeClick = ( isout : boolean ) => {
            if ( modeldata.monthstat_isout != isout ) {
                modeldata.monthstat_isout = isout;

                setupmonthchartdata();

            }

        }

        const daytypeClick = ( isout : boolean ) => {
            if ( modeldata.daystat_isout != isout ) {
                modeldata.daystat_isout = isout;

                setupdaychartdata();
            }
        }

        const initdata = () => {
            //有可能vuex中记录了,上次的记录,取回来用

            if ( store.state.StatPageData != null ) {

                modeldata.userselectyear = store.state.StatPageData.year;
                modeldata.userselectmonth = store.state.StatPageData.month;

                modeldata.typedata_isout = store.state.StatPageData.typedata_isout;
                modeldata.monthstat_isout = store.state.StatPageData.monthstat_isout;
                modeldata.daystat_isout = store.state.StatPageData.daystat_isout;

            }
        }

        onMounted( async () => {
            initdata();

            await RefreshAll();

            var monthchartDom : any = document.getElementById( 'monthchart' );
            MonthChart = echarts.init( monthchartDom );

            // 为echarts对象加载数据
            MonthChart.setOption( monthoption );

            //
            var daychartDom : any = document.getElementById( 'daychart' );
            DayChart = echarts.init( daychartDom );

            // 为echarts对象加载数据
            DayChart.setOption( dayoption );

            setupmonthchartdata();
            setupdaychartdata();
        } )

        onBeforeRouteLeave( ( to , from ) => {
            // 导航离开该组件的对应路由时调用
            // 离开时,记录一下,页面参数
            var payload : IStatPageData = {
                year : modeldata.userselectyear ,
                month : modeldata.userselectmonth ,
                typedata_isout : modeldata.typedata_isout ,
                monthstat_isout : modeldata.monthstat_isout ,
                daystat_isout : modeldata.daystat_isout ,
            };

            store.commit( UserMutationType.updatestatpagedata , payload )

            // 这里 默认按金额
            store.commit( UserMutationType.updatedatalistpagemode , 'money' )

            return;
        } )

        return {
            ...toRefs( modeldata ) ,
            monthchart ,
            selectdateRef ,
            suminmoney , sumoutmoney , typedata_list , isdisplayoutmore , selectyyyymm ,
            MonthChartList , IsMonthChartStrideYear , MonthChartXVal , MonthChartYVal ,

            DayChartList , IsDayChartStrideMonth , DayMinMonthData , DayChartYVal , DayChartXVal ,

            onClickMore ,
            SelectYearMonth , userselectdate ,
            deleteitemresult , typeitemClick , typeClick ,
            monthtypeClick , daytypeClick ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style lang="less"
       scoped
       src="./Stat.less">
</style>

