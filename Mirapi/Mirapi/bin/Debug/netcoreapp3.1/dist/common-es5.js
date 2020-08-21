function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["common"], {
  /***/
  "./src/app/main/authentication/invitedRegister/inviteRegister.service.ts":
  /*!*******************************************************************************!*\
    !*** ./src/app/main/authentication/invitedRegister/inviteRegister.service.ts ***!
    \*******************************************************************************/

  /*! exports provided: InviteRegisterService */

  /***/
  function srcAppMainAuthenticationInvitedRegisterInviteRegisterServiceTs(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony export (binding) */


    __webpack_require__.d(__webpack_exports__, "InviteRegisterService", function () {
      return InviteRegisterService;
    });
    /* harmony import */


    var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
    /*! tslib */
    "./node_modules/tslib/tslib.es6.js");
    /* harmony import */


    var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
    /*! @angular/core */
    "./node_modules/@angular/core/fesm2015/core.js");
    /* harmony import */


    var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
    /*! @angular/router */
    "./node_modules/@angular/router/fesm2015/router.js");
    /* harmony import */


    var rxjs__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
    /*! rxjs */
    "./node_modules/rxjs/_esm2015/index.js");
    /* harmony import */


    var app_shared_services_httpRequests_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
    /*! app/shared/services/httpRequests.service */
    "./src/app/shared/services/httpRequests.service.ts");
    /* harmony import */


    var app_shared_services_alertify_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
    /*! app/shared/services/alertify.service */
    "./src/app/shared/services/alertify.service.ts");

    var InviteRegisterService = /*#__PURE__*/function () {
      /**
       * Constructor
       *
       * @param {HttpClient} _httpClient
       */
      function InviteRegisterService(_httpClient, _alertifyService, router) {
        _classCallCheck(this, InviteRegisterService);

        this._httpClient = _httpClient;
        this._alertifyService = _alertifyService;
        this.router = router; // Set the defaults

        this.onUserChanged = new rxjs__WEBPACK_IMPORTED_MODULE_3__["BehaviorSubject"]({});
      }
      /**
       * Resolver
       *
       * @param {ActivatedRouteSnapshot} route
       * @param {RouterStateSnapshot} state
       * @returns {Observable<any> | Promise<any> | any}
       */


      _createClass(InviteRegisterService, [{
        key: "resolve",
        value: function resolve(route, state) {
          var _this = this;

          this.routeParams = route.params;
          return new Promise(function (resolve, reject) {
            Promise.all([_this.getUserDetails()]).then(function () {
              resolve();
            }, reject);
          });
        }
        /**
         * Get product
         *
         * @returns {Promise<any>}
         */

      }, {
        key: "getUserDetails",
        value: function getUserDetails() {
          var _this2 = this;

          return new Promise(function (resolve, reject) {
            _this2._httpClient.get('Employee/verify/' + _this2.routeParams.id).subscribe(function (response) {
              _this2.user = response['data'];

              _this2.onUserChanged.next(_this2.user);

              resolve(response['data']);
            }, reject);
          });
          return null;
        }
      }, {
        key: "verifyCode",
        value: function verifyCode(code) {
          var _this3 = this;

          return new Promise(function (resolve, reject) {
            _this3._httpClient.get('Employee/verify/' + code).subscribe(function (response) {
              _this3.user = response['data'];

              _this3.onUserChanged.next(_this3.user);

              resolve(response['data']);

              _this3._alertifyService.warning(response['message']);
            }, reject);
          });
        }
      }, {
        key: "updatePassword",
        value: function updatePassword(userId, password) {
          var data = {
            newPass: password
          };
          return this._httpClient.post('Users/' + userId + "/password", data);
        }
        /**
         * Save product
         *
         * @param product
         * @returns {Promise<any>}
         */

      }, {
        key: "saveUser",
        value: function saveUser(user) {
          var _this4 = this;

          return new Promise(function (resolve, reject) {
            _this4._httpClient.put('User/' + _this4.routeParams.id, user).subscribe(function (response) {
              _this4._alertifyService.success('Başarıyla güncellendi.');

              resolve(response);

              _this4.router.navigateByUrl('/auth/login');
            }, reject);
          });
        }
      }]);

      return InviteRegisterService;
    }();

    InviteRegisterService.ctorParameters = function () {
      return [{
        type: app_shared_services_httpRequests_service__WEBPACK_IMPORTED_MODULE_4__["HttpRequestsService"]
      }, {
        type: app_shared_services_alertify_service__WEBPACK_IMPORTED_MODULE_5__["AlertifyService"]
      }, {
        type: _angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"]
      }];
    };

    InviteRegisterService = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])(), tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [app_shared_services_httpRequests_service__WEBPACK_IMPORTED_MODULE_4__["HttpRequestsService"], app_shared_services_alertify_service__WEBPACK_IMPORTED_MODULE_5__["AlertifyService"], _angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"]])], InviteRegisterService);
    /***/
  }
}]);
//# sourceMappingURL=common-es5.js.map