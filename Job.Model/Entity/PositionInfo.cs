/*
 * XCoder v6.4.5630.33408
 * 作者：zhangchanglin/A-ZHANGCHANGLIN
 * 时间：2016-09-01 14:57:48
 * 版权：版权所有 (C) 新生命开发团队 2002~2016
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Job.Model.Entity
{
    /// <summary>职位</summary>
    [Serializable]
    [DataObject]
    [Description("职位")]
    [BindIndex("PRIMARY", true, "ID")]
    [BindTable("PositionInfo", Description = "职位", ConnName = "Common", DbType = DatabaseType.MySql)]
    public partial class PositionInfo : IPositionInfo
    {
        #region 属性
        private Int32 _ID;
        /// <summary>主键</summary>
        [DisplayName("主键")]
        [Description("主键")]
        [DataObjectField(true, true, false, 19)]
        [BindColumn(1, "ID", "主键", null, "int(11)", 19, 0, false)]
        public virtual Int32 ID
        {
            get { return _ID; }
            set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } }
        }

        private String _Key;
        /// <summary>关键词</summary>
        [DisplayName("关键词")]
        [Description("关键词")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(2, "Key", "关键词", null, "varchar(200)", 0, 0, false)]
        public virtual String Key
        {
            get { return _Key; }
            set { if (OnPropertyChanging(__.Key, value)) { _Key = value; OnPropertyChanged(__.Key); } }
        }

        private String _TitleName;
        /// <summary>招聘主信息</summary>
        [DisplayName("招聘主信息")]
        [Description("招聘主信息")]
        [DataObjectField(false, false, true, 255)]
        [BindColumn(3, "TitleName", "招聘主信息", null, "varchar(255)", 0, 0, false)]
        public virtual String TitleName
        {
            get { return _TitleName; }
            set { if (OnPropertyChanging(__.TitleName, value)) { _TitleName = value; OnPropertyChanged(__.TitleName); } }
        }

        private String _Company;
        /// <summary>公司名称</summary>
        [DisplayName("公司名称")]
        [Description("公司名称")]
        [DataObjectField(false, false, true, 255)]
        [BindColumn(4, "Company", "公司名称", null, "varchar(255)", 0, 0, false)]
        public virtual String Company
        {
            get { return _Company; }
            set { if (OnPropertyChanging(__.Company, value)) { _Company = value; OnPropertyChanged(__.Company); } }
        }

        private String _City;
        /// <summary>所属市</summary>
        [DisplayName("所属市")]
        [Description("所属市")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(5, "City", "所属市", null, "varchar(200)", 0, 0, false)]
        public virtual String City
        {
            get { return _City; }
            set { if (OnPropertyChanging(__.City, value)) { _City = value; OnPropertyChanged(__.City); } }
        }

        private String _Area;
        /// <summary>所属区</summary>
        [DisplayName("所属区")]
        [Description("所属区")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(6, "Area", "所属区", null, "varchar(200)", 0, 0, false)]
        public virtual String Area
        {
            get { return _Area; }
            set { if (OnPropertyChanging(__.Area, value)) { _Area = value; OnPropertyChanged(__.Area); } }
        }

        private String _Address;
        /// <summary>地址</summary>
        [DisplayName("地址")]
        [Description("地址")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(7, "Address", "地址", null, "varchar(200)", 0, 0, false)]
        public virtual String Address
        {
            get { return _Address; }
            set { if (OnPropertyChanging(__.Address, value)) { _Address = value; OnPropertyChanged(__.Address); } }
        }

        private String _Date;
        /// <summary>发布时间</summary>
        [DisplayName("发布时间")]
        [Description("发布时间")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(8, "Date", "发布时间", null, "varchar(50)", 0, 0, false)]
        public virtual String Date
        {
            get { return _Date; }
            set { if (OnPropertyChanging(__.Date, value)) { _Date = value; OnPropertyChanged(__.Date); } }
        }

        private String _Salary;
        /// <summary>薪资方式</summary>
        [DisplayName("薪资方式")]
        [Description("薪资方式")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(9, "Salary", "薪资方式", null, "varchar(50)", 0, 0, false)]
        public virtual String Salary
        {
            get { return _Salary; }
            set { if (OnPropertyChanging(__.Salary, value)) { _Salary = value; OnPropertyChanged(__.Salary); } }
        }

        private String _SalaryEm;
        /// <summary>薪资</summary>
        [DisplayName("薪资")]
        [Description("薪资")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(10, "SalaryEm", "薪资", null, "varchar(50)", 0, 0, false)]
        public virtual String SalaryEm
        {
            get { return _SalaryEm; }
            set { if (OnPropertyChanging(__.SalaryEm, value)) { _SalaryEm = value; OnPropertyChanged(__.SalaryEm); } }
        }

        private Double _MinSalary;
        /// <summary>最低薪水</summary>
        [DisplayName("最低薪水")]
        [Description("最低薪水")]
        [DataObjectField(false, false, true, 22)]
        [BindColumn(11, "MinSalary", "最低薪水", null, "double", 22, 0, false)]
        public virtual Double MinSalary
        {
            get { return _MinSalary; }
            set { if (OnPropertyChanging(__.MinSalary, value)) { _MinSalary = value; OnPropertyChanged(__.MinSalary); } }
        }

        private Double _MaxSalary;
        /// <summary>最高薪水</summary>
        [DisplayName("最高薪水")]
        [Description("最高薪水")]
        [DataObjectField(false, false, true, 22)]
        [BindColumn(12, "MaxSalary", "最高薪水", null, "double", 22, 0, false)]
        public virtual Double MaxSalary
        {
            get { return _MaxSalary; }
            set { if (OnPropertyChanging(__.MaxSalary, value)) { _MaxSalary = value; OnPropertyChanged(__.MaxSalary); } }
        }

        private String _InfoUrl;
        /// <summary>详细信息url</summary>
        [DisplayName("详细信息url")]
        [Description("详细信息url")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(13, "InfoUrl", "详细信息url", null, "varchar(200)", 0, 0, false)]
        public virtual String InfoUrl
        {
            get { return _InfoUrl; }
            set { if (OnPropertyChanging(__.InfoUrl, value)) { _InfoUrl = value; OnPropertyChanged(__.InfoUrl); } }
        }

        private String _Source;
        /// <summary>来源</summary>
        [DisplayName("来源")]
        [Description("来源")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(14, "Source", "来源", null, "varchar(50)", 0, 0, false)]
        public virtual String Source
        {
            get { return _Source; }
            set { if (OnPropertyChanging(__.Source, value)) { _Source = value; OnPropertyChanged(__.Source); } }
        }

        private DateTime _CreateDate;
        /// <summary>入库日期</summary>
        [DisplayName("入库日期")]
        [Description("入库日期")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn(15, "CreateDate", "入库日期", null, "datetime", 0, 0, false)]
        public virtual DateTime CreateDate
        {
            get { return _CreateDate; }
            set { if (OnPropertyChanging(__.CreateDate, value)) { _CreateDate = value; OnPropertyChanged(__.CreateDate); } }
        }
        #endregion

        #region 获取/设置 字段值
        /// <summary>
        /// 获取/设置 字段值。
        /// 一个索引，基类使用反射实现。
        /// 派生实体类可重写该索引，以避免反射带来的性能损耗
        /// </summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case __.ID : return _ID;
                    case __.Key : return _Key;
                    case __.TitleName : return _TitleName;
                    case __.Company : return _Company;
                    case __.City : return _City;
                    case __.Area : return _Area;
                    case __.Address : return _Address;
                    case __.Date : return _Date;
                    case __.Salary : return _Salary;
                    case __.SalaryEm : return _SalaryEm;
                    case __.MinSalary : return _MinSalary;
                    case __.MaxSalary : return _MaxSalary;
                    case __.InfoUrl : return _InfoUrl;
                    case __.Source : return _Source;
                    case __.CreateDate : return _CreateDate;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.Key : _Key = Convert.ToString(value); break;
                    case __.TitleName : _TitleName = Convert.ToString(value); break;
                    case __.Company : _Company = Convert.ToString(value); break;
                    case __.City : _City = Convert.ToString(value); break;
                    case __.Area : _Area = Convert.ToString(value); break;
                    case __.Address : _Address = Convert.ToString(value); break;
                    case __.Date : _Date = Convert.ToString(value); break;
                    case __.Salary : _Salary = Convert.ToString(value); break;
                    case __.SalaryEm : _SalaryEm = Convert.ToString(value); break;
                    case __.MinSalary : _MinSalary = Convert.ToDouble(value); break;
                    case __.MaxSalary : _MaxSalary = Convert.ToDouble(value); break;
                    case __.InfoUrl : _InfoUrl = Convert.ToString(value); break;
                    case __.Source : _Source = Convert.ToString(value); break;
                    case __.CreateDate : _CreateDate = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得职位字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>主键</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>关键词</summary>
            public static readonly Field Key = FindByName(__.Key);

            ///<summary>招聘主信息</summary>
            public static readonly Field TitleName = FindByName(__.TitleName);

            ///<summary>公司名称</summary>
            public static readonly Field Company = FindByName(__.Company);

            ///<summary>所属市</summary>
            public static readonly Field City = FindByName(__.City);

            ///<summary>所属区</summary>
            public static readonly Field Area = FindByName(__.Area);

            ///<summary>地址</summary>
            public static readonly Field Address = FindByName(__.Address);

            ///<summary>发布时间</summary>
            public static readonly Field Date = FindByName(__.Date);

            ///<summary>薪资方式</summary>
            public static readonly Field Salary = FindByName(__.Salary);

            ///<summary>薪资</summary>
            public static readonly Field SalaryEm = FindByName(__.SalaryEm);

            ///<summary>最低薪水</summary>
            public static readonly Field MinSalary = FindByName(__.MinSalary);

            ///<summary>最高薪水</summary>
            public static readonly Field MaxSalary = FindByName(__.MaxSalary);

            ///<summary>详细信息url</summary>
            public static readonly Field InfoUrl = FindByName(__.InfoUrl);

            ///<summary>来源</summary>
            public static readonly Field Source = FindByName(__.Source);

            ///<summary>入库日期</summary>
            public static readonly Field CreateDate = FindByName(__.CreateDate);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得职位字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>主键</summary>
            public const String ID = "ID";

            ///<summary>关键词</summary>
            public const String Key = "Key";

            ///<summary>招聘主信息</summary>
            public const String TitleName = "TitleName";

            ///<summary>公司名称</summary>
            public const String Company = "Company";

            ///<summary>所属市</summary>
            public const String City = "City";

            ///<summary>所属区</summary>
            public const String Area = "Area";

            ///<summary>地址</summary>
            public const String Address = "Address";

            ///<summary>发布时间</summary>
            public const String Date = "Date";

            ///<summary>薪资方式</summary>
            public const String Salary = "Salary";

            ///<summary>薪资</summary>
            public const String SalaryEm = "SalaryEm";

            ///<summary>最低薪水</summary>
            public const String MinSalary = "MinSalary";

            ///<summary>最高薪水</summary>
            public const String MaxSalary = "MaxSalary";

            ///<summary>详细信息url</summary>
            public const String InfoUrl = "InfoUrl";

            ///<summary>来源</summary>
            public const String Source = "Source";

            ///<summary>入库日期</summary>
            public const String CreateDate = "CreateDate";

        }
        #endregion
    }

    /// <summary>职位接口</summary>
    public partial interface IPositionInfo
    {
        #region 属性
        /// <summary>主键</summary>
        Int32 ID { get; set; }

        /// <summary>关键词</summary>
        String Key { get; set; }

        /// <summary>招聘主信息</summary>
        String TitleName { get; set; }

        /// <summary>公司名称</summary>
        String Company { get; set; }

        /// <summary>所属市</summary>
        String City { get; set; }

        /// <summary>所属区</summary>
        String Area { get; set; }

        /// <summary>地址</summary>
        String Address { get; set; }

        /// <summary>发布时间</summary>
        String Date { get; set; }

        /// <summary>薪资方式</summary>
        String Salary { get; set; }

        /// <summary>薪资</summary>
        String SalaryEm { get; set; }

        /// <summary>最低薪水</summary>
        Double MinSalary { get; set; }

        /// <summary>最高薪水</summary>
        Double MaxSalary { get; set; }

        /// <summary>详细信息url</summary>
        String InfoUrl { get; set; }

        /// <summary>来源</summary>
        String Source { get; set; }

        /// <summary>入库日期</summary>
        DateTime CreateDate { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}