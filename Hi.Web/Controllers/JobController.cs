using Job.Common;
using Job.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Job.Model.Entity;

namespace Hi.Web.Controllers
{
    /// <summary>职位控制器</summary>
    public class JobController : Controller
    {
        BaiduHelper helper = new BaiduHelper();
        /// <summary>构造函数</summary>
        public JobController()
        {
            ViewBag.BaiduAk = helper.Ak;
            ViewBag.BaiduJsAk = helper.JsAk;
        }

        /// <summary>首页</summary>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>获取geo数据列</summary>
        public ActionResult ColumnList()
        {
            return Content(helper.GetGeoColumn("150268"));
        }

        /// <summary>创建geo数据表</summary>
        public ActionResult CreateGeoTable()
        {
            //云存储表名
            String[] tableNames = new String[] { "LaGouNet","LaGouJava", "LaGouPhp", "LaGouAccountant",
                                                 "ZhiLianNet","ZhiLianJava", "ZhiLianPhp", "ZhiLianAccountant",
                                                 "Job51Net","Job51Java", "Job51Php", "Job51Accountant",
                                                 "LiePinNet","LiePinJava", "LiePinPhp", "LiePinAccountant" };
            BaiduResult tableResult = null;
            foreach (var tableName in tableNames)
            {
                tableResult = helper.CreateGeoTable(tableName);
                if (tableResult.Status == 0)
                {
                    List<BaiduGeoColumn> columnList = new List<BaiduGeoColumn>();
                    columnList.Add(new BaiduGeoColumn("City", "所属城市", 3, tableResult.ID) { IsSearchField = 1 });
                    columnList.Add(new BaiduGeoColumn("Area", "所属区", 3, tableResult.ID) { IsSearchField = 1 });
                    columnList.Add(new BaiduGeoColumn("Key", "关键字_职位", 3, tableResult.ID) { IsSearchField = 1 });
                    columnList.Add(new BaiduGeoColumn("Source", "来源", 3, tableResult.ID) { IsSearchField = 1 });
                    columnList.Add(new BaiduGeoColumn("MaxPositionName", "最高职位名称", 3, tableResult.ID));
                    columnList.Add(new BaiduGeoColumn("MaxSalary", "最高薪水", 2, tableResult.ID) { IsSortfilterField = 1 });
                    columnList.Add(new BaiduGeoColumn("MaxSalaryRange", "最高薪水范围", 3, tableResult.ID));
                    columnList.Add(new BaiduGeoColumn("MinPositionName", "最低职位名称", 3, tableResult.ID));
                    columnList.Add(new BaiduGeoColumn("MinSalary", "最低薪水", 2, tableResult.ID) { IsSortfilterField = 1 });
                    columnList.Add(new BaiduGeoColumn("MinSalaryRange", "最低薪水范围", 3, tableResult.ID));
                    columnList.Add(new BaiduGeoColumn("PositionCount", "职位数量", 1, tableResult.ID) { IsSortfilterField = 1 });
                    columnList.Add(new BaiduGeoColumn("UpdateDate", "更新日期", 3, tableResult.ID));
                    foreach (var item in columnList)
                    {
                        helper.CreateGeoColumn(item);
                    }
                }


            }
            return Json(tableResult, JsonRequestBehavior.AllowGet);

        }

        /// <summary>更新geo数据列</summary>
        public ActionResult UpdateColumn()
        {
            return Json(helper.UpdateGeoColumn(260313, "150268", 1), JsonRequestBehavior.AllowGet);
        }
        /// <summary>报表 </summary>
        /// <param name="position">职位</param>
        /// <param name="source">来源</param>
        /// <returns></returns>
        public ActionResult Report(String position = ".net", String source = "拉勾网")
        {
            var model = PositionInfo.GetReports(position, source);
            return View(model);
        }
        /// <summary>地图显示</summary>
        public ActionResult Map()
        {
            return View();
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
            return helper.Local(150268, q, "北京", "Key:.net");
        }
    }
}
