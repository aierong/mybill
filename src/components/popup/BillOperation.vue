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
                        <div :class="{bgout:billtypeid==mxitem.ids}"
                             @click="outitemselect(mxitem.ids)"
                             style="margin-left: 5px;display: inline-block;height: 30px;">
                            <aliicon :iconname="mxitem.avatar"
                                     :iconsize="26"
                                     colortypes="no"/>
                            {{ mxitem.typename }}
                        </div>
                    </template>
                    <!--                    这里带一个添加按钮-->
                    <span style="margin-left: 15px;margin-right: 15px;"><van-icon @click="onAddType"
                                                                                  name="add-o"
                                                                                  size="26"/></span>
                </div>
                <div class="divitem"
                     v-else>
                    <template v-for="(mxitem,mxindex) in inlist">
                        <div :class="{bgin:billtypeid==mxitem.ids}"
                              @click="initemselect(mxitem.ids)"
                              style="margin-left: 5px;display: inline-block;height: 30px;">
                            <aliicon :iconname="mxitem.avatar"
                                     :iconsize="26"
                                     colortypes="no"
                            />{{ mxitem.typename }}
                        </div>
                    </template>
                    <!--                    这里带一个添加按钮-->
                    <span style="margin-left: 15px;margin-right: 15px;"><van-icon @click="onAddType"
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
                             @cancel="onCancelDate"
                             @confirm="onConfirmDate"
                             v-model="CurrentSelectDate"/>
    </van-popup>
</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">
import { IBillType , IBillDto } from "@comp/types";

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
} from "vue";

//引入一下
import { Toast } from 'vant';

import * as billtypeapi from "@/http/api/billtype";
import * as billapi from '@/http/api/bill'

import { EncryptPassWord } from "@common/util";

export default defineComponent( {

    // 声明 props
    props : {
        isadddata : {
            type : Boolean ,
            required : true
        } ,
        updatedata : {
            type : Object
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

        const moneys = ref<number>( 0.00 );

        const amount = ref( '0.00' )

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
        }

        onMounted( async () => {
            await getoutlist();
            await getinlist();

            if ( isout.value ) {
                if ( listmodeldata.outlist != null && listmodeldata.outlist.length > 0 ) {
                    billtypeid.value = listmodeldata.outlist[ 0 ].ids;
                }
            }
            else {
                if ( listmodeldata.inlist != null && listmodeldata.inlist.length > 0 ) {
                    billtypeid.value = listmodeldata.inlist[ 0 ].ids;
                }
            }
        } );

        const getinlist = async () => {

            var instatus = await billtypeapi.getlist( false , true );

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
                            if ( listmodeldata.outlist != null && listmodeldata.outlist.length > 0 ) {
                                billtypeid.value = listmodeldata.outlist[ listmodeldata.outlist.length - 1 ].ids;
                            }
                        }
                        else {
                            if ( listmodeldata.inlist != null && listmodeldata.inlist.length > 0 ) {
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
            if ( _isout ) {
                if ( listmodeldata.outlist != null && listmodeldata.outlist.length > 0 ) {
                    billtypeid.value = listmodeldata.outlist[ 0 ].ids;
                }
            }
            else {
                if ( listmodeldata.inlist != null && listmodeldata.inlist.length > 0 ) {
                    billtypeid.value = listmodeldata.inlist[ 0 ].ids;
                }
            }
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

                if ( newval ) {
                    moneys.value = parseFloat( newval );
                }
                else {
                    moneys.value = 0.00;
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
            var savedata : IBillDto = {
                isadd : props.isadddata ,
                ids : props.isadddata ? 0 : 0 ,
                billtypeid : billtypeid.value ,
                isout : isout.value ,
                moneys : moneys.value ,
                moneydate : datetxt.value ,
                memo : mometxt.value
            };

            let status = await billapi.add( savedata );
            console.log( status )
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
            CurrentSelectDate ,
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
