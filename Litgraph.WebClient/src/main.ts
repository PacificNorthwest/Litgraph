import Vue from "vue";
import Router from "vue-router"
import Axios from "axios"

import { vuexOidcCreateRouterMiddleware } from "vuex-oidc"

import AppComponent from "./components/App.vue"
import DashboardComponent from "./components/Dashboard.vue"
import OidcCallbackComponent from "./components/OidcCallback.vue"
import ErrorComponent from "./components/Error.vue"

import store from "./vuexStore"
Vue.use(Router);

let routes = [
    { path: "/", component: DashboardComponent, meta: { isPublic: false } },
    { path: "/oidc", component: OidcCallbackComponent, meta: { isPublic: true, isOidcCallback: true } },
    { path: "/error", component: ErrorComponent, meta: { isPublic: true }}
]

let router = new Router({routes, mode: 'history'})
router.beforeEach(vuexOidcCreateRouterMiddleware(store, 'identity'))

let v = new Vue({
    router,
    store,
    el: "#vue-app",
    render: h => h(AppComponent)
})