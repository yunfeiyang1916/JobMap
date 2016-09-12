using System;
using System.Collections.Generic;
using System.Linq;
using Job.Model;
using XCode;
using NewLife.Log;
using Ivony.Html.Parser;
using Ivony.Html;
using System.Text.RegularExpressions;

namespace Job.Common
{
    /// <summary>前程无忧请求类</summary>
    public class Job51Request : BaseRequest
    {
        #region 属性

        private String _Name = "前程无忧";
        /// <summary>名称</summary>
        public override String Name { get { return _Name; } set { _Name = value; } }

        private Dictionary<String, Dictionary<String, Int32>> _UsefulDic;
        /// <summary>有用数据字典，用于限制只存储有效的数据，以关键词为键，城市与数据有效页码组成的字典为值</summary>
        public override Dictionary<String, Dictionary<String, Int32>> UsefulDic { get { return _UsefulDic; } set { _UsefulDic = value; } }

        private String _UrlTemplate = "http://search.51job.com/jobsearch/search_result.php?jobarea={0}&keyword={1}&curr_page={2}&fromJs=1";
        /// <summary>网址模板，这里不能直接赋值，要不然UrlTemplate永远都是第一个被赋的值</summary>
        public String UrlTemplate { get { return _UrlTemplate; } set { _UrlTemplate = value; } }

        /// <summary>省市编码字典</summary>
        public static Dictionary<String, String> cityCodeCache = new Dictionary<String, String>();

        #endregion

        #region 构造函数

        /// <summary>静态构造</summary>
        static Job51Request()
        {
            cityCodeCache.Add("北京", "010000");
            cityCodeCache.Add("上海", "020000");
            cityCodeCache.Add("深圳", "040000");
            cityCodeCache.Add("广州", "030200");
            cityCodeCache.Add("杭州", "080200");
            cityCodeCache.Add("成都", "090200");
            cityCodeCache.Add("南京", "070200");
            cityCodeCache.Add("武汉", "180200");
            cityCodeCache.Add("西安", "200200");
            cityCodeCache.Add("厦门", "110300");
            cityCodeCache.Add("长沙", "190200");
            cityCodeCache.Add("苏州", "070300");
            cityCodeCache.Add("天津", "050000");

            cityCodeCache.Add("重庆", "060000");
            cityCodeCache.Add("郑州", "170200");
            cityCodeCache.Add("青岛", "120300");
            cityCodeCache.Add("合肥", "150200");
            cityCodeCache.Add("福州", "110200");
            cityCodeCache.Add("济南", "120200");
            cityCodeCache.Add("大连", "230300");
            cityCodeCache.Add("珠海", "030500");
            cityCodeCache.Add("无锡", "070400");
            cityCodeCache.Add("佛山", "030600");
            cityCodeCache.Add("东莞", "030800");
            cityCodeCache.Add("宁波", "080300");
            cityCodeCache.Add("常州", "070500");
            cityCodeCache.Add("沈阳", "230200");
            cityCodeCache.Add("石家庄", "160200");
            cityCodeCache.Add("昆明", "250200");
            cityCodeCache.Add("南昌", "130200");
            cityCodeCache.Add("南宁", "140200");
            cityCodeCache.Add("哈尔滨", "220200");
            cityCodeCache.Add("海口", "100200");
            cityCodeCache.Add("中山", "030700");
            cityCodeCache.Add("惠州", "030300");
            cityCodeCache.Add("贵阳", "260200");
            cityCodeCache.Add("长春", "240200");
            cityCodeCache.Add("太原", "210200");
            cityCodeCache.Add("嘉兴", "080700");
            cityCodeCache.Add("泰安", "121100");
            cityCodeCache.Add("昆山", "070600");
            cityCodeCache.Add("烟台", "120400");
            cityCodeCache.Add("兰州", "270200");
            cityCodeCache.Add("泉州", "110400");
        }
        /// <summary>前程无忧请求类</summary>
        public Job51Request()
        {
            _UsefulDic = new Dictionary<String, Dictionary<String, Int32>>();
            _UsefulDic.Add(".net", new Dictionary<String, Int32>() { { "北京", 15 }, { "上海", 30 }, { "杭州", 5 }, { "苏州", 3 }, { "郑州", 3 } });
            _UsefulDic.Add("java", new Dictionary<String, Int32>() { { "北京", 35 }, { "上海", 35 }, { "杭州", 18 }, { "苏州", 5 }, { "郑州", 4 } });
            _UsefulDic.Add("php", new Dictionary<String, Int32>() { { "北京", 15 }, { "上海", 15 }, { "杭州", 6 }, { "苏州", 6 }, { "郑州", 5 } });
            _UsefulDic.Add("会计", new Dictionary<String, Int32>() { { "北京", 40 }, { "上海", 40 }, { "杭州", 20 }, { "苏州", 10 }, { "郑州", 7 } });
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
            //组装url
            String url = String.Format(UrlTemplate, cityCode, key, pageIndex);
            XTrace.WriteLine("完整Url为：{0}", url);
            IHtmlDocument doc = null;
            try
            {
                doc = jumonyParser.LoadDocument(url);
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
                return null;
            }
            //查找职位列表，并去除第一列的标题项
            var tagList = doc.Find("#resultList .el:not(.title)");
            if (tagList == null || tagList.Count() <= 0)
            {
                return null;
            }
            List<JobInfo> list = new List<JobInfo>();
            foreach (var item in tagList)
            {
                JobInfo job = new JobInfo();
                job.key = key;
                job.area = job.city = city;
                var t1A = item.FindSingle(".t1 span a");
                job.titleName = t1A.InnerText();
                job.info_url = t1A.Attribute("href").AttributeValue;
                job.company = item.FindSingle(".t2 a").InnerText().Trim();
                job.salary_em = item.FindSingle(".t4").InnerText();
                var ss = SplitSalary(job.salary_em);
                job.min_salary = ss[0];
                job.max_salary = ss[1];
                job.date = item.FindSingle(".t5").InnerText();
                job.salary = "月薪";
                job.source = Name;
                var detail = GetDetail(job.info_url);
                job.address = detail != null ? detail.address : null;
                list.Add(job);

            }
            return list;
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
            try
            {
                JumonyParser jumonyParser = new JumonyParser();
                var doc = jumonyParser.LoadDocument(url);
                var divs = doc.Find(".bmsg");
                if (divs != null && divs.Count() >= 2)
                {
                    var div = divs.ToList()[1];
                    var p = div.FindFirst("p");
                    if (p != null)
                    {
                        JobInfo job = new JobInfo();
                        String address = p.InnerText().Replace("上班地址：", "").Trim();
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
        }

        #endregion
    }
}
