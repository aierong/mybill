<!--

作者: chenghao
Date: 2021/4/30
Time: 17:52
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>
    <div>
        <div style="background-color: #3EB575;">
            <div style="padding-top: 8px;">
                <!--                <span style="margin-left: 10px;color: white;">{{ userselectbilltypetxt }}</span>-->
                <!--                <van-icon name="apps-o"-->
                <!--                          color="white"/>-->

                <van-button hairline
                            @click="onBillTypeSelect"
                            color="#3EB575"
                            size="small">{{ userselectbilltypetxt }} |
                    <van-icon name="apps-o"/>
                </van-button>

                <SelectBillTypeDialog @selectbilltype="userselectbilltype"
                                      @dialogclose="userclosebilltype"
                                      :selectbilltypeid="userselectbilltypeid"
                                      :dialogshow="billtypeselectdialogshow"></SelectBillTypeDialog>
            </div>
            <div style="padding-top: 8px;padding-bottom: 8px;">
                <span style="margin-left: 10px;color: white;"
                      @click="SelectYearMonth">{{ selectymtxt }}</span>
                <van-icon name="arrow-down"
                          color="white"
                          @click="SelectYearMonth"/>
                <span v-show="isdisplayin"
                      class="summoneytxt">总收入¥{{ FormatNumber( suminmoney ) }}</span>
                <span v-show="isdisplayout"
                      class="summoneytxt">总支出¥{{ FormatNumber( sumoutmoney ) }}</span>
                <SelectYearMonthDialog @selectdate="userselectdate"
                                       @dialogclose="userclosedate"
                                       :year="userselectyear"
                                       :month="userselectmonth"
                                       :dialogshow="dateselectdialogshow"></SelectYearMonthDialog>
            </div>
        </div>
        <div :key="index"
             v-for="(item,index) in displaylist">
            <van-cell-group>
                <template #title>
                    <span>{{ item.moneydate }}</span><span style="margin-left: 5px;">{{ getweekstring( item.week ) }}</span>
                    <span style="position: absolute;right: 28px;">
                  <span v-show="isdisplayin">
                     <span style="background-color: #f5f0f0;">收</span>
                     <span>{{ FormatNumber( item.sumin ) }}</span>
                  </span>
                  <span style="margin-left: 10px;"
                        v-show="isdisplayout">
                      <span style="background-color: #f5f0f0;">支</span>
                      <span>{{ FormatNumber( item.sumout ) }}</span>
                  </span>

                </span>

                </template>
                <van-cell v-for="(mxitem,mxindex) in item.list"
                          :key="mxindex">
                    <template #title>
                        <aliicon :iconname="mxitem.avatar"
                                 :iconsize="22"
                                 :iconcolor="getcolor(mxitem.isout)"/>
                        <span style="margin-left: 5px;">{{ mxitem.typename }}</span>

                        <span style="position: absolute;right: 28px;">
                    <span :style="getcolorobject(mxitem.isout)">{{ mxitem.moneys }}</span>
                    </span>
                    </template>
                </van-cell>
            </van-cell-group>
        </div>
    </div>
</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">/**
 * 查询类型
 */
type QueryType = "all" | "out" | "in";

interface IBillObj {
    ids : number,

    mobile : string,
    billtypeid : number,
    typename : string,
    avatar : string,
    isout : boolean,
    moneys : number,
    moneydate : string,

    moneyyear : number,
    moneymonth : number,
    moneyday : number,
    memo : string,
    sources : string,
    adddate : string,
    updatedate : string,
    deletedate : string,
    delmark : string,

}

interface IBillList {
    list : IBillObj[]
}

interface IDisplayDayBill {
    moneydate : string,
    sumout : number,
    sumin : number,
    week : number,

    list : IBillObj[]
}

interface IQuery {
    userselectyear : number,
    userselectmonth : number,
    userselectbilltypeid : number,
    userselectbilltypetxt : string,
    querytype : QueryType
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

import * as billapi from '@/http/api/bill'
// 引入lodash
import * as _ from "lodash"
import dayjs from 'dayjs'

import { FormatNumber } from '@common/util'

import SelectYearMonthDialog from "@comp/SelectYearMonthDialog.vue";
import { ISelectDateObj , ISelectBillTypeObj } from "@comp/types";

import SelectBillTypeDialog from "@comp/SelectBillTypeDialog.vue";

export default defineComponent( {
    // 子组件
    components : {
        SelectYearMonthDialog , SelectBillTypeDialog ,
    } ,
    setup () {
        var now = new Date();
        const incolor : string = '#63e945'
        const outcolor : string = '#E98545'

        const dateselectdialogshow = ref( false )
        const billtypeselectdialogshow = ref( false )

        const billmodeldata = reactive<IBillList>( {
            list : []
        } );

        const querymodeldata = reactive<IQuery>( {
            querytype : "all" ,

            // 默认当月
            userselectyear : now.getFullYear() ,
            userselectmonth : 1 + now.getMonth() ,

            // 默认所有类型
            userselectbilltypeid : 0 ,
            userselectbilltypetxt : '全部'
        } )

        const selectymtxt = computed( () => {
            return `${ querymodeldata.userselectyear }年${ querymodeldata.userselectmonth }月`
        } )

        const isqueryall = computed( () => {
            return querymodeldata.querytype == 'all';
        } )

        const isqueryout = computed( () => {
            return querymodeldata.querytype == 'out';
        } )

        const isqueryin = computed( () => {
            return querymodeldata.querytype == 'in';
        } )

        const isdisplayout = computed( () => {
            return isqueryout.value || isqueryall.value;
        } )

        const isdisplayin = computed( () => {
            return isqueryin.value || isqueryall.value;
        } )

        onMounted( () => {
            // let status = await billapi.getlist( 2021 , 1 , 0 );
            // // console.log( 'result' , status )
            // if ( status.data.Success ) {
            //     billmodeldata.list = status.data.Result;
            // }
            // else {
            //     billmodeldata.list = [];
            // }

            getlist();

        } )

        const getlist = async () => {
            // let status = await billapi.getlist( 2021 , 1 , 0 );
            let status = await billapi.getlist( querymodeldata.userselectyear ,
                querymodeldata.userselectmonth ,
                querymodeldata.userselectbilltypeid );

            // console.log( 'result' , status )
            if ( status.data.Success ) {
                billmodeldata.list = status.data.Result;
            }
            else {
                billmodeldata.list = [];
            }
        }

        const sumoutmoney = computed( () => {
            if ( displaylist.value != null && displaylist.value.length > 0 ) {
                var sum : number = _.sumBy( displaylist.value , ( item : IDisplayDayBill ) => {
                    return item.sumout;
                } );

                return sum;
            }

            return 0;
        } )

        const suminmoney = computed( () => {
            if ( displaylist.value != null && displaylist.value.length > 0 ) {
                var sum : number = _.sumBy( displaylist.value , ( item : IDisplayDayBill ) => {
                    return item.sumin;
                } );

                return sum;
            }

            return 0;
        } )

        const displaylist = computed<IDisplayDayBill[]>( () => {
            if ( billmodeldata.list != null && billmodeldata.list.length > 0 ) {
                //groupBy返回的是对象
                var gr = _.groupBy( billmodeldata.list , "moneydate" );

                var daylist : IDisplayDayBill[] = [];

                let all = Object.entries( gr );
                // console.log( 'arr3' , arr3 )

                all.forEach( ( item : any[] , index : number ) => {
                    //item是一个数组,只存放2个元素
                    var billlist : IBillObj[] = item[ 1 ] as IBillObj[];

                    var sumin : number = _.sumBy( billlist , ( item : IBillObj ) => {
                        if ( item.isout ) {
                            return 0;
                        }
                        return item.moneys;
                    } );

                    var sumout : number = _.sumBy( billlist , ( item : IBillObj ) => {
                        if ( item.isout ) {
                            return item.moneys;
                        }
                        return 0;
                    } );

                    var _moneydate : string = item[ 0 ];
                    var dayobj : IDisplayDayBill = {
                        moneydate : _moneydate ,
                        list : billlist ,
                        week : dayjs( _moneydate ).day() ,
                        sumin ,
                        sumout
                    }

                    daylist.push( dayobj )
                } )

                // return _.groupBy( modeldata.list , "moneydate" );
                return daylist;
            }

            return []
        } )

        const getweekstring = ( week : number ) : string => {
            var str = '天'

            switch ( week ) {
                case 1:
                    str = '一'
                    break;
                case 2:
                    str = '二'
                    break;
                case 3:
                    str = '三'
                    break;
                case 4:
                    str = '四'
                    break;
                case 5:
                    str = '五'
                    break;
                case 6:
                    str = '六'
                    break;
                default:
                    str = '天'
            }

            return '星期' + str;
        }

        const getcolor = ( isout : boolean ) : string => {
            return isout ? outcolor : incolor;
        }

        const getcolorobject = ( isout : boolean ) : any => {
            return {
                color : getcolor( isout )
            };
        }

        const SelectYearMonth = () => {
            // console.log( 'SelectYearMonth' )

            dateselectdialogshow.value = true;
        }

        const userclosedate = () => {
            dateselectdialogshow.value = false;
        }

        const userselectdate = ( val : ISelectDateObj ) => {
            querymodeldata.userselectyear = val.year;
            querymodeldata.userselectmonth = val.month;

            dateselectdialogshow.value = false;

            //再从新请求 服务器
            getlist();
        }

        const onBillTypeSelect = () => {
            billtypeselectdialogshow.value = true;
        }

        const userclosebilltype = () => {
            billtypeselectdialogshow.value = false;
        }

        const userselectbilltype = ( val : ISelectBillTypeObj ) => {
            querymodeldata.userselectbilltypeid = val.id;
            querymodeldata.userselectbilltypetxt = val.name;
            querymodeldata.querytype = val.id == 0 ? "all" : ( val.isout ? "out" : "in" );

            //再从新请求 服务器
            getlist();
        }

        return {
            ...toRefs( billmodeldata ) ,
            ...toRefs( querymodeldata ) ,
            dateselectdialogshow , billtypeselectdialogshow ,
            //这3个暂时无用,暂时不导出
            isqueryall , isqueryout , isqueryin ,
            isdisplayout , isdisplayin ,
            displaylist , sumoutmoney , suminmoney ,
            getweekstring , selectymtxt ,
            SelectYearMonth , userselectdate , userclosedate ,
            onBillTypeSelect , userclosebilltype , userselectbilltype ,
            FormatNumber , getcolor , getcolorobject ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       src="./Bill.css">
</style>
