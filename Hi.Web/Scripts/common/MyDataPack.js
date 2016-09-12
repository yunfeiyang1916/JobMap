/// <reference path="../jquery-1.7.1.min.js" />


var DataClass = {
    create: function () {
        return function () {
            this.MyInit.apply(this, arguments);//创建对象的构造函数  //arguments 参数集合  系统名称 不能写错
        }
    }
}

var MyDataPack = DataClass.create();
MyDataPack.prototype = {
    //初始化
    MyInit: function (url, operation, params) {

        this.data = new Object();   //所有数据容量

        var bdata = new Object();
        bdata.url = url;            //地址
        bdata.operation = operation;//操作
        bdata.params = params;      //参数

        this.data.BasicData = bdata; //基本数据
    },
    //添加数据 如：addValue("obj", obj对象);
    addValue: function (p, obj) {
        this.data[p] = obj;
    },
    //取得 所有标记控件的值 并写入数据
    getValueSetData: function (togName) {
        var values = Object(); //值的集合
        $("[subtag='" + togName + "']").each(function () {            
            //如果是input 类型 控件
            if (this.tagName == "INPUT") {
                //如果是text 控件
                if (this.type == "text" || this.type == "hidden") {
                    values[this.id] = this.value;
                }
                else if (this.type == "checkbox") {                    
                    values[this.id] = this.checked;
                }
                else if (this.type == "...") {

                }
                //......
            }
            else if (this.tagName == "SELECT") {
                values[this.id] = this.value;
            }
            else if (this.tagName == "...") {

            }
            //................
        });
        this.data[togName] = values;//添加到数据集合
    },
    //取值 如：getValue("BasicData")
    getValue: function (p) {
        return this.data[p];
    },
    //获取或设置url
    getUrl: function (url) {
        if (url)
            this.data.BasicData["url"] = url;
        else
            return this.data.BasicData["url"];
    }
    ,
    //取值 转成字符串的对象 数据
    getJsonData: function () {
        return $.toJSONString(this.data);
    },
    //弹出消息 如果有跳转页面 页面跳转
    ShowAjaxResult: function (obj) {
        //异常 或 session超时
        if (obj.State == "0") {
            if (obj.Messg)
                alert(obj.Messg);//这里暂时alert一下  因为要跳转需要用户确定
            //  mymsg.showMsgErr(obj.Messg);
            if (!$("#a_err_href").length)
                $("body").append("<a id='a_err_href' target='_blank'></a>");
            $("#a_err_href").attr("href", "http://" + location.host + obj.JSurl);
            document.getElementById("a_err_href").click();
            //location.href = obj.JSurl;            
            return false;
        }
            //成功
        else if (obj.State == "1") {
            if (obj.Messg)
                mymsg.showMsgOk(obj.Messg);
            return true;
        }
            //失败
        else if (obj.State == "2") {
            if (obj.Messg)
                mymsg.showMsgErr(obj.Messg);
            return false;
        }
            //正常重定向
        else if (obj.State == "3") {
            if (obj.Messg)
                mymsg.showMsgErr(obj.Messg);
            location.href = obj.JSurl;
            return true;
        }
        else return true;
    }
}