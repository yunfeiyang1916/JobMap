using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLife.Agent;
using System.Threading;
using Job.Common;
using Job.Model;
using Job.Model.Entity;
using XCode;

namespace Job.Agent
{
    class Program
    {
        static void Main(string[] args)
        {
            JobAgent.ServiceMain();
            //ConsoleJob();
            //ProxyInfo.SaveAll();
            //ConsoleMap();
            //LaGouRequest request = new LaGouRequest();
            //LiePinRequest request = new LiePinRequest();
            //Console.WriteLine(d.address);
            //request.GetJobs(".net","北京");
            Console.ReadLine();
        }
        /// <summary>输出工作信息</summary>
        public static void ConsoleJob()
        {
            JobAgent jobAgent = new JobAgent();
            String[] sources = new String[] { "智联", "前程", "猎聘", "拉勾" };
            String[] places = new String[] { "北京", "上海", "杭州", "苏州" };
            String key = ".net";
            Int32 pageIndex = 1;
            //var list = jobAgent.GetJobBasicInfo(places[0], key, sources[2], pageIndex);
            //LaGouRequest lagouRequest = new LaGouRequest();
            var request = new LiePinRequest();
            var list = request.GetJobs(key, places[2]);
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("未查到");
            }
        }
        /// <summary>输出地图信息</summary>
        public static void ConsoleMap()
        {
            BaiduHelper baiduHelper = new BaiduHelper();
            var result = baiduHelper.GetPOI("阿里巴巴");
            if (result != null)
            {
                Console.WriteLine(result.ToString());
            }
            else
            {
                Console.WriteLine("未查到地址");
            }
        }

    }
    /// <summary>服务</summary>
    public class JobAgent : AgentServiceBase<JobAgent>
    {
        #region 属性
        /// <summary>线程数</summary>
        public override int ThreadCount
        {
            get
            {
                return 1;
            }
        }
        /// <summary>显示名</summary>
        public override string DisplayName
        {
            get
            {
                return "JobAgent";
            }
        }
        /// <summary>描述</summary>
        public override string Description { get { return "用于爬取招聘网站数据！"; } }
        /// <summary>省市列表</summary>
        String[] places = new String[] {
                                            "北京",
                                            "上海",
                                            "杭州",
                                            "苏州",
                                            "郑州"
                                        };
        /// <summary>职位列表</summary>
        String[] keys = new String[] {
                                        ".net",
                                        "java",
                                        "php",
                                        "会计"
                                      };
        /// <summary>来源列表</summary>
        String[] sources = new String[] {
                                            "拉勾网",
                                            "智联招聘",
                                            "前程无忧",
                                            "猎聘网"
                                        };
        #endregion

        #region 构造函数
        /// <summary>构造函数</summary>
        public JobAgent()
        {
            //一般在构造函数里面指定服务名
            ServiceName = "JobAgent";
        }

        #endregion

        #region 重写
        /// <summary>开始循环工作</summary>
        /// <param name="index">线程序号</param>
        public override void StartWork(Int32 index)
        {
            if (index < 0 || index >= ThreadCount) throw new ArgumentOutOfRangeException("index");

            // 可以通过设置任务的时间间隔小于0来关闭指定任务
            var ts = NewLife.Agent.Setting.Current.Intervals.SplitAsInt();
            Int32 time = ts[0];
            // 使用专用的时间间隔
            if (index < ts.Length) time = ts[index];
            if (time < 0) return;

            Thread t = new Thread(ThreadCallback);
            //String name = "XAgent_" + index;
            String name = "A" + index;
            if (ThreadNames != null && ThreadNames.Length > index && !String.IsNullOrEmpty(ThreadNames[index]))
                name = ThreadNames[index];
            t.Name = name;
            t.IsBackground = true;
            t.Priority = ThreadPriority.AboveNormal;
            t.Start(index);
        }

        /// <summary>线程回调</summary>
        /// <param name="data">线程序号</param>
        private void ThreadCallback(Object data)
        {
            Int32 index = (Int32)data;

            // 旧异常
            Exception oldEx = null;

            //while (true)
            //{
            Boolean isContinute = false;
            Active[index] = DateTime.Now;

            try
            {
                isContinute = Work(index);

                oldEx = null;
            }
            catch (ThreadAbortException) //线程被取消
            {
                WriteLine("线程" + index + "被取消！");
                //break;
            }
            catch (ThreadInterruptedException) //线程中断错误
            {
                WriteLine("线程" + index + "中断错误！");
                //break;
            }
            catch (Exception ex) //确保拦截了所有的异常，保证服务稳定运行
            {
                // 避免同样的异常信息连续出现，造成日志膨胀
                if (oldEx == null || oldEx.GetType() != ex.GetType() || oldEx.Message != ex.Message)
                {
                    oldEx = ex;

                    WriteLine(ex.ToString());
                }
            }
            Active[index] = DateTime.Now;

            //检查服务是否正在重启
            //if (IsShutdowning)
            //{
            //    WriteLine("服务准备重启，" + Thread.CurrentThread.Name + "退出！");
            //    break;
            //}

            //var ts = NewLife.Agent.Setting.Current.Intervals.SplitAsInt();
            //Int32 time = ts[0];
            ////使用专用的时间间隔
            //if (index < ts.Length) time = ts[index];

            ////如果有数据库连接错误，则将等待间隔放大十倍
            ////if (hasdberr) time *= 10;
            //if (oldEx != null) time *= 10;

            //    if (!isContinute) Thread.Sleep(time * 1000);
            //}
        }

        #endregion

        #region 核心

        /// <summary>核心工作方法。调度线程会定期调用该方法</summary>
        /// <param name="index">线程序号</param>
        /// <returns>是否立即开始下一步工作。某些任务能达到满负荷，线程可以不做等待</returns>
        public override bool Work(int index)
        {
            // XAgent将开启ThreadCount个线程，0<index<ThreadCount，本函数即为每个任务线程的主函数，间隔Interval循环调用
            WriteLine("任务{0}开始，当前时间：{1}", index, DateTime.Now);
            Int32 threadID = Thread.CurrentThread.ManagedThreadId;
            WriteLine("任务线程ID:{0}", threadID);
            //LaGouRequest request = new LaGouRequest();
            //request.GetJobsByProxy(".net", "北京");
            //LiePinRequest request = new LiePinRequest();
            //Console.WriteLine(d.address);
            //request.GetJobs(".net", "北京");

            //这里是执行核心
            //服务执行分三步，第一步拉取一批代理.第二步爬取职位信息.第三步根据爬取到的地址获取对应poi地址
            //可以把下面的注释全放开一次性全跑完，不过这样使用时间长，并且一出错，还需要重头跑。建议一个个注释，一个个跑。
            //先跑一批代理，使用拉勾网站测试代理ip是否可用
            LaGouRequest request = new LaGouRequest();
            request.TestProxy(".net", "北京");
            //Int32 count = 4;
            //for (int i = 0; i < count; i++)
            //{
            //    //爬取职位信息
            //    DownloadPosition(i);
            //}
            //处理公司信息，并根据地址获取poi坐标
            //ProcessCompany(index);
            WriteLine("任务{0}结束，当前时间：{1}", index, DateTime.Now);
            return false;
        }

        /// <summary>爬取职位信息</summary>
        /// <param name="index">线程序号</param>
        public void DownloadPosition(Int32 index)
        {
            //可以开四个线程跑，每个线程跑一个网站
            //网站请求接口
            IRequest request = null;
            //根据线程序号选择网站来源
            switch (index)
            {
                case 0:
                    request = new LaGouRequest();
                    break;
                case 1:
                    request = new ZhiLianRequest();
                    break;
                case 2:
                    request = new Job51Request();
                    break;
                case 3:
                    request = new LiePinRequest();
                    break;
            }
            foreach (var key in keys)
            {
                //每个城市
                foreach (var place in places)
                {
                    request.SaveAll(key, place);
                    WriteLine("爬取{0}网站{1}的{2}数据完成，当前时间：{3}", request.Name, place, key, DateTime.Now);
                }
            }

        }
        /// <summary>处理公司信息，并根据地址获取poi坐标</summary>
        /// <param name="index">线程序号</param>
        public void ProcessCompany(Int32 index)
        {
            foreach (var key in keys)
            {
                foreach (var city in places)
                {
                    Dictionary<String, Company> dic = PositionInfo.ConvertToCompany(key, city, sources[index]);
                    if (dic != null && dic.Count > 0)
                    {
                        BaiduHelper baiduHelper = new BaiduHelper();
                        foreach (var item in dic)
                        {
                            var result = baiduHelper.GetPOI(item.Value.Address ?? item.Value.Name, item.Value.City);
                            if (result != null)
                            {
                                item.Value.Lat = result.Location.Lat;
                                item.Value.Lng = result.Location.Lng;
                            }
                            item.Value.Save();
                        }
                    }
                }
            }
        }
        #endregion
    }
}
