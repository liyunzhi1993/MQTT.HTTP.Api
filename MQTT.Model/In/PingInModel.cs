using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model.In
{
    public class PingInModel :BaseIoTModel
    {
        /// <summary>
        /// 网络延迟，单位ms【前端记录请求时间，记录返回请求时间，相减，下次ping传】
        /// </summary>
        [Range(typeof(int), "0", "10000",ErrorMessage ="网络延迟的值为{1}到{2}之间的整数")]
        public int Delay { get; set; }
    }
}
