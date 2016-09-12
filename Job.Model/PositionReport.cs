using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Model
{
    /// <summary>职位报表</summary>
    public class PositionReport
    {
        private String _City;
        /// <summary>所属城市</summary>
        public String City { get { return _City; } set { _City = value; } }

        private String _Source;
        /// <summary>来源</summary>
        public String Source { get { return _Source; } set { _Source = value; } }

        private Int32 _DiscussCount;
        /// <summary>面议数量</summary>
        public Int32 DiscussCount { get { return _DiscussCount; } set { _DiscussCount = value; } }

        private Int32 _K0Count;
        /// <summary>0-5k数量</summary>
        public Int32 K0Count { get { return _K0Count; } set { _K0Count = value; } }

        private Int32 _K5Count;
        /// <summary>5-10K数量</summary>
        public Int32 K5Count { get { return _K5Count; } set { _K5Count = value; } }

        private Int32 _K10Count;
        /// <summary>10-15k数量</summary>
        public Int32 K10Count { get { return _K10Count; } set { _K10Count = value; } }

        private Int32 _K15Count;
        /// <summary>15-20k数量</summary>
        public Int32 K15Count { get { return _K15Count; } set { _K15Count = value; } }

        private Int32 _K20Count;
        /// <summary>20-25k数量</summary>
        public Int32 K20Count { get { return _K20Count; } set { _K20Count = value; } }

        private Int32 _K25Count;
        /// <summary>25k以上</summary>
        public Int32 K25Count { get { return _K25Count; } set { _K25Count = value; } }
        /// <summary>重写ToString</summary>
        /// <returns></returns>
        public override String ToString()
        {
            return String.Format("{{name:'{0}',data:[{1},{2},{3},{4},{5},{6},{7}]}}",City,DiscussCount,K0Count,K5Count,K10Count,K15Count,K20Count,K25Count);
        }

    }
}
