function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["main-dashboard-dashboard-module"], {
  /***/
  "./node_modules/raw-loader/dist/cjs.js!./src/app/main/dashboard/dashboard.component.html":
  /*!***********************************************************************************************!*\
    !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/main/dashboard/dashboard.component.html ***!
    \***********************************************************************************************/

  /*! exports provided: default */

  /***/
  function node_modulesRawLoaderDistCjsJsSrcAppMainDashboardDashboardComponentHtml(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony default export */


    __webpack_exports__["default"] = "<div class=\"page-layout simple tabbed\">\n\n    <!-- HEADER -->\n    <!--<div class=\"header p-24\" fxLayout=\"column\" fxLayoutAlign=\"center center\" fxLayout.gt-sm=\"row\"\n         fxLayoutAlign.gt-sm=\"space-between end\">\n\n        <div class=\"user-info\" fxLayout=\"column\" fxLayoutAlign=\"center center\" fxLayout.gt-sm=\"row\"\n             fxLayoutAlign.gt-sm=\"start center\">\n            <div class=\"name\" [@animate]=\"{value:'*',params:{delay:'100ms',x:'-25px'}}\">\n            </div>\n        </div>\n\n\n    </div>\n    -->\n    <!-- / HEADER -->\n\n    <!-- CONTENT -->\n    <div class=\"content\">\n\n\n    </div>\n    <!-- / CONTENT -->\n\n</div>";
    /***/
  },

  /***/
  "./src/app/main/dashboard/dashboard.component.scss":
  /*!*********************************************************!*\
    !*** ./src/app/main/dashboard/dashboard.component.scss ***!
    \*********************************************************/

  /*! exports provided: default */

  /***/
  function srcAppMainDashboardDashboardComponentScss(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony default export */


    __webpack_exports__["default"] = "table {\n  width: 100%; }\n\n.checkinBtn {\n  position: absolute;\n  right: 1px;\n  bottom: 1px;\n  padding: 1px 1px;\n  width: 100px;\n  height: 100px;\n  background-color: green; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvbWFpbi9kYXNoYm9hcmQvQzpcXFVzZXJzXFxNaXJhY1xcRGVza3RvcFxcQW5ndWxhciBUZW1wbGF0ZS9zcmNcXGFwcFxcbWFpblxcZGFzaGJvYXJkXFxkYXNoYm9hcmQuY29tcG9uZW50LnNjc3MiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBSUE7RUFDSSxXQUFXLEVBQUE7O0FBSWY7RUFDSSxrQkFBa0I7RUFDbEIsVUFBVTtFQUNWLFdBQVU7RUFDVixnQkFBZTtFQUNmLFlBQVk7RUFDWixhQUFZO0VBQ1osdUJBQXVCLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9tYWluL2Rhc2hib2FyZC9kYXNoYm9hcmQuY29tcG9uZW50LnNjc3MiLCJzb3VyY2VzQ29udGVudCI6WyIuYm94e1xuICAgIFxufVxuXG50YWJsZSB7XG4gICAgd2lkdGg6IDEwMCU7XG4gIH1cblxuXG4uY2hlY2tpbkJ0bntcbiAgICBwb3NpdGlvbjogYWJzb2x1dGU7XG4gICAgcmlnaHQ6IDFweDtcbiAgICBib3R0b206MXB4O1xuICAgIHBhZGRpbmc6MXB4IDFweDtcbiAgICB3aWR0aDogMTAwcHg7XG4gICAgaGVpZ2h0OjEwMHB4O1xuICAgIGJhY2tncm91bmQtY29sb3I6IGdyZWVuO1xufSJdfQ== */";
    /***/
  },

  /***/
  "./src/app/main/dashboard/dashboard.component.ts":
  /*!*******************************************************!*\
    !*** ./src/app/main/dashboard/dashboard.component.ts ***!
    \*******************************************************/

  /*! exports provided: DashboardComponent */

  /***/
  function srcAppMainDashboardDashboardComponentTs(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony export (binding) */


    __webpack_require__.d(__webpack_exports__, "DashboardComponent", function () {
      return DashboardComponent;
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


    var app_shared_services_auth_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
    /*! app/shared/services/auth.service */
    "./src/app/shared/services/auth.service.ts");
    /* harmony import */


    var _mirapi_components_navigation_navigation_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
    /*! @mirapi/components/navigation/navigation.service */
    "./src/@mirapi/components/navigation/navigation.service.ts");
    /* harmony import */


    var app_shared_services_httpRequests_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
    /*! app/shared/services/httpRequests.service */
    "./src/app/shared/services/httpRequests.service.ts");
    /* harmony import */


    var app_navigation_navigation__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
    /*! app/navigation/navigation */
    "./src/app/navigation/navigation.ts");
    /* harmony import */


    var _angular_router__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
    /*! @angular/router */
    "./node_modules/@angular/router/fesm2015/router.js");

    var DashboardComponent = /*#__PURE__*/function () {
      function DashboardComponent(authService, mirapiNavigationService, httpRequestService, router) {
        _classCallCheck(this, DashboardComponent);

        this.authService = authService;
        this.mirapiNavigationService = mirapiNavigationService;
        this.httpRequestService = httpRequestService;
        this.router = router;
        this.lastEntranceAndExits = [];
        this.displayedColumns = ['Giriş Zamanı', 'Çıkış Zamanı', 'Gün', 'Çalışılması Gereken Süre', 'Çalışılan Süre'];
        this.userId = this.authService.getCurrentUserId(); // this.getEmployees();
      }

      _createClass(DashboardComponent, [{
        key: "ngOnInit",
        value: function ngOnInit() {
          return tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"](this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee() {
            return regeneratorRuntime.wrap(function _callee$(_context) {
              while (1) {
                switch (_context.prev = _context.next) {
                  case 0:
                  case "end":
                    return _context.stop();
                }
              }
            }, _callee);
          }));
        }
      }, {
        key: "getFiveDaysAgoFromNow",
        value: function getFiveDaysAgoFromNow() {
          var date = new Date();
          date.setDate(date.getDate() - 7);
          return date;
        }
      }, {
        key: "setNavigations",
        value: function setNavigations() {
          return tslib__WEBPACK_IMPORTED_MODULE_0__["__awaiter"](this, void 0, void 0, /*#__PURE__*/regeneratorRuntime.mark(function _callee2() {
            return regeneratorRuntime.wrap(function _callee2$(_context2) {
              while (1) {
                switch (_context2.prev = _context2.next) {
                  case 0:
                    this.navigation = app_navigation_navigation__WEBPACK_IMPORTED_MODULE_5__["navigation"].slice();
                    _context2.next = 3;
                    return this.removeUnauthorizedUrlsFromNavigation(this.navigation);

                  case 3:
                    this.navigation = _context2.sent;
                    this.mirapiNavigationService.unregister('main');
                    this.navigation = this.mirapiNavigationService.register('main', this.navigation.slice()); // Set the main navigation as our current navigation

                    this.mirapiNavigationService.setCurrentNavigation('main');

                  case 7:
                  case "end":
                    return _context2.stop();
                }
              }
            }, _callee2, this);
          }));
        }
      }, {
        key: "removeUnauthorizedUrlsFromNavigation",
        value: function removeUnauthorizedUrlsFromNavigation(array) {
          for (var i = 0; i < array.length; i++) {
            var value = array[i];

            if (value.url) {} else if (value.children) {
              value.children = this.removeUnauthorizedUrlsFromNavigation(value.children);

              if (!value.url && value.children.length === 0) {
                array[i] = null;
              }
            }
          }

          return array.filter(function (a) {
            return a;
          });
        }
      }]);

      return DashboardComponent;
    }();

    DashboardComponent.ctorParameters = function () {
      return [{
        type: app_shared_services_auth_service__WEBPACK_IMPORTED_MODULE_2__["AuthService"]
      }, {
        type: _mirapi_components_navigation_navigation_service__WEBPACK_IMPORTED_MODULE_3__["MirapiNavigationService"]
      }, {
        type: app_shared_services_httpRequests_service__WEBPACK_IMPORTED_MODULE_4__["HttpRequestsService"]
      }, {
        type: _angular_router__WEBPACK_IMPORTED_MODULE_6__["Router"]
      }];
    };

    DashboardComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
      selector: 'app-dashboard',
      template: tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(
      /*! raw-loader!./dashboard.component.html */
      "./node_modules/raw-loader/dist/cjs.js!./src/app/main/dashboard/dashboard.component.html"))["default"],
      styles: [tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(
      /*! ./dashboard.component.scss */
      "./src/app/main/dashboard/dashboard.component.scss"))["default"]]
    }), tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [app_shared_services_auth_service__WEBPACK_IMPORTED_MODULE_2__["AuthService"], _mirapi_components_navigation_navigation_service__WEBPACK_IMPORTED_MODULE_3__["MirapiNavigationService"], app_shared_services_httpRequests_service__WEBPACK_IMPORTED_MODULE_4__["HttpRequestsService"], _angular_router__WEBPACK_IMPORTED_MODULE_6__["Router"]])], DashboardComponent);
    /***/
  },

  /***/
  "./src/app/main/dashboard/dashboard.module.ts":
  /*!****************************************************!*\
    !*** ./src/app/main/dashboard/dashboard.module.ts ***!
    \****************************************************/

  /*! exports provided: DashboardModule */

  /***/
  function srcAppMainDashboardDashboardModuleTs(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony export (binding) */


    __webpack_require__.d(__webpack_exports__, "DashboardModule", function () {
      return DashboardModule;
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


    var _angular_common__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
    /*! @angular/common */
    "./node_modules/@angular/common/fesm2015/common.js");
    /* harmony import */


    var _dashboard_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
    /*! ./dashboard.component */
    "./src/app/main/dashboard/dashboard.component.ts");
    /* harmony import */


    var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
    /*! @angular/router */
    "./node_modules/@angular/router/fesm2015/router.js");
    /* harmony import */


    var _angular_material_button__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
    /*! @angular/material/button */
    "./node_modules/@angular/material/esm2015/button.js");
    /* harmony import */


    var _angular_material_divider__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
    /*! @angular/material/divider */
    "./node_modules/@angular/material/esm2015/divider.js");
    /* harmony import */


    var _angular_material_icon__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(
    /*! @angular/material/icon */
    "./node_modules/@angular/material/esm2015/icon.js");
    /* harmony import */


    var _angular_material_table__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(
    /*! @angular/material/table */
    "./node_modules/@angular/material/esm2015/table.js");
    /* harmony import */


    var _mirapi_shared_module__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(
    /*! @mirapi/shared.module */
    "./src/@mirapi/shared.module.ts");
    /* harmony import */


    var enums_pageTypes_enum__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(
    /*! enums/pageTypes.enum */
    "./src/enums/pageTypes.enum.ts");
    /* harmony import */


    var app_shared_guards_login_guard__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(
    /*! app/shared/guards/login.guard */
    "./src/app/shared/guards/login.guard.ts");

    var routes = [{
      path: '',
      component: _dashboard_component__WEBPACK_IMPORTED_MODULE_3__["DashboardComponent"],
      canActivate: [app_shared_guards_login_guard__WEBPACK_IMPORTED_MODULE_11__["LoginGuard"]],
      data: {
        pageType: enums_pageTypes_enum__WEBPACK_IMPORTED_MODULE_10__["PageClaims"].dashboard
      }
    }];

    var DashboardModule = function DashboardModule() {
      _classCallCheck(this, DashboardModule);
    };

    DashboardModule = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
      imports: [_angular_common__WEBPACK_IMPORTED_MODULE_2__["CommonModule"], _angular_router__WEBPACK_IMPORTED_MODULE_4__["RouterModule"].forChild(routes), _angular_material_button__WEBPACK_IMPORTED_MODULE_5__["MatButtonModule"], _angular_material_divider__WEBPACK_IMPORTED_MODULE_6__["MatDividerModule"], _angular_material_icon__WEBPACK_IMPORTED_MODULE_7__["MatIconModule"], _mirapi_shared_module__WEBPACK_IMPORTED_MODULE_9__["MirapiSharedModule"], _angular_material_table__WEBPACK_IMPORTED_MODULE_8__["MatTableModule"]],
      declarations: [_dashboard_component__WEBPACK_IMPORTED_MODULE_3__["DashboardComponent"]]
    })], DashboardModule);
    /***/
  }
}]);
//# sourceMappingURL=main-dashboard-dashboard-module-es5.js.map