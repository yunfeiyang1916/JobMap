using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Model
{
    /// <summary>拉勾网影响结果</summary>
    public class LagouInfo
    {
        /// <summary>响应码</summary>
        public String code;
        /// <summary>响应内容</summary>
        public Content content;
        /// <summary>消息</summary>
        public String msg;
        public String requestId;
        public String resubmitToken;
        /// <summary>是否成功</summary>
        public String success;

        /// <summary>拉勾网响应内容</summary>
        public class Content
        {
            /// <summary>页码</summary>
            public String pageNo;
            /// <summary>每页显示数量</summary>
            public String pageSize;
            /// <summary>职位结果</summary>
            public PositionResult positionResult;
        }

        /// <summary>职位结果</summary>
        public class PositionResult
        {
            /// <summary>总数</summary>
            public Int32 totalCount;
            /// <summary>结果数量</summary>
            public Int32 resultSize;
            /// <summary>结果列表</summary>
            public List<Result> result;
        }
        /// <summary>结果详情</summary>
        public class Result
        {
            /// <summary>商圈</summary>
            public List<String> businessZones;
            /// <summary>省市</summary>
            public String city;
            /// <summary>区（街道）</summary>
            public String district;
            /// <summary>公司标签</summary>
            public List<String> companyLabelList;
            /// <summary>公司Logo</summary>
            public String companyLogo;
            /// <summary>公司全称</summary>
            public String companyFullName;
            /// <summary>公司简称</summary>
            public String companyShortName;
            /// <summary>公司规模</summary>
            public String companySize;
            /// <summary>创建日期</summary>
            public String createTime;
            /// <summary>格式化后的发布日期</summary>
            public String formatCreateTime;
            /// <summary>学历要求</summary>
            public String education;
            /// <summary>融资阶段</summary>
            public String financeStage;
            /// <summary>行业领域</summary>
            public String industryField;
            /// <summary>工作性质</summary>
            public String jobNature;
            /// <summary>职位优势</summary>
            public String positionAdvantage;
            /// <summary>职位id</summary>
            public String positionId;
            /// <summary>职位名称</summary>
            public String positionName;
            /// <summary>薪水</summary>
            public String salary;
            /// <summary>评分</summary>
            public String score;
            /// <summary>工作年限</summary>
            public String workYear;
        }
    }
}
