<!--
作者: chenghao
Date: 2021/4/30
Time: 17:52
功能: TypeScript版本vue文件
-->

<!-- html代码片段 -->
<template>
    <div>
        <!--     头部   -->
        <div class="header">
            <div style="padding-top: 10px;margin-left: 10px;">
                <van-button hairline
                            @click="onBillTypeSelect"
                            color="#3EB575"
                            size="small">{{ userselectbilltypetxt }} |
                    <van-icon name="apps-o"/>
                </van-button>

                <!--    选择类型弹窗   -->
                <SelectBillTypeDialog @selectbilltype="userselectbilltype"
                                      :selectbilltypeid="userselectbilltypeid"
                                      ref="selectbilltypedialogRef"/>
            </div>
            <div style="margin-top: 5px;margin-left: 4px;padding-bottom: 10px;">
                <span style="margin-left: 10px;color: white;"
                      @click="SelectYearMonth">{{ selectyyyymm }}</span>
                <van-icon name="arrow-down"
                          color="white"
                          @click="SelectYearMonth"/>
                <span v-show="isdisplayin"
                      class="summoneytxt">总收入¥{{ $FormatMoney( suminmoney ) }}</span>
                <span v-show="isdisplayout"
                      class="summoneytxt">总支出¥{{ $FormatMoney( sumoutmoney ) }}</span>

                <!--    选择日期弹窗   -->
                <SelectYearMonthDialog ref="selectdateRef"
                                       @selectdate="userselectdate"
                                       :year="userselectyear"
                                       :month="userselectmonth"/>
            </div>
        </div>

        <!--     列表,循环   -->
        <div v-if="isdisplaylist"
             :key="index"
             v-for="(item,index) in displaylist">
            <van-cell-group>
                <template #title>
                    <span>{{ item.moneydate }}</span><span style="margin-left: 5px;">{{ getweekstring( item.week ) }}</span>
                    <span class="itemmoney">
                      <span v-show="isdisplayin">
                         <span class="iteminout">收</span>
                         <span>{{ $FormatMoney( item.sumin ) }}</span>
                      </span>
                      <span style="margin-left: 10px;"
                            v-show="isdisplayout">
                          <span class="iteminout">支</span>
                          <span>{{ $FormatMoney( item.sumout ) }}</span>
                      </span>
                    </span>
                </template>
                <van-cell @click="itemClick(mxitem.ids)"
                          v-for="(mxitem,mxindex) in item.list"
                          :key="mxindex">
                    <template #title>
                        <aliicon :iconname="mxitem.avatar"
                                 :iconsize="22"
                                 :colortypes="mxitem.isout?'out':'in'"/>
                        <span style="margin-left: 5px;">{{ mxitem.typename }}</span>
                    </template>
                    <template #default>
                        <span :class="{ inmoney:!mxitem.isout,outmoney:mxitem.isout }">{{ mxitem.isout ? '-' : '+' }}{{ $FormatMoney( mxitem.moneys ) }}</span>
                    </template>
                </van-cell>
            </van-cell-group>
        </div>
        <div v-else>
            <van-empty description="无记录,赶紧添加吧"/>
        </div>

        <!--     悬浮添加按钮   -->
        <div class="navplus"
             @click="onAdd">
            <van-icon size="25"
                      name="records"
                      color="#39be77"/>
        </div>

        <!--    填写账单弹窗   -->
        <BillOperation :isrunadd="true"
                       @runresult="billrunresult"
                       ref="operationRef"/>

        <!--        tabbar-->
        <br><br><br>
        <mytabbar/>
    </div>
</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">


import { IBillObj , QueryType } from '@/types'

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

import { IAddPageData } from "@store/types";

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
// 引入lodash
import * as _ from "lodash"
import dayjs from 'dayjs'

import { ISelectDateObj , ISelectBillTypeObj } from "@comp/types";

import SelectYearMonthDialog from "@comp/popup/SelectYearMonthDialog.vue";
import SelectBillTypeDialog from "@comp/popup/SelectBillTypeDialog.vue";
import BillOperation from "@comp/popup/BillOperation.vue";

export default defineComponent( {
    // 子组件
    components : {
        SelectYearMonthDialog ,
        SelectBillTypeDialog ,
        BillOperation ,
    } ,
    setup () {
        const store = useStore( key )
        const router = useRouter()

        const selectbilltypedialogRef = ref<typeof SelectBillTypeDialog | null>( null )
        const selectdateRef = ref<typeof SelectYearMonthDialog | null>( null )
        const operationRef = ref<typeof BillOperation | null>( null );

        var now = new Date();

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

        const selectyyyymm = computed( () => {
            return `${ querymodeldata.userselectyear }年${ querymodeldata.userselectmonth }月`
        } )

        const isdisplayout = computed( () => {
            return querymodeldata.querytype == 'out' || querymodeldata.querytype == 'all';
        } )

        const isdisplayin = computed( () => {
            return querymodeldata.querytype == 'in' || querymodeldata.querytype == 'all';
        } )

        const initdata = () => {
            //有可能vuex中记录了,上次的记录,取回来用

            if ( store.state.AddPageData != null ) {
                // var datas = store.state.AddPageData;

                querymodeldata.querytype = store.state.AddPageData.querytype;
                querymodeldata.userselectyear = store.state.AddPageData.year;
                querymodeldata.userselectmonth = store.state.AddPageData.month;

                querymodeldata.userselectbilltypeid = store.state.AddPageData.billtypeid;
                querymodeldata.userselectbilltypetxt = store.state.AddPageData.billtypetxt;

            }
        }

        onMounted( async () => {
            // console.log( 'onMounted bill' )
            initdata();

            await getlist();
        } )

        const getlist = async () => {

            const { data } = await billapi.getlist( querymodeldata.userselectyear ,
                querymodeldata.userselectmonth ,
                querymodeldata.userselectbilltypeid );

            // console.log( 'result' , status )
            if ( data.Success ) {
                billmodeldata.list = data.Result;
            }
            else {
                billmodeldata.list = [];
            }
        }

        const sumoutmoney = computed( () => {
            if ( displaylist.value.length > 0 ) {
                var sum : number = _.sumBy( displaylist.value , ( item : IDisplayDayBill ) => {
                    return item.sumout;
                } );

                return sum;
            }

            return 0;
        } )

        const suminmoney = computed( () => {
            if ( displaylist.value.length > 0 ) {
                var sum : number = _.sumBy( displaylist.value , ( item : IDisplayDayBill ) => {
                    return item.sumin;
                } );

                return sum;
            }

            return 0;
        } )

        const displaylist = computed<IDisplayDayBill[]>( () => {
            if ( billmodeldata.list.length > 0 ) {
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

                return daylist;
            }

            return []
        } )

        const isdisplaylist = computed( () => {

            return displaylist.value.length > 0;
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

            var isrefresh = false;

            //有可能选择的和之前一样的,这里判断一下
            if ( querymodeldata.userselectmonth != val.month || querymodeldata.userselectyear != val.year ) {
                querymodeldata.userselectyear = val.year;
                querymodeldata.userselectmonth = val.month;

                isrefresh = true;
            }

            if ( isrefresh ) {
                //再从新请求 服务器
                await getlist();
            }

        }

        const onBillTypeSelect = () => {
            if ( selectbilltypedialogRef.value != null ) {
                selectbilltypedialogRef.value.toggle();
            }
        }

        /**
         * 选择类型后,确定
         * @param val
         */
        const userselectbilltype = async ( val : ISelectBillTypeObj ) => {
            var isrefresh = false;

            //有可能选择的和之前一样的,这里判断一下
            if ( querymodeldata.userselectbilltypeid != val.id ) {
                querymodeldata.userselectbilltypeid = val.id;
                querymodeldata.userselectbilltypetxt = val.name;
                querymodeldata.querytype = val.id == 0 ? "all" : ( val.isout ? "out" : "in" );

                isrefresh = true;
            }

            if ( isrefresh ) {
                //再从新请求 服务器
                await getlist();
            }

        }

        const onAdd = () => {
            // console.log( 'onAdd' )

            if ( operationRef.value != null ) {
                operationRef.value.toggle();
            }
        }

        const billrunresult = async ( isok : boolean ) => {
            if ( !isok ) {
                //  失败,暂时不处理

                return;
            }
            else {
                //再从新请求 服务器
                await getlist();

                return;
            }
        }

        const itemClick = ( ids : number ) : void => {
            // console.log( 'itemClick' , ids )

            router.push( `/detail?ids=${ ids }` )

            return;
        }

        onBeforeRouteLeave( ( to , from ) => {
            // 导航离开该组件的对应路由时调用
            // 离开时,记录一下,页面参数
            var payload : IAddPageData = {
                year : querymodeldata.userselectyear ,
                month : querymodeldata.userselectmonth ,
                billtypeid : querymodeldata.userselectbilltypeid ,
                billtypetxt : querymodeldata.userselectbilltypetxt ,
                querytype : querymodeldata.querytype ,

                // isrefresh : false
            };

            // 记录状态数据
            store.commit( UserMutationType.updateaddpagedata , payload );

        } );

        return {
            ...toRefs( billmodeldata ) ,
            ...toRefs( querymodeldata ) ,
            selectbilltypedialogRef , selectdateRef , operationRef ,

            onAdd ,

            isdisplayout , isdisplayin ,
            displaylist , isdisplaylist , sumoutmoney , suminmoney ,
            getweekstring , selectyyyymm ,
            SelectYearMonth , userselectdate ,

            onBillTypeSelect , userselectbilltype ,
            billrunresult ,
            itemClick ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       lang="less"
       src="./Bill.less">
</style>
