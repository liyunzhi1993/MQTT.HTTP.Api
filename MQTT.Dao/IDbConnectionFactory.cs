using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MQTT.API.Common.Enum;

namespace MQTT.API.Dao
{
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        string GetDbConnectionStr(DbConnectionNameEnum connectionName);
    }
}
