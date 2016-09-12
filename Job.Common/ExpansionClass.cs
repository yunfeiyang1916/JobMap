using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace System
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class ExpansionClass
    {
        #region 关闭进程
        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="t"></param>
        public static void ThreadClose(this Thread t)
        {
            if (t != null)
            {
                try
                {
                    t.Abort();
                    t.DisableComObjectEagerCleanup();
                    t = null;
                }
                catch (Exception)
                { }

            }
        }
        #endregion

        #region MD5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            #region MD5实现方式一
            //MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //byte[] bytValue, bytHash;
            //bytValue = System.Text.Encoding.UTF8.GetBytes(str);
            //bytHash = md5.ComputeHash(bytValue);
            //md5.Clear();
            //string sTemp = "";
            //for (int i = 0; i < bytHash.Length; i++)
            //{
            //    sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
            //} 
            #endregion

            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
        }
        #endregion

        #region 序列号对象
        /// <summary>
        /// 序列号对象
        /// </summary>
        /// <param name="myclass"></param>
        /// <returns></returns>
        public static string ToJson(this object myclass)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(myclass);
        }
        #endregion

        #region 字符串的扩展方法
        /// <summary>
        /// 转DateTime 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? MyToDateTime(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            else
                return DateTime.Parse(str);
        }

        /// <summary>
        /// 转double
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double MyToDouble(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return -1;
            else
                return double.Parse(str);
        }

        /// <summary>
        /// 转int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int MyToInt(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return -1;
            else
                return int.Parse(str);
        }
        #endregion
    }
}
