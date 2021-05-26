<!--

作者: chenghao
Date: 2021/5/25
Time: 17:30
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div class="item">
        <van-cell center
                  v-for="(mxitem,mxindex) in list"
                  :key="mxindex"
                  @click="itemClick(mxitem.ids)">
            <template #title>
                <aliicon :iconname="mxitem.avatar"
                         :iconsize="36"
                         colortypes="out"/>
                <span class="itemtxt">{{ mxitem.typename }}</span>
            </template>
            <template #default>
                <span class="outmoney">-{{ $FormatMoney( mxitem.moneys ) }}</span>
                <br>
                <span class="outdate">{{ formatdate( mxitem.moneydate ) }}</span>
            </template>
        </van-cell>
    </div>

</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">

import dayjs from 'dayjs'

import { IBillObj } from '@/types'

// 导入
import {
    defineComponent ,
    // 注意导入PropType
    PropType ,
    ref ,
    reactive ,
    toRefs ,
    computed
} from "vue";

import { useRouter } from 'vue-router'

export default defineComponent( {
    // 声明 props
    props : {
        list : {
            type : Object as PropType<IBillObj[]> ,
            required : true ,
            default : []
        }
    } ,
    setup () {
        const router = useRouter()

        const itemClick = ( ids : number ) => {
            router.push( `/detail?ids=${ ids }` )

            return;
        }

        const formatdate = ( date : string ) => {
            return dayjs( date ).format( 'MM月DD日' );
        }

        return {
            itemClick ,
            formatdate ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       lang="less"
       src="./outitemlist.less">
</style>
