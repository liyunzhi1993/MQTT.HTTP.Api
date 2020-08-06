using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using MQTT.API.Common.Enum;
using System.Collections.Generic;
using MQTT.API.Common.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json.Serialization;
using MQTT.API.Common.Middleware;
using MQTT.API.Common.Filter;
using MQTT.API.Common.MQTT;
using MQTTnet;
using MQTT.API.Model;
using Newtonsoft.Json;

namespace MQTT.API
{
    public class Startup
    {
        private static MqttNetClient _mqttNetClient;
        private static ServiceProvider _serviceProvider;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMvc(options => {
                options.Filters.Add<ModelActionFilter>();
            })
            .AddNewtonsoftJson(options => {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();//json字符串大小写原样输出
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.Configure<ApiBehaviorOptions>(options =>
             options.SuppressModelStateInvalidFilter = true);

            var configBuilder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", true, true);


            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "IoT物联网MQTT接口",
                    Version = "v1.0",
                    Contact = new OpenApiContact
                    {
                        Name = "在线文档",
                        Email = string.Empty
                    }
                });
                x.CustomSchemaIds(t => t.FullName);
                var basePath = AppContext.BaseDirectory;
                var xmlApiPath = Path.Combine(basePath, "MQTT.API.xml");
                var xmlModelPath = Path.Combine(basePath, "MQTT.Model.xml");
                x.IncludeXmlComments(xmlApiPath, true);
                x.IncludeXmlComments(xmlModelPath, true);
            });

            var mqttConfig = new MqttConfig
            {
                //Server = _config.Get("EMQServer"),
                //Port = int.Parse(_config.Get("EMQPort")),
                ClientIdPre = "MQTT.API.MQTT"//IoT MQTT连接
            };

            mqttConfig.TopicList = new List<string>()
            {
                IoTSubscribeMqttTopicEnum.主动订阅.GetEnumDescription()
            };

            //_mqttNetClient = new MqttNetClient(mqttConfig, ReceivedMessageHandler, _cache);
            _mqttNetClient = new MqttNetClient(mqttConfig, ReceivedMessageHandler);
            services.AddSingleton(_mqttNetClient);

            IoCUtil.BatchRegister(services, "MQTT.API");

            Console.WriteLine("开始监听IoT.MQTT");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseMiddleware(typeof(CustomExceptionHandlerMiddleware));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }

        /// <summary>
        /// 接收MQTT消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ReceivedMessageHandler(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            var topic = e.ApplicationMessage.Topic;
            var payload = EncryptUtil.Base64Decode(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));

            if (topic.Equals(IoTSubscribeMqttTopicEnum.主动订阅.GetEnumDescription()))
            {
                _mqttNetClient.SubscribeAsync(payload);
            }
            else
            {
                var mqttPayloadModel = JsonConvert.DeserializeObject<MqttPayloadModel<MqttContentModel>>(payload);
                var cacheKey = $"{RedisKeyEnum.MQTT接收消息.GetEnumDescription()}:{mqttPayloadModel.Con.Mid}";
                //自行注入 缓存操作类
                //var _cache = _serviceProvider.GetService<ICache>();
                //_cache.InsertAsync(cacheKey, payload, 5 * 60);
            }
        }
    }
}
