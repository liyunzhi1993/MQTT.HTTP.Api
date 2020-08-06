using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model.Out
{
    public class PublishMessageOutModel
    {
        /// <summary>
        /// 回复消息的内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 回复状态【1 已回复，0 未回复 即超时】
        /// </summary>
        public int ReceiveStatus { get; set; }
    }
}
