using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model
{
    public class MqttPayloadModel<T>
    {
        /// <summary>
        /// Payload方法
        /// </summary>
        public string Met { get; set; }
        /// <summary>
        /// 操作正文 对应每个Method的正文内容
        /// </summary>
        public T Con { get; set; }
        /// <summary>
        /// 后续消息唯一ID
        /// </summary>
        public string Ts { get; set; }
    }
}
