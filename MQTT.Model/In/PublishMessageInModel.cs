using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model.In
{
    public class PublishMessageInModel
    {
        /// <summary>
        /// 发布的Topic
        /// </summary>
        [Required(ErrorMessage = "发布的Topic不能为空")]
        public string Topic { get; set; }
        /// <summary>
        /// 发布的消息体
        /// </summary>
        [Required(ErrorMessage = "发布的消息不能为空")]
        public string Content { get; set; }

        /// <summary>
        /// 接收消息Topic【只有不为空，IoT才会去处理接收设备端的回复消息】
        /// </summary>
        public string ReceiveTopic { get; set; }
        /// <summary>
        /// 设备编号【只有对接过IoT平台才能使用】
        /// </summary>
        [Required(ErrorMessage = "设备编号不能为空")]
        public string DeviceSn { get; set; }
    }
}
