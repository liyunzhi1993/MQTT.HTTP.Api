using System;
using System.Collections.Generic;
using System.Text;

namespace DiYi.IoT.Common.Enum
{
    /// <summary>
    /// 设备通过心跳下发的命令类型
    /// </summary>
    public enum DeviceCommandTypeEnum
    {
        /// <summary>
        /// 重启
        /// </summary>
        ReBoot=1,
        /// <summary>
        /// 更新
        /// </summary>
        Update=2,
        /// <summary>
        /// 获取配置
        /// </summary>
        Config=3
    }
}
