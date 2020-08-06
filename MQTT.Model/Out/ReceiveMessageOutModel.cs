using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model.Out
{
    public class ReceiveMessageOutModel
    {
        /// <summary>
        /// 回复消息的内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 回复状态【0：未回复，即超时 1：已回复】
        /// </summary>
        public int ReceiveStatus { get; set; }
    }
}
