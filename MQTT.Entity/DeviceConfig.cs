using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace MQTT.API.Entity
{
    /// <summary>
    /// 设备配置表
    /// </summary>
    [Table("device_config")]
    public class DeviceConfig
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 配置Key
        /// </summary>
        public string ConfigKey { get; set; }
        /// <summary>
        /// 配置Value
        /// </summary>
        public string ConfigValue { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceSn { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
