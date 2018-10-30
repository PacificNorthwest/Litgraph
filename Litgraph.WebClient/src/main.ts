import Vue from "vue";
import ElementUI from "element-ui"
import Router from "vue-router"
import Axios from "axios"
import "element-ui/lib/theme-chalk/index.css"

import AppComponent from "./components/App.vue"
import SignInComponent from "./components/SignIn.vue"
import SignUpComponent from "./components/SignUp.vue"
import DashboardComponent from "./components/Dashboard.vue"

import Store from "./vuexStore"

Vue.use(ElementUI);
Vue.use(Router);

let routes = [
    { path: "/", component: DashboardComponent, meta: {requiresAuth: true} },
    { path: "/signin", component: SignInComponent, meta: {requiresAuth: false} },
    { path: "/signup", component: SignUpComponent, meta: {requiresAuth: false} }
]
let router = new Router({routes})
router.beforeEach((to, from, next) => {
    if (to.matched.some(record => record.meta.requiresAuth)) {
        if (Store.getters['identity/isLoggedIn']) {
            next();
            return;
        }
        next('/signin');
    } else {
        next();
    }
})

if (Store.getters['identity/isLoggedIn']) {
    Axios.defaults.headers.common['Authorization'] = 'Bearer ' + Store.getters['identity/token'];
}

let v = new Vue({
    router,
    store: Store,
    el: "#vue-app",
    render: h => h(AppComponent)
})