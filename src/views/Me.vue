<!--

作者: chenghao
Date: 2021/4/30
Time: 17:40
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>

        <van-cell size="large"
                  @click="SetupAvatarClick">
            <template #title>
                <van-icon :name="userinfo.avatar"
                          size="58"
                          @click="avatarclick"/>
                <span class="cellspantitleclass">{{ userinfo.name + '(' + userinfo.mobile + ')' }}</span>
            </template>
        </van-cell>
        <br>
        <van-cell icon="setting-o"
                  title="修改密码"
                  is-link/>
        <br>
        <van-cell icon="user-o"
                  title="关于我们"
                  @click="AboutClick"
                  is-link/>
        <br><br>
        <van-button size="large"
                    hairline
                    plain
                    icon="friends"
                    round
                    @click="exitClick"
                    type="warning">退出

        </van-button>

        <br>
        <selectavatar :avatarobj="avatarobj"
                      @userselectavatar="userselectavatar"
                      v-if="avatarobj.showdialog"></selectavatar>
    </div>

</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">
interface IUserObj {
    userinfo : {
        mobile : string,
        name : string,
        avatar : string
    },
    showdialog : boolean
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

import { useRouter , useRoute } from 'vue-router'
import { useStore } from 'vuex'
import { key } from '@store/index.ts'
import * as UserMutationType from '@store/mutations/mutation-types'

import { Dialog } from 'vant';
import * as constant from "@common/constant";
import * as userapi from '@/http/api/user'

import selectavatar from "@comp/selectavatar.vue";
import { IAvatarObj } from '@comp/types'

export default defineComponent( {
    // 子组件
    components : {
        selectavatar ,
    } ,
    setup () {
        const router = useRouter()
        const store = useStore( key )

        const modeldata = reactive<IUserObj>( {
            userinfo : {
                mobile : store.state.loginusermobile ,
                name : '' ,
                avatar : ''
            } ,
            showdialog : false
        } );

        const avatarobj = computed( () => {
            var obj : IAvatarObj = {
                showdialog : modeldata.showdialog ,
                avatar : modeldata.userinfo.avatar
            };

            return obj;
        } );

        const SetupAvatarClick = () => {
        }

        const exitClick = () => {
            Dialog.confirm( {
                // title : "退出" ,
                message : "确定退出?"
            } )
            .then( () => {
                // on confirm
                // console.log( "点确定按钮" )

                exitsystem( store.state.loginusermobile );
            } )
            .catch( () => {
                // on cancel
                // console.log( "点取消按钮" )
            } )
        }

        const AboutClick = () => {
            console.log( 'AboutClick' )
        }

        const exitsystem = ( mobile : string = '' ) => {

            store.commit( UserMutationType.clearloginuser );

            //返回的token也清空一下
            localStorage.setItem( constant.tokenname , '' );

            gotologin( mobile );

            return;
        }

        const gotologin = ( mobile : string = '' ) => {
            router.push( `/login?mobile=${ mobile }` )

            return;
        }

        const avatarclick = () => {
        }

        const userselectavatar = ( avatar : string ) => {
        }

        onMounted( async () => {
            let status = await userapi.getuser();

            // console.log( 'me status' , status )

            if ( status.data.Success ) {
                var userDto = status.data.Result;

                modeldata.userinfo.name = userDto.name;
                modeldata.userinfo.avatar = userDto.avatar;
            }
        } );

        return {
            ...toRefs( modeldata ) ,
            avatarobj ,
            SetupAvatarClick , exitClick , AboutClick ,
            avatarclick , userselectavatar ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       src="./Me.css">
</style>
