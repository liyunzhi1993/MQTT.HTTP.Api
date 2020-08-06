using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model.In
{
    public class AuthorizeInModel
    {
        /// <summary>   
        /// 设备编号
        /// </summary>
        [Required(ErrorMessage = "设备编号不能为空")]
        public string DeviceSn { get; set; }
        /// <summary>
        /// 设备密码
        /// </summary>
        [Required(ErrorMessage = "设备密码不能为空")]
        public string DevicePassword { get; set; }
        /// <summary>
        /// 软件版本【一期不使用】
        /// </summary>
        public string Version { get; set; }
    }
}
