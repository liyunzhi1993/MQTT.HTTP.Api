using MQTT.API.Common.Enum;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MQTT.API.Dao
{
    public class DbConnectionFactoryImpl : IDbConnectionFactory
    {
        private readonly Dictionary<DbConnectionNameEnum, string> _connectionDict;

        public DbConnectionFactoryImpl(Dictionary<DbConnectionNameEnum, string> connectionDict)
        {
            _connectionDict = connectionDict;
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public string GetDbConnectionStr(DbConnectionNameEnum connectionName)
        {
            string connectionString;
            if (_connectionDict.TryGetValue(connectionName, out connectionString))
            {
                return connectionString;
            }
            return string.Empty;
        }
    }
}
