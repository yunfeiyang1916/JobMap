using Common.HiLogHelper;
using HtmlAgilityPack;
using Ivony.Html.Parser;
using Ivony.Html;
using Job.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Job.Common
{
    public class JobRequest
    {

        private HtmlWeb htmlWeb = new HtmlWeb();

        JumonyParser jumony = new JumonyParser();

        #region 获取get请求返回的基本数据
        /// <summary>
        /// 获取get请求返回的基本数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public void GetRequest(ref List<JobInfo> listJobInfo, string url, DataType type, int pn = 1, string kd = ".net")
        {
            HtmlAgilityPack.HtmlDocument response = null;
            switch (type)
            {
                case DataType.猎聘网:
                    htmlWeb.OverrideEncoding = Encoding.GetEncoding("UTF-8");
                    try
                    {
                        response = htmlWeb.Load(url);
                    }
                    catch (Exception ex)
                    {
                        LogSave.ErrLogSave("", ex);
                        break;
                    }
                    #region MyRegion
                    var ulS = response.DocumentNode.SelectNodes("//*[@id='sojob']/div[2]/div/div/ul/li");
                    if (ulS == null || ulS.Count <= 0)
                        break;
                    foreach (var item in ulS)
                    {
                        var xpath = item.XPath;
                        string titleName, infourl, company, city, date, salary, salary_em, source;
                        titleName = item.SelectSingleNode(xpath + "/a").Attributes["title"].Value;
                        infourl = item.SelectSingleNode(xpath + "/a").Attributes["href"].Value;
                        company = item.SelectSingleNode(xpath + "/a/dl/dt[@class='company']").InnerText;
                        city = item.SelectSingleNode(xpath + "/a/dl/dt[@class='city']/span").InnerText;
                        date = item.SelectSingleNode(xpath + "/a/dl/dt[@class='date']/span").InnerText;
                        salary = item.SelectSingleNode(xpath + "/a/dl/dt[@class='salary']/span").InnerText;
                        salary_em = item.SelectSingleNode(xpath + "/a/dl/dt[@class='salary']/em").InnerText;
                        source = "猎聘网";

                        listJobInfo.Add(
                            new JobInfo()
                            {
                                city = city,
                                company = company,
                                date = date,
                                info_url = infourl,
                                salary = salary,
                                salary_em = salary_em,
                                titleName = titleName,
                                source = source
                            });
                    }
                    #endregion
                    break;
                case DataType.智联招聘:
                    htmlWeb.OverrideEncoding = Encoding.GetEncoding("UTF-8");
                    try
                    {
                        response = htmlWeb.Load(url);
                    }
                    catch (Exception ex)
                    {
                        LogSave.ErrLogSave("", ex);
                        break;
                    }
                    #region MyRegion
                    ulS = response.DocumentNode.SelectNodes("//*[@id='newlist_list_content_table']/table[@class='newlist']");
                    if (ulS == null || ulS.Count <= 0)
                        break;
                    for (int i = 1; i < ulS.Count; i++)
                    {
                        var item = ulS[i];
                        var xpath = item.XPath;
                        string titleName, infourl, company, city, date, salary, salary_em, source;
                        titleName = item.SelectSingleNode(xpath + "/tr/td[@class='zwmc']/div/a").InnerText;
                        infourl = item.SelectSingleNode(xpath + "/tr/td[@class='zwmc']/div/a").Attributes["href"].Value;
                        company = item.SelectSingleNode(xpath + "/tr/td[@class='gsmc']/a").InnerText;
                        city = item.SelectSingleNode(xpath + "/tr/td[@class='gzdd']").InnerText;
                        date = item.SelectSingleNode(xpath + "/tr/td[@class='gxsj']/span").InnerText;
                        salary = "月薪"; //item.SelectSingleNode(xpath + "/td[@class='gsmc']/a").InnerText;
                        salary_em = item.SelectSingleNode(xpath + "/tr/td[@class='zwyx']").InnerText;
                        source = "智联招聘";

                        listJobInfo.Add(
                            new JobInfo()
                            {
                                city = city,
                                company = company,
                                date = date,
                                info_url = infourl,
                                salary = salary,
                                salary_em = salary_em,
                                titleName = titleName,
                                source = source
                            });
                    }
                    #endregion
                    break;
                case DataType.前程无忧:
                    htmlWeb.OverrideEncoding = Encoding.GetEncoding("GBK");
                    try
                    {
                        // var document = jumony.LoadDocument(url);
                        response = htmlWeb.Load(url);
                    }
                    catch (Exception ex)
                    {
                        LogSave.ErrLogSave("", ex);
                        break;
                    }

                    #region MyRegion
                    ulS = response.DocumentNode.SelectNodes("//*[@id='resultList']/tr[@class='tr0']");
                    if (ulS == null || ulS.Count <= 0)
                        break;
                    for (int i = 1; i < ulS.Count; i++)
                    {
                        var item = ulS[i];
                        var xpath = item.XPath;
                        string titleName, infourl, company, city, date, salary, salary_em, source;
                        titleName = item.SelectSingleNode(xpath + "/td[@class='td1']/a").InnerText;
                        infourl = item.SelectSingleNode(xpath + "/td[@class='td1']/a").Attributes["href"].Value;
                        company = item.SelectSingleNode(xpath + "/td[@class='td2']/a").InnerText;
                        city = item.SelectSingleNode(xpath + "/td[@class='td3']/span").InnerText;
                        date = item.SelectSingleNode(xpath + "/td[@class='td4']/span").InnerText;
                        salary = "月薪"; //item.SelectSingleNode(xpath + "/td[@class='gsmc']/a").InnerText;
                        salary_em = "面议";
                        source = "前程无忧";

                        listJobInfo.Add(
                            new JobInfo()
                            {
                                city = city,
                                company = company,
                                date = date,
                                info_url = infourl,
                                salary = salary,
                                salary_em = salary_em,
                                titleName = titleName,
                                source = source
                            });
                    }
                    #endregion
                    break;
                case DataType.拉勾网:

                    #region 旧代码

                    //StringContent fromurlcontent = new StringContent("first=true&pn=" + pn + "&kd=" + kd);
                    //fromurlcontent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                    ////fromurlcontent.Headers.Add("dataType", "json");
                    //fromurlcontent.Headers.Add("X-Requested-With", "XMLHttpRequest");
                    ////fromurlcontent.Headers.Add("VerificationToken", str1.Trim());

                    //HttpClient httpclient = new HttpClient();
                    //HttpResponseMessage responseMsg = httpclient.PostAsync(new Uri(url), fromurlcontent).Result;
                    //var result = responseMsg.Content.ReadAsStringAsync().Result;
                    //JavaScriptSerializer _jsSerializer = new JavaScriptSerializer();
                    //LagouInfo JPostData = _jsSerializer.Deserialize<LagouInfo>(result);


                    //#region MyRegion

                    //if (JPostData == null || JPostData.content == null || JPostData.content.result == null)
                    //    return;
                    //for (int i = 0; i < JPostData.content.result.Count; i++)
                    //{
                    //    var item = JPostData.content.result[i];
                    //    string titleName, infourl, company, city, date, salary, salary_em, source;
                    //    titleName = item.positionName;
                    //    infourl = "http://www.lagou.com/jobs/" + item.positionId + ".html";
                    //    company = item.companyShortName;
                    //    city = item.city;
                    //    date = DateTime.Parse(item.createTime).ToString("yyyy-MM-dd");
                    //    salary = "月薪"; //item.SelectSingleNode(xpath + "/td[@class='gsmc']/a").InnerText;
                    //    salary_em = item.salary;
                    //    source = "拉勾网";

                    //    listJobInfo.Add(
                    //        new JobInfo()
                    //        {
                    //            city = city,
                    //            company = company,
                    //            date = date,
                    //            info_url = infourl,
                    //            salary = salary,
                    //            salary_em = salary_em,
                    //            titleName = titleName,
                    //            source = source
                    //        });
                    //}
                    //#endregion

                    #endregion
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 根据url请求，返回详细信息
        /// <summary>
        /// 根据url请求，返回详细信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetUrlInfo(string url, DataType type)
        {
            var ulS = string.Empty;
            switch (type)
            {
                case DataType.智联招聘:
                    #region 问题：“gzip”不是受支持的编码名 的处理方法  http://www.cnblogs.com/soundcode/p/3785152.html
                    HtmlAgilityPack.HtmlWeb.PreRequestHandler handler = delegate (HttpWebRequest request)
                               {
                                   request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";
                                   request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                                   request.CookieContainer = new System.Net.CookieContainer();
                                   return true;
                               };
                    htmlWeb.PreRequest += handler;
                    #endregion
                    htmlWeb.OverrideEncoding = Encoding.GetEncoding("UTF-8");
                    HtmlAgilityPack.HtmlDocument response = htmlWeb.Load(url);

                    var fuli = response.DocumentNode.SelectNodes("/html/body/div[3]/div[1]/div[1]/div");
                    var jiben = response.DocumentNode.SelectNodes("/html/body/div[4]/div[1]/ul");
                    var miaoshu = response.DocumentNode.SelectNodes("/html/body/div[4]/div[1]/div[1]/div/div[1]");

                    if (fuli != null && fuli.Count >= 1 && !string.IsNullOrEmpty(fuli[0].InnerText.Trim()))
                        ulS += "<h3>福利诱惑:</h3>" + fuli[0].InnerText;
                    if (jiben != null && jiben.Count >= 1 && !string.IsNullOrEmpty(jiben[0].InnerText.Trim()))
                        ulS += "<h3>基本信息:</h3>" + jiben[0].InnerText;
                    if (miaoshu != null && miaoshu.Count >= 1 && !string.IsNullOrEmpty(miaoshu[0].InnerText.Trim()))
                        ulS += "<h3>职位描述:</h3>" + miaoshu[0].InnerText;
                    break;
                case DataType.猎聘网:
                    htmlWeb.OverrideEncoding = Encoding.GetEncoding("UTF-8");
                    response = htmlWeb.Load(url);
                    //--基本信息
                    var jbinfo = response.DocumentNode.SelectNodes("//*[@id='job-view-enterprise']/div[1]/div[1]/div[1]/div[3]/div") ??
                          response.DocumentNode.SelectNodes("//*[@id='job-hunter']/div[1]/div[1]/div[1]/div[3]/div");
                    //职位描述
                    var selectNodes = response.DocumentNode.SelectNodes("//*[@id='job-hunter']/div[1]/div[1]/div[1]/div[4]")
                        ?? response.DocumentNode.SelectNodes("//*[@id='job-view-enterprise']/div[1]/div[1]/div[1]/div[4]");
                    //岗位要求                    
                    var ganwei = response.DocumentNode.SelectNodes("//*[@id='job-hunter']/div[1]/div[1]/div[1]/div[5]/div")
                      ?? response.DocumentNode.SelectNodes("//*[@id='job-view-enterprise']/div[1]/div[1]/div[1]/div[5]/div");
                    ulS = "<h3>基本信息:</h3>" + jbinfo[0].InnerText +
                          "<h3>职位描述:</h3>" + selectNodes[0].InnerText +
                          "<h3>岗位要求:</h3>" + ganwei[0].InnerText;
                    break;
                case DataType.前程无忧:
                    htmlWeb.OverrideEncoding = Encoding.GetEncoding("GBK");
                    response = htmlWeb.Load(url);
                    //--
                    ulS = "<h3>基本信息:</h3>" + response.DocumentNode.SelectNodes("/html/body/div[3]/div/div[2]/table[1]/tr[3]/td[1]")[0].InnerText +
                          "<h3>职位描述:</h3>" + response.DocumentNode.SelectNodes("/html/body/div[3]/div/div[2]/div[1]/div[2]/div/table")[0].InnerText;

                    break;
                case DataType.拉勾网:
                    htmlWeb.OverrideEncoding = Encoding.GetEncoding("UTF-8");
                    response = htmlWeb.Load(url);
                    ulS = "<h3>基本信息:</h3>" + response.DocumentNode.SelectNodes("//*[@id='container']/div[1]/div[1]/dl/dd[1]")[0].InnerText +
                          "<h3>职位描述:</h3>" + response.DocumentNode.SelectNodes("//*[@id='container']/div[1]/div[1]/dl/dd[2]")[0].InnerText;
                    break;
            }

            return ulS.ToJson();
        }
        #endregion

    }
}
