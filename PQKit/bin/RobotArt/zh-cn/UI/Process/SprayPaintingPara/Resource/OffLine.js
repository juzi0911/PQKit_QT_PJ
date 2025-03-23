$(document).ready(function () {
    // 插入的弹窗
    // $(".jdEms").click(function () {
    // })
    $.ajaxSetup({ cache: false });
    RegisterResEvent();
    RegisterEscEvent();

   
});


  $(function () {
      window.oncontextmenu = function (e) {
          e.preventDefault();
      }
  });


function RegisterEscEvent() {
    document.onkeydown = function (ev) {
        var oEvent = ev || event;//获取事件对象(IE和其他浏览器不一样，这里要处理一下浏览器的兼容性event是IE；ev是chrome等)
        //Esc键的keyCode是27
        if (oEvent.keyCode == 27) {
            window.close();
        }
    }
}
function RegisterResEvent() {
    
    // 点击弹窗的取消按钮的事件
    $(".toolOff").click(function () {
            $('.tanbg').fadeOut(800);
            $('.gl-itemTan4').fadeOut(800);
    })
    // 点击弹窗的确定按钮的事件
    $(".toolEnsure").click(function () {
            $('.tanbg').fadeOut(800);
            $('.gl-itemTan4').fadeOut(800);
    })
}

function SetResDetailValues(obj) {
  
    var tanDiv = $(".gl-itemTan");
    tanDiv.find("img[name='defaultPic']").attr("src", obj.attr("defaultPic"));

    var divPicHtml = obj.siblings().filter("div[name='ResPics']").html();
    $("#imageMenu").html(divPicHtml);
  
    var btnText = obj.next().text();
    var $ResBtn = tanDiv.find("a[name='ResBtn']");
    $ResBtn.text(btnText);
    var divParasHtml = obj.siblings().filter("div[name='ResParas']").html();
    $ResBtn.siblings().remove();
    $ResBtn.before(divParasHtml);
    var xmlStr = obj.siblings().filter("div[name='ResXml']").text();
    $ResBtn.data("ResXml", xmlStr);
 
  
  
}
function InsertOfflineRes(obj) {
    
    var btn = $(obj);
    window.XML = btn.data("ResXml");
    window.InsertModelOffline(window.XML);
}
function DoSearch()
{
	
    var key = $.trim($("#txtSearchTxt").val()).toLowerCase();
	
    if (key == "")
    {
        renderList(RobotAllLst);
        return;
    }
    var tempRobotArray=[];
    $.each(RobotAllLst, function (index,item) {
        var model = item.Name.toLowerCase();
        if (model.indexOf(key) >= 0)
        {
            tempRobotArray.push(item);
        }
    });
    renderList(tempRobotArray);
}

window.XML = "";
function renderList(list)
{
    var html = "";
    $.each(list, function (index, item) {

         html += '<LI class="gl-item" style="height: 300px;">' +
            '<A href="javascript:;">' +
            '<DIV class="tan" oid="' + item.Model + '">' +
            '<IMG width="173" height="175" src="./' + item.Image + '" >' +
            '<SPAN>' + item.Name + '</SPAN>' +
            '</DIV>' +
            '<EM class="jd" oid="' + item.Model + '">插入</EM>' +
            '<DIV style="display: none;" name="ResXml">' +
            '&lt;?xml version="1.0"?&gt;' +
            '&lt;modelinfo&gt;' +
            '	&lt;guid&gt;' + item.Model + '&lt;/guid&gt;' +
            '	&lt;url/&gt;' +
            '	&lt;md5/&gt;' +
            '	&lt;propertyinfo&gt;' +
            '		&lt;property unit="" type="String" value="' + item.Name + '" name="资源名称" id="RO_ATTRIB_RES_NAME"/&gt;' +
            '	&lt;/propertyinfo&gt;' +
            '	&lt;pic_url/&gt;' +
            '&lt;/modelinfo&gt;' +
            '</DIV>' +
            '</A></LI>';

    });
    $(".gl-warp").empty();
    $('.gl-warp').append(html);
	if(typeof(list)!="undefined"&&list!=null&&list.length>0)
	{
		
		$('.jd').unbind("click").click(function () {
			var currentInsertButton = $(this);
			//var id = currentInsertButton.attr("oid");
			//window.XML = id;
		    //window.InsertModelOffline(window.XML);
			var xmlStr = currentInsertButton.siblings().filter("div[name='ResXml']").text();
			window.InsertModelOffline(xmlStr);
		});
			
		
	}
}
var RobotAllLst;
function renderView(data) {
   
        var jsonObj = JSON.parse(data);
        RobotAllLst = jsonObj.LibraryInfo
        renderList(RobotAllLst);
    }

 