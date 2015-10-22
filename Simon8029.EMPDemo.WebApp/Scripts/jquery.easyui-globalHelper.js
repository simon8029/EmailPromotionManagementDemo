(function ($) {
    
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
                    .attr("src", url)
                ;

                window.top.$.messager.progress({ text: "Loading..." });
            },
            closeCommonWindow: function () {
                window.top.$("#commonWindow").window("close").children("iframe");
                window.top.$.messager.progress("close");
            },
            // Refresh datagrid
            reloadSeletecTabDataGrid: function () {
                window.top.$("#tabBox") // get parent tab container
                    .tabs("getSelected") //get current tab container
                    .children("iframe")[0] // get iframe 
                    .contentWindow // get window object
                    .$("#tbList") //get tblist
                    .datagrid("reload"); //reload datagrid
            },
            //common datagrid parameter
            datagridPara: {
                paras: {
                    onLoadSuccess: function (jsonData) {
                       
                        if (jsonData.Status) {
                            $.messageProcess(jsonData);
                        }
                    },
                    rownumbers: true,
                    fitColumns: true,
                    pagination: true, 
                    pageList: [10, 20, 30], 
                    pageSize: 10, 
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
            // convert json data format
            changeDateFormat: function (cellval) {
                if (cellval != null) {
                    var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
                    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
                    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
                    return date.getFullYear() + "-" + month + "-" + currentDate;
                } else {
                    return "";
                }
            }
        }
    });
})(window.$);