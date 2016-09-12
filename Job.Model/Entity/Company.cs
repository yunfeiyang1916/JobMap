/*
 * XCoder v6.4.5630.33408
 * 作者：zhangchanglin/A-ZHANGCHANGLIN
 * 时间：2016-09-01 16:17:26
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
    /// <summary>公司</summary>
    [Serializable]
    [DataObject]
    [Description("公司")]
    [BindIndex("PRIMARY", true, "ID")]
    [BindTable("Company", Description = "公司", ConnName = "Common", DbType = DatabaseType.MySql)]
    public partial class Company : ICompany
    {
        #region 属性
        private Int32 _ID;
        /// <summary>ID</summary>
        [DisplayName("ID")]
        [Description("ID")]
        [DataObjectField(true, true, false, 10)]
        [BindColumn(1, "ID", "ID", null, "int(11)", 10, 0, false)]
        public virtual Int32 ID
        {
            get { return _ID; }
            set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } }
        }

        private String _Key;
        /// <summary>关键字</summary>
        [DisplayName("关键字")]
        [Description("关键字")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(2, "Key", "关键字", null, "varchar(200)", 0, 0, false)]
        public virtual String Key
        {
            get { return _Key; }
            set { if (OnPropertyChanging(__.Key, value)) { _Key = value; OnPropertyChanged(__.Key); } }
        }

        private String _Name;
        /// <summary>公司名称</summary>
        [DisplayName("公司名称")]
        [Description("公司名称")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(3, "Name", "公司名称", null, "varchar(200)", 0, 0, false, Master=true)]
        public virtual String Name
        {
            get { return _Name; }
            set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } }
        }

        private String _City;
        /// <summary>所属城市</summary>
        [DisplayName("所属城市")]
        [Description("所属城市")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(4, "City", "所属城市", null, "varchar(50)", 0, 0, false)]
        public virtual String City
        {
            get { return _City; }
            set { if (OnPropertyChanging(__.City, value)) { _City = value; OnPropertyChanged(__.City); } }
        }

        private String _Area;
        /// <summary>所属区</summary>
        [DisplayName("所属区")]
        [Description("所属区")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(5, "Area", "所属区", null, "varchar(50)", 0, 0, false)]
        public virtual String Area
        {
            get { return _Area; }
            set { if (OnPropertyChanging(__.Area, value)) { _Area = value; OnPropertyChanged(__.Area); } }
        }

        private String _Size;
        /// <summary>公司规模</summary>
        [DisplayName("公司规模")]
        [Description("公司规模")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn(6, "Size", "公司规模", null, "varchar(100)", 0, 0, false)]
        public virtual String Size
        {
            get { return _Size; }
            set { if (OnPropertyChanging(__.Size, value)) { _Size = value; OnPropertyChanged(__.Size); } }
        }

        private String _Nature;
        /// <summary>公司性质</summary>
        [DisplayName("公司性质")]
        [Description("公司性质")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn(7, "Nature", "公司性质", null, "varchar(100)", 0, 0, false)]
        public virtual String Nature
        {
            get { return _Nature; }
            set { if (OnPropertyChanging(__.Nature, value)) { _Nature = value; OnPropertyChanged(__.Nature); } }
        }

        private String _Industry;
        /// <summary>公司行业</summary>
        [DisplayName("公司行业")]
        [Description("公司行业")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn(8, "Industry", "公司行业", null, "varchar(100)", 0, 0, false)]
        public virtual String Industry
        {
            get { return _Industry; }
            set { if (OnPropertyChanging(__.Industry, value)) { _Industry = value; OnPropertyChanged(__.Industry); } }
        }

        private String _Label;
        /// <summary>公司标签</summary>
        [DisplayName("公司标签")]
        [Description("公司标签")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn(9, "Label", "公司标签", null, "varchar(100)", 0, 0, false)]
        public virtual String Label
        {
            get { return _Label; }
            set { if (OnPropertyChanging(__.Label, value)) { _Label = value; OnPropertyChanged(__.Label); } }
        }

        private String _RecruitUrl;
        /// <summary>在招聘网站的地址</summary>
        [DisplayName("在招聘网站的地址")]
        [Description("在招聘网站的地址")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(10, "RecruitUrl", "在招聘网站的地址", null, "varchar(200)", 0, 0, false)]
        public virtual String RecruitUrl
        {
            get { return _RecruitUrl; }
            set { if (OnPropertyChanging(__.RecruitUrl, value)) { _RecruitUrl = value; OnPropertyChanged(__.RecruitUrl); } }
        }

        private String _Url;
        /// <summary>公司网站地址</summary>
        [DisplayName("公司网站地址")]
        [Description("公司网站地址")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(11, "Url", "公司网站地址", null, "varchar(200)", 0, 0, false)]
        public virtual String Url
        {
            get { return _Url; }
            set { if (OnPropertyChanging(__.Url, value)) { _Url = value; OnPropertyChanged(__.Url); } }
        }

        private String _Address;
        /// <summary>地址</summary>
        [DisplayName("地址")]
        [Description("地址")]
        [DataObjectField(false, false, true, 500)]
        [BindColumn(12, "Address", "地址", null, "varchar(500)", 0, 0, false)]
        public virtual String Address
        {
            get { return _Address; }
            set { if (OnPropertyChanging(__.Address, value)) { _Address = value; OnPropertyChanged(__.Address); } }
        }

        private Double _Lng;
        /// <summary>经度</summary>
        [DisplayName("经度")]
        [Description("经度")]
        [DataObjectField(false, false, true, 22)]
        [BindColumn(13, "Lng", "经度", null, "double", 22, 0, false)]
        public virtual Double Lng
        {
            get { return _Lng; }
            set { if (OnPropertyChanging(__.Lng, value)) { _Lng = value; OnPropertyChanged(__.Lng); } }
        }

        private Double _Lat;
        /// <summary>维度</summary>
        [DisplayName("维度")]
        [Description("维度")]
        [DataObjectField(false, false, true, 22)]
        [BindColumn(14, "Lat", "维度", null, "double", 22, 0, false)]
        public virtual Double Lat
        {
            get { return _Lat; }
            set { if (OnPropertyChanging(__.Lat, value)) { _Lat = value; OnPropertyChanged(__.Lat); } }
        }

        private Double _MinSalary;
        /// <summary>最低薪水</summary>
        [DisplayName("最低薪水")]
        [Description("最低薪水")]
        [DataObjectField(false, false, true, 22)]
        [BindColumn(15, "MinSalary", "最低薪水", null, "double", 22, 0, false)]
        public virtual Double MinSalary
        {
            get { return _MinSalary; }
            set { if (OnPropertyChanging(__.MinSalary, value)) { _MinSalary = value; OnPropertyChanged(__.MinSalary); } }
        }

        private String _MinSalaryRange;
        /// <summary>最低薪水范围</summary>
        [DisplayName("最低薪水范围")]
        [Description("最低薪水范围")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(16, "MinSalaryRange", "最低薪水范围", null, "varchar(200)", 0, 0, false)]
        public virtual String MinSalaryRange
        {
            get { return _MinSalaryRange; }
            set { if (OnPropertyChanging(__.MinSalaryRange, value)) { _MinSalaryRange = value; OnPropertyChanged(__.MinSalaryRange); } }
        }

        private String _MinPositionName;
        /// <summary>最低职位名称</summary>
        [DisplayName("最低职位名称")]
        [Description("最低职位名称")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(17, "MinPositionName", "最低职位名称", null, "varchar(200)", 0, 0, false)]
        public virtual String MinPositionName
        {
            get { return _MinPositionName; }
            set { if (OnPropertyChanging(__.MinPositionName, value)) { _MinPositionName = value; OnPropertyChanged(__.MinPositionName); } }
        }

        private Double _MaxSalary;
        /// <summary>最高薪水</summary>
        [DisplayName("最高薪水")]
        [Description("最高薪水")]
        [DataObjectField(false, false, true, 22)]
        [BindColumn(18, "MaxSalary", "最高薪水", null, "double", 22, 0, false)]
        public virtual Double MaxSalary
        {
            get { return _MaxSalary; }
            set { if (OnPropertyChanging(__.MaxSalary, value)) { _MaxSalary = value; OnPropertyChanged(__.MaxSalary); } }
        }

        private String _MaxSalaryRange;
        /// <summary>最高薪水范围</summary>
        [DisplayName("最高薪水范围")]
        [Description("最高薪水范围")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(19, "MaxSalaryRange", "最高薪水范围", null, "varchar(200)", 0, 0, false)]
        public virtual String MaxSalaryRange
        {
            get { return _MaxSalaryRange; }
            set { if (OnPropertyChanging(__.MaxSalaryRange, value)) { _MaxSalaryRange = value; OnPropertyChanged(__.MaxSalaryRange); } }
        }

        private String _MaxPositionName;
        /// <summary>最高职位名称</summary>
        [DisplayName("最高职位名称")]
        [Description("最高职位名称")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(20, "MaxPositionName", "最高职位名称", null, "varchar(200)", 0, 0, false)]
        public virtual String MaxPositionName
        {
            get { return _MaxPositionName; }
            set { if (OnPropertyChanging(__.MaxPositionName, value)) { _MaxPositionName = value; OnPropertyChanged(__.MaxPositionName); } }
        }

        private Int32 _PositionCount;
        /// <summary>职位数量</summary>
        [DisplayName("职位数量")]
        [Description("职位数量")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(21, "PositionCount", "职位数量", null, "int(11)", 10, 0, false)]
        public virtual Int32 PositionCount
        {
            get { return _PositionCount; }
            set { if (OnPropertyChanging(__.PositionCount, value)) { _PositionCount = value; OnPropertyChanged(__.PositionCount); } }
        }

        private String _Source;
        /// <summary>来源</summary>
        [DisplayName("来源")]
        [Description("来源")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(22, "Source", "来源", null, "varchar(200)", 0, 0, false)]
        public virtual String Source
        {
            get { return _Source; }
            set { if (OnPropertyChanging(__.Source, value)) { _Source = value; OnPropertyChanged(__.Source); } }
        }

        private DateTime _CreateDate;
        /// <summary>创建日期</summary>
        [DisplayName("创建日期")]
        [Description("创建日期")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn(23, "CreateDate", "创建日期", null, "datetime", 0, 0, false)]
        public virtual DateTime CreateDate
        {
            get { return _CreateDate; }
            set { if (OnPropertyChanging(__.CreateDate, value)) { _CreateDate = value; OnPropertyChanged(__.CreateDate); } }
        }

        private DateTime _UpdateDate;
        /// <summary>更新日期</summary>
        [DisplayName("更新日期")]
        [Description("更新日期")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn(24, "UpdateDate", "更新日期", null, "datetime", 0, 0, false)]
        public virtual DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { if (OnPropertyChanging(__.UpdateDate, value)) { _UpdateDate = value; OnPropertyChanged(__.UpdateDate); } }
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
                    case __.Name : return _Name;
                    case __.City : return _City;
                    case __.Area : return _Area;
                    case __.Size : return _Size;
                    case __.Nature : return _Nature;
                    case __.Industry : return _Industry;
                    case __.Label : return _Label;
                    case __.RecruitUrl : return _RecruitUrl;
                    case __.Url : return _Url;
                    case __.Address : return _Address;
                    case __.Lng : return _Lng;
                    case __.Lat : return _Lat;
                    case __.MinSalary : return _MinSalary;
                    case __.MinSalaryRange : return _MinSalaryRange;
                    case __.MinPositionName : return _MinPositionName;
                    case __.MaxSalary : return _MaxSalary;
                    case __.MaxSalaryRange : return _MaxSalaryRange;
                    case __.MaxPositionName : return _MaxPositionName;
                    case __.PositionCount : return _PositionCount;
                    case __.Source : return _Source;
                    case __.CreateDate : return _CreateDate;
                    case __.UpdateDate : return _UpdateDate;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.Key : _Key = Convert.ToString(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.City : _City = Convert.ToString(value); break;
                    case __.Area : _Area = Convert.ToString(value); break;
                    case __.Size : _Size = Convert.ToString(value); break;
                    case __.Nature : _Nature = Convert.ToString(value); break;
                    case __.Industry : _Industry = Convert.ToString(value); break;
                    case __.Label : _Label = Convert.ToString(value); break;
                    case __.RecruitUrl : _RecruitUrl = Convert.ToString(value); break;
                    case __.Url : _Url = Convert.ToString(value); break;
                    case __.Address : _Address = Convert.ToString(value); break;
                    case __.Lng : _Lng = Convert.ToDouble(value); break;
                    case __.Lat : _Lat = Convert.ToDouble(value); break;
                    case __.MinSalary : _MinSalary = Convert.ToDouble(value); break;
                    case __.MinSalaryRange : _MinSalaryRange = Convert.ToString(value); break;
                    case __.MinPositionName : _MinPositionName = Convert.ToString(value); break;
                    case __.MaxSalary : _MaxSalary = Convert.ToDouble(value); break;
                    case __.MaxSalaryRange : _MaxSalaryRange = Convert.ToString(value); break;
                    case __.MaxPositionName : _MaxPositionName = Convert.ToString(value); break;
                    case __.PositionCount : _PositionCount = Convert.ToInt32(value); break;
                    case __.Source : _Source = Convert.ToString(value); break;
                    case __.CreateDate : _CreateDate = Convert.ToDateTime(value); break;
                    case __.UpdateDate : _UpdateDate = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得公司字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>ID</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>关键字</summary>
            public static readonly Field Key = FindByName(__.Key);

            ///<summary>公司名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            ///<summary>所属城市</summary>
            public static readonly Field City = FindByName(__.City);

            ///<summary>所属区</summary>
            public static readonly Field Area = FindByName(__.Area);

            ///<summary>公司规模</summary>
            public static readonly Field Size = FindByName(__.Size);

            ///<summary>公司性质</summary>
            public static readonly Field Nature = FindByName(__.Nature);

            ///<summary>公司行业</summary>
            public static readonly Field Industry = FindByName(__.Industry);

            ///<summary>公司标签</summary>
            public static readonly Field Label = FindByName(__.Label);

            ///<summary>在招聘网站的地址</summary>
            public static readonly Field RecruitUrl = FindByName(__.RecruitUrl);

            ///<summary>公司网站地址</summary>
            public static readonly Field Url = FindByName(__.Url);

            ///<summary>地址</summary>
            public static readonly Field Address = FindByName(__.Address);

            ///<summary>经度</summary>
            public static readonly Field Lng = FindByName(__.Lng);

            ///<summary>维度</summary>
            public static readonly Field Lat = FindByName(__.Lat);

            ///<summary>最低薪水</summary>
            public static readonly Field MinSalary = FindByName(__.MinSalary);

            ///<summary>最低薪水范围</summary>
            public static readonly Field MinSalaryRange = FindByName(__.MinSalaryRange);

            ///<summary>最低职位名称</summary>
            public static readonly Field MinPositionName = FindByName(__.MinPositionName);

            ///<summary>最高薪水</summary>
            public static readonly Field MaxSalary = FindByName(__.MaxSalary);

            ///<summary>最高薪水范围</summary>
            public static readonly Field MaxSalaryRange = FindByName(__.MaxSalaryRange);

            ///<summary>最高职位名称</summary>
            public static readonly Field MaxPositionName = FindByName(__.MaxPositionName);

            ///<summary>职位数量</summary>
            public static readonly Field PositionCount = FindByName(__.PositionCount);

            ///<summary>来源</summary>
            public static readonly Field Source = FindByName(__.Source);

            ///<summary>创建日期</summary>
            public static readonly Field CreateDate = FindByName(__.CreateDate);

            ///<summary>更新日期</summary>
            public static readonly Field UpdateDate = FindByName(__.UpdateDate);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得公司字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>ID</summary>
            public const String ID = "ID";

            ///<summary>关键字</summary>
            public const String Key = "Key";

            ///<summary>公司名称</summary>
            public const String Name = "Name";

            ///<summary>所属城市</summary>
            public const String City = "City";

            ///<summary>所属区</summary>
            public const String Area = "Area";

            ///<summary>公司规模</summary>
            public const String Size = "Size";

            ///<summary>公司性质</summary>
            public const String Nature = "Nature";

            ///<summary>公司行业</summary>
            public const String Industry = "Industry";

            ///<summary>公司标签</summary>
            public const String Label = "Label";

            ///<summary>在招聘网站的地址</summary>
            public const String RecruitUrl = "RecruitUrl";

            ///<summary>公司网站地址</summary>
            public const String Url = "Url";

            ///<summary>地址</summary>
            public const String Address = "Address";

            ///<summary>经度</summary>
            public const String Lng = "Lng";

            ///<summary>维度</summary>
            public const String Lat = "Lat";

            ///<summary>最低薪水</summary>
            public const String MinSalary = "MinSalary";

            ///<summary>最低薪水范围</summary>
            public const String MinSalaryRange = "MinSalaryRange";

            ///<summary>最低职位名称</summary>
            public const String MinPositionName = "MinPositionName";

            ///<summary>最高薪水</summary>
            public const String MaxSalary = "MaxSalary";

            ///<summary>最高薪水范围</summary>
            public const String MaxSalaryRange = "MaxSalaryRange";

            ///<summary>最高职位名称</summary>
            public const String MaxPositionName = "MaxPositionName";

            ///<summary>职位数量</summary>
            public const String PositionCount = "PositionCount";

            ///<summary>来源</summary>
            public const String Source = "Source";

            ///<summary>创建日期</summary>
            public const String CreateDate = "CreateDate";

            ///<summary>更新日期</summary>
            public const String UpdateDate = "UpdateDate";

        }
        #endregion
    }

    /// <summary>公司接口</summary>
    public partial interface ICompany
    {
        #region 属性
        /// <summary>ID</summary>
        Int32 ID { get; set; }

        /// <summary>关键字</summary>
        String Key { get; set; }

        /// <summary>公司名称</summary>
        String Name { get; set; }

        /// <summary>所属城市</summary>
        String City { get; set; }

        /// <summary>所属区</summary>
        String Area { get; set; }

        /// <summary>公司规模</summary>
        String Size { get; set; }

        /// <summary>公司性质</summary>
        String Nature { get; set; }

        /// <summary>公司行业</summary>
        String Industry { get; set; }

        /// <summary>公司标签</summary>
        String Label { get; set; }

        /// <summary>在招聘网站的地址</summary>
        String RecruitUrl { get; set; }

        /// <summary>公司网站地址</summary>
        String Url { get; set; }

        /// <summary>地址</summary>
        String Address { get; set; }

        /// <summary>经度</summary>
        Double Lng { get; set; }

        /// <summary>维度</summary>
        Double Lat { get; set; }

        /// <summary>最低薪水</summary>
        Double MinSalary { get; set; }

        /// <summary>最低薪水范围</summary>
        String MinSalaryRange { get; set; }

        /// <summary>最低职位名称</summary>
        String MinPositionName { get; set; }

        /// <summary>最高薪水</summary>
        Double MaxSalary { get; set; }

        /// <summary>最高薪水范围</summary>
        String MaxSalaryRange { get; set; }

        /// <summary>最高职位名称</summary>
        String MaxPositionName { get; set; }

        /// <summary>职位数量</summary>
        Int32 PositionCount { get; set; }

        /// <summary>来源</summary>
        String Source { get; set; }

        /// <summary>创建日期</summary>
        DateTime CreateDate { get; set; }

        /// <summary>更新日期</summary>
        DateTime UpdateDate { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}