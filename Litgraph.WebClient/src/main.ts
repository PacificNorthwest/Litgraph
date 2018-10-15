import Vue from "vue";
import ElementUI from "element-ui"
import "element-ui/lib/theme-chalk/index.css"

import AppComponent from "./components/App.vue"

Vue.use(ElementUI);

let v = new Vue({
    el: "#vue-app",
    render: h => h(AppComponent)
})