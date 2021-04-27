/*

 作者: chenghao
 Date: 2021/4/27
 Time: 11:13
 功能: TypeScript脚本

 */

/**
 * 验证手机号码
 * @param val
 * @constructor
 */
const ValidatorMobile = ( val : string ) : string => {
    if ( !Number.isFinite( val ) && val.length != 11 ) {
        return '请输入合法手机号码(11位长度)';
    }

    if ( /^1[3|4|5|8]\d{9}$/.test( val ) ) {
        return '';  //对的
    }
    else {
        return '请输入合法手机号码';  //不对
    }

    return '';
}

/**
 * 验证密码
 * @param val
 * @constructor
 */
const ValidatorPassword = ( val : string ) : string => {
    if ( val.length < 3 ) {
        return '请输入合法密码(至少3位长度)';
    }

    return '';
}

const IsEmail = ( val : string ) : boolean => {
    //验证邮箱的正则表达式
    var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;

    if ( !reg.test( val ) ) {
        return false;    //不对
    }
    else {
        return true;
    }
}

export {

    ValidatorMobile ,
    ValidatorPassword ,
    IsEmail ,

}
