using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model
{
    public class MqttContentModel
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string Mid { get; set; }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string Sn { get; set; }
    }
}
    