using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;


namespace MQTT.API.Common.Enum
{
    /// <summary>
    /// 枚举公共方法
    /// </summary>
    public static class EnumExtention
    {
        #region 获取枚举描述
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="enumType">类型</param>
        /// <param name="enumValue">值</param>
        /// <returns></returns>
        public static string GetEnumDescription(this Type enumType, object enumValue)
        {
            try
            {

                FieldInfo fi = enumType.GetField(System.Enum.GetName(enumType, enumValue));
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return (attributes.Length > 0) ? attributes[0].Description : System.Enum.GetName(enumType, enumValue);
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region 获取枚举描述

        /// <summary>
        /// 获取描述信息
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns></returns>
        public static string GetEnumDescription(this System.Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }

        /// <summary>
        /// 获取描述信息
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns></returns>
        public static string GetEnumDescription<T>(this object intEnumValue)
        {
            Type type = typeof(T);
            string name = System.Enum.GetName(type, intEnumValue);
            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute == null)
            {
                return string.Empty;
            }

            return attribute.Description;
        }
        #endregion

        #region 获取枚举值

        /// <summary>
        /// 根据Description获取枚举
        /// 说明：
        /// 单元测试-->通过
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="description">枚举描述</param>
        /// <returns>枚举</returns>
        public static T GetEnumValue<T>(this string description)
        {
            Type _type = typeof(T);
            foreach (FieldInfo field in _type.GetFields())
            {
                DescriptionAttribute[] _curDesc = field.GetDescriptAttr();
                if (_curDesc != null && _curDesc.Length > 0)
                {
                    if (_curDesc[0].Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException(string.Format("{0} 未能找到对应的枚举.", description), "Description");
        }

        /// <summary>
        /// 获取字段Description
        /// </summary>
        /// <param name="fieldInfo">FieldInfo</param>
        /// <returns>DescriptionAttribute[] </returns>
        private static DescriptionAttribute[] GetDescriptAttr(this FieldInfo fieldInfo)
        {
            if (fieldInfo != null)
            {
                return (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            }
            return null;
        }

        #endregion

        #region 获取枚举列表
        /// <summary>
        /// 获取枚举列表
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static SortedList<int, string> GetEnumList(this Type enumType)
        {
            SortedList<int, string> sortedList = null;
            var enumNames = System.Enum.GetValues(enumType);
            int length = enumNames.Length;
            if (length > 0)
            {
                sortedList = new SortedList<int, string>();
                for (int i = 0; i < length; i++)
                {
                    string enumName = enumNames.GetValue(i).ToString();
                    object enumValue = System.Enum.Parse(enumType, enumName);
                    string enumDescription = enumType.GetEnumDescription(enumValue);
                    sortedList.Add((int)enumValue, enumDescription);
                }
            }
            return sortedList;
        }
        #endregion

        #region 枚举转换字符串
        public static string EnumToString<T>(this T _enumType)
        {
            return _enumType.ToString();
        }
        #endregion

        #region 字符串转换枚举
        public static T StringToEnum<T>(this string _value)
        {
            //把要转化的枚举用泛型来代替
            return (T)System.Enum.Parse(typeof(T), _value);
        }
        #endregion

        #region 枚举转换整形
        public static int IntToEnum<T>(this T _enumType)
        {          
            return Convert.ToInt32(_enumType);
        }
        #endregion
    }
}
