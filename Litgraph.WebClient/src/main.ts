import Vue from "vue";
import ElementUI from "element-ui"
import Router from "vue-router"
import "element-ui/lib/theme-chalk/index.css"


import AppComponent from "./components/App.vue"
import SignInComponent from "./components/SignIn.vue"
import SignUpComponent from "./components/SignUp.vue"
import DashboardComponent from "./components/Dashboard.vue"

Vue.use(ElementUI);
Vue.use(Router);

let routes = [
    { path: "/", component: DashboardComponent, meta: {requiresAuth: true} },
    { path: "/signin", component: SignInComponent, meta: {requiresAuth: false} },
    { path: "/signup", component: SignUpComponent, meta: {requiresAuth: false} }
]

let router = new Router({routes})

let v = new Vue({
    router,
    el: "#vue-app",
    render: h => h(AppComponent)
})