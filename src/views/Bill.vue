<!--

作者: chenghao
Date: 2021/4/30
Time: 17:52
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>

    </div>

</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">
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

    list : IBillObj[]
    // list : any
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

export default defineComponent( {
    // 子组件
    components : {} ,

    setup () {
        const modeldata = reactive<IBillList>( {
            list : []
        } );

        onMounted( async () => {
            let status = await billapi.getlist( 2021 , 1 , 0 );
            // console.log( 'result' , status )
            if ( status.data.Success ) {
                modeldata.list = status.data.Result;
            }
            else {
                modeldata.list = [];
            }

        } )

        const displaylist = computed<IDisplayDayBill[]>( () => {
            if ( modeldata.list != null && modeldata.list.length > 0 ) {
                //groupBy返回的是对象
                var gr = _.groupBy( modeldata.list , "moneydate" );

                var daylist : IDisplayDayBill[] = [];

                let all = Object.entries( gr );
                // console.log( 'arr3' , arr3 )

                all.forEach( ( item , index , array ) => {
                    //item是一个数组,只存放2个元素
                    var billlist : IBillObj[] = item[ 1 ] as IBillObj[];

                    var sumin : number = _.sumBy( billlist , ( item ) => {
                        if ( item.isout ) {
                            return 0;
                        }
                        return item.moneys;
                    } );

                    var sumout : number = _.sumBy( billlist , ( item ) => {
                        if ( item.isout ) {
                            return item.moneys;
                        }
                        return 0;
                    } );

                    var dayobj : IDisplayDayBill = {
                        moneydate : item[ 0 ] ,
                        list : billlist ,
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

        return {
            ...toRefs( modeldata ) ,
            displaylist ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style>

</style>
