using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MQTT.API.Common.Enum
{
    public enum RedisKeyEnum
    {
        [Description("IOT:DEVICE:SHADOWDATA:")]
        设备影子数据 =1,
        [Description("IOT:DEVICE:REBOOT:")]
        设备重启命令 = 2,
        [Description("IOT:DEVICE:UPDATE:")]
        设备升级命令 = 3,
        [Description("IOT:DEVICE:GETCONFIG:")]
        设备需要获取配置命令 = 4,
        [Description("IOT:DEVICE:INFO:")]
        设备信息 = 5,
        [Description("IOT:MQTT:MESSAGE:")]
        MQTT接收消息 = 6,
    }
}
