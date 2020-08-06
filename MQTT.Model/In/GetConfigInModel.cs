using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model.In
{
    public class GetConfigInModel:BaseIoTModel
    {
        /// <summary>
        /// 是否获取全部配置，如果为false，则获取configList列表中的数据
        /// </summary>
        public bool IsAll { get; set; } = false;
        /// <summary>
        /// 单个获取、批量获取配置
        /// </summary>
        [Required(ErrorMessage = "配置Key列表不能为空")]
        public List<ConfigKeyInModel> ConfigList { get; set; }
    }

    public class ConfigKeyInModel
    {
        /// <summary>
        /// 配置的Key
        /// </summary>
        [Required(ErrorMessage = "配置Key不能为空")]
        public string Key { get; set; }
    }
}
