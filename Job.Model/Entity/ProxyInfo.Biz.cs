/*
 * XCoder v6.4.5630.33408
 * 作者：zhangchanglin/A-ZHANGCHANGLIN
 * 时间：2016-08-29 17:25:33
 * 版权：版权所有 (C) 新生命开发团队 2002~2016
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using NewLife.Log;
using NewLife.Web;
using NewLife.Data;
using XCode;
using XCode.Configuration;
using XCode.Membership;
using System.Net.Http;
using Ivony.Html.Parser;
using Ivony.Html;
using System.Linq;

namespace Job.Model.Entity
{
    /// <summary>代理IP信息</summary>
    public partial class ProxyInfo : Entity<ProxyInfo>
    {
        #region 对象操作


        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
            //if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException(_.Name, _.Name.DisplayName + "无效！");
            //if (!isNew && ID < 1) throw new ArgumentOutOfRangeException(_.ID, _.ID.DisplayName + "必须大于0！");

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            // 在新插入数据或者修改了指定字段时进行唯一性验证，CheckExist内部抛出参数异常
            //if (isNew || Dirtys[__.Name]) CheckExist(__.Name);

            if (!Dirtys[__.IP]) IP = WebHelper.UserHost;
            if (isNew && !Dirtys[__.CreateDate]) CreateDate = DateTime.Now;
            if (!Dirtys[__.UpdateDate]) UpdateDate = DateTime.Now;
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    base.InitData();

        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    // Meta.Count是快速取得表记录数
        //    if (Meta.Count > 0) return;

        //    // 需要注意的是，如果该方法调用了其它实体类的首次数据库操作，目标实体类的数据初始化将会在同一个线程完成
        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}[{1}]数据……", typeof(ProxyInfo).Name, Meta.Table.DataTable.DisplayName);

        //    var entity = new ProxyInfo();
        //    entity.IP = "abc";
        //    entity.Port = 0;
        //    entity.Country = "abc";
        //    entity.Address = "abc";
        //    entity.AnonymityType = "abc";
        //    entity.Protocol = "abc";
        //    entity.Speed = 0;
        //    entity.ConnectTime = 0;
        //    entity.TTL = "abc";
        //    entity.VerifyTime = "abc";
        //    entity.Source = "abc";
        //    entity.CreateDate = DateTime.Now;
        //    entity.UpdateDate = DateTime.Now;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(ProxyInfo).Name, Meta.Table.DataTable.DisplayName);
        //}

        /// <summary>已重载。删除关联数据</summary>
        /// <returns></returns>
        protected override int OnDelete()
        {
            if (ProxyInfoLogs != null) ProxyInfoLogs.Delete();

            return base.OnDelete();
        }

        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnInsert()
        //{
        //    return base.OnInsert();
        //}

        #endregion

        #region 扩展属性

        [NonSerialized]
        private EntityList<ProxyInfoLog> _ProxyInfoLogs;
        /// <summary>该代理IP信息所拥有的使用代理IP信息记录集合</summary>
        [XmlIgnore]
        public EntityList<ProxyInfoLog> ProxyInfoLogs
        {
            get
            {
                if (_ProxyInfoLogs == null && ID > 0 && !Dirtys.ContainsKey("ProxyInfoLogs"))
                {
                    _ProxyInfoLogs = ProxyInfoLog.FindAllByProxyInfoID(ID);
                    Dirtys["ProxyInfoLogs"] = true;
                }
                return _ProxyInfoLogs;
            }
            set { _ProxyInfoLogs = value; }
        }

        #endregion

        #region 扩展查询

        /// <summary>根据ID查找</summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ProxyInfo FindByID(Int32 id)
        {
            if (Meta.Count >= 1000)
                return Find(_.ID, id);
            else // 实体缓存
                return Meta.Cache.Entities.Find(__.ID, id);
            // 单对象缓存
            //return Meta.SingleCache[id];
        }

        #endregion

        #region 高级查询
        // 以下为自定义高级查询的例子

        /// <summary>查询满足条件的记录集，分页、排序</summary>
        /// <param name="userid">用户编号</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="key">关键字</param>
        /// <param name="param">分页排序参数，同时返回满足条件的总记录数</param>
        /// <returns>实体集</returns>
        public static EntityList<ProxyInfo> Search(Int32 userid, DateTime start, DateTime end, String key, PageParameter param)
        {
            // WhereExpression重载&和|运算符，作为And和Or的替代
            // SearchWhereByKeys系列方法用于构建针对字符串字段的模糊搜索，第二个参数可指定要搜索的字段
            var exp = SearchWhereByKeys(key, null, null);

            // 以下仅为演示，Field（继承自FieldItem）重载了==、!=、>、<、>=、<=等运算符
            //if (userid > 0) exp &= _.OperatorID == userid;
            //if (isSign != null) exp &= _.IsSign == isSign.Value;
            //exp &= _.OccurTime.Between(start, end); // 大于等于start，小于end，当start/end大于MinValue时有效

            return FindAll(exp, param);
        }
        #endregion

        #region 扩展操作
        #endregion

        #region 业务

        /// <summary>从西刺免费代理IP（http://www.xicidaili.com/nn/）获取</summary>
        /// <returns></returns>
        public static EntityList<ProxyInfo> GetAllByXiCi(Int32 pageIndex=1)
        {
            String url = "http://www.xicidaili.com/nn/" + pageIndex;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Host", "www.xicidaili.com");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            httpClient.DefaultRequestHeaders.Add("Referer", "http://www.xicidaili.com/");
            var result = httpClient.GetAsync(url).Result;

            JumonyParser jumonyParser = new JumonyParser();
            //因为这个代理站点请求需要带User-Agent头，所以不能直接使用jumonyParser.LoadDocument(url)
            IHtmlDocument doc = jumonyParser.Parse(result.Content.ReadAsStringAsync().Result);//jumonyParser.LoadDocument(url);
            //查找ip列表，并去除第一列的标题项
            var trList = doc.Find("#ip_list tr").Skip(1);
            if (trList == null || trList.Count() <= 0)
            {
                return null;
            }
            EntityList<ProxyInfo> list = new EntityList<ProxyInfo>();
            foreach (var item in trList)
            {
                ProxyInfo info = new ProxyInfo();
                info.Country = "中国";
                var tdList = item.Find("td").ToList();
                info.IP = tdList[1].InnerText();
                info.Port = tdList[2].InnerText().ToInt();
                info.Address = tdList[3].InnerText();
                info.AnonymityType = tdList[4].InnerText();
                info.Protocol = tdList[5].InnerText();
                info.Speed = tdList[6].FindSingle(".bar").Attribute("title").Value().Replace("秒", "").ToDouble();
                info.ConnectTime = tdList[7].FindSingle(".bar").Attribute("title").Value().Replace("秒", "").ToDouble();
                info.TTL = tdList[8].InnerText();
                info.VerifyTime = tdList[9].InnerText();
                list.Add(info);
            }
            return list;
        }


        /// <summary>批量入库</summary>
        /// <param name="key"></param>
        /// <param name="city"></param>
        public static void SaveAll()
        {
            Int32 pageIndex = 1;
            while (true)
            {
                //设置最大页码，100页就行了
                Int32 maxPageIndex = 100;
                if (pageIndex > maxPageIndex)
                {
                    break;
                }
                var list = GetAllByXiCi(pageIndex);
                pageIndex++;
                if (list == null || list.Count == 0)
                {
                    break;
                }
                //批量插入数据库
                list.Insert();
            }
        }
        #endregion
    }
}