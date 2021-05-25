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

        {{ userselectyear }}
        <br>
        {{ userselectmonth }}
    </div>

</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">

type mode = "money" | "date";

import { IBillObj } from '@/types'

interface IState {
    list : IBillObj[],
    querymode : mode
}

// 导入
import {
    defineComponent ,

    ref ,
    reactive ,
    toRefs ,
    computed , onMounted ,
} from "vue";

import { useStore } from 'vuex'
import { key } from '@store/index.ts'

import * as billapi from '@/http/api/bill'

export default defineComponent( {
    // 子组件
    components : {} ,
    // 声明 props
    props : {} ,
    setup () {
        const store = useStore( key )

        //本页面是从stat页面过来的,所以年月,可以取过来

        var now = new Date();

        var userselectyear : number = now.getFullYear();
        var userselectmonth : number = 1 + now.getMonth();

        const state = reactive<IState>( {
            list : [] ,
            querymode : 'money'
        } );

        const onClickLeft = () => {
            // gotoback();
        }

        const initval = () => {
            if ( store.state.StatPageData != null ) {
                userselectyear = store.state.StatPageData.year;
                userselectmonth = store.state.StatPageData.month;
            }
        }

        onMounted( async () => {
            initval();

            await getlist();
        } )

        const getlist = async () => {
            let status = await billapi.getoutlist( userselectyear , userselectmonth , state.querymode );

            if ( status.data.Success ) {
                state.list = status.data.Result;
            }
            else {
                state.list = []
            }
        }

        return {
            ...toRefs( state ) ,
            userselectyear , userselectmonth ,
            onClickLeft ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style lang="less"
       scoped
       src="./OutList.less">

</style>
