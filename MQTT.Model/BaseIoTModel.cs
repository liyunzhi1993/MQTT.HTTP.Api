using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model
{
    public class BaseIoTModel
    {
        /// <summary>
        /// 设备类型【不用传】
        /// </summary>
        public int DeviceType { get; set; }
        /// <summary>
        /// 站点ID【不用传】
        /// </summary>
        public int StationId { get; set; }
        /// <summary>
        /// 商户ID【不用传】
        /// </summary>
        public int TenantId { get; set; }
        /// <summary>
        /// 设备ID【不用传】
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// 设备号【不用传】
        /// </summary>
        public string DeviceSn { get; set; }
    }
}
