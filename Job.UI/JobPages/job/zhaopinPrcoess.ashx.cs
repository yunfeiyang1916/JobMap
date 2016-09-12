using Job.Common;
using Job.Model;
using Job.UI.Pages.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.HttpContext.Current.Server;

namespace Job.UI.Pages.job
{
    /// <summary>
    /// zhaopinPrcoess 的摘要说明
    /// </summary>
    public class zhaopinPrcoess : HttpHandlerBase, IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string _result = string.Empty;
            base.InitPostData(context);
            switch (base.OperationCMD)
            {
                //读取招聘简单信息
                case "GETZHAOPININFO":
                    _result = GetZhaoPinInfo(dicData);
                    break;
                case "MYLOGIN"://登录
                    _result = MyLogin(dicData);
                    break;
                case "GETZHAOPINDETAILSINFO"://读取招聘详细信息
                    _result = GetZhaoPinDetailsInfo(dicData["obj"], context);
                    break;
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(_result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region 读取招聘详细信息
        /// <summary>
        /// 读取招聘详细信息
        /// </summary>
        /// <param name="dicData"></param>
        /// <returns></returns>
        private string GetZhaoPinInfo(Dictionary<string, Dictionary<string, string>> dicData)
        {
            Dictionary<string, string> dic = dicData["sub"];
            Dictionary<string, string> dic_obj = dicData["obj"];
            var key = GetDicKeyValue(dic, "inp_key") ?? ".net";
            var city = GetDicKeyValue(dic, "sel_city") ?? "上海";
            var chk_zhilian = GetDicKeyValue(dic, "chk_zhilian") ?? "False";
            var chk_liepin = GetDicKeyValue(dic, "chk_liepin") ?? "False";
            var chk_qiancheng = GetDicKeyValue(dic, "chk_qiancheng") ?? "False";
            var chk_lashou = GetDicKeyValue(dic, "chk_lashou") ?? "False";
            var index = GetDicKeyValue(dic_obj, "index") ?? "1";

            var city_liepin = DataClass.GetDic_liepin(city);
            var city_qiancheng = DataClass.GetDic_qiancheng(city);
            var city_zhilian = DataClass.GetDic_zhilian(city);

            string url_智联招聘 = string.Format("http://sou.zhaopin.com/jobs/searchresult.ashx?jl={0}&kw={1}&p={2}", city_zhilian, key, index);
            string url_猎聘网 = string.Format("http://www.liepin.com/zhaopin/?key={0}&dqs={1}&curPage={2}", key, city_liepin, index);
            string url_前程无忧 = string.Format("http://search.51job.com/jobsearch/search_result.php?jobarea={0}&keyword={1}&curr_page={2}&fromJs=1", city_qiancheng, key, index);
            //            string url_拉勾网 = string.Format("http://www.lagou.com/jobs/list_{0}?city={1}&pn={2}", key, city_zhilian, index);//city_zhilian  这里和智联一样直接用的中文地址码
            string url_拉勾网 = string.Format("http://www.lagou.com/jobs/positionAjax.json?city={0}", city_zhilian);

            List<ZhaopinInfo> zpInfoList = new List<ZhaopinInfo>();
            MyHttpClient client = new MyHttpClient();
            try
            {
                if (chk_zhilian == "True")
                    client.GetRequest(ref zpInfoList, url_智联招聘, ZhaopinType.智联招聘);
                if (chk_liepin == "True")
                    client.GetRequest(ref zpInfoList, url_猎聘网, ZhaopinType.猎聘网);
                if (chk_qiancheng == "True")
                    client.GetRequest(ref zpInfoList, url_前程无忧, ZhaopinType.前程无忧);
                if (chk_lashou == "True")
                    client.GetRequest(ref zpInfoList, url_拉勾网, ZhaopinType.拉勾网,index,key);
            }
            catch (Exception)
            { }

            return zpInfoList.ToJson();
        }
        #endregion

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dicData"></param>
        /// <returns></returns>
        private string MyLogin(Dictionary<string, Dictionary<string, string>> dicData)
        {
            Dictionary<string, string> dic = dicData["sub"];
            Dictionary<string, string> dic_obj = dicData["obj"];
            var userName = GetDicKeyValue(dic, "userName") ?? string.Empty;
            var userPass = GetDicKeyValue(dic, "userPass") ?? string.Empty;
            var type = GetDicKeyValue(dic_obj, "type") ?? string.Empty;
            MyHttpClient client = new MyHttpClient();

            return "";
        }

        #region 取Dictionary中的value 如果没有返回null
        /// <summary>
        /// 取Dictionary中的value 如果没有返回null
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetDicKeyValue(Dictionary<string, string> dic, string key)
        {
            if (dic.Keys.Contains(key))
                return string.IsNullOrEmpty(dic[key]) ? null : dic[key];
            return null;
        }
        #endregion

        private string GetZhaoPinDetailsInfo(Dictionary<string, string> dic, HttpContext context)
        {
            MyHttpClient client = new MyHttpClient();
            try
            {
                return client.GetUrlInfo(context.Server.UrlDecode(dic["url"]), dic["type"]);
            }
            catch (Exception ex)
            {
                return ("后台请求异常~请联系管理员zhaopeiym@163.com" + ex.ToString()).ToJson();
            }

        }
    }
}