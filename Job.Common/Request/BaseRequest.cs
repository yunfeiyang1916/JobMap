using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using Job.Model.Entity;
using XCode;
using NewLife.Log;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Job.Common
{
    /// <summary>请求基类</summary>
    public abstract class BaseRequest : IRequest
    {
        #region 属性

        /// <summary>名称</summary>
        public abstract string Name { get; set; }

        /// <summary>有用数据字典，用于限制只存储有效的数据，以关键词为键，城市与数据有效页码组成的字典为值</summary>
        public abstract Dictionary<String, Dictionary<String, Int32>> UsefulDic { get; set; }


        private EntityList<ProxyInfoLog> _ProxyInfoLogList;
        /// <summary>所有可用代理记录</summary>
        public EntityList<ProxyInfoLog> ProxyInfoLogList { get { return _ProxyInfoLogList ?? (_ProxyInfoLogList = ProxyInfoLog.FindAll(1)); } set { _ProxyInfoLogList = value; } }

        private ProxyInfoLog _ProxyInfoLog;
        /// <summary>当前可用代理记录</summary>
        public ProxyInfoLog ProxyInfoLog { get { return _ProxyInfoLog; } set { _ProxyInfoLog = value; } }

        private Int32 _MaxTryTimes=10;
        /// <summary>使用代理最大尝试次数，默认10次</summary>
        public Int32 MaxTryTimes { get { return _MaxTryTimes; } set { _MaxTryTimes = value; } }
        #endregion

        #region 方法

        /// <summary>获取招聘职位集合</summary>
        /// <param name="key">搜索关键词</param>
        /// <param name="city">城市</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        public abstract IList<JobInfo> GetJobs(String key, String city, Int32 pageIndex = 1, Int32 pageSize = 200);

        /// <summary>使用代理处理</summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="url"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public TResult UseAgent<TResult>(String url, Func<String, TResult> func)
        {
            EntityList<ProxyInfoLog> logList = null;
            //当前代理记录是否为null，如果不为null继续使用，否则再找下一个
            if (ProxyInfoLog == null)
            {
                //获取有效的代理ip并且无效次数不能超过十次的
                logList = ProxyInfoLogList.FindAll(p => p.IsEnabled == 1 && p.InvalidCount <= 10);

                if (logList == null || logList.Count <= 0)
                {
                    XTrace.WriteLine("没有可用代理ip!!!");
                    return default(TResult);
                }
                ProxyInfoLog = logList[0];
            }

            Random r = new Random();
            //尝试次数
            Int32 tryTimes = 1;
            while (true)
            {
                var hch = new HttpClientHandler() { Proxy = new WebProxy(ProxyInfoLog.IP, ProxyInfoLog.Port), UseProxy = true };
                HttpClient httpClient = new HttpClient(hch);
                XTrace.WriteLine("==============第{0}次开始获取====================", tryTimes);
                String userAgent = HttpHelper.UserAgentList[r.Next(HttpHelper.UserAgentList.Count)];
                XTrace.WriteLine("当前User-Agent为：{0}", userAgent);
                httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
                Int32 sleepTime = r.Next(3) * 1000;
                XTrace.WriteLine("当前等待时间为：{0}", sleepTime);
                Thread.Sleep(sleepTime);
                Stopwatch sw = new Stopwatch();

                XTrace.WriteLine("当前代理：{0}:{1}", ProxyInfoLog.IP, ProxyInfoLog.Port);
                try
                {
                    sw.Start();
                    HttpResponseMessage responseMsg = httpClient.GetAsync(url).Result;
                    sw.Stop();
                    XTrace.WriteLine("获取共用时间：{0}秒", sw.ElapsedMilliseconds / 1000.0);
                    XTrace.WriteLine("==============第{0}次获取结束=====================", tryTimes);
                    String result = responseMsg.Content.ReadAsStringAsync().Result;
                    //调用回调，至于为什么需要回调而不是直接返回请求结果，是因为可能网站禁用ip了，返回不正确的内容，需要继续换ip处理
                    //所以如果不是代理问题是业务问题回调就不要抛出异常
                    try
                    {
                        return func(result);
                    }
                    catch (Exception ex)
                    {
                        XTrace.WriteLine("处理响应结果：{0}.失败！！！", result);
                        //抛出异常，让上一层捕捉到然后换ip去处理
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    XTrace.WriteLine("第{0}次尝试失败！！！失败原因:{1}", tryTimes, ex.InnerException!=null?ex.InnerException.Message:ex.Message);
                    //递增ip记录无效次数
                    ProxyInfoLog.InvalidCount++;
                    ProxyInfoLog.Save();
                    if (tryTimes >= MaxTryTimes)
                    {
                        XTrace.WriteLine("已经{0}次失败，结束获取。", tryTimes);
                        return default(TResult);
                    }
                    tryTimes++;
                    //以无效次数与速度正序排下序
                    ProxyInfoLogList.Sort(new String[] { ProxyInfoLog._.InvalidCount, ProxyInfoLog._.Elapsed }, new Boolean[] { false, false });
                    //换一批代理ip
                    //获取有效的代理ip并且无效次数不能超过十次的
                    logList = ProxyInfoLogList.FindAll(p => p.ID != ProxyInfoLog.ID && p.IsEnabled == 1 && p.InvalidCount <= 10);

                    if (logList == null || logList.Count <= 0)
                    {
                        XTrace.WriteLine("没有可用代理ip!!!");
                        return default(TResult);
                    }
                    ProxyInfoLog = logList[0];
                }
            }
        }

        /// <summary>获取详情信息</summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public abstract JobInfo GetDetail(String url);

        /// <summary>批量入库</summary>
        /// <param name="key"></param>
        /// <param name="city"></param>
        public void SaveAll(String key, String city)
        {
            Int32 pageIndex = 1;
            //最大页码限制
            Int32 maxPageSize = 0;
            //是否设置有用数据字典限制，如果设置则加入判断
            if (UsefulDic != null && UsefulDic.ContainsKey(key))
            {
                if (UsefulDic[key].ContainsKey(city))
                {
                    maxPageSize = UsefulDic[key][city];
                }
            }
            while (true)
            {
                //最大页码限制大于0，并且查询页码数已经大于最大页码限制，则结束循环
                if (maxPageSize > 0 && pageIndex > maxPageSize)
                {
                    break;
                }

                var list = GetJobs(key, city, pageIndex);
                pageIndex++;
                if (list == null || list.Count == 0)
                {
                    break;
                }
                EntityList<PositionInfo> entityList = new EntityList<PositionInfo>();
                foreach (var item in list)
                {
                    PositionInfo p = new PositionInfo();
                    p.FromJobInfo(item);
                    entityList.Add(p);
                }
                try
                {
                    //批量插入数据库
                    entityList.Insert();
                }
                catch (Exception ex)
                {
                    XTrace.WriteLine("批量入库失败。失败原因：{0}！！！", ex.Message);
                }

            }
        }

        /// <summary>拆分薪水范围区间</summary>
        /// <param name="salary"></param>
        /// <returns></returns>
        public static Int32[] SplitSalary(String salary)
        {
            Int32[] ss = new Int32[2];
            if (String.IsNullOrWhiteSpace(salary))
            {
                return ss;
            }
            String[] s = salary.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Regex regex = new Regex(@"\d+");
            if (s.Length > 0)
            {
                Match match = regex.Match(s[0]);
                if (!String.IsNullOrWhiteSpace(match.Value))
                {
                    ss[0] = match.Value.ToInt();
                }
            }
            if (s.Length > 1)
            {
                Match match = regex.Match(s[1]);
                if (!String.IsNullOrWhiteSpace(match.Value))
                {
                    ss[1] = match.Value.ToInt();
                }
            }
            return ss;
        }

        #endregion
    }
}
