using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MQTT.API.Model;
using MQTT.API.Model.In;
using MQTT.API.Model.Out;

namespace MQTT.API.MQTT.Service
{
    public interface IMQTTService
    {
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="publishInModel"></param>
        /// <returns></returns>
        Task<BaseOutModel<PublishMessageOutModel>> PublishMessage(PublishMessageInModel publishInModel);
    }
}
