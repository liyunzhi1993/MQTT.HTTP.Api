using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MQTT.API.Common.MQTT
{
    /// <summary>
    /// 配置
    /// </summary>
    public class MqttConfig
    {
        public string Server { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Port { get; set; } = 1883;

        public int SslPort { get; set; }

        public int WebSocketPort { get; set; }

        public int SslWebSocketPort { get; set; }
        public string ClientIdPre { get; set; }
        public bool CleanSeesion { get; set; } = false;
        public List<string> TopicList { get; set; }
    }
}
