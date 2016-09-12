using Ivony.Html;
using Ivony.Html.Parser;
using Job.Model;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using XCode;

namespace Job.Common
{
    /// <summary>拉勾网请求类</summary>
    public class LiePinRequest : BaseRequest
    {
        #region 属性
        private String _Name = "猎聘网";
        /// <summary>名称</summary>
        public override String Name { get { return _Name; } set { _Name = value; } }

        private String _UrlTemplate = "http://www.liepin.com/zhaopin/?key={0}&dqs={1}&curPage={2}";
        /// <summary>网址模板，这里不能直接赋值，要不然UrlTemplate永远都是第一个被赋的值</summary>
        public String UrlTemplate { get { return _UrlTemplate; } set { _UrlTemplate = value; } }

        private Dictionary<String, Dictionary<String, Int32>> _UsefulDic;
        /// <summary>有用数据字典，用于限制只存储有效的数据，以关键词为键，城市与数据有效页码组成的字典为值</summary>
        public override Dictionary<String, Dictionary<String, Int32>> UsefulDic { get { return _UsefulDic; } set { _UsefulDic = value; } }

        /// <summary>省市编码字典</summary>
        public static Dictionary<String, String> cityCodeCache = new Dictionary<String, String>();


        #endregion

        #region 构造函数

        /// <summary>静态构造</summary>
        static LiePinRequest()
        {
            cityCodeCache.Add("北京", "010");
            cityCodeCache.Add("上海", "020");
            cityCodeCache.Add("深圳", "050090");
            cityCodeCache.Add("广州", "050020");
            cityCodeCache.Add("杭州", "070020");
            cityCodeCache.Add("成都", "280020");
            cityCodeCache.Add("南京", "060020");
            cityCodeCache.Add("武汉", "170020");
            cityCodeCache.Add("西安", "270020");
            cityCodeCache.Add("厦门", "090040");
            cityCodeCache.Add("长沙", "180020");
            cityCodeCache.Add("苏州", "060080");
            cityCodeCache.Add("天津", "030");

            cityCodeCache.Add("重庆", "040");
            cityCodeCache.Add("郑州", "150020");
            cityCodeCache.Add("青岛", "250070");
            cityCodeCache.Add("合肥", "080020");
            cityCodeCache.Add("福州", "090020");
            cityCodeCache.Add("济南", "250020");
            cityCodeCache.Add("大连", "210040");
            cityCodeCache.Add("珠海", "050140");
            cityCodeCache.Add("无锡", "060100");
            cityCodeCache.Add("佛山", "050050");
            cityCodeCache.Add("东莞", "050040");
            cityCodeCache.Add("宁波", "070030");
            cityCodeCache.Add("常州", "060040");
            cityCodeCache.Add("沈阳", "210020");
            cityCodeCache.Add("石家庄", "140020");
            cityCodeCache.Add("昆明", "310020");
            cityCodeCache.Add("南昌", "200020");
            cityCodeCache.Add("南宁", "110020");
            cityCodeCache.Add("哈尔滨", "160020");
            cityCodeCache.Add("海口", "130020");
            cityCodeCache.Add("中山", "050130");
            cityCodeCache.Add("惠州", "050060");
            cityCodeCache.Add("贵阳", "120020");
            cityCodeCache.Add("长春", "190020");
            cityCodeCache.Add("太原", "260020");
            cityCodeCache.Add("嘉兴", "070090");
            cityCodeCache.Add("泰安", "250090");
            cityCodeCache.Add("昆山", "060050");
            cityCodeCache.Add("烟台", "250120");
            cityCodeCache.Add("兰州", "100020");
            cityCodeCache.Add("泉州", "090030");
        }
        /// <summary>拉勾网</summary>
        public LiePinRequest()
        {
            _UsefulDic = new Dictionary<String, Dictionary<String, Int32>>();
            _UsefulDic.Add(".net", new Dictionary<String, Int32>() { { "北京", 18 }, { "上海", 9 }, { "杭州", 6 }, { "苏州", 3 }, { "郑州", 3 } });
            _UsefulDic.Add("java", new Dictionary<String, Int32>() { { "北京", 40 }, { "上海", 28 }, { "杭州", 20 }, { "苏州", 4 }, { "郑州", 3 } });
            _UsefulDic.Add("php", new Dictionary<String, Int32>() { { "北京", 28 }, { "上海", 22 }, { "杭州", 12 }, { "苏州", 3 }, { "郑州", 2 } });
            _UsefulDic.Add("会计", new Dictionary<String, Int32>() { { "北京", 25 }, { "上海", 22 }, { "杭州", 10 }, { "苏州", 3 }, { "郑州", 2 } });
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
            JumonyParser jumonyParser = new JumonyParser();
            String cityCode;
            if (!cityCodeCache.TryGetValue(city, out cityCode))
            {
                throw new Exception("不支持城市：" + city);
            }
            //因为猎聘网分页是从第0页开始的，所以页码需要减一
            if (pageIndex > 0)
            {
                pageIndex--;
            }
            //组装url
            String url = String.Format(UrlTemplate, key, cityCode, pageIndex);
            XTrace.WriteLine("完整Url为：{0}", url);

            return UseAgent<IList<JobInfo>>(url, r =>
            {
                IHtmlDocument doc = jumonyParser.Parse(r);
                var liList = doc.Find(".sojob-list li");
                if (liList == null || liList.Count() <= 0)
                {
                    var warnings = doc.Exists(".warning-msg");
                    if (doc.Exists(".warning-msg"))
                    {
                        var warningMsg = doc.FindFirst(".warning-msg").InnerText();
                        if (warningMsg.Contains("系统检测到异常浏览行为"))
                        {
                            //说明要被封号了，抛出异常给上层，让上层换一个ip
                            XTrace.WriteLine(warningMsg);
                            throw new Exception(warningMsg);
                        }
                    }

                    return null;
                }
                List<JobInfo> list = new List<JobInfo>();
                foreach (var item in liList)
                {
                    JobInfo job = new JobInfo();
                    job.key = key;
                    job.city = city;
                    var divJobInfo = item.FindSingle(".job-info");
                    var title = divJobInfo.FindSingle(".job-name");
                    job.titleName = title.Attribute("title").Value().Trim();
                    job.info_url = title.FindFirst("a").Attribute("href").Value();
                    var pCondition = divJobInfo.FindSingle("p.condition ");
                    job.salary_em = pCondition.FindSingle(".text-warning").InnerText();
                    var ss = SplitSalary(job.salary_em);
                    //按年薪算的
                    job.min_salary = ss[0] * 10000 / 12;
                    job.max_salary = ss[1] * 10000 / 12;
                    job.area = pCondition.FindSingle(".area").InnerText();
                    job.date = divJobInfo.FindSingle(".time-info time").InnerText();
                    job.company = item.FindSingle(".company-info .company-name a").InnerText().Trim();
                    job.salary = "月薪";
                    job.source = Name;
                    JobInfo detail = null;//GetDetail(job.info_url);
                    job.address = detail != null ? detail.address : null;
                    list.Add(job);
                }
                return list;
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
                JobInfo job = new JobInfo();
                try
                {
                    var div = doc.FindFirst(".company-infor");
                    if (div != null)
                    {
                        var p = div.FindFirst("p");
                        if (p != null)
                        {
                            String address = p.InnerText().Trim();
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
                        }
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    XTrace.WriteLine("调用{0}获取{1}职位详情时出错，错误原因：{2}！！！", url, Name, ex.Message);
                    return null;
                }
            });
        }
        #endregion
    }
}
