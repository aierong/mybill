<!--

作者: chenghao
Date: 2021/5/11
Time: 17:35
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>
    <!--
    :style="{ height: '70%' }"
    这里不用百分比,我会指定div的高度
    -->
    <van-popup closeable
               v-model:show="show"
               position="bottom">
        <br><br><br>
        <div class="add-wrap">
            <!--            头部 -->
            <div>
                <span @click="changeType(true)"
                      :class="{filterspan:true,outactive:isout}">支出</span>
                <span @click="changeType(false)"
                      :class="{filterspan:true,inactive:!isout}">收入</span>
                <span @click="opendate"
                      class="dateselect">{{ displaydate }}<van-icon name="arrow-down"/></span>
                <van-button class="savebtn"
                            hairline
                            @click="onAddBill"
                            color="#3EB575"
                            size="small">确定
                </van-button>
            </div>
            <br>
            <!--        金额-->
            <div class="money">
                <span class="sufix">¥</span>
                <span class="amount animation">{{ amount }}</span>
            </div>
            <br>
            <!--        类型列表选择-->
            <div>
                <div class="divitem"
                     v-if="isout">
                    <template v-for="(mxitem,mxindex) in outlist">
                        <div :class="{item:true, bgout:billtypeid==mxitem.ids}"
                             @click="outitemselect(mxitem.ids)">
                            <aliicon :iconname="mxitem.avatar"
                                     :iconsize="26"
                                     colortypes="no"/>
                            {{ mxitem.typename }}
                        </div>
                    </template>
                    <!--                    这里带一个添加按钮-->
                    <span style="margin-left: 15px;margin-right: 15px;">
                        <van-icon @click="onAddType"
                                  name="add-o"
                                  size="26"/></span>
                </div>
                <div class="divitem"
                     v-else>
                    <template v-for="(mxitem,mxindex) in inlist">
                        <div :class="{item:true, bgin:billtypeid==mxitem.ids}"
                             @click="initemselect(mxitem.ids)">
                            <aliicon :iconname="mxitem.avatar"
                                     :iconsize="26"
                                     colortypes="no"/>
                            {{ mxitem.typename }}
                        </div>
                    </template>
                    <!--                    这里带一个添加按钮-->
                    <span style="margin-left: 15px;margin-right: 15px;">
                        <van-icon @click="onAddType"
                                  name="add-o"
                                  size="26"/></span>
                </div>
            </div>
            <br>
            <!--        备注-->
            <div>
                <div class="remarktip"
                     @click="openmomedlg"
                     v-if="isdisplaymometip">添加备注
                </div>
                <div v-else>
                    <span class="remark">{{ mometxt }}</span><span @click="openmomedlg"
                                                                   class="remarkupdate">修改</span>
                </div>
            </div>
            <br>
            <!--        数组键盘-->
            <van-number-keyboard :show="true"
                                 @delete="remove"
                                 @input="inputChange"
                                 extra-key="."/>
        </div>
    </van-popup>
    <br>
    <van-dialog v-model:show="showtypedlg"
                title="填写类型"
                show-cancel-button
                :before-close="typebeforeClose">
        <van-field v-model="typetxt"
                   autosize
                   maxlength="4"
                   placeholder="请输入类型"
                   show-word-limit/>
    </van-dialog>
    <van-dialog v-model:show="showmomedlg"
                title="填写备注"
                show-cancel-button>
        <van-field v-model="mometxt"
                   autosize
                   rows="2"
                   type="textarea"
                   maxlength="10"
                   placeholder="请输入备注"
                   show-word-limit/>
    </van-dialog>
    <van-popup :close-on-click-overlay="false"
               v-model:show="showdatedlg"
               position="bottom">
        <van-datetime-picker type="date"
                             title="选择年月日"
                             :max-date="maxDate"
                             @cancel="onCancelDate"
                             @confirm="onConfirmDate"
                             v-model="CurrentSelectDate"/>
    </van-popup>
</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">
import { IBillType , IBillDto , ISelectBillTypeObj } from "@comp/types";

import { IBillObj } from '@/types'

interface IAllList {
    outlist : IBillType[],
    inlist : IBillType[],
}

// 导入
import {
    defineComponent ,
    ref ,
    reactive ,
    toRefs ,
    computed ,
    onMounted ,
    watch ,
    PropType ,
} from "vue";

//引入一下
import { Toast } from 'vant';

import * as billtypeapi from "../../http/api/billtype";
import * as billapi from '@/http/api/bill'

import { EncryptPassWord } from "@common/util";
import { AxiosResponse } from "axios";

import dayjs from 'dayjs'

export default defineComponent( {
    // 定义是事件
    emits : {
        runresult : ( isok : boolean ) => {
            //上面已经定义了参数类型,系统会验证的参数类型
            return true
        } ,
    } ,
    // 声明 props
    props : {
        isrunadd : {
            type : Boolean ,
            required : true
        } ,
        updatedata : {
            type : Object as PropType<IBillObj>
        }
    } ,
    setup ( props , { emit } ) {
        var now = new Date();

        const show = ref<boolean>( false )

        const showtypedlg = ref<boolean>( false )
        const typetxt = ref<string>( '' )

        const showmomedlg = ref<boolean>( false )
        const mometxt = ref<string>( '' )

        const showdatedlg = ref( false );

        const isout = ref<boolean>( true );
        const billtypeid = ref<number>( 0 );

        const year = ref<number>( now.getFullYear() );
        const month = ref( 1 + now.getMonth() );
        const day = ref( now.getDate() );

        const CurrentSelectDate = ref( now )
        //限制一下,日期控件的最大和最小
        const maxDate : Date = now;
        // const minDate : Date = dayjs( now ).subtract( 3 , 'year' ).toDate();

        const moneys = ref<number>( 0.00 );

        const amount = ref( '' )

        const initval = () => {

            var now = new Date();

            year.value = now.getFullYear();
            month.value = 1 + now.getMonth();
            day.value = now.getDate();

            CurrentSelectDate.value = now;

            moneys.value = 0;
            amount.value = ''

            mometxt.value = ''

            isout.value = true;

            if ( listmodeldata.outlist.length > 0 ) {
                billtypeid.value = listmodeldata.outlist[ 0 ].ids;
            }
        }

        watch(
            () => props.updatedata ,
            ( newval ) => {
                // console.log( '子组件：监听props' , newval , props.isrunadd )

                // 修改模式
                if ( newval != null && !props.isrunadd ) {
                    // console.log( '子组件' , newval , props.isrunadd )

                    year.value = newval.moneyyear;
                    month.value = newval.moneymonth;
                    day.value = newval.moneyday;

                    CurrentSelectDate.value = new Date( newval.moneyyear , newval.moneymonth - 1 , newval.moneyday );
                    moneys.value = newval.moneys;
                    amount.value = newval.moneys.toFixed( 2 );

                    mometxt.value = newval.memo;

                    isout.value = newval.isout;

                    billtypeid.value = newval.billtypeid;
                }
            } ,
            {
                // 这里如果不设置immediate = true,那么最初绑定的时候是不会执行的,要等到num改变时才执行监听计算
                immediate : true
            }
        )

        const selectlistfirst = ( _isout : boolean ) => {

            //默认选择第1个
            if ( _isout ) {
                if ( listmodeldata.outlist.length > 0 ) {
                    billtypeid.value = listmodeldata.outlist[ 0 ].ids;
                }
            }
            else {
                if ( listmodeldata.inlist.length > 0 ) {
                    billtypeid.value = listmodeldata.inlist[ 0 ].ids;
                }
            }
        }

        const displaydate = computed( () => {
            var monthtxt = month.value <= 9 ? '0' + month.value.toString() : month.value.toString();
            var daytxt = day.value <= 9 ? '0' + day.value.toString() : day.value.toString();

            return `${ monthtxt }月${ daytxt }日`;
        } )

        const datetxt = computed( () => {
            var monthtxt = month.value <= 9 ? '0' + month.value.toString() : month.value.toString();
            var daytxt = day.value <= 9 ? '0' + day.value.toString() : day.value.toString();

            return `${ year.value }-${ monthtxt }-${ daytxt }`;
        } )

        const listmodeldata = reactive<IAllList>( {
            outlist : [] ,
            inlist : []
        } );

        const isdisplaymometip = computed( () => {
            if ( mometxt.value == '' ) {
                return true;
            }

            return false;
        } )

        const toggle = () => {
            show.value = !show.value

            // 每次打开初始化一下
            if ( show.value ) {
                if ( props.isrunadd ) {
                    initval();  // 添加模式, 初始化一下
                }

            }
        }

        onMounted( async () => {
            if ( props.isrunadd ) {
                initval();  // 初始化一下
            }

            await getoutlist();
            await getinlist();

            // 选择第1个
            if ( props.isrunadd ) {
                selectlistfirst( isout.value );
            }

        } );

        const getinlist = async () => {

            var instatus = await billtypeapi.getlist( false , true );

            // var status2 = await billtypeapi.getlistnew( false , true );
            // //status2.data.Result中有带回来的数据
            // console.log( 'status2' , status2 , status2.data.Result )

            if ( instatus.data.Success ) {
                listmodeldata.inlist = instatus.data.Result;
            }
            else {
                listmodeldata.inlist = [];
            }

        }

        const getoutlist = async () => {
            var outstatus = await billtypeapi.getlist( true , true );

            if ( outstatus.data.Success ) {
                listmodeldata.outlist = outstatus.data.Result;
            }
            else {
                listmodeldata.outlist = [];
            }

        }

        const onAddType = () => {
            showtypedlg.value = true;
        }

        const typebeforeClose = async ( action : string ) => {
            // console.log( 'typebeforeClose' )

            if ( action === "confirm" ) {
                if ( typetxt.value != '' ) {
                    let status = await billtypeapi.add( isout.value , typetxt.value );

                    if ( status.data.Success ) {
                        typetxt.value = '';  //清空一下

                        // 成功了,刷新一下
                        var _isout = isout.value;
                        if ( _isout ) {
                            await getoutlist();
                        }
                        else {
                            await getinlist();
                        }

                        // 默认添加成功的那个项目,也就是最后一个
                        if ( _isout ) {
                            if ( listmodeldata.outlist.length > 0 ) {
                                billtypeid.value = listmodeldata.outlist[ listmodeldata.outlist.length - 1 ].ids;
                            }
                        }
                        else {
                            if ( listmodeldata.inlist.length > 0 ) {
                                billtypeid.value = listmodeldata.inlist[ listmodeldata.inlist.length - 1 ].ids;
                            }
                        }

                        return true;
                    }
                    else {
                        Toast.fail( status.data.Message )

                        return false;
                    }
                }
                else {
                    Toast( '请填写类型' );

                    return false;
                }
            }
            else {
                // 点击了取消按钮

                // return true;
            }

            return true;
        }

        const openmomedlg = () => {
            showmomedlg.value = true;
        }

        const changeType = ( _isout : boolean ) => {
            isout.value = _isout;

            //默认选择第1个
            selectlistfirst( _isout );
        }

        const opendate = () => {
            showdatedlg.value = true;
        }

        const onCancelDate = () => {
            showdatedlg.value = false;
        }

        const onConfirmDate = ( value : Date ) => {
            year.value = value.getFullYear();
            month.value = value.getMonth() + 1;
            day.value = value.getDate();

            showdatedlg.value = false;
        }

        // 监听输入框改变值
        const inputChange = ( value : string ) => {
            // console.log( 'inputChange' , value )

            // 当输入的值为 '.' 且 已经存在 '.'，则不让其继续字符串相加。
            if ( value == '.' && amount.value.includes( '.' ) ) {
                return
            }

            // 小数点后保留两位，当超过两位时，不让其字符串继续相加。
            if ( value != '.' && amount.value.includes( '.' ) && amount.value && amount.value.split( '.' )[ 1 ].length >= 2 ) {
                return
            }

            amount.value = amount.value + value;
        }

        const remove = () => {
            amount.value = amount.value.slice( 0 , amount.value.length - 1 )
        }

        watch(
            amount ,
            ( newval : string ) => {
                if ( newval == '' ) {
                    moneys.value = 0.00;

                    return;
                }
                else {
                    if ( newval ) {
                        moneys.value = parseFloat( newval );
                    }
                    else {
                        moneys.value = 0.00;
                    }
                }

            } ,
            {
                // 这里如果不设置immediate = true,那么最初绑定的时候是不会执行的,要等到num改变时才执行监听计算
                immediate : true
            }
        )

        const outitemselect = ( id : number ) => {
            billtypeid.value = id;
        }

        const initemselect = ( id : number ) => {
            billtypeid.value = id;
        }

        const onAddBill = async () => {

            if ( moneys.value <= 0 ) {
                Toast.fail( '请输入金额' )

                return;
            }

            var savedata : IBillDto = {
                isadd : props.isrunadd ,
                ids : props.isrunadd ? 0 : props.updatedata != null ? props.updatedata.ids : 0 ,
                billtypeid : billtypeid.value ,
                isout : isout.value ,
                moneys : moneys.value ,
                moneydate : datetxt.value ,
                memo : mometxt.value
            };

            // console.log( 'savedata' , savedata )

            let status : AxiosResponse<any>;

            if ( props.isrunadd ) {
                status = await billapi.add( savedata );
            }
            else {
                status = await billapi.update( savedata );
            }

            // console.log( status )

            if ( status.data.Success ) {
                emit( "runresult" , true );

                initval();  // 初始化一下
                show.value = false;  //关闭对话框

                return;
            }
            else {
                Toast.fail( status.data.Message )
                emit( "runresult" , false );

                return;
            }
        }

        return {
            ...toRefs( listmodeldata ) ,
            moneys ,
            amount ,
            year ,
            month ,
            day ,
            displaydate , datetxt ,
            showdatedlg ,
            CurrentSelectDate , maxDate ,
            // minDate ,
            onCancelDate ,
            onConfirmDate ,
            isout , billtypeid ,

            show ,
            toggle ,
            showtypedlg ,
            typetxt ,
            showmomedlg ,
            mometxt ,
            isdisplaymometip ,
            openmomedlg ,
            onAddType ,
            typebeforeClose ,
            changeType ,
            opendate ,
            inputChange ,
            remove ,
            onAddBill ,
            outitemselect , initemselect ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       lang="less"
       src="./BillOperation.less">
</style>
