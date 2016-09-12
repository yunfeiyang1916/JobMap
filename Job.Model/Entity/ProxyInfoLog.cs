/*
 * XCoder v6.4.5630.33408
 * 作者：zhangchanglin/A-ZHANGCHANGLIN
 * 时间：2016-08-30 15:48:34
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
    /// <summary>使用代理IP信息记录</summary>
    [Serializable]
    [DataObject]
    [Description("使用代理IP信息记录")]
    [BindIndex("PRIMARY", true, "ID")]
    [BindIndex("IX_ProxyInfoLog_ProxyInfoID", false, "ProxyInfoID")]
    [BindRelation("ProxyInfoID", false, "ProxyInfo", "ID")]
    [BindTable("ProxyInfoLog", Description = "使用代理IP信息记录", ConnName = "Common", DbType = DatabaseType.MySql)]
    public partial class ProxyInfoLog : IProxyInfoLog
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

        private Int32 _ProxyInfoID;
        /// <summary>关联代理IP信息ID</summary>
        [DisplayName("关联代理IP信息ID")]
        [Description("关联代理IP信息ID")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(2, "ProxyInfoID", "关联代理IP信息ID", null, "int(11)", 10, 0, false)]
        public virtual Int32 ProxyInfoID
        {
            get { return _ProxyInfoID; }
            set { if (OnPropertyChanging(__.ProxyInfoID, value)) { _ProxyInfoID = value; OnPropertyChanged(__.ProxyInfoID); } }
        }

        private String _IP;
        /// <summary>IP</summary>
        [DisplayName("IP")]
        [Description("IP")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(3, "IP", "IP", null, "varchar(50)", 0, 0, false)]
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
        [BindColumn(4, "Port", "端口", null, "int(11)", 10, 0, false)]
        public virtual Int32 Port
        {
            get { return _Port; }
            set { if (OnPropertyChanging(__.Port, value)) { _Port = value; OnPropertyChanged(__.Port); } }
        }

        private UInt64 _IsEnabled;
        /// <summary>是否可用</summary>
        [DisplayName("是否可用")]
        [Description("是否可用")]
        [DataObjectField(false, false, true, 1)]
        [BindColumn(5, "IsEnabled", "是否可用", null, "bit(1)", 1, 0, false)]
        public virtual UInt64 IsEnabled
        {
            get { return _IsEnabled; }
            set { if (OnPropertyChanging(__.IsEnabled, value)) { _IsEnabled = value; OnPropertyChanged(__.IsEnabled); } }
        }

        private Double _Elapsed;
        /// <summary>耗时（秒）</summary>
        [DisplayName("耗时秒")]
        [Description("耗时（秒）")]
        [DataObjectField(false, false, true, 22)]
        [BindColumn(6, "Elapsed", "耗时（秒）", null, "double", 22, 0, false)]
        public virtual Double Elapsed
        {
            get { return _Elapsed; }
            set { if (OnPropertyChanging(__.Elapsed, value)) { _Elapsed = value; OnPropertyChanged(__.Elapsed); } }
        }

        private Int32 _EffectiveCount;
        /// <summary>有效次数</summary>
        [DisplayName("有效次数")]
        [Description("有效次数")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(7, "EffectiveCount", "有效次数", null, "int(11)", 10, 0, false)]
        public virtual Int32 EffectiveCount
        {
            get { return _EffectiveCount; }
            set { if (OnPropertyChanging(__.EffectiveCount, value)) { _EffectiveCount = value; OnPropertyChanged(__.EffectiveCount); } }
        }

        private Int32 _InvalidCount;
        /// <summary>无效次数</summary>
        [DisplayName("无效次数")]
        [Description("无效次数")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(8, "InvalidCount", "无效次数", null, "int(11)", 10, 0, false)]
        public virtual Int32 InvalidCount
        {
            get { return _InvalidCount; }
            set { if (OnPropertyChanging(__.InvalidCount, value)) { _InvalidCount = value; OnPropertyChanged(__.InvalidCount); } }
        }

        private DateTime _CreateDate;
        /// <summary>创建日期</summary>
        [DisplayName("创建日期")]
        [Description("创建日期")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn(9, "CreateDate", "创建日期", null, "datetime", 0, 0, false)]
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
        [BindColumn(10, "UpdateDate", "更新日期", null, "datetime", 0, 0, false)]
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
                    case __.ProxyInfoID : return _ProxyInfoID;
                    case __.IP : return _IP;
                    case __.Port : return _Port;
                    case __.IsEnabled : return _IsEnabled;
                    case __.Elapsed : return _Elapsed;
                    case __.EffectiveCount : return _EffectiveCount;
                    case __.InvalidCount : return _InvalidCount;
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
                    case __.ProxyInfoID : _ProxyInfoID = Convert.ToInt32(value); break;
                    case __.IP : _IP = Convert.ToString(value); break;
                    case __.Port : _Port = Convert.ToInt32(value); break;
                    case __.IsEnabled : _IsEnabled = Convert.ToUInt64(value); break;
                    case __.Elapsed : _Elapsed = Convert.ToDouble(value); break;
                    case __.EffectiveCount : _EffectiveCount = Convert.ToInt32(value); break;
                    case __.InvalidCount : _InvalidCount = Convert.ToInt32(value); break;
                    case __.CreateDate : _CreateDate = Convert.ToDateTime(value); break;
                    case __.UpdateDate : _UpdateDate = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得使用代理IP信息记录字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>ID</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>关联代理IP信息ID</summary>
            public static readonly Field ProxyInfoID = FindByName(__.ProxyInfoID);

            ///<summary>IP</summary>
            public static readonly Field IP = FindByName(__.IP);

            ///<summary>端口</summary>
            public static readonly Field Port = FindByName(__.Port);

            ///<summary>是否可用</summary>
            public static readonly Field IsEnabled = FindByName(__.IsEnabled);

            ///<summary>耗时（秒）</summary>
            public static readonly Field Elapsed = FindByName(__.Elapsed);

            ///<summary>有效次数</summary>
            public static readonly Field EffectiveCount = FindByName(__.EffectiveCount);

            ///<summary>无效次数</summary>
            public static readonly Field InvalidCount = FindByName(__.InvalidCount);

            ///<summary>创建日期</summary>
            public static readonly Field CreateDate = FindByName(__.CreateDate);

            ///<summary>更新日期</summary>
            public static readonly Field UpdateDate = FindByName(__.UpdateDate);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得使用代理IP信息记录字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>ID</summary>
            public const String ID = "ID";

            ///<summary>关联代理IP信息ID</summary>
            public const String ProxyInfoID = "ProxyInfoID";

            ///<summary>IP</summary>
            public const String IP = "IP";

            ///<summary>端口</summary>
            public const String Port = "Port";

            ///<summary>是否可用</summary>
            public const String IsEnabled = "IsEnabled";

            ///<summary>耗时（秒）</summary>
            public const String Elapsed = "Elapsed";

            ///<summary>有效次数</summary>
            public const String EffectiveCount = "EffectiveCount";

            ///<summary>无效次数</summary>
            public const String InvalidCount = "InvalidCount";

            ///<summary>创建日期</summary>
            public const String CreateDate = "CreateDate";

            ///<summary>更新日期</summary>
            public const String UpdateDate = "UpdateDate";

        }
        #endregion
    }

    /// <summary>使用代理IP信息记录接口</summary>
    public partial interface IProxyInfoLog
    {
        #region 属性
        /// <summary>ID</summary>
        Int32 ID { get; set; }

        /// <summary>关联代理IP信息ID</summary>
        Int32 ProxyInfoID { get; set; }

        /// <summary>IP</summary>
        String IP { get; set; }

        /// <summary>端口</summary>
        Int32 Port { get; set; }

        /// <summary>是否可用</summary>
        UInt64 IsEnabled { get; set; }

        /// <summary>耗时（秒）</summary>
        Double Elapsed { get; set; }

        /// <summary>有效次数</summary>
        Int32 EffectiveCount { get; set; }

        /// <summary>无效次数</summary>
        Int32 InvalidCount { get; set; }

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