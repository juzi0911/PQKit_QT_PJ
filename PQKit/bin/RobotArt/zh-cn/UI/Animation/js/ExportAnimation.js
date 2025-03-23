
function GetUrlParms() {
    var args = new Object();
    var query = location.search.substring(1); //获取查询串
    var pairs = query.split("&"); //在逗号处断开
    for (var i = 0; i < pairs.length; i++) {
        var pos = pairs[i].indexOf('='); //查找name=value
        if (pos == -1) continue; //如果没有找到就跳过
        var argname = pairs[i].substring(0, pos); //提取name
        var value = pairs[i].substring(pos + 1); //提取value
        args[argname] = decodeURI(value); //存为属性
    }
    return args;
}
$(function () {
    var args = GetUrlParms();
    if(args["coverImg"]!="undefind")
    {
        $("#imgCover").attr("src",args["coverImg"]);
    }
    if(args["qrCodeImg"]!="undefind")
    {
        $("#imgQrCode").attr("src",args["qrCodeImg"]);
    }
   
});
function CheckIsValidCharacter(key) {
    var _regCharacter = /&|'|"|=|;|>|<|%/i;
    if (_regCharacter.test(key)) {
        return false;
    }
    return true;
}

