<template>
    <div class="home">
        <router-view/>
        <!--        这里加几个回车,要不上面router-view内容会挡住最下面 -->
        <br> <br> <br><br><br>
        <tabbars v-show="show"/>
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

import { useRoute } from 'vue-router'

import tabbars from "@comp/tabbars.vue";

export default defineComponent( {
    name : 'Home' ,
    components : {
        tabbars ,
    } ,
    setup () {
        const route = useRoute()

        const show = ref<boolean>( true );

        // console.log( 'home' , route )

        watch(
            route ,
            ( newroute ) => {

                // 重新获取一下参数
                if ( newroute != null && newroute.meta != null ) {
                    // console.log( 'newroute' , newroute )
                    if ( newroute.meta.notabbar != null && newroute.meta.notabbar ) {
                        // console.log( 'newroute' , newroute )

                        show.value = false;
                    }
                }

            }
        )

        return {
            show ,
        }
    }
} );
</script>
