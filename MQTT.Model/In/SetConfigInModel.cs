using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model.In
{
    public class SetConfigInModel:BaseIoTModel
    {
        /// <summary>
        /// 配置列表
        /// </summary>
        [Required(ErrorMessage = "配置列表不能为空")]
        public List<ConfigInModel> ConfigList { get; set; }
    }
    public class ConfigInModel
    {
        /// <summary>
        /// 配置的Key
        /// </summary>
        [Required(ErrorMessage = "配置Key不能为空")]
        public string Key { get; set; }
        /// <summary>
        /// 配置的Value
        /// </summary>
        public string Value { get; set; }
    }
}
