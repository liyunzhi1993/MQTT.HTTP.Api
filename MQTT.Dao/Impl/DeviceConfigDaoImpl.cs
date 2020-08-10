using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using MQTT.API.Entity;

namespace MQTT.API.Dao.Impl
{
    public class DeviceConfigDaoImpl : BaseDao, IDeviceConfigDao
    {
        public DeviceConfigDaoImpl(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        /// <summary>
        /// 根据配置Key字符串列表来获取配置列表
        /// </summary>
        /// <param name="configKeyListStr"></param>
        /// <param name="deviceSn"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DeviceConfig>> GetByConfigKeyListAsync(string[] configKeyList,string deviceSn)
        {
            return await QueryListAsync<DeviceConfig>("select * from device_config where DeviceSn = @deviceSn and ConfigKey in @configKeyList", new { deviceSn, configKeyList });
        }

        /// <summary>
        /// 根据设备编号获取配置列表
        /// </summary>
        /// <param name="deviceSn"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DeviceConfig>> GetListByDeviceSnAsync(String deviceSn)
        {
            return await QueryListAsync<DeviceConfig>("select * from device_config where DeviceSn = @deviceSn",new { deviceSn } );
        }

        /// <summary>
        /// 根据配置Key字符串来获取配置
        /// </summary>
        /// <param name="configKeyListStr"></param>
        /// <param name="deviceSn"></param>
        /// <returns></returns>
        public async Task<DeviceConfig> GetByConfigKeyAsync(string configKey, string deviceSn)
        {
            return await QueryFirstAsync<DeviceConfig>("select * from device_config where DeviceSn = @deviceSn and ConfigKey =@configKey", new { deviceSn, configKey });
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="kernelDeviceMonitor"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(DeviceConfig deviceConfig)
        {
            using (var connection = Connection)
            {
                deviceConfig.CreateTime = DateTime.Now;
                return await connection.InsertAsync(deviceConfig) > 0;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="kernelDeviceMonitor"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(DeviceConfig deviceConfig)
        {
            using (var connection = Connection)
            {
                deviceConfig.UpdateTime = DateTime.Now;
                return await connection.UpdateAsync(deviceConfig);
            }
        }
    }
}
