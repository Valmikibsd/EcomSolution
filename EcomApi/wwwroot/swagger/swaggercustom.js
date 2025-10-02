window.onload = function () {
    if (window.ui) {
        window.ui.getConfigs().requestInterceptor = function (request) {
            if (request.headers.Authorization && !request.headers.Authorization.startsWith("Bearer ")) {
                request.headers.Authorization = "Bearer " + request.headers.Authorization;
            }
            return request;
        };
    }
};
