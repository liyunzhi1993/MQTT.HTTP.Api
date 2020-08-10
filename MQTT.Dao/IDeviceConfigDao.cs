using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MQTT.API.Entity;

namespace MQTT.API.Dao
{
    public interface IDeviceConfigDao
    {
        Task<IEnumerable<DeviceConfig>> GetByConfigKeyListAsync(string[] configKeyList, string deviceSn);
        Task<IEnumerable<DeviceConfig>> GetListByDeviceSnAsync(string deviceSn);
        Task<DeviceConfig> GetByConfigKeyAsync(string configKey, string deviceSn);
        Task<bool> InsertAsync(DeviceConfig deviceConfig);
        Task<bool> UpdateAsync(DeviceConfig deviceConfig);
    }
}
