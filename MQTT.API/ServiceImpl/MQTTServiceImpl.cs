using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MQTT.API.Common.Enum;
using MQTT.API.Common.MQTT;
using MQTT.API.Common.Util;
using MQTT.API.Model;
using MQTT.API.Model.In;
using MQTT.API.Model.Out;
using MQTT.API.MQTT.Service;
using Newtonsoft.Json;

namespace MQTT.API.MQTT.ServiceImpl
{
    public class MQTTServiceImpl :IMQTTService
    {
        private readonly MqttNetClient _mqttNetClient;
        //请自行注入缓存操作类
        //private readonly ICache _cache;

        public MQTTServiceImpl(MqttNetClient mqttNetClient)
        {
            _mqttNetClient = mqttNetClient;
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="publishInModel"></param>
        /// <returns></returns>
        public async Task<BaseOutModel<PublishMessageOutModel>> PublishMessage(PublishMessageInModel publishInModel)
        {
            //如果回复Topic不为空，则需要等待mqtt返回消息，通过MID来保持一致
            if (!String.IsNullOrEmpty(publishInModel.ReceiveTopic))
            {
                //先主动订阅
                _mqttNetClient.PublishMessageAsync(IoTSubscribeMqttTopicEnum.主动订阅.GetEnumDescription(), EncryptUtil.Base64Encode(publishInModel.ReceiveTopic));
                //发布消息
                _mqttNetClient.PublishMessageAsync(publishInModel.Topic, publishInModel.Content);
                var messageId= JsonConvert.DeserializeObject<MqttPayloadModel<MqttContentModel>>(EncryptUtil.Base64Decode(publishInModel.Content)).Con.Mid;
                var cacheKey = $"{RedisKeyEnum.MQTT接收消息.GetEnumDescription()}:{messageId}";
                //8秒为超时
                var endTime = DateTime.Now.AddSeconds(8);
                var received = false;

                var cacheMsg = String.Empty;
                while (!received)
                {
                    //cacheMsg = await _cache.GetAsync<String>(cacheKey);
                    if (cacheMsg != null || DateTime.Now > endTime)
                    {
                        received = true;
                    }
                }
                if (cacheMsg != null)
                {
                    //_cache.RemoveAsync(cacheKey);
                    return new BaseOutModel<PublishMessageOutModel>
                    {
                        Message = "发布消息成功，接收消息成功",
                        Data = new PublishMessageOutModel { Content = cacheMsg, ReceiveStatus = MqttReceiveStatusEnum.Yes.IntToEnum() }
                    };
                }
                return new BaseOutModel<PublishMessageOutModel>
                {
                    Message = "发布消息成功，接收消息失败",
                    Data = new PublishMessageOutModel { ReceiveStatus = MqttReceiveStatusEnum.No.IntToEnum() }
                };
            }
            await _mqttNetClient.PublishMessageAsync(publishInModel.Topic, publishInModel.Content);
            return new BaseOutModel<PublishMessageOutModel> { Message = "发布消息成功",Data=new PublishMessageOutModel { } };
        }
    }
}
