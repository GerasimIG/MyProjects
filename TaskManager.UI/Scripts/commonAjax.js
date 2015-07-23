var sendRequest = function (url, verb, data, successCallback, errorCallback, options) {

    var requestOptions = options || {};
    requestOptions.type = verb;
    requestOptions.success = successCallback;
    requestOptions.error = errorCallback;
    requestOptions.contentType = "application/json";

    if (!url || !verb) {
        errorCallback(401, "URL and HTTP verb required");
    }

    if (data) {
        requestOptions.data = data;
    }
    $.ajax(url, requestOptions);
}

var setDefaultCallbacks = function (successCallback, errorCallback) {
    $.ajaxSetup({
        complete: function (jqXhr) {
            if (jqXhr.status >= 200 && jqXhr.status < 300) {
                successCallback(jqXhr.responseJSON);
            } else {
                errorCallback(jqXhr.status, jqXhr.statusText);
            }
        }
    });
}

var setAjaxHeaders = function (requestHeaders) {
    $.ajaxSetup({ headers: requestHeaders });
}
