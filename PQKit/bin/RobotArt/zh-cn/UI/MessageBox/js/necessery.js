 var isDown = false;
 $(document).mousedown(function (e) {
     
     var downX = e.pageX
     var downY = e.pageY;

     if ((downY >= 246 && downY <= 281)) {
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
      window.oncontextmenu = function (e) {
          //取消默认的浏览器自带右键 很重要！！
          e.preventDefault();
      }
  });