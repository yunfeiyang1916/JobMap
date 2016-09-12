/// <reference path="../jquery-1.7.1.js" /> 

//自执行函数
(function ($) {
    $.extend({
        //将json对象转换成字符串   [貌似jquery没有自带的这种方法]
        toJSONString: function (object) {
            if (object == null)
                return;
            var type = typeof object;
            if ('object' == type) {
                if (Array == object.constructor) type = 'array';
                else if (RegExp == object.constructor) type = 'regexp';
                else type = 'object';
            }
            switch (type) {
                case 'undefined':
                case 'unknown':
                    return;
                    break;
                case 'function':
                case 'boolean':
                case 'regexp':
                    return object.toString();
                    break;
                case 'number':
                    return isFinite(object) ? object.toString() : 'null';
                    break;
                case 'string':
                    return '"' + object.replace(/(\\|\")/g, "\\$1").replace(/\n|\r|\t/g, function () {
                        var a = arguments[0];
                        return (a == '\n') ? '\\n' : (a == '\r') ? '\\r' : (a == '\t') ? '\\t' : ""
                    }) + '"';
                    break;
                case 'object':
                    if (object === null) return 'null';
                    var results = [];
                    for (var property in object) {
                        var value = $.toJSONString(object[property]);
                        if (value !== undefined) results.push($.toJSONString(property) + ':' + value);
                    }
                    return '{' + results.join(',') + '}';
                    break;
                case 'array':
                    var results = [];
                    for (var i = 0; i < object.length; i++) {
                        var value = $.toJSONString(object[i]);
                        if (value !== undefined) results.push(value);
                    }
                    return '[' + results.join(',') + ']';
                    break;
            }
        },
        //获取url 参数
        getUrlVars: function () {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        },
        //获取url 参数
        getUrlVar: function (name) {
            return $.getUrlVars()[name];
        },
        //取窗口可视范围的高度[浏览器可见区域高度]
        getClientHeight: function () {
            var clientHeight = 0;
            if (document.body.clientHeight && document.documentElement.clientHeight) {
                var clientHeight = (document.body.clientHeight < document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
            } else {
                var clientHeight = (document.body.clientHeight > document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
            }
            return clientHeight;
        },
        //取窗口滚动条高度[滚动条距离顶部的高度]
        getScrollTop: function () {
            var scrollTop = 0;
            if (document.documentElement && document.documentElement.scrollTop) {
                scrollTop = document.documentElement.scrollTop;
            } else if (document.body) {
                scrollTop = document.body.scrollTop;
            }
            return scrollTop;
        },
        //取文档内容实际高度
        getScrollHeight: function () {
            return Math.max(document.body.scrollHeight, document.documentElement.scrollHeight);
        },
        //滚动条距离底部的高度
        getScrollbheight: function () {
            return this.getScrollHeight() - this.getScrollTop() - this.getClientHeight();
        }
    });
}(jQuery))






