using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model.Out
{
    public class PingOutModel
    {
        /// <summary>
        /// 心跳回传命令类型 1：需要重启 2：需要升级 3：需要重新获取配置
        /// </summary>
        public int CommandType { get; set; }
        /// <summary>
        /// 心跳回传命令返回的值 1：需要升级的url
        /// </summary>
        public string CommandValue { get; set; }
    }
}
