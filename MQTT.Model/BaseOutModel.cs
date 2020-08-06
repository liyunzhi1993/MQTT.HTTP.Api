using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model
{
    public class BaseOutModel<T>
    {
        /// <summary>
        /// 具体业务数据
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 返回状态码【200正常，0有错误】
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误描述，Code为0使用
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public BaseOutModel()
        {
            Code = 200;
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="msg"></param>
        public void Error(string msg)
        {
            Code = 0;
            Message = msg;
        }
    }
}
