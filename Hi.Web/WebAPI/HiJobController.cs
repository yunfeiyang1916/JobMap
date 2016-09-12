using Job.Common;
using Job.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace Hi.Web.WebAPI
{
    public class HiJobController : ApiController
    {
        #region 获取job基本信息
        /// <summary>
        /// 获取job基本信息
        /// </summary>
        /// <param name="place">地区(如:北京 上海)</param>
        /// <param name="key">关键字(如:.net)</param>
        /// <param name="sources">数据源[前程_智联_拉勾_猎聘 注意以'_'分割]</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public object GetJobBasicInfo(string place, string key, string sources, int pageIndex)
        {
            List<JobInfo> jobInfoList = new List<JobInfo>();

            var sourceList = sources.Split('_');
            if (sourceList.Length <= 0)
                return new { state = 0, messg = "您输入的数据源有误" };
            foreach (var sourceValue in sourceList)
            {
                switch (sourceValue)
                {
                    case "智联":
                        IRequest zhiLianRequest = new ZhiLianRequest();
                        jobInfoList.AddRange(zhiLianRequest.GetJobs(key, place, pageIndex));
                        break;
                    case "拉勾":
                        IRequest laGouRequest = new LaGouRequest();
                        jobInfoList.AddRange(laGouRequest.GetJobs(key, place, pageIndex));
                        break;
                    case "前程":
                        IRequest job51Request = new Job51Request();
                        jobInfoList.AddRange(job51Request.GetJobs(key, place, pageIndex));
                        break;
                    
                    case "猎聘":
                        IRequest liePinRequest = new LiePinRequest();
                        jobInfoList.AddRange(liePinRequest.GetJobs(key, place, pageIndex));
                        break;
                }
            }
            return jobInfoList;
        }
        #endregion

        #region  获取job详细信息
        /// <summary>
        ///  获取job详细信息
        /// </summary>
        /// <param name="url"></param>        
        /// <returns></returns>
        public object GetJobDetailsInfo(string url)
        {
            #region 设置数据源
            //猎聘网 = 1,智联招聘 = 2,前程无忧 = 3,拉勾网 = 4
            DataType dateType = new DataType();
            if (url.Contains("zhaopin.com/"))
                dateType = DataType.智联招聘;
            else if (url.Contains("liepin.com/"))
                dateType = DataType.猎聘网;
            else if (url.Contains("51job.com/"))
                dateType = DataType.前程无忧;
            else if (url.Contains("lagou.com/"))
                dateType = DataType.拉勾网;
            #endregion
            JobRequest client = new JobRequest();
            var data = client.GetUrlInfo(url, dateType);
            return new { data = data };
        }
        #endregion
    }
}