using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace MQTT.API.Common.Util
{
    public class IoCUtil
    {
        /// <summary>  
        /// 批量注册
        /// </summary>
        /// <param name="services"></param>  
        /// <param name="assemblyName">程序集</param>
        public static void BatchRegister(IServiceCollection services, string assemblyName)
        {
            var assembly = Assembly.Load(assemblyName);
            var ts = assembly.GetTypes().Where(s => !s.IsInterface && s.FullName.EndsWith("Impl")).ToList();
            foreach (var implType in ts)
            {
                //获取接口名称忽略大小学
                var interfaceType = implType.GetInterface("I" + implType.Name.Replace("Impl", ""), true);
                if (interfaceType != null)
                {
                    services.AddSingleton(interfaceType, implType);
                }
            }
        }
    }
}
