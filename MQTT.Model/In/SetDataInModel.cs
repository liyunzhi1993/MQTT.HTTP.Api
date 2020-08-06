using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model.In
{
    public class SetDataInModel:BaseIoTModel
    {
        /// <summary>
        /// 影子数据
        /// </summary>
        [Required(ErrorMessage = "影子数据不能为空")]
        public string ShadowData { get; set; }
    }
}
