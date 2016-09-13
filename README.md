# JobMap
职位分布图
##效果图
![github-01.jpg](/效果图/职位分布图.png "职位分布图.png")

1、环境配置，分别需要在Hi.Web/Web.config与Job.Agent/App.config配置百度地图浏览器端（BaiduJsAk）与服务器端访问密钥BaiduAk

2、服务执行分三步，第一步拉取一批代理.第二步爬取职位信息.第三步根据爬取到的地址获取对应poi地址
##### 示例代码
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
3、执行JobController下的CreateGeoTable用来在百度地图云平台创建云存储表。
#### 效果图
![github-01.jpg](/效果图/百度地图云存储数据表.png "百度地图云存储数据表.png")

4、将第三步创建完云存储表的表id替换到Map.cshtml中
#### 示例代码
        //来源于云数据库映射
        var sourceGeotableIds = {
            //拉勾网
            lagou: { net: 150268, java: 151369, php: 151594, accountant: 151595 },
            //智联招聘
            zhilian: { net: 149937, java: 151604, php: 151605, accountant: 151606 },
            //前程无忧
            job51: { net: 150263, java: 151607, php: 151608, accountant: 151609 },
            //猎聘网
            liepin: { net: 150266, java: 151610, php: 151612, accountant: 151612 }
        };
#### 引用说明   
   爬虫与前端借鉴博客：http://www.cnblogs.com/zhaopei/p/job_hunting.html
   
   代理爬取借鉴博客：http://www.cnblogs.com/Lands-ljk/p/5673017.html
