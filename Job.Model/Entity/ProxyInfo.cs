/*
 * XCoder v6.4.5630.33408
 * 作者：zhangchanglin/A-ZHANGCHANGLIN
 * 时间：2016-08-29 17:25:40
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
    /// <summary>代理IP信息</summary>
    [Serializable]
    [DataObject]
    [Description("代理IP信息")]
    [BindIndex("PRIMARY", true, "ID")]
    [BindRelation("ID", true, "ProxyInfoLog", "ProxyInfoID")]
    [BindTable("ProxyInfo", Description = "代理IP信息", ConnName = "Common", DbType = DatabaseType.MySql)]
    public partial class ProxyInfo : IProxyInfo
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

        private String _IP;
        /// <summary>IP</summary>
        [DisplayName("IP")]
        [Description("IP")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(2, "IP", "IP", null, "varchar(50)", 0, 0, false)]
        public virtual String IP
        {
            get { return _IP; }
            set { if (OnPropertyChanging(__.IP, value)) { _IP = value; OnPropertyChanged(__.IP); } }
        }

        private Int32 _Port;
        /// <summary>端口</summary>
        [DisplayName("端口")]
        [Description("端口")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(3, "Port", "端口", null, "int(11)", 10, 0, false)]
        public virtual Int32 Port
        {
            get { return _Port; }
            set { if (OnPropertyChanging(__.Port, value)) { _Port = value; OnPropertyChanged(__.Port); } }
        }

        private String _Country;
        /// <summary>国家</summary>
        [DisplayName("国家")]
        [Description("国家")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(4, "Country", "国家", null, "varchar(50)", 0, 0, false)]
        public virtual String Country
        {
            get { return _Country; }
            set { if (OnPropertyChanging(__.Country, value)) { _Country = value; OnPropertyChanged(__.Country); } }
        }

        private String _Address;
        /// <summary>地址</summary>
        [DisplayName("地址")]
        [Description("地址")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(5, "Address", "地址", null, "varchar(200)", 0, 0, false)]
        public virtual String Address
        {
            get { return _Address; }
            set { if (OnPropertyChanging(__.Address, value)) { _Address = value; OnPropertyChanged(__.Address); } }
        }

        private String _AnonymityType;
        /// <summary>匿名类型</summary>
        [DisplayName("匿名类型")]
        [Description("匿名类型")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(6, "AnonymityType", "匿名类型", null, "varchar(50)", 0, 0, false)]
        public virtual String AnonymityType
        {
            get { return _AnonymityType; }
            set { if (OnPropertyChanging(__.AnonymityType, value)) { _AnonymityType = value; OnPropertyChanged(__.AnonymityType); } }
        }

        private String _Protocol;
        /// <summary>协议类型</summary>
        [DisplayName("协议类型")]
        [Description("协议类型")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(7, "Protocol", "协议类型", null, "varchar(50)", 0, 0, false)]
        public virtual String Protocol
        {
            get { return _Protocol; }
            set { if (OnPropertyChanging(__.Protocol, value)) { _Protocol = value; OnPropertyChanged(__.Protocol); } }
        }

        private Double _Speed;
        /// <summary>速度（秒）</summary>
        [DisplayName("速度秒")]
        [Description("速度（秒）")]
        [DataObjectField(false, false, true, 22)]
        [BindColumn(8, "Speed", "速度（秒）", null, "double", 22, 0, false)]
        public virtual Double Speed
        {
            get { return _Speed; }
            set { if (OnPropertyChanging(__.Speed, value)) { _Speed = value; OnPropertyChanged(__.Speed); } }
        }

        private Double _ConnectTime;
        /// <summary>连接时间（秒）</summary>
        [DisplayName("连接时间秒")]
        [Description("连接时间（秒）")]
        [DataObjectField(false, false, true, 22)]
        [BindColumn(9, "ConnectTime", "连接时间（秒）", null, "double", 22, 0, false)]
        public virtual Double ConnectTime
        {
            get { return _ConnectTime; }
            set { if (OnPropertyChanging(__.ConnectTime, value)) { _ConnectTime = value; OnPropertyChanged(__.ConnectTime); } }
        }

        private String _TTL;
        /// <summary>存活时间</summary>
        [DisplayName("存活时间")]
        [Description("存活时间")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(10, "TTL", "存活时间", null, "varchar(50)", 0, 0, false)]
        public virtual String TTL
        {
            get { return _TTL; }
            set { if (OnPropertyChanging(__.TTL, value)) { _TTL = value; OnPropertyChanged(__.TTL); } }
        }

        private String _VerifyTime;
        /// <summary>验证时间</summary>
        [DisplayName("验证时间")]
        [Description("验证时间")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(11, "VerifyTime", "验证时间", null, "varchar(50)", 0, 0, false)]
        public virtual String VerifyTime
        {
            get { return _VerifyTime; }
            set { if (OnPropertyChanging(__.VerifyTime, value)) { _VerifyTime = value; OnPropertyChanged(__.VerifyTime); } }
        }

        private String _Source;
        /// <summary>来源</summary>
        [DisplayName("来源")]
        [Description("来源")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(12, "Source", "来源", null, "varchar(200)", 0, 0, false)]
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
        [BindColumn(13, "CreateDate", "创建日期", null, "datetime", 0, 0, false)]
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
        [BindColumn(14, "UpdateDate", "更新日期", null, "datetime", 0, 0, false)]
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
                    case __.IP : return _IP;
                    case __.Port : return _Port;
                    case __.Country : return _Country;
                    case __.Address : return _Address;
                    case __.AnonymityType : return _AnonymityType;
                    case __.Protocol : return _Protocol;
                    case __.Speed : return _Speed;
                    case __.ConnectTime : return _ConnectTime;
                    case __.TTL : return _TTL;
                    case __.VerifyTime : return _VerifyTime;
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
                    case __.IP : _IP = Convert.ToString(value); break;
                    case __.Port : _Port = Convert.ToInt32(value); break;
                    case __.Country : _Country = Convert.ToString(value); break;
                    case __.Address : _Address = Convert.ToString(value); break;
                    case __.AnonymityType : _AnonymityType = Convert.ToString(value); break;
                    case __.Protocol : _Protocol = Convert.ToString(value); break;
                    case __.Speed : _Speed = Convert.ToDouble(value); break;
                    case __.ConnectTime : _ConnectTime = Convert.ToDouble(value); break;
                    case __.TTL : _TTL = Convert.ToString(value); break;
                    case __.VerifyTime : _VerifyTime = Convert.ToString(value); break;
                    case __.Source : _Source = Convert.ToString(value); break;
                    case __.CreateDate : _CreateDate = Convert.ToDateTime(value); break;
                    case __.UpdateDate : _UpdateDate = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得代理IP信息字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>ID</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>IP</summary>
            public static readonly Field IP = FindByName(__.IP);

            ///<summary>端口</summary>
            public static readonly Field Port = FindByName(__.Port);

            ///<summary>国家</summary>
            public static readonly Field Country = FindByName(__.Country);

            ///<summary>地址</summary>
            public static readonly Field Address = FindByName(__.Address);

            ///<summary>匿名类型</summary>
            public static readonly Field AnonymityType = FindByName(__.AnonymityType);

            ///<summary>协议类型</summary>
            public static readonly Field Protocol = FindByName(__.Protocol);

            ///<summary>速度（秒）</summary>
            public static readonly Field Speed = FindByName(__.Speed);

            ///<summary>连接时间（秒）</summary>
            public static readonly Field ConnectTime = FindByName(__.ConnectTime);

            ///<summary>存活时间</summary>
            public static readonly Field TTL = FindByName(__.TTL);

            ///<summary>验证时间</summary>
            public static readonly Field VerifyTime = FindByName(__.VerifyTime);

            ///<summary>来源</summary>
            public static readonly Field Source = FindByName(__.Source);

            ///<summary>创建日期</summary>
            public static readonly Field CreateDate = FindByName(__.CreateDate);

            ///<summary>更新日期</summary>
            public static readonly Field UpdateDate = FindByName(__.UpdateDate);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得代理IP信息字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>ID</summary>
            public const String ID = "ID";

            ///<summary>IP</summary>
            public const String IP = "IP";

            ///<summary>端口</summary>
            public const String Port = "Port";

            ///<summary>国家</summary>
            public const String Country = "Country";

            ///<summary>地址</summary>
            public const String Address = "Address";

            ///<summary>匿名类型</summary>
            public const String AnonymityType = "AnonymityType";

            ///<summary>协议类型</summary>
            public const String Protocol = "Protocol";

            ///<summary>速度（秒）</summary>
            public const String Speed = "Speed";

            ///<summary>连接时间（秒）</summary>
            public const String ConnectTime = "ConnectTime";

            ///<summary>存活时间</summary>
            public const String TTL = "TTL";

            ///<summary>验证时间</summary>
            public const String VerifyTime = "VerifyTime";

            ///<summary>来源</summary>
            public const String Source = "Source";

            ///<summary>创建日期</summary>
            public const String CreateDate = "CreateDate";

            ///<summary>更新日期</summary>
            public const String UpdateDate = "UpdateDate";

        }
        #endregion
    }

    /// <summary>代理IP信息接口</summary>
    public partial interface IProxyInfo
    {
        #region 属性
        /// <summary>ID</summary>
        Int32 ID { get; set; }

        /// <summary>IP</summary>
        String IP { get; set; }

        /// <summary>端口</summary>
        Int32 Port { get; set; }

        /// <summary>国家</summary>
        String Country { get; set; }

        /// <summary>地址</summary>
        String Address { get; set; }

        /// <summary>匿名类型</summary>
        String AnonymityType { get; set; }

        /// <summary>协议类型</summary>
        String Protocol { get; set; }

        /// <summary>速度（秒）</summary>
        Double Speed { get; set; }

        /// <summary>连接时间（秒）</summary>
        Double ConnectTime { get; set; }

        /// <summary>存活时间</summary>
        String TTL { get; set; }

        /// <summary>验证时间</summary>
        String VerifyTime { get; set; }

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