# http接口封装mqtt协议
# 前言
- .Net Core 3.1 WebApi
- 列出了mqtt客户端的封装目的是为了了解运作机制

## 1、封装mqtt客户端
- mqtt底层协议基于MQTTnet 版本2.8.5 [github地址](https://github.com/chkr1011/MQTTnet)
- 实例化【单例注入AOC】
- 发布消息
- 订阅消息

### 实例化
```csharp
public MqttNetClient(MqttConfig _mqttConfig, EventHandler<MqttApplicationMessageReceivedEventArgs> receivedMessageHanddler,
    ICache iCache)
{
    mqttConfig = _mqttConfig;
    _iCache=iCache;
    var factory = new MqttFactory();
    mqttClient = factory.CreateMqttClient() as MqttClient;
    clientId = "MQTT:Cloud:Client:" + _mqttConfig.ClientIdPre + ":" + Guid.NewGuid();
    //实例化一个MqttClientOptionsBulider
    options = new MqttClientOptionsBuilder()
        .WithTcpServer(_mqttConfig.Server, _mqttConfig.Port)
        .WithCredentials(_mqttConfig.Username, _mqttConfig.Password)
        .WithClientId(clientId)
        .Build();
    if (receivedMessageHanddler != null)
    {
        //是服务器接收到消息时触发的事件，可用来响应特定消息
        mqttClient.ApplicationMessageReceived +=receivedMessageHanddler;
    }
    //是客户端连接成功时触发的事件
    mqttClient.Connected+=Connected;
    //是客户端断开连接时触发的事件
    mqttClient.Disconnected+=Disconnected;
    //连接服务器
    mqttClient.ConnectAsync(options);
}
```
### 发布消息
```csharp
public async Task PublishAsync(string topic, string content)
{
    var message = new MqttApplicationMessageBuilder();
    message.WithTopic(topic);
    message.WithPayload(content);
    message.WithAtMostOnceQoS();
    message.WithRetainFlag(false);
    await mqttClient.PublishAsync(message.Build());
}
```

### 订阅消息
```csharp
public async Task SubscribeAsync(string topic)
{
    await mqttClient.SubscribeAsync(topic);
}
```
### 监听mqtt回复消息
随WebApi应用自启动单例MqttClient去消费消息

- 初始化mqtt连接配置
- 主动订阅Topic
- 消费mqtt消息handler

### 初始化mqtt连接配置&主动订阅Topic代码
```csharp
var mqttConfig = new MqttConfig
{
    Server = _config.Get("EMQServer"),//服务器IP
    Port = int.Parse(_config.Get("EMQPort")),//端口
    ClientIdPre = "DiYi.IoT.MQTT"//IoT MQTT连接
};

mqttConfig.TopicList = new List<string>()
{
    IoTSubscribeMqttTopicEnum.主动订阅.GetEnumDescription()
};
_mqttNetClient = new MqttNetClient(mqttConfig, ReceivedMessageHandler, _cache);
```

### 消费mqtt消息handler
```csharp
public static void ReceivedMessageHandler(object sender, MqttApplicationMessageReceivedEventArgs e)
{
    var topic = e.ApplicationMessage.Topic;
    var payload = EncryptUtil.Base64Decode(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));

    //如果Topic是主动订阅，则去订阅http接口入参传过来的回复Topic字段
    if (topic.Equals(IoTSubscribeMqttTopicEnum.主动订阅.GetEnumDescription()))
    {
        _mqttNetClient.SubscribeAsync(payload);
    }
    else
    {
        var mqttPayloadModel = JsonConvert.DeserializeObject<MqttPayloadModel<MqttContentModel>>(payload);
        //插入缓存是为了在回复的时候用，通过MID保持一致
        var cacheKey = $"{RedisKeyEnum.MQTT接收消息.GetEnumDescription()}:{mqttPayloadModel.Con.Mid}";
        var _cache = _serviceProvider.GetService<ICache>();
        _cache.InsertAsync(cacheKey, payload, 5 * 60);
    }
}
```

## 2、封装http接口

### 发布消息代码
```csharp
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
            cacheMsg = await _cache.GetAsync<String>(cacheKey);
            if (cacheMsg != null || DateTime.Now > endTime)
            {
                received = true;
            }
        }
        //缓存不为空，代表收到了mqtt客户端的响应
        if (cacheMsg != null)
        {
            //移除回复消息缓存
            _cache.RemoveAsync(cacheKey);
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
    //如果Topic为空，则只需要发布消息就好
    await _mqttNetClient.PublishMessageAsync(publishInModel.Topic, publishInModel.Content);
    return new BaseOutModel<PublishMessageOutModel> { Message = "发布消息成功",Data=new PublishMessageOutModel { } };
}
```

### 接口请求入参

|参数名|必选|类型|说明|
|:----|:---|:-----|-----|
| Topic|是|string|发布的Topic  |
| Content|是|string|发布的消息体，需要Base64字符串  |
| ReceiveTopic|否|string|接收消息Topic【只有不为空，才会去处理接收设备端mqtt的回复消息】  |
| DeviceSn|是|string|设备编号  |

### 接口请求出参

|参数名|类型|说明|
|:-----|:-----|-----|
|Data - Content|string|回复消息的内容  |
|Data - ReceiveStatus|integer|回复状态【1 已回复，0 未回复 即超时】  |
| Code|integer|返回状态码【200正常，0有错误】  |
| Message|string|错误描述，Code为0使用  |


### 接口请求示例

#### 1、在Startup中将HttpClient注入IOC
```csharp
services.AddHttpClient("iotmqtt", x =>
{
    x.BaseAddress= new Uri("http://iotmqtt.liyunzhi.com");
});
```
#### 2、调用
```csharp
//请求mqtt的消息体
var content = new
{
    Met = "Reboot",
    Con = new
    {
        Mid = Mid,
        Ts = Ts,
        Sign = Sign,
        Cid = Cid
    }
};

//请求入参模型
var iotPublishMessageModel = new
{
    //请求Topic，目前递易体系的Topic定义请结合自身项目
    Topic = "Device/Reboot",
    //请求mqtt的消息体
    Content = EncryptUtil.Base64Encode(JsonConvert.SerializeObject(content)),
    //消息ID
    MessageId = Mid,
    //可选参数 需要消息回复则传，目前递易体系的回复Topic定义请结合自身项目
    ReceiveTopic = Mid+"Device/Reboot",
    //设备号
    DeviceSn = "123123123"
};

//
var client = _httpClientFactory.CreateClient("iotmqtt");
var httpResponseMessage = client.PostAsJsonAsync("Api/PublishMessage", iotPublishMessageModel).Result;
//如果当前方法用async异步修饰，请不要.Result 请await
//var httpResponseMessage =await client.PostAsJsonAsync("Api/PublishMessage", iotPublishMessageModel);
if (httpResponseMessage.IsSuccessStatusCode)
{
    //成功，返回内容自己自行解析，解析参考接口请求出参
    var data= httpResponseMessage.Content.ReadAsStringAsync().Result;
    //如果当前方法用async异步修饰，请不要.Result 请await
   //var data= await httpResponseMessage.Content.ReadAsStringAsync();
}
else
{
    //这里是失败的代码了，即发送失败了
}
```
