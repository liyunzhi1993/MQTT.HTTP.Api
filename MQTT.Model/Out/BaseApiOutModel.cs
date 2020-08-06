using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model.Out
{
    public class BaseApiOutModel
    {
        /// <summary>
        /// 执行成功/失败
        /// </summary>
        public bool IsSuccess { get; set; } = true;
    }
}
