using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model.Out
{
    public class GetConfigOutModel
    {
        /// <summary>
        /// 配置列表
        /// </summary>
        public List<ConfigOutModel> ConfigList { get; set; }
    }

    public class ConfigOutModel
    {
        /// <summary>
        /// 配置的Key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 配置的Value
        /// </summary>
        public string Value { get; set; }
    }
}
