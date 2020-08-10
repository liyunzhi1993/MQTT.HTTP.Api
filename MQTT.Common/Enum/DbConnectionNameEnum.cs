using System;
using System.Collections.Generic;
using System.Text;

namespace MQTT.API.Common.Enum
{
    public enum DbConnectionNameEnum
    {
        MainConnection=1,//主数据库
        AccountConnection=2,//账户数据库
        /// <summary>
        /// IOT数据库
        /// </summary>
        IoTConnection=3,
    }
}
