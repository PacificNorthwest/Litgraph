import Vue from "vue";
import Router from "vue-router"
import Vuetify from "vuetify"
import VueApollo from "vue-apollo"
import "vuetify/dist/vuetify.min.css"
import "./styles/fonts.scss"

import { vuexOidcCreateRouterMiddleware } from "vuex-oidc"

import AppComponent from "./components/App.vue"
import DashboardComponent from "./components/Dashboard.vue"
import MaterialsCollectionComponent from "./components/MaterialsCollection.vue"
import OidcCallbackComponent from "./components/OidcCallback.vue"
import ErrorComponent from "./components/Error.vue"
import WelcomeComponent from "./components/Welcome.vue"

import store from "./vuexStore"
import apolloClient from "./apollo"

Vue.use(Vuetify);
Vue.use(Router);
Vue.use(VueApollo)

let routes = [
    { path: "/", component: WelcomeComponent, meta: { isPublic: true }},
    { path: "/dashboard", component: DashboardComponent, children: [
        { path: "", component: MaterialsCollectionComponent }
    ] },
    { path: "/oidc", component: OidcCallbackComponent, meta: { isPublic: true, isOidcCallback: true } },
    { path: "/error", component: ErrorComponent, meta: { isPublic: true }}
]

let router = new Router({ routes, mode: 'history' })
router.beforeEach(vuexOidcCreateRouterMiddleware(store, 'identity'))

const apolloProvider = new VueApollo({
    defaultClient: apolloClient
})

let v = new Vue({
    router,
    store,
    apolloProvider,
    el: "#vue-app",
    render: h => h(AppComponent)
})