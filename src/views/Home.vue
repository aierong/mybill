<template>
    <div class="home">
        <router-view/>
        <!--        这里加几个回车,要不上面router-view内容会挡住最下面 -->
        <!--        <br> <br> <br><br><br>-->
        <!--        <mytabbar v-show="show"/>-->
    </div>
</template>

<script lang="ts">
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

// import { useRoute } from 'vue-router'
// import { useStore } from 'vuex'
// import { key } from '@store/index.ts'
//
// import mytabbar from "@comp/mytabbar.vue";

export default defineComponent( {
    name : 'Home' ,
    components : {
        // mytabbar ,
    } ,
    setup () {
        // const store = useStore( key )
        //
        // //通过vuex计算属性,控制tabbar要不要显示
        // const show = computed( () => {
        //     return store.getters.gettabbarshow;
        // } )

        return {
            // show ,
        }
    }
} );
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       src="./Home.less"
       lang="less">
</style>
