<%@ Page Title="" Language="C#" MasterPageFile="~/JobPages/Master/Pages.Master" AutoEventWireup="true" CodeBehind="zhaopin.aspx.cs" Inherits="Job.UI.Pages.job.zhaopin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body, #top_div {
            background-color: #e7f6e9;
        }

        div.blockUI {
            /*这里是弹出遮罩的div*/
        }

        table.mytable {
            margin: 0px auto;
            border: 1px solid #0094ff;
            border-collapse: collapse;
        }

            table.mytable td {
                border: 1px solid #0094ff;
                padding: 3px;
            }

        table.mytableTh, table.mytableSelect {
            background-color: #0094ff;
            color: #ffffff;
            margin-top: 5px;
        }

            table.mytableTh th {
                background-color: #0094ff;
                color: #ffffff;
                border: 1px solid #fff;
                padding: 6px;
            }

        #inp_key {
            width: 300px;
            height: 30px;
            margin: auto;
        }

        #but {
            width: 80px;
            height: 30px;
        }

        .div_a_bottom {
            color: #fff;
            text-decoration: none;
        }

        .scrollUp {
            animation-name: myfirst;
            animation-duration: 0.5s;
            animation-timing-function: linear;
            animation-play-state: running;
            /*animation-fill-mode:forwards;*/
            -webkit-animation-name: myfirst;
            -webkit-animation-duration: 0.5s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-play-state: running;
            /*-webkit-animation-fill-mode:forwards;*/
        }

        @keyframes myfirst {
            0% {
                bottom: 50px;
            }

            100% {
                bottom: 800px;
            }
        }

        @-webkit-keyframes myfirst {
            0% {
                bottom: 50px;
            }

            100% {
                bottom: 800px;
            }
        }

        h3 {
            -webkit-margin-before: 0.3em;
            -webkit-margin-after: 0.3em;
            margin: 1px auto;
        }

        #mytabledata td {
            border: 1px solid #C9CEBC;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a name="top"></a>
    <div>
        <div>
            <div class="fixed_tableTh" style="display: none; position: fixed; top: 0px; left: 0px; width: 100%;">
                <table class="mytable mytableTh" style="width: 90%; margin-top: 0px">
                    <tr>
                        <th style="width: 4%;">标记</th>
                        <th style="width: 31%">招聘职位</th>
                        <th style="width: 27%">公司名称</th>
                        <th style="width: 10%">地址</th>
                        <th style="width: 8%">发布时间</th>
                        <th style="width: 5%">方式</th>
                        <th style="width: 8%">薪资</th>                        
                        <th style="width: 7%">来源</th>
                    </tr>
                </table>
            </div>
        </div>
        <div id="top_div">
            <div style="margin-top: 10px;">
            </div>
            <table class="mytable mytableSelect">
                <tr>
                    <td>
                        <input type="text" class="" name="" id="inp_key" subtag="sub" onkeyup="keyupsojob();" value=".net" /></td>
                    <td>
                        <select id="sel_city" subtag="sub">
                            <option value="北京">北京</option>
                            <option selected="selected" value="上海">上海</option>
                            <option value="广州">广州</option>
                            <option value="深圳">深圳</option>
                            <option value="天津">天津</option>
                            <option value="苏州">苏州</option>
                            <option value="重庆">重庆</option>
                            <option value="南京">南京</option>
                            <option value="杭州">杭州</option>
                            <option value="大连">大连</option>
                            <option value="成都">成都</option>
                            <option value="武汉">武汉</option>
                            <option value="长沙">长沙</option>
                            <option value="沈阳">沈阳</option>
                        </select>
                    </td>
                    <td>
                        <input type="button" value="重新搜索" id="but" onclick="sojob();" /></td>
                    <td>
                        <input type="checkbox" checked="checked" id="chk_zhilian" value="智联" subtag="sub" /><a href="http://www.zhaopin.com" target="_blank">智联</a>
                        <input type="checkbox" checked="checked" id="chk_liepin" value="猎聘" subtag="sub" /><a href="http://www.liepin.com" target="_blank">猎聘</a>
                        <input type="checkbox" checked="checked" id="chk_qiancheng" value="前程" subtag="sub" /><a href="http://51job.com" target="_blank">前程</a>
                        <input type="checkbox" checked="checked" id="chk_lashou" value="拉勾" subtag="sub" /><a href="http://www.lagou.com" target="_blank">拉勾</a>
                    </td>
                </tr>
            </table>
            <table class="mytable mytableTh" style="width: 91%; cursor: pointer">
                <tr>
                    <th style="width: 4%;">标记</th>
                    <th style="width: 31%">招聘职位</th>
                    <th style="width: 27%">公司名称</th>
                    <th style="width: 10%">地址</th>
                    <th style="width: 8%">发布时间</th>
                    <th style="width: 5%">方式</th>
                    <th style="width: 8%">薪资</th>                    
                    <th style="width: 7%">来源</th>
                </tr>
            </table>
        </div>
        <div class="div_mytabledata">
            <table id="mytabledata" class="mytabledata mytable" style="width: 91%">
            </table>
        </div>
        <div id="div_bottom" style="position: fixed; bottom: 0px; left: 0px; width: 100%;">
            <div style="/*height: 5px; */ background-color: #e7f6e9; text-align: right;">
                <span style="color: red; font-size: 12px; padding-right: 20px; display: none">更新(2015.04.26)：<span style="color: #0094ff">双击行看看有什么变化~~</span></span>
            </div>
            <div style="width: 100%; background-color: #0094ff; margin: 0 auto; padding: 5px">
                <table style="margin: 0 auto; width: 40%; color: #fff">
                    <tr>
                        <td><a class="div_a_bottom" href="http://www.cnblogs.com/zhaopei/p/4368417.html" target="_blank">MyBlog</a></td>
                        <td><a class="div_a_bottom" href="Messg.html" target="_blank">建议留言</a></td>
                        <td><a class="div_a_bottom" href="#">关于</a></td>
                    </tr>
                </table>
            </div>
        </div>

        <div id="scrollUp" title="回到顶部" style="position: fixed; bottom: 50px; right: 15px; display: none; background: url(../../JobResources/imgs/top.png)  no-repeat scroll 0px 0px transparent; width: 36px; height: 65px"
            onmouseout="topdiv_mouseout(this)"
            onmouseover="topdiv_ouseover(this)"
            onclick="topdiv_click(this)">
        </div>

    </div>
    <script type="text/javascript">
        if (window.location.href == "http://www.zhaopei.shaxtv.com/jobpages/job/zhaopin.aspx") {
            alert("改域名啦~~请保存新的地址~~");
            window.location.href = "http://www.haojima.net/jobpages/job/zhaopin.aspx";
        }
    </script>
    <script src="../../JobResources/script/common/jquery.blockUI.js"></script>
    <script type="text/javascript">
        var index = 0;
        //双击选中的颜色
        var selectOK = "#EDBFF7",
            //鼠标停留在上面时的颜色
            selecting = "#C9F0E8";
        //重新搜索
        function sojob() {
            index = 0;
            GetZhaoPinInfo(true);
            window.scrollTo(0, 0);
        }
        function keyupsojob() {
            if (event.keyCode == 13)
                sojob();
        }
        //页面加载完成~~
        $(function () {
            //
            index = 0;
            GetZhaoPinInfo(true);
            //滚动条事件
            window.onscroll = function () {
                if ($.getScrollbheight() <= 0) {
                    GetZhaoPinInfo();
                }
                var scrollTop = $.getScrollTop();
                if (scrollTop >= 50) {
                    $(".fixed_tableTh").show();
                    $("#scrollUp").slideDown();
                }
                else {
                    $(".fixed_tableTh").hide();
                    // $("#scrollUp").hide();
                }
                if (scrollTop <= 0) $("#scrollUp").hide();
            }
        });

        function topdiv_mouseout(obj) {
            $(obj).css("background", "url(../../JobResources/imgs/top.png)  no-repeat scroll 0px -65px transparent");
        }
        function topdiv_ouseover(obj) {
            $(obj).css("background", "url(../../JobResources/imgs/top.png)  no-repeat scroll 0px 0px transparent");
        }
        function topdiv_click() {
            $("#scrollUp").removeClass().addClass("scrollUp");
            var set = setTimeout(function () {
                $("#scrollUp").removeClass();
                $("#scrollUp").hide();
            }, 500)

            if (document.documentElement && document.documentElement.scrollTop) {
                document.documentElement.scrollTop = 1;
            } else if (document.body) {
                document.body.scrollTop = 1;
            }
        }


        //当鼠标移动到某对象范围的上方时触发的事件
        function img_over(obj) {
            $(obj).attr('src', '../../JobResources/imgs/星星ok.ico');
        }

        //当鼠标离开某对象范围时触发的事件
        function img_out(obj) {
            if ($(obj).attr('isClick') == "false")
                $(obj).attr('src', '../../JobResources/imgs/星星no.ico');
            else
                $(obj).attr('src', '../../JobResources/imgs/星星ok.ico');
        }
        //鼠标上的按钮被按下了
        function img_down(obj) {
            if ($(obj).attr('isClick') == "false") {
                $(obj).attr('isClick', 'true');
                $(obj).parents("tr").addClass("isDbClickTag");//  .attr("isDbClick", "true");
                $(obj).parents("tr").css("background-color", selectOK);
                $(obj).attr('src', '../../JobResources/imgs/星星ok.ico');
            }
            else {
                $(obj).attr('isClick', 'false');
                $(obj).parents("tr").removeClass("isDbClickTag"); //.attr("isDbClick", "false");
                $(obj).parents("tr").css("background-color", selecting);
                $(obj).attr('src', '../../JobResources/imgs/星星no.ico');
            }
        }

        //进来
        function tr_over(obj) {
            $(obj).css("background-color", selecting);
        }
        //出去
        function tr_out(obj) {
            var mythis = $(obj);
            if (mythis.hasClass("isDbClickTag"))//如果标记的 
                mythis.css("background-color", selectOK);
            else if (mythis.hasClass("selectTr"))//当前选中的
                mythis.css("background-color", "#7fcc80");
            else if (mythis.hasClass("dblclickTr")) //如果点击过的
                mythis.css("background-color", "#D8E3C6");
            else
                mythis.css("background-color", "#e7f6e9");//默认颜色   
        }
        //双击行
        function tr_dblclick(obj) {
            var isdistr = false;
            if ($(obj).next(".dis_tr").css("display") == "none")
                isdistr = true;
            var tr_siblings = $(obj).siblings();
            tr_siblings.filter(".dis_tr").css("display", "none");//(隐藏所有详细信息)filter过滤   

            var tr_selectTr = tr_siblings.filter(".selectTr");
            if (tr_selectTr.hasClass("isDbClickTag"))//当前选中行 是否 标记过
                tr_selectTr.css("background-color", selectOK);//如果标记的 
            else
                tr_selectTr.css("background-color", "#D8E3C6");//设置当前选中行 为 点击过的颜色      

            tr_siblings.removeClass("selectTr");//删除所有兄弟节点 的 class  “selectTr”

            $(obj).addClass("selectTr dblclickTr").css("background-color", "#7fcc80");//设置点击行class 和 颜色
            var tr = "<tr class='dis_tr'><td colspan='9' style='text-align:center;'>正在加载...</td></tr>";
            if ($(obj).next(".dis_tr").length) {
                if (isdistr) {
                    $(obj).next(".dis_tr").css("display", "");
                    $(obj).focus();
                }
                else {
                    $(obj).next(".dis_tr").css("display", "none");
                }
            }
            else {
                $(obj).after(tr);
                var mydata = new MyDataPack("zhaopinPrcoess.ashx", "GetZhaoPinDetailsInfo");
                var mobj = new Object();
                mobj.url = encodeURIComponent($($(obj).find("td")[1]).find("a").attr("href"));
                mobj.type = $($(obj).find("td")[7]).text();
                mydata.addValue("obj", mobj)
                $.ajax({
                    type: "post",
                    dataType: 'json',
                    url: mydata.getUrl(),
                    data: mydata.getJsonData(),
                    beforeSend: function (XMLHttpRequest) {
                    },
                    complete: function (jqXHR, status, responseText) {
                    },
                    success: function (data) {
                        if (mydata.ShowAjaxResult(data)) {
                            $(obj).next(".dis_tr").find("td").html(data);
                            $(obj).next(".dis_tr").find("td").css("font-size", "12px");
                            $(obj).focus();
                        }
                    },
                    error: function (msg) {
                        alert(msg.statusText);
                    }
                });
            }
        }

        function GetZhaoPinInfo(isReload) {
            index++;
            var mydata = new MyDataPack("zhaopinPrcoess.ashx", "GetZhaoPinInfo");
            mydata.getValueSetData("sub");
            var obj = new Object();
            obj.index = index;
            mydata.addValue("obj", obj)
            $.ajax({
                type: "post",
                dataType: 'json',
                url: mydata.getUrl(),
                data: mydata.getJsonData(),
                beforeSend: function (XMLHttpRequest) {
                    $.blockUI({ type: 2 });
                },
                complete: function (jqXHR, status, responseText) {
                    $.unblockUI();
                },
                success: function (data) {
                    if (mydata.ShowAjaxResult(data)) {
                        var tr_html = "";
                        for (var i = 0; i < data.length; i++) {                        
                            tr_html += "<tr style='cursor:pointer' isDbClick='false' onmouseover='tr_over(this);' onmouseout='tr_out(this);' onclick='tr_dblclick(this);'>"; //双击改成单击 ondblclick -》onclick
                            tr_html += "<td style='width:4%;text-align:center'><img isClick='false' class='img_xin'  onmouseover='img_over(this);' onmouseout='img_out(this);' onmousedown='img_down(this);' src='../../JobResources/imgs/星星no.ico' /></td>";
                            tr_html += "<td style='width:31%'><a href='" + data[i].info_url + "' target='_blank' style='text-decoration:none;color:#890D18'>" + data[i].titleName + "</a></td>";
                            tr_html += "<td style='width:27%'>" + data[i].company + "</td>";
                            tr_html += "<td style='width:10%'>" + data[i].city + "</td>";
                            tr_html += "<td style='width:8%'>" + data[i].date + "</td>";
                            tr_html += "<td style='width:5%'>" + data[i].salary + "</td>";
                            tr_html += "<td style='width:8%'>" + data[i].salary_em + "</td>";
                            //tr_html += "<td style='width:5%'><a href='" + data[i].info_url + "' target='_blank'>详情</a></td>";
                            tr_html += "<td style='width:7%'>" + data[i].source + "</td>";
                            tr_html += "</tr>";
                        }
                        if (isReload)
                            $(".mytabledata").html(tr_html);
                        else
                            $(".mytabledata").append(tr_html);
                    }
                },
                error: function (msg) {
                    debugger;
                    alert(msg.statusText);
                }
            });
        }

    </script>
</asp:Content>

