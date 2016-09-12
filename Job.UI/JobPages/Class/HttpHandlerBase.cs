using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

#region 类文件头注释
/* =============================================== 
* 创 建 人：zhaopei
* 联系方式: zhaopeiym@163.com
* 创建日期：2015/3/19 14:57:38
* 修改日期：2015/3/19 14:57:38
* CLR版本：4.0.30319.18408
* 机器名称：ZP-PC
* 用户所在域：zp-PC
* 注册组织名：Microsoft
* 命名空间名称：Job.UI.Pages.Class
* 当前登录用户名：Administrator
* 功能描述：HttpHandlerBase
* ================================================*/
#endregion
namespace Job.UI.Pages.Class
{
    public class HttpHandlerBase : IRequiresSessionState
    {

        //context.Request.Headers["X-Requested-With"]

        /// <summary>
        /// 前台传过来的数据集 【标记了tag】
        /// </summary>
        protected Dictionary<string, Dictionary<string, string>> dicData;

        /// <summary>
        /// 要执行的方法名
        /// </summary>
        protected string OperationCMD;       

        /// <summary>
        /// 初始化前台传过来的值
        /// </summary>
        /// <param name="context"></param>
        protected void InitPostData(HttpContext context, string TagName = null)
        {            
            var values = context.Request.Form[0];
            //需要引入程序集System.Web.Extensions.dll
            JavaScriptSerializer _jsSerializer = new JavaScriptSerializer();
            //将 json 对象字符串  转成 Dictionary 对象
            dicData = _jsSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(values);
            OperationCMD = dicData["BasicData"]["operation"].ToUpper();
        }
    }
}