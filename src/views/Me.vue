<!--

作者: chenghao
Date: 2021/4/30
Time: 17:40
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>

        <van-cell size="large">
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
                  @click="PwdClick"
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
        <br>
        <van-dialog v-model:show="pwdinfo.showdialog"
                    title="请输入密码"
                    show-cancel-button
                    :before-close="pwdbeforeclose">
            <van-field v-model="pwdinfo.pwd"
                       type="password"
                       label="密码"
                       placeholder="请输入密码"/>
            <van-field v-model="pwdinfo.pwd2"
                       type="password"
                       label="密码"
                       placeholder="请输入密码"/>
        </van-dialog>
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
    showdialog : boolean,
    pwdinfo : {
        showdialog : boolean,
        pwd : string,
        pwd2 : string
    }
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

import { Dialog , Toast } from 'vant';
import * as constant from "@common/constant";
import * as userapi from '@/http/api/user'

import selectavatar from "@comp/selectavatar.vue";
import { IAvatarObj } from '@comp/types'

import { EncryptPassWord , ValidatorPassword } from '@common/util.ts'

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
            showdialog : false ,
            pwdinfo : {
                showdialog : false ,
                pwd : '' ,
                pwd2 : ''
            }
        } );

        const avatarobj = computed( () => {
            var obj : IAvatarObj = {
                showdialog : modeldata.showdialog ,
                avatar : modeldata.userinfo.avatar
            };

            return obj;
        } );

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

            Toast( "我的记账本" )

            return;
        }

        const exitsystem = ( mobile : string = '' ) => {

            store.commit( UserMutationType.clearloginuser );

            //返回的token也清空一下
            localStorage.removeItem( constant.tokenname );

            gotologin( mobile );

            return;
        }

        const gotologin = ( mobile : string = '' ) : void => {
            router.push( `/login?mobile=${ mobile }` )

            return;
        }

        const avatarclick = () : void => {
            modeldata.showdialog = true;
        }

        const userselectavatar = async ( avatar : string ) => {
            modeldata.showdialog = false;

            if ( avatar != '' ) {
                let status = await userapi.updateavatar( avatar );

                if ( status.data.Success ) {
                    modeldata.userinfo.avatar = avatar;
                }

            }

            return;
        }

        const PwdClick = () => {
            //初始化一下
            modeldata.pwdinfo.pwd = ''
            modeldata.pwdinfo.pwd2 = ''

            pwddialog( true );
        }

        // const pwdconfirm = async () => {
        //     console.log( 'pwdconfirm' )
        //
        //     // return;
        //
        //     let status = await userapi.updatepassword( EncryptPassWord( modeldata.pwdinfo.pwd ) );
        //
        //     var isok : boolean = false;
        //     if ( status.data.Success ) {
        //         isok = true;
        //     }
        //
        //     // pwddialog( false );
        //
        //     if ( isok ) {
        //         Toast( "修改成功!请重新登录!" )
        //
        //         exitsystem( store.state.loginusermobile );
        //
        //         return;
        //     }
        // }

        // const pwdcancel = () => {
        //     // console.log( 'cancel' )
        //
        //     // 不用代码控制关闭状态
        //     // pwddialog( false );
        // }

        const pwdbeforeclose = async ( action : string ) => {
            // console.log( 'pwdbeforeclose' , action )

            if ( action === 'confirm' ) {
                // 开始验证密码有效

                if ( modeldata.pwdinfo.pwd == '' || modeldata.pwdinfo.pwd2 == '' ) {
                    Toast( "请填写密码" )

                    return false;
                }

                if ( modeldata.pwdinfo.pwd != modeldata.pwdinfo.pwd2 ) {
                    Toast( "2次密码不一致" )

                    return false;
                }

                let msg = ValidatorPassword( modeldata.pwdinfo.pwd );
                // console.log( 'ValidatorPassword' , msg )
                if ( msg != '' ) {
                    Toast.fail( msg )

                    return false;
                }

                let status = await userapi.updatepassword( EncryptPassWord( modeldata.pwdinfo.pwd ) );

                var isok : boolean = false;
                if ( status.data.Success ) {
                    isok = true;
                }

                if ( isok ) {
                    Toast( "修改成功!请重新登录!" )

                    exitsystem( store.state.loginusermobile );

                    return true;
                }
            }

            return true;
        }

        const pwddialog = ( isopen : boolean = false ) => {
            modeldata.pwdinfo.showdialog = isopen;
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
            PwdClick , pwdbeforeclose ,
            exitClick , AboutClick ,
            avatarclick , userselectavatar ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style scoped
       lang="less"
       src="./Me.less">
</style>
