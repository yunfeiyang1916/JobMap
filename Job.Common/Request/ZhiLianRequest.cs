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
    /// <summary>智联招聘请求类</summary>
    public class ZhiLianRequest : BaseRequest
    {
        #region 属性

        private String _Name = "智联招聘";
        /// <summary>名称</summary>
        public override String Name { get { return _Name; } set { _Name = value; } }

        private Dictionary<String, Dictionary<String, Int32>> _UsefulDic;
        /// <summary>有用数据字典，用于限制只存储有效的数据，以关键词为键，城市与数据有效页码组成的字典为值</summary>
        public override Dictionary<String, Dictionary<String, Int32>> UsefulDic { get { return _UsefulDic; } set { _UsefulDic = value; } }

        private String _UrlTemplate = "http://sou.zhaopin.com/jobs/searchresult.ashx?jl={0}&kw={1}&p={2}&kt=1";
        /// <summary>网址模板，这里不能直接赋值，要不然UrlTemplate永远都是第一个被赋的值</summary>
        public String UrlTemplate { get { return _UrlTemplate; } set { _UrlTemplate = value; } }

        #endregion

        #region 构造函数
        /// <summary>智联招聘请求类</summary>
        public ZhiLianRequest()
        {
            _UsefulDic = new Dictionary<String, Dictionary<String, Int32>>();
            _UsefulDic.Add(".net", new Dictionary<String, Int32>() { { "北京", 12 }, { "上海", 8 }, { "杭州", 2 }, { "苏州", 2 }, { "郑州", 3 } });
            _UsefulDic.Add("java", new Dictionary<String, Int32>() { { "北京", 30 }, { "上海", 12 }, { "杭州", 15 }, { "苏州", 5 }, { "郑州", 8 } });
            _UsefulDic.Add("php", new Dictionary<String, Int32>() { { "北京", 12 }, { "上海", 9 }, { "杭州", 3 }, { "苏州", 1 }, { "郑州", 6 } });
            _UsefulDic.Add("会计", new Dictionary<String, Int32>() { { "北京", 20 }, { "上海", 30 }, { "杭州", 7 }, { "苏州", 4 }, { "郑州", 14 } });
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
            //组装url
            String url = String.Format(UrlTemplate, city, key, pageIndex);
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
            //查找职位列表，并去除第一列的标题项，每页有60条
            var tagList = doc.Find("#newlist_list_content_table .newlist").Skip(1);
            if (tagList == null || tagList.Count() <= 0)
            {
                return null;
            }
            List<JobInfo> list = new List<JobInfo>();
            foreach (var item in tagList)
            {
                JobInfo job = new JobInfo();
                job.key = key;
                job.city = city;
                var title = item.FindFirst(".zwmc div a");
                job.titleName = title.InnerText();
                job.info_url = title.Attribute("href").AttributeValue;
                job.company = item.FindFirst(".gsmc a").InnerText().Trim(' ');
                job.salary_em = item.FindSingle(".zwyx").InnerText();
                var ss = SplitSalary(job.salary_em);
                job.min_salary = ss[0];
                job.max_salary = ss[1];
                job.area = item.FindSingle(".gzdd").InnerText();
                job.date = item.FindSingle(".gxsj span").InnerText();
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
                var ul = doc.FindFirst(".terminal-company");
                if (ul != null)
                {
                    var li = ul.FindLast("li");
                    if (li != null)
                    {
                        JobInfo job = new JobInfo();
                        String address = li.FindSingle("strong").InnerText().Replace("查看公司地图", "").Trim(' ');
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
