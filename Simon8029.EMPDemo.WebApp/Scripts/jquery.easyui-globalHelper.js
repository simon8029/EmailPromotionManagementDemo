(function ($) {
    //向jquery对象上 扩展 一个 globalHelper 属性
    $.extend($, {
        globalHelper: {
            showCommonWindow: function (title, url, width, height) {

                var widthEnd = (width && typeof (width) == "number") ? width : 1200;
                var heightEnd = (height && typeof (height) == "number") ? height : 550;

                window.top.$("#commonWindow")
                    .window({ "width": widthEnd, "height": heightEnd, "title": title })
                    .window("center")
                    .window("open")
                    .children("iframe")
                    .attr("src", url);

                window.top.$.messager.progress({ text: "Loading..." });
            },
            closeCommonWindow: function () {
                window.top.$("#commonWindow").window("close").children("iframe");
                window.top.$.messager.progress("close");
            },
            //刷新管理首页的 当前选中选项卡里的 iframe里的datagrid
            reloadSeletecTabDataGrid: function () {
                window.top.$("#tabBox") //获取父页面的 选项卡容器
                    .tabs("getSelected") //获取 当前选中的选项卡
                    .children("iframe")[0] //获取 选项卡里的 iframe
                    .contentWindow //获取iframe里的window对象
                    .$("#tbList") //获取iframe里面页面的 表格组件
                    .datagrid("reload"); //调用 表格组件的 刷新方法，重新请求 当前页的表格数据
            },
            //公用的datagrid组件参数
            datagridPara: {
                paras: {
                    onLoadSuccess: function (jsonData) {
                        //如果返回的消息 对象 包含 Status属性，说明是返回的 json消息，那么就调用 msgProcess方法 统一处理
                        //                   不包含Status属性，说明 返回的是 节点json数据
                        if (jsonData.Status) {
                            $.messageProcess(jsonData);
                        }
                    },
                    rownumbers: true,
                    fitColumns: true,
                    pagination: true, //启用分页
                    pageList: [10, 20, 30], //页容量组
                    pageSize: 10, //初始页容量，注意 值必须和 pageList里的某个值一致
                    singleSelect: true,
                    url: "",
                    toolbar: null,
                    columns: null
                },
                init: function (url, toolbar, columns) {
                    this.paras.url = url;
                    this.paras.toolbar = toolbar;
                    this.paras.columns = columns;
                }
            },
            //将序列化成json格式后日期(毫秒数)转成日期格式
            changeDateFormat: function (cellval) {
                var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
                var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
                var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
                return date.getFullYear() + "-" + month + "-" + currentDate;
            }
        }
    });
})(window.$);