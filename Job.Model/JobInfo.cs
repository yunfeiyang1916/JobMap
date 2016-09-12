using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Job.Model
{
    /// <summary>工作信息</summary>
    public class JobInfo
    {
        /// <summary>搜索关键词</summary>
        public String key;
        /// <summary>标题</summary>
        public string titleName;
        /// <summary>公司名称</summary>
        public string company;
        /// <summary>省市</summary>
        public string city;
        /// <summary>区</summary>
        public string area;
        /// <summary>地址</summary>
        public String address;
        /// <summary>发布时间</summary>
        public string date;
        /// <summary>薪资方式</summary>
        public string salary;
        /// <summary>薪资</summary>
        public string salary_em;
        /// <summary>详细信息url</summary>
        public string info_url;
        /// <summary>来源</summary>
        public string source;
        /// <summary>最低薪水</summary>
        public double min_salary;
        /// <summary>最高薪水</summary>
        public double max_salary;

        /// <summary>重写ToString</summary>
        public override string ToString()
        {
            return String.Format("招聘主信息:{0},公司名称:{1},薪资:{2},来源:{3},地址:{4}.",titleName,company,salary_em,source,address);
        }

    }
}
