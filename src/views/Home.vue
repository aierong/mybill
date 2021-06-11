<template>
    <div>
        <router-view/>
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

} from "vue";

export default defineComponent( {
    name : 'Home' ,
    setup () {

        return {}
    }
} );
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       src="./Home.less"
       lang="less">
</style>
