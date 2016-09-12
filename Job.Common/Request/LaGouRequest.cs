using System;
using System.Collections.Generic;
using Job.Model;
using System.Net.Http;
using System.Web.Script.Serialization;
using NewLife.Log;
using System.Net;
using System.Diagnostics;
using System.Linq;
using Job.Model.Entity;
using XCode;
using System.Threading;
using Ivony.Html.Parser;
using Ivony.Html;
using System.Text.RegularExpressions;

namespace Job.Common
{
    /// <summary>拉勾网请求类</summary>
    public class LaGouRequest : BaseRequest
    {
        #region 属性

        private String _Name = "拉勾网";
        /// <summary>名称</summary>
        public override String Name { get { return _Name; } set { _Name = value; } }

        private Dictionary<String, Dictionary<String, Int32>> _UsefulDic;
        /// <summary>有用数据字典，用于限制只存储有效的数据，以关键词为键，城市与数据有效页码组成的字典为值</summary>
        public override Dictionary<String, Dictionary<String, Int32>> UsefulDic { get { return _UsefulDic; } set { _UsefulDic = value; } }

        private String _UrlTemplate = "http://www.lagou.com/jobs/positionAjax.json?city={0}&first=true&pn={1}&kd={2}";
        /// <summary>网址模板，这里不能直接赋值，要不然UrlTemplate永远都是第一个被赋的值</summary>
        public String UrlTemplate { get { return _UrlTemplate; } set { _UrlTemplate = value; } }

        private String _DetailUrl = "http://www.lagou.com/jobs/{0}.html";
        /// <summary>详情地址</summary>
        public String DetailUrl { get { return _DetailUrl; } set { _DetailUrl = value; } }

        #endregion

        #region 构造函数
        /// <summary>拉勾网请求类</summary>
        public LaGouRequest()
        {
            _UsefulDic = new Dictionary<String, Dictionary<String, Int32>>();
            _UsefulDic.Add(".net", new Dictionary<String, Int32>() { { "北京", 30 }, { "上海", 30 }, { "杭州", 15 }, { "苏州", 6 }, { "郑州", 5 } });
            _UsefulDic.Add("java", new Dictionary<String, Int32>() { { "北京", 30 }, { "上海", 30 }, { "杭州", 30 }, { "苏州", 12 }, { "郑州", 12 } });
            _UsefulDic.Add("php", new Dictionary<String, Int32>() { { "北京", 30 }, { "上海", 30 }, { "杭州", 30 }, { "苏州", 4 }, { "郑州", 10 } });
            _UsefulDic.Add("会计", new Dictionary<String, Int32>() { { "北京", 30 }, { "上海", 13 }, { "杭州", 6 }, { "苏州", 1 }, { "郑州", 1 } });
        }

        #endregion

        #region 方法

        /// <summary>获取招聘职位集合</summary>
        /// <param name="key">搜索关键词</param>
        /// <param name="city">城市</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        public override IList<JobInfo> GetJobs(String key, String city, Int32 pageIndex = 1, Int32 pageSize = 200)
        {
            //组装url
            String url = String.Format(UrlTemplate, city, pageIndex, key);
            XTrace.WriteLine("完整Url为：{0}", url);
            return UseAgent<IList<JobInfo>>(url, r =>
            {
                //json序列化
                JavaScriptSerializer jss = new JavaScriptSerializer();
                LagouInfo lagouInfo = jss.Deserialize<LagouInfo>(r);
                if (lagouInfo != null && lagouInfo.content != null && lagouInfo.content.positionResult != null && lagouInfo.content.positionResult.result != null)
                {
                    List<JobInfo> list = new List<JobInfo>();
                    foreach (var item in lagouInfo.content.positionResult.result)
                    {
                        JobInfo job = new JobInfo();
                        job.key = key;
                        job.city = item.city;
                        job.area = item.city + item.district;
                        job.company = item.companyFullName;
                        job.titleName = item.positionName;
                        job.info_url = String.Format(DetailUrl, item.positionId);
                        job.date = item.formatCreateTime;
                        job.salary = "月薪";
                        job.salary_em = item.salary;
                        var ss = SplitSalary(job.salary_em);
                        job.min_salary = ss[0]*1000;
                        job.max_salary = ss[1]*1000;
                        job.source = Name;
                        var detail = GetDetail(job.info_url);
                        job.address = detail != null ? detail.address : null;
                        list.Add(job);
                    }
                    return list;
                }
                return null;
            });
        }

        /// <summary>获取详情信息</summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public override JobInfo GetDetail(String url)
        {
            if (String.IsNullOrEmpty(url))
            {
                return null;
            }

            return UseAgent<JobInfo>(url, r =>
            {
                JumonyParser jumonyParser = new JumonyParser();
                var doc = jumonyParser.Parse(r);
                var div = doc.FindFirst(".work_addr");
                JobInfo job = new JobInfo();
                String address = div.InnerText().Replace(" - ", "").Trim();
                //长度至少大于6
                if (!String.IsNullOrWhiteSpace(address) && address.Length > 6)
                {
                    //只有包含中文的地址才有价值
                    Regex regex = new Regex("[\u4e00-\u9fa5]");
                    if (regex.IsMatch(address))
                    {
                        job.address = address;

                        return job;
                    }
                }
                return null;
            });
        }

        /// <summary>测试代理，获取一批代理</summary>
        /// <param name="key">搜索关键词</param>
        /// <param name="city">城市</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        public void TestProxy(String key, String city, Int32 pageIndex = 1, Int32 pageSize = 200)
        {
            //先跑一批代理信息
            ProxyInfo.SaveAll();

            String url = String.Format(UrlTemplate, city, pageIndex, key);
            XTrace.WriteLine("完整Url为：{0}", url);
            var proxyInfoList = ProxyInfo.FindAll(null, ProxyInfo._.ID, null, 0, 0);
            foreach (var proxyInfo in proxyInfoList)
            {
                var hch = new HttpClientHandler() { Proxy = new WebProxy(proxyInfo.IP, proxyInfo.Port), UseProxy = true };
                HttpClient httpClient = new HttpClient(hch);
                Stopwatch sw = new Stopwatch();
                XTrace.WriteLine("当前代理：{0}:{1}", proxyInfo.IP, proxyInfo.Port);
                ProxyInfoLog log = new ProxyInfoLog();
                log.ProxyInfoID = proxyInfo.ID;

                log.IP = proxyInfo.IP;
                log.Port = proxyInfo.Port;
                try
                {
                    sw.Start();
                    HttpResponseMessage responseMsg = httpClient.GetAsync(url).Result;
                    sw.Stop();
                    log.Elapsed = sw.ElapsedMilliseconds / 1000.0;
                    XTrace.WriteLine("获取共用时间：{0}秒", log.Elapsed);
                    String result = responseMsg.Content.ReadAsStringAsync().Result;
                    try
                    {
                        //json序列化
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        LagouInfo lagouInfo = jss.Deserialize<LagouInfo>(result);
                        log.IsEnabled = 1;
                        log.EffectiveCount++;
                    }
                    catch (Exception ex)
                    {
                        XTrace.WriteLine("处理响应结果：{0}.失败！！！", result);
                        Console.WriteLine("处理响应结果：{0}.失败！！！", result);
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    XTrace.WriteLine("失败原因:{0}", ex.Message);
                    Console.WriteLine("失败原因:{0}", ex.Message);
                    log.IsEnabled = 0;
                    log.InvalidCount++;
                }
                try
                {
                    log.Save();
                }
                catch (Exception ex)
                {
                    XTrace.WriteLine("{0}:{1}保存失败,失败原因：{2}！！！", log.IP, log.Port, ex.Message);
                }

            }
        }

        #endregion
    }
}
