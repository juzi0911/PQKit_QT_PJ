// JavaScript Document
// create by zcy 2016/10/10

var context;
var canvasC;
var rotataRadians;
var outerColorStart;
var outerColorMid;
var outerColorEnd;
var circleBottomColor;
var innerColorStart;
var innerColorEnd;
var percent;
var ra;


var R0, R1;
var posX, PosY;
var lineW0, lineW1;
var startA, startAa;
var ProcessPosX, ProcessPosY;

window.exportEndUrl = "";

function InitScreen(settings) {
    var defaultSetting = {
        url: 'images/fire.png',   //飞机小图地址
        obj: '',                  //场景添加的canvas id 名
        percent: 1,               //圆环转动的百分比  0-1 
        circleBottomColor: "#f2f2f2",//圆环底色
        outerColorStart: '#fffaf7',  //外部圆环 渐变色
        outerColorMid: '#fff4ec',
        outerColorEnd: '#ffd2b0',
        innerColorStart: '#ffdd00',  //内部圆环 渐变色
        innerColorEnd: '#fc7d37'
    };
    var option = $.extend({}, defaultSetting, settings);

    var imageUrl = option.url;
    var obj = option.obj;
    percent = option.percent;
    outerColorStart = option.outerColorStart;
    outerColorMid = option.outerColorMid;
    outerColorEnd = option.outerColorEnd;
    innerColorStart = option.innerColorStart;
    innerColorEnd = option.innerColorEnd;
    circleBottomColor = option.circleBottomColor;

    var preA = Math.PI / 180;

    canvasC = document.getElementById(obj);
    /*控制运动*/
    context = canvasC.getContext('2d');
    var windowW = parseInt($(canvasC).parent().width());
    lineW1 = 18;
    lineW0 = 5;
    
    ProcessPosX = 218;
    ProcessPosY = 360;

    if (windowW > 500) {
        lineW1 = 36;
        lineW0 = 10;
    }
    var canvasW = windowW * 0.76;
    R = parseInt(canvasW / 2 - 2 * lineW1 - 2 * lineW0 - 10);
    R0 = parseInt(canvasW / 2 - lineW0 - 4);
    R1 = parseInt(canvasW / 2 - lineW1 - lineW0 - 10);
    ra = parseInt(canvasW / 2 - lineW0 / 2 - 5);
    var canvasH = canvasW;
    var rotateAngle = percent * 360;
    var anotherA = 0;
    if (percent > 0.5) {
        anotherA = (percent - 0.5) * 360 * preA;
    }
    rotataRadians = preA * rotateAngle;
    canvasC.width = canvasW;
    canvasC.height = canvasH;

    posX = canvasC.width / 2;
    posY = canvasC.height / 2;
    startAa = -Math.PI / 2;
    startA = 0;
    
    var preSceond = 100 / (Math.PI * 2)
    var imageAir = new Image();
    imageAir.onload = CanvasApp;
    imageAir.src = imageUrl;

    //-imageAir.width/4-2,-imageAir.height/4,imageAir.width/2,imageAir.height/2
    function CanvasApp() {
        var loading = 0;
        canvasC.setAttribute("data-run", "1")
        var imgWidth = option.imgWidth || imageAir.width / 2;
        var imgHeight = option.imgHeight || imageAir.height / 2;
        console.log(imgWidth)
        var imgX = -imgWidth / 2 - 3;
        var imgY = -imgHeight / 2 - 6;

        drawScreen(0);

    }
}

function drawScreen(pos) {

    if (startA < rotataRadians) {
        startA = (rotataRadians * (pos)) / 100;
    }
   
    context.fillStyle = "#ffffff";
    context.fillRect(0, 0, canvasC.width, canvasC.height);
    //
    context.save();
    context.setTransform(1, 0, 0, 1, 0, 0);
    context.fillStyle = "#ffffff";
    context.fillRect(0, 0, canvasC.width, canvasC.height);
    context.translate(posX, posY);
    context.rotate(-Math.PI / 2);
    //外环
    context.beginPath();
   
    var gradient1 = context.createLinearGradient(R1, 0, -R1, 0);
    gradient1.addColorStop(0, outerColorStart);
    gradient1.addColorStop(0.01, outerColorMid);
    gradient1.addColorStop(1, outerColorEnd);
    context.strokeStyle = gradient1;
    context.lineWidth = lineW0;
    context.arc(0, 0, R0, 0, startA, false);
    context.stroke();
    context.closePath();
    //中环
    context.beginPath();
    context.strokeStyle = circleBottomColor;
    context.lineWidth = lineW1;
    context.arc(0, 0, R1, 0, Math.PI * 2, false);
    context.stroke();
    context.closePath();
    context.beginPath();
    // Linear gradients
    var gradient2 = context.createLinearGradient(R1, 0, -R1, 0);
    gradient2.addColorStop(0, innerColorStart);
    gradient2.addColorStop(1, innerColorEnd);
    context.lineCap = "round";
    context.strokeStyle = gradient2;
    context.lineWidth = lineW1;
    context.arc(0, 0, R1, 0, startA, false);
    context.stroke();
    context.closePath();


    //内环
    context.beginPath();
    context.strokeStyle = circleBottomColor;
    context.lineWidth = lineW0;
    context.arc(0, 0, R, 0, Math.PI * 2, false);
    context.stroke();
    context.closePath();
    context.restore();

    //文字  进度
    var num = pos;
    if (num > percent * 100) {
        num = percent * 100;
    };
    if(num >= 10){
        ProcessPosX = 211;
    }

    context.beginPath();
    context.fillStyle = '#349dff';
    context.font = "bold 68px Verdana";
    context.fillText(num + "%", ProcessPosX, ProcessPosY);
    if(num > 0){
    	$("#lblProgress").html(num + "%");
    }   
    context.closePath();
    
    context.save();
    context.setTransform(1, 0, 0, 1, 0, 0);
    var ax = ra * Math.cos(startAa);
    var ay = ra * Math.sin(startAa);
    context.translate(posX + ax, posY + ay);
    context.rotate(startAa);
   
    context.restore();


}

