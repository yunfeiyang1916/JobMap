using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Job.Common
{
    public class DataClass
    {
        #region dic_zhilian
        public static Dictionary<string, string> dic_zhilian = new Dictionary<string, string>();
        public static string GetDic_zhilian(string key)
        {
            if (dic_zhilian.Count <= 0)
            {
                dic_zhilian.Add("北京", "北京");
                dic_zhilian.Add("上海", "上海");
                dic_zhilian.Add("广州", "广州");
                dic_zhilian.Add("深圳", "深圳");
                dic_zhilian.Add("天津", "天津");
                dic_zhilian.Add("苏州", "苏州");
                dic_zhilian.Add("重庆", "重庆");
                dic_zhilian.Add("南京", "南京");
                dic_zhilian.Add("杭州", "杭州");
                dic_zhilian.Add("大连", "大连");
                dic_zhilian.Add("成都", "成都");
                dic_zhilian.Add("武汉", "武汉");
                dic_zhilian.Add("长沙", "长沙");
                dic_zhilian.Add("沈阳", "沈阳");
            }
            if (dic_zhilian.Keys.Contains(key))
                return dic_zhilian[key];
            return string.Empty;
        }
        #endregion

        #region dic_qiancheng
        public static Dictionary<string, string> dic_qiancheng = new Dictionary<string, string>();
        public static string GetDic_qiancheng(string key)
        {
            if (dic_qiancheng.Count <= 0)
            {
                dic_qiancheng.Add("北京", "010000");
                dic_qiancheng.Add("上海", "020000");
                dic_qiancheng.Add("广州", "030200");
                dic_qiancheng.Add("深圳", "040000");
                dic_qiancheng.Add("天津", "050000");
                dic_qiancheng.Add("苏州", "070300");
                dic_qiancheng.Add("重庆", "060000");
                dic_qiancheng.Add("南京", "070200");
                dic_qiancheng.Add("杭州", "080200");
                dic_qiancheng.Add("大连", "230300");
                dic_qiancheng.Add("成都", "090200");
                dic_qiancheng.Add("武汉", "180200");
                dic_qiancheng.Add("长沙", "190200");
                dic_qiancheng.Add("沈阳", "230200");
            }
            if (dic_qiancheng.Keys.Contains(key))
                return dic_qiancheng[key];
            return string.Empty;
        }
        #endregion

        #region dic_liepin
        public static Dictionary<string, string> dic_liepin = new Dictionary<string, string>();
        public static string GetDic_liepin(string key)
        {
            if (dic_liepin.Count <= 0)
            {
                dic_liepin.Add("北京", "010");
                dic_liepin.Add("上海", "020");
                dic_liepin.Add("广州", "050020");
                dic_liepin.Add("深圳", "050090");
                dic_liepin.Add("天津", "030");
                dic_liepin.Add("苏州", "060080");
                dic_liepin.Add("重庆", "040");
                dic_liepin.Add("南京", "060020");
                dic_liepin.Add("杭州", "070020");
                dic_liepin.Add("大连", "210040");
                dic_liepin.Add("成都", "280020");
                dic_liepin.Add("武汉", "170020");
                dic_liepin.Add("长沙", "180020");
                dic_liepin.Add("沈阳", "210020");
            }
            if (dic_liepin.Keys.Contains(key))
                return dic_liepin[key];
            return string.Empty;
        }
        #endregion

        #region dic_hi 0:dic_zhilian 1:dic_qiancheng 2:dic_liepin
        public static Dictionary<string, string[]> dic_hi = new Dictionary<string, string[]>();
        /// <summary>
        /// 0:dic_zhilian【智联】
        /// 1:dic_qiancheng【前程】
        /// 2:dic_liepin【猎聘】
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string[] GetDic_hi(string key)
        {
            key = key.Trim().TrimEnd('市');
            if (dic_hi.Keys.Contains(key))
                return dic_hi[key];
            return null;
        }
        #endregion

        /// <summary>静态构造</summary>
        static DataClass()
        {
            dic_hi.Add("北京", new string[] { "北京", "010000", "010" });
            dic_hi.Add("上海", new string[] { "上海", "020000", "020" });
            dic_hi.Add("深圳", new string[] { "深圳", "040000", "050090" });
            dic_hi.Add("广州", new string[] { "广州", "030200", "050020" });
            dic_hi.Add("杭州", new string[] { "杭州", "080200", "070020" });
            dic_hi.Add("成都", new string[] { "成都", "090200", "280020" });
            dic_hi.Add("南京", new string[] { "南京", "070200", "060020" });
            dic_hi.Add("武汉", new string[] { "武汉", "180200", "170020" });
            dic_hi.Add("西安", new string[] { "西安", "200200", "270020" });
            dic_hi.Add("厦门", new string[] { "厦门", "110300", "090040" });
            dic_hi.Add("长沙", new string[] { "长沙", "190200", "180020" });
            dic_hi.Add("苏州", new string[] { "苏州", "070300", "060080" });
            dic_hi.Add("天津", new string[] { "天津", "050000", "030" });

            dic_hi.Add("重庆", new string[] { "重庆", "060000", "040" });
            dic_hi.Add("郑州", new string[] { "郑州", "170200", "150020" });
            dic_hi.Add("青岛", new string[] { "青岛", "120300", "250070" });
            dic_hi.Add("合肥", new string[] { "合肥", "150200", "080020" });
            dic_hi.Add("福州", new string[] { "福州", "110200", "090020" });
            dic_hi.Add("济南", new string[] { "济南", "120200", "250020" });
            dic_hi.Add("大连", new string[] { "大连", "230300", "210040" });
            dic_hi.Add("珠海", new string[] { "珠海", "030500", "050140" });
            dic_hi.Add("无锡", new string[] { "无锡", "070400", "060100" });
            dic_hi.Add("佛山", new string[] { "佛山", "030600", "050050" });
            dic_hi.Add("东莞", new string[] { "东莞", "030800", "050040" });
            dic_hi.Add("宁波", new string[] { "宁波", "080300", "070030" });
            dic_hi.Add("常州", new string[] { "常州", "070500", "060040" });
            dic_hi.Add("沈阳", new string[] { "沈阳", "230200", "210020" });
            dic_hi.Add("石家庄", new string[] { "石家庄", "160200", "140020" });
            dic_hi.Add("昆明", new string[] { "昆明", "250200", "310020" });
            dic_hi.Add("南昌", new string[] { "南昌", "130200", "200020" });
            dic_hi.Add("南宁", new string[] { "南宁", "140200", "110020" });
            dic_hi.Add("哈尔滨", new string[] { "哈尔滨", "220200", "160020" });
            dic_hi.Add("海口", new string[] { "海口", "100200", "130020" });
            dic_hi.Add("中山", new string[] { "中山", "030700", "050130" });
            dic_hi.Add("惠州", new string[] { "惠州", "030300", "050060" });
            dic_hi.Add("贵阳", new string[] { "贵阳", "260200", "120020" });
            dic_hi.Add("长春", new string[] { "长春", "240200", "190020" });
            dic_hi.Add("太原", new string[] { "太原", "210200", "260020" });
            dic_hi.Add("嘉兴", new string[] { "嘉兴", "080700", "070090" });
            dic_hi.Add("泰安", new string[] { "泰安", "121100", "250090" });
            dic_hi.Add("昆山", new string[] { "昆山", "070600", "060050" });
            dic_hi.Add("烟台", new string[] { "烟台", "120400", "250120" });
            dic_hi.Add("兰州", new string[] { "兰州", "270200", "100020" });
            dic_hi.Add("泉州", new string[] { "泉州", "110400", "090030" });
        }

    }
}
