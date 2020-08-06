using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MQTT.API.Model;
using MQTT.API.Model.In;
using MQTT.API.Model.Out;
using MQTT.API.MQTT.Service;
using Microsoft.AspNetCore.Mvc;

namespace MQTT.API.Controllers
{
    /// <summary>
    /// MQTT的Api
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class ApiController : ControllerBase
    {
        private readonly IMQTTService _mqttService;
        public ApiController(IMQTTService mqttService)
        {
            _mqttService = mqttService;
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="publishMessageInModel"></param>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseOutModel<PublishMessageOutModel>> PublishMessage(PublishMessageInModel publishMessageInModel)
        {
            return await _mqttService.PublishMessage(publishMessageInModel);
        }

        ///// <summary>
        ///// 接收消息
        ///// </summary>
        ///// <param name="receiveMessageInModel"></param>
        ///// <remarks>
        ///// </remarks>
        ///// <returns></returns>
        //[HttpPost]
        //[DeviceCheck]
        //public async Task<BaseOutModel<ReceiveMessageOutModel>> ReceiveMessage(ReceiveMessageInModel receiveMessageInModel)
        //{
        //    return await _mqttService.ReceiveMessage(receiveMessageInModel);
        //}
    }
}
