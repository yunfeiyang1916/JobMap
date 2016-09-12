using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Common
{
    /// <summary>请求工厂</summary>
    public class RequestFactory
    {
        /// <summary>缓存</summary>
        private static Dictionary<String, IRequest> cache = new Dictionary<String, IRequest>();
        /// <summary>根据来源创建</summary>
        /// <param name="source">来源</param>
        /// <returns></returns>
        public static IRequest Create(String source)
        {
            switch (source)
            {
                case "智联招聘":
                    return new ZhiLianRequest();
                case "前程无忧":
                    return new Job51Request();
                case "猎聘网":
                    return new LiePinRequest();
                case "拉勾网":
                    return new LaGouRequest();
                default:
                    throw new ArgumentException("无效的来源：" + source);
            }
        }

        /// <summary>根据来源获取请求实体，带缓存</summary>
        /// <param name="source">来源</param>
        /// <returns></returns>
        public static IRequest Get(String source)
        {
            if (!cache.ContainsKey(source))
            {
                cache[source] = Create(source);
            }
            return cache[source];
        }
    }
}
