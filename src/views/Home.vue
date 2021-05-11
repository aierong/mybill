<template>
    <div class="home">
        <router-view/>
        <!--        这里加几个回车,要不上面router-view内容会挡住最下面 -->
        <br> <br> <br><br><br>
        <tabbars></tabbars>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

import tabbars from "@comp/tabbars.vue";

export default defineComponent( {
    name : 'Home' ,
    components : {
        tabbars ,
    } ,
} );
</script>
