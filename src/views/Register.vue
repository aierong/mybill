<!--

作者: chenghao
Date: 2021/4/26
Time: 16:39
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>
        <van-nav-bar title="注册新用户"
                     left-text="返回"
                     right-text="说明"
                     left-arrow
                     @click-left="onClickLeft"
                     @click-right="onClickRight"/>
        <br>
        <van-form @submit="onSubmit"
                  :validate-first="true"
                  @failed="onFailed">
            <van-field v-model="userinfo.mobile"
                       required
                       clearable
                       label="手机"
                       placeholder="请输入手机号码"
                       :rules="[{ required: true, message: '请填写手机号码' },
                                { validator: validatorMobileMessage } ]"/>
            <van-field v-model="userinfo.name"
                       required
                       clearable
                       label="用户名"
                       placeholder="请输入用户名"
                       :rules="[{ required: true, message: '请填写用户名' }  ]"/>

            <van-field v-model="userinfo.password"
                       type="password"
                       label="密码"
                       placeholder="请输入密码"
                       required
                       :rules="[{ required: true, message: '请填写密码' },{ validator: validatorPwdMessage } ]"/>

            <van-field v-model="userinfo.password2"
                       type="password"
                       label="再次密码"
                       placeholder="请输入密码"
                       required
                       :rules="[{ required: true, message: '请填写密码' },{ validator: validatorPwd2Message } ]"/>
            <van-field v-model="userinfo.email"
                       clearable
                       label="邮箱"
                       placeholder="请输入邮箱"
                       :rules="[{ validator: validatorEmailMessage } ]"/>

            <van-field label="头像">
                <template #input>
                    <van-icon :name="userinfo.avatar"
                              size="40"/>
                </template>
            </van-field>
            <div style="margin: 16px;">
                <van-button round
                            block
                            type="primary"
                            native-type="submit">注册
                </van-button>
            </div>
        </van-form>
    </div>

</template>

<!-- TypeScript脚本代码片段 -->
<script lang="ts">

// 导入
import {
    defineComponent ,
    ref ,
    reactive ,
    toRefs ,
    computed
} from "vue";

import {
    useRouter ,
    useRoute
} from 'vue-router'

//引入一下
import { Toast } from 'vant';

import {
    ValidatorMobile ,
    ValidatorPassword ,
    IsEmail
} from '@common/util'

interface UserObj {
    userinfo : {
        mobile : string,
        password : string,
        password2 : string,
        name : string,
        email : string,
        avatar : string
    }
}

import selectavatar from "@comp/selectavatar.vue";
import { avatariconlist } from '@common/constant'

export default defineComponent( {
    // 子组件
    components : {
        selectavatar
    } ,
    setup () {
        const router = useRouter()

        const modeldata = reactive<UserObj>( {
            userinfo : {
                mobile : '' ,
                password : '' ,
                password2 : '' ,
                name : '' ,
                email : '' ,
                //头像 先默认一个头像
                avatar : avatariconlist[ 0 ]
            } ,
        } );

        const onClickLeft = () => {
            gotologin( '' );
        }

        const onClickRight = () => {
            Toast( "这里可以注册新用户" )

            return;
        }

        const gotologin = ( mobile : string = '' ) => {
            router.push( `/login?mobile=${ mobile }` )

            return;
        }

        // 提交
        const onSubmit = () => {
            console.log( 'ok' )
        }

        //failed	提交表单且验证不通过后触发	errorInfo: { values: object, errors: object[] }
        const onFailed = () => {
        }

        const validatorMobileMessage = ( val : string ) : string => {

            return ValidatorMobile( val );
        }

        const validatorPwdMessage = ( val : string ) : string => {

            return ValidatorPassword( val );
        }

        const validatorPwd2Message = ( val : string ) : string => {
            var msg : string = ValidatorPassword( val );

            if ( msg == '' ) {
                //继续判断,2个密码是否一样
                if ( modeldata.userinfo.password != val ) {
                    return '2次输入密码不一致'
                }
            }

            return msg;
        }

        const validatorEmailMessage = ( val : string ) : string => {
            if ( val == '' ) {
                return ''
            }

            var bl : boolean = IsEmail( val );

            if ( !bl ) {
                return '请输入有效Email'
            }

            return ''
        }

        return {
            ...toRefs( modeldata ) ,
            onClickLeft ,
            onClickRight ,
            onSubmit ,
            onFailed ,
            validatorMobileMessage ,
            validatorPwdMessage ,
            validatorPwd2Message ,
            validatorEmailMessage ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style>

</style>
