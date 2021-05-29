<!--

作者: chenghao
Date: 2021/5/25
Time: 17:37
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>
        <van-nav-bar title="详细数据"
                     left-text="返回"
                     left-arrow
                     @click-left="onClickLeft"/>

        <div class="monthsumtxt">{{ userselectmonth }}月<span v-if="billtypeid>0">{{ billtypetxt }}</span>共支出</div>
        <div class="summoneytxt">¥{{ $FormatMoney( summoney ) }}</div>
        <br>
        <div>
            <span :class="{ active:isselectmoney ,moneytxt:true }"
                  @click="onClickMoney">按金额</span>
            <span :class="{ active:!isselectmoney ,datetxt:true }"
                  @click="onClickDate">按时间</span>
        </div>

        <br>
        <outitemlist :list="list"
                     @deleteitemresult="deleteitemresult"/>
        <br>
    </div>

</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">


import { IBillObj , querymode } from '@/types'

interface IState {
    list : IBillObj[],
    querymode : querymode
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

// 引入lodash
import * as _ from "lodash"

import { useStore } from 'vuex'
import { key } from '@store/index.ts'
import * as UserMutationType from '@store/mutations/mutation-types.ts'
import { useRouter , useRoute , onBeforeRouteLeave } from 'vue-router'

import * as billapi from '@/http/api/bill'

import outitemlist from "@comp/outitemlist.vue";
import { IStatPageData } from "@store/types";

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

        //本页面是从stat页面过来的,所以年月,可以取过来

        var now = new Date();

        var userselectyear = ref<number>( now.getFullYear() );
        var userselectmonth = ref<number>( 1 + now.getMonth() );

        const state = reactive<IState>( {
            list : [] ,
            querymode : 'money'
        } );

        const isselectmoney = computed( () => {
            return state.querymode == 'money'
        } )

        const summoney = computed( () => {
            return _.sumBy( state.list , function ( item : IBillObj ) {
                return item.moneys
            } )
        } )

        const billtypeid = computed<number>( () => {
            return store.getters.getoutlistpagebilltypeid;
        } )

        const billtypetxt = computed<string>( () => {
            return store.getters.getoutlistpagebilltypetxt;
        } )

        const onClickLeft = () => {
            router.push( { path : '/stat' } )

            return;
        }

        const initval = () => {
            if ( store.state.StatPageData != null ) {
                userselectyear.value = store.state.StatPageData.year;
                userselectmonth.value = store.state.StatPageData.month;
            }

            if ( store.state.OutListPageData != null ) {
                state.querymode = store.state.OutListPageData.mode;
            }
        }

        const getlist = async () => {
            let status = await billapi.getoutlist( userselectyear.value ,
                userselectmonth.value ,
                billtypeid.value ,
                state.querymode );

            if ( status.data.Success ) {
                state.list = status.data.Result;
            }
            else {
                state.list = []
            }
        }

        const onClickMoney = async () => {
            state.querymode = 'money'

            await getlist();
        }

        const onClickDate = async () => {
            state.querymode = 'date'

            await getlist();
        }

        const deleteitemresult = async ( isok : boolean ) => {
            if ( isok ) {
                await getlist();
            }

            // console.log('isok',isok)
        }

        onMounted( async () => {
            initval();

            await getlist();
        } )

        onBeforeRouteLeave( ( to , from ) => {
            var payload : querymode = state.querymode;

            store.commit( UserMutationType.updateoutlistpagemode , payload )

            return;
        } )

        return {
            ...toRefs( state ) ,
            userselectyear , userselectmonth , isselectmoney , summoney ,
            billtypeid , billtypetxt ,
            onClickLeft ,
            onClickMoney ,
            onClickDate ,
            deleteitemresult ,

        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style lang="less"
       scoped
       src="./OutList.less">

</style>
