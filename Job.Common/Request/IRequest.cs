using Job.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Common
{
    /// <summary>网站请求接口</summary>
    public interface IRequest
    {
        /// <summary>名称</summary>
        String Name { get; set; }
        /// <summary>有用数据字典，用于限制只存储有效的数据，以关键词为键，城市与数据有效页码组成的字典为值</summary>
        Dictionary<String, Dictionary<String, Int32>> UsefulDic { get; set; }

        /// <summary>获取招聘职位集合</summary>
        /// <param name="key">搜索关键词</param>
        /// <param name="city">城市</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        IList<JobInfo> GetJobs(String key, String city, Int32 pageIndex = 1, Int32 pageSize = 200);

        /// <summary>获取详情信息</summary>
        /// <param name="url"></param>
        /// <returns></returns>
        JobInfo GetDetail(String url);

        /// <summary>批量入库</summary>
        /// <param name="key"></param>
        /// <param name="city"></param>
        void SaveAll(String key, String city);
    }
}
