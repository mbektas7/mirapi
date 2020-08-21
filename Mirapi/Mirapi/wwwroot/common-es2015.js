(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["common"],{

/***/ "./src/app/main/authentication/invitedRegister/inviteRegister.service.ts":
/*!*******************************************************************************!*\
  !*** ./src/app/main/authentication/invitedRegister/inviteRegister.service.ts ***!
  \*******************************************************************************/
/*! exports provided: InviteRegisterService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "InviteRegisterService", function() { return InviteRegisterService; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm2015/router.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm2015/index.js");
/* harmony import */ var app_shared_services_httpRequests_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! app/shared/services/httpRequests.service */ "./src/app/shared/services/httpRequests.service.ts");
/* harmony import */ var app_shared_services_alertify_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! app/shared/services/alertify.service */ "./src/app/shared/services/alertify.service.ts");






let InviteRegisterService = class InviteRegisterService {
    /**
     * Constructor
     *
     * @param {HttpClient} _httpClient
     */
    constructor(_httpClient, _alertifyService, router) {
        this._httpClient = _httpClient;
        this._alertifyService = _alertifyService;
        this.router = router;
        // Set the defaults
        this.onUserChanged = new rxjs__WEBPACK_IMPORTED_MODULE_3__["BehaviorSubject"]({});
    }
    /**
     * Resolver
     *
     * @param {ActivatedRouteSnapshot} route
     * @param {RouterStateSnapshot} state
     * @returns {Observable<any> | Promise<any> | any}
     */
    resolve(route, state) {
        this.routeParams = route.params;
        return new Promise((resolve, reject) => {
            Promise.all([
                this.getUserDetails()
            ]).then(() => {
                resolve();
            }, reject);
        });
    }
    /**
     * Get product
     *
     * @returns {Promise<any>}
     */
    getUserDetails() {
        return new Promise((resolve, reject) => {
            this._httpClient.get('Employee/verify/' + this.routeParams.id)
                .subscribe((response) => {
                this.user = response['data'];
                this.onUserChanged.next(this.user);
                resolve(response['data']);
            }, reject);
        });
        return null;
    }
    verifyCode(code) {
        return new Promise((resolve, reject) => {
            this._httpClient.get('Employee/verify/' + code)
                .subscribe((response) => {
                this.user = response['data'];
                this.onUserChanged.next(this.user);
                resolve(response['data']);
                this._alertifyService.warning(response['message']);
            }, reject);
        });
    }
    updatePassword(userId, password) {
        const data = { newPass: password };
        return this._httpClient.post('Users/' + userId + "/password", data);
    }
    /**
     * Save product
     *
     * @param product
     * @returns {Promise<any>}
     */
    saveUser(user) {
        return new Promise((resolve, reject) => {
            this._httpClient.put('User/' + this.routeParams.id, user)
                .subscribe((response) => {
                this._alertifyService.success('Başarıyla güncellendi.');
                resolve(response);
                this.router.navigateByUrl('/auth/login');
            }, reject);
        });
    }
};
InviteRegisterService.ctorParameters = () => [
    { type: app_shared_services_httpRequests_service__WEBPACK_IMPORTED_MODULE_4__["HttpRequestsService"] },
    { type: app_shared_services_alertify_service__WEBPACK_IMPORTED_MODULE_5__["AlertifyService"] },
    { type: _angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"] }
];
InviteRegisterService = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])(),
    tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [app_shared_services_httpRequests_service__WEBPACK_IMPORTED_MODULE_4__["HttpRequestsService"],
        app_shared_services_alertify_service__WEBPACK_IMPORTED_MODULE_5__["AlertifyService"],
        _angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"]])
], InviteRegisterService);



/***/ })

}]);
//# sourceMappingURL=common-es2015.js.map