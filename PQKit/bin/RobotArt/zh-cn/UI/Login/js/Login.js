
function GetUrlParms() {
    var args = new Object();
    var query = location.search.substring(1); //获取查询串
    var pairs = query.split("&");
    for (var i = 0; i < pairs.length; i++) {
        var pos = pairs[i].indexOf('=');
        if (pos == -1) continue;
        var argname = pairs[i].substring(0, pos);
        var value = pairs[i].substring(pos + 1);
        args[argname] = unescape(value);
    }
    return args;
}

$(function () {

    var args = GetUrlParms();
    if (args["LoginPage"] == "0") {
        $('.fatherPosition').show();
        $('.logintu').css({ 'display': 'none' });
    }
    else if (args["LoginPage"] == "1") {

        $('.fatherPosition').hide();
        $('.logintu').css({ 'display': 'block' });
    }

    window.LoginType = args["LoginType"];

    var iFrameSrc = "http://www.pq1959.com/account/weixinlogin?type=1&isbig=false";
    if (args["ServiceType"] == "1") {
        iFrameSrc = "http://www.prepro.pq1959.com/account/weixinlogin?type=1&isbig=false";
    }
    $("#login_wechat0").attr("src", iFrameSrc);

    $("#Phone_Number").blur(function () {
        ValidQuickLogin(0);
    });
    $("#Email_Number").blur(function () {
        ValidQuickLogin(4);
    });

    $("#Ver_Code").blur(function () {
        var loginType = parseInt(window.LoginType);

        ValidQuickLogin(loginType);
        if (window.ErrorMsg == "") {
            ValidQuickLogin(1);
        }

    });

    $("#Login_Name").blur(function () {
        //alert("Login_Name");
        ValidQuickLogin(2);
    });

    $("#Login_Pwd").blur(function () {
        if (window.ErrorMsg == "") {
            ValidQuickLogin(3);
        }

    });

});

$(document).on('keyup', function (e) {
    if (e.keyCode === 13) {
        if (window.LoginType != "3" && 
        window.LoginType != "undefined" &&
        typeof(window.LoginType) != "undefined") 
        {
            Login();
        }
    }
});

window.errorMsgPre = "<img src=\"images/error.png\">";

function ValidQuickLogin(type) {
    var reg;
    window.ErrorMsg = "";

    var phoneNumber = "";
    if (type == 0) {
        phoneNumber = $.trim($("input[name='PhoneNumber']").val());
        if (phoneNumber == "") {
            window.ErrorMsg = "请输入您的手机号";
        }
        else {
            if (phoneNumber.length != 11) {
                window.ErrorMsg = "请输入11位手机号码";
            }
            else {
                reg = /^[1]+[3,4,5,6,7,8,9]+\d{9}$/;
                if (!reg.test(phoneNumber))
                    window.ErrorMsg = "请输入正确的手机号码";
            }
        }

        window.PhoneNumber = phoneNumber;
    }

    if (type == 1) {
        var validCode = $.trim($("#Ver_Code").val());
        if (validCode == "") {
            window.ErrorMsg = "请输入验证码";
        }
    }

    if (type == 2) {
        var Account = document.getElementById("Login_Name").value;
        if (Account == "") {
            window.ErrorMsg = "请输入用户名";
        }

    }
    if (type == 3) {
        var PassWord = document.getElementById("Login_Pwd").value;
        if (PassWord == "") {
            window.ErrorMsg = "请输入密码";
        }
    }
    if (type == 4) {
        var email = $.trim($("input[name='EmailNumber']").val());
        if (email == "") {
            window.ErrorMsg = "请输入您的邮箱";
        }
        else {
            var emailReg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-|_)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
            if (!emailReg.test(email)) {
                window.ErrorMsg = "邮箱格式不正确";
            }

        }

        window.PhoneNumber = email;
    }
    if (window.ErrorMsg != "") {
        OperErrorMessage(1);
    }


}

function OperErrorMessage(type) {
    if (type == 1) {
        var msg = window.errorMsgPre;
        msg += window.ErrorMsg;
        $("#errorMessage").html(msg);
        $("#errorMessage").show();
    }
    else if (type == 0) {
        $("#errorMessage").hide();
    }

}

function jsUpdateLoginResult(strMsg) {
    $("#errorMessage").html(strMsg);
    $("#errorMessage").show();
    var feature = $(".but").attr("feature");
    if (feature == "4") {
        window.LoginType = "4";
    }
}

function SendVerCode(obj) {

    var state = $(obj).attr("state");
    if (state == "no")
        return;
    var feature = $(obj).attr("feature");
    feature = typeof (feature) == "undefined" || feature == null || feature == "" ? 0 : parseInt(feature);
    ValidQuickLogin(feature);
    if (window.ErrorMsg != "") {
        return;
    }

    //调C++方法，webservice发验证码
    var returnCode = "";
    returnCode = window.OnClickedSendVerCode(window.PhoneNumber)

    if (returnCode == "1") {
        VerCodeCountDown(obj);
    }
}

function updateUsetInputInfo() {

    var reRow = {};
    if (window.LoginType == "0" || window.LoginType == "4") {
        reRow.PhoneNumber = window.PhoneNumber;
        var phoneCode = "";
        phoneCode = $.trim($("#Ver_Code").val());
        reRow.PhoneCode = phoneCode;
    }

    if (window.LoginType == "1") {
        var Account = "";
        var PassWord = "";
        Account = document.getElementById("Login_Name").value;
        PassWord = document.getElementById("Login_Pwd").value;
        reRow.UserName = Account;
        reRow.UserPwd = PassWord;

        var remberPwd = "false";
        var boxRemberPwd = document.getElementById("Login_RemberPwd");
        if (boxRemberPwd.checked) {
            remberPwd = "true";
        }

        var autoLogin = "false";
        var boxAutoLogin = document.getElementById("Login_AutoLogin");
        if (boxAutoLogin.checked) {
            autoLogin = "true";
        }
        reRow.RemberPwd = remberPwd;
        reRow.AutoLogin = autoLogin;
    }

    reRow.LoginType = window.LoginType;
    var jsonStr = JSON.stringify(reRow);
    //alert(jsonStr);
    return jsonStr;
}

function Login() {

    if (window.LoginType == "0") {
        ValidQuickLogin(0);
        if (window.ErrorMsg != "") {
            return;
        }
        ValidQuickLogin(1);
        if (window.ErrorMsg != "") {
            return;
        }
    }

    if (window.LoginType == "1") {
        ValidQuickLogin(2);
        if (window.ErrorMsg == "") {
            ValidQuickLogin(3);
        }
        if (window.ErrorMsg != "") {
            return;
        }
    }
    if (window.LoginType == "4") {
        ValidQuickLogin(4);
        if (window.ErrorMsg != "") {
            return;
        }
        ValidQuickLogin(1);
        if (window.ErrorMsg != "") {
            return;
        }
        window.LoginType = "0";
    }

    $('.fatherPosition').hide();

    var userInputInfo = "";
    userInputInfo = updateUsetInputInfo();
    window.OnClickedLogin(userInputInfo);

    $('.logintu').css({ 'display': 'flex' });
}


//clear user info
function ClearData() {

    $("input[name='Login_Name']").val("");
    $("input[name='Login_Pwd']").val("");
    $("input[name='Login_RemberPwd']").removeAttr("checked");
    $("input[name='Login_AutoLogin']").removeAttr("checked");
    window.OnBnClickedClearUserInfo();
}

function jsReturnToHome(strValue) {
    // alert(strValue);
    window.location.href = strValue;
}

function Closedlg() {
    window.close();
}

function jsDisplayProgress(strValue) {
    if (strValue == "TRUE") {
        $('.fatherPosition').hide();
        //$("#welTitle").html(window.greetings);
        $('.logintu').css({ 'display': 'flex' });
    }
    else {
        $('.fatherPosition').show();
        $('.logintu').css({ 'display': 'none' });

    }

}

function IsAutoLoginType(typeValue) {
    window.LoginType = typeValue;

    var AutoLoginType = "";
    var box = document.getElementById("AutoLogin_Type");
    if (box.checked) {
        AutoLoginType = "true";
    }
    else {
        AutoLoginType = "false";
    }

    var returnText = "";
    returnText = typeValue;
    returnText = returnText + "&";
    returnText = returnText + AutoLoginType;

    return returnText;
}

function accountLogin() {
    var autoLoginType = "";
    autoLoginType = IsAutoLoginType("1");
    window.setLoginType(autoLoginType);
}

function phoneLogin() {
    var autoLoginType = "";
    autoLoginType = IsAutoLoginType("0");
    window.setLoginType(autoLoginType);
}
function emailLogin() {
    var autoLoginType = "";
    autoLoginType = IsAutoLoginType("4");
    window.setLoginType(autoLoginType);
}

function wechatLogin() {
    var autoLoginType = "";
    autoLoginType = IsAutoLoginType("3");
    window.setLoginType(autoLoginType);
    window.OnClickedWeChat();
}

var setTime;
var time = 120;
function VerCodeCountDown(obj) {

    $(obj).attr("state", "no");
    $(obj).css("background", "#aaa");

    setTime = setInterval(function () {

        if (time <= 0) {
            clearInterval(setTime);
            $(obj).text("获取验证码");
            $(obj).css("background", "#349DFF");
            $(obj).attr("state", "yes");
            time = 120;
            return;
        }
        time--;

        $(obj).text(time + "s后重新获取");


    }, 1000);

}










