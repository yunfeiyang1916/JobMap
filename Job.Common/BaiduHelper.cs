using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using System.Net.Http;
using NewLife.Log;
using System.Net.Http.Headers;
using System.Configuration;

namespace Job.Common
{
    /// <summary>调用百度API辅助类</summary>
    public class BaiduHelper
    {
        #region 属性

        private String _Ak;
        /// <summary>access_key</summary>
        public String Ak { get { return _Ak; } set { _Ak = value; } }

        private String _JsAk;
        /// <summary>JsAk，这是前端用的，配置到这是为了前端使用方便</summary>
        public String JsAk { get { return _JsAk; } set { _JsAk = value; } }

        private String _Url = "http://api.map.baidu.com";
        /// <summary>百度地图请求Api地址</summary>
        public String Url { get { return _Url; } set { _Url = value; } }

        #endregion

        #region 构造函数

        /// <summary>构造函数</summary>
        public BaiduHelper()
        {
            _Ak = ConfigurationManager.AppSettings["BaiduAk"];
            //这是前端用的，配置到这是为了前端使用方便
            _JsAk = ConfigurationManager.AppSettings["BaiduJsAk"];
            if (String.IsNullOrWhiteSpace(_Ak) || String.IsNullOrWhiteSpace(_JsAk))
            {
                throw new ArgumentNullException("百度地图api浏览器端ak或服务端ak不能为空.可以去百度地图平台（http://lbsyun.baidu.com/）获取ak.");
            }
        }

        #endregion

        #region 获取POI坐标

        /// <summary>
        /// 根据地址获取POI点坐标，百度有次数限制，大概6000次，可以多换几个ak用
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public BaiduMapGeocodingResult GetPOI(String address, String city = null)
        {
            String url = String.Format("{0}/geocoder/v2/?output=json&ak={1}&address={2}", Url, Ak, HttpUtility.UrlEncode(address));
            if (!String.IsNullOrWhiteSpace(city))
            {
                url += "&city=" + HttpUtility.UrlEncode(city);
            }
            try
            {
                //HttpWebRequestHelper request = new HttpWebRequestHelper();
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage responseMsg = httpClient.GetAsync(url).Result;
                String resultStr = responseMsg.Content.ReadAsStringAsync().Result;
                //String resultStr = request.Get(Url + "&address=" + HttpUtility.UrlEncode(address));
                if (!String.IsNullOrEmpty(resultStr))
                {
                    BaiduMapGeocoding result = JsonConvert.DeserializeObject<BaiduMapGeocoding>(resultStr);
                    //成功查询到
                    if (result.Status == 0)
                    {
                        result.Result.Address = address;
                    }
                    return result.Result;
                }
                return null;
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("获取poi失败，请求url为：{0}，失败原因：{1}", url, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 批量获取POI点坐标
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public List<BaiduMapGeocodingResult> GetPOIs(List<String> addresss)
        {
            List<BaiduMapGeocodingResult> list = new List<BaiduMapGeocodingResult>();
            foreach (var item in addresss)
            {
                var result = GetPOI(item);
                if (result != null)
                {
                    list.Add(result);
                }
            }
            return list;
            //return JsonConvert.SerializeObject(list);
        }


        #endregion

        #region 云存储/云检索

        /// <summary>创建geo数据表</summary>
        /// <param name="name">表名</param>
        /// <param name="geotype">geotable持有数据的类型，1：点；2：线；3：面。默认为1（当前不支持“线”）</param>
        /// <param name="is_published">是否发布到检索</param>
        /// <returns></returns>
        public BaiduResult CreateGeoTable(String name, Int32 geotype = 1, Int32 is_published = 1)
        {
            String url = String.Format("{0}/geodata/v3/geotable/create", Url);

            XTrace.WriteLine("创建geo数据表请求url为：{0}", url);
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpContent content = new StringContent(String.Format("ak={0}&name={1}&geotype={2}&is_published={3}", Ak, name, geotype, is_published));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage responseMsg = httpClient.PostAsync(url, content).Result;
                String resultStr = responseMsg.Content.ReadAsStringAsync().Result;
                if (!String.IsNullOrWhiteSpace(resultStr))
                {
                    var result = JsonConvert.DeserializeObject<BaiduResult>(resultStr);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("创建geo数据表失败，请求url为：{0}，失败原因：{1}", url, ex.Message);
                throw;
            }
        }

        /// <summary>创建geo数据列</summary>
        ///<param name="column">列数据</param>
        /// <returns></returns>
        public BaiduResult CreateGeoColumn(BaiduGeoColumn column)
        {
            String url = String.Format("{0}/geodata/v3/column/create", Url);

            XTrace.WriteLine("创建geo数据列请求url为：{0}", url);
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpContent content = new StringContent(String.Format("ak={0}&{1}", Ak, column.ToString()));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage responseMsg = httpClient.PostAsync(url, content).Result;
                String resultStr = responseMsg.Content.ReadAsStringAsync().Result;
                if (!String.IsNullOrWhiteSpace(resultStr))
                {
                    var result = JsonConvert.DeserializeObject<BaiduResult>(resultStr);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("创建geo数据列失败，请求url为：{0}，失败原因：{1}", url, ex.Message);
                throw;
            }
        }

        /// <summary>创建geo数据列Poi数据</summary>
        ///<param name="data">列Poi数据</param>
        /// <returns></returns>
        public BaiduResult CreateGeoPoi(String data)
        {
            String url = String.Format("{0}/geodata/v3/poi/create", Url);

            XTrace.WriteLine("创建geo数据列请求url为：{0}", url);
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpContent content = new StringContent(String.Format("ak={0}&{1}", Ak, data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage responseMsg = httpClient.PostAsync(url, content).Result;
                String resultStr = responseMsg.Content.ReadAsStringAsync().Result;
                if (!String.IsNullOrWhiteSpace(resultStr))
                {
                    var result = JsonConvert.DeserializeObject<BaiduResult>(resultStr);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("创建geo数据列失败，请求url为：{0}，失败原因：{1}", url, ex.Message);
                throw;
            }
        }

        /// <summary>更新geo数据列</summary>
        /// <param name="id">列id</param>
        /// <param name="geotable_id"></param>
        /// <param name="is_index_field"></param>
        /// <returns></returns>
        public BaiduResult UpdateGeoColumn(Int32 id, String geotable_id, Int32 is_index_field)
        {
            String url = String.Format("{0}/geodata/v3/column/update", Url);

            XTrace.WriteLine("创建geo数据列请求url为：{0}", url);
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpContent content = new StringContent(String.Format("ak={0}&geotable_id={1}&id={2}&is_index_field={3}", Ak, geotable_id, id, is_index_field));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage responseMsg = httpClient.PostAsync(url, content).Result;
                String resultStr = responseMsg.Content.ReadAsStringAsync().Result;
                if (!String.IsNullOrWhiteSpace(resultStr))
                {
                    var result = JsonConvert.DeserializeObject<BaiduResult>(resultStr);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("创建geo数据列失败，请求url为：{0}，失败原因：{1}", url, ex.Message);
                throw;
            }
        }

        /// <summary>创建geo数据列</summary>
        /// <param name="geotable_id">表id</param>
        /// <param name="key">列名</param>
        /// <param name="name">列中文名</param>
        /// <returns></returns>
        public String GetGeoColumn(String geotable_id, String key = null, String name = null)
        {
            String url = String.Format("{0}/geodata/v3/column/list?ak={1}&geotable_id={2}&key={3}&name={4}", Url, Ak, geotable_id, key, name);

            XTrace.WriteLine("创建geo数据列请求url为：{0}", url);
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage responseMsg = httpClient.GetAsync(url).Result;
                String resultStr = responseMsg.Content.ReadAsStringAsync().Result;
                return resultStr;
                //if (!String.IsNullOrWhiteSpace(resultStr))
                //{
                //    var result = JsonConvert.DeserializeObject<BaiduResult>(resultStr);
                //    return result;
                //}
                //return null;
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("创建geo数据列失败，请求url为：{0}，失败原因：{1}", url, ex.Message);
                throw;
            }
        }

        /// <summary>poi本地检索</summary>
        /// <remarks>本地检索是指可检索指定区域范围内的poi信息，区域通过region参数来设定，可以是全国范围也可以是小范围的如海淀区</remarks>
        /// <param name="geotable_id">云存储表id</param>
        /// <param name="q">关键字</param>
        /// <param name="region">区域</param>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        public String Local(Int32 geotable_id, String q, String region, String filter)
        {
            String url = String.Format("{0}/geosearch/v3/local?ak={1}&geotable_id={2}&region={3}", Url, Ak, geotable_id, region);
            if (!String.IsNullOrWhiteSpace(q))
            {
                url += "&q=" + HttpUtility.UrlEncode(q);
            }
            if (!String.IsNullOrWhiteSpace(filter))
            {
                url += "&filter=" + HttpUtility.UrlEncode(filter);
            }
            XTrace.WriteLine("poi本地检索请求url为：{0}", url);
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage responseMsg = httpClient.GetAsync(url).Result;
                String resultStr = responseMsg.Content.ReadAsStringAsync().Result;
                return resultStr;
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("poi本地检索失败，请求url为：{0}，失败原因：{1}", url, ex.Message);
                throw;
            }
        }

        #endregion
    }

    #region POI相关类

    /// <summary>百度地图点坐标</summary>
    public class BaiduMapPoint
    {
        private Double _Lng;
        /// <summary>经度</summary>
        public Double Lng { get { return _Lng; } set { _Lng = value; } }

        private Double _Lat;
        /// <summary>维度</summary>
        public Double Lat { get { return _Lat; } set { _Lat = value; } }


        /// <summary>重写ToString</summary>
        public override String ToString()
        {
            return String.Format("经度：{0},维度：{1}", Lng, Lat);
        }
    }

    /// <summary>百度地图Api返回的地理编码</summary>
    public class BaiduMapGeocoding
    {
        private Int32 _Status;
        /// <summary>返回结果状态，成功返回0</summary>
        public Int32 Status { get { return _Status; } set { _Status = value; } }

        private BaiduMapGeocodingResult _Result;
        /// <summary>结果</summary>
        public BaiduMapGeocodingResult Result { get { return _Result; } set { _Result = value; } }

        private String[] _Results;
        /// <summary>结果集，查询出错的时候会返回一个空数组</summary>
        public String[] Results { get { return _Results; } set { _Results = value; } }

        private String _Msg;
        /// <summary>消息</summary>
        public String Msg { get { return _Msg; } set { _Msg = value; } }
    }
    /// <summary>百度地图Api返回的地理编码结果</summary>
    public class BaiduMapGeocodingResult
    {
        private BaiduMapPoint _Location;
        /// <summary>经纬度坐标</summary>
        public BaiduMapPoint Location { get { return _Location; } set { _Location = value; } }

        private Int32 _Precise;
        /// <summary>位置的附加信息，是否精确查找。1为精确查找，0为不精确</summary>
        public Int32 Precise { get { return _Precise; } set { _Precise = value; } }

        private Int32 _Confidence;
        /// <summary>可信度</summary>
        public Int32 Confidence { get { return _Confidence; } set { _Confidence = value; } }

        private String _Level;
        /// <summary>地址类型</summary>
        public String Level { get { return _Level; } set { _Level = value; } }

        private String _Address;
        /// <summary>地址（接口不会返回这个值，需要自己手动赋值）</summary>
        public String Address { get { return _Address; } set { _Address = value; } }
        /// <summary>重写ToString</summary>
        public override String ToString()
        {
            return String.Format("地址：{0},{1}", Address, Location.ToString());
        }
    }

    #endregion


    #region 云存储

    /// <summary>响应结果</summary>
    public class BaiduResult
    {
        private Int32 _Status;
        /// <summary>状态码，0代表成功，其它取值含义另行说明</summary>
        public Int32 Status { get { return _Status; } set { _Status = value; } }

        private String _Message;
        /// <summary>状态码描述 </summary>
        public String Message { get { return _Message; } set { _Message = value; } }

        private String _ID;
        /// <summary>ID</summary>
        public String ID { get { return _ID; } set { _ID = value; } }
    }

    /// <summary>geo数据列</summary>
    public class BaiduGeoColumn
    {
        #region 扩展属性

        private String _Name;
        /// <summary>中文名</summary>
        public String Name { get { return _Name; } set { _Name = value; } }

        private String _Key;
        /// <summary>列名</summary>
        public String Key { get { return _Key; } set { _Key = value; } }

        private Int32 _Type;
        /// <summary>列类型，必选，枚举值1:Int64, 2:double, 3:string, 4:在线图片url </summary>
        public Int32 Type { get { return _Type; } set { _Type = value; } }

        private Int32 _MaxLength = 50;
        /// <summary>长度，最大值2048，最小值为1。当type为string该字段有效，此时该字段必填。此值代表utf8的汉字个数，不是字节个数 </summary>
        public Int32 MaxLength { get { return _MaxLength; } set { _MaxLength = value; } }

        private String _DefaultValue;
        /// <summary>默认值</summary>
        public String DefaultValue { get { return _DefaultValue; } set { _DefaultValue = value; } }

        private Int32 _IsSortfilterField;
        /// <summary>是否检索引擎的数值排序筛选字段</summary>
        /// <remarks>必选,1代表是，0代表否。设置后，在请求 LBS云检索时可针对该字段进行排序。该字段只能为int或double类型，最多设置15个</remarks>
        public Int32 IsSortfilterField { get { return _IsSortfilterField; } set { _IsSortfilterField = value; } }

        private Int32 _IsSearchField;
        /// <summary>是否检索引擎的文本检索字段</summary>
        /// <remarks>必选，1代表支持，0为不支持。只有type为string时可以设置检索字段，只能用于字符串类型的列且最大长度不能超过512个字节 </remarks>
        public Int32 IsSearchField { get { return _IsSearchField; } set { _IsSearchField = value; } }

        private Int32 _IsIndexField;
        /// <summary>是否存储引擎的索引字段</summary>
        /// <remarks>必选，用于存储接口查询:1代表支持，0为不支持 注：is_index_field=1 时才能在根据该列属性值检索时检索到数据</remarks>
        public Int32 IsIndexField { get { return _IsIndexField; } set { _IsIndexField = value; } }

        private Int32 _IsUniqueField;
        /// <summary>是否云存储唯一索引字段，方便更新，删除，查询</summary>
        /// <remarks>可选，1代表是，0代表否。设置后将在数据创建和更新时进行该字段唯一性检查，并可以以此字段为条件进行数据的更新、删除和查询。最多设置1个 </remarks>
        public Int32 IsUniqueField { get { return _IsUniqueField; } set { _IsUniqueField = value; } }

        private String _GeoTableID;
        /// <summary>表ID</summary>
        public String GeoTableID { get { return _GeoTableID; } set { _GeoTableID = value; } }

        #endregion

        #region 构造函数
        /// <summary>geo数据列</summary>
        public BaiduGeoColumn()
        {

        }

        /// <summary>geo数据列</summary>
        /// <param name="key">列名</param>
        /// <param name="name">中文名</param>
        /// <param name="type">列类型，必选，枚举值1:Int64, 2:double, 3:string, 4:在线图片url</param>
        /// <param name="geoTableID">表id</param>
        public BaiduGeoColumn(String key, String name, Int32 type, String geoTableID)
        {
            _Key = key;
            _Name = name;
            _Type = type;
            _GeoTableID = geoTableID;
        }

        #endregion

        #region 方法

        /// <summary>重写ToString</summary>
        public override String ToString()
        {
            return String.Format("name={0}&key={1}&type={2}&max_length={3}&default_value={4}&is_sortfilter_field={5}&is_search_field={6}&is_index_field={7}&is_unique_field={8}&geotable_id={9}",
                                  Name, Key, Type, MaxLength, DefaultValue, IsSortfilterField, IsSearchField, IsIndexField, IsUniqueField, GeoTableID);
        }

        #endregion
    }

    #endregion
}
