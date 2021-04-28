<!--

作者: chenghao
Date: 2021/4/26
Time: 16:40
功能: TypeScript版本vue文件

-->

<!-- html代码片段 -->
<template>

    <div>
        <van-nav-bar right-text="注册账号"
                     title="用户登录"
                     @click-right="onClickRight"/>
        <br>
        <van-form @submit="onSubmit"
                  :validate-first="true"
                  @failed="onFailed">
            <van-field v-model="userinfo.mobile"
                       clearable
                       required
                       label="手机"
                       placeholder="请输入手机"
                       :rules="[{ required: true, message: '请填写手机号码' },
                                { validator: validatorMobileMessage } ]"/>
            <van-field v-model="userinfo.password"
                       type="password"
                       clearable
                       label="密码"
                       placeholder="请输入密码"
                       required
                       :rules="[{ required: true, message: '请填写密码' },{ validator: validatorPwdMessage } ]"/>
            <div style="margin: 16px;">
                <van-button round
                            block
                            type="primary"
                            native-type="submit">提交
                </van-button>
            </div>
        </van-form>
        <br>
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

import { useRouter , useRoute } from 'vue-router'

interface UserObj {
    userinfo : {
        mobile : string,
        password : string
    }
}

import { ValidatorMobile , ValidatorPassword } from '@common/util.ts'

export default defineComponent( {
    // 声明 props
    props : {
        returnmobile : {
            type : String ,
            default : ''
        } ,
    } ,
    setup ( props ) {
        const router = useRouter()

        const modeldata = reactive<UserObj>( {
            userinfo : {
                mobile : props.returnmobile ,
                password : ''
            } ,
        } );

        //转向注册页面
        const onClickRight = () => {
            router.push( '/register' )

            return;
        }

        const validatorMobileMessage = ( val : string ) => {

            return ValidatorMobile( val );
        }

        const validatorPwdMessage = ( val : string ) => {

            return ValidatorPassword( val );
        }

        // 提交
        const onSubmit = () => {
            //values 可以收到表单中各个项目的值
            // console.log( 'submit' , values );

        }

        // failed	提交表单且验证不通过后触发	errorInfo: { values: object, errors: object[] }
        const onFailed = () => {
            //console.log( 'failed' , errorInfo );
        }

        return {
            ...toRefs( modeldata ) ,
            onClickRight ,
            validatorMobileMessage , validatorPwdMessage ,
            onSubmit ,
            onFailed ,
        };
    } ,

} )
</script>

<!-- 样式代码片段  scoped -->
<style>

</style>
