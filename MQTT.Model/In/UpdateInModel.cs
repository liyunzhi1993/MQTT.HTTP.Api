using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model.In
{
    public class UpdateInModel
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        [Required(ErrorMessage = "设备编号不能为空")]
        public string DeviceSn { get; set; }
        /// <summary>
        /// 软件包下载地址
        /// </summary>
        [Required(ErrorMessage = "软件包下载地址不能为空")]
        public string Url { get; set; }
    }
}
