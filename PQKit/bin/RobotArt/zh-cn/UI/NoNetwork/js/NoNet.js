function RefreshUrl() {
    window.OnClickRefreshUrl();
    if (window.OnLineUrl != "" && typeof (window.OnLineUrl) != "undefined") {
        window.location.href = window.OnLineUrl;
    }
}

function ReLogin() {
    window.OnClickReLogin();

}
