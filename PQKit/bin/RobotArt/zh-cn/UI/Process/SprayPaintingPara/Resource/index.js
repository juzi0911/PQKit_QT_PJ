
/*
renjiahui
2017/6/27
学徒宝JS
*/

$(function () {
    $.ajaxSetup({ cache: false });
    // 手机版菜单栏
    $(window).on('load resize', function(event) {
        $(".nav-slide-button").click(function(event) {
            $(".neirong").addClass('neirong-show');
        });
        $(".neirong-close").click(function(event) {
           $(".neirong").removeClass('neirong-show');
        });
    });
    $(window).on('scroll', function(event) {
        if($(window).scrollTop()>0){
            $(".nav-slide-button").stop().addClass('show')
        }else{
            $(".nav-slide-button").stop().removeClass('show')
        }
    });

    $('.tab-nav li').click(function(){
      //alert('aa')
        $(this).addClass('zdsli-cur').siblings().removeClass('zdsli-cur') ;
    })


    $('.tab-nav li').click(function(){
        $('.tanI-con .con').hide();
        $('.tanI-con .con').eq($(this).index()).show();     
    })

    //机器人选择
    $('.jscur a').click(function(){
      //alert('aa')
        $(this).addClass('cur').siblings().removeClass('cur') ;
    })
    $('.overview > div').click(function(){
      //alert('aa')
        $(this).addClass('cur').siblings().removeClass('cur') ;
    })
    $('.f-sort li').click(function(){
      //alert('aa')
        $(this).addClass('cur').siblings().removeClass('cur') ;
    })
   
    //点击更多
    $('.more i.up').unbind("click").click(function () {
      
        $('.letter').fadeIn(800); 
        $('.scrollbar').css({'opacity':'1','top':'0'}); 
        $('.more i.down').show();
        $('.more i.up').hide();
    })
    $('.more i.down').unbind("click").click(function () {
        
        $('.letter').fadeOut(800); 
        $('.scrollbar').css('opacity','0'); 
		$('.thumb').css('top','0');
		$('.overview').css('top','0');
        $('.more i.down').hide();
        $('.more i.up').show();
    })
    //弹框
    $('.tan').click(function () {
      
        $('.tanbg').fadeIn(800); 
        $('.gl-itemTan').fadeIn(800); 
        $('body').css({'overflow-y':'hidden'});
        //$(".box").toggle(); 
        $('body').one('click',function(){
            $('.tanbg').fadeOut(800);
            $('.gl-itemTan').fadeOut(800);
            $('body').css({'overflow-y':'auto'});
        })
        return false;
    })
    $('b.guanbi').click(function () {
       
        $('.tanbg').fadeOut(800);
        $('.gl-itemTan').fadeOut(800);
        //$('body').css({'overflow-y':'auto'});
    })
    $('.gl-itemTan').click(function(){
        return false;
    });

    //左侧收起
    $(".one > span").click(function () {
       
        var a=$(this).attr('class');
        if(a=='now'){          
            $(".one > .now").removeClass('now');
            $(this).next('.open').slideToggle()
        }else{
            $(".one > .open").hide();
            $(this).next('.open').slideToggle();
            $(".one > .now").removeClass('now');
            $(this).toggleClass('now')
        }        
    });

  
    //小屏幕左侧导航
    $(".menu-img").click(function (event) {
        //alert('a');
        $(".menu-box").toggleClass("menu-show");
        //$('body').css({'overflow-y':'hidden'});
    });
    
    function Stick()
    {
     
        var times;
        clearInterval(times)
        times = setInterval(function () {
            var speed = $(document).scrollTop() / 10;
            speed = speed > 0 ? Math.ceil(speed) : Math.floor(speed);
            $(document).scrollTop($(document).scrollTop() - speed);
            if ($(document).scrollTop() <= 0) {
                clearInterval(times)
            }
        }, 10)
    }
    $("#help_zhi_dID").click(function () {
        Stick();
    });
    // 置顶
    $('#zhi_dID').click(function () {
       
        Stick();
    })
    window.onscroll = function () {
        var zhi_d = $(document).scrollTop();
        if (zhi_d > 10) {
            $('#zhi_dID').fadeIn(500);
        } else {
            $('#zhi_dID').fadeOut(500);
        }
    }
    //弹框登录
	//$('.logintan').click(function(){
    //    $('.tanbg').fadeIn(800); 
    //    $('.sa-logintan').fadeIn(800); 
    //    //$(".box").toggle(); 
    //    $('body').one('click',function(){
    //        $('.tanbg').fadeOut(800);
    //        $('.sa-logintan').fadeOut(800);
    //    })
    //    return false;
    //})
    $('b.guanbi').click(function () {
       
        $('.tanbg').fadeOut(800);
        $('.sa-logintan').fadeOut(800);
    })
    //$('.sa-logintan').click(function(){
    //    return false;
    //});
    //点赞
    $('.dz').click(function(){
        var ab=true;
        if(ab){
            $(this).find('b').css({'background':'url(./images/icon2.png) -31px -262px no-repeat'});
        }else{
            $(this).find('b').css({'background':'url(./images/icon2.png) -31px -137px no-repeat'});
        }
        ab=!ab;
    })
    //点击回复
    $('.hfjs').click(function(){
        $(this).parent('.top-2itme').next('.equally').fadeIn(800); 
        $(this).parent('.top-2itme').next('.equally').fadeIn(800); 
    })

})








