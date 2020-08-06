using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT.API.Model.In
{
    public class StatusInModel :BaseIoTModel
    {
        /// <summary>
        /// 摄像头状态
        /// </summary>
        [Range(typeof(int), "0", "1", ErrorMessage = "摄像头状态的值为{1}到{2}之间的整数")]
        public int? CameraStatus { get; set; }
        /// <summary>
        /// 扫码器
        /// </summary>
        [Range(typeof(int), "0", "1", ErrorMessage = "扫码器的值为{1}到{2}之间的整数")]
        public int? Scanner { get; set; }
        /// <summary>
        /// 锁控板状态
        /// </summary>
        [Range(typeof(int), "0", "1", ErrorMessage = "锁控板状态的值为{1}到{2}之间的整数")]
        public int? LockStatus { get; set; }
        /// <summary>
        /// 可用存储空间 单位M
        /// </summary>
        [Range(typeof(decimal), "0", "10000000", ErrorMessage = "可用存储空间的值为{1}到{2}之间的浮点数")]
        public decimal? AvailableStorage { get; set; }
        /// <summary>
        /// Temperature 温度
        /// </summary>
        [Range(typeof(decimal), "0", "100", ErrorMessage = "温度的值为{1}到{2}之间的浮点数")]
        public decimal? Temperature { get; set; }
        /// <summary>
        /// cpu百分比（分子的值，分母100.0，如98% 则只需要98.0）
        /// </summary>
        [Range(typeof(decimal), "0", "100", ErrorMessage = "cpu的值为{1}到{2}之间的浮点数")]
        public decimal? Cpu { get; set; }
        /// <summary>
        /// Ram可用量 单位M
        /// </summary>
        [Range(typeof(decimal), "0", "10000000", ErrorMessage = "Ram可用量的值为{1}到{2}之间的浮点数")]
        public decimal? Ram { get; set; }
        /// <summary>
        /// 供电电压
        /// </summary>
        [Range(typeof(int), "0", "10000000", ErrorMessage = "摄像头状态的值为{1}到{2}之间的整数")]
        public int? SupplyVoltage { get; set; }
    }
}
