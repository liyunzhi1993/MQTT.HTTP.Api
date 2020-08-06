using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model.In
{
    public class ReceiveMessageInModel
    {
        /// <summary>
        /// 设备编号【只有对接过IoT平台才能使用】
        /// </summary>
        [Required(ErrorMessage = "设备编号不能为空")]
        public string DeviceSn { get; set; }
        /// <summary>
        /// 消息ID【与发布的MID请保持一致才能获取到消息】
        /// </summary>
        [Required(ErrorMessage = "消息ID不能为空")]
        public string MessageId { get; set; }
    }
}
