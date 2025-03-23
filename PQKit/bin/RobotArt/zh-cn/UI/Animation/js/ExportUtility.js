$(document).ready(function () {
    $.ajaxSetup({ cache: false });
    RegisterEscEvent();
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

