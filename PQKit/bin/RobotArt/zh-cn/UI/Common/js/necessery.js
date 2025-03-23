 var isDown = false;
 $(document).mousedown(function (e) {
     
     var downX = e.pageX
     var downY = e.pageY;

     if (downX >= 333 && downX <= 662 && downY >= 99 && downY <= 235){
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
  
