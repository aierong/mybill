<template>
    <div class="home">

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
