using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MQTT.API.Common.Enum
{
    public enum ResponseCodeEnum
    {
        [Description("请求(或处理)成功")]
        Success = 200, //请求(或处理)
        [Description("请求(或处理)失败")]
        Fail = 0, //请求(或处理)失败

        [Description("内部请求出错")]
        Error = 500, //内部请求出错

        [Description("请求参数不完整或不正确")]
        ParameterError = 400,//请求参数不完整或不正确

        [Description("未授权标识")]
        Unauthorized = 401,//未授权标识

        [Description("授权参数不足")]
        AuthParameterError = 402,//授权参数不足

        [Description("请求TOKEN失效")]
        TokenInvalid = 403,//请求TOKEN失效
        [Description("请求的网页不存在")]
        PageDoesNotExist =404, // 请求的网页不存在

        [Description("HTTP请求类型不合法")]
        HttpMehtodError = 405,//HTTP请求类型不合法

        [Description("HTTP请求不合法,请求参数可能被篡改")]
        HttpRequestError = 406,//HTTP请求不合法

        [Description("该URL已经失效")]
        URLExpireError = 407,//HTTP请求不合法

        [Description("缺少签名")]
        NoSignature = 408,//缺少签名
        [Description("请求过于频繁")]
        MoreTimes = 409,//缺少签名 
        [Description("无效设备")]
        InvalidDeviceSn = 410//缺少签名
    }
}
