 var isDown = false;
 $(document).mousedown(function (e) {
     
     var downX = e.pageX
     var downY = e.pageY;

     if (/*(downX >= 106 && downX <= 369) ||*/ (downY >= 246 && downY <= 281)) {
         isDown = false;
     }
     else{
         isDown = true; 
     }
     if (isDown){
         window.RoInnerLButtonDown();     //调c++方法 
     }
 });
 $(document).mouseup(function () {
     isDown = false;
     window.RoInnerLButtonUp();
 });       
  $(document).mousemove(function (e) {
      if (isDown) {
          //调c++方法
          window.RoInnerMouseMove();       
      }
  });
  


  $(function () {
      //window.RoInnerSetStringValue(SetStringValue);
      //window.RoInnerGetStringValue(GetStringValue);
      //window.RoInnerSetBoolValue(SetBoolValue);
      //window.RoInnerGetBoolValue(GetBoolValue); 
      //window.OnLoad();
  });