/*
 * XCoder v6.4.5630.33408
 * 作者：zhangchanglin/A-ZHANGCHANGLIN
 * 时间：2016-08-19 16:33:45
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
using System.Text.RegularExpressions;

namespace Job.Model.Entity
{
    /// <summary>职位</summary>
    public partial class PositionInfo : Entity<PositionInfo>
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

            if (isNew && !Dirtys[__.CreateDate]) CreateDate = DateTime.Now;
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
        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}[{1}]数据……", typeof(PositionInfo).Name, Meta.Table.DataTable.DisplayName);

        //    var entity = new PositionInfo();
        //    entity.Key = "abc";
        //    entity.TitleName = "abc";
        //    entity.Company = "abc";
        //    entity.City = "abc";
        //    entity.Address = "abc";
        //    entity.Date = "abc";
        //    entity.Salary = "abc";
        //    entity.SalaryEm = "abc";
        //    entity.InfoUrl = "abc";
        //    entity.Source = "abc";
        //    entity.CreateDate = DateTime.Now;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(PositionInfo).Name, Meta.Table.DataTable.DisplayName);
        //}


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


        #endregion

        #region 扩展查询

        /// <summary>根据主键查找</summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static PositionInfo FindByID(Int64 id)
        {
            if (Meta.Count >= 1000)
                return Find(_.ID, id);
            else // 实体缓存
                return Meta.Cache.Entities.Find(__.ID, id);
            // 单对象缓存
            //return Meta.SingleCache[id];
        }
        /// <summary>根据职位、省市与来源获取所有</summary>
        /// <param name="position">职位</param>
        /// <param name="city"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static EntityList<PositionInfo> FindAll(String position, String city, String source)
        {
            var exp = new WhereExpression();
            if (!String.IsNullOrWhiteSpace(position))
            {
                exp &= _.Key == position;
            }
            if (!String.IsNullOrEmpty(city))
            {
                exp &= _.City == city;
            }
            if (!String.IsNullOrEmpty(source))
            {
                exp &= _.Source == source;
            }

            return FindAll(exp, __.ID, null, 0, 0);
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
        public static EntityList<PositionInfo> Search(Int32 userid, DateTime start, DateTime end, String key, PageParameter param)
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

        /// <summary>
        /// 将JobInfo转成Position
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public PositionInfo FromJobInfo(JobInfo jobInfo)
        {
            Key = jobInfo.key;
            TitleName = jobInfo.titleName;
            Company = jobInfo.company;
            Address = jobInfo.address;
            City = jobInfo.city;
            Area = jobInfo.area;
            Date = jobInfo.date;
            Salary = jobInfo.salary;
            SalaryEm = jobInfo.salary_em;
            MinSalary = jobInfo.min_salary;
            MaxSalary = jobInfo.max_salary;
            InfoUrl = jobInfo.info_url;
            Source = jobInfo.source;

            return this;
        }

        /// <summary>根据职位、省市与来源将职位信息转化为公司信息</summary>
        /// <param name="position">职位</param>
        /// <param name="city"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Dictionary<String, Company> ConvertToCompany(String position, String city, String source)
        {
            EntityList<PositionInfo> positionInfoList = PositionInfo.FindAll(position, city, source);

            if (positionInfoList != null && positionInfoList.Count > 0)
            {
                Dictionary<String, Company> dic = new Dictionary<String, Company>();
                //招聘网站请求
                foreach (var item in positionInfoList)
                {
                    String key = item.Company + "-" + item.Address;

                    if (String.IsNullOrWhiteSpace(key))
                    {
                        continue;
                    }
                    if (dic.ContainsKey(key))
                    {
                        var company = dic[key];
                        company.PositionCount++;
                        if (item.MinSalary != 0)
                        {
                            if (item.MinSalary < company.MinSalary)
                            {
                                company.MinSalary = item.MinSalary;
                                company.MinSalaryRange = item.SalaryEm;
                                company.MinPositionName = item.TitleName;
                            }
                            else if (item.MinSalary > company.MaxSalary)
                            {
                                company.MaxSalary = item.MaxSalary;
                                company.MaxSalaryRange = item.SalaryEm;
                                company.MaxPositionName = item.TitleName;
                            }
                        }
                    }
                    else
                    {
                        var company = new Company();
                        company.Name = item.Company;
                        company.City = item.City;
                        company.Key = item.Key;
                        company.Area = item.Area;
                        company.Source = item.Source;
                        company.MinPositionName = company.MaxPositionName = item.TitleName;
                        company.MinSalaryRange = company.MaxSalaryRange = item.SalaryEm;
                        company.MinSalary = item.MinSalary;
                        company.MaxSalary = item.MaxSalary;
                        company.PositionCount = 1;
                        company.RecruitUrl = item.InfoUrl;
                        company.Address = item.Address;
                        dic.Add(key, company);
                    }
                }
                return dic;
            }
            return null;
        }

        /// <summary>获取报表数据</summary>
        /// <param name="position">职位</param>
        /// <param name="source">来源</param>
        /// <returns></returns>
        public static List<PositionReport> GetReports(String position = ".net", String source = "拉勾网")
        {
            PositionReport bj = new PositionReport();
            bj.City = "北京";

            PositionReport sh = new PositionReport();
            sh.City = "上海";

            PositionReport hz = new PositionReport();
            hz.City = "杭州";

            PositionReport sz = new PositionReport();
            sz.City = "苏州";

            PositionReport zz = new PositionReport();
            zz.City = "郑州";
            List<PositionReport> result = new List<PositionReport>();
            result.Add(bj);
            result.Add(sh);
            result.Add(hz);
            result.Add(sz);
            var list = FindAll(position, null, source);
            if (list != null && list.Count > 0)
            {

                foreach (var item in list)
                {
                    switch (item.City)
                    {
                        case "北京":
                            SetPositionReport(bj, item);
                            break;
                        case "上海":
                            SetPositionReport(sh, item);
                            break;
                        case "杭州":
                            SetPositionReport(hz, item);
                            break;
                        case "苏州":
                            SetPositionReport(sz, item);
                            break;
                        case "郑州":
                            SetPositionReport(zz, item);
                            break;
                    }
                }
            }
            return result;
        }
        /// <summary>设置职位报表</summary>
        /// <param name="report"></param>
        /// <param name="info"></param>
        public static void SetPositionReport(PositionReport report, PositionInfo info)
        {
            //只有上区间
            if (info.MaxSalary == 0)
            {
                Double salary = info.MinSalary;
                //这是面议的
                if (salary == 0)
                {
                    report.DiscussCount++;
                }
                else if (salary < 5000)
                {
                    report.K0Count++;
                }
                else if (salary >= 25000)
                {
                    report.K25Count++;
                }
            }
            else
            {
                Double salary = info.MaxSalary;
                if (salary < 5000)
                {
                    report.K0Count++;
                }
                else if (salary >= 5000 && salary < 10000)
                {
                    report.K5Count++;
                }
                else if (salary >= 10000 && salary < 15000)
                {
                    report.K10Count++;
                }
                else if (salary >= 15000 && salary < 20000)
                {
                    report.K15Count++;
                }
                else if (salary >= 20000 && salary < 25000)
                {
                    report.K20Count++;
                }
                else if (salary >= 25000)
                {
                    report.K25Count++;
                }
            }
        }

        #endregion
    }
}