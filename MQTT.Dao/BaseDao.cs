using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using MQTT.API.Common.Enum;

namespace MQTT.API.Dao
{
    public abstract class BaseDao
    {
        private string _dbConnectionStr;

        private IDbConnection _dbConnection;

        public BaseDao(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionStr = dbConnectionFactory.GetDbConnectionStr(DbConnectionNameEnum.MainConnection);
        }

        /// <summary>
        /// 查询第一条
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected async Task<T> QueryFirstAsync<T>(string sql, object parameters = null)
        {
            using (var connection = Connection)
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected async Task<IEnumerable<T>> QueryListAsync<T>(string sql, object parameters = null)
        {
            using (var connection = Connection)
            {
                return await connection.QueryAsync<T>(sql, parameters);
            }
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected async Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            using (var connection = Connection)
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }

        /// <summary>
        /// 数据库连接
        /// </summary>
        protected IDbConnection Connection
        {
            get
            {
                if (_dbConnection == null)
                {
                    return new MySqlConnection(_dbConnectionStr);
                }
                else
                {
                    return _dbConnection;
                }
            }
        }
    }
}
